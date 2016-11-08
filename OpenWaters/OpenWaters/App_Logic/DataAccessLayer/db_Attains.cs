using System;
using System.Collections.Generic;
using System.Linq;
using OpenEnvironment.App_Logic.BusinessLogicLayer;

namespace OpenEnvironment.App_Logic.DataAccessLayer
{
    public class AssessDisplay
    {
        public int ATTAINS_ASSESS_IDX { get; set; }
        public string REPORTING_CYCLE { get; set; }
        public string REPORT_STATUS { get; set; }
        public int ATTAINS_ASSESS_UNIT_IDX { get; set; }
        public string ASSESS_UNIT_NAME { get; set; }
        public string AGENCY_CODE { get; set; }
        public string CYCLE_LAST_ASSESSED { get; set; }
        public string CYCLE_LAST_MONITORED { get; set; }
    }

    public class db_Attains
    {

        //***************************** ATTAINS_REPORT ************************************************
        public static List<T_ATTAINS_REPORT> GetT_ATTAINS_REPORT_byORG_ID(string OrgID)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_ATTAINS_REPORT
                            where a.ORG_ID == OrgID
                            select a).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static T_ATTAINS_REPORT GetT_ATTAINS_REPORT_byID(int ReportID)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_ATTAINS_REPORT
                            where a.ATTAINS_REPORT_IDX == ReportID
                            select a).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static int InsertOrUpdateATTAINS_REPORT(int? aTTAINS_REPORT_IDX, string oRG_ID, string rEPORT_NAME, DateTime? dATA_FROM, DateTime? dATA_TO,
            bool? aTTAINS_IND, string aTTAINS_SUBMIT_STATUS, DateTime? aTTAINS_UPDATE_DT, String cREATE_USER = "system")
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                Boolean insInd = false;
                try
                {
                    T_ATTAINS_REPORT a = new T_ATTAINS_REPORT();

                    if (aTTAINS_REPORT_IDX != null)
                        a = (from c in ctx.T_ATTAINS_REPORT
                             where c.ATTAINS_REPORT_IDX == aTTAINS_REPORT_IDX
                             select c).FirstOrDefault();

                    if (aTTAINS_REPORT_IDX == null) //insert case
                    {
                        insInd = true;
                    }

                    if (oRG_ID != null) a.ORG_ID = oRG_ID;
                    if (rEPORT_NAME != null) a.REPORT_NAME = rEPORT_NAME;
                    if (dATA_FROM != null) a.DATA_FROM = dATA_FROM;
                    if (dATA_TO != null) a.DATA_TO = dATA_TO;
                    if (aTTAINS_IND != null) a.ATTAINS_IND = aTTAINS_IND;
                    if (aTTAINS_SUBMIT_STATUS != null) a.ATTAINS_SUBMIT_STATUS = aTTAINS_SUBMIT_STATUS;
                    if (aTTAINS_UPDATE_DT != null) a.ATTAINS_UPDATE_DT = aTTAINS_UPDATE_DT;

                    if (insInd) //insert case
                    {
                        a.CREATE_USERID = cREATE_USER.ToUpper();
                        a.CREATE_DT = System.DateTime.Now;
                        ctx.AddToT_ATTAINS_REPORT(a);
                    }

                    ctx.SaveChanges();

                    return a.ATTAINS_REPORT_IDX;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }

        public static int DeleteT_ATTAINS_REPORT(int aTTAINS_REPORT_IDX)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    T_ATTAINS_REPORT r = (from c in ctx.T_ATTAINS_REPORT
                                          where c.ATTAINS_REPORT_IDX == aTTAINS_REPORT_IDX
                                          select c).FirstOrDefault();

                    if (r.ATTAINS_SUBMIT_STATUS == "Y" || r.ATTAINS_SUBMIT_STATUS == "U")
                        return -1;

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


        //***************************** ATTAINS_REPORT_LOG **************************************************
        public static int InsertOrUpdateATTAINS_REPORT_LOG(int? aTTAINS_LOG_IDX, int? aTTAINS_REPORT_IDX,  DateTime? sUBMIT_DT, 
            string sUBMIT_FILE, byte[] rESPONSE_FILE, string rESPONSE_TXT, string cDX_SUBMIT_TRANSID, string cDX_SUBMIT_STATUS, string cREATE_USER = "system")
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                Boolean insInd = false;
                try
                {
                    T_ATTAINS_REPORT_LOG a = new T_ATTAINS_REPORT_LOG();

                    if (aTTAINS_LOG_IDX != null)
                        a = (from c in ctx.T_ATTAINS_REPORT_LOG
                             where c.ATTAINS_LOG_IDX == aTTAINS_LOG_IDX
                             select c).FirstOrDefault();

                    if (aTTAINS_REPORT_IDX == null) //insert case
                    {
                        insInd = true;
                    }

                    if (aTTAINS_REPORT_IDX != null) a.ATTAINS_REPORT_IDX = aTTAINS_REPORT_IDX.ConvertOrDefault<int>();
                    if (sUBMIT_DT != null) a.SUBMIT_DT = sUBMIT_DT.ConvertOrDefault<DateTime>();
                    if (sUBMIT_FILE != null) a.SUBMIT_FILE = sUBMIT_FILE;
                    if (rESPONSE_FILE != null) a.RESPONSE_FILE = rESPONSE_FILE;
                    if (rESPONSE_TXT != null) a.RESPONSE_TXT = rESPONSE_TXT;
                    if (cDX_SUBMIT_TRANSID != null) a.CDX_SUBMIT_TRANSID = cDX_SUBMIT_TRANSID;
                    if (cDX_SUBMIT_STATUS != null) a.CDX_SUBMIT_STATUS = cDX_SUBMIT_STATUS;

                    if (insInd) //insert case
                    {
                        ctx.AddToT_ATTAINS_REPORT_LOG(a);
                    }

                    ctx.SaveChanges();

                    return a.ATTAINS_LOG_IDX ;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }



        //***************************** ATTAINS_ASSESS **************************************
        public static List<AssessDisplay> GetT_ATTAINS_ASSESS_byReportID(int ReportID)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_ATTAINS_ASSESS
                            join b in ctx.T_ATTAINS_ASSESS_UNITS on a.ATTAINS_ASSESS_UNIT_IDX equals b.ATTAINS_ASSESS_UNIT_IDX
                            where b.ATTAINS_REPORT_IDX == ReportID
                            select new AssessDisplay {
                                ATTAINS_ASSESS_IDX = a.ATTAINS_ASSESS_IDX,
                                REPORTING_CYCLE = a.REPORTING_CYCLE,
                                REPORT_STATUS = a.REPORT_STATUS,
                                ATTAINS_ASSESS_UNIT_IDX = a.ATTAINS_ASSESS_UNIT_IDX,
                                ASSESS_UNIT_NAME = b.ASSESS_UNIT_NAME,
                                AGENCY_CODE = a.AGENCY_CODE,
                                CYCLE_LAST_ASSESSED = a.CYCLE_LAST_ASSESSED,
                                CYCLE_LAST_MONITORED = a.CYCLE_LAST_MONITORED
                            }).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static T_ATTAINS_ASSESS GetT_ATTAINS_ASSESS_byID(int AssessID)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_ATTAINS_ASSESS
                            where a.ATTAINS_ASSESS_IDX == AssessID
                            select a).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static int InsertOrUpdateATTAINS_ASSESS(int? aTTAINS_ASSESS_IDX, string rEPORTING_CYCLE, string rEPORT_STATUS, int aTTAINS_ASSESS_UNIT_IDX,
            string aGENCY_CODE, string cYCLE_LAST_ASSESSED, string cYCLE_LAST_MONITORED, string tROPHIC_STATUS_CODE, String cREATE_USER = "system")
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                Boolean insInd = false;
                try
                {
                    if (aTTAINS_ASSESS_IDX == -1) aTTAINS_ASSESS_IDX = null;

                    T_ATTAINS_ASSESS a = null;

                    if (aTTAINS_ASSESS_IDX != null)
                        a = (from c in ctx.T_ATTAINS_ASSESS
                             where c.ATTAINS_ASSESS_IDX == aTTAINS_ASSESS_IDX
                             select c).FirstOrDefault();

                    if (a == null)
                    {
                        a = new T_ATTAINS_ASSESS();
                        insInd = true;
                    }

                    if (rEPORTING_CYCLE != null) a.REPORTING_CYCLE = rEPORTING_CYCLE;
                    if (rEPORT_STATUS != null) a.REPORT_STATUS = rEPORT_STATUS;
                    a.ATTAINS_ASSESS_UNIT_IDX = aTTAINS_ASSESS_UNIT_IDX;
                    if (aGENCY_CODE != null) a.AGENCY_CODE = aGENCY_CODE;
                    if (cYCLE_LAST_ASSESSED != null) a.CYCLE_LAST_ASSESSED = cYCLE_LAST_ASSESSED;
                    if (cYCLE_LAST_MONITORED != null) a.CYCLE_LAST_MONITORED = cYCLE_LAST_MONITORED;
                    if (tROPHIC_STATUS_CODE != null) a.TROPHIC_STATUS_CODE = tROPHIC_STATUS_CODE;

                    if (insInd) //insert case
                    {
                        a.CREATE_USERID = cREATE_USER.ToUpper();
                        a.CREATE_DT = System.DateTime.Now;
                        ctx.AddToT_ATTAINS_ASSESS(a);
                    }
                    else
                    {
                        a.MODIFY_USERID = cREATE_USER.ToUpper();
                        a.MODIFY_DT = System.DateTime.Now;
                    }

                    ctx.SaveChanges();

                    return a.ATTAINS_ASSESS_IDX;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }

        public static int DeleteT_ATTAINS_ASSESS(int aSSESS_IDX)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    T_ATTAINS_ASSESS r = (from c in ctx.T_ATTAINS_ASSESS
                                                where c.ATTAINS_ASSESS_IDX == aSSESS_IDX
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


        //***************************** ATTAINS_ASSESSMENT_UNIT **************************************
        public static List<T_ATTAINS_ASSESS_UNITS> GetT_ATTAINS_ASSESS_UNITS_byReportID(int ReportID)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_ATTAINS_ASSESS_UNITS
                            where a.ATTAINS_REPORT_IDX == ReportID
                            select a).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static T_ATTAINS_ASSESS_UNITS GetT_ATTAINS_ASSESS_UNITS_byID(int? AssessUnitID)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_ATTAINS_ASSESS_UNITS
                            where a.ATTAINS_ASSESS_UNIT_IDX == AssessUnitID
                            select a).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static int InsertOrUpdateATTAINS_ASSESS_UNITS(int? aTTAINS_ASSESS_UNIT_IDX, int? aTTAINS_REPORT_IDX, string aSSESS_UNIT_ID, string aSSESS_UNIT_NAME, 
            string lOCATION_DESC, string aGENCY_CODE, string sTATE_CODE, string aCT_IND, string wATER_TYPE_CODE, decimal? wATER_SIZE, string wATER_UNIT_CODE,
            string uSE_CLASS_CODE, string uSE_CLASS_NAME, String cREATE_USER = "system")
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                Boolean insInd = false;
                try
                {
                    if (aTTAINS_ASSESS_UNIT_IDX == -1) aTTAINS_ASSESS_UNIT_IDX = null;

                    T_ATTAINS_ASSESS_UNITS a = null;

                    if (aTTAINS_ASSESS_UNIT_IDX != null)
                        a = (from c in ctx.T_ATTAINS_ASSESS_UNITS
                             where c.ATTAINS_ASSESS_UNIT_IDX == aTTAINS_ASSESS_UNIT_IDX
                             select c).FirstOrDefault();

                    if (a == null)
                    {
                        a = new T_ATTAINS_ASSESS_UNITS();
                        insInd = true;
                    }

                    if (aTTAINS_REPORT_IDX != null) a.ATTAINS_REPORT_IDX = aTTAINS_REPORT_IDX.ConvertOrDefault<int>();
                    if (aSSESS_UNIT_ID != null) a.ASSESS_UNIT_ID = aSSESS_UNIT_ID;
                    if (aSSESS_UNIT_NAME != null) a.ASSESS_UNIT_NAME = aSSESS_UNIT_NAME;
                    if (lOCATION_DESC != null) a.LOCATION_DESC = lOCATION_DESC;
                    if (aGENCY_CODE != null) a.AGENCY_CODE = aGENCY_CODE;
                    if (sTATE_CODE != null) a.STATE_CODE = sTATE_CODE;
                    if (aCT_IND != null) a.ACT_IND = aCT_IND;
                    if (wATER_TYPE_CODE != null) a.WATER_TYPE_CODE = wATER_TYPE_CODE;
                    if (wATER_SIZE != null) a.WATER_SIZE = wATER_SIZE;
                    if (wATER_UNIT_CODE != null) a.WATER_UNIT_CODE = wATER_UNIT_CODE;
                    if (uSE_CLASS_CODE != null) a.USE_CLASS_CODE = uSE_CLASS_CODE;
                    if (uSE_CLASS_NAME != null) a.USE_CLASS_NAME = uSE_CLASS_NAME;

                    if (insInd) //insert case
                    {
                        a.CREATE_USERID = cREATE_USER.ToUpper();
                        a.CREATE_DT = System.DateTime.Now;
                        ctx.AddToT_ATTAINS_ASSESS_UNITS(a);
                    }
                    else
                    {
                        a.MODIFY_USERID = cREATE_USER.ToUpper();
                        a.MODIFY_DT = System.DateTime.Now;
                    }

                    ctx.SaveChanges();

                    return a.ATTAINS_ASSESS_UNIT_IDX;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }

        public static int DeleteT_ATTAINS_ASSESS_UNITS(int aSSESS_UNIT_IDX)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    T_ATTAINS_ASSESS_UNITS r = (from c in ctx.T_ATTAINS_ASSESS_UNITS
                                                where c.ATTAINS_ASSESS_UNIT_IDX == aSSESS_UNIT_IDX
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


        //***************************** ATTAINS_ASSESS_UNITS_MLOC **************************************
        public static int InsertOrUpdateATTAINS_ASSESS_UNITS_MLOC(int aSSESS_UNIT_IDX, int mONLOC_IDX, String cREATE_USER = "system")
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                Boolean insInd = false;
                try
                {
                    T_ATTAINS_ASSESS_UNITS_MLOC a = (from c in ctx.T_ATTAINS_ASSESS_UNITS_MLOC
                         where c.ATTAINS_ASSESS_UNIT_IDX == aSSESS_UNIT_IDX
                         && c.MONLOC_IDX == mONLOC_IDX
                         select c).FirstOrDefault();

                    if (a == null)
                    {
                        a = new T_ATTAINS_ASSESS_UNITS_MLOC();
                        insInd = true;
                    }

                    a.ATTAINS_ASSESS_UNIT_IDX = aSSESS_UNIT_IDX;
                    a.MONLOC_IDX = mONLOC_IDX;

                    if (insInd) //insert case
                    {
                        a.CREATE_USERID = cREATE_USER.ToUpper();
                        a.CREATE_DT = System.DateTime.Now;
                        ctx.AddToT_ATTAINS_ASSESS_UNITS_MLOC(a);
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

        public static List<T_WQX_MONLOC> GetT_ATTAINS_ASSESS_UNITS_MLOC_byAssessUnit(int? AssessUnitID)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_ATTAINS_ASSESS_UNITS_MLOC
                            join b in ctx.T_WQX_MONLOC on a.MONLOC_IDX equals b.MONLOC_IDX
                            where a.ATTAINS_ASSESS_UNIT_IDX == AssessUnitID
                            select b).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static int DeleteT_ATTAINS_ASSESS_UNITS_MLOC(int aSSESS_UNIT_IDX, int mONLOC_IDX)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    T_ATTAINS_ASSESS_UNITS_MLOC r = (from c in ctx.T_ATTAINS_ASSESS_UNITS_MLOC
                                                     where c.ATTAINS_ASSESS_UNIT_IDX == aSSESS_UNIT_IDX
                                                     && c.MONLOC_IDX == mONLOC_IDX
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


        //***************************** ATTAINS_REF_WATER_TYPE **************************************
        public static List<T_ATTAINS_REF_WATER_TYPE> GetT_ATTAINS_REF_WATER_TYPE()
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return (from a in ctx.T_ATTAINS_REF_WATER_TYPE
                            orderby a.WATER_TYPE_CODE
                            select a).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }



        //***************************** SP ***********************************************
        public static string SP_GenATTAINSXML(int reportIDX)
        {
            using (OpenEnvironmentEntities ctx = new OpenEnvironmentEntities())
            {
                try
                {
                    return ctx.GenATTAINSXML(reportIDX).First();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
