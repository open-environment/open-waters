using System;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;
using OpenEnvironment.App_Logic.BusinessLogicLayer;
using OpenEnvironment.App_Logic.DataAccessLayer;
using System.Linq;
using System.Collections.Generic;

namespace OpenEnvironment.Account
{
    public class CustMembershipProvider : MembershipProvider
    {
        //public properties
        private bool _EnablePasswordRetrieval;
        private bool _EnablePasswordReset = true;
        private int _MaxInvalidPasswordAttempts;
        private int _PasswordAttemptWindow;
        private int _MinRequiredPasswordLength;
        private int _MinRequiredNonalphanumericCharacters;
        private MembershipPasswordFormat _PasswordFormat;
        private bool _RequiresQuestionAndAnswer = false;
        private bool _RequiresUniqueEmail;

        public override bool EnablePasswordRetrieval { get { return _EnablePasswordRetrieval; } }
        public override bool EnablePasswordReset { get { return _EnablePasswordReset; } }
        public override int MaxInvalidPasswordAttempts { get { return _MaxInvalidPasswordAttempts; } }
        public override int MinRequiredNonAlphanumericCharacters { get { return _MinRequiredNonalphanumericCharacters; } }
        public override int MinRequiredPasswordLength { get { return _MinRequiredPasswordLength; } }
        public override int PasswordAttemptWindow { get { return _PasswordAttemptWindow; } }
        public override MembershipPasswordFormat PasswordFormat { get { return _PasswordFormat; } }
        public override string PasswordStrengthRegularExpression { get { return null; } }
        public override bool RequiresUniqueEmail { get { return _RequiresUniqueEmail; } }
        public override bool RequiresQuestionAndAnswer { get { return _RequiresQuestionAndAnswer; } }
        public override string ApplicationName
        {
            get { return "OE"; }
            set { throw new NotImplementedException(); }
        }

        // Initialize methods
        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            base.Initialize(name, config);

            _EnablePasswordRetrieval = false;
            _EnablePasswordReset = true;
            _RequiresQuestionAndAnswer = false;
            _RequiresUniqueEmail = true;
            _MaxInvalidPasswordAttempts = 5;
            _PasswordAttemptWindow = 10;
            _MinRequiredPasswordLength = 8;
            _MinRequiredNonalphanumericCharacters = 0;
            _PasswordFormat = MembershipPasswordFormat.Hashed;

        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            //validate new password length
            if (!Utils.ValidateParameter(ref newPassword, true, true, false, 0, _MinRequiredPasswordLength))
                return false;

            //Validate Non-AlphaNumeric characters
            char[] charpwd = newPassword.ToCharArray();
            int pwdNonNumericCount = 0;
            for (int i = 1; i < newPassword.Length; i++)
            {
                if (!char.IsLetterOrDigit(charpwd[i]))
                    pwdNonNumericCount += 1;
            }

            if (pwdNonNumericCount < _MinRequiredNonalphanumericCharacters)
                return false;


            using (OpenEnvironmentEntities context = new OpenEnvironmentEntities())
            {
                T_OE_USERS u = db_Accounts.GetT_OE_USERSByID(username);
                if (u != null)
                {
                    //first check accuracy of old password
                    if (!CheckPassword(oldPassword, u.PWD_HASH, u.PWD_SALT))
                        return false;

                    //generate new password
                    string salt = GenerateSalt();
                    string hashpass = HashPassword(newPassword, MembershipPasswordFormat.Hashed, salt);
                    //save updated information
                    if (db_Accounts.UpdateT_OE_USERS(u.USER_IDX, hashpass, salt, null, null, null, null, false, null, null, null, null, "system") > 0)
                        return true;
                    else
                        return false;
                }
            }

            return true;
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            status = MembershipCreateStatus.Success;

            //******************************** BEGIN VALIDATION ********************************************************
            //Validate Username Length
            if (!Utils.ValidateParameter(ref username, true, true, true, 25))
            {
                status = MembershipCreateStatus.InvalidUserName;
                return null;
            }
            
            T_OE_USERS u = db_Accounts.GetT_OE_USERSByID(username);            
            if (u != null)            
            {            
                //Duplicate username found -return error                
                status = MembershipCreateStatus.DuplicateUserName;                
                return null;                
            }

