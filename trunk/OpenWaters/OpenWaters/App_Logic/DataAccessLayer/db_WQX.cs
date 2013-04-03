using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OpenEnvironment.App_Logic.BusinessLogicLayer;

namespace OpenEnvironment.App_Logic.DataAccessLayer
{
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
                            && a.ORG_ID == OrgID
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

                    if (aCTIVITY_IDX != null) a.ACTIVITY_IDX = aCTIVITY_IDX;
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
            global::System.String tELEPHONE_NUM, global::System.String tELEPHONE_NUM_TYPE, global::System.String TELEPHONE_EXT, String cREATE_USER = "system")
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


        // *************************** XML GENERATION *****************************
        // *********************************************************************
        public static string SP_GenWQXXML(string TypeText, int MonLocIDX)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return ctx.GenWQXXML(TypeText ,MonLocIDX).First();
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


    }
}