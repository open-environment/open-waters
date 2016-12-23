using System;
using System.Collections.Generic;
using System.Linq;
using OpenEnvironment.App_Logic.BusinessLogicLayer;

namespace OpenEnvironment.App_Logic.DataAccessLayer
{
    public class ImportSampleResultDisplay
    {
        public int TEMP_SAMPLE_IDX { get; set; }
        public string ORG_ID { get; set; }
        public string PROJECT_ID { get; set; }
        public string MONLOC_ID { get; set; }
        public string ACTIVITY_ID { get; set; }
        public string ACT_TYPE { get; set; }
        public string ACT_MEDIA { get; set; }
        public string ACT_SUBMEDIA { get; set; }
        public DateTime? ACT_START_DT { get; set; }
        public DateTime? ACT_END_DT { get; set; }
        public string ACT_TIME_ZONE { get; set; }
        public string RELATIVE_DEPTH_NAME { get; set; }
        public string ACT_DEPTHHEIGHT_MSR { get; set; }
        public string ACT_DEPTHHEIGHT_MSR_UNIT { get; set; }
        public string TOP_DEPTHHEIGHT_MSR { get; set; }
        public string TOP_DEPTHHEIGHT_MSR_UNIT { get; set; }
        public string BOT_DEPTHHEIGHT_MSR { get; set; }
        public string BOT_DEPTHHEIGHT_MSR_UNIT { get; set; }
        public string DEPTH_REF_POINT { get; set; }
        public string ACT_COMMENT { get; set; }
        public string BIO_ASSEMBLAGE_SAMPLED { get; set; }
        public string BIO_DURATION_MSR { get; set; }
        public string BIO_DURATION_MSR_UNIT { get; set; }
        public string BIO_SAMP_COMPONENT { get; set; }
        public int? BIO_SAMP_COMPONENT_SEQ { get; set; }
        public string SAMP_COLL_METHOD_ID { get; set; }
        public string SAMP_COLL_METHOD_CTX { get; set; }
        public string SAMP_COLL_EQUIP { get; set; }
        public string SAMP_COLL_EQUIP_COMMENT { get; set; }
        public string SAMP_PREP_ID { get; set; }
        public string SAMP_PREP_CTX { get; set; }

        public int? TEMP_RESULT_IDX { get; set; }
        public string DATA_LOGGER_LINE { get; set; }
        public string RESULT_DETECT_CONDITION { get; set; }
        public string CHAR_NAME { get; set; }
        public string METHOD_SPECIATION_NAME { get; set; }
        public string RESULT_SAMP_FRACTION { get; set; }
        public string RESULT_MSR { get; set; }
        public string RESULT_MSR_UNIT { get; set; }
        public string RESULT_MSR_QUAL { get; set; }
        public string RESULT_STATUS { get; set; }
        public string STATISTIC_BASE_CODE { get; set; }
        public string RESULT_VALUE_TYPE { get; set; }
        public string WEIGHT_BASIS { get; set; }
        public string TIME_BASIS { get; set; }
        public string TEMP_BASIS { get; set; }
        public string PARTICLESIZE_BASIS { get; set; }
        public string PRECISION_VALUE { get; set; }
        public string BIAS_VALUE { get; set; }
        public string RESULT_COMMENT { get; set; }
        public string RES_DEPTH_HEIGHT_MSG { get; set; }
        public string RES_DEPTH_HEIGHT_MSR_UNIT { get; set; }

        public string BIO_INTENT_NAME { get; set; }
        public string BIO_INDIVIDUAL_ID { get; set; }
        public string BIO_SUBJECT_TAXONOMY { get; set; }
        public string BIO_UNIDENTIFIED_SPECIES_ID { get; set; }
        public string BIO_SAMPLE_TISSUE_ANATOMY { get; set; }
        public string GRP_SUMM_COUNT_WEIGHT_MSR { get; set; }
        public string GRP_SUMM_COUNT_WEIGHT_MSR_UNIT { get; set; }
        public string FREQ_CLASS_CODE { get; set; }
        public string FREQ_CLASS_UNIT { get; set; }
        public string ANAL_METHOD_ID { get; set; }
        public string ANAL_METHOD_CTX { get; set; }
        public string LAB_NAME { get; set; }
        public DateTime? ANAL_START_DT { get; set; }
        public DateTime? ANAL_END_DT { get; set; }
        public string LAB_COMMENT_CODE { get; set; }
        public string DETECTION_LIMIT { get; set; }
        public string LAB_REPORTING_LEVEL { get; set; }
        public string PQL { get; set; }
        public string LOWER_QUANT_LIMIT { get; set; }
        public string UPPER_QUANT_LIMIT { get; set; }
        public string DETECTION_LIMIT_UNIT { get; set; }
        public DateTime? LAB_SAMP_PREP_START_DT { get; set; }
        public string DILUTION_FACTOR { get; set; }
        public string IMPORT_STATUS_CD { get; set; }
        public string IMPORT_STATUS_DESC { get; set; }
    }

    public class CharDisplay
    {
        public string CHAR_NAME { get; set; }
    }

    public class ActivityListDisplay
    {
        public int ACTIVITY_IDX { get; set; }
        public string ORG_ID { get; set; }
        public string PROJECT_ID { get; set; }
        public string MONLOC_ID { get; set; }
        public string ACTIVITY_ID { get; set; }
        public string ACT_TYPE { get; set; }
        public string ACT_MEDIA { get; set; }
        public string ACT_SUBMEDIA { get; set; }
        public DateTime? ACT_START_DT { get; set; }
        public DateTime? ACT_END_DT { get; set; }
        public string  ACT_DEPTHHEIGHT_MSR { get; set; }
        public string ACT_DEPTHHEIGHT_MSR_UNIT { get; set; }
        public string TOP_DEPTHHEIGHT_MSR { get; set; }
        public string BOT_DEPTHHEIGHT_MSR { get; set; }
        public string DEPTH_REF_POINT { get; set; }
        public string ACT_COMMENT { get; set; }
        public string SAMP_COLL_METHOD { get; set; }
        public string SAMP_COLL_EQUIP { get; set; }
        public string SAMP_COLL_EQUIP_COMMENT { get; set; }
        public string SAMP_PREP_METHOD { get; set; }
        public Boolean? WQX_IND { get; set; }
        public string WQX_SUBMIT_STATUS { get; set; }
        public Boolean? ACT_IND { get; set; }
    }

    public class ResultGridDisplay
    {
        public int RESULT_IDX { get; set; }
        public int ACTIVITY_IDX { get; set; }
        public string DATA_LOGGER_LINE { get; set; }
        public string RESULT_DETECT_CONDITION { get; set; }
        public string CHAR_NAME { get; set; }
        public string METHOD_SPECIATION_NAME { get; set; }
        public string RESULT_SAMP_FRACTION { get; set; }
        public string RESULT_MSR { get; set; }
        public string RESULT_MSR_UNIT { get; set; }
        public string RESULT_MSR_QUAL { get; set; }
        public string RESULT_STATUS { get; set; }
        public string STATISTIC_BASE_CODE { get; set; }
        public string RESULT_VALUE_TYPE { get; set; }
        public string WEIGHT_BASIS { get; set; }
        public string TIME_BASIS { get; set; }
        public string TEMP_BASIS { get; set; }
        public string PARTICLESIZE_BASIS { get; set; }
        public string PRECISION_VALUE { get; set; }
        public string BIAS_VALUE { get; set; }
        public string RESULT_COMMENT { get; set; }
        public string RES_DEPTH_HEIGHT_MSG { get; set; }
        public string RES_DEPTH_HEIGHT_MSR_UNIT { get; set; }

        public string BIO_INTENT_NAME { get; set; }
        public string BIO_INDIVIDUAL_ID { get; set; }
        public string BIO_SUBJECT_TAXONOMY { get; set; }
        public string BIO_UNIDENTIFIED_SPECIES_ID { get; set; }
        public string BIO_SAMPLE_TISSUE_ANATOMY { get; set; }
        public string GRP_SUMM_COUNT_WEIGHT_MSR { get; set; }
        public string GRP_SUMM_COUNT_WEIGHT_MSR_UNIT { get; set; }

        public int? ANAL_METHOD_IDX { get; set; }
        public string ANAL_METHOD_ID { get; set; }
        public string ANAL_METHOD_CTX { get; set; }
        public int? LAB_IDX { get; set; }
        public string LAB_NAME { get; set; }
        public DateTime? ANAL_START_DT { get; set; }
        public DateTime? ANAL_END_DT { get; set; }
        public string LAB_COMMENT_CODE { get; set; }

        public string DETECTION_LIMIT_TYPE { get; set; }
        public string DETECTION_LIMIT { get; set; }
        public string LAB_REPORTING_LEVEL { get; set; }
        public string PQL { get; set; }
        public string LOWER_QUANT_LIMIT { get; set; }
        public string UPPER_QUANT_LIMIT { get; set; }
        public string DETECTION_LIMIT_UNIT { get; set; }
        public int? LAB_SAMP_PREP_IDX { get; set; }
        public string LAB_SAMP_PREP_ID { get; set; }

    }