            if (Utils.IsEmail(email) == false)
            {
                status = MembershipCreateStatus.InvalidEmail;
                return null;
            }
            //******************************** END VALIDATION ***********************************************************


            try
            {
                //Generate password and hash it
                password = RandomString(10); 
                string salt = GenerateSalt();
                string hashpass = HashPassword(password, MembershipPasswordFormat.Hashed, salt);

                //create user record
                int createUser = db_Accounts.CreateT_OE_USERS(username, hashpass, salt, "", "", email, true, true, null, null, null, "system");
                if (createUser > 0)  //Success
                {
                    //Add user to PUBLIC Role
                    db_Accounts.CreateT_VCCB_USER_ROLE(3, createUser, "system");

                    //encrypt username for email
                    string encryptOauth = new SimpleAES().Encrypt(password + "||" + username);
                    encryptOauth = System.Web.HttpUtility.UrlEncode(encryptOauth);

                    //send verification email to user
                    string message = "Welcome to Open Waters. Open Waters allows you to manage your water quality data and synchronize it with EPA-WQX.  "
                        + "\r\n\r\n Your username is: " + username
                        + "\r\n\r\n You must activate your account by clicking the following link: "
                        + "\r\n\r\n " + db_Ref.GetT_OE_APP_SETTING("Public App Path") + "Account/Verify.aspx?oauthcrd=" + encryptOauth
                        + "\r\n\r\n After verifying your account you will be prompted to enter a permanent password.";

                    
                    bool EmailStatus = Utils.SendEmail(null, email.Split(';').ToList(), null, null, "Confirm Your Open Waters Account", message, null);
                    if (EmailStatus == false)
                    {
                        status = MembershipCreateStatus.InvalidEmail;
                        db_Accounts.DeleteT_OE_USERS(createUser);
                    }

                    //if enabled, send email to admin notifying of account creation
                    if (db_Ref.GetT_OE_APP_SETTING("Notify Register") == "Y")
                    {
                        T_OE_USERS adm = db_Accounts.GetT_OE_USERSInRole(2).FirstOrDefault();
                        if (adm != null)
                        {
                            Utils.SendEmail(null, adm.EMAIL.Split(';').ToList(), null, null, "Notification: Open Waters Account", "An Open Waters account has just been created by " + username + " (" + email + ")", null);
                        }
                    }

                    return new MembershipUser("CustMembershipProvider", username, createUser, email, passwordQuestion, null, isApproved, false, System.DateTime.Now, System.DateTime.Now, System.DateTime.Now, System.DateTime.Now, System.DateTime.Now);
                }
                else
                {
                    status = MembershipCreateStatus.ProviderError;
                    return null;
                }

            }
            catch (Exception exp)
            {
                throw exp;
            }

        }

        protected override byte[] DecryptPassword(byte[] encodedPassword)
        {
            return base.DecryptPassword(encodedPassword);
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            T_OE_USERS u = db_Accounts.GetT_OE_USERSByIDX((int)providerUserKey);

            var newCreateDate = u.CREATE_DT ?? System.DateTime.Now;
            var newModifyDate = u.MODIFY_DT ?? System.DateTime.Now;

            return new MembershipUser(this.Name, u.USER_ID, u.USER_IDX, u.EMAIL, null, null, true, false, newCreateDate, newModifyDate, newModifyDate, newModifyDate, newModifyDate);
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            T_OE_USERS u = db_Accounts.GetT_OE_USERSByID(username);
            if (u != null)
            {
                var newCreateDate = u.CREATE_DT ?? System.DateTime.Now;
                var newModifyDate = u.MODIFY_DT ?? System.DateTime.Now;

                return new MembershipUser(this.Name, u.USER_ID, u.USER_IDX, u.EMAIL, null, null, true, false, newCreateDate, newModifyDate, newModifyDate, newModifyDate, newModifyDate);
            }
            else
                return null;

        }

        public override string GetUserNameByEmail(string email)
        {
            T_OE_USERS u = db_Accounts.GetT_VCCB_USERByEmail(email);

            if (u != null)
                return u.USER_ID;
            else
                return null;
        }

