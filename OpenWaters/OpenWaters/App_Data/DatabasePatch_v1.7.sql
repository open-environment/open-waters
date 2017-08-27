/*
Only run this script if upgrading from v1.6.2 to v1.7x

After running this script it is recommended that you reimport the Characteristic Reference Table from EPA (go to Admin --> Data Synch screen)

Version 1.7x Change Log
--------------------
MINOR ENHANCEMENTS: 
1. Added more validation when importing samples: sample fraction requirment check for certain characteristics
2. Better feedback when try to create users if admin has not yet configured email server
3. Admin --> Data Synch --> updated to use new URL when pulling Org list from EPA
4. Updated Installation/Users Guide 
----1.7.1 update
5. Add warning for users if they attempt to enter data before making the initial pull of reference data from EPA
----1.7.2 update
6. New data filter added to Activity Search Page: WQX Submit status
7. When submitting to EPA and a record fails, error report now displays in human-readable format instead of XML report 
8. Additional data validation on import samples

BUG FIXES:
1. Bug fix: Collation error in create database script for REF_DATA table.
2. Fix display issues when using Internet Explorer Version 7 or Compatibility View
3. Fix monitoring location edit page to pull counties drop-down from county reference table 
4. Fix unhandled exception if user tries to import samples using crosstab template but omits monitoring locations
----1.7.1 update
5. Bug fix: unable to edit results on activity edit page
----1.7.2 update
6. Bug fix: WQX submission history page not displaying when accessed via Activity page
7. Bug fix: handle situation where ResultStatus or ValueType is empty string when submitting to EPA

*/

ALTER TABLE T_WQX_REF_DATA ALTER COLUMN [Value] varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS
GO
ALTER TABLE T_WQX_REF_CHARACTERISTIC add [SAMP_FRAC_REQ] varchar(1);
ALTER TABLE T_WQX_REF_CHARACTERISTIC add [PICK_LIST] varchar(1);
GO
UPDATE T_WQX_REF_CHARACTERISTIC set SAMP_FRAC_REQ = 'N', PICK_LIST = 'N';

GO



-- ****************************************************************************************************************************************
-- ************************* [STORED PROCS]*********************************************************************************
-- ****************************************************************************************************************************************