    public class UserOrgDisplay
    {
        public int USER_IDX { get; set; }
        public string USER_ID { get; set; }
        public string USER_NAME { get; set; }
        public string ORG_ID { get; set; }
        public string ROLE_CD { get; set; }
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
        public static List<T_WQX_MONLOC> GetWQX_MONLOC(bool ActInd, string OrgID, bool? WQXPending)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                if (WQXPending == false) WQXPending = null;
                try
                {
                    return (from a in ctx.T_WQX_MONLOC
                            where (ActInd ? a.ACT_IND == true : true)
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
        /// Returns listing of Monitoring Locations, filtered by Organization ID
        /// </summary>
        public static List<T_WQX_MONLOC> GetWQX_MONLOC_ByOrgID(string OrgID)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_WQX_MONLOC
                            where (a.ACT_IND == true)
                            && (a.ORG_ID == OrgID)
                            orderby a.MONLOC_ID
                            select a).ToList();
                }
                catch (Exception ex)
                {
                    return null;
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
                    return false; 
                }
            }
        }

        public static int DeleteT_WQX_MONLOC(int monLocIDX, string UserID)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    T_WQX_MONLOC m = db_WQX.GetWQX_MONLOC_ByID(monLocIDX);
                    if (m != null)
                    {
                        if (m.WQX_SUBMIT_STATUS == "Y" && m.ACT_IND == false)
                        {
                            //only actually delete record from database if it has already been set to inactive and WQX status is passed ("Y")
                            string sql = "DELETE FROM T_WQX_MONLOC WHERE MONLOC_IDX = " + monLocIDX;
                            ctx.ExecuteStoreCommand(sql);
                            return 1;
                        }

                        //if there are any activities for this monitoring location, don't delete becuase this would cause WQX to delete all activities for this mon loc.
                        int iActCount = db_WQX.GetWQX_ACTIVITYByMonLocID(monLocIDX);
                        if (iActCount > 0)
                        {
                            return -1;
                        }
                        else
                        {
                            //mark as inactive (deleted), which will send the delete request to EPA-WQX
                            db_WQX.InsertOrUpdateWQX_MONLOC(monLocIDX, null, null, null, null, null, null, null, null, null, null, null, null, null,
                                null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, "U", null, false, null, UserID);
                            return 1;
                        }

                    }
                    else
                        return 0;

                }
                catch
                {
                    return 0;
                }
            }

        }

        public static int GetWQX_MONLOC_MyOrgCount(int UserIDX)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_WQX_MONLOC
                            join b in ctx.T_WQX_USER_ORGS on a.ORG_ID equals b.ORG_ID
                            where b.USER_IDX == UserIDX
                            select a).Count();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }



        // *************************** PROJECT *********************************
        // *********************************************************************
        public static List<T_WQX_PROJECT> GetWQX_PROJECT(bool ActInd, string OrgID, bool? WQXPending)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                if (WQXPending == false) WQXPending = null;

                try
                {
                    return (from a in ctx.T_WQX_PROJECT
                            where (ActInd ? a.ACT_IND == true : true)
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

        public static T_WQX_PROJECT GetWQX_PROJECT_ByIDString(string ProjectID, string OrgID)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_WQX_PROJECT
                            where a.PROJECT_ID == ProjectID
                            && a.ORG_ID == OrgID
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

        public static int DeleteT_WQX_PROJECT(int ProjectIDX, string UserID)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    T_WQX_PROJECT p = db_WQX.GetWQX_PROJECT_ByID(ProjectIDX);
                    if (p != null)
                    {
                        if (p.WQX_SUBMIT_STATUS == "Y" && p.ACT_IND == false)
                        {
                            //only actually delete record from database if it has already been set to inactive and WQX status is passed ("Y")
                            string sql = "DELETE FROM T_WQX_PROJECT WHERE PROJECT_IDX = " + ProjectIDX;
                            ctx.ExecuteStoreCommand(sql);
                            return 1;
                        }

                        //if there are any active activities for this project, don't delete becuase this would cause WQX to delete all activities for this project.
                        int iActCount = db_WQX.GetWQX_ACTIVITYByProjectID(ProjectIDX);
                        if (iActCount > 0)
                            return -1;
                        else
                        {
                            //mark as inactive (deleted), which will send the delete request to EPA-WQX
                            InsertOrUpdateWQX_PROJECT(ProjectIDX, null, null, null, null, null, null, null, "U", null, false, null, UserID);
                            return 1;
                        }

                    }
                    else
                        return 0;



                }
                catch
                {
                    return 0;
                }
            }

        }

        public static int GetWQX_PROJECT_MyOrgCount(int UserIDX)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_WQX_PROJECT
                            join b in ctx.T_WQX_USER_ORGS on a.ORG_ID equals b.ORG_ID
                            where b.USER_IDX == UserIDX
                            select a).Count();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }




        // *************************** ACTIVITY **********************************
        // *********************************************************************
        public static int InsertOrUpdateWQX_ACTIVITY(global::System.Int32? aCTIVITY_IDX, global::System.String oRG_ID, global::System.Int32? pROJECT_IDX, global::System.Int32? mONLOC_IDX, global::System.String aCTIVITY_ID, 
            global::System.String aCT_TYPE, global::System.String aCT_MEDIA, global::System.String aCT_SUBMEDIA, global::System.DateTime? aCT_START_DT, global::System.DateTime? aCT_END_DT,
            global::System.String aCT_TIME_ZONE, global::System.String rELATIVE_DEPTH_NAME, global::System.String aCT_DEPTHHEIGHT_MSR, global::System.String aCT_DEPTHHEIGHT_MSR_UNIT,
            global::System.String tOP_DEPTHHEIGHT_MSR, global::System.String tOP_DEPTHHEIGHT_MSR_UNIT, global::System.String bOT_DEPTHHEIGHT_MSR, global::System.String bOT_DEPTHHEIGHT_MSR_UNIT,
            global::System.String dEPTH_REF_POINT, global::System.String aCT_COMMENT, global::System.String bIO_ASSEMBLAGE_SAMPLED, global::System.String bIO_DURATION_MSR,
            global::System.String bIO_DURATION_MSR_UNIT, global::System.String bIO_SAMP_COMPONENT, int? bIO_SAMP_COMPONENT_SEQ, global::System.String bIO_REACH_LEN_MSR,
            global::System.String bIO_REACH_LEN_MSR_UNIT, global::System.String bIO_REACH_WID_MSR, global::System.String bIO_REACH_WID_MSR_UNIT, int? bIO_PASS_COUNT,
            global::System.String bIO_NET_TYPE, global::System.String bIO_NET_AREA_MSR, global::System.String bIO_NET_AREA_MSR_UNIT, global::System.String bIO_NET_MESHSIZE_MSR,
            global::System.String bIO_MESHSIZE_MSR_UNIT, global::System.String bIO_BOAT_SPEED_MSR, global::System.String bIO_BOAT_SPEED_MSR_UNIT, global::System.String bIO_CURR_SPEED_MSR,
            global::System.String bIO_CURR_SPEED_MSR_UNIT, global::System.String bIO_TOXICITY_TEST_TYPE, int? sAMP_COLL_METHOD_IDX, global::System.String sAMP_COLL_EQUIP, global::System.String sAMP_COLL_EQUIP_COMMENT,
            int? sAMP_PREP_IDX, global::System.String sAMP_PREP_CONT_TYPE, global::System.String sAMP_PREP_CONT_COLOR, global::System.String sAMP_PREP_CHEM_PRESERV, global::System.String sAMP_PREP_THERM_PRESERV, 
            global::System.String sAMP_PREP_STORAGE_DESC, global::System.String wQX_SUBMIT_STATUS, Boolean? aCT_IND, Boolean? wQX_IND, String cREATE_USER = "system", string eNTRY_TYPE = "C")
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
                    //put in Timezone if missing
                    if (a.ACT_TIME_ZONE == null)
                        a.ACT_TIME_ZONE = Utils.GetWQXTimeZoneByDate(a.ACT_START_DT);

                    if (rELATIVE_DEPTH_NAME != null) a.RELATIVE_DEPTH_NAME = rELATIVE_DEPTH_NAME;
                    if (aCT_DEPTHHEIGHT_MSR != null) a.ACT_DEPTHHEIGHT_MSR = aCT_DEPTHHEIGHT_MSR;
                    if (aCT_DEPTHHEIGHT_MSR_UNIT != null) a.ACT_DEPTHHEIGHT_MSR_UNIT = aCT_DEPTHHEIGHT_MSR_UNIT;
                    if (tOP_DEPTHHEIGHT_MSR != null) a.TOP_DEPTHHEIGHT_MSR = tOP_DEPTHHEIGHT_MSR;
                    if (tOP_DEPTHHEIGHT_MSR_UNIT != null) a.TOP_DEPTHHEIGHT_MSR_UNIT = tOP_DEPTHHEIGHT_MSR_UNIT;
                    if (bOT_DEPTHHEIGHT_MSR != null) a.BOT_DEPTHHEIGHT_MSR = bOT_DEPTHHEIGHT_MSR;
                    if (bOT_DEPTHHEIGHT_MSR_UNIT != null) a.BOT_DEPTHHEIGHT_MSR_UNIT = bOT_DEPTHHEIGHT_MSR_UNIT;
                    if (dEPTH_REF_POINT != null) a.DEPTH_REF_POINT = dEPTH_REF_POINT;
                    if (aCT_COMMENT != null) a.ACT_COMMENT = aCT_COMMENT;
                    if (bIO_ASSEMBLAGE_SAMPLED != null) a.BIO_ASSEMBLAGE_SAMPLED = bIO_ASSEMBLAGE_SAMPLED;
                    if (bIO_DURATION_MSR != null) a.BIO_DURATION_MSR = bIO_DURATION_MSR;
                    if (bIO_DURATION_MSR_UNIT != null) a.BIO_DURATION_MSR_UNIT = bIO_DURATION_MSR_UNIT;
                    if (bIO_SAMP_COMPONENT != null) a.BIO_SAMP_COMPONENT = bIO_SAMP_COMPONENT;
                    if (bIO_SAMP_COMPONENT_SEQ != null) a.BIO_SAMP_COMPONENT_SEQ = bIO_SAMP_COMPONENT_SEQ;
                    if (bIO_REACH_LEN_MSR != null) a.BIO_REACH_LEN_MSR = bIO_REACH_LEN_MSR;
                    if (bIO_REACH_LEN_MSR_UNIT != null) a.BIO_REACH_LEN_MSR_UNIT = bIO_REACH_LEN_MSR_UNIT;
                    if (bIO_REACH_WID_MSR != null) a.BIO_REACH_WID_MSR = bIO_REACH_WID_MSR;
                    if (bIO_REACH_WID_MSR_UNIT != null) a.BIO_REACH_WID_MSR_UNIT = bIO_REACH_WID_MSR_UNIT;
                    if (bIO_PASS_COUNT != null) a.BIO_PASS_COUNT = bIO_PASS_COUNT;
                    if (bIO_NET_TYPE != null) a.BIO_NET_TYPE = bIO_NET_TYPE;
                    if (bIO_NET_AREA_MSR != null) a.BIO_NET_AREA_MSR = bIO_NET_AREA_MSR;
                    if (bIO_NET_AREA_MSR_UNIT != null) a.BIO_NET_AREA_MSR_UNIT = bIO_NET_AREA_MSR_UNIT;
                    if (bIO_NET_MESHSIZE_MSR != null) a.BIO_NET_MESHSIZE_MSR = bIO_NET_MESHSIZE_MSR;
                    if (bIO_MESHSIZE_MSR_UNIT != null) a.BIO_MESHSIZE_MSR_UNIT = bIO_MESHSIZE_MSR_UNIT;
                    if (bIO_BOAT_SPEED_MSR != null) a.BIO_BOAT_SPEED_MSR = bIO_BOAT_SPEED_MSR;
                    if (bIO_BOAT_SPEED_MSR_UNIT != null) a.BIO_BOAT_SPEED_MSR_UNIT = bIO_BOAT_SPEED_MSR_UNIT;
                    if (bIO_CURR_SPEED_MSR != null) a.BIO_CURR_SPEED_MSR = bIO_CURR_SPEED_MSR;
                    if (bIO_CURR_SPEED_MSR_UNIT != null) a.BIO_CURR_SPEED_MSR_UNIT = bIO_CURR_SPEED_MSR_UNIT;
                    if (bIO_TOXICITY_TEST_TYPE != null) a.BIO_TOXICITY_TEST_TYPE = bIO_TOXICITY_TEST_TYPE;
                    if (sAMP_COLL_METHOD_IDX != null) a.SAMP_COLL_METHOD_IDX = sAMP_COLL_METHOD_IDX;
                    if (sAMP_COLL_EQUIP != null) a.SAMP_COLL_EQUIP = sAMP_COLL_EQUIP;
                    if (sAMP_COLL_EQUIP_COMMENT != null) a.SAMP_COLL_EQUIP_COMMENT = sAMP_COLL_EQUIP_COMMENT;
                    if (sAMP_PREP_IDX != null) a.SAMP_PREP_IDX = sAMP_PREP_IDX;
                    if (sAMP_PREP_CONT_TYPE != null) a.SAMP_PREP_CONT_TYPE = sAMP_PREP_CONT_TYPE;
                    if (sAMP_PREP_CONT_COLOR != null) a.SAMP_PREP_CONT_COLOR = sAMP_PREP_CONT_COLOR;
                    if (sAMP_PREP_CHEM_PRESERV != null) a.SAMP_PREP_CHEM_PRESERV = sAMP_PREP_CHEM_PRESERV;
                    if (sAMP_PREP_THERM_PRESERV != null) a.SAMP_PREP_THERM_PRESERV = sAMP_PREP_THERM_PRESERV;
                    if (sAMP_PREP_STORAGE_DESC != null) a.SAMP_PREP_STORAGE_DESC = sAMP_PREP_STORAGE_DESC;                    
                    if (wQX_SUBMIT_STATUS != null) a.WQX_SUBMIT_STATUS = wQX_SUBMIT_STATUS;
                    if (aCT_IND != null) a.ACT_IND = aCT_IND;
                    if (wQX_IND != null) a.WQX_IND = wQX_IND;
                    if (eNTRY_TYPE != null) a.ENTRY_TYPE = eNTRY_TYPE;

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

        public static int UpdateWQX_ACTIVITY_WQXStatus(global::System.Int32? aCTIVITY_IDX, global::System.String wQX_SUBMIT_STATUS, Boolean? aCT_IND, Boolean? wQX_IND, String cREATE_USER = "system")
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    T_WQX_ACTIVITY a = (from c in ctx.T_WQX_ACTIVITY
                                        where c.ACTIVITY_IDX == aCTIVITY_IDX
                                        select c).FirstOrDefault();

                    if (wQX_SUBMIT_STATUS != null) a.WQX_SUBMIT_STATUS = wQX_SUBMIT_STATUS;
                    if (aCT_IND != null) a.ACT_IND = aCT_IND;
                    if (wQX_IND != null) a.WQX_IND = wQX_IND;
                    a.UPDATE_USERID = cREATE_USER.ToUpper();
                    a.UPDATE_DT = System.DateTime.Now;

                    ctx.SaveChanges();

                    return a.ACTIVITY_IDX;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }

        public static int UpdateWQX_ACTIVITY_EntryType(global::System.Int32? aCTIVITY_IDX, string eNTRY_TYPE)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    T_WQX_ACTIVITY a = (from c in ctx.T_WQX_ACTIVITY
                                        where c.ACTIVITY_IDX == aCTIVITY_IDX
                                        select c).FirstOrDefault();

                    if (a != null)
                        if (eNTRY_TYPE != null) a.ENTRY_TYPE = eNTRY_TYPE;

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

        public static List<ActivityListDisplay> GetWQX_ACTIVITYDisplay(bool ActInd, string OrgID, int? MonLocIDX, DateTime? startDt, DateTime? endDt, string ActType, bool WQXPending, int? ProjectIDX, string WQXStatus)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    var query = (from a in ctx.T_WQX_ACTIVITY
                            join p in ctx.T_WQX_PROJECT on a.PROJECT_IDX equals p.PROJECT_IDX
                            join m in ctx.T_WQX_MONLOC on a.MONLOC_IDX equals m.MONLOC_IDX
                            where (ActInd ? a.ACT_IND == true : true)
                            && a.ORG_ID == OrgID
                            && (WQXPending ? a.WQX_SUBMIT_STATUS == "U" : true)
                            && (WQXPending ? a.WQX_IND == true : true)
                            && (MonLocIDX == null ? true : a.MONLOC_IDX == MonLocIDX)
                            && (startDt == null ? true : a.ACT_START_DT >= startDt)
                            && (endDt == null ? true : a.ACT_START_DT <= endDt)
                            && (ActType == null ? true : a.ACT_TYPE == ActType)
                            && (ProjectIDX == null ? true : a.PROJECT_IDX == ProjectIDX)
                            && (WQXStatus == "" ? true : (a.WQX_IND==true && a.WQX_SUBMIT_STATUS == WQXStatus))
                            orderby a.ACT_START_DT descending, a.ACTIVITY_IDX descending
                            select new ActivityListDisplay {
                                ACTIVITY_IDX = a.ACTIVITY_IDX,
                                ORG_ID = a.ORG_ID,
                                PROJECT_ID = p.PROJECT_ID,
                                MONLOC_ID = m.MONLOC_ID,
                                ACTIVITY_ID = a.ACTIVITY_ID,
                                ACT_TYPE = a.ACT_TYPE,
                                ACT_MEDIA = a.ACT_MEDIA,
                                ACT_SUBMEDIA = a.ACT_SUBMEDIA,
                                ACT_START_DT = a.ACT_START_DT,
                                ACT_END_DT = a.ACT_END_DT,
                                ACT_DEPTHHEIGHT_MSR = a.ACT_DEPTHHEIGHT_MSR,
                                ACT_DEPTHHEIGHT_MSR_UNIT = a.ACT_DEPTHHEIGHT_MSR_UNIT,
                                TOP_DEPTHHEIGHT_MSR = a.TOP_DEPTHHEIGHT_MSR,
                                BOT_DEPTHHEIGHT_MSR = a.BOT_DEPTHHEIGHT_MSR,
                                DEPTH_REF_POINT = a.DEPTH_REF_POINT,
                                ACT_COMMENT = a.ACT_COMMENT,
                                SAMP_COLL_METHOD = null,
                                SAMP_COLL_EQUIP = a.SAMP_COLL_EQUIP,
                                SAMP_COLL_EQUIP_COMMENT = a.SAMP_COLL_EQUIP_COMMENT,
                                SAMP_PREP_METHOD = null,
                                WQX_IND = a.WQX_IND,
                                WQX_SUBMIT_STATUS = a.WQX_SUBMIT_STATUS,
                                ACT_IND = a.ACT_IND
                            }).ToList();

                    return query;
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
                            && a.ACT_IND == true
                            select a).Count();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static int GetWQX_ACTIVITYByProjectID(int ProjectIDX)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_WQX_ACTIVITY
                            where a.PROJECT_IDX == ProjectIDX
                            && a.ACT_IND == true
                            select a).Count();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static int DeleteT_WQX_ACTIVITY(int ActivityIDX, string UserID)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    T_WQX_ACTIVITY a = db_WQX.GetWQX_ACTIVITY_ByID(ActivityIDX);
                    if (a != null)
                    {
                        if (a.ACT_IND == false && (a.WQX_IND == false || a.WQX_SUBMIT_STATUS != "U"))
                        {
                            //only actually delete record from database if it has already been set to inactive and WQX status is not pending ("U")
                            string sql = "DELETE FROM T_WQX_ACTIVITY WHERE ACTIVITY_IDX = " + ActivityIDX;
                            ctx.ExecuteStoreCommand(sql);
                            return 1;
                        }
                        else
                        {
                            //mark as inactive (deleted), which will send the delete request to EPA-WQX
                            UpdateWQX_ACTIVITY_WQXStatus(ActivityIDX, "U", false, null, UserID);
                            return 1;
                        }

                    }
                    else
                        return 0;



                }
                catch
                {
                    return 0;
                }
            }

        }

        public static int GetWQX_ACTIVITY_MyOrgCount(int UserIDX)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_WQX_ACTIVITY
                            join b in ctx.T_WQX_USER_ORGS on a.ORG_ID equals b.ORG_ID
                            where b.USER_IDX == UserIDX
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
                            .Include("T_WQX_REF_ANAL_METHOD")
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

        public static T_WQX_RESULT GetT_WQX_RESULT_ByIDX(int ResultIDX)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_WQX_RESULT
                            where a.RESULT_IDX == ResultIDX
                            select a).FirstOrDefault();
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

        public static int InsertOrUpdateT_WQX_RESULT(global::System.Int32? rESULT_IDX, global::System.Int32 aCTIVITY_IDX, global::System.String rESULT_DETECT_CONDITION,            
            global::System.String cHAR_NAME, global::System.String rESULT_SAMP_FRACTION, global::System.String rESULT_MSR, global::System.String rESULT_MSR_UNIT,
            global::System.String rESULT_STATUS, global::System.String rESULT_VALUE_TYPE, global::System.String rESULT_COMMENT, 
            global::System.String bIO_INTENT_NAME, global::System.String bIO_INDIVIDUAL_ID, global::System.String bIO_TAXONOMY, global::System.String bIO_SAMPLE_TISSUE_ANATOMY,
            global::System.Int32? aNALYTIC_METHOD_IDX, int? lAB_IDX, DateTime? lAB_ANALYSIS_START_DT, global::System.String dETECTION_LIMIT, global::System.String pQL,
            global::System.String lOWER_QUANT_LIMIT, global::System.String uPPER_QUANT_LIMIT, int? lAB_SAMP_PREP_IDX, DateTime? lAB_SAMP_PREP_START_DT, string dILUTION_FACTOR, 
            string fREQ_CLASS_CODE, string fREQ_CLASS_UNIT,
            String cREATE_USER = "system")
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

                    if (a == null)
                        a = new T_WQX_RESULT();

                    if (a.RESULT_IDX == 0) //insert case
                        insInd = true;

                    a.ACTIVITY_IDX = aCTIVITY_IDX;

                    if (rESULT_DETECT_CONDITION != null) a.RESULT_DETECT_CONDITION = rESULT_DETECT_CONDITION;
                    if (cHAR_NAME != null) a.CHAR_NAME = cHAR_NAME;
                    
                    if (rESULT_SAMP_FRACTION != null) a.RESULT_SAMP_FRACTION = rESULT_SAMP_FRACTION;
                    if (rESULT_MSR != null) a.RESULT_MSR = rESULT_MSR;
                    if (rESULT_MSR_UNIT != null) a.RESULT_MSR_UNIT = rESULT_MSR_UNIT;
                    if (rESULT_STATUS != null) a.RESULT_STATUS = rESULT_STATUS;
                    if (rESULT_VALUE_TYPE != null) a.RESULT_VALUE_TYPE = rESULT_VALUE_TYPE;
                    if (rESULT_COMMENT != null) a.RESULT_COMMENT = rESULT_COMMENT;
                    if (bIO_INTENT_NAME != null) a.BIO_INTENT_NAME = bIO_INTENT_NAME;
                    if (bIO_INDIVIDUAL_ID != null) a.BIO_INDIVIDUAL_ID = bIO_INDIVIDUAL_ID;
                    if (bIO_TAXONOMY != null) a.BIO_SUBJECT_TAXONOMY = bIO_TAXONOMY;
                    if (bIO_SAMPLE_TISSUE_ANATOMY != null) a.BIO_SAMPLE_TISSUE_ANATOMY = bIO_SAMPLE_TISSUE_ANATOMY;
                    if (aNALYTIC_METHOD_IDX != null) a.ANALYTIC_METHOD_IDX = aNALYTIC_METHOD_IDX;
                    if (lAB_IDX != null) a.LAB_IDX = lAB_IDX;
                    if (lAB_ANALYSIS_START_DT != null) a.LAB_ANALYSIS_START_DT = lAB_ANALYSIS_START_DT;
                    if (dETECTION_LIMIT != null) a.DETECTION_LIMIT = dETECTION_LIMIT;
                    if (pQL != null) a.PQL = pQL;
                    if (lOWER_QUANT_LIMIT != null) a.LOWER_QUANT_LIMIT = lOWER_QUANT_LIMIT;
                    if (uPPER_QUANT_LIMIT != null) a.UPPER_QUANT_LIMIT = uPPER_QUANT_LIMIT;
                    if (lAB_SAMP_PREP_IDX != null) a.LAB_SAMP_PREP_IDX = lAB_SAMP_PREP_IDX;
                    if (lAB_SAMP_PREP_START_DT != null) a.LAB_SAMP_PREP_START_DT = lAB_SAMP_PREP_START_DT;
                    if (dILUTION_FACTOR != null) a.DILUTION_FACTOR = dILUTION_FACTOR;
                    if (fREQ_CLASS_CODE != null) a.FREQ_CLASS_CODE = fREQ_CLASS_CODE;
                    if (fREQ_CLASS_UNIT != null) a.FREQ_CLASS_UNIT = fREQ_CLASS_UNIT;
                    //set freq class unit to count if not provided
                    if (fREQ_CLASS_UNIT == null && fREQ_CLASS_CODE != null) fREQ_CLASS_UNIT = "count";

                    if (insInd) //insert case
                        ctx.AddToT_WQX_RESULT(a);

                    ctx.SaveChanges();

                    return a.RESULT_IDX;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }

        public static List<CharDisplay> GetT_WQX_RESULT_SampledCharacteristics(string OrgID)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from r in ctx.T_WQX_RESULT
                            join a in ctx.T_WQX_ACTIVITY on r.ACTIVITY_IDX equals a.ACTIVITY_IDX
                            where a.ORG_ID == OrgID
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

        public static List<string> GetWQX_ORGANIZATION_PendingDataToSubmit()
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    var x = (from m in ctx.T_WQX_MONLOC
                             join o in ctx.T_WQX_ORGANIZATION on m.ORG_ID equals o.ORG_ID
                             where o.CDX_SUBMIT_IND == true
                             && m.WQX_SUBMIT_STATUS == "U"
                             && m.WQX_IND == true
                             select m.ORG_ID).Union
                             (from a in ctx.T_WQX_ACTIVITY
                              join o in ctx.T_WQX_ORGANIZATION on a.ORG_ID equals o.ORG_ID
                              where o.CDX_SUBMIT_IND == true
                              && a.WQX_SUBMIT_STATUS == "U"
                              && a.WQX_IND == true
                              select a.ORG_ID).Union
                              (from p in ctx.T_WQX_PROJECT
                               join o in ctx.T_WQX_ORGANIZATION on p.ORG_ID equals o.ORG_ID
                               where o.CDX_SUBMIT_IND == true
                               && p.WQX_SUBMIT_STATUS == "U"
                               && p.WQX_IND == true
                               select p.ORG_ID);

                    return x.Distinct().ToList();                               
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    
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

        public static int InsertOrUpdateT_WQX_ORGANIZATION(string oRG_ID, string oRG_NAME, string oRG_DESC, string tRIBAL_CODE, string eLECTRONIC_ADDRESS, 
            string eLECTRONICADDRESSTYPE, string tELEPHONE_NUM, string tELEPHONE_NUM_TYPE, string TELEPHONE_EXT, string cDX_SUBMITTER_ID, 
            string cDX_SUBMITTER_PWD, bool? cDX_SUBMIT_IND, string dEFAULT_TIMEZONE, string cREATE_USER = "system", string mAIL_ADDRESS = null, 
            string mAIL_ADD_CITY = null, string mAIL_ADD_STATE = null, string mAIL_ADD_ZIP = null)
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
                    if (dEFAULT_TIMEZONE != null) a.DEFAULT_TIMEZONE = dEFAULT_TIMEZONE;
                    if (cDX_SUBMITTER_ID != null) a.CDX_SUBMITTER_ID = cDX_SUBMITTER_ID;
                    if (cDX_SUBMIT_IND != null) a.CDX_SUBMIT_IND = cDX_SUBMIT_IND;
                    if (cDX_SUBMITTER_PWD != null && cDX_SUBMITTER_PWD != "--------")
                    {
                        //encrypt CDX submitter password for increased security
                        string encryptOauth = new SimpleAES().Encrypt(cDX_SUBMITTER_PWD);
                        encryptOauth = System.Web.HttpUtility.UrlEncode(encryptOauth);
                        a.CDX_SUBMITTER_PWD_HASH = encryptOauth;
                    }
                    if (dEFAULT_TIMEZONE != null) a.DEFAULT_TIMEZONE = dEFAULT_TIMEZONE;
                    if (mAIL_ADDRESS != null) a.MAILING_ADDRESS = mAIL_ADDRESS;
                    if (mAIL_ADD_CITY != null) a.MAILING_ADD_CITY = mAIL_ADD_CITY;
                    if (mAIL_ADD_STATE != null) a.MAILING_ADD_STATE = mAIL_ADD_STATE;
                    if (mAIL_ADD_ZIP != null) a.MAILING_ADD_ZIP = mAIL_ADD_ZIP;

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


        // ***************************** T_EPA_ORGS ******************************
        // *********************************************************************
        public static int DeleteT_EPA_ORGS()
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    string sql = "DELETE FROM T_EPA_ORGS";
                    ctx.ExecuteStoreCommand(sql);
                    return 1;
                }
                catch
                {
                    return 0;
                }
            }
        }

        public static int InsertOrUpdateT_EPA_ORGS(global::System.String oRG_ID, global::System.String oRG_NAME)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    T_EPA_ORGS a = new T_EPA_ORGS();
                    a.ORG_ID = oRG_ID;
                    if (oRG_NAME != null) a.ORG_FORMAL_NAME = oRG_NAME;
                    a.UPDATE_DT = System.DateTime.Now;
                    ctx.AddToT_EPA_ORGS(a);
                    ctx.SaveChanges();

                    return 1;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }

        public static T_EPA_ORGS GetT_EPA_ORGS_ByOrgID(string OrgID)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_EPA_ORGS
                            where a.ORG_ID == OrgID
                            select a).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static DateTime? GetT_EPA_ORGS_LastUpdateDate()
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_EPA_ORGS
                            select a.UPDATE_DT).Max();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }



        // *************************** V_WQX_ALL_ORGS   ************************
        // *********************************************************************
        public static List<V_WQX_ALL_ORGS> GetV_WQX_ALL_ORGS()
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.V_WQX_ALL_ORGS
                            orderby a.ORG_FORMAL_NAME
                            select a).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        // *************************** V_WQX_PENDING_RECORDS   ************************
        // *********************************************************************
        public static List<V_WQX_PENDING_RECORDS> GetV_WQX_PENDING_RECORDS(string OrgID, DateTime? startDate, DateTime? endDate)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.V_WQX_PENDING_RECORDS
                            where (OrgID != null ? a.ORG_ID == OrgID : true)
                            && (startDate != null ? a.UPDATE_DT >= startDate : true)
                            && (endDate != null ? a.UPDATE_DT <= endDate : true)
                            orderby a.TABLE_CD, a.UPDATE_DT
                            select a).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }



        // *************************** USER ORGANIZATION************************
        // *********************************************************************
        public static List<T_WQX_ORGANIZATION> GetWQX_USER_ORGS_ByUserIDX(int UserIDX, bool excludePendingInd)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_WQX_USER_ORGS
                            join b in ctx.T_WQX_ORGANIZATION on a.ORG_ID equals b.ORG_ID
                            where a.USER_IDX == UserIDX
                            && (excludePendingInd == true ? a.ROLE_CD != "P" : true)
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

        public static int ApproveRejectT_WQX_USER_ORGS(global::System.String oRG_ID, global::System.Int32 uSER_IDX, string ApproveRejectCode)
        {
            //ApproveRejectCode = U (for user approve) A (for Admin approve) or R (for reject)
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    if (ApproveRejectCode == "R")
                    {
                        DeleteT_WQX_USER_ORGS(oRG_ID, uSER_IDX);
                        return -1;
                    }
                    else
                    {
                        T_WQX_USER_ORGS a = (from c in ctx.T_WQX_USER_ORGS
                                             where c.USER_IDX == uSER_IDX
                                             && c.ORG_ID == oRG_ID
                                             select c).FirstOrDefault();

                        if (a == null)
                            return 0;

                        a.ROLE_CD = ApproveRejectCode;
                        ctx.SaveChanges();

                        return 1;
                    }
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }

        }

        public static List<UserOrgDisplay> GetT_OE_USERSInOrganization(string OrgID)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from u in ctx.T_OE_USERS
                            join uo in ctx.T_WQX_USER_ORGS on u.USER_IDX equals uo.USER_IDX
                            where uo.ORG_ID == OrgID
                            //orderby u.USER_ID
                            select new UserOrgDisplay
                            {
                                USER_IDX = u.USER_IDX,
                                USER_ID = u.USER_ID,
                                USER_NAME = u.FNAME + " " + u.LNAME,
                                ORG_ID = uo.ORG_ID,
                                ROLE_CD = uo.ROLE_CD
                            }).ToList();
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

        public static List<UserOrgDisplay> GetT_OE_USERSPending(string OrgID)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from u in ctx.T_OE_USERS
                            join uo in ctx.T_WQX_USER_ORGS on u.USER_IDX equals uo.USER_IDX
                            where uo.ROLE_CD == "P"
                            && (OrgID == null ? true : uo.ORG_ID == OrgID)
                            select new UserOrgDisplay { 
                                USER_IDX = u.USER_IDX,
                                USER_ID = u.USER_ID,
                                USER_NAME = u.FNAME + " " + u.LNAME,
                                ORG_ID = uo.ORG_ID
                            }).ToList();
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
                            && a.ROLE_CD != "P"
                            select b).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static T_WQX_USER_ORGS GetWQX_USER_ORGS_ByUserIDX_OrgID(int UserIDX, string OrgID)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_WQX_USER_ORGS
                            where a.USER_IDX == UserIDX
                            && a.ORG_ID == OrgID
                            select a).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static bool CanUserEditOrg(int UserIDX, string OrgID)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    var xxx = (from a in ctx.T_WQX_USER_ORGS
                            where a.USER_IDX == UserIDX
                            && a.ORG_ID == OrgID
                            && (a.ROLE_CD == "A" || a.ROLE_CD == "U")
                            select a).Count();

                    return xxx > 0;

                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public static bool CanUserAdminOrgs(int UserIDX)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    var xxx = (from a in ctx.T_WQX_USER_ORGS
                               where a.USER_IDX == UserIDX
                               && (a.ROLE_CD == "A")
                               select a).Count();

                    return xxx > 0;

                }
                catch (Exception ex)
                {
                    return false;
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
            string cHAR_NAME, string cHAR_DEFAULT_UNIT, String cREATE_USER = "system", string cHAR_DEFAULT_SAMP_FRACTION = null)
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
                    if (cHAR_DEFAULT_SAMP_FRACTION != null) a.CHAR_DEFAULT_SAMP_FRACTION = cHAR_DEFAULT_SAMP_FRACTION;

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


        // *************************** IMPORT_TRANSLATE   ******************************
        // *****************************************************************************
        public static Dictionary<string, string> GetWQX_IMPORT_TRANSLATE_byColName(string OrgID, string ColName)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    var translators = (from a in ctx.T_WQX_IMPORT_TRANSLATE
                            where a.COL_NAME == ColName
                            && a.ORG_ID == OrgID
                            select a).ToList();

                    var xxx = translators.ToDictionary(DataFrom => DataFrom.DATA_FROM, DataTo => DataTo.DATA_TO);
                    return xxx;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static List<string> GetWQX_IMPORT_TRANSLATE_byColName(string OrgID)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_WQX_IMPORT_TRANSLATE
                                       where a.ORG_ID == OrgID
                                       select a.COL_NAME).Distinct().ToList();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static string GetWQX_IMPORT_TRANSLATE_byColNameAndValue(string OrgID, string ColName, string Value)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    var xxx = (from a in ctx.T_WQX_IMPORT_TRANSLATE
                            where a.ORG_ID == OrgID
                            && a.COL_NAME == ColName
                            && a.DATA_FROM == Value
                            select a).FirstOrDefault();

                    return xxx != null ? xxx.DATA_TO : Value;

                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static List<T_WQX_IMPORT_TRANSLATE> GetWQX_IMPORT_TRANSLATE_byOrg(string OrgID)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_WQX_IMPORT_TRANSLATE
                            where a.ORG_ID == OrgID
                            orderby a.COL_NAME, a.DATA_FROM
                            select a).ToList();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static int DeleteT_WQX_IMPORT_TRANSLATE(int TranslateID)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    string sql = "DELETE FROM T_WQX_IMPORT_TRANSLATE WHERE TRANSLATE_IDX = " + TranslateID;
                    ctx.ExecuteStoreCommand(sql);
                    return 1;
                }
                catch
                {
                    return 0;
                }
            }

        }

        public static int InsertOrUpdateWQX_IMPORT_TRANSLATE(int? tRANSLATE_IDX, string oRG_ID, string cOL_NAME, string dATA_FROM, string dATA_TO, string cREATE_USER = "system")
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                Boolean insInd = false;
                try
                {
                    T_WQX_IMPORT_TRANSLATE a = null;

                    if (tRANSLATE_IDX != null)
                        a = (from c in ctx.T_WQX_IMPORT_TRANSLATE
                             where c.TRANSLATE_IDX == tRANSLATE_IDX
                             select c).FirstOrDefault();

                    if (a == null) //insert case
                    {
                        insInd = true;
                        a = new T_WQX_IMPORT_TRANSLATE();
                    }

                    if (oRG_ID != null) a.ORG_ID = oRG_ID;
                    if (cOL_NAME != null) a.COL_NAME = cOL_NAME;
                    if (dATA_FROM != null) a.DATA_FROM = dATA_FROM;
                    if (dATA_TO != null) a.DATA_TO = dATA_TO;

                    if (insInd) //insert case
                    {
                        a.CREATE_DT = DateTime.Now;
                        a.CREATE_USERID = cREATE_USER;
                        ctx.AddToT_WQX_IMPORT_TRANSLATE(a);
                    }

                    ctx.SaveChanges();

                    return a.TRANSLATE_IDX;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }




        // *************************** IMPORT_COL_ALIAS    ******************************
        // *****************************************************************************
        public static List<string> GetWQX_IMPORT_COL_ALIAS_byField(string ColName)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    var x = (from a in ctx.T_WQX_IMPORT_COL_ALIAS
                            where a.COL_NAME == ColName
                            select a.ALIAS_NAME.ToUpper()).ToList();
                    return x;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        


        // *************************** IMPORT: MONLOC    ******************************
        // *****************************************************************************

        public static int InsertOrUpdateWQX_IMPORT_TEMP_MONLOC(int? tEMP_MONLOC_IDX, string uSER_ID, int? mONLOC_IDX, string oRG_ID, string mONLOC_ID, 
            string mONLOC_NAME, string mONLOC_TYPE, string mONLOC_DESC, string hUC_EIGHT, string HUC_TWELVE, string tRIBAL_LAND_IND, string tRIBAL_LAND_NAME, string lATITUDE_MSR, 
            string lONGITUDE_MSR, int? sOURCE_MAP_SCALE, string hORIZ_ACCURACY, string hORIZ_ACCURACY_UNIT, string hORIZ_COLL_METHOD, string hORIZ_REF_DATUM, string vERT_MEASURE, 
            string vERT_MEASURE_UNIT, string vERT_COLL_METHOD, string vERT_REF_DATUM, string cOUNTRY_CODE, string sTATE_CODE, string cOUNTY_CODE, string wELL_TYPE, 
            string aQUIFER_NAME, string fORMATION_TYPE, string wELLHOLE_DEPTH_MSR, string wELLHOLE_DEPTH_MSR_UNIT, string sTATUS_CD, string sTATUS_DESC)
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

                    if (uSER_ID != null)
                    {
                        a.USER_ID = uSER_ID;
                        if (uSER_ID.Length > 25) { sTATUS_CD = "F"; sTATUS_DESC += "User ID length exceeded. "; }
                    }

                    if (mONLOC_IDX != null) a.MONLOC_IDX = mONLOC_IDX;
                    if (oRG_ID != null) a.ORG_ID = oRG_ID;

                    if (mONLOC_ID != null)
                    {
                        a.MONLOC_ID = mONLOC_ID.SubStringPlus(0, 35).Trim();
                        if (mONLOC_ID.Length > 35) { sTATUS_CD = "F"; sTATUS_DESC += "Monitoring Location ID length exceeded. "; }

                        T_WQX_MONLOC mtemp = db_WQX.GetWQX_MONLOC_ByIDString(oRG_ID, mONLOC_ID);
                        if (mtemp != null) { sTATUS_CD = "F"; sTATUS_DESC += "Monitoring Location ID already exists. "; }
                    }

                    if (!string.IsNullOrEmpty(mONLOC_NAME))
                    {
                        a.MONLOC_NAME = mONLOC_NAME.SubStringPlus(0, 255).Trim();
                        if (mONLOC_NAME.Length > 255) { sTATUS_CD = "F"; sTATUS_DESC += "Monitoring Location Name length exceeded. "; }
                    }

                    if (!string.IsNullOrEmpty(mONLOC_TYPE))
                    {
                        a.MONLOC_TYPE = mONLOC_TYPE.SubStringPlus(0, 45);
                        if (db_Ref.GetT_WQX_REF_DATA_ByKey("MonitoringLocationType", mONLOC_TYPE.Trim()) == false) { sTATUS_CD = "F"; sTATUS_DESC += "Monitoring Location Type not valid. "; }
                    }

                    if (!string.IsNullOrEmpty(mONLOC_DESC))
                    {
                        a.MONLOC_DESC = mONLOC_DESC.SubStringPlus(0, 1999);
                        if (mONLOC_DESC.Length > 1999) { sTATUS_CD = "F"; sTATUS_DESC += "Monitoring Location Description length exceeded. "; }
                    }

                    if (!string.IsNullOrEmpty(hUC_EIGHT))
                    {
                        a.HUC_EIGHT = hUC_EIGHT.Trim().SubStringPlus(0, 8);
                        if (hUC_EIGHT.Length > 8) { sTATUS_CD = "F"; sTATUS_DESC += "HUC8 length exceeded. "; }
                    }

                    if (!string.IsNullOrEmpty(HUC_TWELVE))
                    {
                        a.HUC_TWELVE = HUC_TWELVE.Trim().SubStringPlus(0, 12);
                        if (HUC_TWELVE.Length > 12) { sTATUS_CD = "F"; sTATUS_DESC += "HUC12 length exceeded. "; }
                    }

                    if (!string.IsNullOrEmpty(tRIBAL_LAND_IND))
                    {
                        if (tRIBAL_LAND_IND.ToUpper() == "TRUE") tRIBAL_LAND_IND = "Y";
                        if (tRIBAL_LAND_IND.ToUpper() == "FALSE") tRIBAL_LAND_IND = "N";

                        a.TRIBAL_LAND_IND = tRIBAL_LAND_IND.SubStringPlus(0, 1);
                        if (tRIBAL_LAND_IND.Length > 1) { sTATUS_CD = "F"; sTATUS_DESC += "Tribal Land Indicator length exceeded. "; }
                    }

                    if (!string.IsNullOrEmpty(tRIBAL_LAND_NAME))
                    {
                        a.TRIBAL_LAND_NAME = tRIBAL_LAND_NAME.SubStringPlus(0, 200);
                        if (tRIBAL_LAND_NAME.Length > 200) { sTATUS_CD = "F"; sTATUS_DESC += "Tribal Land Name length exceeded. "; }
                    }

                    if (!string.IsNullOrEmpty(lATITUDE_MSR))
                    {
                        a.LATITUDE_MSR = lATITUDE_MSR.SubStringPlus(0, 30);
                        decimal iii = 0;
                        if (Decimal.TryParse(lATITUDE_MSR, out iii) == false) { sTATUS_CD = "F"; sTATUS_DESC += "Latitude is not decimal format. "; }
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
                        a.VERT_MEASURE = vERT_MEASURE.Trim().SubStringPlus(0, 12);
                        if (vERT_MEASURE.Length > 12) { sTATUS_CD = "F"; sTATUS_DESC += "Vertical Measure length exceeded. "; }
                    }

                    if (!string.IsNullOrEmpty(vERT_MEASURE_UNIT))
                    {
                        a.VERT_MEASURE_UNIT = vERT_MEASURE_UNIT.Trim().SubStringPlus(0, 12);
                        if (db_Ref.GetT_WQX_REF_DATA_ByKey("MeasureUnit", vERT_MEASURE_UNIT.Trim()) == false) { sTATUS_CD = "F"; sTATUS_DESC += "Vertical Measure Unit not valid. "; }
                    }

                    if (!string.IsNullOrEmpty(vERT_COLL_METHOD))
                    {
                        a.VERT_COLL_METHOD = vERT_COLL_METHOD.Trim().SubStringPlus(0, 50);
                        if (db_Ref.GetT_WQX_REF_DATA_ByKey("VerticalCollectionMethod", vERT_COLL_METHOD.Trim()) == false) { sTATUS_CD = "F"; sTATUS_DESC += "Vertical Collection Method not acceptable. "; }
                    }

                    if (!string.IsNullOrEmpty(vERT_REF_DATUM))
                    {
                        a.VERT_REF_DATUM = vERT_REF_DATUM.Trim().SubStringPlus(0, 6);
                        if (db_Ref.GetT_WQX_REF_DATA_ByKey("VerticalCoordinateReferenceSystemDatum", vERT_REF_DATUM.Trim()) == false) { sTATUS_CD = "F"; sTATUS_DESC += "Vertical Collection Datum not acceptable. "; }
                    }

                    if (!string.IsNullOrEmpty(cOUNTRY_CODE))
                    {
                        //if there is a match of country NAME value to reference data text (in case user is importing country name instead of code)
                        T_WQX_REF_DATA rd = db_Ref.GetT_WQX_REF_DATA_ByTextGetRow("Country", cOUNTRY_CODE);
                        if (rd != null)
                            a.COUNTRY_CODE = rd.VALUE.SubStringPlus(0, 2);
                        else
                        {
                            a.COUNTRY_CODE = cOUNTRY_CODE.SubStringPlus(0, 2);
                            if (cOUNTRY_CODE.Length > 2) { sTATUS_CD = "F"; sTATUS_DESC += "Country Code length exceeded. "; }
                        }
                    }

                    if (!string.IsNullOrEmpty(sTATE_CODE))
                    {
                        //if there is a match of state value to reference data text (in case user is importing state name instead of code)
                        T_WQX_REF_DATA rd = db_Ref.GetT_WQX_REF_DATA_ByTextGetRow("State", sTATE_CODE);
                        if (rd != null)
                            a.STATE_CODE = rd.VALUE;
                        else
                        {
                            a.STATE_CODE = sTATE_CODE.SubStringPlus(0, 2);
                            if (sTATE_CODE.Length > 2) { sTATUS_CD = "F"; sTATUS_DESC += "State Code length exceeded. "; }
                        }
                    }

                    if (!string.IsNullOrEmpty(cOUNTY_CODE))
                    {
                        //if there is a match of county value to reference data text (in case user is importing county text instead of code)
                        T_WQX_REF_COUNTY c = db_Ref.GetT_WQX_REF_COUNTY_ByCountyNameAndState(sTATE_CODE, cOUNTY_CODE);
                        if (c != null)
                            a.COUNTY_CODE = c.COUNTY_CODE;
                        else
                        {
                            a.COUNTY_CODE = cOUNTY_CODE.SubStringPlus(0, 3);
                            if (cOUNTY_CODE.Length > 3) { sTATUS_CD = "F"; sTATUS_DESC += "County Code length exceeded. "; }
                        }
                    }

                    if (!string.IsNullOrEmpty(wELL_TYPE))
                    {
                        a.WELL_TYPE = wELL_TYPE.Trim().SubStringPlus(0, 255);
                    }

                    if (!string.IsNullOrEmpty(aQUIFER_NAME))
                    {
                        a.AQUIFER_NAME = aQUIFER_NAME.Trim().SubStringPlus(0, 120);
                    }

                    if (!string.IsNullOrEmpty(fORMATION_TYPE))
                    {
                        a.FORMATION_TYPE = fORMATION_TYPE.Trim().SubStringPlus(0, 50);
                    }

                    if (!string.IsNullOrEmpty(wELLHOLE_DEPTH_MSR))
                    {
                        a.WELLHOLE_DEPTH_MSR = wELLHOLE_DEPTH_MSR.Trim().SubStringPlus(0, 12);
                    }

                    if (!string.IsNullOrEmpty(wELLHOLE_DEPTH_MSR_UNIT))
                    {
                        a.WELLHOLE_DEPTH_MSR_UNIT = wELLHOLE_DEPTH_MSR_UNIT.Trim().SubStringPlus(0, 12);
                    }

                    if (sTATUS_CD != null) a.IMPORT_STATUS_CD = sTATUS_CD;
                    if (sTATUS_DESC != null) a.IMPORT_STATUS_DESC = sTATUS_DESC.SubStringPlus(0, 100);

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

        public static int InsertWQX_IMPORT_TEMP_MONLOC_New(string uSER_ID, string oRG_ID, Dictionary<string, string> colVals)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    //get import config rules
                    List<ConfigInfoType> _allRules = Utils.GetAllColumnInfo("M");

                    T_WQX_IMPORT_TEMP_MONLOC a = new T_WQX_IMPORT_TEMP_MONLOC();

                    a.IMPORT_STATUS_CD = "P";
                    a.IMPORT_STATUS_DESC = "";

                    if (!string.IsNullOrEmpty(uSER_ID)) a.USER_ID = uSER_ID;
                    if (!string.IsNullOrEmpty(oRG_ID)) a.ORG_ID = oRG_ID;

                    //*************** PRE CUSTOM VALIDATION **********************************************
                    string _t = null;

                    _t = Utils.GetValueOrDefault(colVals, "TRIBAL_LAND_IND");
                    if (!string.IsNullOrEmpty(_t))
                    {
                        if (_t.ToUpper() == "TRUE") colVals["TRIBAL_LAND_IND"] = "Y";
                        if (_t.ToUpper() == "FALSE") colVals["TRIBAL_LAND_IND "] = "N";
                    }

                    //if there is a match of county value to reference data text (in case user is importing county text instead of code)
                    _t = Utils.GetValueOrDefault(colVals, "COUNTY_CODE");
                    if (!string.IsNullOrEmpty(_t))
                    {
                        T_WQX_REF_COUNTY c = db_Ref.GetT_WQX_REF_COUNTY_ByCountyNameAndState(Utils.GetValueOrDefault(colVals, "STATE_CODE"), _t);
                        if (c != null)
                            a.COUNTY_CODE = c.COUNTY_CODE;
                        else
                            WQX_IMPORT_TEMP_MONLOC_GenVal(ref a, _allRules, colVals, "COUNTY_CODE");
                    }
                    //********************** end custom validation ********************************************

                    WQX_IMPORT_TEMP_MONLOC_GenVal(ref a, _allRules, colVals, "MONLOC_ID");
                    WQX_IMPORT_TEMP_MONLOC_GenVal(ref a, _allRules, colVals, "MONLOC_NAME");
                    WQX_IMPORT_TEMP_MONLOC_GenVal(ref a, _allRules, colVals, "MONLOC_TYPE");
                    WQX_IMPORT_TEMP_MONLOC_GenVal(ref a, _allRules, colVals, "MONLOC_DESC");
                    WQX_IMPORT_TEMP_MONLOC_GenVal(ref a, _allRules, colVals, "HUC_EIGHT");
                    WQX_IMPORT_TEMP_MONLOC_GenVal(ref a, _allRules, colVals, "HUC_TWELVE");
                    WQX_IMPORT_TEMP_MONLOC_GenVal(ref a, _allRules, colVals, "TRIBAL_LAND_IND");
                    WQX_IMPORT_TEMP_MONLOC_GenVal(ref a, _allRules, colVals, "TRIBAL_LAND_NAME");
                    WQX_IMPORT_TEMP_MONLOC_GenVal(ref a, _allRules, colVals, "LATITUDE_MSR");
                    WQX_IMPORT_TEMP_MONLOC_GenVal(ref a, _allRules, colVals, "LONGITUDE_MSR");
                    WQX_IMPORT_TEMP_MONLOC_GenVal(ref a, _allRules, colVals, "SOURCE_MAP_SCALE");
                    WQX_IMPORT_TEMP_MONLOC_GenVal(ref a, _allRules, colVals, "HORIZ_COLL_METHOD");
                    WQX_IMPORT_TEMP_MONLOC_GenVal(ref a, _allRules, colVals, "HORIZ_REF_DATUM");
                    WQX_IMPORT_TEMP_MONLOC_GenVal(ref a, _allRules, colVals, "VERT_MEASURE");
                    WQX_IMPORT_TEMP_MONLOC_GenVal(ref a, _allRules, colVals, "VERT_MEASURE_UNIT");
                    WQX_IMPORT_TEMP_MONLOC_GenVal(ref a, _allRules, colVals, "VERT_COLL_METHOD");
                    WQX_IMPORT_TEMP_MONLOC_GenVal(ref a, _allRules, colVals, "VERT_REF_DATUM");
                    WQX_IMPORT_TEMP_MONLOC_GenVal(ref a, _allRules, colVals, "COUNTRY_CODE");
                    WQX_IMPORT_TEMP_MONLOC_GenVal(ref a, _allRules, colVals, "STATE_CODE");
                    WQX_IMPORT_TEMP_MONLOC_GenVal(ref a, _allRules, colVals, "WELL_TYPE");
                    WQX_IMPORT_TEMP_MONLOC_GenVal(ref a, _allRules, colVals, "AQUIFER_NAME");
                    WQX_IMPORT_TEMP_MONLOC_GenVal(ref a, _allRules, colVals, "FORMATION_TYPE");
                    WQX_IMPORT_TEMP_MONLOC_GenVal(ref a, _allRules, colVals, "WELLHOLE_DEPTH_MSR");
                    WQX_IMPORT_TEMP_MONLOC_GenVal(ref a, _allRules, colVals, "WELLHOLE_DEPTH_MSR_UNIT");

                    //*************** POST CUSTOM VALIDATION **********************************************
                    if (!string.IsNullOrEmpty(a.MONLOC_ID))
                        if (db_WQX.GetWQX_MONLOC_ByIDString(oRG_ID, a.MONLOC_ID) != null) { a.IMPORT_STATUS_CD = "F"; a.IMPORT_STATUS_DESC += "Monitoring Location ID already exists. "; }

                    decimal ii;
                    if (!string.IsNullOrEmpty(a.LATITUDE_MSR))
                        if (Decimal.TryParse(a.LATITUDE_MSR, out ii) == false) { a.IMPORT_STATUS_CD = "F"; a.IMPORT_STATUS_DESC += "Latitude is not decimal format. "; }

                    if (!string.IsNullOrEmpty(a.LONGITUDE_MSR))
                        if (Decimal.TryParse(a.LONGITUDE_MSR, out ii) == false) { a.IMPORT_STATUS_CD = "F"; a.IMPORT_STATUS_DESC += "Longitude is not decimal format. "; }
                    //*************** END CUSTOM VALIDATION **********************************************


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

        public static void WQX_IMPORT_TEMP_MONLOC_GenVal(ref T_WQX_IMPORT_TEMP_MONLOC a, List<ConfigInfoType> t, Dictionary<string, string> colVals, string f)
        {
            string _value = Utils.GetValueOrDefault(colVals, f); //supplied value for this field
            var _rules = t.Find(item => item._name == f);   //import rules for this field

            if (!string.IsNullOrEmpty(_value)) //if value is supplied
            {
                _value = _value.Trim();

                //strings: field length validation and substring 
                if (_rules._datatype == "" && _rules._length != null)
                {
                    if (_value.Length > _rules._length)
                    {
                        a.IMPORT_STATUS_CD = "F";
                        a.IMPORT_STATUS_DESC = (a.IMPORT_STATUS_DESC + f + " length (" + _rules._length + ") exceeded. ").SubStringPlus(0, 100);

                        _value = _value.SubStringPlus(0, (int)_rules._length);
                    }
                }

                //integers: check type
                if (_rules._datatype == "int")
                {
                    int n;
                    if (int.TryParse(_value, out n) == false)
                    {
                        a.IMPORT_STATUS_CD = "F";
                        a.IMPORT_STATUS_DESC = (a.IMPORT_STATUS_DESC + f + " not numeric. ").SubStringPlus(0, 100);
                    }
                }


                //ref data lookup
                if (_rules._fkey.Length > 0)
                {
                    if (db_Ref.GetT_WQX_REF_DATA_ByKey(_rules._fkey, _value) == false)
                    {
                        a.IMPORT_STATUS_CD = "F";
                        a.IMPORT_STATUS_DESC = (a.IMPORT_STATUS_DESC + f + " not valid. ").SubStringPlus(0, 100);
                    }
                }
            }
            else
            {
                //required check
                if (_rules._req == "Y")
                {
                    _value = "-";
                    a.IMPORT_STATUS_CD = "F";
                    a.IMPORT_STATUS_DESC = (a.IMPORT_STATUS_DESC + "Required field " + f + " missing. ").SubStringPlus(0, 100);
                }
            }

            //finally set the value before returning
            if (_rules._datatype == "")
                typeof(T_WQX_IMPORT_TEMP_MONLOC).GetProperty(f).SetValue(a, _value);
            else if (_rules._datatype == "int")
                typeof(T_WQX_IMPORT_TEMP_MONLOC).GetProperty(f).SetValue(a, _value.ConvertOrDefault<int?>());
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

        public static int GetWQX_IMPORT_TEMP_MONLOC_CountByUserID(string UserID)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_WQX_IMPORT_TEMP_MONLOC
                            where a.USER_ID == UserID
                            select a).Count();
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

        public static int DeleteT_WQX_IMPORT_TEMP_MONLOC(string uSER_ID)
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



        // *************************** IMPORT: PROJECT    ******************************
        // *****************************************************************************
        public static int InsertOrUpdateWQX_IMPORT_TEMP_PROJECT(global::System.Int32? tEMP_PROJECT_IDX, string uSER_ID, global::System.Int32? pROJECT_IDX, global::System.String oRG_ID,
            global::System.String pROJECT_ID, global::System.String pROJECT_NAME, global::System.String pROJECT_DESC, global::System.String sAMP_DESIGN_TYPE_CD,
            global::System.Boolean? qAPP_APPROVAL_IND, global::System.String qAPP_APPROVAL_AGENCY, string sTATUS_CD, string sTATUS_DESC)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                Boolean insInd = false;
                try
                {
                    T_WQX_IMPORT_TEMP_PROJECT a = new T_WQX_IMPORT_TEMP_PROJECT();

                    if (tEMP_PROJECT_IDX != null)
                        a = (from c in ctx.T_WQX_IMPORT_TEMP_PROJECT
                             where c.TEMP_PROJECT_IDX == tEMP_PROJECT_IDX
                             select c).FirstOrDefault();
                    else
                        insInd = true;

                    if (uSER_ID != null)
                    {
                        a.USER_ID = uSER_ID;
                        if (uSER_ID.Length > 25) { sTATUS_CD = "F"; sTATUS_DESC += "User ID length exceeded. "; }
                    }

                    if (pROJECT_IDX != null) a.PROJECT_IDX = pROJECT_IDX;
                    if (oRG_ID != null) a.ORG_ID = oRG_ID;

                    if (pROJECT_ID != null)
                    {
                        a.PROJECT_ID = pROJECT_ID.SubStringPlus(0, 35).Trim();
                        if (pROJECT_ID.Length > 35) { sTATUS_CD = "F"; sTATUS_DESC += "Project ID length exceeded. "; }

                        T_WQX_PROJECT ptemp = db_WQX.GetWQX_PROJECT_ByIDString(pROJECT_ID, oRG_ID);
                        if (ptemp != null) { sTATUS_CD = "F"; sTATUS_DESC += "Project ID already exists. "; }
                    }

                    if (!string.IsNullOrEmpty(pROJECT_NAME))
                    {
                        a.PROJECT_NAME = pROJECT_NAME.SubStringPlus(0, 120).Trim();
                        if (pROJECT_NAME.Length > 120) { sTATUS_CD = "F"; sTATUS_DESC += "Project Name length exceeded. "; }
                    }

                    if (!string.IsNullOrEmpty(pROJECT_DESC))
                    {
                        a.PROJECT_DESC = pROJECT_DESC.SubStringPlus(0, 1999);
                        if (pROJECT_DESC.Length > 1999) { sTATUS_CD = "F"; sTATUS_DESC += "Project Description length exceeded. "; }
                    }

                    if (!string.IsNullOrEmpty(sAMP_DESIGN_TYPE_CD))
                    {
                        a.SAMP_DESIGN_TYPE_CD = sAMP_DESIGN_TYPE_CD.Trim().SubStringPlus(0, 20);
                        if (sAMP_DESIGN_TYPE_CD.Length > 20) { sTATUS_CD = "F"; sTATUS_DESC += "Sample Design Type Code length exceeded. "; }
                    }

                    if (qAPP_APPROVAL_IND != null)
                    {
                        a.QAPP_APPROVAL_IND = qAPP_APPROVAL_IND;
                    }

                    if (!string.IsNullOrEmpty(qAPP_APPROVAL_AGENCY))
                    {
                        a.QAPP_APPROVAL_AGENCY = qAPP_APPROVAL_AGENCY.SubStringPlus(0, 50);
                        if (qAPP_APPROVAL_AGENCY.Length > 50) { sTATUS_CD = "F"; sTATUS_DESC += "QAPP Approval Agency length exceeded. "; }
                    }

                    if (sTATUS_CD != null) a.IMPORT_STATUS_CD = sTATUS_CD;
                    if (sTATUS_DESC != null) a.IMPORT_STATUS_DESC = sTATUS_DESC.SubStringPlus(0, 100);

                    if (insInd) //insert case
                        ctx.AddToT_WQX_IMPORT_TEMP_PROJECT(a);

                    ctx.SaveChanges();

                    return a.TEMP_PROJECT_IDX;
                }
                catch (Exception ex)
                {
                    sTATUS_CD = "F"; 
                    sTATUS_DESC += "Unspecified error. ";
                    return 0;
                }
            }
        }

        public static List<T_WQX_IMPORT_TEMP_PROJECT> GetWQX_IMPORT_TEMP_PROJECT(string UserID)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_WQX_IMPORT_TEMP_PROJECT
                            where a.USER_ID == UserID
                            orderby a.TEMP_PROJECT_IDX
                            select a).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static T_WQX_IMPORT_TEMP_PROJECT GetWQX_IMPORT_TEMP_PROJECT_ByID(int TempProjectID)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_WQX_IMPORT_TEMP_PROJECT
                            where a.TEMP_PROJECT_IDX == TempProjectID
                            select a).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static int DeleteT_WQX_IMPORT_TEMP_PROJECT(global::System.String uSER_ID)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    string sql = "DELETE FROM T_WQX_IMPORT_TEMP_PROJECT WHERE USER_ID = '" + uSER_ID + "'";
                    ctx.ExecuteStoreCommand(sql);
                    return 1;
                }
                catch
                {
                    return 0;
                }
            }
        }



        // *************************** IMPORT: SAMPLE    ******************************
        // *****************************************************************************
        public static int InsertOrUpdateWQX_IMPORT_TEMP_SAMPLE(global::System.Int32? tEMP_SAMPLE_IDX, string uSER_ID, global::System.String oRG_ID, global::System.Int32? pROJECT_IDX,
            string pROJECT_ID, global::System.Int32? mONLOC_IDX, string mONLOC_ID, global::System.Int32? aCTIVITY_IDX, global::System.String aCTIVITY_ID,
            global::System.String aCT_TYPE, global::System.String aCT_MEDIA, global::System.String aCT_SUBMEDIA, global::System.DateTime? aCT_START_DT, global::System.DateTime? aCT_END_DT,
            global::System.String aCT_TIME_ZONE, global::System.String rELATIVE_DEPTH_NAME, global::System.String aCT_DEPTHHEIGHT_MSR, global::System.String aCT_DEPTHHEIGHT_MSR_UNIT, 
            global::System.String tOP_DEPTHHEIGHT_MSR, global::System.String tOP_DEPTHHEIGHT_MSR_UNIT, global::System.String bOT_DEPTHHEIGHT_MSR, global::System.String bOT_DEPTHHEIGHT_MSR_UNIT,
            global::System.String dEPTH_REF_POINT, global::System.String aCT_COMMENT, global::System.String bIO_ASSEMBLAGE_SAMPLED, global::System.String bIO_DURATION_MSR, 
            global::System.String bIO_DURATION_MSR_UNIT, global::System.String bIO_SAMP_COMPONENT, int? bIO_SAMP_COMPONENT_SEQ, global::System.String bIO_REACH_LEN_MSR,
            global::System.String bIO_REACH_LEN_MSR_UNIT, global::System.String bIO_REACH_WID_MSR, global::System.String bIO_REACH_WID_MSR_UNIT, int? bIO_PASS_COUNT,
            global::System.String bIO_NET_TYPE, global::System.String bIO_NET_AREA_MSR, global::System.String bIO_NET_AREA_MSR_UNIT, global::System.String bIO_NET_MESHSIZE_MSR,
            global::System.String bIO_MESHSIZE_MSR_UNIT, global::System.String bIO_BOAT_SPEED_MSR, global::System.String bIO_BOAT_SPEED_MSR_UNIT, global::System.String bIO_CURR_SPEED_MSR, 
            global::System.String bIO_CURR_SPEED_MSR_UNIT, global::System.String bIO_TOXICITY_TEST_TYPE, int? sAMP_COLL_METHOD_IDX, global::System.String sAMP_COLL_METHOD_ID, 
            global::System.String sAMP_COLL_METHOD_CTX, global::System.String sAMP_COLL_METHOD_NAME, global::System.String sAMP_COLL_EQUIP, global::System.String sAMP_COLL_EQUIP_COMMENT, 
            int? sAMP_PREP_IDX, global::System.String sAMP_PREP_ID, global::System.String sAMP_PREP_CTX, global::System.String sAMP_PREP_NAME,
            global::System.String sAMP_PREP_CONT_TYPE, global::System.String sAMP_PREP_CONT_COLOR, global::System.String sAMP_PREP_CHEM_PRESERV,
            global::System.String sAMP_PREP_THERM_PRESERV, global::System.String sAMP_PREP_STORAGE_DESC, string sTATUS_CD, string sTATUS_DESC, bool BioIndicator, Boolean autoImportRefDataInd)
        {
            try
            {
                using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
                {
                    Boolean insInd = false;

                    //******************* GET STARTING RECORD *************************************************
                    T_WQX_IMPORT_TEMP_SAMPLE a;
                    if (tEMP_SAMPLE_IDX != null)  //grab from IDX if given
                        a = (from c in ctx.T_WQX_IMPORT_TEMP_SAMPLE
                             where c.TEMP_SAMPLE_IDX == tEMP_SAMPLE_IDX
                             select c).FirstOrDefault();
                    else  //check if existing activity ID exists in the import
                    {
                        a = (from c in ctx.T_WQX_IMPORT_TEMP_SAMPLE
                             where c.ACTIVITY_ID == aCTIVITY_ID
                             && c.ORG_ID == oRG_ID
                             select c).FirstOrDefault();
                    }

                    //if can't find a match based on supplied IDX or ID, then create a new record
                    if (a == null)
                    {
                        insInd = true;
                        a = new T_WQX_IMPORT_TEMP_SAMPLE();
                    }
                    //********************** END GET STARTING RECORD ************************************************


                    if (!string.IsNullOrEmpty(uSER_ID)) a.USER_ID = uSER_ID;
                    if (!string.IsNullOrEmpty(oRG_ID)) a.ORG_ID = oRG_ID;

                    //PROJECT HANDLING
                    if (pROJECT_IDX == null && pROJECT_ID == null) { sTATUS_CD = "F"; sTATUS_DESC += "Project ID must be provided. "; }
                    if (pROJECT_IDX != null) a.PROJECT_IDX = pROJECT_IDX;

                    if (pROJECT_ID != null)
                    {
                        a.PROJECT_ID = pROJECT_ID.Trim().SubStringPlus(0, 35);

                        T_WQX_PROJECT ptemp = db_WQX.GetWQX_PROJECT_ByIDString(pROJECT_ID, oRG_ID);
                        if (ptemp == null) { sTATUS_CD = "F"; sTATUS_DESC += "Project ID does not exist. Create project first."; }
                        else { a.PROJECT_IDX = ptemp.PROJECT_IDX; }
                    }

                    //MONITORING LOCATION HANDLING
                    if (mONLOC_IDX == null && mONLOC_ID == null) { sTATUS_CD = "F"; sTATUS_DESC += "Monitoring Location ID must be provided. "; }
                    if (mONLOC_IDX != null) a.MONLOC_IDX = mONLOC_IDX;

                    if (mONLOC_ID != null)
                    {
                        a.MONLOC_ID = mONLOC_ID.Trim().SubStringPlus(0, 35);

                        T_WQX_MONLOC mm = db_WQX.GetWQX_MONLOC_ByIDString(oRG_ID, mONLOC_ID);
                        if (mm == null) { sTATUS_CD = "F"; sTATUS_DESC += "Monitoring Location ID does not exist. Import MonLocs first."; }
                        else { a.MONLOC_IDX = mm.MONLOC_IDX; }
                    }


                    //ACTIVITY ID HANDLING
                    if (aCTIVITY_IDX == null && aCTIVITY_ID == null) { sTATUS_CD = "F"; sTATUS_DESC += "Activity ID must be provided. "; }
                    if (aCTIVITY_IDX != null) a.ACTIVITY_IDX = aCTIVITY_IDX;
                    if (!string.IsNullOrEmpty(aCTIVITY_ID)) a.ACTIVITY_ID = aCTIVITY_ID.Trim().SubStringPlus(0, 35);


                    //ACTIVITY TYPE HANDLING
                    if (!string.IsNullOrEmpty(aCT_TYPE))
                    {
                        a.ACT_TYPE = aCT_TYPE.SubStringPlus(0, 70) ?? "";
                        if (db_Ref.GetT_WQX_REF_DATA_ByKey("ActivityType", aCT_TYPE.Trim()) == false) { sTATUS_CD = "F"; sTATUS_DESC += "Activity Type not valid. "; }
                    }
                    else
                    { a.ACT_TYPE = "";  sTATUS_CD = "F"; sTATUS_DESC += "Activity Type is required."; }

                    if (!string.IsNullOrEmpty(aCT_MEDIA))
                    {
                        a.ACT_MEDIA = aCT_MEDIA.SubStringPlus(0, 20) ?? "";
                        if (db_Ref.GetT_WQX_REF_DATA_ByKey("ActivityMedia", aCT_MEDIA.Trim()) == false) { sTATUS_CD = "F"; sTATUS_DESC += "Activity Media not valid. "; }
                    }
                    else
                    { a.ACT_MEDIA = ""; sTATUS_CD = "F"; sTATUS_DESC += "Activity Media is required."; }

                    if (!string.IsNullOrEmpty(aCT_SUBMEDIA))
                    {
                        a.ACT_SUBMEDIA = aCT_SUBMEDIA.SubStringPlus(0, 45);
                        if (db_Ref.GetT_WQX_REF_DATA_ByKey("ActivityMediaSubdivision", aCT_SUBMEDIA.Trim()) == false) { sTATUS_CD = "F"; sTATUS_DESC += "Activity Media Subdivision not valid. "; }
                    }


                    if (aCT_START_DT == null)
                    {
                        sTATUS_CD = "F"; sTATUS_DESC += "Activity Start Date must be provided. ";
                    }
                    else
                    {
                        //fix improperly formatted datetime
                        if (aCT_START_DT.ConvertOrDefault<DateTime>().Year < 1900)
                            { sTATUS_CD = "F"; sTATUS_DESC += "Activity Start Date is formatted incorrectly. "; }
                        else
                            a.ACT_START_DT = aCT_START_DT;
                    }

                    if (aCT_END_DT != null)
                    {
                        //fix improperly formatted datetime
                        if (aCT_END_DT.ConvertOrDefault<DateTime>().Year < 1900)
                            aCT_END_DT = null;

                        a.ACT_END_DT = aCT_END_DT;
                    }

                    if (!string.IsNullOrEmpty(aCT_TIME_ZONE))
                    {
                        a.ACT_TIME_ZONE = aCT_TIME_ZONE.Trim().SubStringPlus(0, 4);
                        if (db_Ref.GetT_WQX_REF_DATA_ByKey("TimeZone", aCT_TIME_ZONE.Trim()) == false) { sTATUS_CD = "F"; sTATUS_DESC += "TimeZone not valid. "; }
                    }
                    else
                    {
                        //put in Timezone if missing
                        a.ACT_TIME_ZONE = Utils.GetWQXTimeZoneByDate(a.ACT_START_DT.ConvertOrDefault<DateTime>());
                    }


                    if (!string.IsNullOrEmpty(rELATIVE_DEPTH_NAME))
                    {
                        a.RELATIVE_DEPTH_NAME = rELATIVE_DEPTH_NAME.Trim().SubStringPlus(0, 15);
                        if (db_Ref.GetT_WQX_REF_DATA_ByKey("ActivityRelativeDepth", rELATIVE_DEPTH_NAME.Trim()) == false) { sTATUS_CD = "F"; sTATUS_DESC += "Relative Depth Name not valid. "; }
                    }

                    if (!string.IsNullOrEmpty(aCT_DEPTHHEIGHT_MSR))
                    {
                        a.ACT_DEPTHHEIGHT_MSR = aCT_DEPTHHEIGHT_MSR.Trim().SubStringPlus(0, 12);
                    }
                    
                    if (!string.IsNullOrEmpty(aCT_DEPTHHEIGHT_MSR_UNIT))
                    {
                        a.ACT_DEPTHHEIGHT_MSR_UNIT = aCT_DEPTHHEIGHT_MSR_UNIT.Trim().SubStringPlus(0, 12);
                        if (db_Ref.GetT_WQX_REF_DATA_ByKey("MeasureUnit", aCT_DEPTHHEIGHT_MSR_UNIT.Trim()) == false) { sTATUS_CD = "F"; sTATUS_DESC += "Depth Measure Unit not valid. "; }
                    }

                    if (!string.IsNullOrEmpty(tOP_DEPTHHEIGHT_MSR))
                    {
                        a.TOP_DEPTHHEIGHT_MSR = tOP_DEPTHHEIGHT_MSR.Trim().SubStringPlus(0, 12);
                    }

                    if (!string.IsNullOrEmpty(tOP_DEPTHHEIGHT_MSR_UNIT))
                    {
                        a.TOP_DEPTHHEIGHT_MSR_UNIT = tOP_DEPTHHEIGHT_MSR_UNIT.Trim().SubStringPlus(0, 12);
                        if (db_Ref.GetT_WQX_REF_DATA_ByKey("MeasureUnit", tOP_DEPTHHEIGHT_MSR_UNIT.Trim()) == false) { sTATUS_CD = "F"; sTATUS_DESC += "Top Depth Measure Unit not valid. "; }
                    }

                    if (!string.IsNullOrEmpty(bOT_DEPTHHEIGHT_MSR))
                    {
                        a.BOT_DEPTHHEIGHT_MSR = bOT_DEPTHHEIGHT_MSR.Trim().SubStringPlus(0, 12);
                    }

                    if (!string.IsNullOrEmpty(bOT_DEPTHHEIGHT_MSR_UNIT))
                    {
                        a.BOT_DEPTHHEIGHT_MSR_UNIT = bOT_DEPTHHEIGHT_MSR_UNIT.Trim().SubStringPlus(0, 12);
                        if (db_Ref.GetT_WQX_REF_DATA_ByKey("MeasureUnit", bOT_DEPTHHEIGHT_MSR_UNIT.Trim()) == false) { sTATUS_CD = "F"; sTATUS_DESC += "Bottom Depth Measure Unit not valid. "; }
                    }

                    if (!string.IsNullOrEmpty(dEPTH_REF_POINT))
                    {
                        a.DEPTH_REF_POINT = dEPTH_REF_POINT.Trim().SubStringPlus(0, 125);
                    }

                    if (!string.IsNullOrEmpty(aCT_COMMENT))
                    {
                        a.ACT_COMMENT = aCT_COMMENT.Trim().SubStringPlus(0, 4000);
                    }

                    //BIOLOGICAL MONITORING 
                    if (BioIndicator == true)
                    {
                        if (!string.IsNullOrEmpty(bIO_ASSEMBLAGE_SAMPLED))
                        {
                            a.BIO_ASSEMBLAGE_SAMPLED = bIO_ASSEMBLAGE_SAMPLED.Trim().SubStringPlus(0, 50);
                            if (db_Ref.GetT_WQX_REF_DATA_ByKey("Assemblage", bIO_ASSEMBLAGE_SAMPLED.Trim()) == false) { sTATUS_CD = "F"; sTATUS_DESC += "Bio Assemblage not valid. "; }
                        }

                        if (!string.IsNullOrEmpty(bIO_DURATION_MSR))
                        {
                            a.BIO_DURATION_MSR = bIO_DURATION_MSR.Trim().SubStringPlus(0, 12);
                        }

                        if (!string.IsNullOrEmpty(bIO_DURATION_MSR_UNIT))
                        {
                            a.BIO_DURATION_MSR_UNIT = bIO_DURATION_MSR_UNIT.Trim().SubStringPlus(0, 12);
                            if (db_Ref.GetT_WQX_REF_DATA_ByKey("MeasureUnit", bIO_DURATION_MSR_UNIT.Trim()) == false) { sTATUS_CD = "F"; sTATUS_DESC += "Bio Collection Duration Unit not valid. "; }
                        }

                        if (!string.IsNullOrEmpty(bIO_SAMP_COMPONENT))
                        {
                            a.BIO_SAMP_COMPONENT = bIO_SAMP_COMPONENT.Trim().SubStringPlus(0, 15);
                        }

                        if (bIO_SAMP_COMPONENT_SEQ != null) a.BIO_SAMP_COMPONENT_SEQ = bIO_SAMP_COMPONENT_SEQ;

                        if (!string.IsNullOrEmpty(bIO_REACH_LEN_MSR))
                        {
                            a.BIO_REACH_LEN_MSR = bIO_REACH_LEN_MSR.Trim().SubStringPlus(0, 12);
                        }

                        if (!string.IsNullOrEmpty(bIO_REACH_LEN_MSR_UNIT))
                        {
                            a.BIO_REACH_LEN_MSR_UNIT = bIO_REACH_LEN_MSR_UNIT.Trim().SubStringPlus(0, 12);
                            if (db_Ref.GetT_WQX_REF_DATA_ByKey("MeasureUnit", bIO_REACH_LEN_MSR_UNIT.Trim()) == false) { sTATUS_CD = "F"; sTATUS_DESC += "Bio Reach Length Unit not valid. "; }
                        }

                        if (!string.IsNullOrEmpty(bIO_REACH_WID_MSR))
                        {
                            a.BIO_REACH_WID_MSR = bIO_REACH_WID_MSR.Trim().SubStringPlus(0, 12);
                        }

                        if (!string.IsNullOrEmpty(bIO_REACH_WID_MSR_UNIT))
                        {
                            a.BIO_REACH_WID_MSR_UNIT = bIO_REACH_WID_MSR_UNIT.Trim().SubStringPlus(0, 12);
                            if (db_Ref.GetT_WQX_REF_DATA_ByKey("MeasureUnit", bIO_REACH_WID_MSR_UNIT.Trim()) == false) { sTATUS_CD = "F"; sTATUS_DESC += "Bio Reach Width Unit not valid. "; }
                        }

                        if (bIO_PASS_COUNT != null) a.BIO_PASS_COUNT = bIO_PASS_COUNT;

                        if (!string.IsNullOrEmpty(bIO_NET_TYPE))
                        {
                            a.BIO_NET_TYPE = bIO_NET_TYPE.Trim().SubStringPlus(0, 30);
                            if (db_Ref.GetT_WQX_REF_DATA_ByKey("NetType", bIO_NET_TYPE.Trim()) == false) { sTATUS_CD = "F"; sTATUS_DESC += "Bio Net Type not valid. "; }
                        }

                        if (!string.IsNullOrEmpty(bIO_NET_AREA_MSR))
                        {
                            a.BIO_NET_AREA_MSR = bIO_NET_AREA_MSR.Trim().SubStringPlus(0, 12);
                        }

                        if (!string.IsNullOrEmpty(bIO_NET_AREA_MSR_UNIT))
                        {
                            a.BIO_NET_AREA_MSR_UNIT = bIO_NET_AREA_MSR_UNIT.Trim().SubStringPlus(0, 12);
                            if (db_Ref.GetT_WQX_REF_DATA_ByKey("MeasureUnit", bIO_NET_AREA_MSR_UNIT.Trim()) == false) { sTATUS_CD = "F"; sTATUS_DESC += "Bio Net Area Unit not valid. "; }
                        }

                        if (!string.IsNullOrEmpty(bIO_NET_MESHSIZE_MSR))
                        {
                            a.BIO_NET_MESHSIZE_MSR = bIO_NET_MESHSIZE_MSR.Trim().SubStringPlus(0, 12);
                        }

                        if (!string.IsNullOrEmpty(bIO_MESHSIZE_MSR_UNIT))
                        {
                            a.BIO_MESHSIZE_MSR_UNIT = bIO_MESHSIZE_MSR_UNIT.Trim().SubStringPlus(0, 12);
                            if (db_Ref.GetT_WQX_REF_DATA_ByKey("MeasureUnit", bIO_MESHSIZE_MSR_UNIT.Trim()) == false) { sTATUS_CD = "F"; sTATUS_DESC += "Bio Net Mesh Size Unit not valid. "; }
                        }

                        if (!string.IsNullOrEmpty(bIO_BOAT_SPEED_MSR))
                        {
                            a.BIO_BOAT_SPEED_MSR = bIO_BOAT_SPEED_MSR.Trim().SubStringPlus(0, 12);
                        }

                        if (!string.IsNullOrEmpty(bIO_BOAT_SPEED_MSR_UNIT))
                        {
                            a.BIO_BOAT_SPEED_MSR_UNIT = bIO_BOAT_SPEED_MSR_UNIT.Trim().SubStringPlus(0, 12);
                            if (db_Ref.GetT_WQX_REF_DATA_ByKey("MeasureUnit", bIO_BOAT_SPEED_MSR_UNIT.Trim()) == false) { sTATUS_CD = "F"; sTATUS_DESC += "Boat Speed Unit not valid. "; }
                        }

                        if (!string.IsNullOrEmpty(bIO_CURR_SPEED_MSR))
                        {
                            a.BIO_CURR_SPEED_MSR = bIO_CURR_SPEED_MSR.Trim().SubStringPlus(0, 12);
                        }

                        if (!string.IsNullOrEmpty(bIO_CURR_SPEED_MSR_UNIT))
                        {
                            a.BIO_CURR_SPEED_MSR_UNIT = bIO_CURR_SPEED_MSR_UNIT.Trim().SubStringPlus(0, 12);
                            if (db_Ref.GetT_WQX_REF_DATA_ByKey("MeasureUnit", bIO_CURR_SPEED_MSR_UNIT.Trim()) == false) { sTATUS_CD = "F"; sTATUS_DESC += "Current Speed Unit not valid. "; }
                        }

                        if (!string.IsNullOrEmpty(bIO_TOXICITY_TEST_TYPE))
                        {
                            a.BIO_TOXICITY_TEST_TYPE = bIO_TOXICITY_TEST_TYPE.Trim().SubStringPlus(0, 7);
                            if (db_Ref.GetT_WQX_REF_DATA_ByKey("ToxicityTestType", bIO_TOXICITY_TEST_TYPE.Trim()) == false) { sTATUS_CD = "F"; sTATUS_DESC += "Toxicity Test Type not valid. "; }
                        }
                    }

                    if (sAMP_COLL_METHOD_IDX != null)
                    {
                        a.SAMP_COLL_METHOD_IDX = sAMP_COLL_METHOD_IDX;

                        //if IDX is populated but ID/Name/Ctx aren't then grab them
                        T_WQX_REF_SAMP_COL_METHOD scm = db_Ref.GetT_WQX_REF_SAMP_COL_METHOD_ByIDX(a.SAMP_COLL_METHOD_IDX);
                        if (scm != null)
                        {
                            a.SAMP_COLL_METHOD_ID = scm.SAMP_COLL_METHOD_ID;
                            a.SAMP_COLL_METHOD_NAME = scm.SAMP_COLL_METHOD_NAME;
                            a.SAMP_COLL_METHOD_CTX = scm.SAMP_COLL_METHOD_CTX;                           
                        }
                    }
                    else
                    {
                        //set context to org id if none is provided 
                        if (!string.IsNullOrEmpty(sAMP_COLL_METHOD_ID) && string.IsNullOrEmpty(sAMP_COLL_METHOD_CTX))
                            sAMP_COLL_METHOD_CTX = oRG_ID;

                        if (!string.IsNullOrEmpty(sAMP_COLL_METHOD_ID) && !string.IsNullOrEmpty(sAMP_COLL_METHOD_CTX))
                        {
                            //lookup matching collection method IDX
                            T_WQX_REF_SAMP_COL_METHOD scm = db_Ref.GetT_WQX_REF_SAMP_COL_METHOD_ByIDandContext(sAMP_COLL_METHOD_ID.Trim(), sAMP_COLL_METHOD_CTX.Trim());
                            if (scm != null)
                                a.SAMP_COLL_METHOD_IDX = scm.SAMP_COLL_METHOD_IDX;
                            else  //no matching sample collection method lookup found
                            {
                                if (autoImportRefDataInd == true)
                                {
                                    db_Ref.InsertOrUpdateT_WQX_REF_SAMP_COL_METHOD(null, sAMP_COLL_METHOD_ID.Trim(), sAMP_COLL_METHOD_CTX.Trim(), sAMP_COLL_METHOD_NAME.Trim(), "", true);
                                }
                                else
                                {
                                    sTATUS_CD = "F"; sTATUS_DESC += "No matching Sample Collection Method found - please add it at the Reference Data screen first. ";
                                }
                            }
                            //****************************************

                            a.SAMP_COLL_METHOD_ID = sAMP_COLL_METHOD_ID.Trim().SubStringPlus(0, 20);
                            a.SAMP_COLL_METHOD_CTX = sAMP_COLL_METHOD_CTX.Trim().SubStringPlus(0, 120);

                            if (!string.IsNullOrEmpty(sAMP_COLL_METHOD_NAME))
                            {
                                a.SAMP_COLL_METHOD_NAME = sAMP_COLL_METHOD_NAME.Trim().SubStringPlus(0, 120);
                            }
                        }
                    }

                    if (a.SAMP_COLL_METHOD_IDX == null && a.ACT_TYPE.ToUpper().Contains("SAMPLE"))
                    { sTATUS_CD = "F"; sTATUS_DESC += "Sample Collection Method is required when Activity Type contains the term -Sample-. "; }


                    if (!string.IsNullOrEmpty(sAMP_COLL_EQUIP))
                    {
                        a.SAMP_COLL_EQUIP = sAMP_COLL_EQUIP.Trim().SubStringPlus(0, 40);
                        if (db_Ref.GetT_WQX_REF_DATA_ByKey("SampleCollectionEquipment", sAMP_COLL_EQUIP.Trim()) == false) { sTATUS_CD = "F"; sTATUS_DESC += "Sample Collection Equipment not valid. "; }
                    }
                    else
                    {
                        //special validation requiring sampling collection equipment if activity type contains "Sample"
                        if (a.ACT_TYPE.ToUpper().Contains("SAMPLE"))
                        { sTATUS_CD = "F"; sTATUS_DESC += "Sample Collection Equipment is required when Activity Type contains the term -Sample-. "; }
                    }


                    if (!string.IsNullOrEmpty(sAMP_COLL_EQUIP_COMMENT))
                    {
                        a.SAMP_COLL_EQUIP_COMMENT = sAMP_COLL_EQUIP_COMMENT.Trim().SubStringPlus(0, 4000);
                    }


                    if (sAMP_PREP_IDX != null)
                        a.SAMP_PREP_IDX = sAMP_PREP_IDX;
                    else
                    {
                        //set context to org id if none is provided 
                        if (!string.IsNullOrEmpty(sAMP_PREP_ID) && string.IsNullOrEmpty(sAMP_PREP_CTX))
                            sAMP_PREP_CTX = oRG_ID;
                        
                        if (!string.IsNullOrEmpty(sAMP_PREP_ID) && !string.IsNullOrEmpty(sAMP_PREP_CTX))
                        {
                            //see if matching prep method exists
                            T_WQX_REF_SAMP_PREP sp = db_Ref.GetT_WQX_REF_SAMP_PREP_ByIDandContext(sAMP_PREP_ID.Trim(), sAMP_PREP_CTX.Trim());
                            if (sp != null)
                                a.SAMP_PREP_IDX = sp.SAMP_PREP_IDX;
                            //****************************************

                            a.SAMP_PREP_ID = sAMP_PREP_ID.Trim().SubStringPlus(0, 20);
                            a.SAMP_PREP_CTX = sAMP_PREP_CTX.Trim().SubStringPlus(0, 120);

                            if (!string.IsNullOrEmpty(sAMP_PREP_NAME))
                            {
                                a.SAMP_PREP_NAME = sAMP_PREP_NAME.Trim().SubStringPlus(0, 120);
                            }
                        }
                    }


                    if (!string.IsNullOrEmpty(sAMP_PREP_CONT_TYPE))
                    {
                        a.SAMP_PREP_CONT_TYPE = sAMP_PREP_CONT_TYPE.Trim().SubStringPlus(0, 35);
                        if (db_Ref.GetT_WQX_REF_DATA_ByKey("SampleContainerType", sAMP_PREP_CONT_TYPE.Trim()) == false) { sTATUS_CD = "F"; sTATUS_DESC += "Sample Container Type not valid. "; }
                    }


                    if (!string.IsNullOrEmpty(sAMP_PREP_CONT_COLOR))
                    {
                        a.SAMP_PREP_CONT_COLOR = sAMP_PREP_CONT_COLOR.Trim().SubStringPlus(0, 15);
                        if (db_Ref.GetT_WQX_REF_DATA_ByKey("SampleContainerColor", sAMP_PREP_CONT_COLOR.Trim()) == false) { sTATUS_CD = "F"; sTATUS_DESC += "Sample Container Color not valid. "; }
                    }


                    if (!string.IsNullOrEmpty(sAMP_PREP_CHEM_PRESERV))
                    {
                        a.SAMP_PREP_CHEM_PRESERV = sAMP_PREP_CHEM_PRESERV.Trim().SubStringPlus(0, 250);
                    }

                    if (!string.IsNullOrEmpty(sAMP_PREP_THERM_PRESERV))
                    {
                        a.SAMP_PREP_THERM_PRESERV = sAMP_PREP_THERM_PRESERV.Trim().SubStringPlus(0, 25);
                        if (db_Ref.GetT_WQX_REF_DATA_ByKey("ThermalPreservativeUsed", sAMP_PREP_THERM_PRESERV.Trim()) == false) { sTATUS_CD = "F"; sTATUS_DESC += "Thermal Preservative Used not valid. "; }
                    }

                    if (!string.IsNullOrEmpty(sAMP_PREP_STORAGE_DESC))
                    {
                        a.SAMP_PREP_STORAGE_DESC = sAMP_PREP_STORAGE_DESC.Trim().SubStringPlus(0, 250);
                    }


                    if (sTATUS_CD != null) a.IMPORT_STATUS_CD = sTATUS_CD;
                    if (sTATUS_DESC != null) a.IMPORT_STATUS_DESC = sTATUS_DESC.SubStringPlus(0,100);

                    if (insInd) //insert case
                        ctx.AddToT_WQX_IMPORT_TEMP_SAMPLE(a);

                    ctx.SaveChanges();

                    return a.TEMP_SAMPLE_IDX;
                }
            }
            catch (Exception ex)
            {
                sTATUS_CD = "F";
                sTATUS_DESC += "Unspecified error";
                return 0;
            }

        }

        public static int InsertUpdateWQX_IMPORT_TEMP_SAMPLE_New(string uSER_ID, string oRG_ID, int? pROJECT_IDX, string pROJECT_ID, Dictionary<string, string> colVals)
        {
            try
            {
                using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
                {
                    bool insInd = false;

                    //******************* GET STARTING RECORD *************************************************
                    string _a = Utils.GetValueOrDefault(colVals, "ACTIVITY_ID");
                    T_WQX_IMPORT_TEMP_SAMPLE a = (from c in ctx.T_WQX_IMPORT_TEMP_SAMPLE
                            where c.ACTIVITY_ID == _a
                            && c.ORG_ID == oRG_ID
                            select c).FirstOrDefault();

                    //if can't find a match based on supplied ID, then create a new record
                    if (a == null)
                    {
                        insInd = true;
                        a = new T_WQX_IMPORT_TEMP_SAMPLE();
                    }
                    //********************** END GET STARTING RECORD ************************************************


                    a.IMPORT_STATUS_CD = "P";
                    a.IMPORT_STATUS_DESC = "";

                    if (!string.IsNullOrEmpty(uSER_ID)) a.USER_ID = uSER_ID; else return 0;
                    if (!string.IsNullOrEmpty(oRG_ID)) a.ORG_ID = oRG_ID; else return 0;
                    if (pROJECT_IDX != null) a.PROJECT_IDX = pROJECT_IDX; else return 0;
                    if (!string.IsNullOrEmpty(pROJECT_ID)) a.PROJECT_ID = pROJECT_ID; else return 0;

                    //get import config rules
                    List<ConfigInfoType> _allRules = Utils.GetAllColumnInfo("S");


                    //validate mandatory fields
                    WQX_IMPORT_TEMP_SAMPLE_GenVal(ref a, _allRules, colVals, "MONLOC_ID");
                    WQX_IMPORT_TEMP_SAMPLE_GenVal(ref a, _allRules, colVals, "ACTIVITY_ID");
                    WQX_IMPORT_TEMP_SAMPLE_GenVal(ref a, _allRules, colVals, "ACT_TYPE");
                    WQX_IMPORT_TEMP_SAMPLE_GenVal(ref a, _allRules, colVals, "ACT_MEDIA");
                    WQX_IMPORT_TEMP_SAMPLE_GenVal(ref a, _allRules, colVals, "ACT_START_DT");

                    //loop through all optional fields
                    List<string> rFields = new List<string>(new string[] { "ACT_SUBMEDIA","ACT_END_DT","ACT_TIME_ZONE","RELATIVE_DEPTH_NAME","ACT_DEPTHHEIGHT_MSR",
                        "ACT_DEPTHHEIGHT_MSR_UNIT","TOP_DEPTHHEIGHT_MSR","TOP_DEPTHHEIGHT_MSR_UNIT","BOT_DEPTHHEIGHT_MSR","BOT_DEPTHHEIGHT_MSR_UNIT","DEPTH_REF_POINT",
                        "ACT_COMMENT","BIO_ASSEMBLAGE_SAMPLED","BIO_DURATION_MSR","BIO_DURATION_MSR_UNIT","BIO_SAMP_COMPONENT", "BIO_SAMP_COMPONENT_SEQ","BIO_REACH_LEN_MSR",
                        "BIO_REACH_LEN_MSR_UNIT","BIO_REACH_WID_MSR","BIO_REACH_WID_MSR_UNIT","BIO_PASS_COUNT","BIO_NET_TYPE","BIO_NET_AREA_MSR","BIO_NET_AREA_MSR_UNIT",
                        "BIO_NET_MESHSIZE_MSR","BIO_MESHSIZE_MSR_UNIT","BIO_BOAT_SPEED_MSR","BIO_BOAT_SPEED_MSR_UNIT","BIO_CURR_SPEED_MSR","BIO_CURR_SPEED_MSR_UNIT",
                        "BIO_TOXICITY_TEST_TYPE","SAMP_COLL_METHOD_IDX","SAMP_COLL_METHOD_ID","SAMP_COLL_METHOD_CTX","SAMP_COLL_EQUIP","SAMP_COLL_EQUIP_COMMENT",
                        "SAMP_PREP_IDX","SAMP_PREP_ID","SAMP_PREP_CTX","SAMP_PREP_CONT_TYPE","SAMP_PREP_CONT_COLOR","SAMP_PREP_CHEM_PRESERV","SAMP_PREP_THERM_PRESERV","SAMP_PREP_STORAGE_DESC"
                    });

                    foreach (KeyValuePair<string, string> entry in colVals)
                        if (rFields.Contains(entry.Key))
                            WQX_IMPORT_TEMP_SAMPLE_GenVal(ref a, _allRules, colVals, entry.Key);


                    //********************** CUSTOM POST VALIDATION ********************************************
                    //SET MONLOC_IDX based on supplied MONLOC_ID
                    if (!string.IsNullOrEmpty(a.MONLOC_ID))
                    {
                        T_WQX_MONLOC mm = db_WQX.GetWQX_MONLOC_ByIDString(oRG_ID, a.MONLOC_ID);
                        if (mm == null) { a.IMPORT_STATUS_CD = "F"; a.IMPORT_STATUS_DESC += "Invalid Monitoring Location ID."; }
                        else { a.MONLOC_IDX = mm.MONLOC_IDX; }
                    }

                    //SET ACTIVITY TIMEZONE IF NOT SUPPLIED
                    if (string.IsNullOrEmpty(a.ACT_TIME_ZONE))
                        a.ACT_TIME_ZONE = Utils.GetWQXTimeZoneByDate(a.ACT_START_DT.ConvertOrDefault<DateTime>());

                    //special sampling collection method handling
                    if (a.SAMP_COLL_METHOD_IDX != null)
                    {
                        //if IDX is populated, grab ID/Name/Ctx 
                        T_WQX_REF_SAMP_COL_METHOD scm = db_Ref.GetT_WQX_REF_SAMP_COL_METHOD_ByIDX(a.SAMP_COLL_METHOD_IDX);
                        if (scm != null)
                        {
                            a.SAMP_COLL_METHOD_ID = scm.SAMP_COLL_METHOD_ID;
                            a.SAMP_COLL_METHOD_NAME = scm.SAMP_COLL_METHOD_NAME;
                            a.SAMP_COLL_METHOD_CTX = scm.SAMP_COLL_METHOD_CTX;
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(a.SAMP_COLL_METHOD_ID))
                        {
                            T_WQX_REF_SAMP_COL_METHOD scm = db_Ref.GetT_WQX_REF_SAMP_COL_METHOD_ByIDandContext(a.SAMP_COLL_METHOD_ID, a.SAMP_COLL_METHOD_CTX);
                            if (scm != null)
                                a.SAMP_COLL_METHOD_IDX = scm.SAMP_COLL_METHOD_IDX;
                            else  //no matching sample collection method lookup found
                            { a.IMPORT_STATUS_CD = "F"; a.IMPORT_STATUS_DESC += "No matching Sample Collection Method found - please add it at the Reference Data screen first. "; }
                        }
                    }


                    //special validation requiring sampling collection method if activity type contains "Sample"
                    if (a.SAMP_COLL_METHOD_IDX == null && a.ACT_TYPE.ToUpper().Contains("SAMPLE"))
                    { a.IMPORT_STATUS_CD = "F"; a.IMPORT_STATUS_DESC += "Sample Collection Method is required when Activity Type contains the term -Sample-. "; }

                    //special validation requiring sampling collection equipment if activity type contains "Sample"
                    if (string.IsNullOrEmpty(a.SAMP_COLL_EQUIP) && a.ACT_TYPE.ToUpper().Contains("SAMPLE"))
                    { a.IMPORT_STATUS_CD = "F"; a.IMPORT_STATUS_DESC += "Sample Collection Equipment is required when Activity Type contains the term -Sample-. "; }


                    //sampling prep method handling
                    if (a.SAMP_PREP_IDX == null)
                    {
                        if (string.IsNullOrEmpty(a.SAMP_PREP_CTX) && !string.IsNullOrEmpty(a.SAMP_PREP_ID))
                            a.SAMP_PREP_CTX = oRG_ID;

                        if (!string.IsNullOrEmpty(a.SAMP_PREP_ID) && !string.IsNullOrEmpty(a.SAMP_PREP_CTX))
                        {
                            //see if matching prep method exists
                            T_WQX_REF_SAMP_PREP sp = db_Ref.GetT_WQX_REF_SAMP_PREP_ByIDandContext(a.SAMP_PREP_ID, a.SAMP_PREP_CTX);
                            if (sp != null)
                                a.SAMP_PREP_IDX = sp.SAMP_PREP_IDX;
                            else  //no matching sample prep method lookup found
                            { a.IMPORT_STATUS_CD = "F"; a.IMPORT_STATUS_DESC += "No matching Sample Prep Method found - please add it at the Reference Data screen first. ";  }
                        }
                    }
                    //********************** CUSTOM POST VALIDATION ********************************************


                    a.IMPORT_STATUS_DESC = a.IMPORT_STATUS_DESC.SubStringPlus(0, 200);

                    if (insInd) //insert case
                        ctx.AddToT_WQX_IMPORT_TEMP_SAMPLE(a);

                    ctx.SaveChanges();

                    return a.TEMP_SAMPLE_IDX;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static void WQX_IMPORT_TEMP_SAMPLE_GenVal(ref T_WQX_IMPORT_TEMP_SAMPLE a, List<ConfigInfoType> t, Dictionary<string, string> colVals, string f)
        {
            var _rules = t.Find(item => item._name == f);   //import validation rules for this field
            if (_rules == null)
                return;

            string _value = Utils.GetValueOrDefault(colVals, f); //supplied value for this field

            if (!string.IsNullOrEmpty(_value)) //if value is supplied
            {
                _value = _value.Trim();

                //if this field has another field which gets added to it (used for Date + Time fields)
                if (!string.IsNullOrEmpty(_rules._addfield)) 
                    _value = _value + " " + Utils.GetValueOrDefault(colVals, _rules._addfield);

                //strings: field length validation and substring 
                if (_rules._datatype == "" && _rules._length != null)
                {
                    if (_value.Length > _rules._length)
                    {
                        a.IMPORT_STATUS_CD = "F";
                        a.IMPORT_STATUS_DESC = (a.IMPORT_STATUS_DESC + f + " length (" + _rules._length + ") exceeded. ");

                        _value = _value.SubStringPlus(0, (int)_rules._length);
                    }
                }

                //integers: check type
                if (_rules._datatype == "int")
                {
                    int n;
                    if (int.TryParse(_value, out n) == false)
                    {
                        a.IMPORT_STATUS_CD = "F";
                        a.IMPORT_STATUS_DESC = (a.IMPORT_STATUS_DESC + f + " not numeric. ");
                    }
                }

                //datetime: check type
                if (_rules._datatype == "datetime")
                {
                    if (_value.ConvertOrDefault<DateTime>().Year < 1900)
                    {
                        if (_rules._req == "Y")
                            _value = new DateTime(1900, 1, 1).ToString();

                        a.IMPORT_STATUS_CD = "F";
                        a.IMPORT_STATUS_DESC = (a.IMPORT_STATUS_DESC + f + " not properly formatted. ");
                    }
                }


                //ref data lookup
                if (_rules._fkey.Length > 0)
                {
                    if (db_Ref.GetT_WQX_REF_DATA_ByKey(_rules._fkey, _value) == false)
                    {
                        a.IMPORT_STATUS_CD = "F";
                        a.IMPORT_STATUS_DESC = (a.IMPORT_STATUS_DESC + f + " not valid. ");
                    }
                }
            }
            else
            {
                //required check
                if (_rules._req == "Y")
                {
                    if (_rules._datatype == "")
                        _value = "-";
                    else if (_rules._datatype == "datetime")
                        _value = new DateTime(1900, 1, 1).ToString();
                    a.IMPORT_STATUS_CD = "F";
                    a.IMPORT_STATUS_DESC = (a.IMPORT_STATUS_DESC + "Required field " + f + " missing. ");
                }
            }

            //finally set the value before returning
            try
            {
                if (_rules._datatype == "")
                    typeof(T_WQX_IMPORT_TEMP_SAMPLE).GetProperty(f).SetValue(a, _value);
                else if (_rules._datatype == "int")
                    typeof(T_WQX_IMPORT_TEMP_SAMPLE).GetProperty(f).SetValue(a, _value.ConvertOrDefault<int?>());
                else if (_rules._datatype == "datetime" && _rules._req == "Y")
                    typeof(T_WQX_IMPORT_TEMP_SAMPLE).GetProperty(f).SetValue(a, _value.ConvertOrDefault<DateTime>());
                else if (_rules._datatype == "datetime" && _rules._req == "N")
                    typeof(T_WQX_IMPORT_TEMP_SAMPLE).GetProperty(f).SetValue(a, _value.ConvertOrDefault<DateTime?>());
            }
            catch { }
        }

        public static int InsertOrUpdateWQX_IMPORT_TEMP_SAMPLE_Status(int tEMP_SAMPLE_IDX, string sTATUS_CD, string sTATUS_DESC)
        {
            try
            {
                using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
                {
                    T_WQX_IMPORT_TEMP_SAMPLE a = 
                        (from c in ctx.T_WQX_IMPORT_TEMP_SAMPLE
                         where c.TEMP_SAMPLE_IDX == tEMP_SAMPLE_IDX
                         select c).FirstOrDefault();

                    a.IMPORT_STATUS_CD = sTATUS_CD;
                    a.IMPORT_STATUS_DESC = (a.IMPORT_STATUS_DESC + " " + sTATUS_DESC).SubStringPlus(0,100);

                    ctx.SaveChanges();

                    return tEMP_SAMPLE_IDX;
                }
            }
            catch (Exception ex)
            {
                sTATUS_CD = "F";
                sTATUS_DESC += "Unspecified error";
                return 0;
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
    
        public static int GetWQX_IMPORT_TEMP_SAMPLE_CountByUserID(string UserID)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_WQX_IMPORT_TEMP_SAMPLE
                            where a.USER_ID == UserID
                            select a).Count();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static int GetWQX_IMPORT_TEMP_SAMPLE_DupActivityIDs(string UserID, string OrgID)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from t in ctx.T_WQX_IMPORT_TEMP_SAMPLE
                            join a in ctx.T_WQX_ACTIVITY on t.ACTIVITY_ID equals a.ACTIVITY_ID
                            where t.USER_ID == UserID
                            && a.ORG_ID == OrgID
                            select a).Count();
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }

        public static int SP_ImportActivityFromTemp(string userID, string WQXInd, string activityReplacedInd)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return ctx.ImportActivityFromTemp(userID, WQXInd, activityReplacedInd);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }



        // *************************** IMPORT: RESULT ******************************
        // *****************************************************************************
        public static int InsertOrUpdateWQX_IMPORT_TEMP_RESULT(global::System.Int32? tEMP_RESULT_IDX, int tEMP_SAMPLE_IDX, global::System.Int32? rESULT_IDX, string dATA_LOGGER_LINE, 
            string rESULT_DETECT_CONDITION, global::System.String cHAR_NAME, string mETHOD_SPECIATION_NAME, string rESULT_SAMP_FRACTION, global::System.String rESULT_MSR, global::System.String rESULT_MSR_UNIT,
            string rESULT_MSR_QUAL, string rESULT_STATUS, string sTATISTIC_BASE_CODE, string rESULT_VALUE_TYPE, string wEIGHT_BASIS, string tIME_BASIS, string tEMP_BASIS, string pARTICAL_BASIS,
            string pRECISION_VALUE, string bIAS_VALUE, string cONFIDENCE_INTERVAL_VALUE, string uP_CONFIDENCE_LIMIT, string lOW_CONFIDENCE_LIMIT, string rESULT_COMMENT, string dEPTH_HEIGHT_MSR, 
            string dEPTH_HEIGHT_MSR_UNIT, string dEPTHALTITUDEREFPOINT, string bIO_INTENT_NAME, string bIO_INDIVIDUAL_ID, string bIO_SUBJECT_TAXONOMY, string bIO_UNIDENTIFIED_SPECIES_ID,
            string bIO_SAMPLE_TISSUE_ANATOMY, string gRP_SUMM_COUNT_WEIGHT_MSR, string gRP_SUMM_COUNT_WEIGHT_MSR_UNIT, string tAX_DTL_CELL_FORM, string tAX_DTL_CELL_SHAPE, string tAX_DTL_HABIT, 
            string tAX_DTL_VOLTINISM, string tAX_DTL_POLL_TOLERANCE, string tAX_DTL_POLL_TOLERANCE_SCALE, string tAX_DTL_TROPHIC_LEVEL, string tAX_DTL_FUNC_FEEDING_GROUP1,
            string tAX_DTL_FUNC_FEEDING_GROUP2, string tAX_DTL_FUNC_FEEDING_GROUP3, string fREQ_CLASS_CODE, string fREQ_CLASS_UNIT, string fREQ_CLASS_UPPER, string fREQ_CLASS_LOWER,
            int? aNALYTIC_METHOD_IDX, string aNALYTIC_METHOD_ID, string aNALYTIC_METHOD_CTX, string aNALYTIC_METHOD_NAME,
            int? lAB_IDX, string lAB_NAME, DateTime? lAB_ANALYSIS_START_DT, DateTime? lAB_ANALYSIS_END_DT, string lAB_ANALYSIS_TIMEZONE, string rESULT_LAB_COMMENT_CODE, 
            string mETHOD_DETECTION_LEVEL, string lAB_REPORTING_LEVEL, string pQL, string lOWER_QUANT_LIMIT, string uPPER_QUANT_LIMIT, string dETECTION_LIMIT_UNIT, int? lAB_SAMP_PREP_IDX, 
            string lAB_SAMP_PREP_ID, string lAB_SAMP_PREP_CTX, DateTime? lAB_SAMP_PREP_START_DT, DateTime? lAB_SAMP_PREP_END_DT, string dILUTION_FACTOR, string sTATUS_CD, string sTATUS_DESC, bool BioIndicator, string orgID, Boolean autoImportRefDataInd)
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

                    if (!string.IsNullOrEmpty(dATA_LOGGER_LINE))
                    {
                        a.DATA_LOGGER_LINE = dATA_LOGGER_LINE.Trim().SubStringPlus(0, 15);
                    }

                    if (rESULT_DETECT_CONDITION == "DNQ" || rESULT_MSR == "DNQ") { rESULT_DETECT_CONDITION = "Detected Not Quantified"; rESULT_MSR = "DNQ"; }
                    if (rESULT_DETECT_CONDITION == "ND" || rESULT_MSR == "ND") { rESULT_DETECT_CONDITION = "Not Detected"; rESULT_MSR = "ND"; }
                    if (rESULT_DETECT_CONDITION == "NR" || rESULT_MSR == "NR") { rESULT_DETECT_CONDITION = "Not Reported"; rESULT_MSR = "NR"; }
                    if (rESULT_DETECT_CONDITION == "PAQL" || rESULT_MSR == "PAQL") { rESULT_DETECT_CONDITION = "Present Above Quantification Limit"; rESULT_MSR = "PAQL"; }
                    if (rESULT_DETECT_CONDITION == "PBQL" || rESULT_MSR == "PBQL") { rESULT_DETECT_CONDITION = "Present Below Quantification Limit"; rESULT_MSR = "PBQL"; }

                    if (!string.IsNullOrEmpty(rESULT_DETECT_CONDITION))
                    {
                        a.RESULT_DETECT_CONDITION = rESULT_DETECT_CONDITION.Trim().SubStringPlus(0, 35);
                        if (db_Ref.GetT_WQX_REF_DATA_ByKey("ResultDetectionCondition", rESULT_DETECT_CONDITION.Trim()) == false) { sTATUS_CD = "F"; sTATUS_DESC += "Result Detection Condition not valid. "; }                            
                    }

                    if (!string.IsNullOrEmpty(cHAR_NAME))
                    {
                        a.CHAR_NAME = cHAR_NAME.Trim().SubStringPlus(0, 120);
                        if (db_Ref.GetT_WQX_REF_CHARACTERISTIC_ExistCheck(cHAR_NAME.Trim()) == false) { sTATUS_CD = "F"; sTATUS_DESC += "Characteristic Name not valid. "; }
                    }

                    if (!string.IsNullOrEmpty(mETHOD_SPECIATION_NAME))
                    {
                        a.METHOD_SPECIATION_NAME = mETHOD_SPECIATION_NAME.Trim().SubStringPlus(0, 20);
                        if (db_Ref.GetT_WQX_REF_DATA_ByKey("MethodSpeciation", mETHOD_SPECIATION_NAME.Trim()) == false) { sTATUS_CD = "F"; sTATUS_DESC += "Method Speciation not valid. "; }
                    }

                    if (!string.IsNullOrEmpty(rESULT_SAMP_FRACTION))
                    {
                        a.RESULT_SAMP_FRACTION = rESULT_SAMP_FRACTION.Trim().SubStringPlus(0, 25);
                        if (db_Ref.GetT_WQX_REF_DATA_ByKey("ResultSampleFraction", rESULT_SAMP_FRACTION.Trim()) == false) { sTATUS_CD = "F"; sTATUS_DESC += "Result Sample Fraction not valid. "; }
                    }
                    else
                    {
                        if (db_Ref.GetT_WQX_REF_CHARACTERISTIC_SampFracReqCheck(cHAR_NAME.Trim()) == true) { sTATUS_CD = "F"; sTATUS_DESC += "Sample Fraction must be reported."; }
                    }

                    if (!string.IsNullOrEmpty(rESULT_MSR))
                    {
                        a.RESULT_MSR = rESULT_MSR.Trim().SubStringPlus(0, 60).Replace(",","");
                    }
                    else
                    {
                        if ( string.IsNullOrEmpty(rESULT_DETECT_CONDITION) ) { sTATUS_CD = "F"; sTATUS_DESC += "Either Result Measure or Result Detection Condition must be reported."; }
                    }

                    if (!string.IsNullOrEmpty(rESULT_MSR_UNIT))
                    {
                        if (db_Ref.GetT_WQX_REF_DATA_ByKey("MeasureUnit", rESULT_MSR_UNIT.Trim()) == false) { sTATUS_CD = "F"; sTATUS_DESC += "Measurement Unit not valid. "; }
                        a.RESULT_MSR_UNIT = rESULT_MSR_UNIT.Trim().SubStringPlus(0, 12);
                    }

                    if (!string.IsNullOrEmpty(rESULT_MSR_QUAL))
                    {
                        if (db_Ref.GetT_WQX_REF_DATA_ByKey("ResultMeasureQualifier", rESULT_MSR_QUAL.Trim()) == false) { sTATUS_CD = "F"; sTATUS_DESC += "Measurement Qualifier not valid. "; }
                        a.RESULT_MSR_QUAL = rESULT_MSR_QUAL.Trim().SubStringPlus(0, 5);
                    }

                    if (!string.IsNullOrEmpty(rESULT_STATUS))
                    {
                        if (db_Ref.GetT_WQX_REF_DATA_ByKey("ResultStatus", rESULT_STATUS.Trim()) == false) { sTATUS_CD = "F"; sTATUS_DESC += "Result Status not valid. "; }
                        a.RESULT_STATUS = rESULT_STATUS.Trim().SubStringPlus(0, 12);
                    }

                    if (!string.IsNullOrEmpty(sTATISTIC_BASE_CODE))
                    {
                        if (db_Ref.GetT_WQX_REF_DATA_ByKey("StatisticalBase", sTATISTIC_BASE_CODE.Trim()) == false) { sTATUS_CD = "F"; sTATUS_DESC += "Statistic Base Code not valid. "; }
                        a.STATISTIC_BASE_CODE = sTATISTIC_BASE_CODE.Trim().SubStringPlus(0, 25);
                    }

                    if (!string.IsNullOrEmpty(rESULT_VALUE_TYPE))
                    {
                        if (db_Ref.GetT_WQX_REF_DATA_ByKey("ResultValueType", rESULT_VALUE_TYPE.Trim()) == false) { sTATUS_CD = "F"; sTATUS_DESC += "Result Value Type not valid. "; }
                        a.RESULT_VALUE_TYPE = rESULT_VALUE_TYPE.Trim().SubStringPlus(0, 20);
                    }

                    if (!string.IsNullOrEmpty(wEIGHT_BASIS))
                    {
                        if (db_Ref.GetT_WQX_REF_DATA_ByKey("ResultWeightBasis", wEIGHT_BASIS.Trim()) == false) { sTATUS_CD = "F"; sTATUS_DESC += "Weight Basis not valid. "; }
                        a.WEIGHT_BASIS = wEIGHT_BASIS.Trim().SubStringPlus(0, 15);
                    }

                    if (!string.IsNullOrEmpty(tIME_BASIS))
                    {
                        if (db_Ref.GetT_WQX_REF_DATA_ByKey("ResultTimeBasis", tIME_BASIS.Trim()) == false) { sTATUS_CD = "F"; sTATUS_DESC += "Time Basis not valid. "; }
                        a.TIME_BASIS = tIME_BASIS.Trim().SubStringPlus(0, 12);
                    }

                    if (!string.IsNullOrEmpty(tEMP_BASIS))
                    {
                        if (db_Ref.GetT_WQX_REF_DATA_ByKey("ResultTemperatureBasis", tEMP_BASIS.Trim()) == false) { sTATUS_CD = "F"; sTATUS_DESC += "Temp Basis not valid. "; }
                        a.TEMP_BASIS = tEMP_BASIS.Trim().SubStringPlus(0, 12);
                    }

                    if (!string.IsNullOrEmpty(pARTICAL_BASIS))
                        a.PARTICLESIZE_BASIS = pARTICAL_BASIS.Trim().SubStringPlus(0, 40);

                    if (!string.IsNullOrEmpty(pRECISION_VALUE))
                        a.PRECISION_VALUE = pRECISION_VALUE.Trim().SubStringPlus(0,60);
                    
                    if (!string.IsNullOrEmpty(bIAS_VALUE))
                        a.BIAS_VALUE = bIAS_VALUE.Trim().SubStringPlus(0,60);

                    if (!string.IsNullOrEmpty(cONFIDENCE_INTERVAL_VALUE))
                        a.CONFIDENCE_INTERVAL_VALUE = cONFIDENCE_INTERVAL_VALUE.Trim().SubStringPlus(0,15);

                    if (!string.IsNullOrEmpty(uP_CONFIDENCE_LIMIT))
                        a.UPPER_CONFIDENCE_LIMIT = uP_CONFIDENCE_LIMIT.Trim().SubStringPlus(0, 15);

                    if (!string.IsNullOrEmpty(lOW_CONFIDENCE_LIMIT))
                        a.LOWER_CONFIDENCE_LIMIT = lOW_CONFIDENCE_LIMIT.Trim().SubStringPlus(0, 15);

                    if (!string.IsNullOrEmpty(rESULT_COMMENT))
                        a.RESULT_COMMENT = rESULT_COMMENT.Trim().SubStringPlus(0,4000);

                    if (!string.IsNullOrEmpty(dEPTH_HEIGHT_MSR))
                        a.DEPTH_HEIGHT_MSR = dEPTH_HEIGHT_MSR.Trim().SubStringPlus(0, 12);

                    if (!string.IsNullOrEmpty(dEPTH_HEIGHT_MSR_UNIT))
                    {
                        if (db_Ref.GetT_WQX_REF_DATA_ByKey("MeasureUnit", dEPTH_HEIGHT_MSR_UNIT.Trim()) == false) { sTATUS_CD = "F"; sTATUS_DESC += "Result Depth Unit not valid. "; }
                        a.DEPTH_HEIGHT_MSR_UNIT = dEPTH_HEIGHT_MSR_UNIT.Trim().SubStringPlus(0, 12);
                    }

                    if (!string.IsNullOrEmpty(dEPTHALTITUDEREFPOINT))
                        a.DEPTHALTITUDEREFPOINT = dEPTHALTITUDEREFPOINT.Trim().SubStringPlus(0, 125);


                    if (BioIndicator == true)
                    {

                        if (!string.IsNullOrEmpty(bIO_INTENT_NAME))
                        {
                            if (db_Ref.GetT_WQX_REF_DATA_ByKey("BiologicalIntent", bIO_INTENT_NAME.Trim()) == false) { sTATUS_CD = "F"; sTATUS_DESC += "Biological Intent not valid. "; }
                            a.BIO_INTENT_NAME = bIO_INTENT_NAME.Trim().SubStringPlus(0, 35);

                            if (string.IsNullOrEmpty(bIO_SUBJECT_TAXONOMY)) { sTATUS_CD = "F"; sTATUS_DESC += "Taxonomy must be reported when intent is reported. "; }
                        }

                        if (!string.IsNullOrEmpty(bIO_INDIVIDUAL_ID))
                            a.BIO_INDIVIDUAL_ID = bIO_INDIVIDUAL_ID.Trim().SubStringPlus(0, 4);

                        if (!string.IsNullOrEmpty(bIO_SUBJECT_TAXONOMY))
                        {
                            if (db_Ref.GetT_WQX_REF_DATA_ByKey("Taxon", bIO_SUBJECT_TAXONOMY.Trim()) == false) { sTATUS_CD = "F"; sTATUS_DESC += "Subject Taxonomy not valid. "; }
                            a.BIO_SUBJECT_TAXONOMY = bIO_SUBJECT_TAXONOMY.Trim().SubStringPlus(0, 120);

                            if (string.IsNullOrEmpty(bIO_INTENT_NAME)) { sTATUS_CD = "F"; sTATUS_DESC += "Biological intent must be reported when taxonomy is reported. "; }
                        }

                        if (!string.IsNullOrEmpty(bIO_UNIDENTIFIED_SPECIES_ID))
                            a.BIO_UNIDENTIFIED_SPECIES_ID = bIO_UNIDENTIFIED_SPECIES_ID.Trim().SubStringPlus(0, 120);

                        if (!string.IsNullOrEmpty(bIO_SAMPLE_TISSUE_ANATOMY))
                        {
                            if (db_Ref.GetT_WQX_REF_DATA_ByKey("SampleTissueAnatomy", bIO_SAMPLE_TISSUE_ANATOMY.Trim()) == false) { sTATUS_CD = "F"; sTATUS_DESC += "Sample Tissue Anatomy not valid. "; }
                            a.BIO_SAMPLE_TISSUE_ANATOMY = bIO_SAMPLE_TISSUE_ANATOMY.Trim().SubStringPlus(0, 30);
                        }

                        if (!string.IsNullOrEmpty(gRP_SUMM_COUNT_WEIGHT_MSR))
                            a.GRP_SUMM_COUNT_WEIGHT_MSR = gRP_SUMM_COUNT_WEIGHT_MSR.Trim().SubStringPlus(0, 12);

                        if (!string.IsNullOrEmpty(gRP_SUMM_COUNT_WEIGHT_MSR_UNIT))
                        {
                            if (db_Ref.GetT_WQX_REF_DATA_ByKey("MeasureUnit", gRP_SUMM_COUNT_WEIGHT_MSR_UNIT.Trim()) == false) { sTATUS_CD = "F"; sTATUS_DESC += "Group Summary Unit not valid. "; }
                            a.GRP_SUMM_COUNT_WEIGHT_MSR_UNIT = gRP_SUMM_COUNT_WEIGHT_MSR_UNIT.Trim().SubStringPlus(0, 12);
                        }

                        if (!string.IsNullOrEmpty(tAX_DTL_CELL_FORM))
                        {
                            if (db_Ref.GetT_WQX_REF_DATA_ByKey("CellForm", tAX_DTL_CELL_FORM.Trim()) == false) { sTATUS_CD = "F"; sTATUS_DESC += "Cell Form not valid. "; }
                            a.TAX_DTL_CELL_FORM = tAX_DTL_CELL_FORM.Trim().SubStringPlus(0, 11);
                        }

                        if (!string.IsNullOrEmpty(tAX_DTL_CELL_SHAPE))
                        {
                            if (db_Ref.GetT_WQX_REF_DATA_ByKey("CellShape", tAX_DTL_CELL_SHAPE.Trim()) == false) { sTATUS_CD = "F"; sTATUS_DESC += "Cell Shape not valid. "; }
                            a.TAX_DTL_CELL_SHAPE = tAX_DTL_CELL_SHAPE.Trim().SubStringPlus(0, 18);
                        }

                        if (!string.IsNullOrEmpty(tAX_DTL_HABIT))
                        {
                            if (db_Ref.GetT_WQX_REF_DATA_ByKey("Habit", tAX_DTL_HABIT.Trim()) == false) { sTATUS_CD = "F"; sTATUS_DESC += "Habit not valid. "; }
                            a.TAX_DTL_HABIT = tAX_DTL_HABIT.Trim().SubStringPlus(0, 15);
                        }

                        if (!string.IsNullOrEmpty(tAX_DTL_VOLTINISM))
                        {
                            if (db_Ref.GetT_WQX_REF_DATA_ByKey("Voltinism", tAX_DTL_VOLTINISM.Trim()) == false) { sTATUS_CD = "F"; sTATUS_DESC += "Voltinism not valid. "; }
                            a.TAX_DTL_VOLTINISM = tAX_DTL_VOLTINISM.Trim().SubStringPlus(0, 25);
                        }


                        if (!string.IsNullOrEmpty(tAX_DTL_POLL_TOLERANCE))
                            a.TAX_DTL_POLL_TOLERANCE = tAX_DTL_POLL_TOLERANCE.Trim().SubStringPlus(0, 4);


                        if (!string.IsNullOrEmpty(tAX_DTL_POLL_TOLERANCE_SCALE))
                            a.TAX_DTL_POLL_TOLERANCE_SCALE = tAX_DTL_POLL_TOLERANCE_SCALE.Trim().SubStringPlus(0, 50);

                        if (!string.IsNullOrEmpty(tAX_DTL_TROPHIC_LEVEL))
                            a.TAX_DTL_TROPHIC_LEVEL = tAX_DTL_TROPHIC_LEVEL.Trim().SubStringPlus(0, 4);

                        if (!string.IsNullOrEmpty(tAX_DTL_FUNC_FEEDING_GROUP1))
                            a.TAX_DTL_FUNC_FEEDING_GROUP1 = tAX_DTL_FUNC_FEEDING_GROUP1.Trim().SubStringPlus(0, 6);

                        if (!string.IsNullOrEmpty(tAX_DTL_FUNC_FEEDING_GROUP2))
                            a.TAX_DTL_FUNC_FEEDING_GROUP2 = tAX_DTL_FUNC_FEEDING_GROUP2.Trim().SubStringPlus(0, 6);

                        if (!string.IsNullOrEmpty(tAX_DTL_FUNC_FEEDING_GROUP3))
                            a.TAX_DTL_FUNC_FEEDING_GROUP3 = tAX_DTL_FUNC_FEEDING_GROUP3.Trim().SubStringPlus(0, 6);

                        if (!string.IsNullOrEmpty(fREQ_CLASS_CODE))
                            a.FREQ_CLASS_CODE = fREQ_CLASS_CODE.Trim().SubStringPlus(0, 50);

                        if (!string.IsNullOrEmpty(fREQ_CLASS_UNIT))
                            a.FREQ_CLASS_UNIT = fREQ_CLASS_UNIT.Trim().SubStringPlus(0, 12);

                        if (!string.IsNullOrEmpty(fREQ_CLASS_CODE))
                            a.FREQ_CLASS_UPPER = fREQ_CLASS_UPPER.Trim().SubStringPlus(0, 8);

                        if (!string.IsNullOrEmpty(fREQ_CLASS_CODE))
                            a.FREQ_CLASS_LOWER = fREQ_CLASS_LOWER.Trim().SubStringPlus(0, 8);

                    }                
                    
                    
                    //analysis method
                    //first populate the IDX if it is supplied
                    if (aNALYTIC_METHOD_IDX != null)
                        a.ANALYTIC_METHOD_IDX = aNALYTIC_METHOD_IDX;
                    else
                    {
                        //if ID is supplied but Context is not, set context to org id 
                        if (!string.IsNullOrEmpty(aNALYTIC_METHOD_ID) && string.IsNullOrEmpty(aNALYTIC_METHOD_CTX))
                            aNALYTIC_METHOD_CTX = orgID;

                        //if we now have values for the ID and context
                        if (!string.IsNullOrEmpty(aNALYTIC_METHOD_ID) && !string.IsNullOrEmpty(aNALYTIC_METHOD_CTX))
                        {
                            //see if matching collection method exists
                            T_WQX_REF_ANAL_METHOD am = db_Ref.GetT_WQX_REF_ANAL_METHODByIDandContext(aNALYTIC_METHOD_ID.Trim(), aNALYTIC_METHOD_CTX.Trim());
                            if (am != null)
                                a.ANALYTIC_METHOD_IDX = am.ANALYTIC_METHOD_IDX;
                            else  //no matching anal method lookup found                            
                            {
                                if (autoImportRefDataInd == true)
                                {
                                    db_Ref.InsertOrUpdateT_WQX_REF_ANAL_METHOD(null, aNALYTIC_METHOD_ID.Trim(), aNALYTIC_METHOD_CTX.Trim(), aNALYTIC_METHOD_NAME.Trim(), "", true);
                                }
                                else
                                { sTATUS_CD = "F"; sTATUS_DESC += "No matching Analysis Method found - please add it at the Reference Data screen first. "; }
                            }

                            //****************************************
                            a.ANALYTIC_METHOD_ID = aNALYTIC_METHOD_ID.Trim().SubStringPlus(0, 20);
                            a.ANALYTIC_METHOD_CTX = aNALYTIC_METHOD_CTX.Trim().SubStringPlus(0, 120);

                            if (!string.IsNullOrEmpty(aNALYTIC_METHOD_NAME))
                                a.ANALYTIC_METHOD_NAME = aNALYTIC_METHOD_NAME.Trim().SubStringPlus(0, 120);
                        }
                        else
                        {
                            //if IDX, ID, and Context not supplied, lookup the method from the default Org Char reference list
                            T_WQX_REF_CHAR_ORG rco = db_Ref.GetT_WQX_REF_CHAR_ORGByName(orgID, cHAR_NAME.Trim().SubStringPlus(0, 120));
                            if (rco != null)
                            {
                                a.ANALYTIC_METHOD_IDX = rco.DEFAULT_ANAL_METHOD_IDX;
                                if (rco.DEFAULT_ANAL_METHOD_IDX != null)
                                {
                                    T_WQX_REF_ANAL_METHOD anal = db_Ref.GetT_WQX_REF_ANAL_METHODByIDX(rco.DEFAULT_ANAL_METHOD_IDX.ConvertOrDefault<int>());
                                    if (anal != null)
                                    {
                                        a.ANALYTIC_METHOD_ID = anal.ANALYTIC_METHOD_ID;
                                        a.ANALYTIC_METHOD_NAME = anal.ANALYTIC_METHOD_NAME;
                                        a.ANALYTIC_METHOD_CTX = anal.ANALYTIC_METHOD_CTX;
                                    }
                                }
                            }

                        }
                    }

                    //********** LABORATORY **********
                    if (lAB_IDX != null)
                        a.LAB_IDX = lAB_IDX;
                    else
                    {
                        if (!string.IsNullOrEmpty(lAB_NAME))
                        {
                             a.LAB_NAME = lAB_NAME;

                            //see if matching lab name exists for this org
                            T_WQX_REF_LAB lab = db_Ref.GetT_WQX_REF_LAB_ByIDandContext(lAB_NAME, orgID);
                            if (lab == null) {
                                if (autoImportRefDataInd == true)
                                {
                                    db_Ref.InsertOrUpdateT_WQX_REF_LAB(null, lAB_NAME.Trim(), null, null, orgID, true);
                                }
                                else { sTATUS_CD = "F"; sTATUS_DESC += "No matching Lab Name found - please add it at the Reference Data screen first. "; }
                            }
                            else
                                a.LAB_IDX = lab.LAB_IDX;
                        }
                    }


                    if (lAB_ANALYSIS_START_DT != null)
                    {
                        //fix improperly formatted datetime
                        if (lAB_ANALYSIS_START_DT.ConvertOrDefault<DateTime>().Year < 1900)
                            lAB_ANALYSIS_START_DT = null;

                        a.LAB_ANALYSIS_START_DT = lAB_ANALYSIS_START_DT;
                    }
                    if (lAB_ANALYSIS_END_DT != null)
                    {
                        //fix improperly formatted datetime
                        if (lAB_ANALYSIS_END_DT.ConvertOrDefault<DateTime>().Year < 1900)
                            lAB_ANALYSIS_END_DT = null;

                        a.LAB_ANALYSIS_END_DT = lAB_ANALYSIS_END_DT;
                    }


                    if (!string.IsNullOrEmpty(lAB_ANALYSIS_TIMEZONE))
                    {
                        a.LAB_ANALYSIS_TIMEZONE = lAB_ANALYSIS_TIMEZONE.Trim().SubStringPlus(0, 4);
                        if (db_Ref.GetT_WQX_REF_DATA_ByKey("TimeZone", lAB_ANALYSIS_TIMEZONE.Trim()) == false) { sTATUS_CD = "F"; sTATUS_DESC += "TimeZone not valid. "; }
                    }
                    else
                    {
                        //put in Timezone if missing
                        if (lAB_ANALYSIS_START_DT != null || lAB_ANALYSIS_END_DT != null)
                            a.LAB_ANALYSIS_TIMEZONE = Utils.GetWQXTimeZoneByDate(a.LAB_ANALYSIS_TIMEZONE.ConvertOrDefault<DateTime>());
                    }

                    if (!string.IsNullOrEmpty(rESULT_LAB_COMMENT_CODE))
                        a.RESULT_LAB_COMMENT_CODE = rESULT_LAB_COMMENT_CODE.Trim().SubStringPlus(0, 3);

                    if (!string.IsNullOrEmpty(mETHOD_DETECTION_LEVEL))
                        a.METHOD_DETECTION_LEVEL = mETHOD_DETECTION_LEVEL.Trim().SubStringPlus(0, 12);

                    if (!string.IsNullOrEmpty(lAB_REPORTING_LEVEL))
                        a.LAB_REPORTING_LEVEL = lAB_REPORTING_LEVEL.Trim().SubStringPlus(0, 12);

                    if (!string.IsNullOrEmpty(pQL))
                        a.PQL = pQL.Trim().SubStringPlus(0, 12);

                    if (!string.IsNullOrEmpty(lOWER_QUANT_LIMIT))
                        a.LOWER_QUANT_LIMIT = lOWER_QUANT_LIMIT.Trim().SubStringPlus(0, 12);

                    //if result is PBQL, but no value has been reported for MDL, LRL, PQL, or Lower Quant Limit, then grab from Org Char default
                    if (rESULT_DETECT_CONDITION == "Present Below Quantification Limit" && string.IsNullOrEmpty(mETHOD_DETECTION_LEVEL) && string.IsNullOrEmpty(lAB_REPORTING_LEVEL) && string.IsNullOrEmpty(pQL) && string.IsNullOrEmpty(lOWER_QUANT_LIMIT))
                    {
                        T_WQX_REF_CHAR_ORG rco = db_Ref.GetT_WQX_REF_CHAR_ORGByName(orgID, cHAR_NAME);
                        if (rco != null)
                            a.LOWER_QUANT_LIMIT = rco.DEFAULT_LOWER_QUANT_LIMIT;

                        //if still null, then error
                        if (a.LOWER_QUANT_LIMIT == null)
                            { sTATUS_CD = "F"; sTATUS_DESC += "No Lower Quantification limit reported or default value specified. "; }
                    }

                    if (!string.IsNullOrEmpty(uPPER_QUANT_LIMIT))
                        a.UPPER_QUANT_LIMIT = uPPER_QUANT_LIMIT.Trim().SubStringPlus(0, 12);

                    //if result is PAQL, but no value has been reported for MDL, LRL, PQL, or Lower Quant Limit, then grab from Org Char default
                    if (rESULT_DETECT_CONDITION == "Present Above Quantification Limit" && string.IsNullOrEmpty(mETHOD_DETECTION_LEVEL) && string.IsNullOrEmpty(lAB_REPORTING_LEVEL) && string.IsNullOrEmpty(pQL) && string.IsNullOrEmpty(uPPER_QUANT_LIMIT))
                    {
                        T_WQX_REF_CHAR_ORG rco = db_Ref.GetT_WQX_REF_CHAR_ORGByName(orgID, cHAR_NAME);
                        if (rco != null)
                            a.UPPER_QUANT_LIMIT = rco.DEFAULT_UPPER_QUANT_LIMIT;

                        //if still null, then error
                        if (a.UPPER_QUANT_LIMIT == null)
                        { sTATUS_CD = "F"; sTATUS_DESC += "No Upper Quantification limit reported. "; }
                    }

                    if (!string.IsNullOrEmpty(dETECTION_LIMIT_UNIT))
                    {
                        if (db_Ref.GetT_WQX_REF_DATA_ByKey("MeasureUnit", dETECTION_LIMIT_UNIT.Trim()) == false) { sTATUS_CD = "F"; sTATUS_DESC += "Detection Level Unit not valid. "; }
                        a.DETECTION_LIMIT_UNIT = dETECTION_LIMIT_UNIT.Trim().SubStringPlus(0, 12);
                    }


                    //********** LAB SAMPLE PREP
                    if (lAB_SAMP_PREP_IDX != null)
                        a.LAB_SAMP_PREP_IDX = lAB_SAMP_PREP_IDX;
                    else
                    {
                        if (!string.IsNullOrEmpty(lAB_SAMP_PREP_ID))
                        {
                            //set context to org id if none is provided 
                            if (string.IsNullOrEmpty(lAB_SAMP_PREP_CTX))
                                lAB_SAMP_PREP_CTX = orgID;

                            a.LAB_SAMP_PREP_ID = lAB_SAMP_PREP_ID.Trim().SubStringPlus(0, 20);
                            a.LAB_SAMP_PREP_CTX = lAB_SAMP_PREP_CTX.Trim().SubStringPlus(0, 120);

                            //see if matching lab prep method exists for this org
                            T_WQX_REF_SAMP_PREP ppp = db_Ref.GetT_WQX_REF_SAMP_PREP_ByIDandContext(lAB_SAMP_PREP_ID, lAB_SAMP_PREP_CTX);
                            if (ppp == null) //no match found
                            {                                
                                if (autoImportRefDataInd == true)
                                {
                                    db_Ref.InsertOrUpdateT_WQX_REF_SAMP_PREP(null, lAB_SAMP_PREP_ID.Trim(), lAB_SAMP_PREP_CTX.Trim(), lAB_SAMP_PREP_ID.Trim(), "", true);
                                }
                                else
                                { sTATUS_CD = "F"; sTATUS_DESC += "No matching Lab Sample Prep ID found - please add it at the Reference Data screen first. "; }
                            }
                            else  //match found
                                a.LAB_SAMP_PREP_IDX = ppp.SAMP_PREP_IDX;

                        }
                    }

                    if (lAB_SAMP_PREP_START_DT != null)
                    {
                        //fix improperly formatted datetime
                        if (lAB_SAMP_PREP_START_DT.ConvertOrDefault<DateTime>().Year < 1900)
                            lAB_SAMP_PREP_START_DT = null;

                        a.LAB_SAMP_PREP_START_DT = lAB_SAMP_PREP_START_DT;
                    }

                    if (lAB_SAMP_PREP_END_DT != null)
                    {
                        //fix improperly formatted datetime
                        if (lAB_SAMP_PREP_END_DT.ConvertOrDefault<DateTime>().Year < 1900)
                            lAB_SAMP_PREP_END_DT = null;

                        a.LAB_SAMP_PREP_END_DT = lAB_SAMP_PREP_END_DT;
                    }

                    if (!string.IsNullOrEmpty(dILUTION_FACTOR))
                        a.DILUTION_FACTOR = dILUTION_FACTOR.Trim().SubStringPlus(0, 12);


                    if (sTATUS_CD != null) a.IMPORT_STATUS_CD = sTATUS_CD;
                    if (sTATUS_DESC != null) a.IMPORT_STATUS_DESC = sTATUS_DESC.SubStringPlus(0,100);

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

        public static int InsertWQX_IMPORT_TEMP_RESULT_New(int tEMP_SAMPLE_IDX, Dictionary<string, string> colVals, string orgID)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    T_WQX_IMPORT_TEMP_RESULT a = new T_WQX_IMPORT_TEMP_RESULT();

                    a.TEMP_SAMPLE_IDX = tEMP_SAMPLE_IDX;
                    a.IMPORT_STATUS_CD = "P";
                    a.IMPORT_STATUS_DESC = "";

                    //get import config rules
                    List<ConfigInfoType> _allRules = Utils.GetAllColumnInfo("S");

                    //******************* PRE VALIDATION *************************************
                    //special rule: set values of ND, etc
                    string _rdc = Utils.GetValueOrDefault(colVals, "RESULT_DETECT_CONDITION"); 
                    string _res = Utils.GetValueOrDefault(colVals, "RESULT_MSR"); 
                    if (_rdc == "DNQ" || _res == "DNQ") { colVals["RESULT_DETECT_CONDITION"] = "Detected Not Quantified"; colVals["RESULT_MSR"] = "DNQ"; }
                    if (_rdc == "ND" || _res == "ND") { colVals["RESULT_DETECT_CONDITION"] = "Not Detected"; colVals["RESULT_MSR"] = "ND"; }
                    if (_rdc == "NR" || _res == "NR") { colVals["RESULT_DETECT_CONDITION"] = "Not Reported"; colVals["RESULT_MSR"] = "NR"; }
                    if (_rdc == "PAQL" || _res == "PAQL") { colVals["RESULT_DETECT_CONDITION"] = "Present Above Quantification Limit"; colVals["RESULT_MSR"] = "PAQL"; }
                    if (_rdc == "PBQL" || _res == "PBQL") { colVals["RESULT_DETECT_CONDITION"] = "Present Below Quantification Limit"; colVals["RESULT_MSR"] = "PBQL"; }
                    // ******************* END PRE VALIDATION *********************************


                    //loop through all optional fields
                    List<string> rFields = new List<string>(new string[] { "DATA_LOGGER_LINE","RESULT_DETECT_CONDITION","CHAR_NAME", "METHOD_SPECIATION_NAME",
                        "RESULT_SAMP_FRACTION", "RESULT_MSR","RESULT_MSR_UNIT","RESULT_MSR_QUAL","RESULT_STATUS","STATISTIC_BASE_CODE","RESULT_VALUE_TYPE","WEIGHT_BASIS",
                        "TIME_BASIS","TEMP_BASIS","PARTICLESIZE_BASIS","PRECISION_VALUE","BIAS_VALUE","CONFIDENCE_INTERVAL_VALUE","UPPER_CONFIDENCE_LIMIT","LOWER_CONFIDENCE_LIMIT",
                            "RESULT_COMMENT","DEPTH_HEIGHT_MSR","DEPTH_HEIGHT_MSR_UNIT","DEPTHALTITUDEREFPOINT","BIO_INTENT_NAME","BIO_INDIVIDUAL_ID","BIO_SUBJECT_TAXONOMY",
                            "BIO_UNIDENTIFIED_SPECIES_ID","BIO_SAMPLE_TISSUE_ANATOMY","GRP_SUMM_COUNT_WEIGHT_MSR","GRP_SUMM_COUNT_WEIGHT_MSR_UNIT","TAX_DTL_CELL_FORM",
                            "TAX_DTL_CELL_SHAPE","TAX_DTL_HABIT","TAX_DTL_VOLTINISM","TAX_DTL_POLL_TOLERANCE","TAX_DTL_POLL_TOLERANCE_SCALE","TAX_DTL_TROPHIC_LEVEL",
                            "TAX_DTL_FUNC_FEEDING_GROUP1","TAX_DTL_FUNC_FEEDING_GROUP2","TAX_DTL_FUNC_FEEDING_GROUP3","FREQ_CLASS_CODE","FREQ_CLASS_UNIT","FREQ_CLASS_UPPER",
                            "FREQ_CLASS_LOWER","ANALYTIC_METHOD_IDX","ANALYTIC_METHOD_ID","ANALYTIC_METHOD_CTX","LAB_NAME","LAB_ANALYSIS_START_DT","LAB_ANALYSIS_END_DT",
                            "RESULT_LAB_COMMENT_CODE","METHOD_DETECTION_LEVEL","LAB_REPORTING_LEVEL","PQL","LOWER_QUANT_LIMIT","UPPER_QUANT_LIMIT","DETECTION_LIMIT_UNIT",
                            "LAB_SAMP_PREP_IDX","LAB_SAMP_PREP_ID","LAB_SAMP_PREP_CTX","LAB_SAMP_PREP_START_DT","LAB_SAMP_PREP_END_DT","DILUTION_FACTOR" });

                    foreach (KeyValuePair<string, string> entry in colVals)
                        if (rFields.Contains(entry.Key))
                            WQX_IMPORT_TEMP_RESULT_GenVal(ref a, _allRules, colVals, entry.Key);

                    if (!string.IsNullOrEmpty(a.CHAR_NAME))
                        if (db_Ref.GetT_WQX_REF_CHARACTERISTIC_ExistCheck(a.CHAR_NAME) == false) { a.IMPORT_STATUS_CD = "F"; a.IMPORT_STATUS_DESC += "Characteristic Name not valid. "; }

                    if (string.IsNullOrEmpty(a.RESULT_SAMP_FRACTION))
                        if (db_Ref.GetT_WQX_REF_CHARACTERISTIC_SampFracReqCheck(a.CHAR_NAME) == true) { a.IMPORT_STATUS_CD = "F"; a.IMPORT_STATUS_DESC += "Sample Fraction must be reported."; }

                    if (!string.IsNullOrEmpty(a.RESULT_MSR))
                        a.RESULT_MSR = a.RESULT_MSR.Replace(",", "");
                    else
                        if (string.IsNullOrEmpty(a.RESULT_DETECT_CONDITION))
                            { a.IMPORT_STATUS_CD = "F"; a.IMPORT_STATUS_DESC += "Either Result Measure or Result Detection Condition must be reported."; }

                    //if result is PBQL, but no value has been reported for MDL, LRL, PQL, or Lower Quant Limit, then grab from Org Char default
                    if (a.RESULT_DETECT_CONDITION == "Present Below Quantification Limit" && string.IsNullOrEmpty(a.METHOD_DETECTION_LEVEL) && string.IsNullOrEmpty(a.LAB_REPORTING_LEVEL) && string.IsNullOrEmpty(a.PQL) && string.IsNullOrEmpty(a.LOWER_QUANT_LIMIT))
                    {
                        T_WQX_REF_CHAR_ORG rco = db_Ref.GetT_WQX_REF_CHAR_ORGByName(orgID, a.CHAR_NAME);
                        if (rco != null)
                            a.LOWER_QUANT_LIMIT = rco.DEFAULT_LOWER_QUANT_LIMIT;

                        //if still null, then error
                        if (a.LOWER_QUANT_LIMIT == null)
                        { a.IMPORT_STATUS_CD = "F"; a.IMPORT_STATUS_DESC += "No Lower Quantification limit reported or default value specified. "; }
                    }

                    if (string.IsNullOrEmpty(a.BIO_INTENT_NAME) != string.IsNullOrEmpty(a.BIO_SUBJECT_TAXONOMY))
                        if (string.IsNullOrEmpty(a.BIO_SUBJECT_TAXONOMY)) { a.IMPORT_STATUS_CD = "F"; a.IMPORT_STATUS_DESC += "Taxonomy must be reported when bio intent is reported. "; }


                    //analysis method
                    if (a.ANALYTIC_METHOD_IDX == null)
                    {
                        //if ID is supplied but Context is not, set context to org id 
                        if (!string.IsNullOrEmpty(a.ANALYTIC_METHOD_ID) && string.IsNullOrEmpty(a.ANALYTIC_METHOD_CTX))
                            a.ANALYTIC_METHOD_CTX = orgID;

                        //if we now have values for the ID and context
                        if (!string.IsNullOrEmpty(a.ANALYTIC_METHOD_ID) && !string.IsNullOrEmpty(a.ANALYTIC_METHOD_CTX))
                        {
                            //see if matching collection method exists
                            T_WQX_REF_ANAL_METHOD am = db_Ref.GetT_WQX_REF_ANAL_METHODByIDandContext(a.ANALYTIC_METHOD_ID, a.ANALYTIC_METHOD_CTX);
                            if (am != null)
                                a.ANALYTIC_METHOD_IDX = am.ANALYTIC_METHOD_IDX;
                            else  //no matching anal method lookup found                            
                                { a.IMPORT_STATUS_CD = "F"; a.IMPORT_STATUS_DESC += "No matching Analysis Method found - please add it at the Reference Data screen first. "; }
                        }
                        else
                        {
                            //if IDX, ID, and Context not supplied, lookup the method from the default Org Char reference list
                            T_WQX_REF_CHAR_ORG rco = db_Ref.GetT_WQX_REF_CHAR_ORGByName(orgID, a.CHAR_NAME);
                            if (rco != null)
                            {
                                a.ANALYTIC_METHOD_IDX = rco.DEFAULT_ANAL_METHOD_IDX;
                                if (rco.DEFAULT_ANAL_METHOD_IDX != null)
                                {
                                    T_WQX_REF_ANAL_METHOD anal = db_Ref.GetT_WQX_REF_ANAL_METHODByIDX(rco.DEFAULT_ANAL_METHOD_IDX.ConvertOrDefault<int>());
                                    if (anal != null)
                                    {
                                        a.ANALYTIC_METHOD_ID = anal.ANALYTIC_METHOD_ID;
                                        a.ANALYTIC_METHOD_NAME = anal.ANALYTIC_METHOD_NAME;
                                        a.ANALYTIC_METHOD_CTX = anal.ANALYTIC_METHOD_CTX;
                                    }
                                }
                            }
                        }
                    }



                    if (!string.IsNullOrEmpty(a.LAB_NAME))
                    {
                        T_WQX_REF_LAB lab = db_Ref.GetT_WQX_REF_LAB_ByIDandContext(a.LAB_NAME, orgID);
                        if (lab != null)
                            a.LAB_IDX = lab.LAB_IDX;
                        else
                        { a.IMPORT_STATUS_CD = "F"; a.IMPORT_STATUS_DESC += "No matching Lab Name found - please add it at the Reference Data screen first. "; }
                    }


                    //if result is PAQL, but no value has been reported for MDL, LRL, PQL, or Lower Quant Limit, then grab from Org Char default
                    if (a.RESULT_DETECT_CONDITION == "Present Above Quantification Limit" && string.IsNullOrEmpty(a.METHOD_DETECTION_LEVEL) && string.IsNullOrEmpty(a.LAB_REPORTING_LEVEL) && string.IsNullOrEmpty(a.PQL) && string.IsNullOrEmpty(a.UPPER_QUANT_LIMIT))
                    {
                        T_WQX_REF_CHAR_ORG rco = db_Ref.GetT_WQX_REF_CHAR_ORGByName(orgID, a.CHAR_NAME);
                        if (rco != null)
                            a.UPPER_QUANT_LIMIT = rco.DEFAULT_UPPER_QUANT_LIMIT;

                        //if still null, then error
                        if (a.UPPER_QUANT_LIMIT == null)
                        { a.IMPORT_STATUS_CD = "F"; a.IMPORT_STATUS_DESC += "No Upper Quantification limit reported. "; }
                    }

                    //put in Timezone if missing
                    if (a.LAB_ANALYSIS_START_DT != null || a.LAB_ANALYSIS_END_DT != null)
                        a.LAB_ANALYSIS_TIMEZONE = Utils.GetWQXTimeZoneByDate(a.LAB_ANALYSIS_START_DT.ConvertOrDefault<DateTime>());


                    //********** LAB SAMPLE PREP*************************
                    if (a.LAB_SAMP_PREP_IDX == null && !string.IsNullOrEmpty(a.LAB_SAMP_PREP_ID)) 
                    {
                        //set context to org id if none is provided 
                        if (string.IsNullOrEmpty(a.LAB_SAMP_PREP_CTX))
                            a.LAB_SAMP_PREP_CTX = orgID;

                        //see if matching lab prep method exists for this org
                        T_WQX_REF_SAMP_PREP ppp = db_Ref.GetT_WQX_REF_SAMP_PREP_ByIDandContext(a.LAB_SAMP_PREP_ID, a.LAB_SAMP_PREP_CTX);
                        if (ppp != null)
                            a.LAB_SAMP_PREP_IDX = ppp.SAMP_PREP_IDX;
                        else
                        { a.IMPORT_STATUS_CD = "F"; a.IMPORT_STATUS_DESC += "No matching Lab Sample Prep ID found - please add it at the Reference Data screen first. ";  }
                    }


                    a.IMPORT_STATUS_DESC = a.IMPORT_STATUS_DESC.SubStringPlus(0, 200);
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

        public static void WQX_IMPORT_TEMP_RESULT_GenVal(ref T_WQX_IMPORT_TEMP_RESULT a, List<ConfigInfoType> t, Dictionary<string, string> colVals, string f)
        {
            var _rules = t.Find(item => item._name == f);   //import validation rules for this field
            if (_rules == null)
                return;

            string _value = Utils.GetValueOrDefault(colVals, f); //supplied value for this field

            if (!string.IsNullOrEmpty(_value)) //if value is supplied
            {
                _value = _value.Trim();

                //if this field has another field which gets added to it (used for Date + Time fields)
                if (!string.IsNullOrEmpty(_rules._addfield))
                    _value = _value + " " + Utils.GetValueOrDefault(colVals, _rules._addfield);

                //strings: field length validation and substring 
                if (_rules._datatype == "" && _rules._length != null)
                {
                    if (_value.Length > _rules._length)
                    {
                        a.IMPORT_STATUS_CD = "F";
                        a.IMPORT_STATUS_DESC = (a.IMPORT_STATUS_DESC + f + " length (" + _rules._length + ") exceeded. ");

                        _value = _value.SubStringPlus(0, (int)_rules._length);
                    }
                }

                //integers: check type
                if (_rules._datatype == "int")
                {
                    int n;
                    if (int.TryParse(_value, out n) == false)
                    {
                        a.IMPORT_STATUS_CD = "F";
                        a.IMPORT_STATUS_DESC = (a.IMPORT_STATUS_DESC + f + " not numeric. ");
                    }
                }

                //datetime: check type
                if (_rules._datatype == "datetime")
                {
                    if (_value.ConvertOrDefault<DateTime>().Year < 1900)
                    {
                        a.IMPORT_STATUS_CD = "F";
                        a.IMPORT_STATUS_DESC = (a.IMPORT_STATUS_DESC + f + " not properly formatted. ");
                    }
                }


                //ref data lookup
                if (_rules._fkey.Length > 0)
                {
                    if (db_Ref.GetT_WQX_REF_DATA_ByKey(_rules._fkey, _value) == false)
                    {
                        a.IMPORT_STATUS_CD = "F";
                        a.IMPORT_STATUS_DESC = (a.IMPORT_STATUS_DESC + f + " not valid. ");
                    }
                }
            }
            else
            {
                //required check
                if (_rules._req == "Y")
                {
                    if (_rules._datatype == "")
                        _value = "-";
                    else if (_rules._datatype == "datetime")
                        _value = new DateTime(1900, 1, 1).ToString();
                    a.IMPORT_STATUS_CD = "F";
                    a.IMPORT_STATUS_DESC = (a.IMPORT_STATUS_DESC + "Required field " + f + " missing. ");
                }
            }

            //finally set the value before returning
            try
            {

                if (_rules._datatype == "")
                    typeof(T_WQX_IMPORT_TEMP_RESULT).GetProperty(f).SetValue(a, _value);
                else if (_rules._datatype == "int")
                    typeof(T_WQX_IMPORT_TEMP_RESULT).GetProperty(f).SetValue(a, _value.ConvertOrDefault<int?>());
                else if (_rules._datatype == "datetime" && _rules._req == "Y")
                    typeof(T_WQX_IMPORT_TEMP_RESULT).GetProperty(f).SetValue(a, _value.ConvertOrDefault<DateTime>());
                else if (_rules._datatype == "datetime" && _rules._req == "N")
                    typeof(T_WQX_IMPORT_TEMP_RESULT).GetProperty(f).SetValue(a, _value.ConvertOrDefault<DateTime?>());
            }
            catch { }
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
                            join b in ctx.T_WQX_IMPORT_TEMP_RESULT on a.TEMP_SAMPLE_IDX equals b.TEMP_SAMPLE_IDX into tjoin
                            where a.USER_ID == UserID
                            orderby a.ACTIVITY_IDX
                            from b in tjoin.DefaultIfEmpty()
                            select new ImportSampleResultDisplay {
                                TEMP_SAMPLE_IDX = a.TEMP_SAMPLE_IDX,
                                ORG_ID = a.ORG_ID, 
                                PROJECT_ID = a.PROJECT_ID,
                                MONLOC_ID = a.MONLOC_ID, 
                                ACTIVITY_ID = a.ACTIVITY_ID,
                                ACT_TYPE = a.ACT_TYPE,
                                ACT_MEDIA = a.ACT_MEDIA,
                                ACT_SUBMEDIA = a.ACT_SUBMEDIA,
                                ACT_START_DT = a.ACT_START_DT,
                                ACT_END_DT = a.ACT_END_DT,
                                ACT_TIME_ZONE = a.ACT_TIME_ZONE,
                                RELATIVE_DEPTH_NAME = a.RELATIVE_DEPTH_NAME,
                                ACT_DEPTHHEIGHT_MSR = a.ACT_DEPTHHEIGHT_MSR,
                                ACT_DEPTHHEIGHT_MSR_UNIT = a.ACT_DEPTHHEIGHT_MSR_UNIT,
                                TOP_DEPTHHEIGHT_MSR = a.TOP_DEPTHHEIGHT_MSR,
                                TOP_DEPTHHEIGHT_MSR_UNIT = a.TOP_DEPTHHEIGHT_MSR_UNIT,
                                BOT_DEPTHHEIGHT_MSR = a.BOT_DEPTHHEIGHT_MSR,
                                BOT_DEPTHHEIGHT_MSR_UNIT = a.BOT_DEPTHHEIGHT_MSR_UNIT,
                                DEPTH_REF_POINT = a.DEPTH_REF_POINT,
                                ACT_COMMENT = a.ACT_COMMENT,
                                BIO_ASSEMBLAGE_SAMPLED = a.BIO_ASSEMBLAGE_SAMPLED,
                                BIO_DURATION_MSR = a.BIO_DURATION_MSR,
                                BIO_DURATION_MSR_UNIT = a.BIO_DURATION_MSR_UNIT,
                                BIO_SAMP_COMPONENT = a.BIO_SAMP_COMPONENT,
                                BIO_SAMP_COMPONENT_SEQ = a.BIO_SAMP_COMPONENT_SEQ,
                                SAMP_COLL_METHOD_ID = a.SAMP_COLL_METHOD_ID,
                                SAMP_COLL_METHOD_CTX = a.SAMP_COLL_METHOD_CTX,
                                SAMP_COLL_EQUIP = a.SAMP_COLL_EQUIP,
                                SAMP_COLL_EQUIP_COMMENT = a.SAMP_COLL_EQUIP_COMMENT,
                                SAMP_PREP_ID = a.SAMP_PREP_ID,
                                SAMP_PREP_CTX = a.SAMP_PREP_CTX,
                                TEMP_RESULT_IDX = b.TEMP_RESULT_IDX,
                                DATA_LOGGER_LINE = b.DATA_LOGGER_LINE,
                                RESULT_DETECT_CONDITION = b.RESULT_DETECT_CONDITION,
                                CHAR_NAME = b.CHAR_NAME,
                                METHOD_SPECIATION_NAME = b.METHOD_SPECIATION_NAME,
                                RESULT_SAMP_FRACTION = b.RESULT_SAMP_FRACTION,
                                RESULT_MSR = b.RESULT_MSR,
                                RESULT_MSR_UNIT = b.RESULT_MSR_UNIT,
                                RESULT_MSR_QUAL = b.RESULT_MSR_QUAL,
                                RESULT_STATUS = b.RESULT_STATUS,
                                STATISTIC_BASE_CODE = b.STATISTIC_BASE_CODE,
                                RESULT_VALUE_TYPE = b.RESULT_VALUE_TYPE,
                                WEIGHT_BASIS = b.WEIGHT_BASIS,
                                TIME_BASIS = b.TIME_BASIS,
                                TEMP_BASIS = b.TEMP_BASIS,
                                PARTICLESIZE_BASIS = b.PARTICLESIZE_BASIS,
                                PRECISION_VALUE = b.PRECISION_VALUE,
                                BIAS_VALUE = b.BIAS_VALUE,
                                RESULT_COMMENT = b.RESULT_COMMENT,

                                BIO_INTENT_NAME = b.BIO_INTENT_NAME,
                                BIO_INDIVIDUAL_ID = b.BIO_INDIVIDUAL_ID,
                                BIO_SUBJECT_TAXONOMY = b.BIO_SUBJECT_TAXONOMY,
                                BIO_UNIDENTIFIED_SPECIES_ID = b.BIO_UNIDENTIFIED_SPECIES_ID,
                                BIO_SAMPLE_TISSUE_ANATOMY = b.BIO_SAMPLE_TISSUE_ANATOMY,
                                GRP_SUMM_COUNT_WEIGHT_MSR = b.GRP_SUMM_COUNT_WEIGHT_MSR,
                                GRP_SUMM_COUNT_WEIGHT_MSR_UNIT = b.GRP_SUMM_COUNT_WEIGHT_MSR_UNIT,
                                FREQ_CLASS_CODE = b.FREQ_CLASS_CODE,
                                FREQ_CLASS_UNIT = b.FREQ_CLASS_UNIT,
                                ANAL_METHOD_ID = b.ANALYTIC_METHOD_ID,
                                ANAL_METHOD_CTX = b.ANALYTIC_METHOD_CTX,
                                LAB_NAME = b.LAB_NAME,
                                ANAL_START_DT = b.LAB_ANALYSIS_START_DT,
                                ANAL_END_DT = b.LAB_ANALYSIS_END_DT,
                                LAB_COMMENT_CODE = b.RESULT_LAB_COMMENT_CODE,
                                DETECTION_LIMIT = b.METHOD_DETECTION_LEVEL,
                                LAB_REPORTING_LEVEL = b.LAB_REPORTING_LEVEL,
                                PQL = b.PQL,
                                LOWER_QUANT_LIMIT = b.LOWER_QUANT_LIMIT,
                                UPPER_QUANT_LIMIT = b.UPPER_QUANT_LIMIT,
                                DETECTION_LIMIT_UNIT = b.DETECTION_LIMIT_UNIT,
                                LAB_SAMP_PREP_START_DT = b.LAB_SAMP_PREP_START_DT,
                                DILUTION_FACTOR = b.DILUTION_FACTOR,
                                IMPORT_STATUS_CD = (a.IMPORT_STATUS_CD == "F" || b.IMPORT_STATUS_CD == null ) ? a.IMPORT_STATUS_CD : b.IMPORT_STATUS_CD,
                                IMPORT_STATUS_DESC = (a.IMPORT_STATUS_DESC ?? " ") + " " + (b.IMPORT_STATUS_DESC ?? "")
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

        public static string SP_GenWQXXML_Single_Delete(string TypeText, int recordIDX)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return ctx.GenWQXXML_Single_Delete(TypeText, recordIDX).First();
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
        //***********************************************************************
        public static List<WQXAnalysis_Result> SP_WQXAnalysis(string TypeText, string OrgID, int? MonLocIDX, string charName, DateTime? startDt, DateTime? endDt, string DataIncludeInd)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return ctx.WQXAnalysis(TypeText, OrgID, MonLocIDX, charName, startDt, endDt, DataIncludeInd).ToList();
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
                            where (OrgID != null ? a.ORG_ID == OrgID : true)
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