        public override string ResetPassword(string username, string answer)
        {
            using (OpenEnvironmentEntities context = new OpenEnvironmentEntities())
            {
                T_OE_USERS u = db_Accounts.GetT_OE_USERSByID(username);
                if (u != null)
                {
                    //generate new password
                    string newPass = RandomString(8);
                    string salt = GenerateSalt();

                    string hashpass = HashPassword(newPass, _PasswordFormat, salt);
                    //save updated information
                    if (db_Accounts.UpdateT_OE_USERS(u.USER_IDX, hashpass, salt, null, null, null, null, true, null, null, null, null, "system") > 0)
                    {
                        //send email
                        string msg = "Your password has been reset. Your new temporary password is: " + "\r\n\r\n";
                        msg += newPass + "\r\n\r\n";
                        msg += "When you login for the first time you will be asked to set a permanent password.";
                        if (u.EMAIL == null)
                            return "User does not have email address.";
                        if (Utils.SendEmail(null, u.EMAIL.Split(';').ToList(), null, null, "Open Waters Password Reset", msg, null))
                            return "Email has been sent.";
                        else
                            return "Error in sending email";
                    }
                    else
                    {
                        return "Error updating password";
                    }
                }
                else
                    return "Email does not exist in the system.";
            }
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override bool ValidateUser(string username, string password)
        {
            //raise error if null username/password or too long
            if (!Utils.ValidateParameter(ref username, true, true, false, 25))
                return false;

            if (!Utils.ValidateParameter(ref password, true, true, false, 100))
                return false;

            //check if password matches hashed/salted password
            T_OE_USERS u = db_Accounts.GetT_OE_USERSByID(username);

            if (u != null)
            {
                if (u.ACT_IND == false) 
                    return false; //fail if user is inactive

                if (CheckPassword(password, u.PWD_HASH, u.PWD_SALT))
                    return true;
                else
                {
                    //db_Accounts.UpdateT_OE_USERS(u.USER_IDX, null, null, null, null, null, null, u.LOG_ATMPT.ConvertOrDefault<int>() < MaxInvalidPasswordAttempts, null, null, null, null, null, u.LOG_ATMPT.ConvertOrDefault<int>() + 1, null, null, null);

                    //user account is locked due to too many invalid login attempts
                    //if (u.LOG_ATMPT.ConvertOrDefault<int>() + 1 > MaxInvalidPasswordAttempts)
                    //    Utils.SendEmail(null, u.EMAIL, "Your account is locked.", "Your user account has been locked due to too many incorrect login attempts. Please contact the system administrator to reset your user account.");

                    return false;
                }
            }
            else
                return false;

        }

        private string GenerateSalt()
        {
            byte[] buf = new byte[32];
            (new RNGCryptoServiceProvider()).GetBytes(buf);
            return Convert.ToBase64String(buf);
        }

        private string HashPassword(string pass, MembershipPasswordFormat passwordFormat, string salt)
        {
            if (passwordFormat == MembershipPasswordFormat.Clear)
                return pass;
            else
            {
                SHA256Managed hash = new SHA256Managed();
                byte[] utf8 = UTF8Encoding.UTF8.GetBytes(pass + salt);
                StringBuilder s = new StringBuilder(hash.ComputeHash(utf8).Length * 2);
                foreach (byte b in hash.ComputeHash(utf8))
                    s.Append(b.ToString("x2"));
                return s.ToString();
            }
        }

        public bool CheckPassword(string password, string correctHash, string correctsalt)
        {
            string hashpass = "";
            if (correctsalt.Length < 1)  //if no salt value stored in DB, let users authenticate with clear password
                hashpass = HashPassword(password, MembershipPasswordFormat.Clear, correctsalt);
            else
                hashpass = HashPassword(password, MembershipPasswordFormat.Hashed, correctsalt);

            return (correctHash == hashpass);
        }

        private readonly Random _rng = new Random();
        private const string _chars = "ABCDEFGHJKLMNPQRSTUVWXYZbdefghjpry23456789";
        private string RandomString(int size)
        {
            char[] buffer = new char[size];

            for (int i = 0; i < size; i++)
                buffer[i] = _chars[_rng.Next(_chars.Length)];

            return new string(buffer);
        }

    }
}