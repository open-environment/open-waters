using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OpenEnvironment.App_Logic.BusinessLogicLayer;
//using System.Web.Security;
using System.Security.Cryptography;
using System.Text;

namespace OpenEnvironment.App_Logic.DataAccessLayer
{
    public class ImportSampleResultDisplay
    {
        public int TEMP_SAMPLE_IDX { get; set; }
        public string ORG_ID { get; set; }
        public string PROJECT_ID { get; set; }
        public string MONLOC_ID { get; set; }
        public string ACTIVITY_ID { get; set; }
        public DateTime? ACT_START_DT { get; set; }
        public int TEMP_RESULT_IDX { get; set; }
        public string CHAR_NAME { get; set; }
        public string RESULT_MSR { get; set; }
        public string RESULT_MSR_UNIT { get; set; }
        public string IMPORT_STATUS_CD { get; set; }
        public string IMPORT_STATUS_DESC { get; set; }
    }

    public class CharDisplay
    {
        public string CHAR_NAME { get; set; }
    }

    public class db_WQX
    {

        // *************************** MONLOC **********************************
        // *********************************************************************
        public static int InsertOrUpdateWQX_MONLOC(global::System.Int32? mONLOC_IDX, global::System.String oRG_ID,  global::System.String mONLOC_ID, global::System.String mONLOC_NAME,
            global::System.String mONLOC_TYPE, global::System.String mONLOC_DESC, global::System.String hUC_EIGHT, global::System.String HUC_TWELVE, global::System.String tRIBAL_LAND_IND,
            global::System.String tRIBAL_LAND_NAME, global::System.String lATITUDE_MSR, global::System.String lONGITUDE_MSR, global::System.Int32? sOURCE_MAP_SCALE, 
            global::System.String hORIZ_ACCURACY, global::System.String hORIZ_ACCURACY_UNIT, global::System.String hORIZ_COLL_METHOD, global::System.String hORIZ_REF_DATUM,
            global::System.String vERT_MEASURE, global::System.String vERT_MEASURE_UNIT, global::System.String vERT_COLL_METHOD, global::System.String vERT_REF_DATUM, 
            global::System.String cOUNTRY_CODE, global::System.String sTATE_CODE, global::System.String cOUNTY_CODE, global::System.String wELL_TYPE, global::System.String aQUIFER_NAME,
            global::System.String fORMATION_TYPE, global::System.String wELLHOLE_DEPTH_MSR, global::System.String wELLHOLE_DEPTH_MSR_UNIT, global::System.String wQX_SUBMIT_STATUS,
            DateTime? wQXUpdateDate, Boolean? aCT_IND, Boolean? wQX_IND, String cREATE_USER = "system")
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                Boolean insInd = false;
                try
                {
                    T_WQX_MONLOC a = new T_WQX_MONLOC();

                    if (mONLOC_IDX != null)
                        a = (from c in ctx.T_WQX_MONLOC
                             where c.MONLOC_IDX == mONLOC_IDX
                             select c).FirstOrDefault();
                    else
                        insInd = true;

                    if (a == null) //insert case
                    {
                        a = new T_WQX_MONLOC();
                        insInd = true;
                    }

                    if (oRG_ID != null) a.ORG_ID = oRG_ID;
                    if (mONLOC_ID != null) a.MONLOC_ID = mONLOC_ID;
                    if (mONLOC_NAME != null) a.MONLOC_NAME = mONLOC_NAME;
                    if (mONLOC_TYPE != null) a.MONLOC_TYPE = mONLOC_TYPE;
                    if (mONLOC_DESC != null) a.MONLOC_DESC = mONLOC_DESC;
                    if (hUC_EIGHT != null) a.HUC_EIGHT = hUC_EIGHT;
                    if (HUC_TWELVE != null) a.HUC_TWELVE = HUC_TWELVE;
                    if (tRIBAL_LAND_IND  != null) a.TRIBAL_LAND_IND = tRIBAL_LAND_IND;
                    if (tRIBAL_LAND_NAME != null) a.TRIBAL_LAND_NAME = tRIBAL_LAND_NAME;
                    if (lATITUDE_MSR != null) a.LATITUDE_MSR = lATITUDE_MSR;
                    if (lONGITUDE_MSR != null) a.LONGITUDE_MSR = lONGITUDE_MSR;
                    if (sOURCE_MAP_SCALE != null) a.SOURCE_MAP_SCALE = sOURCE_MAP_SCALE;
                    if (hORIZ_ACCURACY != null) a.HORIZ_ACCURACY = hORIZ_ACCURACY;
                    if (hORIZ_ACCURACY_UNIT != null) a.HORIZ_ACCURACY_UNIT = hORIZ_ACCURACY_UNIT;
                    if (hORIZ_COLL_METHOD != null) a.HORIZ_COLL_METHOD = hORIZ_COLL_METHOD;
                    if (hORIZ_REF_DATUM != null) a.HORIZ_REF_DATUM = hORIZ_REF_DATUM;
                    if (vERT_MEASURE != null) a.VERT_MEASURE= vERT_MEASURE;
                    if (vERT_MEASURE_UNIT != null) a.VERT_MEASURE_UNIT = vERT_MEASURE_UNIT;
                    if (vERT_COLL_METHOD != null) a.VERT_COLL_METHOD = vERT_COLL_METHOD;
                    if (vERT_REF_DATUM != null) a.VERT_REF_DATUM = vERT_REF_DATUM;
                    if (cOUNTRY_CODE != null) a.COUNTRY_CODE = cOUNTRY_CODE;
                    if (sTATE_CODE != null) a.STATE_CODE = sTATE_CODE;
                    if (cOUNTY_CODE != null) a.COUNTY_CODE = cOUNTY_CODE;
                    if (wELL_TYPE != null) a.WELL_TYPE = wELL_TYPE;
                    if (aQUIFER_NAME != null) a.AQUIFER_NAME = aQUIFER_NAME;
                    if (fORMATION_TYPE != null) a.FORMATION_TYPE = fORMATION_TYPE;
                    if (wELLHOLE_DEPTH_MSR != null) a.WELLHOLE_DEPTH_MSR = wELLHOLE_DEPTH_MSR;
                    if (wELLHOLE_DEPTH_MSR_UNIT != null) a.WELLHOLE_DEPTH_MSR_UNIT = wELLHOLE_DEPTH_MSR_UNIT;
                    if (wQX_SUBMIT_STATUS != null) a.WQX_SUBMIT_STATUS = wQX_SUBMIT_STATUS;
                    if (aCT_IND != null) a.ACT_IND = aCT_IND;
                    if (wQX_IND != null) a.WQX_IND = wQX_IND;

                    if (insInd) //insert case
                    {
                        a.CREATE_USERID = cREATE_USER.ToUpper();
                        a.CREATE_DT = System.DateTime.Now;
                        ctx.AddToT_WQX_MONLOC(a);
                    }
                    else
                    {
                        a.UPDATE_USERID = cREATE_USER.ToUpper();
                        a.UPDATE_DT = System.DateTime.Now;
                    }

                    ctx.SaveChanges();

                    return a.MONLOC_IDX;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Returns listing of Monitoring Locations, filtered by Organization ID
        /// </summary>
        public static List<T_WQX_MONLOC> GetWQX_MONLOC(bool? ActInd, string OrgID, bool? WQXPending)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                if (ActInd == false) ActInd = null;
                if (WQXPending == false) WQXPending = null;
                try
                {
                    return (from a in ctx.T_WQX_MONLOC
                            where (!ActInd.HasValue ? true : a.ACT_IND == true)
                            && (!WQXPending.HasValue ? true : a.WQX_SUBMIT_STATUS == "U")
                            && (!WQXPending.HasValue ? true : a.WQX_IND == true)
                            && (OrgID == null ? true : a.ORG_ID == OrgID)
                            orderby a.MONLOC_ID
                            select a).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Returns Monitoring Location record by ID
        /// </summary>
        public static T_WQX_MONLOC GetWQX_MONLOC_ByID(int MonLocIDX)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_WQX_MONLOC
                            where a.MONLOC_IDX == MonLocIDX
                            select a).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static Int32? GetWQX_MONLOC_ByImportID(string ImportID)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_WQX_MONLOC
                            where a.IMPORT_MONLOC_ID == ImportID
                            select a).FirstOrDefault().MONLOC_IDX;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static T_WQX_MONLOC GetWQX_MONLOC_ByIDString(string orgID, string MonLocID)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_WQX_MONLOC
                            where a.MONLOC_ID == MonLocID
                            && a.ORG_ID == orgID
                            select a).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static bool GetT_WQX_MONLOC_PendingInd(string OrgID)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    if (ctx.T_WQX_MONLOC.Any(u => u.ORG_ID == OrgID && u.WQX_SUBMIT_STATUS == "U" && u.WQX_IND == true))
                        return true;
                    else
                        return false;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static int DeleteT_WQX_MONLOC(int monLocIDX)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    //check to see if there are any activities under the monitoring location - if yes then just make it inactive, otherwise delete
                    int iActCount = db_WQX.GetWQX_ACTIVITYByMonLocID(monLocIDX);
                    if (iActCount == 0)
                    {
                        string sql = "DELETE FROM T_WQX_MONLOC WHERE MONLOC_IDX = " + monLocIDX;
                        ctx.ExecuteStoreCommand(sql);
                        return 1;
                    }
                    else
                    {
                        db_WQX.InsertOrUpdateWQX_MONLOC(monLocIDX, null, null, null, null, null, null, null, null, null, null, null, null, null,
                            null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, "U", null, false, null,
                            "system");
                        return -1;
                    }
                }
                catch
                {
                    return 0;
                }
            }

        }



        // *************************** PROJECT *********************************
        // *********************************************************************
        public static List<T_WQX_PROJECT> GetWQX_PROJECT(bool? ActInd, string OrgID, bool? WQXPending)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                if (ActInd == false) ActInd = null;
                if (WQXPending == false) WQXPending = null;

                try
                {
                    return (from a in ctx.T_WQX_PROJECT
                            where (!ActInd.HasValue ? true : a.ACT_IND == true)
                            && a.ORG_ID == OrgID
                            && (!WQXPending.HasValue ? true : a.WQX_SUBMIT_STATUS == "U")
                            && (!WQXPending.HasValue ? true : a.WQX_IND == true)
                            orderby a.PROJECT_ID
                            select a).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static T_WQX_PROJECT GetWQX_PROJECT_ByID(int ProjectIDX)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_WQX_PROJECT
                            where a.PROJECT_IDX == ProjectIDX
                            select a).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static int InsertOrUpdateWQX_PROJECT(global::System.Int32? pROJECT_IDX, global::System.String oRG_ID, global::System.String pROJECT_ID,
            global::System.String pROJECT_NAME, global::System.String pROJECT_DESC, global::System.String sAMP_DESIGN_TYPE_CD, global::System.Boolean? qAPP_APPROVAL_IND,
            global::System.String qAPP_APPROVAL_AGENCY, global::System.String wQX_SUBMIT_STATUS, DateTime? wQX_SUBMIT_DT, Boolean? aCT_IND, Boolean? wQX_IND, String cREATE_USER = "system")
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                Boolean insInd = false;
                try
                {
                    T_WQX_PROJECT a = new T_WQX_PROJECT();

                    if (pROJECT_IDX != null)
                        a = (from c in ctx.T_WQX_PROJECT
                             where c.PROJECT_IDX == pROJECT_IDX
                             select c).FirstOrDefault();

                    if (pROJECT_IDX == null) //insert case
                    {
                        a = new T_WQX_PROJECT();
                        insInd = true;
                    }

                    if (oRG_ID != null) a.ORG_ID = oRG_ID;
                    if (pROJECT_ID != null) a.PROJECT_ID = pROJECT_ID;
                    if (pROJECT_NAME != null) a.PROJECT_NAME = pROJECT_NAME;
                    if (pROJECT_DESC != null) a.PROJECT_DESC = pROJECT_DESC;
                    if (sAMP_DESIGN_TYPE_CD != null) a.SAMP_DESIGN_TYPE_CD = sAMP_DESIGN_TYPE_CD;
                    if (qAPP_APPROVAL_IND != null) a.QAPP_APPROVAL_IND = qAPP_APPROVAL_IND;
                    if (qAPP_APPROVAL_AGENCY != null) a.QAPP_APPROVAL_AGENCY = qAPP_APPROVAL_AGENCY;
                    if (wQX_SUBMIT_STATUS != null) a.WQX_SUBMIT_STATUS = wQX_SUBMIT_STATUS;
                    if (wQX_SUBMIT_DT != null) a.WQX_UPDATE_DT = wQX_SUBMIT_DT;
                    if (aCT_IND != null) a.ACT_IND = aCT_IND;
                    if (wQX_IND != null) a.WQX_IND = wQX_IND;

                    if (insInd) //insert case
                    {
                        a.CREATE_USERID = cREATE_USER.ToUpper();
                        a.CREATE_DT = System.DateTime.Now;
                        ctx.AddToT_WQX_PROJECT(a);
                    }
                    else
                    {
                        a.UPDATE_USERID = cREATE_USER.ToUpper();
                        a.UPDATE_DT = System.DateTime.Now;
                    }

                    ctx.SaveChanges();

                    return a.PROJECT_IDX;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }

        public static int DeleteT_WQX_PROJECT(int ProjectIDX)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    T_WQX_PROJECT r = new T_WQX_PROJECT();
                    r = (from c in ctx.T_WQX_PROJECT where c.PROJECT_IDX == ProjectIDX select c).FirstOrDefault();
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



        // *************************** ACTIVITY **********************************
        // *********************************************************************
        public static int InsertOrUpdateWQX_ACTIVITY(global::System.Int32? aCTIVITY_IDX, global::System.String oRG_ID, global::System.Int32? pROJECT_IDX, global::System.Int32? mONLOC_IDX, global::System.String aCTIVITY_ID, 
            global::System.String aCT_TYPE, global::System.String aCT_MEDIA, global::System.String aCT_SUBMEDIA, global::System.DateTime? aCT_START_DT, global::System.DateTime? aCT_END_DT,
            global::System.String aCT_TIME_ZONE, global::System.String aCT_COMMENT, global::System.String wQX_SUBMIT_STATUS, Boolean? aCT_IND, Boolean? wQX_IND, String cREATE_USER = "system")
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                Boolean insInd = false;
                try
                {
                    T_WQX_ACTIVITY a = new T_WQX_ACTIVITY();

                    if (aCTIVITY_IDX != null)
                        a = (from c in ctx.T_WQX_ACTIVITY
                             where c.ACTIVITY_IDX == aCTIVITY_IDX
                             select c).FirstOrDefault();
                    if (aCTIVITY_IDX == null) //insert case
                    {
                        a = new T_WQX_ACTIVITY();
                        insInd = true;
                    }

                    if (oRG_ID != null) a.ORG_ID = oRG_ID;
                    if (mONLOC_IDX != null) a.MONLOC_IDX = mONLOC_IDX;
                    if (pROJECT_IDX != null) a.PROJECT_IDX = (int)pROJECT_IDX;
                    if (aCTIVITY_ID != null) a.ACTIVITY_ID = aCTIVITY_ID;
                    if (aCT_TYPE != null) a.ACT_TYPE = aCT_TYPE;
                    if (aCT_MEDIA != null) a.ACT_MEDIA = aCT_MEDIA;
                    if (aCT_SUBMEDIA != null) a.ACT_SUBMEDIA = aCT_SUBMEDIA;
                    if (aCT_START_DT != null) a.ACT_START_DT = (DateTime)aCT_START_DT;
                    if (aCT_END_DT != null) a.ACT_END_DT = (DateTime)aCT_END_DT;
                    if (aCT_TIME_ZONE != null) a.ACT_TIME_ZONE = aCT_TIME_ZONE;
                    if (aCT_COMMENT != null) a.ACT_COMMENT = aCT_COMMENT;
                    if (wQX_SUBMIT_STATUS != null) a.WQX_SUBMIT_STATUS = wQX_SUBMIT_STATUS;
                    if (aCT_IND != null) a.ACT_IND = aCT_IND;
                    if (wQX_IND != null) a.WQX_IND = wQX_IND;

                    //put in Timezone if missing
                    if (a.ACT_TIME_ZONE == null)
                        a.ACT_TIME_ZONE = Utils.GetTimeZone(a.ACT_START_DT, "Central Standard Time");

                    if (insInd) //insert case
                    {
                        a.CREATE_USERID = cREATE_USER.ToUpper();
                        a.CREATE_DT = System.DateTime.Now;
                        ctx.AddToT_WQX_ACTIVITY(a);
                    }
                    else
                    {
                        a.UPDATE_USERID = cREATE_USER.ToUpper();
                        a.UPDATE_DT = System.DateTime.Now;
                    }

                    ctx.SaveChanges();

                    return a.ACTIVITY_IDX;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }

        public static List<T_WQX_ACTIVITY> GetWQX_ACTIVITY(bool ActInd, string OrgID, int? MonLocIDX, DateTime? startDt, DateTime? endDt, string ActType, bool WQXPending, int? ProjectIDX)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_WQX_ACTIVITY
                            where (ActInd ? a.ACT_IND == true : true)
                            && a.ORG_ID == OrgID
                            && (WQXPending ? a.WQX_SUBMIT_STATUS == "U" : true )
                            && (WQXPending ? a.WQX_IND == true : true)
                            && (MonLocIDX == null ? true : a.MONLOC_IDX == MonLocIDX)
                            && (startDt == null ? true : a.ACT_START_DT >= startDt)
                            && (endDt == null ? true : a.ACT_START_DT <= endDt)
                            && (ActType == null ? true : a.ACT_TYPE == ActType)
                            && (ProjectIDX == null ? true : a.PROJECT_IDX == ProjectIDX)
                            orderby a.ACT_END_DT descending
                            select a).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static T_WQX_ACTIVITY GetWQX_ACTIVITY_ByID(int ActivityIDX)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_WQX_ACTIVITY
                            where a.ACTIVITY_IDX == ActivityIDX
                            select a).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static T_WQX_ACTIVITY GetWQX_ACTIVITY_ByUnique(string OrgID, string ActivityID)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_WQX_ACTIVITY
                            where a.ACTIVITY_ID == ActivityID
                            && a.ORG_ID == OrgID
                            select a).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static int GetWQX_ACTIVITYByMonLocID(int MonLocIDX)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_WQX_ACTIVITY
                            where a.MONLOC_IDX == MonLocIDX
                            select a).Count();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        // *************************** ACTIVITY METRICS*************************
        // *********************************************************************
        public static int InsertOrUpdateWQX_ACTIVITY_METRIC(global::System.Int32? aCTIVITY_METRIC_IDX, global::System.Int32 aCTIVITY_IDX, global::System.String mETRIC_TYPE_ID,
            global::System.String mETRIC_TYPE_ID_CONTEXT, global::System.String mETRIC_TYPE_NAME, global::System.String cITATION_TITLE, global::System.String cITATION_CREATOR,
            global::System.String cITATION_SUBJECT, global::System.String cITATION_PUBLISHER, global::System.DateTime? cITATION_DATE, global::System.String cITATION_ID,
            global::System.String mETRIC_SCALE, global::System.String mETRIC_FORMULA_DESC, global::System.String mETRIC_VALUE_MSR, global::System.String mETRIC_VALUE_MSR_UNIT, 
            global::System.String mETRIC_SCORE, global::System.String mETRIC_COMMENT, Boolean? wQX_IND, global::System.String wQX_SUBMIT_STATUS, DateTime? WQX_UPDATE_DT, Boolean? aCT_IND, String cREATE_USER = "system")
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                Boolean insInd = false;
                try
                {
                    T_WQX_ACTIVITY_METRIC a = new T_WQX_ACTIVITY_METRIC();

                    if (aCTIVITY_METRIC_IDX != null)
                        a = (from c in ctx.T_WQX_ACTIVITY_METRIC
                             where c.ACTIVITY_METRIC_IDX == aCTIVITY_METRIC_IDX
                             select c).FirstOrDefault();
                    if (aCTIVITY_METRIC_IDX == null) //insert case
                    {
                        a = new T_WQX_ACTIVITY_METRIC();
                        insInd = true;
                    }

                    a.ACTIVITY_IDX = aCTIVITY_IDX;
                    if (mETRIC_TYPE_ID != null) a.METRIC_TYPE_ID= mETRIC_TYPE_ID;
                    if (mETRIC_TYPE_ID_CONTEXT != null) a.METRIC_TYPE_ID_CONTEXT = mETRIC_TYPE_ID_CONTEXT;
                    if (mETRIC_TYPE_NAME != null) a.METRIC_TYPE_NAME = mETRIC_TYPE_NAME;
                    if (cITATION_TITLE != null) a.CITATION_TITLE = cITATION_TITLE;
                    if (cITATION_CREATOR != null) a.CITATION_CREATOR = cITATION_CREATOR;
                    if (cITATION_SUBJECT != null) a.CITATION_SUBJECT = cITATION_SUBJECT;
                    if (cITATION_PUBLISHER != null) a.CITATION_PUBLISHER= cITATION_PUBLISHER;
                    if (cITATION_DATE != null) a.CITATION_DATE = (DateTime)cITATION_DATE;
                    if (cITATION_ID != null) a.CITATION_ID = cITATION_ID;
                    if (mETRIC_SCALE != null) a.METRIC_SCALE = mETRIC_SCALE;
                    if (mETRIC_FORMULA_DESC != null) a.METRIC_FORMULA_DESC = mETRIC_FORMULA_DESC;
                    if (mETRIC_VALUE_MSR != null) a.METRIC_VALUE_MSR = mETRIC_VALUE_MSR;
                    if (mETRIC_VALUE_MSR_UNIT != null) a.METRIC_VALUE_MSR_UNIT = mETRIC_VALUE_MSR_UNIT;
                    if (mETRIC_SCORE != null) a.METRIC_SCORE = mETRIC_SCORE;
                    if (mETRIC_COMMENT != null) a.METRIC_COMMENT = mETRIC_COMMENT;
                    if (wQX_IND != null) a.WQX_IND = wQX_IND;
                    if (wQX_SUBMIT_STATUS != null) a.WQX_SUBMIT_STATUS = wQX_SUBMIT_STATUS;
                    if (WQX_UPDATE_DT != null) a.WQX_UPDATE_DT = WQX_UPDATE_DT;
                    if (aCT_IND != null) a.ACT_IND = aCT_IND;

                    if (insInd) //insert case
                    {
                        a.CREATE_USERID = cREATE_USER.ToUpper();
                        a.CREATE_DT = System.DateTime.Now;
                        ctx.AddToT_WQX_ACTIVITY_METRIC(a);
                    }
                    else
                    {
                        a.UPDATE_USERID = cREATE_USER.ToUpper();
                        a.UPDATE_DT = System.DateTime.Now;
                    }

                    ctx.SaveChanges();

                    return a.ACTIVITY_METRIC_IDX;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }



        // *************************** BIO INDICES******************************
        // *********************************************************************
        public static int InsertOrUpdateWQX_BIO_HABITAT_INDEX(global::System.Int32? bIO_HABITAT_INDEX_IDX, global::System.String oRG_ID, global::System.Int32? mONLOC_IDX, 
            global::System.String iNDEX_ID, global::System.String iNDEX_TYPE_ID, global::System.String iNDEX_TYPE_ID_CONTEXT, global::System.String INDEX_TYPE_NAME, 
            global::System.String rESOURCE_TITLE, global::System.String rESOURCE_CREATOR, global::System.String rESOURCE_SUBJECT, global::System.String rESOURCE_PUBLISHER, 
            global::System.DateTime? rESOURCE_DATE, global::System.String rESOURCE_ID, global::System.String iNDEX_TYPE_SCALE, global::System.String iNDEX_SCORE, global::System.String iNDEX_QUAL_CD,
            global::System.String iNDEX_COMMENT, global::System.DateTime? iNDEX_CALC_DATE, Boolean? wQX_IND, global::System.String wQX_SUBMIT_STATUS, DateTime? WQX_UPDATE_DT, Boolean? aCT_IND, String cREATE_USER = "system")
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                Boolean insInd = false;
                try
                {
                    T_WQX_BIO_HABITAT_INDEX a = new T_WQX_BIO_HABITAT_INDEX();

                    if (bIO_HABITAT_INDEX_IDX != null)
                        a = (from c in ctx.T_WQX_BIO_HABITAT_INDEX
                             where c.BIO_HABITAT_INDEX_IDX == bIO_HABITAT_INDEX_IDX
                             select c).FirstOrDefault();
                    if (bIO_HABITAT_INDEX_IDX == null) //insert case
                    {
                        a = new T_WQX_BIO_HABITAT_INDEX();
                        insInd = true;
                    }

                    if (oRG_ID != null) a.ORG_ID = oRG_ID;
                    if (mONLOC_IDX != null) a.MONLOC_IDX = mONLOC_IDX;
                    if (iNDEX_ID != null) a.INDEX_ID = iNDEX_ID;
                    if (iNDEX_TYPE_ID != null) a.INDEX_TYPE_ID = iNDEX_TYPE_ID;
                    if (iNDEX_TYPE_ID_CONTEXT != null) a.INDEX_TYPE_ID_CONTEXT = iNDEX_TYPE_ID_CONTEXT;
                    if (INDEX_TYPE_NAME != null) a.INDEX_TYPE_NAME = INDEX_TYPE_NAME;
                    if (rESOURCE_TITLE != null) a.RESOURCE_TITLE = rESOURCE_TITLE;
                    if (rESOURCE_CREATOR != null) a.RESOURCE_CREATOR = rESOURCE_CREATOR;
                    if (rESOURCE_SUBJECT != null) a.RESOURCE_SUBJECT = rESOURCE_SUBJECT;
                    if (rESOURCE_PUBLISHER != null) a.RESOURCE_PUBLISHER = rESOURCE_PUBLISHER;
                    if (rESOURCE_DATE != null) a.RESOURCE_DATE = (DateTime)rESOURCE_DATE;
                    if (rESOURCE_ID != null) a.RESOURCE_ID = rESOURCE_ID;
                    if (iNDEX_TYPE_SCALE != null) a.INDEX_TYPE_SCALE = iNDEX_TYPE_SCALE;
                    if (iNDEX_SCORE != null) a.INDEX_SCORE = iNDEX_SCORE;
                    if (iNDEX_QUAL_CD != null) a.INDEX_QUAL_CD = iNDEX_QUAL_CD;
                    if (iNDEX_COMMENT != null) a.INDEX_COMMENT = iNDEX_COMMENT;
                    if (iNDEX_CALC_DATE != null) a.INDEX_CALC_DATE = iNDEX_CALC_DATE;
                    if (wQX_SUBMIT_STATUS != null) a.WQX_SUBMIT_STATUS = wQX_SUBMIT_STATUS;
                    if (aCT_IND != null) a.ACT_IND = aCT_IND;
                    if (wQX_IND != null) a.WQX_IND = wQX_IND;

                    if (insInd) //insert case
                    {
                        a.CREATE_USERID = cREATE_USER.ToUpper();
                        a.CREATE_DT = System.DateTime.Now;
                        ctx.AddToT_WQX_BIO_HABITAT_INDEX(a);
                    }
                    else
                    {
                        a.UPDATE_USERID = cREATE_USER.ToUpper();
                        a.UPDATE_DT = System.DateTime.Now;
                    }

                    ctx.SaveChanges();

                    return a.BIO_HABITAT_INDEX_IDX;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }


        // *************************** RESULTS    ******************************
        // *********************************************************************
        public static List<T_WQX_RESULT> GetT_WQX_RESULT(int ActivityIDX)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_WQX_RESULT
                            where a.ACTIVITY_IDX == ActivityIDX
                            orderby a.CHAR_NAME ascending
                            select a).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static int GetT_WQX_RESULTCount(string OrgID)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from r in ctx.T_WQX_RESULT
                            join a in ctx.T_WQX_ACTIVITY on r.ACTIVITY_IDX equals a.ACTIVITY_IDX
                            where a.ORG_ID == OrgID
                            select r).Count();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static int DeleteT_WQX_RESULT(int ResultIDX)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    T_WQX_RESULT r = new T_WQX_RESULT();
                    r = (from c in ctx.T_WQX_RESULT where c.RESULT_IDX == ResultIDX select c).FirstOrDefault();
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

        public static int InsertOrUpdateT_WQX_RESULT(global::System.Int32? rESULT_IDX, global::System.Int32 aCTIVITY_IDX, global::System.String cHAR_NAME,
            global::System.String rESULT_MSR, global::System.String rESULT_MSR_UNIT, global::System.Int32? aNALYTIC_METHOD_IDX, global::System.String dETECTION_LIMIT, 
            global::System.String rESULT_COMMENT, global::System.String bIO_INTENT_NAME, global::System.String bIO_INDIVIDUAL_ID, global::System.String bIO_TAXONOMY, 
            global::System.String bIO_SAMPLE_TISSUE_ANATOMY, String cREATE_USER = "system")
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                Boolean insInd = false;
                try
                {
                    T_WQX_RESULT a = new T_WQX_RESULT();

                    if (rESULT_IDX != null)
                        a = (from c in ctx.T_WQX_RESULT
                             where c.RESULT_IDX == rESULT_IDX
                             select c).FirstOrDefault();
                    if (a.RESULT_IDX == 0) //insert case
                    {
                        a = new T_WQX_RESULT();
                        insInd = true;
                    }

                    a.ACTIVITY_IDX = aCTIVITY_IDX;
                    if (cHAR_NAME != null) a.CHAR_NAME = cHAR_NAME;
                    if (rESULT_MSR != null) a.RESULT_MSR = rESULT_MSR;
                    if (rESULT_MSR_UNIT != null) a.RESULT_MSR_UNIT = rESULT_MSR_UNIT;
                    if (aNALYTIC_METHOD_IDX != null) a.ANALYTIC_METHOD_IDX = aNALYTIC_METHOD_IDX;
                    if (dETECTION_LIMIT != null) a.DETECTION_LIMIT = dETECTION_LIMIT;
                    if (rESULT_COMMENT != null) a.RESULT_COMMENT = rESULT_COMMENT;

                    if (bIO_INTENT_NAME != null) a.BIO_INTENT_NAME = bIO_INTENT_NAME;
                    if (bIO_INDIVIDUAL_ID != null) a.BIO_INDIVIDUAL_ID = bIO_INDIVIDUAL_ID;
                    if (bIO_TAXONOMY != null) a.BIO_SUBJECT_TAXONOMY = bIO_TAXONOMY;
                    if (bIO_SAMPLE_TISSUE_ANATOMY != null) a.BIO_SAMPLE_TISSUE_ANATOMY = bIO_SAMPLE_TISSUE_ANATOMY;

                    if (insInd) //insert case
                    {
                        //a.CREATE_USERID = cREATE_USER.ToUpper();
                        //a.CREATE_DT = System.DateTime.Now;
                        ctx.AddToT_WQX_RESULT(a);
                    }
                    else
                    {
                        //a.UPDATE_USERID = cREATE_USER.ToUpper();
                        //a.UPDATE_DT = System.DateTime.Now;
                    }

                    ctx.SaveChanges();

                    return a.RESULT_IDX;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }


        public static List<CharDisplay> GetT_WQX_RESULT_SampledCharacteristics()
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from r in ctx.T_WQX_RESULT
                            select new CharDisplay{
                            CHAR_NAME = r.CHAR_NAME
                            }).Distinct().ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        // *************************** ORGANIZATION*****************************
        // *********************************************************************
        public static List<T_WQX_ORGANIZATION> GetWQX_ORGANIZATION()
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_WQX_ORGANIZATION
                            orderby a.ORG_FORMAL_NAME
                            select a).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static List<T_WQX_ORGANIZATION> GetWQX_ORGANIZATIONCanSubmit()
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_WQX_ORGANIZATION
                            where a.CDX_SUBMIT_IND == true
                            orderby a.ORG_FORMAL_NAME
                            select a).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }



        /// <summary>
        /// Returns single Organization record based on ID.
        /// </summary>
        public static T_WQX_ORGANIZATION GetWQX_ORGANIZATION_ByID(string OrgID)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_WQX_ORGANIZATION
                            where a.ORG_ID == OrgID
                            select a).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static int InsertOrUpdateT_WQX_ORGANIZATION(global::System.String oRG_ID, global::System.String oRG_NAME, global::System.String oRG_DESC,
            global::System.String tRIBAL_CODE, global::System.String eLECTRONIC_ADDRESS, global::System.String eLECTRONICADDRESSTYPE,
            global::System.String tELEPHONE_NUM, global::System.String tELEPHONE_NUM_TYPE, global::System.String TELEPHONE_EXT, global::System.String cDX_SUBMITTER_ID, 
            global::System.String cDX_SUBMITTER_PWD, global::System.Boolean? cDX_SUBMIT_IND, String cREATE_USER = "system")
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                Boolean insInd = false;
                try
                {
                    T_WQX_ORGANIZATION a = new T_WQX_ORGANIZATION();

                    if (oRG_ID != null)
                        a = (from c in ctx.T_WQX_ORGANIZATION
                             where c.ORG_ID == oRG_ID
                             select c).FirstOrDefault();

                    if (a == null) //insert case
                    {
                        a = new T_WQX_ORGANIZATION();
                        insInd = true;
                        a.ORG_ID = oRG_ID;
                    }

                    if (oRG_NAME != null) a.ORG_FORMAL_NAME = oRG_NAME;
                    if (oRG_DESC != null) a.ORG_DESC = oRG_DESC;
                    if (tRIBAL_CODE != null) a.TRIBAL_CODE = tRIBAL_CODE;
                    if (eLECTRONIC_ADDRESS != null) a.ELECTRONICADDRESS = eLECTRONIC_ADDRESS;
                    if (eLECTRONICADDRESSTYPE != null) a.ELECTRONICADDRESSTYPE = eLECTRONICADDRESSTYPE;
                    if (tELEPHONE_NUM != null) a.TELEPHONE_NUM = tELEPHONE_NUM;
                    if (tELEPHONE_NUM_TYPE != null) a.TELEPHONE_NUM_TYPE = tELEPHONE_NUM_TYPE;
                    if (TELEPHONE_EXT != null) a.TELEPHONE_EXT = TELEPHONE_EXT;
                    if (cDX_SUBMITTER_ID != null) a.CDX_SUBMITTER_ID = cDX_SUBMITTER_ID;
                    if (cDX_SUBMIT_IND != null) a.CDX_SUBMIT_IND = cDX_SUBMIT_IND;
                    if (cDX_SUBMITTER_PWD != null && cDX_SUBMITTER_PWD != "--------")
                    {
                        //encrypt CDX submitter password for increased security
                        string encryptOauth = new SimpleAES().Encrypt(cDX_SUBMITTER_PWD);
                        encryptOauth = System.Web.HttpUtility.UrlEncode(encryptOauth);
                        a.CDX_SUBMITTER_PWD_HASH = encryptOauth;
                        a.CDX_SUBMIT_IND = true;
                    }

                    if (insInd) //insert case
                    {
                        a.CREATE_USERID = cREATE_USER.ToUpper();
                        a.CREATE_DT = System.DateTime.Now;
                        ctx.AddToT_WQX_ORGANIZATION(a);
                    }
                    else
                    {
                        a.UPDATE_USERID = cREATE_USER.ToUpper();
                        a.UPDATE_DT = System.DateTime.Now;
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


        // *************************** USER ORGANIZATION************************
        // *********************************************************************
        public static List<T_WQX_ORGANIZATION> GetWQX_USER_ORGS_ByUserIDX(int UserIDX)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_WQX_USER_ORGS
                            join b in ctx.T_WQX_ORGANIZATION on a.ORG_ID equals b.ORG_ID
                            where a.USER_IDX == UserIDX
                            select b).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static int InsertT_WQX_USER_ORGS(global::System.String oRG_ID, global::System.Int32 uSER_IDX, string rOLE_CD, String cREATE_USER = "system")
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    T_WQX_USER_ORGS a = new T_WQX_USER_ORGS();

                    a.ORG_ID = oRG_ID;
                    a.USER_IDX = uSER_IDX;
                    if (rOLE_CD != null) a.ROLE_CD = rOLE_CD;
                    a.CREATE_USERID = cREATE_USER.ToUpper();
                    a.CREATE_DT = System.DateTime.Now;

                    ctx.AddToT_WQX_USER_ORGS(a);
                    ctx.SaveChanges();

                    return 1;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }

        public static int DeleteT_WQX_USER_ORGS(global::System.String oRG_ID, global::System.Int32 uSER_IDX)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    T_WQX_USER_ORGS r = new T_WQX_USER_ORGS();
                    r = (from c in ctx.T_WQX_USER_ORGS where c.USER_IDX == uSER_IDX && c.ORG_ID == oRG_ID  select c).FirstOrDefault();
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

        public static List<T_OE_USERS> GetT_OE_USERSInOrganization(string OrgID)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    var users = from itemA in ctx.T_OE_USERS
                                join itemB in ctx.T_WQX_USER_ORGS on itemA.USER_IDX equals itemB.USER_IDX
                                where itemB.ORG_ID == OrgID
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

        public static List<T_OE_USERS> GetT_OE_USERSNotInOrganization(string OrgID)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    //first get all users 
                    var allUsers = (from itemA in ctx.T_OE_USERS select itemA);

                    //next get all users in role
                    var UsersInRole = (from itemA in ctx.T_OE_USERS
                                       join itemB in ctx.T_WQX_USER_ORGS on itemA.USER_IDX equals itemB.USER_IDX
                                       where itemB.ORG_ID == OrgID
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

        public static List<T_OE_USERS> GetWQX_USER_ORGS_AdminsByOrg(string OrgID)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_WQX_USER_ORGS
                            join b in ctx.T_OE_USERS on a.USER_IDX equals b.USER_IDX
                            where a.ORG_ID == OrgID
                            select b).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }



        // *************************** IMPORT_TEMPLATE    ******************************
        // *****************************************************************************
        public static int InsertOrUpdateWQX_IMPORT_TEMPLATE(global::System.Int32? tEMPLATE_ID, global::System.String oRG_ID, string tYPE_CD, string tEMPLATE_NAME, String cREATE_USER = "system")
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                Boolean insInd = false;
                try
                {
                    T_WQX_IMPORT_TEMPLATE a = new T_WQX_IMPORT_TEMPLATE();

                    if (tEMPLATE_ID != null)
                        a = (from c in ctx.T_WQX_IMPORT_TEMPLATE
                             where c.TEMPLATE_ID == tEMPLATE_ID
                             select c).FirstOrDefault();

                    if (tEMPLATE_ID == null) //insert case
                        insInd = true;

                    if (oRG_ID != null) a.ORG_ID = oRG_ID;
                    if (tYPE_CD != null) a.TYPE_CD = tYPE_CD;
                    if (tEMPLATE_NAME != null) a.TEMPLATE_NAME = tEMPLATE_NAME;

                    if (insInd) //insert case
                    {
                        a.CREATE_DT = System.DateTime.Now;
                        a.CREATE_USERID = cREATE_USER;
                        ctx.AddToT_WQX_IMPORT_TEMPLATE(a);
                    }

                    ctx.SaveChanges();

                    return a.TEMPLATE_ID;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }
                
        public static List<T_WQX_IMPORT_TEMPLATE> GetWQX_IMPORT_TEMPLATE(string OrgID)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_WQX_IMPORT_TEMPLATE
                            where a.ORG_ID == OrgID
                            orderby a.TEMPLATE_ID
                            select a).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static int DeleteT_WQX_IMPORT_TEMPLATE(int TemplateID)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    string sql = "DELETE FROM T_WQX_IMPORT_TEMPLATE WHERE TEMPLATE_ID = " + TemplateID;
                    ctx.ExecuteStoreCommand(sql);
                    return 1;
                }
                catch
                {
                    return 0;
                }
            }

        }


        // *************************** IMPORT_TEMPLATE_DTL    ******************************
        // *****************************************************************************
        public static T_WQX_IMPORT_TEMPLATE_DTL GetWQX_IMPORT_TEMPLATE_DTL_byFieldMap(int TemplateID, string FieldMap)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_WQX_IMPORT_TEMPLATE_DTL
                            where a.TEMPLATE_ID == TemplateID
                            && a.FIELD_MAP == FieldMap
                            select a).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        
        public static List<T_WQX_IMPORT_TEMPLATE_DTL> GetWQX_IMPORT_TEMPLATE_DTL_CharsByTemplateID(int TemplateID)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_WQX_IMPORT_TEMPLATE_DTL
                            where a.TEMPLATE_ID == TemplateID
                            && a.FIELD_MAP == "CHAR"
                            select a).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static List<T_WQX_IMPORT_TEMPLATE_DTL> GetWQX_IMPORT_TEMPLATE_DTL_DynamicByTemplateID(int TemplateID)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_WQX_IMPORT_TEMPLATE_DTL
                            where a.TEMPLATE_ID == TemplateID
                            && a.COL_NUM > 0
                            orderby a.COL_NUM
                            select a).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static List<T_WQX_IMPORT_TEMPLATE_DTL> GetWQX_IMPORT_TEMPLATE_DTL_HardCodeByTemplateID(int TemplateID)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_WQX_IMPORT_TEMPLATE_DTL
                            where a.TEMPLATE_ID == TemplateID
                            && a.COL_NUM == 0
                            orderby a.TEMPLATE_DTL_ID
                            select a).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static int DeleteT_WQX_IMPORT_TEMPLATE_DTL(int TemplateDtlID)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    string sql = "DELETE FROM T_WQX_IMPORT_TEMPLATE_DTL WHERE TEMPLATE_DTL_ID = " + TemplateDtlID;
                    ctx.ExecuteStoreCommand(sql);
                    return 1;
                }
                catch
                {
                    return 0;
                }
            }

        }

        public static int InsertOrUpdateWQX_IMPORT_TEMPLATE_DTL(global::System.Int32? tEMPLATE_DTL_ID, global::System.Int32? tEMPLATE_ID, global::System.Int32? cOL_NUM, global::System.String fIELD_MAP,
            string cHAR_NAME, string cHAR_DEFAULT_UNIT, String cREATE_USER = "system")
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                Boolean insInd = false;
                try
                {
                    T_WQX_IMPORT_TEMPLATE_DTL a = new T_WQX_IMPORT_TEMPLATE_DTL();

                    if (tEMPLATE_ID != null)
                        a = (from c in ctx.T_WQX_IMPORT_TEMPLATE_DTL
                             where c.TEMPLATE_DTL_ID == tEMPLATE_DTL_ID
                             select c).FirstOrDefault();

                    if (a == null) //insert case
                    {
                        insInd = true;
                        a = new T_WQX_IMPORT_TEMPLATE_DTL();
                    }

                    if (tEMPLATE_ID != null) a.TEMPLATE_ID = tEMPLATE_ID.ConvertOrDefault<int>();
                    if (cOL_NUM != null) a.COL_NUM = cOL_NUM.ConvertOrDefault<int>();
                    if (fIELD_MAP != null) a.FIELD_MAP = fIELD_MAP;
                    if (cHAR_NAME != null) a.CHAR_NAME = cHAR_NAME;
                    if (cHAR_DEFAULT_UNIT != null) a.CHAR_DEFAULT_UNIT = cHAR_DEFAULT_UNIT;

                    if (insInd) //insert case
                    {
                        a.CREATE_DT = System.DateTime.Now;
                        a.CREATE_USERID = cREATE_USER;
                        ctx.AddToT_WQX_IMPORT_TEMPLATE_DTL(a);
                    }

                    ctx.SaveChanges();

                    return a.TEMPLATE_DTL_ID;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }
                


        // *************************** IMPORT_DATA    ******************************
        // *****************************************************************************
        public static int InsertOrUpdateWQX_IMPORT_TEMP_SAMPLE(global::System.Int32? tEMP_SAMPLE_IDX, string uSER_ID, global::System.String oRG_ID, global::System.Int32? pROJECT_IDX,
            string pROJECT_ID, global::System.Int32? mONLOC_IDX, string mONLOC_ID, global::System.Int32? aCTIVITY_IDX, global::System.String aCTIVITY_ID,
            global::System.String aCT_TYPE, global::System.String aCT_MEDIA, global::System.String aCT_SUBMEDIA, global::System.DateTime? aCT_START_DT, global::System.DateTime? aCT_END_DT,
            global::System.String aCT_TIME_ZONE, global::System.String aCT_COMMENT, string sTATUS_CD, string sTATUS_DESC)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                Boolean insInd = false;
                try
                {
                    T_WQX_IMPORT_TEMP_SAMPLE a = new T_WQX_IMPORT_TEMP_SAMPLE();

                    if (tEMP_SAMPLE_IDX != null)
                        a = (from c in ctx.T_WQX_IMPORT_TEMP_SAMPLE
                             where c.TEMP_SAMPLE_IDX == tEMP_SAMPLE_IDX
                             select c).FirstOrDefault();

                    if (tEMP_SAMPLE_IDX == null) //insert case
                        insInd = true;

                    if (uSER_ID != null) a.USER_ID = uSER_ID;
                    if (oRG_ID != null) a.ORG_ID = oRG_ID;
                    if (pROJECT_IDX != null) a.PROJECT_IDX = pROJECT_IDX;
                    if (pROJECT_ID != null) a.PROJECT_ID = pROJECT_ID;
                    if (mONLOC_IDX != null) a.MONLOC_IDX = mONLOC_IDX;
                    if (mONLOC_ID != null) a.MONLOC_ID = mONLOC_ID;
                    if (aCTIVITY_IDX != null) a.ACTIVITY_IDX = aCTIVITY_IDX;
                    if (aCTIVITY_ID != null) a.ACTIVITY_ID = aCTIVITY_ID;
                    if (aCT_TYPE != null) a.ACT_TYPE = aCT_TYPE;
                    if (aCT_MEDIA != null) a.ACT_MEDIA = aCT_MEDIA;
                    if (aCT_SUBMEDIA != null) a.ACT_SUBMEDIA = aCT_SUBMEDIA;
                    if (aCT_START_DT != null) a.ACT_START_DT = aCT_START_DT;
                    if (aCT_END_DT != null) a.ACT_END_DT = (DateTime)aCT_END_DT;
                    if (aCT_TIME_ZONE != null) a.ACT_TIME_ZONE = aCT_TIME_ZONE;
                    //put in Timezone if missing
                    if (a.ACT_TIME_ZONE == null)
                        a.ACT_TIME_ZONE = Utils.GetTimeZone(a.ACT_START_DT.ConvertOrDefault<DateTime>(), "Central Standard Time");

                    if (aCT_COMMENT != null) a.ACT_COMMENT = aCT_COMMENT;
                    if (sTATUS_CD != null) a.IMPORT_STATUS_CD = sTATUS_CD;
                    if (sTATUS_DESC != null) a.IMPORT_STATUS_DESC = sTATUS_DESC;

                    if (insInd) //insert case
                        ctx.AddToT_WQX_IMPORT_TEMP_SAMPLE(a);

                    ctx.SaveChanges();

                    return a.TEMP_SAMPLE_IDX;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }

        public static int InsertOrUpdateWQX_IMPORT_TEMP_RESULT(global::System.Int32? tEMP_RESULT_IDX, int tEMP_SAMPLE_IDX, global::System.Int32? rESULT_IDX, string dATA_LOGGER_LINE, 
            string rESULT_DETECT_CONDITION, global::System.String cHAR_NAME, string mETHOD_SPECIATION_NAME, string rESULT_SAMP_FRACTION, global::System.String rESULT_MSR, global::System.String rESULT_MSR_UNIT,
            string rESULT_MSR_QUAL, string rESULT_STATUS, string sTATISTIC_BASE_CODE, string rESULT_VALUE_TYPE, string wEIGHT_BASIS, string tIME_BASIS, string tEMP_BASIS, string pARTICAL_BASIS,
            string pRECISION_VALUE, string bIAS_VALUE, string cONFIDENCE_INTERVAL_VALUE, string rESULT_COMMENT, string dEPTH_HEIGHT_MSR, string dEPTH_HEIGHT_MSR_UNIT,
            string aNALYTIC_METHOD_ID, global::System.String dETECTION_LIMIT_TYPE, global::System.String dETECTION_LIMIT, string sTATUS_CD, string sTATUS_DESC)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                Boolean insInd = false;
                try
                {
                    T_WQX_IMPORT_TEMP_RESULT a = new T_WQX_IMPORT_TEMP_RESULT();

                    if (tEMP_RESULT_IDX != null)
                        a = (from c in ctx.T_WQX_IMPORT_TEMP_RESULT
                             where c.TEMP_RESULT_IDX == tEMP_RESULT_IDX
                             select c).FirstOrDefault();

                    if (tEMP_RESULT_IDX == null) //insert case
                        insInd = true;

                    a.TEMP_SAMPLE_IDX = tEMP_SAMPLE_IDX;
                    if (rESULT_IDX != null) a.RESULT_IDX = rESULT_IDX;
                    if (dATA_LOGGER_LINE != null) a.DATA_LOGGER_LINE = dATA_LOGGER_LINE;
                    if (rESULT_DETECT_CONDITION != null) a.RESULT_DETECT_CONDITION = rESULT_DETECT_CONDITION;
                    if (cHAR_NAME != null) a.CHAR_NAME = cHAR_NAME;
                    if (mETHOD_SPECIATION_NAME != null) a.METHOD_SPECIATION_NAME = mETHOD_SPECIATION_NAME;
                    if (rESULT_SAMP_FRACTION != null) a.RESULT_SAMP_FRACTION = rESULT_SAMP_FRACTION;
                    if (rESULT_MSR != null) a.RESULT_MSR = rESULT_MSR;
                    if (rESULT_MSR_UNIT != null) a.RESULT_MSR_UNIT = rESULT_MSR_UNIT;
                    if (rESULT_MSR_QUAL != null) a.RESULT_MSR_QUAL = rESULT_MSR_QUAL;
                    if (rESULT_STATUS != null) a.RESULT_STATUS = rESULT_STATUS;
                    if (sTATISTIC_BASE_CODE != null) a.STATISTIC_BASE_CODE = sTATISTIC_BASE_CODE;
                    if (rESULT_VALUE_TYPE != null) a.RESULT_VALUE_TYPE = rESULT_VALUE_TYPE;
                    if (wEIGHT_BASIS != null) a.WEIGHT_BASIS = wEIGHT_BASIS;
                    if (tIME_BASIS != null) a.TIME_BASIS = tIME_BASIS;
                    if (tEMP_BASIS != null) a.TEMP_BASIS = tEMP_BASIS;
                    if (pARTICAL_BASIS != null) a.PARTICLESIZE_BASIS = pARTICAL_BASIS;
                    if (pRECISION_VALUE != null) a.PRECISION_VALUE = pRECISION_VALUE;
                    if (bIAS_VALUE != null) a.BIAS_VALUE = bIAS_VALUE;
                    if (cONFIDENCE_INTERVAL_VALUE != null) a.CONFIDENCE_INTERVAL_VALUE = cONFIDENCE_INTERVAL_VALUE;
                    if (rESULT_COMMENT != null) a.RESULT_COMMENT = rESULT_COMMENT;
                    if (dEPTH_HEIGHT_MSR != null) a.DEPTH_HEIGHT_MSR = dEPTH_HEIGHT_MSR;
                    if (dEPTH_HEIGHT_MSR_UNIT != null) a.DEPTH_HEIGHT_MSR_UNIT = dEPTH_HEIGHT_MSR_UNIT;
                    //analysis method
                    if (dETECTION_LIMIT != null) a.DETECTION_LIMIT = dETECTION_LIMIT;
                    if (dETECTION_LIMIT_TYPE != null) a.DETECTION_LIMIT_TYPE = dETECTION_LIMIT_TYPE;
                    if (sTATUS_CD != null) a.IMPORT_STATUS_CD = sTATUS_CD;
                    if (sTATUS_DESC != null) a.IMPORT_STATUS_DESC = sTATUS_DESC;

                    if (insInd) //insert case
                        ctx.AddToT_WQX_IMPORT_TEMP_RESULT(a);

                    ctx.SaveChanges();

                    return a.TEMP_RESULT_IDX;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }

        public static int InsertOrUpdateWQX_IMPORT_TEMP_MONLOC(global::System.Int32? tEMP_MONLOC_IDX, string uSER_ID, global::System.Int32? mONLOC_IDX, global::System.String oRG_ID, 
            global::System.String mONLOC_ID, global::System.String mONLOC_NAME, global::System.String mONLOC_TYPE, global::System.String mONLOC_DESC, global::System.String hUC_EIGHT, 
            global::System.String HUC_TWELVE, global::System.String tRIBAL_LAND_IND, global::System.String tRIBAL_LAND_NAME, global::System.String lATITUDE_MSR, global::System.String lONGITUDE_MSR, 
            global::System.Int32? sOURCE_MAP_SCALE, global::System.String hORIZ_ACCURACY, global::System.String hORIZ_ACCURACY_UNIT, global::System.String hORIZ_COLL_METHOD, global::System.String hORIZ_REF_DATUM,
            global::System.String vERT_MEASURE, global::System.String vERT_MEASURE_UNIT, global::System.String vERT_COLL_METHOD, global::System.String vERT_REF_DATUM,
            global::System.String cOUNTRY_CODE, global::System.String sTATE_CODE, global::System.String cOUNTY_CODE, global::System.String wELL_TYPE, global::System.String aQUIFER_NAME,
            global::System.String fORMATION_TYPE, global::System.String wELLHOLE_DEPTH_MSR, global::System.String wELLHOLE_DEPTH_MSR_UNIT, string sTATUS_CD, string sTATUS_DESC)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                Boolean insInd = false;
                try
                {
                    T_WQX_IMPORT_TEMP_MONLOC a = new T_WQX_IMPORT_TEMP_MONLOC();

                    if (tEMP_MONLOC_IDX != null)
                        a = (from c in ctx.T_WQX_IMPORT_TEMP_MONLOC
                             where c.TEMP_MONLOC_IDX == tEMP_MONLOC_IDX
                             select c).FirstOrDefault();
                    else
                        insInd = true;

                    if (uSER_ID != null) { 
                        a.USER_ID = uSER_ID;
                        if (uSER_ID.Length > 25) { sTATUS_CD = "F"; sTATUS_DESC += "User ID length exceeded. "; }
                    }

                    if (mONLOC_IDX != null) a.MONLOC_IDX = mONLOC_IDX;
                    if (oRG_ID != null) a.ORG_ID = oRG_ID;

                    if (mONLOC_ID != null) { 
                        a.MONLOC_ID = mONLOC_ID.SubStringPlus(0,35).Trim();
                        if (mONLOC_ID.Length > 25) { sTATUS_CD = "F"; sTATUS_DESC += "Monitoring Location ID length exceeded. "; }

                        T_WQX_MONLOC mtemp = db_WQX.GetWQX_MONLOC_ByIDString(oRG_ID, mONLOC_ID);
                        if (mtemp != null) { sTATUS_CD = "F"; sTATUS_DESC += "Monitoring Location ID already exists. ";  }
                    }

                    if (!string.IsNullOrEmpty(mONLOC_NAME))
                    {
                        a.MONLOC_NAME = mONLOC_NAME.SubStringPlus(0, 255).Trim();
                        if (mONLOC_NAME.Length > 255) { sTATUS_CD = "F"; sTATUS_DESC += "Monitoring Location Name length exceeded. "; }
                    }

                    if (!string.IsNullOrEmpty(mONLOC_TYPE))
                    {
                        a.MONLOC_TYPE = mONLOC_TYPE.SubStringPlus(0,45);
                        if (db_Ref.GetT_WQX_REF_DATA_ByKey("MonitoringLocationType", mONLOC_TYPE.Trim()) == false) { sTATUS_CD = "F"; sTATUS_DESC += "Monitoring Location Type not valid. "; }
                        if (mONLOC_TYPE.Length > 45) { sTATUS_CD = "F"; sTATUS_DESC += "Monitoring Location Type length exceeded. "; }
                    }

                    if (!string.IsNullOrEmpty(mONLOC_DESC))
                    {
                        a.MONLOC_DESC = mONLOC_DESC.SubStringPlus(0,1999);
                        if (mONLOC_DESC.Length > 1999) { sTATUS_CD = "F"; sTATUS_DESC += "Monitoring Location Description length exceeded. "; }
                    }

                    if (!string.IsNullOrEmpty(hUC_EIGHT))
                    {
                        a.HUC_EIGHT = hUC_EIGHT.Trim().SubStringPlus(0, 8);
                        if (hUC_EIGHT.Length > 8) { sTATUS_CD = "F"; sTATUS_DESC += "HUC8 length exceeded. "; }
                    }

                    if (!string.IsNullOrEmpty(HUC_TWELVE))
                    {
                        a.HUC_TWELVE = HUC_TWELVE.Trim().SubStringPlus(0,12);
                        if (HUC_TWELVE.Length > 12) { sTATUS_CD = "F"; sTATUS_DESC += "HUC12 length exceeded. "; }
                    }

                    if (!string.IsNullOrEmpty(tRIBAL_LAND_IND))
                    { 
                        a.TRIBAL_LAND_IND = tRIBAL_LAND_IND.SubStringPlus(0,1);
                        if (tRIBAL_LAND_IND.Length > 1) { sTATUS_CD = "F"; sTATUS_DESC += "Tribal Land Indicator length exceeded. "; }
                    }

                    if (!string.IsNullOrEmpty(tRIBAL_LAND_NAME))
                    { 
                        a.TRIBAL_LAND_NAME = tRIBAL_LAND_NAME.SubStringPlus(0,200);
                        if (tRIBAL_LAND_NAME.Length > 200) { sTATUS_CD = "F"; sTATUS_DESC += "Tribal Land Name length exceeded. "; }
                    }

                    if (!string.IsNullOrEmpty(lATITUDE_MSR))
                    {
                        a.LATITUDE_MSR = lATITUDE_MSR.SubStringPlus(0, 30);
                        decimal iii = 0;
                        if (Decimal.TryParse(lATITUDE_MSR, out iii) == false) { sTATUS_CD = "F"; sTATUS_DESC += "Latitude is not decimal format. ";  }
                    }

                    if (!string.IsNullOrEmpty(lONGITUDE_MSR))
                    {
                        a.LONGITUDE_MSR = lONGITUDE_MSR.SubStringPlus(0, 30);
                        decimal iii = 0;
                        if (Decimal.TryParse(lONGITUDE_MSR, out iii) == false) { sTATUS_CD = "F"; sTATUS_DESC += "Longitude is not decimal format. "; }
                    }

                    if (sOURCE_MAP_SCALE != null)
                    {
                        a.SOURCE_MAP_SCALE = sOURCE_MAP_SCALE;
                    }

                    if (!string.IsNullOrEmpty(hORIZ_COLL_METHOD))
                    {
                        a.HORIZ_COLL_METHOD = hORIZ_COLL_METHOD.SubStringPlus(0, 150).Trim();
                        if (db_Ref.GetT_WQX_REF_DATA_ByKey("HorizontalCollectionMethod", hORIZ_COLL_METHOD.Trim()) == false) { sTATUS_CD = "F"; sTATUS_DESC += "Horizontal Collection Method not valid. "; }
                    }

                    if (!string.IsNullOrEmpty(hORIZ_REF_DATUM))
                    {
                        a.HORIZ_REF_DATUM = hORIZ_REF_DATUM.Trim().SubStringPlus(0, 6);
                        if (db_Ref.GetT_WQX_REF_DATA_ByKey("HorizontalCoordinateReferenceSystemDatum", hORIZ_REF_DATUM.Trim()) == false) { sTATUS_CD = "F"; sTATUS_DESC += "Horizontal Collection Datum not valid. "; }
                    }

                    if (!string.IsNullOrEmpty(vERT_MEASURE))
                    {
                        a.VERT_MEASURE = vERT_MEASURE.Trim().SubStringPlus(0,12);
                        if (vERT_MEASURE.Length > 12) { sTATUS_CD = "F"; sTATUS_DESC += "Vertical Measure length exceeded. "; }
                    }

                    if (!string.IsNullOrEmpty(vERT_MEASURE_UNIT))
                    {
                        a.VERT_MEASURE_UNIT = vERT_MEASURE_UNIT.Trim().SubStringPlus(0,12);
                        if (db_Ref.GetT_WQX_REF_DATA_ByKey("MeasureUnit", vERT_MEASURE_UNIT.Trim()) == false) { sTATUS_CD = "F"; sTATUS_DESC += "Vertical Measure Unit not valid. "; }                   
                    }

                    if (!string.IsNullOrEmpty(vERT_COLL_METHOD))
                    {
                        a.VERT_COLL_METHOD = vERT_COLL_METHOD.Trim().SubStringPlus(0,50);
                        if (db_Ref.GetT_WQX_REF_DATA_ByKey("VerticalCollectionMethod", vERT_COLL_METHOD.Trim()) == false) { sTATUS_CD = "F"; sTATUS_DESC += "Vertical Collection Method not acceptable. "; }
                    }

                    if (!string.IsNullOrEmpty(vERT_REF_DATUM))
                    {
                        a.VERT_REF_DATUM = vERT_REF_DATUM.Trim().SubStringPlus(0,6);
                        if (db_Ref.GetT_WQX_REF_DATA_ByKey("VerticalCoordinateReferenceSystemDatum", vERT_REF_DATUM.Trim()) == false) { sTATUS_CD = "F"; sTATUS_DESC += "Vertical Collection Datum not acceptable. "; }
                    }

                    if (!string.IsNullOrEmpty(cOUNTRY_CODE))
                    {
                        a.COUNTRY_CODE = cOUNTRY_CODE.SubStringPlus(0, 2);
                        if (cOUNTRY_CODE.Length > 2) { sTATUS_CD = "F"; sTATUS_DESC += "Country Code length exceeded. "; }
                    }

                    if (!string.IsNullOrEmpty(sTATE_CODE))
                    {
                        a.STATE_CODE = sTATE_CODE.SubStringPlus(0,2);
                        if (sTATE_CODE.Length > 2) { sTATUS_CD = "F"; sTATUS_DESC += "State Code length exceeded. "; }
                    }

                    if (!string.IsNullOrEmpty(cOUNTY_CODE))
                    {
                        a.COUNTY_CODE = cOUNTY_CODE.SubStringPlus(0,3);
                        if (cOUNTY_CODE.Length > 3) { sTATUS_CD = "F"; sTATUS_DESC += "County Code length exceeded. "; }
                    }

                    if (!string.IsNullOrEmpty(wELL_TYPE))
                    {
                        a.WELL_TYPE = wELL_TYPE.Trim().SubStringPlus(0,255);
                    }

                    if (!string.IsNullOrEmpty(aQUIFER_NAME))
                    {
                        a.AQUIFER_NAME = aQUIFER_NAME.Trim().SubStringPlus(0,120);
                    }

                    if (!string.IsNullOrEmpty(fORMATION_TYPE))
                    {
                        a.FORMATION_TYPE = fORMATION_TYPE.Trim().SubStringPlus(0,50);
                    }

                    if (!string.IsNullOrEmpty(wELLHOLE_DEPTH_MSR))
                    {
                        a.WELLHOLE_DEPTH_MSR = wELLHOLE_DEPTH_MSR.Trim().SubStringPlus(0,12);
                    }

                    if (!string.IsNullOrEmpty(wELLHOLE_DEPTH_MSR_UNIT))
                    {
                        a.WELLHOLE_DEPTH_MSR_UNIT = wELLHOLE_DEPTH_MSR_UNIT.Trim().SubStringPlus(0,12);
                    }

                    if (sTATUS_CD != null) a.IMPORT_STATUS_CD = sTATUS_CD;
                    if (sTATUS_DESC != null) a.IMPORT_STATUS_DESC = sTATUS_DESC.SubStringPlus(0,100);

                    if (insInd) //insert case
                        ctx.AddToT_WQX_IMPORT_TEMP_MONLOC(a);

                    ctx.SaveChanges();

                    return a.TEMP_MONLOC_IDX;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }


        public static List<T_WQX_IMPORT_TEMP_MONLOC> GetWQX_IMPORT_TEMP_MONLOC(string UserID)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_WQX_IMPORT_TEMP_MONLOC
                            where a.USER_ID == UserID
                            orderby a.TEMP_MONLOC_IDX
                            select a).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static T_WQX_IMPORT_TEMP_MONLOC GetWQX_IMPORT_TEMP_MONLOC_ByID(int TempMonLocID)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_WQX_IMPORT_TEMP_MONLOC
                            where a.TEMP_MONLOC_IDX == TempMonLocID
                            select a).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static int DeleteT_WQX_IMPORT_TEMP_MONLOC(global::System.String uSER_ID)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    string sql = "DELETE FROM T_WQX_IMPORT_TEMP_MONLOC WHERE USER_ID = '" + uSER_ID + "'";
                    ctx.ExecuteStoreCommand(sql);
                    return 1;
                }
                catch
                {
                    return 0;
                }
            }

        }

        public static int DeleteT_WQX_IMPORT_TEMP_SAMPLE(global::System.String uSER_ID)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    string sql = "DELETE FROM T_WQX_IMPORT_TEMP_SAMPLE WHERE USER_ID = '" + uSER_ID + "'";
                    ctx.ExecuteStoreCommand(sql);
                    return 1;
                }
                catch
                {
                    return 0;
                }
            }

        }

        public static T_WQX_IMPORT_TEMP_SAMPLE GetWQX_IMPORT_TEMP_SAMPLE_ByID(int TempSampleID)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_WQX_IMPORT_TEMP_SAMPLE
                            where a.TEMP_SAMPLE_IDX == TempSampleID
                            select a).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static List<T_WQX_IMPORT_TEMP_RESULT> GetWQX_IMPORT_TEMP_RESULT_ByTempSampIDX(int TempSampIDX)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_WQX_IMPORT_TEMP_RESULT
                            where a.TEMP_SAMPLE_IDX == TempSampIDX
                            select a).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static List<ImportSampleResultDisplay> GetWQX_IMPORT_TEMP_SAMP_RESULT_Disp(string UserID)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_WQX_IMPORT_TEMP_SAMPLE
                            join b in ctx.T_WQX_IMPORT_TEMP_RESULT on a.TEMP_SAMPLE_IDX equals b.TEMP_SAMPLE_IDX
                            where a.USER_ID == UserID
                            orderby b.TEMP_RESULT_IDX
                            select new ImportSampleResultDisplay {
                                TEMP_SAMPLE_IDX = a.TEMP_SAMPLE_IDX,
                                ORG_ID = a.ORG_ID, 
                                PROJECT_ID = a.PROJECT_ID,
                                MONLOC_ID = a.MONLOC_ID, 
                                ACTIVITY_ID = a.ACTIVITY_ID,
                                ACT_START_DT = a.ACT_START_DT,
                                TEMP_RESULT_IDX = b.TEMP_RESULT_IDX,
                                CHAR_NAME = b.CHAR_NAME,
                                RESULT_MSR = b.RESULT_MSR,
                                RESULT_MSR_UNIT = b.RESULT_MSR_UNIT,
                                IMPORT_STATUS_CD = a.IMPORT_STATUS_CD == "F" ? a.IMPORT_STATUS_CD : b.IMPORT_STATUS_CD,
                                IMPORT_STATUS_DESC = a.IMPORT_STATUS_DESC + " " + b.IMPORT_STATUS_DESC
                            }).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }



        // *************************** XML GENERATION ********************************
        // ***************************************************************************
        public static string SP_GenWQXXML_Single(string TypeText, int recordIDX)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return ctx.GenWQXXML_Single(TypeText, recordIDX).First();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static string SP_GenWQXXML_Org(string orgID)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return ctx.GenWQXXML_Org(orgID).First();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        // *************************** ANALYSIS *********************************
        public static List<WQXAnalysis_Result> SP_WQXAnalysis(string TypeText, string OrgID, int? MonLocIDX, string charName, DateTime? startDt, DateTime? endDt)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return ctx.WQXAnalysis(TypeText, OrgID, MonLocIDX.ToString(), charName, startDt, endDt).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static List<V_WQX_ACTIVITY_LATEST> GetV_WQX_ACTIVITY_LATEST(string OrgID)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.V_WQX_ACTIVITY_LATEST
                            where a.ORG_ID == OrgID
                            select a).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static V_WQX_ACTIVITY_LATEST GetV_WQX_ACTIVITY_LATESTByMonLocID(int MonLocIDX)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.V_WQX_ACTIVITY_LATEST
                            where a.MONLOC_IDX == MonLocIDX
                            select a).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

    }
}