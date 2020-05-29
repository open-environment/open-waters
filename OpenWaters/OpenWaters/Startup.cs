using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using System.Threading.Tasks;
using OpenEnvironment.App_Logic.DataAccessLayer;
using Microsoft.Owin.Extensions;

namespace OpenEnvironment
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //IdentityServer configuration settings
            if (ConfigurationManager.AppSettings["UseIdentityServer"] == "true")
            {
                //*********************************************************************************************************
                //db_Ref.InsertT_OE_SYS_LOG("DEBUG", "starting up auth");

                JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();

                app.UseCookieAuthentication(new CookieAuthenticationOptions
                {
                    AuthenticationType = "Cookies",
                    ExpireTimeSpan = System.TimeSpan.FromMinutes(60),
                });


                app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
                {
                    //IMPLICIT 
                    ClientId = "open_waters",
                    Authority = ConfigurationManager.AppSettings["IdentityServerAuthority"],      //"http://localhost:3892/",  //ID Server
                    RedirectUri = ConfigurationManager.AppSettings["IdentityServerRedirectURI"],  //"http://localhost:1244/signinoidc",  , // 
                    PostLogoutRedirectUri = ConfigurationManager.AppSettings["IdentityServerPostLogoutURI"], //"http://localhost:1244/signoutcallbackoidc",
                    ResponseType = "id_token",
                    UseTokenLifetime = false,
                    //CallbackPath = new Microsoft.Owin.PathString("/home/index/"),  // Critical to prevent infinite loop**


                    SignInAsAuthenticationType = "Cookies",
                    Scope = "openid profile email",

                    TokenValidationParameters = {
                        NameClaimType = "name"
                    },
                    Notifications = new OpenIdConnectAuthenticationNotifications()
                    {
                        SecurityTokenValidated = (context) =>
                        {
                            //*********************************************************************************************************
                            //db_Ref.InsertT_OE_SYS_LOG("DEBUG", "validating user");

                            //grab information about User
                            ClaimsIdentity _identity = context.AuthenticationTicket.Identity;
                            var UserID_portal = _identity.FindFirst("sub").Value;
                            int UserIDX = 0;

                            //check if user with this email already in system
                            T_OE_USERS t = db_Accounts.GetT_VCCB_USERByEmail(_identity.Name);
                            if (t == null)
                            {
                                db_Ref.InsertT_OE_SYS_LOG("DEBUG", "No user with email exists - creating with ID=[" + _identity.Name + "]");

                                //insert new USERS table if not yet there
                                UserIDX = db_Accounts.CreateT_OE_USERS(_identity.Name, "unused", "unused", "temp", "temp", _identity.Name, true, false, System.DateTime.Now, null, null, "portal");

                                db_Ref.InsertT_OE_SYS_LOG("DEBUG", "New User created IDX" + UserIDX);

                                //Add user to GENERAL USER Role
                                if (UserIDX > 0)
                                    db_Accounts.CreateT_VCCB_USER_ROLE(3, UserIDX, "system");
                            }
                            else
                            {
                                //update existing user record
                                UserIDX = t.USER_IDX;

                                //switch "User.Identity.Name" to the username
                                context.AuthenticationTicket.Identity.RemoveClaim(_identity.FindFirst("name"));
                                Claim nameClaim = new Claim("name", t.USER_ID, ClaimValueTypes.String, "LocalAuthority");
                                context.AuthenticationTicket.Identity.AddClaim(nameClaim);
                            }


                            if (UserIDX > 0)
                            {
                                //now add UserIDX to claims 
                                Claim userIDXClaim = new Claim("UserIDX", UserIDX.ToString(), ClaimValueTypes.Integer, "LocalAuthority");
                                context.AuthenticationTicket.Identity.AddClaim(userIDXClaim);
                            }
                            else
                                throw new System.IdentityModel.Tokens.SecurityTokenValidationException();


                            //delete all orgs for this user to Inactive
                            db_WQX.DeleteT_WQX_USER_ORGS_AllByUserIDX(UserIDX);

                            //now handling jurisdiction associations
                            var authorizedOrgs = _identity.FindAll("open_waters");

                            foreach (var org in authorizedOrgs)
                            {
                                string[] org_array = org.Value.Split(';');

                                T_WQX_ORGANIZATION o = db_WQX.GetWQX_ORGANIZATION_ByID(org_array[0]);
                                if (o != null)
                                {
                                    db_WQX.InsertT_WQX_USER_ORGS(o.ORG_ID, UserIDX, org_array[1] == "True" ? "A" : "U");

                                    //set their default OPEN WATERS ORG ID (assuming vast majority of users only have rights to 1 org)
                                    db_Accounts.UpdateT_OE_USERSDefaultOrg(UserIDX, o.ORG_ID);
                                }
                            }

                            return Task.FromResult(0);
                        }
                    }

                });

                app.UseStageMarker(PipelineStage.Authenticate);
            }

        }

    }
}