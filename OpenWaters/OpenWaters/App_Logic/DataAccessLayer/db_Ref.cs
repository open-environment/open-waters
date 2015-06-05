using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using OpenEnvironment.App_Logic.BusinessLogicLayer;

namespace OpenEnvironment.App_Logic.DataAccessLayer
{
    public class AnalMethodDisplay
    {
        public int ANALYTIC_METHOD_IDX { get; set; }
        public string AnalMethodDisplayName { get; set; }
    }

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
                    return "";
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
            string rESPONSE_TXT, string cDX_SUBMIT_TRANS_ID, string cDX_SUBMIT_STATUS, string oRG_ID)
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
                    if (oRG_ID != null) t.ORG_ID = oRG_ID;

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

        public static List<V_WQX_TRANSACTION_LOG> GetV_WQX_TRANSACTION_LOG(string TableCD, DateTime? startDt, DateTime? endDt, string OrgID)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from i in ctx.V_WQX_TRANSACTION_LOG
                            where (OrgID == null ? true : i.ORG_ID == OrgID)
                            && (TableCD == null ? true : i.TABLE_CD == TableCD)
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


        //******************REF ANAL METHOD ****************************************
        public static List<AnalMethodDisplay> GetT_WQX_REF_ANAL_METHOD(Boolean ActInd)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_WQX_REF_ANAL_METHOD
                            where (ActInd ? a.ACT_IND == true : true)
                            select new AnalMethodDisplay{
                                ANALYTIC_METHOD_IDX = a.ANALYTIC_METHOD_IDX,
                                AnalMethodDisplayName = a.ANALYTIC_METHOD_CTX + " - " + a.ANALYTIC_METHOD_ID
                            }).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static int InsertOrUpdateT_WQX_REF_ANAL_METHOD(global::System.Int32? aNALYTIC_METHOD_IDX, global::System.String aNALYTIC_METHOD_ID, string aNALYTIC_METHOD_CTX,
            string aNALYTIC_METHOD_NAME, string aNALYTIC_METHOD_DESC, bool aCT_IND)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    Boolean insInd = true;
                    T_WQX_REF_ANAL_METHOD a = new T_WQX_REF_ANAL_METHOD();

                    if (ctx.T_WQX_REF_ANAL_METHOD.Any(o => o.ANALYTIC_METHOD_IDX == aNALYTIC_METHOD_IDX))
                    {
                        //update case
                        a = (from c in ctx.T_WQX_REF_ANAL_METHOD
                             where c.ANALYTIC_METHOD_IDX == aNALYTIC_METHOD_IDX
                             select c).FirstOrDefault();
                        insInd = false;
                    }
                    else
                    {
                        if (ctx.T_WQX_REF_ANAL_METHOD.Any(o => o.ANALYTIC_METHOD_ID == aNALYTIC_METHOD_ID && o.ANALYTIC_METHOD_CTX == aNALYTIC_METHOD_CTX))
                        {
                            //update case
                            a = (from c in ctx.T_WQX_REF_ANAL_METHOD
                                 where c.ANALYTIC_METHOD_ID == aNALYTIC_METHOD_ID
                                 && c.ANALYTIC_METHOD_CTX == aNALYTIC_METHOD_CTX
                                 select c).FirstOrDefault();
                            insInd = false;
                        }
                    }

                    if (aNALYTIC_METHOD_ID != null) a.ANALYTIC_METHOD_ID = aNALYTIC_METHOD_ID;
                    if (aNALYTIC_METHOD_CTX != null) a.ANALYTIC_METHOD_CTX = aNALYTIC_METHOD_CTX;
                    if (aNALYTIC_METHOD_NAME != null) a.ANALYTIC_METHOD_NAME = aNALYTIC_METHOD_NAME;
                    if (aNALYTIC_METHOD_DESC != null) a.ANALYTIC_METHOD_DESC = aNALYTIC_METHOD_DESC;
                    if (aCT_IND != null) a.ACT_IND = aCT_IND;

                    a.UPDATE_DT = System.DateTime.Now;

                    if (insInd) //insert case
                        ctx.AddToT_WQX_REF_ANAL_METHOD(a);

                    ctx.SaveChanges();
                    return a.ANALYTIC_METHOD_IDX;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }

        public static T_WQX_REF_ANAL_METHOD GetT_WQX_REF_ANAL_METHODByIDandContext(string ID, string Context)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_WQX_REF_ANAL_METHOD
                            where a.ANALYTIC_METHOD_ID == ID
                            && a.ANALYTIC_METHOD_CTX == Context
                            select a).FirstOrDefault();
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
                            orderby a.VALUE
                            select a).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static List<T_WQX_REF_DATA> GetT_WQX_REF_DATA_TaxaSearch(string searchStr)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_WQX_REF_DATA
                            where a.ACT_IND == true
                            && a.USED_IND == true
                            && a.TABLE == "Taxon"
                            && a.VALUE.Contains(searchStr)
                            orderby a.TEXT
                            select a).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static List<T_WQX_REF_DATA> GetT_WQX_REF_DATA_ActivityTypeUsed(string OrgID)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_WQX_REF_DATA
                            join b in ctx.T_WQX_ACTIVITY on a.VALUE equals b.ACT_TYPE
                            where a.TABLE == "ActivityType"
                            && b.ORG_ID == OrgID
                            orderby a.TEXT
                            select a).Distinct().ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static bool GetT_WQX_REF_DATA_ByKey(string tABLE, string vALUE)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    int iCount = (from a in ctx.T_WQX_REF_DATA
                            where (a.ACT_IND == true)
                            && a.TABLE == tABLE
                            && a.VALUE == vALUE 
                            orderby a.TEXT
                            select a).Count();

                    if (iCount == 0)
                        return false;
                    else
                        return true;

                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public static T_WQX_REF_DATA GetT_WQX_REF_DATA_ByTextGetRow(string tABLE, string tEXT)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_WQX_REF_DATA
                                  where (a.ACT_IND == true)
                                  && a.TABLE == tABLE
                                  && a.TEXT.ToUpper() == tEXT.ToUpper()
                                  select a).FirstOrDefault();

                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static DateTime? GetT_WQX_REF_DATA_LastUpdate()
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_WQX_REF_DATA
                                  select a.UPDATE_DT).Max();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }


        //******************REF CHARACTERISTIC****************************************
        public static int InsertOrUpdateT_WQX_REF_CHARACTERISTIC(global::System.String cHAR_NAME, global::System.Decimal? dETECT_LIMIT, global::System.String dEFAULT_UNIT, global::System.Boolean? uSED_IND,
            global::System.Boolean aCT_IND, global::System.String sAMP_FRAC_REQ, global::System.String pICK_LIST)
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
                    if (sAMP_FRAC_REQ != null) a.SAMP_FRAC_REQ = sAMP_FRAC_REQ;
                    if (pICK_LIST != null) a.PICK_LIST = pICK_LIST;

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

        public static bool GetT_WQX_REF_CHARACTERISTIC_ExistCheck(string CharName)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    int iCount = (from a in ctx.T_WQX_REF_CHARACTERISTIC
                                  where (a.ACT_IND == true)
                                  && a.CHAR_NAME == CharName
                                  select a).Count();

                    if (iCount == 0)
                        return false;
                    else
                        return true;

                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public static bool GetT_WQX_REF_CHARACTERISTIC_SampFracReqCheck(string CharName)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    string SampFrac = (from a in ctx.T_WQX_REF_CHARACTERISTIC
                                  where (a.ACT_IND == true)
                                  && a.CHAR_NAME == CharName
                                  select a).FirstOrDefault().SAMP_FRAC_REQ;

                    if (SampFrac == "Y")
                        return true;
                    else
                        return false;

                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }


        public static List<T_WQX_REF_CHARACTERISTIC> GetT_WQX_REF_CHARACTERISTIC_ByOrg(string OrgID, Boolean RBPInd)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_WQX_REF_CHARACTERISTIC
                            join b in ctx.T_WQX_REF_CHAR_ORG on a.CHAR_NAME equals b.CHAR_NAME
                            where b.ORG_ID == OrgID
                            && (RBPInd == true ? a.CHAR_NAME.Contains("RBP") : true)
                            && (RBPInd == false ? !a.CHAR_NAME.Contains("RBP") : true)
                            select a).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        
        //******************REF CHAR_ORG ****************************************
        public static List<T_WQX_REF_CHAR_ORG> GetT_WQX_REF_CHAR_ORG(string orgName)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_WQX_REF_CHAR_ORG
                                .Include("T_WQX_REF_ANAL_METHOD")
                            where a.ORG_ID == orgName
                            select a).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static T_WQX_REF_CHAR_ORG GetT_WQX_REF_CHAR_ORGByName(string orgName, string charName)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_WQX_REF_CHAR_ORG
                            where a.ORG_ID == orgName
                            && a.CHAR_NAME == charName
                            select a).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static int GetT_WQX_REF_CHAR_ORG_Count(string orgName)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_WQX_REF_CHAR_ORG
                            where a.ORG_ID == orgName
                            select a).Count();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static int DeleteT_WQX_REF_CHAR_ORG(string orgName, string charName)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    T_WQX_REF_CHAR_ORG r = new T_WQX_REF_CHAR_ORG();
                    r = (from c in ctx.T_WQX_REF_CHAR_ORG
                         where c.ORG_ID == orgName 
                         && c.CHAR_NAME == charName
                         select c).FirstOrDefault();
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

        public static int InsertOrUpdateT_WQX_REF_CHAR_ORG(global::System.String cHAR_NAME, global::System.String oRG_NAME, global::System.String cREATE_USER_ID,
            string dEFAULT_DETECT_LIMIT, string dEFAULT_UNIT, int? dEFAULT_ANAL_METHOD_IDX, string dEFAULT_SAMP_FRACTION, string dEFAULT_RESULT_STATUS, string dEFAULT_RESULT_VALUE_TYPE)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    Boolean insInd = true;
                    T_WQX_REF_CHAR_ORG a = new T_WQX_REF_CHAR_ORG();

                    if (ctx.T_WQX_REF_CHAR_ORG.Any(o => o.CHAR_NAME == cHAR_NAME && o.ORG_ID == oRG_NAME))
                    {
                        //update case
                        a = (from c in ctx.T_WQX_REF_CHAR_ORG
                             where c.CHAR_NAME == cHAR_NAME
                             && c.ORG_ID == oRG_NAME
                             select c).FirstOrDefault();
                        insInd = false;
                    }

                    a.CHAR_NAME = cHAR_NAME;
                    a.ORG_ID = oRG_NAME;
                    if (dEFAULT_DETECT_LIMIT != null) a.DEFAULT_DETECT_LIMIT = dEFAULT_DETECT_LIMIT;
                    if (dEFAULT_UNIT != null) a.DEFAULT_UNIT = dEFAULT_UNIT;
                    if (dEFAULT_ANAL_METHOD_IDX != null) a.DEFAULT_ANAL_METHOD_IDX = dEFAULT_ANAL_METHOD_IDX;
                    if (dEFAULT_SAMP_FRACTION != null) a.DEFAULT_SAMP_FRACTION = dEFAULT_SAMP_FRACTION;
                    if (dEFAULT_RESULT_STATUS != null) a.DEFAULT_RESULT_STATUS = dEFAULT_RESULT_STATUS;
                    if (dEFAULT_RESULT_VALUE_TYPE != null) a.DEFAULT_RESULT_VALUE_TYPE = dEFAULT_RESULT_VALUE_TYPE;

                    if (insInd) //insert case
                    {
                        a.CREATE_DT = System.DateTime.Now;
                        a.CREATE_USERID = cREATE_USER_ID;
                        ctx.AddToT_WQX_REF_CHAR_ORG(a);
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


        //******************REF COUNTY ****************************************
        public static int InsertOrUpdateT_WQX_REF_COUNTY(global::System.String sTATE_CODE, global::System.String cOUNTY_CODE, global::System.String cOUNTY_NAME, global::System.Boolean? UsedInd)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    Boolean insInd = true;
                    T_WQX_REF_COUNTY a = new T_WQX_REF_COUNTY();

                    if (ctx.T_WQX_REF_COUNTY.Any(o => o.STATE_CODE == sTATE_CODE && o.COUNTY_CODE == cOUNTY_CODE))
                    {
                        //update case
                        a = (from c in ctx.T_WQX_REF_COUNTY
                             where c.STATE_CODE == sTATE_CODE
                             && c.COUNTY_CODE == cOUNTY_CODE
                             select c).FirstOrDefault();
                        insInd = false;
                    }

                    a.STATE_CODE = sTATE_CODE;
                    a.COUNTY_CODE = cOUNTY_CODE;
                    a.COUNTY_NAME = cOUNTY_NAME;

                    if (UsedInd != null) a.USED_IND = UsedInd;

                    a.UPDATE_DT = System.DateTime.Now;
                    a.ACT_IND = true;

                    if (insInd) //insert case
                    {
                        if (UsedInd == null) a.USED_IND = true;
                        ctx.AddToT_WQX_REF_COUNTY(a);
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

        public static T_WQX_REF_COUNTY GetT_WQX_REF_COUNTY_ByCountyNameAndState(string sTATE_NAME, string cOUNTY_NAME)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_WQX_REF_COUNTY
                            join b in ctx.T_WQX_REF_DATA on a.STATE_CODE equals b.VALUE
                            where (a.ACT_IND == true)
                            && b.TABLE == "State"
                            && b.TEXT.ToUpper() == sTATE_NAME.ToUpper()
                            && a.COUNTY_NAME.ToUpper() == cOUNTY_NAME.ToUpper()
                            select a).FirstOrDefault();

                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static List<T_WQX_REF_COUNTY> GetT_WQX_REF_COUNTY()
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    //get default state
                    string State = GetT_OE_APP_SETTING("Default State");

                    return (from a in ctx.T_WQX_REF_COUNTY
                            where a.STATE_CODE == State
                            orderby a.COUNTY_NAME descending
                            select a).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }



        //******************REF DEFAULT_TIME_ZONE****************************************
        public static List<T_WQX_REF_DEFAULT_TIME_ZONE> GetT_WQX_REF_DEFAULT_TIME_ZONE()
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_WQX_REF_DEFAULT_TIME_ZONE
                            orderby a.TIME_ZONE_NAME descending
                            select a).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static T_WQX_REF_DEFAULT_TIME_ZONE GetT_WQX_REF_DEFAULT_TIME_ZONE_ByName(string TimeZoneName)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_WQX_REF_DEFAULT_TIME_ZONE
                            where a.TIME_ZONE_NAME == TimeZoneName
                            select a).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }



        //******************REF LAB****************************************
        public static List<T_WQX_REF_LAB> GetT_WQX_REF_LAB(string OrgID)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_WQX_REF_LAB
                            where a.ORG_ID == OrgID
                            orderby a.LAB_NAME descending
                            select a).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static T_WQX_REF_LAB GetT_WQX_REF_LAB_ByIDandContext(string Name, string OrgID)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_WQX_REF_LAB
                            where a.ORG_ID == OrgID
                            && a.LAB_NAME == Name
                            select a).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static int InsertOrUpdateT_WQX_REF_LAB(global::System.Int32? lAB_IDX, global::System.String lAB_NAME, string lAB_ACCRED_IND, string lAB_ACCRED_AUTHORITY, string oRG_ID, bool aCT_IND)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    Boolean insInd = true;
                    T_WQX_REF_LAB a = new T_WQX_REF_LAB();

                    if (ctx.T_WQX_REF_LAB.Any(o => o.LAB_IDX == lAB_IDX))
                    {
                        //update case
                        a = (from c in ctx.T_WQX_REF_LAB
                             where c.LAB_IDX == lAB_IDX
                             select c).FirstOrDefault();
                        insInd = false;
                    }
                    else
                    {
                        if (ctx.T_WQX_REF_LAB.Any(o => o.LAB_NAME == lAB_NAME && o.ORG_ID == oRG_ID))
                        {
                            //update case
                            a = (from c in ctx.T_WQX_REF_LAB
                                 where c.LAB_NAME == lAB_NAME
                                 && c.ORG_ID == oRG_ID
                                 select c).FirstOrDefault();
                            insInd = false;
                        }
                    }

                    if (lAB_NAME != null) a.LAB_NAME = lAB_NAME;
                    if (lAB_ACCRED_IND != null) a.LAB_ACCRED_IND = lAB_ACCRED_IND;
                    if (lAB_ACCRED_AUTHORITY != null) a.LAB_ACCRED_AUTHORITY = lAB_ACCRED_AUTHORITY;
                    if (oRG_ID != null) a.ORG_ID = oRG_ID;
                    if (aCT_IND != null) a.ACT_IND = aCT_IND;

                    a.UPDATE_DT = System.DateTime.Now;

                    if (insInd) //insert case
                    {
                        a.CREATE_DT = System.DateTime.Now;
                        ctx.AddToT_WQX_REF_LAB(a);
                    }

                    ctx.SaveChanges();
                    return a.LAB_IDX;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }




        //***************** REF_SAMP_COL_METHOD *********************************************
        public static T_WQX_REF_SAMP_COL_METHOD GetT_WQX_REF_SAMP_COL_METHOD_ByIDandContext(string ID, string Context)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_WQX_REF_SAMP_COL_METHOD
                            where a.SAMP_COLL_METHOD_ID == ID
                            && a.SAMP_COLL_METHOD_CTX == Context
                            select a).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static List<T_WQX_REF_SAMP_COL_METHOD> GetT_WQX_REF_SAMP_COL_METHOD_ByContext(string Context)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_WQX_REF_SAMP_COL_METHOD
                            where a.SAMP_COLL_METHOD_CTX == Context
                            select a).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static int InsertOrUpdateT_WQX_REF_SAMP_COL_METHOD(global::System.Int32? sAMP_COLL_METHOD_IDX, global::System.String sAMP_COLL_METHOD_ID, 
            string sAMP_COLL_METHOD_CTX, string sAMP_COLL_METHOD_NAME, string sAMP_COLL_METHOD_DESC, bool aCT_IND)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    Boolean insInd = true;
                    T_WQX_REF_SAMP_COL_METHOD a = new T_WQX_REF_SAMP_COL_METHOD();

                    if (ctx.T_WQX_REF_SAMP_COL_METHOD.Any(o => o.SAMP_COLL_METHOD_IDX == sAMP_COLL_METHOD_IDX))
                    {
                        //update case
                        a = (from c in ctx.T_WQX_REF_SAMP_COL_METHOD
                             where c.SAMP_COLL_METHOD_IDX == sAMP_COLL_METHOD_IDX
                             select c).FirstOrDefault();
                        insInd = false;
                    }
                    else
                    {
                        if (ctx.T_WQX_REF_ANAL_METHOD.Any(o => o.ANALYTIC_METHOD_ID == sAMP_COLL_METHOD_ID && o.ANALYTIC_METHOD_CTX == sAMP_COLL_METHOD_CTX))
                        {
                            //update case
                            a = (from c in ctx.T_WQX_REF_SAMP_COL_METHOD
                                 where c.SAMP_COLL_METHOD_ID == sAMP_COLL_METHOD_ID
                                 && c.SAMP_COLL_METHOD_CTX == sAMP_COLL_METHOD_CTX
                                 select c).FirstOrDefault();
                            insInd = false;
                        }
                    }

                    if (sAMP_COLL_METHOD_ID != null) a.SAMP_COLL_METHOD_ID = sAMP_COLL_METHOD_ID;
                    if (sAMP_COLL_METHOD_CTX != null) a.SAMP_COLL_METHOD_CTX = sAMP_COLL_METHOD_CTX;
                    if (sAMP_COLL_METHOD_NAME != null) a.SAMP_COLL_METHOD_NAME = sAMP_COLL_METHOD_NAME;
                    if (sAMP_COLL_METHOD_DESC != null) a.SAMP_COLL_METHOD_DESC = sAMP_COLL_METHOD_DESC;
                    if (aCT_IND != null) a.ACT_IND = aCT_IND;

                    a.UPDATE_DT = System.DateTime.Now;

                    if (insInd) //insert case
                        ctx.AddToT_WQX_REF_SAMP_COL_METHOD(a);

                    ctx.SaveChanges();
                    return a.SAMP_COLL_METHOD_IDX;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }



        //***************** REF_SAMP_PREP *********************************************
        public static T_WQX_REF_SAMP_PREP GetT_WQX_REF_SAMP_PREP_ByIDandContext(string ID, string Context)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_WQX_REF_SAMP_PREP
                            where a.SAMP_PREP_METHOD_ID == ID
                            && a.SAMP_PREP_METHOD_CTX == Context
                            select a).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static List<T_WQX_REF_SAMP_PREP> GetT_WQX_REF_SAMP_PREP_ByContext(string Context)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_WQX_REF_SAMP_PREP
                            where a.SAMP_PREP_METHOD_CTX == Context
                            select a).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public static int InsertOrUpdateT_WQX_REF_SAMP_PREP(global::System.Int32? sAMP_PREP_IDX, global::System.String sAMP_PREP_METHOD_ID,
            string sAMP_PREP_METHOD_CTX, string sAMP_PREP_METHOD_NAME, string sAMP_PREP_METHOD_DESC, bool aCT_IND)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    Boolean insInd = true;
                    T_WQX_REF_SAMP_PREP a = new T_WQX_REF_SAMP_PREP();

                    if (ctx.T_WQX_REF_SAMP_PREP.Any(o => o.SAMP_PREP_IDX == sAMP_PREP_IDX))
                    {
                        //update case
                        a = (from c in ctx.T_WQX_REF_SAMP_PREP
                             where c.SAMP_PREP_IDX == sAMP_PREP_IDX
                             select c).FirstOrDefault();
                        insInd = false;
                    }
                    else
                    {
                        if (ctx.T_WQX_REF_ANAL_METHOD.Any(o => o.ANALYTIC_METHOD_ID == sAMP_PREP_METHOD_ID && o.ANALYTIC_METHOD_CTX == sAMP_PREP_METHOD_CTX))
                        {
                            //update case
                            a = (from c in ctx.T_WQX_REF_SAMP_PREP
                                 where c.SAMP_PREP_METHOD_ID == sAMP_PREP_METHOD_ID
                                 && c.SAMP_PREP_METHOD_CTX == sAMP_PREP_METHOD_CTX
                                 select c).FirstOrDefault();
                            insInd = false;
                        }
                    }

                    if (sAMP_PREP_METHOD_ID != null) a.SAMP_PREP_METHOD_ID = sAMP_PREP_METHOD_ID;
                    if (sAMP_PREP_METHOD_CTX != null) a.SAMP_PREP_METHOD_CTX = sAMP_PREP_METHOD_CTX;
                    if (sAMP_PREP_METHOD_NAME != null) a.SAMP_PREP_METHOD_NAME = sAMP_PREP_METHOD_NAME;
                    if (sAMP_PREP_METHOD_DESC != null) a.SAMP_PREP_METHOD_DESC = sAMP_PREP_METHOD_DESC;
                    if (aCT_IND != null) a.ACT_IND = aCT_IND;

                    a.UPDATE_DT = System.DateTime.Now;

                    if (insInd) //insert case
                        ctx.AddToT_WQX_REF_SAMP_PREP(a);

                    ctx.SaveChanges();
                    return a.SAMP_PREP_IDX;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }


        //***************** REF_SYS_LOG *********************************************
        public static int InsertT_OE_SYS_LOG(string logType, string logMsg)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    T_OE_SYS_LOG e = new T_OE_SYS_LOG();
                    e.LOG_TYPE = logType;
                    if (logMsg != null)
                        e.LOG_MSG = logMsg.SubStringPlus(0, 1999);
                    e.LOG_DT = System.DateTime.Now;

                    ctx.AddToT_OE_SYS_LOG(e);
                    ctx.SaveChanges();
                    return e.SYS_LOG_ID;
                }
                catch
                {
                    return 0;
                }
            }
        }


        //******************REF TAXA_ORG ****************************************
        public static List<T_WQX_REF_TAXA_ORG> GetT_WQX_REF_TAXA_ORG(string orgName)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_WQX_REF_TAXA_ORG
                            where a.ORG_ID == orgName
                            select a).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static int DeleteT_WQX_REF_TAXA_ORG(string orgName, string charName)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    T_WQX_REF_TAXA_ORG r = new T_WQX_REF_TAXA_ORG();
                    r = (from c in ctx.T_WQX_REF_TAXA_ORG
                         where c.ORG_ID == orgName
                         && c.BIO_SUBJECT_TAXONOMY == charName
                         select c).FirstOrDefault();
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

        public static int InsertOrUpdateT_WQX_REF_TAXA_ORG(global::System.String bIO_SUBJECT_TAXAONOMY, global::System.String oRG_NAME, global::System.String cREATE_USER_ID)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    Boolean insInd = true;
                    T_WQX_REF_TAXA_ORG a = new T_WQX_REF_TAXA_ORG();

                    if (ctx.T_WQX_REF_TAXA_ORG.Any(o => o.BIO_SUBJECT_TAXONOMY == bIO_SUBJECT_TAXAONOMY && o.ORG_ID == oRG_NAME))
                    {
                        //update case
                        a = (from c in ctx.T_WQX_REF_TAXA_ORG
                             where c.BIO_SUBJECT_TAXONOMY == bIO_SUBJECT_TAXAONOMY
                             && c.ORG_ID == oRG_NAME
                             select c).FirstOrDefault();
                        insInd = false;
                    }

                    a.BIO_SUBJECT_TAXONOMY = bIO_SUBJECT_TAXAONOMY;
                    a.ORG_ID = oRG_NAME;

                    if (insInd) //insert case
                    {
                        a.CREATE_DT = System.DateTime.Now;
                        a.CREATE_USERID = cREATE_USER_ID;
                        ctx.AddToT_WQX_REF_TAXA_ORG(a);
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

        public static List<T_WQX_REF_DATA> GetT_WQX_REF_TAXA_ByOrg(string OrgID)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_WQX_REF_DATA
                            join b in ctx.T_WQX_REF_TAXA_ORG on a.VALUE equals b.BIO_SUBJECT_TAXONOMY
                            where b.ORG_ID == OrgID
                            && a.TABLE == "Taxon"
                            select a).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }



        //*********************** IMPORT LOG *******************************
        public static int InsertUpdateWQX_IMPORT_LOG(int? iMPORT_ID, string oRG_ID, string tYPE_CD, string fILE_NAME, int fILE_SIZE, string iMPORT_STATUS, string iMPORT_PROGRESS,
            string iMPORT_PROGRESS_MSG, byte[] iMPORT_FILE, string uSER_ID)
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
                    t.FILE_SIZE = fILE_SIZE;
                    if (iMPORT_STATUS != null) t.IMPORT_STATUS = iMPORT_STATUS;
                    if (iMPORT_PROGRESS != null) t.IMPORT_PROGRESS = iMPORT_PROGRESS;
                    if (iMPORT_PROGRESS_MSG != null) t.IMPORT_PROGRESS_MSG = iMPORT_PROGRESS_MSG;
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

        public static int UpdateWQX_IMPORT_LOG_MarkPendingSampImportAsComplete(string oRG_ID)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    T_WQX_IMPORT_LOG t = new T_WQX_IMPORT_LOG();
                    t = (from c in ctx.T_WQX_IMPORT_LOG
                         where c.IMPORT_PROGRESS == "P"
                         && c.TYPE_CD == "Sample"
                         && c.ORG_ID == oRG_ID
                         select c).FirstOrDefault();

                    t.IMPORT_STATUS = "Success";
                    t.IMPORT_PROGRESS = "100";
                    t.IMPORT_PROGRESS_MSG = "Import complete.";

                    ctx.SaveChanges();

                    return t.IMPORT_ID;
                }
                catch
                {
                    return 0;
                }
            }
        }

        public static List<T_WQX_IMPORT_LOG> GetWQX_IMPORT_LOG(string OrgID)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from i in ctx.T_WQX_IMPORT_LOG
                            where i.ORG_ID == OrgID
                            select i).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static T_WQX_IMPORT_LOG GetWQX_IMPORT_LOG_NewActivity()
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from i in ctx.T_WQX_IMPORT_LOG
                            where i.IMPORT_STATUS == "New"
                            && i.TYPE_CD == "Sample"
                            select i).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static int GetWQX_IMPORT_LOG_ProcessingCount()
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from i in ctx.T_WQX_IMPORT_LOG
                            where i.IMPORT_STATUS == "Processing"
                            && i.TYPE_CD == "Sample"
                            select i).Count();
                }
                catch (Exception ex)
                {
                    return 0;
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