ALTER PROCEDURE GenWQXXML_Org
@OrgID varchar(30)
AS
BEGIN
	/*
	DESCRIPTION: RETURNS WQX XML FILE CONTAINING ALL UPDATED RECORDS for a given Organization
	CHANGE LOG: 8/6/2012 DOUG TIMMS, OPEN-ENVIRONMENT.ORG
	9/22/2012 DOUG TIMMS, fix error with project sampling design type code
	11/24/2014 DOUG TIMMS, split procedure out from individual record version
	3/26/2015 DOUG TIMMS, added sample collection and bio fields, expanded detection limit handling
    6/6/2015 DOUG TIMMS, handle situation where ResultStatus or Value Type is empty string
	*/
	SET NOCOUNT ON;

	DECLARE @strOrg varchar(max);
	DECLARE @strProj varchar(max);   --PROJ
	DECLARE @strMon varchar(max);  --MONLOC
	DECLARE @strBioIndex varchar(max);  --BIO INDX
	DECLARE @strActivity varchar(max);  --ACTIVITY
	DECLARE @strActGrp varchar(max);   -- ACTGROUP
	DECLARE @strWQX varchar(max);
	DECLARE @logLevel varchar(20);

	--**********************************************************
	--************ORGANIZATIONS ********************************
	--*that have ORG, MONLOC, PROJ, or ACTIVITY/RESULT updated**   
	--**********************************************************
	select @strOrg =(
	 SELECT distinct rtrim(O.ORG_ID) as "OrganizationDescription/OrganizationIdentifier", 
			rtrim(O.ORG_FORMAL_NAME) as "OrganizationDescription/OrganizationFormalName", 
			rtrim(O.ORG_DESC) as "OrganizationDescription/OrganizationDescriptionText", 
			rtrim(O.TRIBAL_CODE) as "OrganizationDescription/TribalCode", 
			rtrim(O.ELECTRONICADDRESS) as "ElectronicAddress/ElectronicAddressText", 
			rtrim(O.ELECTRONICADDRESSTYPE) as "ElectronicAddress/ElectronicAddressTypeName", 
			rtrim(O.TELEPHONE_NUM) as "Telephonic/TelephoneNumberText",
			'Office' as "Telephonic/TelephoneNumberTypeName", 
			rtrim(O.TELEPHONE_EXT) as "Telephonic/TelephoneExtensionNumberText"
		from T_WQX_ORGANIZATION O 
		LEFT OUTER JOIN T_WQX_PROJECT P on O.ORG_ID = P.ORG_ID
		LEFT OUTER JOIN T_WQX_MONLOC M on O.ORG_ID = M.ORG_ID
		LEFT OUTER JOIN T_WQX_ACTIVITY A on O.ORG_ID = A.ORG_ID
		where ((M.WQX_SUBMIT_STATUS = 'U' and M.WQX_IND=1 and M.ACT_IND=1)
		or (P.WQX_SUBMIT_STATUS = 'U' and P.WQX_IND=1 and P.ACT_IND=1)
		or (A.WQX_SUBMIT_STATUS = 'U' and A.WQX_IND=1 and A.ACT_IND=1))
		and O.ORG_ID = @OrgID		
	  for xml path('')
	 )


	--****************************************************************
	--************PROJECT ********************************
	--****************************************************************
	set @strProj = ''

	select @strProj = (
	select distinct rtrim(PROJECT_ID) as "Project/ProjectIdentifier",
			ISNULL(rtrim(PROJECT_NAME),'') as "Project/ProjectName",
			ISNULL(rtrim(PROJECT_DESC),'') as "Project/ProjectDescriptionText",
			case when nullif(SAMP_DESIGN_TYPE_CD,'') is not null then rtrim(SAMP_DESIGN_TYPE_CD) else null end as "Project/SamplingDesignTypeCode",
			rtrim(QAPP_APPROVAL_IND)  as "Project/QAPPApprovedIndicator",
			rtrim(QAPP_APPROVAL_AGENCY) as "Project/QAPPApprovalAgencyName"
			from T_WQX_PROJECT 
			WHERE WQX_SUBMIT_STATUS='U' 
			and WQX_IND = 1
			and ACT_IND = 1
			and (@OrgID = ORG_ID)
			for xml path('')
		);


	--****************************************************************
	--************MONITORING_LOCATION ********************************
	--****************************************************************
	set @strMon = ''

	select @strMon = (
	select distinct rtrim(MONLOC_ID) as "MonitoringLocation/MonitoringLocationIdentity/MonitoringLocationIdentifier",
			ISNULL(rtrim(MONLOC_NAME),'') as "MonitoringLocation/MonitoringLocationIdentity/MonitoringLocationName",
			ISNULL(rtrim(MONLOC_TYPE),'') as "MonitoringLocation/MonitoringLocationIdentity/MonitoringLocationTypeName",
			ISNULL(rtrim(MONLOC_DESC), '') as "MonitoringLocation/MonitoringLocationIdentity/MonitoringLocationDescriptionText",
			rtrim(HUC_EIGHT)  as "MonitoringLocation/MonitoringLocationIdentity/HUCEightDigitCode",
			rtrim(HUC_TWELVE) AS "MonitoringLocation/MonitoringLocationIdentity/HUCTwelveDigitCode",
			case when TRIBAL_LAND_IND = 'Y' then 'true' else 'false' end AS "MonitoringLocation/MonitoringLocationIdentity/TribalLandIndicator", 
			rtrim(TRIBAL_LAND_NAME) AS "MonitoringLocation/MonitoringLocationIdentity/TribalLandName",
			NULL AS "MonitoringLocation/MonitoringLocationIdentity/AlternateMonitoringLocationIdentity/MonitoringLocationIdentifier",
			NULL AS "MonitoringLocation/MonitoringLocationIdentity/AlternateMonitoringLocationIdentity/MonitoringLocationIdentifierContext",
			ISNULL(rtrim(LATITUDE_MSR), 0) as "MonitoringLocation/MonitoringLocationGeospatial/LatitudeMeasure",
			ISNULL(rtrim(LONGITUDE_MSR), 0) as "MonitoringLocation/MonitoringLocationGeospatial/LongitudeMeasure",
			ISNULL(rtrim(SOURCE_MAP_SCALE),0) AS "MonitoringLocation/MonitoringLocationGeospatial/SourceMapScaleNumeric", 
			ISNULL(HORIZ_COLL_METHOD,'Unknown') as "MonitoringLocation/MonitoringLocationGeospatial/HorizontalCollectionMethodName",
			ISNULL(HORIZ_REF_DATUM,'UNKWN') as "MonitoringLocation/MonitoringLocationGeospatial/HorizontalCoordinateReferenceSystemDatumName",
			case when nullif(VERT_MEASURE,'') is not null then rtrim(VERT_MEASURE) else null end as "MonitoringLocation/MonitoringLocationGeospatial/VerticalMeasure/MeasureValue",
			case when nullif(VERT_MEASURE,'') is not null then rtrim(VERT_MEASURE_UNIT) else null end as "MonitoringLocation/MonitoringLocationGeospatial/VerticalMeasure/MeasureUnitCode",
			case when nullif(VERT_MEASURE,'') is not null then ISNULL(VERT_COLL_METHOD,'Other') else null end as "MonitoringLocation/MonitoringLocationGeospatial/VerticalCollectionMethodName",
			case when nullif(VERT_MEASURE,'') is not null then ISNULL(VERT_REF_DATUM,'OTHER') else null end as "MonitoringLocation/MonitoringLocationGeospatial/VerticalCoordinateReferenceSystemDatumName",
			ISNULL(rtrim(COUNTRY_CODE),'US') as  "MonitoringLocation/MonitoringLocationGeospatial/CountryCode", 
			rtrim(STATE_CODE) as "MonitoringLocation/MonitoringLocationGeospatial/StateCode", 
			rtrim(COUNTY_CODE) as "MonitoringLocation/MonitoringLocationGeospatial/CountyCode",
			nullif(WELL_TYPE,'') as "MonitoringLocation/WellInformation/WellTypeText",
			nullif(AQUIFER_NAME,'') as "MonitoringLocation/WellInformation/AquiferName",
			nullif(FORMATION_TYPE,'') as "MonitoringLocation/WellInformation/FormationTypeText",
			nullif(WELLHOLE_DEPTH_MSR,'') "MonitoringLocation/WellInformation/WellHoleDepthMeasure/MeasureValue",
			nullif(WELLHOLE_DEPTH_MSR_UNIT,'') "MonitoringLocation/WellInformation/WellHoleDepthMeasure/MeasureUnitCode"
			from T_WQX_MONLOC 
			WHERE WQX_SUBMIT_STATUS='U' 
			and WQX_IND=1
			and ACT_IND=1
			and (ORG_ID = @OrgID)
			for xml path('')
		);

	--****************************************************************
	--************BIO INDICES ****************************************
	--****************************************************************
	set @strBioIndex = '';
	select @strBioIndex = (
		select distinct rtrim(B.INDEX_ID) as "BiologicalHabitatIndex/IndexIdentifier",
			   rtrim(B.INDEX_TYPE_ID) as "BiologicalHabitatIndex/IndexType/IndexTypeIdentifier",
			   rtrim(B.INDEX_TYPE_ID_CONTEXT) as "BiologicalHabitatIndex/IndexType/IndexTypeIdentifierContext",
			   rtrim(B.INDEX_TYPE_NAME) as "BiologicalHabitatIndex/IndexType/IndexTypeName",
			   rtrim(B.INDEX_SCORE)  as "BiologicalHabitatIndex/IndexScoreNumeric",
			   rtrim(B.INDEX_QUAL_CD) as "BiologicalHabitatIndex/IndexQualifierCode",
			   rtrim(B.INDEX_COMMENT) as "BiologicalHabitatIndex/IndexCommentText",
			   LEFT(CONVERT(VARCHAR, B.INDEX_CALC_DATE, 120), 10) as "BiologicalHabitatIndex/IndexCalculatedDate"
			   from T_WQX_BIO_HABITAT_INDEX B, T_WQX_MONLOC M
			   WHERE B.MONLOC_IDX = M.MONLOC_IDX
			   and B.WQX_SUBMIT_STATUS='U' 
			   and B.WQX_IND = 1
			   and B.ACT_IND = 1
			   and (@OrgID = B.ORG_ID)
			   for xml path('')
			);


	--****************************************************************
	--************ACTIVITY *******************************************
	--****************************************************************
	set @strActivity = ''

	select @strActivity = (
	select rtrim(ACTIVITY_ID) as "ActivityDescription/ActivityIdentifier",
			ISNULL(rtrim(ACT_TYPE),'') as "ActivityDescription/ActivityTypeCode",
			ISNULL(rtrim(ACT_MEDIA),'') as "ActivityDescription/ActivityMediaName",
			rtrim(ACT_SUBMEDIA) as "ActivityDescription/ActivityMediaSubdivisionName",
			LEFT(CONVERT(VARCHAR, ACT_START_DT, 120), 10) as "ActivityDescription/ActivityStartDate",
			RIGHT(CONVERT(VARCHAR, ACT_START_DT, 120), 8) as "ActivityDescription/ActivityStartTime/Time",
            case when ACT_START_DT is not null then ISNULL(nullif(ACT_TIME_ZONE,''), 'CST') else null end as "ActivityDescription/ActivityStartTime/TimeZoneCode", 
            LEFT(CONVERT(VARCHAR, ACT_END_DT, 120), 10) as "ActivityDescription/ActivityEndDate",
            RIGHT(CONVERT(VARCHAR, ACT_END_DT, 120), 8) AS "ActivityDescription/ActivityEndTime/Time",
            case when ACT_END_DT is not null then ISNULL(nullif(ACT_TIME_ZONE,''), 'CST') else null end as "ActivityDescription/ActivityEndTime/TimeZoneCode", 
			rtrim(RELATIVE_DEPTH_NAME) as "ActivityDescription/ActivityRelativeDepthName", 
			rtrim(ACT_DEPTHHEIGHT_MSR) AS "ActivityDescription/ActivityDepthHeightMeasure/MeasureValue", 
            rtrim(ACT_DEPTHHEIGHT_MSR_UNIT) AS "ActivityDescription/ActivityDepthHeightMeasure/MeasureUnitCode",
	        rtrim(TOP_DEPTHHEIGHT_MSR) AS "ActivityDescription/ActivityTopDepthHeightMeasure/MeasureValue", 
			rtrim(TOP_DEPTHHEIGHT_MSR_UNIT) AS "ActivityDescription/ActivityTopDepthHeightMeasure/MeasureUnitCode",
	        rtrim(BOT_DEPTHHEIGHT_MSR) AS "ActivityDescription/ActivityBottomDepthHeightMeasure/MeasureValue", 
			rtrim(BOT_DEPTHHEIGHT_MSR_UNIT) AS "ActivityDescription/ActivityBottomDepthHeightMeasure/MeasureUnitCode",
            rtrim(DEPTH_REF_POINT) as "ActivityDescription/ActivityDepthAltitudeReferencePointText", 
    		(SELECT p.PROJECT_ID from T_WQX_PROJECT p where p.PROJECT_IDX = T_WQX_ACTIVITY.PROJECT_IDX) AS "ActivityDescription/ProjectIdentifier",
            ORG_ID as "ActivityDescription/ActivityConductingOrganizationText",
            (SELECT MONLOC_ID from T_WQX_MONLOC M WHERE M.MONLOC_IDX = T_WQX_ACTIVITY.MONLOC_IDX)  AS "ActivityDescription/MonitoringLocationIdentifier", 
            ACT_COMMENT as "ActivityDescription/ActivityCommentText",
			   --SAMPLE BIO
	           case when isnull(T_WQX_ACTIVITY.BIO_ASSEMBLAGE_SAMPLED,'') <> '' then rtrim(BIO_ASSEMBLAGE_SAMPLED) else null end AS "BiologicalActivityDescription/AssemblageSampledName", 
	           case when isnull(T_WQX_ACTIVITY.BIO_DURATION_MSR,'') <> '' then rtrim(BIO_DURATION_MSR) else null end AS "BiologicalActivityDescription/BiologicalHabitatCollectionInformation/CollectionDuration/MeasureValue", 
	           case when isnull(T_WQX_ACTIVITY.BIO_DURATION_MSR_UNIT,'') <> '' then rtrim(BIO_DURATION_MSR_UNIT) else null end AS "BiologicalActivityDescription/BiologicalHabitatCollectionInformation/CollectionDuration/MeasureUnitCode", 
	           case when isnull(T_WQX_ACTIVITY.BIO_REACH_LEN_MSR,'') <> '' then rtrim(BIO_REACH_LEN_MSR) else null end AS "BiologicalActivityDescription/BiologicalHabitatCollectionInformation/ReachLengthMeasure/MeasureValue", 
	           case when isnull(T_WQX_ACTIVITY.BIO_REACH_LEN_MSR_UNIT,'') <> '' then rtrim(BIO_REACH_LEN_MSR_UNIT) else null end AS "BiologicalActivityDescription/BiologicalHabitatCollectionInformation/ReachLengthMeasure/MeasureUnitCode", 
	           case when isnull(T_WQX_ACTIVITY.BIO_REACH_WID_MSR,'') <> '' then rtrim(BIO_REACH_WID_MSR) else null end AS "BiologicalActivityDescription/BiologicalHabitatCollectionInformation/ReachWidthMeasure/MeasureValue", 
	           case when isnull(T_WQX_ACTIVITY.BIO_REACH_WID_MSR_UNIT,'') <> '' then rtrim(BIO_REACH_WID_MSR_UNIT) else null end AS "BiologicalActivityDescription/BiologicalHabitatCollectionInformation/ReachWidthMeasure/MeasureUnitCode", 

			   --SAMPLE DESCRIPTION
			   case when isnull(T_WQX_ACTIVITY.SAMP_COLL_METHOD_IDX,'') <> '' then SCL.SAMP_COLL_METHOD_ID else null end as "SampleDescription/SampleCollectionMethod/MethodIdentifier", 
			   case when isnull(T_WQX_ACTIVITY.SAMP_COLL_METHOD_IDX,'') <> '' then SCL.SAMP_COLL_METHOD_CTX else null end as "SampleDescription/SampleCollectionMethod/MethodIdentifierContext", 
			   case when isnull(T_WQX_ACTIVITY.SAMP_COLL_METHOD_IDX,'') <> '' then SCL.SAMP_COLL_METHOD_NAME else null end as "SampleDescription/SampleCollectionMethod/MethodName", 
               case when isnull(T_WQX_ACTIVITY.SAMP_COLL_EQUIP,'') <> '' then rtrim(SAMP_COLL_EQUIP) else null end as "SampleDescription/SampleCollectionEquipmentName",
               case when isnull(T_WQX_ACTIVITY.SAMP_COLL_EQUIP,'') <> '' and isnull(T_WQX_ACTIVITY.SAMP_COLL_EQUIP_COMMENT,'') <> '' then rtrim(SAMP_COLL_EQUIP_COMMENT) else null end AS "SampleDescription/SampleCollectionEquipmentCommentText",

				--RESULT
    			(SELECT rtrim(DATA_LOGGER_LINE) AS "ResultDescription/DataLoggerLineName",
    			case when RESULT_MSR ='ND' then 'Not Detected' 
    				    when RESULT_MSR = 'NR' then 'Not Reported'
    				    when RESULT_MSR = 'PAQL' then 'Present Above Quantification Limit'
    				    when RESULT_MSR = 'PBQL' then 'Present Below Quantification Limit'
    				    when RESULT_MSR = 'DNQ' then 'Detected Not Quantified' 
    				    else rtrim(RESULT_DETECT_CONDITION) END AS "ResultDescription/ResultDetectionConditionText", 
    			rtrim(CHAR_NAME) AS "ResultDescription/CharacteristicName",
    			rtrim(METHOD_SPECIATION_NAME) as "ResultDescription/MethodSpeciationName",
    			rtrim(RESULT_SAMP_FRACTION) AS "ResultDescription/ResultSampleFractionText",
				case when RESULT_MSR in ('ND', 'NR', 'PAQL', 'PBQL', 'DNQ') then null else rtrim(RESULT_MSR) END AS "ResultDescription/ResultMeasure/ResultMeasureValue",
				rtrim(RESULT_MSR_UNIT) AS "ResultDescription/ResultMeasure/MeasureUnitCode",
				rtrim(RESULT_MSR_QUAL) AS "ResultDescription/ResultMeasure/MeasureQualifierCode",
                coalesce(nullif(RESULT_STATUS,''),'Final') AS "ResultDescription/ResultStatusIdentifier",
                rtrim(STATISTIC_BASE_CODE) AS "ResultDescription/StatisticalBaseCode",
                coalesce(nullif(RESULT_VALUE_TYPE,''),'Actual') AS "ResultDescription/ResultValueTypeName",
                RTRIM(WEIGHT_BASIS) AS "ResultDescription/ResultWeightBasisText",
                rtrim(TIME_BASIS) AS "ResultDescription/ResultTimeBasisText",
                rtrim(TEMP_BASIS) AS "ResultDescription/ResultTemperatureBasisText",
				rtrim(PARTICLESIZE_BASIS) AS "ResultDescription/ResultParticleSizeBasisText",
				rtrim(PRECISION_VALUE) AS "ResultDescription/DataQuality/PrecisionValue",
				rtrim(BIAS_VALUE) AS "ResultDescription/DataQuality/BiasValue",
				rtrim(CONFIDENCE_INTERVAL_VALUE) AS "ResultDescription/DataQuality/ConfidenceIntervalValue",
				rtrim(UPPER_CONFIDENCE_LIMIT) AS "ResultDescription/DataQuality/UpperConfidenceLimitValue",
				rtrim(LOWER_CONFIDENCE_LIMIT) AS "ResultDescription/DataQuality/LowerConfidenceLimitValue",
				rtrim(RESULT_COMMENT) AS "ResultDescription/ResultCommentText",
				rtrim(DEPTH_HEIGHT_MSR) AS "ResultDescription/ResultDepthHeightMeasure/MeasureValue",
				rtrim(DEPTH_HEIGHT_MSR_UNIT) AS "ResultDescription/ResultDepthHeightMeasure/MeasureUnitCode",
				rtrim(DEPTHALTITUDEREFPOINT) AS "ResultDescription/ResultDepthAltitudeReferencePointText",
				rtrim(RESULT_SAMP_POINT) AS "ResultDescription/ResultSamplingPointName",
					
				case when nullif(BIO_SUBJECT_TAXONOMY,'') is not null then rtrim(BIO_INTENT_NAME) else null end as "BiologicalResultDescription/BiologicalIntentName",
				case when nullif(BIO_SUBJECT_TAXONOMY,'') is not null then rtrim(BIO_INDIVIDUAL_ID) else null end AS "BiologicalResultDescription/BiologicalIndividualIdentifier",
				case when nullif(BIO_SUBJECT_TAXONOMY,'') is not null then rtrim(BIO_SUBJECT_TAXONOMY) else null end as "BiologicalResultDescription/SubjectTaxonomicName",
				case when nullif(BIO_SUBJECT_TAXONOMY,'') is not null then rtrim(BIO_UNIDENTIFIED_SPECIES_ID) else null end AS "BiologicalResultDescription/UnidentifiedSpeciesIdentifier",
				case when nullif(BIO_SUBJECT_TAXONOMY,'') is not null then rtrim(BIO_SAMPLE_TISSUE_ANATOMY) else null end as "BiologicalResultDescription/SampleTissueAnatomyName",
					
				rtrim(AM.ANALYTIC_METHOD_ID) as "ResultAnalyticalMethod/MethodIdentifier",
				rtrim(AM.ANALYTIC_METHOD_CTX) as "ResultAnalyticalMethod/MethodIdentifierContext",
				rtrim(AM.ANALYTIC_METHOD_NAME) AS "ResultAnalyticalMethod/MethodName",
				null AS "ResultAnalyticalMethod/MethodQualifierTypeName",
				rtrim(AM.ANALYTIC_METHOD_DESC) AS "ResultAnalyticalMethod/MethodDescriptionText",
				rtrim(LL.LAB_NAME) AS "ResultLabInformation/LaboratoryName",
 				LEFT(CONVERT(VARCHAR, LAB_ANALYSIS_START_DT, 120), 10) as "ResultLabInformation/AnalysisStartDate", 
				RIGHT(CONVERT(VARCHAR, LAB_ANALYSIS_START_DT, 120), 8) AS "ResultLabInformation/AnalysisStartTime/Time" , 
				CASE WHEN LAB_ANALYSIS_START_DT IS NOT NULL THEN 'CST' ELSE NULL END AS "ResultLabInformation/AnalysisStartTime/TimeZoneCode", 
 				LEFT(CONVERT(VARCHAR, LAB_ANALYSIS_END_DT, 120), 10) as "ResultLabInformation/AnalysisEndDate", 
				RIGHT(CONVERT(VARCHAR, LAB_ANALYSIS_END_DT, 120), 8) AS "ResultLabInformation/AnalysisEndTime/Time", 
				CASE WHEN LAB_ANALYSIS_END_DT IS NOT NULL THEN 'CST' ELSE NULL END AS "ResultLabInformation/AnalysisEndTime/TimeZoneCode",
				rtrim(RESULT_LAB_COMMENT_CODE) AS "ResultLabInformation/ResultLaboratoryCommentCode",

					--DETECTION LIMIT HANDLING
					case when (RESULT_MSR in ('ND','NR','PAQL','PBQL','DNQ') or nullif(R.DETECTION_LIMIT,'') is not null or nullif(R.LAB_REPORTING_LEVEL,'') is not null or nullif(R.PQL,'') is not null or nullif(R.LOWER_QUANT_LIMIT,'') is not null or nullif(R.UPPER_QUANT_LIMIT,'') is not null) then 
						 case when nullif(R.DETECTION_LIMIT_TYPE,'') is not null then rtrim(R.DETECTION_LIMIT_TYPE) 
						      when nullif(DETECTION_LIMIT,'') is not null then 'Method Detection Level'
						      when nullif(LAB_REPORTING_LEVEL,'') is not null then 'Laboratory Reporting Level'
						      when nullif(PQL,'') is not null then 'Practical Quantitation Limit'
						      when nullif(LOWER_QUANT_LIMIT,'') is not null then 'Lower Quantitation Limit'
						      when nullif(UPPER_QUANT_LIMIT,'') is not null then 'Upper Quantitation Limit'
							  else  'Estimated Detection Level' end
					end AS "ResultLabInformation/ResultDetectionQuantitationLimit/DetectionQuantitationLimitTypeName",
					case when (RESULT_MSR in ('ND','NR','PAQL','PBQL','DNQ') or nullif(R.DETECTION_LIMIT,'') is not null or nullif(R.LAB_REPORTING_LEVEL,'') is not null or nullif(R.PQL,'') is not null or nullif(R.LOWER_QUANT_LIMIT,'') is not null or nullif(R.UPPER_QUANT_LIMIT,'') is not null) then 
						case when nullif(DETECTION_LIMIT,'') is not null then DETECTION_LIMIT 
						      when nullif(LAB_REPORTING_LEVEL,'') is not null then LAB_REPORTING_LEVEL
						      when nullif(PQL,'') is not null then PQL
						      when nullif(LOWER_QUANT_LIMIT,'') is not null then LOWER_QUANT_LIMIT
						      when nullif(UPPER_QUANT_LIMIT,'') is not null then UPPER_QUANT_LIMIT					     
							  else '0' end
					end AS "ResultLabInformation/ResultDetectionQuantitationLimit/DetectionQuantitationLimitMeasure/MeasureValue",
					case when (RESULT_MSR in ('ND','NR','PAQL','PBQL','DNQ') or nullif(R.DETECTION_LIMIT,'') is not null or nullif(R.LAB_REPORTING_LEVEL,'') is not null or nullif(R.PQL,'') is not null or nullif(R.LOWER_QUANT_LIMIT,'') is not null or nullif(R.UPPER_QUANT_LIMIT,'') is not null) then 
						case when nullif(RESULT_MSR_UNIT,'') is not null then RESULT_MSR_UNIT ELSE 
							(select isnull(rc.DEFAULT_UNIT,'None') from T_WQX_REF_CHARACTERISTIC rc where rc.CHAR_NAME = R.CHAR_NAME) end end AS "ResultLabInformation/ResultDetectionQuantitationLimit/DetectionQuantitationLimitMeasure/MeasureUnitCode"

    			FROM T_WQX_RESULT R 
				LEFT JOIN T_WQX_REF_ANAL_METHOD AM on R.ANALYTIC_METHOD_IDX = AM.ANALYTIC_METHOD_IDX
				LEFT JOIN T_WQX_REF_LAB LL on R.LAB_IDX = LL.LAB_IDX
    			where R.ACTIVITY_IDX = T_WQX_ACTIVITY.ACTIVITY_IDX 
				for xml path('Result'),type ) 

			from T_WQX_ACTIVITY
 		    left join T_WQX_REF_SAMP_COL_METHOD SCL on T_WQX_ACTIVITY.SAMP_COLL_METHOD_IDX = SCL.SAMP_COLL_METHOD_IDX
			WHERE WQX_SUBMIT_STATUS='U' 
			and WQX_IND = 1
			and T_WQX_ACTIVITY.	ACT_IND = 1
   			and (@OrgID = ORG_ID)
			for xml path('Activity')
		);

	-- *************************************************************************
	-- ***************** COMBINE ***********************************************
	-- *************************************************************************
	set @strWQX = '<?xml version="1.0" encoding="UTF-8"?>
	<Document Id="UI_' + convert(varchar, getdate(), 112) + '" xmlns="http://www.exchangenetwork.net/schema/v1.0/ExchangeNetworkDocument.xsd">
		<Header>
			<Author>Open Waters</Author>
			<Organization>Open Environment</Organization>
			<Title>WQX</Title>
			<CreationTime>' + LEFT(CONVERT(varchar, getdate(), 120), 10) + 'T' + RIGHT(convert(varchar, getdate(), 120), 8) + '</CreationTime>
			<ContactInfo>doug.timms@open-environment.org</ContactInfo>
		</Header>
		<Payload Operation="Update-Insert">
	<WQX xmlns="http://www.exchangenetwork.net/schema/wqx/2" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:schemaLocation="http://www.exchangenetwork.net/schema/wqx/2 
	http://www.exchangenetwork.net/schema/WQX/2/index.xsd">
		<Organization>' + @strOrg + isnull(@strProj,'') + isnull(@strMon,'') + isnull(@strBioIndex,'') + isnull(@strActivity,'') + isnull(@strActGrp,'') + '</Organization></WQX></Payload></Document>'
	
	select @strWQX;

	--IF DEBUG MODE, WRITE TO TRANSACTION LOG
	select @logLevel = (select SETTING_VALUE from T_OE_APP_SETTINGS where SETTING_NAME = 'Log Level');
	if @logLevel='DEBUG' 
	BEGIN
		insert into T_WQX_TRANSACTION_LOG(TABLE_CD, TABLE_IDX, SUBMIT_DT, SUBMIT_TYPE, RESPONSE_TXT) values ('',0,GetDate(), 'I', @strWQX);
	END

