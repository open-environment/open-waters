using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using OpenEnvironment.App_Logic.BusinessLogicLayer;

namespace OpenEnvironment.App_Logic.DataAccessLayer
{
    public class db_Ref
    {

        //*****************APP SETTINGS**********************************
        public static string GetT_OE_APP_SETTING(string settingName)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_OE_APP_SETTINGS
                            where a.SETTING_NAME == settingName
                            select a).FirstOrDefault().SETTING_VALUE;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        //*****************APP TASKS**********************************
        public static T_OE_APP_TASKS GetT_OE_APP_TASKS_ByName(string taskName)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_OE_APP_TASKS
                            where a.TASK_NAME == taskName
                            select a).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static int UpdateT_OE_APP_TASKS(string tASK_NAME, string tASK_STATUS, int? tASK_FREQ_MS, string mODIFY_USERID)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    T_OE_APP_TASKS t = new T_OE_APP_TASKS();
                        t = (from c in ctx.T_OE_APP_TASKS
                             where c.TASK_NAME == tASK_NAME
                             select c).First();

                    if (tASK_STATUS != null) t.TASK_STATUS = tASK_STATUS;
                    if (tASK_FREQ_MS != null) t.TASK_FREQ_MS = (int)tASK_FREQ_MS;
                    if (mODIFY_USERID != null) t.MODIFY_USERID = mODIFY_USERID;
                    t.MODIFY_DT = System.DateTime.Now;
                    ctx.SaveChanges();

                    return t.TASK_IDX;
                }
                catch
                {
                    return 0;
                }
            }

        }

        //*********************** WQX TRANSACTION LOG*******************************
        public static int InsertUpdateWQX_TRANSACTION_LOG(int? lOG_ID, string tABLE_CD, int tABLE_IDX, string sUBMIT_TYPE, byte[] rESPONSE_FILE, 
            string rESPONSE_TXT, string cDX_SUBMIT_TRANS_ID, string cDX_SUBMIT_STATUS)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    T_WQX_TRANSACTION_LOG t = new T_WQX_TRANSACTION_LOG();
                    if (lOG_ID != null)
                        t = (from c in ctx.T_WQX_TRANSACTION_LOG
                             where c.LOG_ID == lOG_ID
                             select c).First();

                    if (lOG_ID == null)
                        t = new T_WQX_TRANSACTION_LOG();

                    if (tABLE_CD != null) t.TABLE_CD = tABLE_CD;
                    t.TABLE_IDX = tABLE_IDX;
                    if (sUBMIT_TYPE != null) t.SUBMIT_TYPE = sUBMIT_TYPE;
                    if (rESPONSE_FILE != null) t.RESPONSE_FILE = rESPONSE_FILE;
                    if (rESPONSE_TXT != null) t.RESPONSE_TXT = rESPONSE_TXT;
                    if (cDX_SUBMIT_TRANS_ID != null) t.CDX_SUBMIT_TRANSID = cDX_SUBMIT_TRANS_ID;
                    if (cDX_SUBMIT_STATUS != null) t.CDX_SUBMIT_STATUS = cDX_SUBMIT_STATUS;

                    if (lOG_ID == null) //insert case
                    {
                        t.SUBMIT_DT = System.DateTime.Now;
                        ctx.AddToT_WQX_TRANSACTION_LOG(t);
                    }

                    ctx.SaveChanges();

                    return t.LOG_ID;
                }
                catch
                {
                    return 0;
                }
            }
        }

        public static List<T_WQX_TRANSACTION_LOG> GetWQX_TRANSACTION_LOG(string TableCD, int TableIdx)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from i in ctx.T_WQX_TRANSACTION_LOG
                            where i.TABLE_CD == TableCD
                            && i.TABLE_IDX == TableIdx
                            select i).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static T_WQX_TRANSACTION_LOG GetWQX_TRANSACTION_LOG_ByLogID(int LogID)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from i in ctx.T_WQX_TRANSACTION_LOG
                            where i.LOG_ID == LogID
                            select i).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static List<V_WQX_TRANSACTION_LOG> GetV_WQX_TRANSACTION_LOG(string TableCD, DateTime? startDt, DateTime? endDt)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from i in ctx.V_WQX_TRANSACTION_LOG
                            where (TableCD == null ? true : i.TABLE_CD == TableCD)
                            && (startDt == null ? true : i.SUBMIT_DT >= startDt)
                            && (endDt == null ? true : i.SUBMIT_DT <= endDt)
                            orderby i.SUBMIT_DT descending
                            select i).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        //******************REF DATA****************************************
        public static int InsertOrUpdateT_WQX_REF_DATA(global::System.String tABLE, global::System.String vALUE, global::System.String tEXT, global::System.Boolean? UsedInd)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    Boolean insInd = true;
                    T_WQX_REF_DATA a = new T_WQX_REF_DATA();

                    if (ctx.T_WQX_REF_DATA.Any(o => o.VALUE == vALUE && o.TABLE == tABLE))
                    {
                        //update case
                        a = (from c in ctx.T_WQX_REF_DATA
                             where c.VALUE == vALUE
                             && c.TABLE == tABLE
                             select c).FirstOrDefault();
                        insInd = false;
                    }

                    a.TABLE = tABLE;
                    a.VALUE = vALUE;
                    a.TEXT = Utils.SubStringPlus(tEXT, 0,200);
                    if (UsedInd != null) a.USED_IND = UsedInd;

                    a.UPDATE_DT = System.DateTime.Now;
                    a.ACT_IND = true;

                    if (insInd) //insert case
                    {
                        if (UsedInd == null) a.USED_IND = true;
                        ctx.AddToT_WQX_REF_DATA(a);
                    }
                    ctx.SaveChanges();
                    return 1;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }

        public static int UpdateT_WQX_REF_DATAByIDX(global::System.Int32 IDX, global::System.String vALUE, global::System.String tEXT, Boolean ActInd)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    T_WQX_REF_DATA a = new T_WQX_REF_DATA();
                    a = (from c in ctx.T_WQX_REF_DATA
                            where c.REF_DATA_IDX == IDX
                            select c).FirstOrDefault();

                    if (vALUE != null) a.VALUE = vALUE;
                    if (tEXT != null) a.TEXT = Utils.SubStringPlus(tEXT, 0, 200);
                    a.UPDATE_DT = System.DateTime.Now;
                    a.ACT_IND = ActInd;
                    ctx.SaveChanges();
                    return 1;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }

        public static List<T_WQX_REF_DATA> GetT_WQX_REF_DATA(string tABLE, Boolean ActInd, Boolean UsedInd)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_WQX_REF_DATA
                            where (ActInd ? a.ACT_IND == true : true)
                            && (UsedInd ? a.USED_IND == true : true) 
                            && a.TABLE == tABLE
                            orderby a.TEXT
                            select a).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        //******************REF CHARACTERISTIC****************************************
        public static int InsertOrUpdateT_WQX_REF_CHARACTERISTIC(global::System.String cHAR_NAME, global::System.Decimal? dETECT_LIMIT, global::System.String dEFAULT_UNIT, global::System.Boolean? uSED_IND, 
            global::System.Boolean aCT_IND)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    Boolean insInd = true;
                    T_WQX_REF_CHARACTERISTIC a = new T_WQX_REF_CHARACTERISTIC();

                    if (ctx.T_WQX_REF_CHARACTERISTIC.Any(o => o.CHAR_NAME == cHAR_NAME))
                    {
                        //update case
                        a = (from c in ctx.T_WQX_REF_CHARACTERISTIC
                             where c.CHAR_NAME == cHAR_NAME
                             select c).FirstOrDefault();
                        insInd = false;
                    }

                    a.CHAR_NAME = cHAR_NAME;
                    if (dETECT_LIMIT != null) a.DEFAULT_DETECT_LIMIT = dETECT_LIMIT;
                    if (dEFAULT_UNIT != null) a.DEFAULT_UNIT = dEFAULT_UNIT;
                    if (uSED_IND != null) a.USED_IND = uSED_IND;

                    a.UPDATE_DT = System.DateTime.Now;
                    a.ACT_IND = true;

                    if (insInd) //insert case
                    {
                        a.USED_IND = false;
                        ctx.AddToT_WQX_REF_CHARACTERISTIC(a);
                    }
                    ctx.SaveChanges();
                    return 1;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }

        public static List<T_WQX_REF_CHARACTERISTIC> GetT_WQX_REF_CHARACTERISTIC(Boolean ActInd, Boolean onlyUsedInd)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_WQX_REF_CHARACTERISTIC
                            where (ActInd ? a.ACT_IND == true : true)
                            && (onlyUsedInd ? a.USED_IND == true : true)
                            select a).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static T_WQX_REF_CHARACTERISTIC GetT_WQX_REF_CHARACTERISTIC_ByName(string CharName)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_WQX_REF_CHARACTERISTIC
                            where a.CHAR_NAME == CharName
                            select a).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        //******************REF CHAR LIMIT****************************************
        public static T_WQX_REF_CHAR_LIMITS GetT_WQX_REF_CHAR_LIMITS_ByNameUnit(string CharName, string UnitName)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_WQX_REF_CHAR_LIMITS
                            where a.CHAR_NAME == CharName
                            && a.UNIT_NAME == UnitName
                            select a).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        //******************REF ANAL METHOD ****************************************
        public static List<T_WQX_REF_ANAL_METHOD> GetT_WQX_REF_ANAL_METHOD(Boolean ActInd)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_WQX_REF_ANAL_METHOD
                            where (ActInd ? a.ACT_IND == true : true)
                            select a).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        //*********************** IMPORT LOG *******************************
        public static int InsertUpdateWQX_IMPORT_LOG(int? iMPORT_ID, string oRG_ID, string tYPE_CD, string fILE_NAME, int fILE_SIZE, string iMPORT_STATUS,
            byte[] iMPORT_FILE, string uSER_ID)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    T_WQX_IMPORT_LOG t = new T_WQX_IMPORT_LOG();
                    if (iMPORT_ID != null)
                        t = (from c in ctx.T_WQX_IMPORT_LOG
                             where c.IMPORT_ID == iMPORT_ID
                             select c).First();

                    if (iMPORT_ID == null)
                        t = new T_WQX_IMPORT_LOG();

                    if (oRG_ID != null) t.ORG_ID = oRG_ID;
                    if (tYPE_CD != null) t.TYPE_CD = tYPE_CD.Substring(0,5);
                    if (fILE_NAME!= null) t.FILE_NAME = fILE_NAME;
                    if (fILE_SIZE!= null) t.FILE_SIZE = fILE_SIZE;
                    if (iMPORT_STATUS != null) t.IMPORT_STATUS = iMPORT_STATUS;
                    if (iMPORT_FILE != null) t.IMPORT_FILE = iMPORT_FILE;
                    if (uSER_ID != null) t.CREATE_USERID = uSER_ID;                   

                    if (iMPORT_ID == null) //insert case
                    {
                        t.CREATE_DT = DateTime.Now;
                        ctx.AddToT_WQX_IMPORT_LOG(t);
                    }
                    else
                    {
                    }

                    ctx.SaveChanges();

                    return t.IMPORT_ID;
                }
                catch
                {
                    return 0;
                }
            }
        }
        
        public static List<T_WQX_IMPORT_LOG> GetWQX_IMPORT_LOG()
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from i in ctx.T_WQX_IMPORT_LOG
                            select i).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static int DeleteT_WQX_IMPORT_LOG(int iMPORT_ID)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    T_WQX_IMPORT_LOG r = new T_WQX_IMPORT_LOG();
                    r = (from c in ctx.T_WQX_IMPORT_LOG
                         where c.IMPORT_ID == iMPORT_ID select c).FirstOrDefault();
                    ctx.DeleteObject(r);
                    ctx.SaveChanges();
                    return 1;
                }
                catch
                {
                    return 0;
                }
            }

        }

    }
}