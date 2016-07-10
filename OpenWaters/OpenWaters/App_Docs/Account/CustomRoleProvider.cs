using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Configuration;
using OpenEnvironment.App_Logic.DataAccessLayer;


namespace OpenEnvironment.Account
{
    public class CustomRoleProvider : RoleProvider
    {

        private bool pWriteExceptionsToEventLog = false;
        public bool WriteExceptionsToEventLog
        {
            get { return pWriteExceptionsToEventLog; }
            set { pWriteExceptionsToEventLog = value; }
        }


        public override string ApplicationName
        {
            get { return "OE"; }
            set { throw new NotImplementedException(); }
        }


        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            if (config == null)
                throw new ArgumentNullException("config");

            if (name == null || name.Length == 0)
                name = "OERoleProvider";

            if (String.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "OE Role provider");
            }

            base.Initialize(name, config);
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }


        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }


        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }


        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }


        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            string tmpRoleNames = "";

            int UserID = Convert.ToInt32(db_Accounts.GetT_OE_USERSByID(username).USER_IDX.ToString());

            foreach (T_OE_ROLES r in db_Accounts.GetT_OE_ROLESInUser(UserID))
                tmpRoleNames += r.ROLE_NAME + ",";

            if (tmpRoleNames.Length > 0)
            {
                tmpRoleNames = tmpRoleNames.Substring(0, tmpRoleNames.Length - 1); // Remove trailing comma.
                return tmpRoleNames.Split(',');
            }

            return new string[0];

        }


        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }


        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }



        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }




    }
}