END

GO

ALTER PROCEDURE GenWQXXML_Single
  @TypeText varchar(4),
  @RecordIDX int
AS
BEGIN
	/*
	DESCRIPTION: RETURNS WQX XML FILE CONTAINING A SINGLE UPDATED RECORD
	TypeText can be MLOC, PROJ, IDX, or ACT

	CHANGE LOG: 8/6/2012 DOUG TIMMS, OPEN-ENVIRONMENT.ORG
	            9/22/2012 DOUG TIMMS, fix error with project sampling design type code
	            11/24/2014 DOUG TIMMS, added support to submit for single organization within multi-organization database; split into 2 stored procs for performance
				3/26/2015 DOUG TIMMS, added sample collection and bio fields, expanded detection limit handling
				6/6/2015 DOUG TIMMS, handle situation where ResultStatus or Value Type is empty string
	*/
	SET NOCOUNT ON;

	DECLARE @strOrg varchar(max);       --ORG
	DECLARE @strProj varchar(max);      --PROJ
	DECLARE @strMon varchar(max);       --MONLOC
	DECLARE @strBioIndex varchar(max);  --BIO INDX
	DECLARE @strActivity varchar(max);  --ACTIVITY
	DECLARE @strActGrp varchar(max);    --ACTGROUP
	DECLARE @strWQX varchar(max);       --output XML
	DECLARE @logLevel varchar(20);
	DECLARE @orgID varchar(30);         --Org ID for the updated record

	--**********************************************************
    -- GET ORGANIZATION based on record IDX
	--**********************************************************
	if (@TypeText='PROJ') 
		SELECT @orgID = ORG_ID from T_WQX_PROJECT where PROJECT_IDX = @RecordIDX; 
    else if (@TypeText='MLOC')
		SELECT @orgID = ORG_ID from T_WQX_MONLOC where MONLOC_IDX = @RecordIDX; 
    else if (@TypeText='ACT')
		SELECT @orgID = ORG_ID from T_WQX_ACTIVITY where ACTIVITY_IDX = @RecordIDX; 
    else if (@TypeText='IDX')
		SELECT @orgID = ORG_ID from T_WQX_BIO_HABITAT_INDEX where BIO_HABITAT_INDEX_IDX = @RecordIDX; 


	--**********************************************************
	--************ORGANIZATIONS ********************************
	--*that have ORG, MONLOC, PROJ, or ACTIVITY/RESULT updated**   
	--**********************************************************
	select @strOrg =(
	 SELECT distinct rtrim(O.ORG_ID) as "OrganizationDescription/OrganizationIdentifier", 
			rtrim(O.ORG_FORMAL_NAME) as "OrganizationDescription/OrganizationFormalName", 
			rtrim(O.ORG_DESC) as "OrganizationDescription/OrganizationDescriptionText", 
			rtrim(O.TRIBAL_CODE) as "OrganizationDescription/TribalCode", 
			rtrim(O.ELECTRONICADDRESS) as "ElectronicAddress/ElectronicAddressText", 
			rtrim(O.ELECTRONICADDRESSTYPE) as "ElectronicAddress/ElectronicAddressTypeName", 
			rtrim(O.TELEPHONE_NUM) as "Telephonic/TelephoneNumberText",
			'Office' as "Telephonic/TelephoneNumberTypeName", 
			rtrim(O.TELEPHONE_EXT) as "Telephonic/TelephoneExtensionNumberText"
		from T_WQX_ORGANIZATION O 
		where O.ORG_ID = @orgID
		for xml path('')
	 );


	--****************************************************************
	--************PROJECT ********************************
	--****************************************************************
	set @strProj = '';

	if (@TypeText='PROJ') 
	BEGIN
		select @strProj = (
		select distinct rtrim(PROJECT_ID) as "Project/ProjectIdentifier",
			   ISNULL(rtrim(PROJECT_NAME),'') as "Project/ProjectName",
			   ISNULL(rtrim(PROJECT_DESC),'') as "Project/ProjectDescriptionText",
			   case when nullif(SAMP_DESIGN_TYPE_CD,'') is not null then rtrim(SAMP_DESIGN_TYPE_CD) else null end as "Project/SamplingDesignTypeCode",
			   rtrim(QAPP_APPROVAL_IND)  as "Project/QAPPApprovedIndicator",
			   rtrim(QAPP_APPROVAL_AGENCY) as "Project/QAPPApprovalAgencyName"
			   from T_WQX_PROJECT 
			   WHERE WQX_SUBMIT_STATUS='U' 
			   and WQX_IND = 1
			   and ACT_IND = 1
			   and PROJECT_IDX = @RecordIDX
			   for xml path('')
			);
	END


	--****************************************************************
	--************MONITORING_LOCATION ********************************
	--****************************************************************
	set @strMon = ''

	if (@TypeText='MLOC') 
	BEGIN
		select @strMon = (
		select distinct rtrim(MONLOC_ID) as "MonitoringLocation/MonitoringLocationIdentity/MonitoringLocationIdentifier",
			   ISNULL(rtrim(MONLOC_NAME),'') as "MonitoringLocation/MonitoringLocationIdentity/MonitoringLocationName",
			   ISNULL(rtrim(MONLOC_TYPE),'') as "MonitoringLocation/MonitoringLocationIdentity/MonitoringLocationTypeName",
			   ISNULL(rtrim(MONLOC_DESC), '') as "MonitoringLocation/MonitoringLocationIdentity/MonitoringLocationDescriptionText",
			   rtrim(HUC_EIGHT)  as "MonitoringLocation/MonitoringLocationIdentity/HUCEightDigitCode",
			   rtrim(HUC_TWELVE) AS "MonitoringLocation/MonitoringLocationIdentity/HUCTwelveDigitCode",
			   case when TRIBAL_LAND_IND = 'Y' then 'true' else 'false' end AS "MonitoringLocation/MonitoringLocationIdentity/TribalLandIndicator", 
			   rtrim(TRIBAL_LAND_NAME) AS "MonitoringLocation/MonitoringLocationIdentity/TribalLandName",
			   NULL AS "MonitoringLocation/MonitoringLocationIdentity/AlternateMonitoringLocationIdentity/MonitoringLocationIdentifier",
			   NULL AS "MonitoringLocation/MonitoringLocationIdentity/AlternateMonitoringLocationIdentity/MonitoringLocationIdentifierContext",
			   ISNULL(rtrim(LATITUDE_MSR), 0) as "MonitoringLocation/MonitoringLocationGeospatial/LatitudeMeasure",
			   ISNULL(rtrim(LONGITUDE_MSR), 0) as "MonitoringLocation/MonitoringLocationGeospatial/LongitudeMeasure",
			   ISNULL(rtrim(SOURCE_MAP_SCALE),0) AS "MonitoringLocation/MonitoringLocationGeospatial/SourceMapScaleNumeric", 
			   ISNULL(HORIZ_COLL_METHOD,'Unknown') as "MonitoringLocation/MonitoringLocationGeospatial/HorizontalCollectionMethodName",
			   ISNULL(HORIZ_REF_DATUM,'UNKWN') as "MonitoringLocation/MonitoringLocationGeospatial/HorizontalCoordinateReferenceSystemDatumName",
			   case when nullif(VERT_MEASURE,'') is not null then rtrim(VERT_MEASURE) else null end as "MonitoringLocation/MonitoringLocationGeospatial/VerticalMeasure/MeasureValue",
			   case when nullif(VERT_MEASURE,'') is not null then rtrim(VERT_MEASURE_UNIT) else null end as "MonitoringLocation/MonitoringLocationGeospatial/VerticalMeasure/MeasureUnitCode",
			   case when nullif(VERT_MEASURE,'') is not null then ISNULL(VERT_COLL_METHOD,'Other') else null end as "MonitoringLocation/MonitoringLocationGeospatial/VerticalCollectionMethodName",
			   case when nullif(VERT_MEASURE,'') is not null then ISNULL(VERT_REF_DATUM,'OTHER') else null end as "MonitoringLocation/MonitoringLocationGeospatial/VerticalCoordinateReferenceSystemDatumName",
			   ISNULL(rtrim(COUNTRY_CODE),'US') as  "MonitoringLocation/MonitoringLocationGeospatial/CountryCode", 
			   rtrim(STATE_CODE) as "MonitoringLocation/MonitoringLocationGeospatial/StateCode", 
			   rtrim(COUNTY_CODE) as "MonitoringLocation/MonitoringLocationGeospatial/CountyCode",
			   nullif(WELL_TYPE,'') as "MonitoringLocation/WellInformation/WellTypeText",
			   nullif(AQUIFER_NAME,'') as "MonitoringLocation/WellInformation/AquiferName",
			   nullif(FORMATION_TYPE,'') as "MonitoringLocation/WellInformation/FormationTypeText",
			   nullif(WELLHOLE_DEPTH_MSR,'') "MonitoringLocation/WellInformation/WellHoleDepthMeasure/MeasureValue",
			   nullif(WELLHOLE_DEPTH_MSR_UNIT,'') "MonitoringLocation/WellInformation/WellHoleDepthMeasure/MeasureUnitCode"
			   from T_WQX_MONLOC 
			   WHERE WQX_SUBMIT_STATUS='U' 
			   and WQX_IND=1
			   and ACT_IND=1
			   and MONLOC_IDX = @RecordIDX
			   for xml path('')
			)
	END

	--****************************************************************
	--************BIO INDICES ****************************************
	--****************************************************************
	set @strBioIndex = '';
	if (@TypeText='IDX') 
	BEGIN
	select @strBioIndex = (
		select distinct rtrim(B.INDEX_ID) as "BiologicalHabitatIndex/IndexIdentifier",
			   rtrim(B.INDEX_TYPE_ID) as "BiologicalHabitatIndex/IndexType/IndexTypeIdentifier",
			   rtrim(B.INDEX_TYPE_ID_CONTEXT) as "BiologicalHabitatIndex/IndexType/IndexTypeIdentifierContext",
			   rtrim(B.INDEX_TYPE_NAME) as "BiologicalHabitatIndex/IndexType/IndexTypeName",
			   rtrim(B.INDEX_SCORE)  as "BiologicalHabitatIndex/IndexScoreNumeric",
			   rtrim(B.INDEX_QUAL_CD) as "BiologicalHabitatIndex/IndexQualifierCode",
			   rtrim(B.INDEX_COMMENT) as "BiologicalHabitatIndex/IndexCommentText",
			   LEFT(CONVERT(VARCHAR, B.INDEX_CALC_DATE, 120), 10) as "BiologicalHabitatIndex/IndexCalculatedDate"
			   from T_WQX_BIO_HABITAT_INDEX B
			   WHERE B.WQX_SUBMIT_STATUS='U' 
			   and B.WQX_IND = 1
			   and B.ACT_IND = 1
			   and B.BIO_HABITAT_INDEX_IDX = @RecordIDX
			   for xml path('')
			)
	END


	--****************************************************************
	--************ACTIVITY *******************************************
	--****************************************************************
	set @strActivity = ''
	if (@TypeText='ACT') 
	BEGIN
		select @strActivity = (
		select rtrim(ACTIVITY_ID) as "ActivityDescription/ActivityIdentifier",
			   ISNULL(rtrim(ACT_TYPE),'') as "ActivityDescription/ActivityTypeCode",
			   ISNULL(rtrim(ACT_MEDIA),'') as "ActivityDescription/ActivityMediaName",
			   rtrim(ACT_SUBMEDIA) as "ActivityDescription/ActivityMediaSubdivisionName",
			   LEFT(CONVERT(VARCHAR, ACT_START_DT, 120), 10) as "ActivityDescription/ActivityStartDate",
			   RIGHT(CONVERT(VARCHAR, ACT_START_DT, 120), 8) as "ActivityDescription/ActivityStartTime/Time",
               case when ACT_START_DT is not null then ISNULL(nullif(ACT_TIME_ZONE,''), 'CST') else null end as "ActivityDescription/ActivityStartTime/TimeZoneCode", 
               LEFT(CONVERT(VARCHAR, ACT_END_DT, 120), 10) as "ActivityDescription/ActivityEndDate",
               RIGHT(CONVERT(VARCHAR, ACT_END_DT, 120), 8) AS "ActivityDescription/ActivityEndTime/Time",
               case when ACT_END_DT is not null then ISNULL(nullif(ACT_TIME_ZONE,''), 'CST') else null end as "ActivityDescription/ActivityEndTime/TimeZoneCode", 
			   rtrim(RELATIVE_DEPTH_NAME) as "ActivityDescription/ActivityRelativeDepthName", 
			   rtrim(ACT_DEPTHHEIGHT_MSR) AS "ActivityDescription/ActivityDepthHeightMeasure/MeasureValue", 
               rtrim(ACT_DEPTHHEIGHT_MSR_UNIT) AS "ActivityDescription/ActivityDepthHeightMeasure/MeasureUnitCode",
	           rtrim(TOP_DEPTHHEIGHT_MSR) AS "ActivityDescription/ActivityTopDepthHeightMeasure/MeasureValue", 
			   rtrim(TOP_DEPTHHEIGHT_MSR_UNIT) AS "ActivityDescription/ActivityTopDepthHeightMeasure/MeasureUnitCode",
	           rtrim(BOT_DEPTHHEIGHT_MSR) AS "ActivityDescription/ActivityBottomDepthHeightMeasure/MeasureValue", 
			   rtrim(BOT_DEPTHHEIGHT_MSR_UNIT) AS "ActivityDescription/ActivityBottomDepthHeightMeasure/MeasureUnitCode",
               rtrim(DEPTH_REF_POINT) as "ActivityDescription/ActivityDepthAltitudeReferencePointText", 
    		   (SELECT p.PROJECT_ID from T_WQX_PROJECT p where p.PROJECT_IDX = T_WQX_ACTIVITY.PROJECT_IDX) AS "ActivityDescription/ProjectIdentifier",
               ORG_ID as "ActivityDescription/ActivityConductingOrganizationText",
               (SELECT MONLOC_ID from T_WQX_MONLOC M WHERE M.MONLOC_IDX = T_WQX_ACTIVITY.MONLOC_IDX)  AS "ActivityDescription/MonitoringLocationIdentifier", 
               ACT_COMMENT as "ActivityDescription/ActivityCommentText",
			   --SAMPLE BIO
	           rtrim(BIO_ASSEMBLAGE_SAMPLED) AS "BiologicalActivityDescription/AssemblageSampledName", 
	           rtrim(BIO_DURATION_MSR) AS "BiologicalActivityDescription/BiologicalHabitatCollectionInformation/CollectionDuration/MeasureValue", 
	           rtrim(BIO_DURATION_MSR_UNIT) AS "BiologicalActivityDescription/BiologicalHabitatCollectionInformation/CollectionDuration/MeasureUnitCode", 
	           rtrim(BIO_REACH_LEN_MSR) AS "BiologicalActivityDescription/BiologicalHabitatCollectionInformation/ReachLengthMeasure/MeasureValue", 
	           rtrim(BIO_REACH_LEN_MSR_UNIT) AS "BiologicalActivityDescription/BiologicalHabitatCollectionInformation/ReachLengthMeasure/MeasureUnitCode", 
	           rtrim(BIO_REACH_WID_MSR) AS "BiologicalActivityDescription/BiologicalHabitatCollectionInformation/ReachWidthMeasure/MeasureValue", 
	           rtrim(BIO_REACH_WID_MSR_UNIT) AS "BiologicalActivityDescription/BiologicalHabitatCollectionInformation/ReachWidthMeasure/MeasureUnitCode", 

			   --SAMPLE DESCRIPTION
			   case when T_WQX_ACTIVITY.SAMP_COLL_METHOD_IDX is not null then SCL.SAMP_COLL_METHOD_ID else null end as "SampleDescription/SampleCollectionMethod/MethodIdentifier", 
			   case when T_WQX_ACTIVITY.SAMP_COLL_METHOD_IDX is not null then SCL.SAMP_COLL_METHOD_CTX else null end as "SampleDescription/SampleCollectionMethod/MethodIdentifierContext", 
			   case when T_WQX_ACTIVITY.SAMP_COLL_METHOD_IDX is not null then SCL.SAMP_COLL_METHOD_NAME else null end as "SampleDescription/SampleCollectionMethod/MethodName", 
               rtrim(SAMP_COLL_EQUIP) AS "SampleDescription/SampleCollectionEquipmentName",
               rtrim(SAMP_COLL_EQUIP_COMMENT) AS "SampleDescription/SampleCollectionEquipmentCommentText",

					--RESULT
    				(SELECT rtrim(DATA_LOGGER_LINE) AS "ResultDescription/DataLoggerLineName",
    				case when RESULT_MSR ='ND' then 'Not Detected' 
    				     when RESULT_MSR = 'NR' then 'Not Reported'
    				     when RESULT_MSR = 'PAQL' then 'Present Above Quantification Limit'
    				     when RESULT_MSR = 'PBQL' then 'Present Below Quantification Limit'
    				     when RESULT_MSR = 'DNQ' then 'Detected Not Quantified' 
    				     else rtrim(RESULT_DETECT_CONDITION) END AS "ResultDescription/ResultDetectionConditionText", 
    				rtrim(CHAR_NAME) AS "ResultDescription/CharacteristicName",
    				rtrim(METHOD_SPECIATION_NAME) as "ResultDescription/MethodSpeciationName",
    				rtrim(RESULT_SAMP_FRACTION) AS "ResultDescription/ResultSampleFractionText",
					case when RESULT_MSR in ('ND', 'NR', 'PAQL', 'PBQL', 'DNQ') then null else rtrim(RESULT_MSR) END AS "ResultDescription/ResultMeasure/ResultMeasureValue",
					rtrim(RESULT_MSR_UNIT) AS "ResultDescription/ResultMeasure/MeasureUnitCode",
					rtrim(RESULT_MSR_QUAL) AS "ResultDescription/ResultMeasure/MeasureQualifierCode",
                    coalesce(nullif(RESULT_STATUS,''),'Final') AS "ResultDescription/ResultStatusIdentifier",
                    rtrim(STATISTIC_BASE_CODE) AS "ResultDescription/StatisticalBaseCode",
                    coalesce(nullif(RESULT_VALUE_TYPE,''),'Actual') AS "ResultDescription/ResultValueTypeName",
                    RTRIM(WEIGHT_BASIS) AS "ResultDescription/ResultWeightBasisText",
                    rtrim(TIME_BASIS) AS "ResultDescription/ResultTimeBasisText",
                    rtrim(TEMP_BASIS) AS "ResultDescription/ResultTemperatureBasisText",
					rtrim(PARTICLESIZE_BASIS) AS "ResultDescription/ResultParticleSizeBasisText",
					rtrim(PRECISION_VALUE) AS "ResultDescription/DataQuality/PrecisionValue",
					rtrim(BIAS_VALUE) AS "ResultDescription/DataQuality/BiasValue",
					rtrim(CONFIDENCE_INTERVAL_VALUE) AS "ResultDescription/DataQuality/ConfidenceIntervalValue",
					rtrim(UPPER_CONFIDENCE_LIMIT) AS "ResultDescription/DataQuality/UpperConfidenceLimitValue",
					rtrim(LOWER_CONFIDENCE_LIMIT) AS "ResultDescription/DataQuality/LowerConfidenceLimitValue",
					rtrim(RESULT_COMMENT) AS "ResultDescription/ResultCommentText",
					rtrim(DEPTH_HEIGHT_MSR) AS "ResultDescription/ResultDepthHeightMeasure/MeasureValue",
					rtrim(DEPTH_HEIGHT_MSR_UNIT) AS "ResultDescription/ResultDepthHeightMeasure/MeasureUnitCode",
					rtrim(DEPTHALTITUDEREFPOINT) AS "ResultDescription/ResultDepthAltitudeReferencePointText",
					rtrim(RESULT_SAMP_POINT) AS "ResultDescription/ResultSamplingPointName",
					
					case when nullif(BIO_SUBJECT_TAXONOMY,'') is not null then rtrim(BIO_INTENT_NAME) else null end as "BiologicalResultDescription/BiologicalIntentName",
					case when nullif(BIO_SUBJECT_TAXONOMY,'') is not null then rtrim(BIO_INDIVIDUAL_ID) else null end AS "BiologicalResultDescription/BiologicalIndividualIdentifier",
					case when nullif(BIO_SUBJECT_TAXONOMY,'') is not null then rtrim(BIO_SUBJECT_TAXONOMY) else null end as "BiologicalResultDescription/SubjectTaxonomicName",
					case when nullif(BIO_SUBJECT_TAXONOMY,'') is not null then rtrim(BIO_UNIDENTIFIED_SPECIES_ID) else null end AS "BiologicalResultDescription/UnidentifiedSpeciesIdentifier",
					case when nullif(BIO_SUBJECT_TAXONOMY,'') is not null then rtrim(BIO_SAMPLE_TISSUE_ANATOMY) else null end as "BiologicalResultDescription/SampleTissueAnatomyName",
					
					rtrim(AM.ANALYTIC_METHOD_ID) as "ResultAnalyticalMethod/MethodIdentifier",
					rtrim(AM.ANALYTIC_METHOD_CTX) as "ResultAnalyticalMethod/MethodIdentifierContext",
					rtrim(AM.ANALYTIC_METHOD_NAME) AS "ResultAnalyticalMethod/MethodName",
					null AS "ResultAnalyticalMethod/MethodQualifierTypeName",
					rtrim(AM.ANALYTIC_METHOD_DESC) AS "ResultAnalyticalMethod/MethodDescriptionText",
					rtrim(LL.LAB_NAME) AS "ResultLabInformation/LaboratoryName",
 				    LEFT(CONVERT(VARCHAR, LAB_ANALYSIS_START_DT, 120), 10) as "ResultLabInformation/AnalysisStartDate", 
					RIGHT(CONVERT(VARCHAR, LAB_ANALYSIS_START_DT, 120), 8) AS "ResultLabInformation/AnalysisStartTime/Time" , 
					CASE WHEN LAB_ANALYSIS_START_DT IS NOT NULL THEN 'CST' ELSE NULL END AS "ResultLabInformation/AnalysisStartTime/TimeZoneCode", 
 				    LEFT(CONVERT(VARCHAR, LAB_ANALYSIS_END_DT, 120), 10) as "ResultLabInformation/AnalysisEndDate", 
					RIGHT(CONVERT(VARCHAR, LAB_ANALYSIS_END_DT, 120), 8) AS "ResultLabInformation/AnalysisEndTime/Time", 
				    CASE WHEN LAB_ANALYSIS_END_DT IS NOT NULL THEN 'CST' ELSE NULL END AS "ResultLabInformation/AnalysisEndTime/TimeZoneCode",
					rtrim(RESULT_LAB_COMMENT_CODE) AS "ResultLabInformation/ResultLaboratoryCommentCode",
					--DETECTION LIMIT HANDLING
					case when (RESULT_MSR in ('ND','NR','PAQL','PBQL','DNQ') or nullif(R.DETECTION_LIMIT,'') is not null or nullif(R.LAB_REPORTING_LEVEL,'') is not null or nullif(R.PQL,'') is not null or nullif(R.LOWER_QUANT_LIMIT,'') is not null or nullif(R.UPPER_QUANT_LIMIT,'') is not null) then 
						 case when nullif(R.DETECTION_LIMIT_TYPE,'') is not null then rtrim(R.DETECTION_LIMIT_TYPE) 
						      when nullif(DETECTION_LIMIT,'') is not null then 'Method Detection Level'
						      when nullif(LAB_REPORTING_LEVEL,'') is not null then 'Laboratory Reporting Level'
						      when nullif(PQL,'') is not null then 'Practical Quantitation Limit'
						      when nullif(LOWER_QUANT_LIMIT,'') is not null then 'Lower Quantitation Limit'
						      when nullif(UPPER_QUANT_LIMIT,'') is not null then 'Upper Quantitation Limit'
							  else  'Estimated Detection Level' end
					end AS "ResultLabInformation/ResultDetectionQuantitationLimit/DetectionQuantitationLimitTypeName",
					case when (RESULT_MSR in ('ND','NR','PAQL','PBQL','DNQ') or nullif(R.DETECTION_LIMIT,'') is not null or nullif(R.LAB_REPORTING_LEVEL,'') is not null or nullif(R.PQL,'') is not null or nullif(R.LOWER_QUANT_LIMIT,'') is not null or nullif(R.UPPER_QUANT_LIMIT,'') is not null) then 
						case when nullif(DETECTION_LIMIT,'') is not null then DETECTION_LIMIT 
						      when nullif(LAB_REPORTING_LEVEL,'') is not null then LAB_REPORTING_LEVEL
						      when nullif(PQL,'') is not null then PQL
						      when nullif(LOWER_QUANT_LIMIT,'') is not null then LOWER_QUANT_LIMIT
						      when nullif(UPPER_QUANT_LIMIT,'') is not null then UPPER_QUANT_LIMIT					     
							  else '0' end
					end AS "ResultLabInformation/ResultDetectionQuantitationLimit/DetectionQuantitationLimitMeasure/MeasureValue",
					case when (RESULT_MSR in ('ND','NR','PAQL','PBQL','DNQ') or nullif(R.DETECTION_LIMIT,'') is not null or nullif(R.LAB_REPORTING_LEVEL,'') is not null or nullif(R.PQL,'') is not null or nullif(R.LOWER_QUANT_LIMIT,'') is not null or nullif(R.UPPER_QUANT_LIMIT,'') is not null) then 
						case when nullif(RESULT_MSR_UNIT,'') is not null then RESULT_MSR_UNIT ELSE 
							(select isnull(rc.DEFAULT_UNIT,'None') from T_WQX_REF_CHARACTERISTIC rc where rc.CHAR_NAME = R.CHAR_NAME) end end AS "ResultLabInformation/ResultDetectionQuantitationLimit/DetectionQuantitationLimitMeasure/MeasureUnitCode"
    				FROM T_WQX_RESULT R 
					LEFT JOIN T_WQX_REF_ANAL_METHOD AM on R.ANALYTIC_METHOD_IDX = AM.ANALYTIC_METHOD_IDX
					LEFT JOIN T_WQX_REF_LAB LL on R.LAB_IDX = LL.LAB_IDX
    				where R.ACTIVITY_IDX = T_WQX_ACTIVITY.ACTIVITY_IDX 
--					and (RESULT_MSR not in ('ND','NR','PAQL','PBQL','DNQ') or DETECTION_LIMIT is not null)
					for xml path('Result'),type ) 
			   from T_WQX_ACTIVITY
			   left join T_WQX_REF_SAMP_COL_METHOD SCL on T_WQX_ACTIVITY.SAMP_COLL_METHOD_IDX = SCL.SAMP_COLL_METHOD_IDX
			   WHERE WQX_SUBMIT_STATUS='U' 
			   and WQX_IND = 1
			   and T_WQX_ACTIVITY.ACT_IND = 1
			   and ACTIVITY_IDX = @RecordIDX
			   for xml path('Activity')
			)
	END


	-- *************************************************************************
	-- ***************** COMBINE ***********************************************
	-- *************************************************************************
	set @strWQX = '<?xml version="1.0" encoding="UTF-8"?>
	<Document Id="UI_' + convert(varchar, getdate(), 112) + '" xmlns="http://www.exchangenetwork.net/schema/v1.0/ExchangeNetworkDocument.xsd">
		<Header>
			<Author>Doug Timms</Author>
			<Organization>EPA</Organization>
			<Title>WQX</Title>
			<CreationTime>' + LEFT(CONVERT(varchar, getdate(), 120), 10) + 'T' + RIGHT(convert(varchar, getdate(), 120), 8) + '</CreationTime>
			<ContactInfo>doug.timms@open-environment.org</ContactInfo>
		</Header>
		<Payload Operation="Update-Insert">
	<WQX xmlns="http://www.exchangenetwork.net/schema/wqx/2" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:schemaLocation="http://www.exchangenetwork.net/schema/wqx/2 
	http://www.exchangenetwork.net/schema/WQX/2/index.xsd">
		<Organization>' + @strOrg + isnull(@strProj,'') + isnull(@strMon,'') + isnull(@strBioIndex,'') + isnull(@strActivity,'') + isnull(@strActGrp,'') + '</Organization></WQX></Payload></Document>'
	
	select @strWQX;

	--IF DEBUG MODE, WRITE TO TRANSACTION LOG
	select @logLevel = (select SETTING_VALUE from T_OE_APP_SETTINGS where SETTING_NAME = 'Log Level');
	if @logLevel='DEBUG' 
	BEGIN
		insert into T_WQX_TRANSACTION_LOG(TABLE_CD, TABLE_IDX, SUBMIT_DT, SUBMIT_TYPE, RESPONSE_TXT) values ('',0,GetDate(), 'I', @strWQX);
	END
END

GO


