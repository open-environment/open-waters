using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenEnvironment.App_Logic.DataAccessLayer
{
    public class db_Accounts
    {

        //*****************USERS **********************************
        public static int CreateT_OE_USERS(global::System.String uSER_ID, global::System.String pWD_HASH, global::System.String pWD_SALT, global::System.String fNAME, global::System.String lNAME, global::System.String eMAIL, global::System.Boolean aCT_IND, global::System.Boolean iNITAL_PWD_FLAG, global::System.DateTime? lASTLOGIN_DT, global::System.String pHONE, global::System.String pHONE_EXT, global::System.String cREATE_USER)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    T_OE_USERS u = new T_OE_USERS();
                    u.USER_ID = uSER_ID;
                    u.PWD_HASH = pWD_HASH;
                    u.PWD_SALT = pWD_SALT;
                    u.FNAME = fNAME;
                    u.LNAME = lNAME;
                    u.EMAIL = eMAIL;
                    u.ACT_IND = aCT_IND;
                    u.INITAL_PWD_FLAG = iNITAL_PWD_FLAG;
                    u.EFFECTIVE_DT = System.DateTime.Now;
                    u.LASTLOGIN_DT = lASTLOGIN_DT;
                    u.PHONE = pHONE;
                    u.PHONE_EXT = pHONE_EXT;
                    u.CREATE_DT = System.DateTime.Now;
                    u.CREATE_USERID = cREATE_USER;

                    ctx.AddToT_OE_USERS(u);
                    ctx.SaveChanges();
                    return u.USER_IDX;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }

        public static List<T_OE_USERS> GetT_OE_USERS()
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return ctx.T_OE_USERS.ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static T_OE_USERS GetT_OE_USERSByIDX(int idx)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return ctx.T_OE_USERS.FirstOrDefault(usr => usr.USER_IDX == idx);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static T_OE_USERS GetT_OE_USERSByID(string id)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return ctx.T_OE_USERS.FirstOrDefault(usr => usr.USER_ID.ToUpper() == id.ToUpper()
                        );
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static T_OE_USERS GetT_VCCB_USERByEmail(string email)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return ctx.T_OE_USERS.FirstOrDefault(usr => usr.EMAIL.ToUpper() == email.ToUpper());
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static int UpdateT_OE_USERS(int idx, string newPWD_HASH, string newPWD_SALT, string newFNAME, string newLNAME, string newEMAIL, bool? newACT_IND, bool? newINIT_PWD_FLG, DateTime? newEFF_DATE, DateTime? newLAST_LOGIN_DT, string newPHONE, string newPHONE_EXT, string newMODIFY_USR)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    T_OE_USERS row = new T_OE_USERS();
                    row = (from c in ctx.T_OE_USERS where c.USER_IDX == idx select c).First();

                    if (newPWD_HASH != null)
                        row.PWD_HASH = newPWD_HASH;

                    if (newPWD_SALT != null)
                        row.PWD_SALT = newPWD_SALT;

                    if (newFNAME != null)
                        row.FNAME = newFNAME;

                    if (newLNAME != null)
                        row.LNAME = newLNAME;

                    if (newEMAIL != null)
                        row.EMAIL = newEMAIL;

                    if (newACT_IND != null)
                        row.ACT_IND = (bool)newACT_IND;

                    if (newINIT_PWD_FLG != null)
                        row.INITAL_PWD_FLAG = (bool)newINIT_PWD_FLG;

                    if (newEFF_DATE != null)
                        row.EFFECTIVE_DT = (DateTime)newEFF_DATE;

                    if (newLAST_LOGIN_DT != null)
                        row.LASTLOGIN_DT = (DateTime)newLAST_LOGIN_DT;

                    if (newPHONE != null)
                        row.PHONE = newPHONE;

                    if (newPHONE_EXT != null)
                        row.PHONE_EXT = newPHONE_EXT;

                    if (newMODIFY_USR != null)
                        row.MODIFY_USERID = newMODIFY_USR;

                    row.MODIFY_DT = System.DateTime.Now;

                    ctx.SaveChanges();
                    return 1;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }

        public static int DeleteT_OE_USERS(int idx)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    T_OE_USERS row = new T_OE_USERS();
                    row = (from c in ctx.T_OE_USERS where c.USER_IDX == idx select c).First();
                    ctx.DeleteObject(row);
                    ctx.SaveChanges();
                    return 1;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }

        //*****************ROLES **********************************
        public static int CreateT_OE_ROLES(global::System.String rOLE_NAME, global::System.String rOLE_DESC, global::System.String cREATE_USER = "system")
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    T_OE_ROLES r = new T_OE_ROLES();
                    r.ROLE_NAME = rOLE_NAME;
                    r.ROLE_DESC = rOLE_DESC;
                    r.CREATE_DT = System.DateTime.Now;
                    r.CREATE_USERID = cREATE_USER;

                    ctx.AddToT_OE_ROLES(r);
                    ctx.SaveChanges();
                    return 1;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }

        public static T_OE_ROLES GetT_VCCB_ROLEByName(string rolename)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return ctx.T_OE_ROLES.FirstOrDefault(role => role.ROLE_NAME.ToUpper() == rolename.ToUpper());
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static T_OE_ROLES GetT_VCCB_ROLEByIDX(int idx)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return ctx.T_OE_ROLES.FirstOrDefault(role => role.ROLE_IDX == idx);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static int UpdateT_VCCB_ROLE(int idx, string newROLE_NAME, string newROLE_DESC, string newMODIFY_USR)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    T_OE_ROLES row = new T_OE_ROLES();
                    row = (from c in ctx.T_OE_ROLES where c.ROLE_IDX == idx select c).First();

                    if (newROLE_NAME != null)
                        row.ROLE_NAME = newROLE_NAME;

                    if (newROLE_DESC != null)
                        row.ROLE_DESC = newROLE_DESC;

                    if (newMODIFY_USR != null)
                        row.MODIFY_USERID = newMODIFY_USR;

                    row.MODIFY_DT = System.DateTime.Now;

                    ctx.SaveChanges();
                    return 1;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }

        public static int DeleteT_VCCB_ROLE(int idx)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    T_OE_ROLES row = new T_OE_ROLES();
                    row = (from c in ctx.T_OE_ROLES where c.ROLE_IDX == idx select c).First();
                    ctx.DeleteObject(row);
                    ctx.SaveChanges();
                    return 1;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }

        //*****************ROLE / USER RELATIONSHIP **********************************
        public static List<T_OE_USERS> GetT_OE_USERSInRole(int roleID)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    var users = from itemA in ctx.T_OE_USERS
                                join itemB in ctx.T_OE_USER_ROLES on itemA.USER_IDX equals itemB.USER_IDX
                                where itemB.ROLE_IDX == roleID
                                orderby itemA.USER_ID
                                select itemA;

                    return users.ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static List<T_OE_USERS> GetT_OE_USERSNotInRole(int roleID)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    //first get all users 
                    var allUsers = (from itemA in ctx.T_OE_USERS select itemA);

                    //next get all users in role
                    var UsersInRole = (from itemA in ctx.T_OE_USERS
                                       join itemB in ctx.T_OE_USER_ROLES on itemA.USER_IDX equals itemB.USER_IDX
                                       where itemB.ROLE_IDX == roleID
                                       select itemA);

                    //then get exclusions
                    var usersNotInRole = allUsers.Except(UsersInRole);

                    return usersNotInRole.OrderBy(a => a.USER_ID).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static List<T_OE_ROLES> GetT_OE_ROLESInUser(int userIDX)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    var roles = from itemA in ctx.T_OE_ROLES
                                join itemB in ctx.T_OE_USER_ROLES on itemA.ROLE_IDX equals itemB.ROLE_IDX
                                where itemB.USER_IDX == userIDX
                                select itemA;

                    return roles.ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static int CreateT_VCCB_USER_ROLE(global::System.Int32 rOLE_IDX, global::System.Int32 uSER_IDX, global::System.String cREATE_USER = "system")
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    T_OE_USER_ROLES ur = new T_OE_USER_ROLES();
                    ur.ROLE_IDX = rOLE_IDX;
                    ur.USER_IDX = uSER_IDX;
                    ur.CREATE_DT = System.DateTime.Now;
                    ur.CREATE_USERID = cREATE_USER;
                    ctx.AddToT_OE_USER_ROLES(ur);
                    ctx.SaveChanges();
                    return 1;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }

        public static int DeleteT_VCCB_USER_ROLE(int UserIDX, int RoleIDX)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    T_OE_USER_ROLES row = new T_OE_USER_ROLES();
                    row = (from c in ctx.T_OE_USER_ROLES
                           where c.ROLE_IDX == RoleIDX && c.USER_IDX == UserIDX
                           select c).FirstOrDefault();
                    ctx.DeleteObject(row);
                    ctx.SaveChanges();
                    return 1;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }







    }
}