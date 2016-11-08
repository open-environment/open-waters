/*
Only run this script if upgrading from v1.8x to v1.9x

Version 1.9x Change Log
--------------------
ENHANCEMENTS: 
  1. Automatically remove commas (e.g. thousand separators) when importing result values
  2. When importing using cross tab, only validate a result if a result value is reported
  3. When importing using cross tab, warn if activity ID already exists
  4. Organizations can now specify a default lower and upper quantitation limit for each characteristic, which is then automatically applied when PABL or PBQL is reported
  5. Organizations can now specify a default analytical method for each characteristic, which is then automatically applied if none is explicitly included in an import file
  6. When importing using traditional sample template, column headers no longer must be case sensitive.
  7. When importing using traditional sample template, sample preparation date and time can now be stored in separate columns (or combined in a single column)
  8. When importing using traditional sample template, analysis start date and time can now be stored in separate columns (or combined in a single column)

1.9.1 Changes
--------------
  1. When importing using cross tab, added ability to specify default Sample Collection Equipment and Collection Method when defining the template
  2. New validation check when importing samples: if activity type includes "Sample", then Sample Collection Method and Equipments are required
  3. Improved import template configuration screen: now when specifying hard coded fields, you can select reference data from drop-down instead of typing in.

1.9.2 Changes
--------------
  1. Updated Terms and Conditions
  2. Added Description field to Admin --> Global Settings page 
  3. Three new global settings: Hosting Organization Name, Registration Message, and Notify Register 
  4. Added an option to display custom message on registration page
  5. Added an option to have the site administrator get automatically emailed any time a new user registers an account
  6. Moved 3rd party javascript dependency (list.js) to local resource
  7. Added a message informing new users to contact STORET helpdesk if WQX organization ID cannot be found in list

1.9.3 Changes
--------------
  1. Fix error in data validation when PAQL or PBQL reported along with PQL.


1.9.6 Changes
--------------
  1. Add Import Translation screen to allow people to manage translations


1.9.7 Changes
-------------
  1. Add Assessment Report module, including submission to EPA ATTAINS
  2. Added biological monitoring import 
  3. Fix error when importing Lab Analysis Date or Lab Sample Prep date and leaving blank
  4. Fix error on activity delete.
  5. Improve speed of file download at WQX History page.
*/


ALTER TABLE T_WQX_REF_CHAR_ORG ADD DEFAULT_LOWER_QUANT_LIMIT varchar(12) NULL;
ALTER TABLE T_WQX_REF_CHAR_ORG ADD DEFAULT_UPPER_QUANT_LIMIT varchar(12) NULL;


--1.9.2
INSERT INTO T_OE_APP_SETTINGS ([SETTING_NAME],[SETTING_VALUE],[SETTING_DESC],[MODIFY_USERID],[MODIFY_DT]) VALUES ('Hosting Org','Open Environment Software','The name of the organization hosting this installation of Open Waters. This is used on the Terms and Conditions.','SYSTEM',GetDate());
--  INSERT INTO T_OE_APP_SETTINGS ([SETTING_NAME],[SETTING_VALUE],[SETTING_DESC],[MODIFY_USERID],[MODIFY_DT]) VALUES ('Signup Message','If you are authorized to collect and submit WQX data, you can register for a FREE Open Waters account and submit up to 100 samples per year. ','An optional message that can be displayed on the account registration page.','SYSTEM',GetDate());
INSERT INTO T_OE_APP_SETTINGS ([SETTING_NAME],[SETTING_VALUE],[SETTING_DESC],[MODIFY_USERID],[MODIFY_DT]) VALUES ('Signup Message','','An optional message that can be displayed on the account registration page.','SYSTEM',GetDate());
INSERT INTO T_OE_APP_SETTINGS ([SETTING_NAME],[SETTING_VALUE],[SETTING_DESC],[MODIFY_USERID],[MODIFY_DT]) VALUES ('Notify Register','Y','When set to Y Open Waters will send a notification email to the site administrator any time a new user registers an account.','SYSTEM',GetDate());

--1.9.7
ALTER TABLE T_WQX_ORGANIZATION ADD MAILING_ADDRESS varchar(30);
ALTER TABLE T_WQX_ORGANIZATION ADD MAILING_ADDRESS2 varchar(30);
ALTER TABLE T_WQX_ORGANIZATION ADD MAILING_ADD_CITY varchar(25);
ALTER TABLE T_WQX_ORGANIZATION ADD MAILING_ADD_STATE varchar(2);
ALTER TABLE T_WQX_ORGANIZATION ADD MAILING_ADD_ZIP varchar(14);


CREATE TABLE [dbo].[T_ATTAINS_REPORT](
	[ATTAINS_REPORT_IDX] [int] IDENTITY(1,1) NOT NULL,
	[ORG_ID] [varchar](30) NOT NULL,
	[REPORT_NAME] [varchar](100) NOT NULL,
	[DATA_FROM] [datetime] NULL,
	[DATA_TO] [datetime] NULL,
	[ATTAINS_IND] [bit] NULL,
	[ATTAINS_SUBMIT_STATUS] [varchar](1) NULL,
	[ATTAINS_UPDATE_DT] [datetime] NULL,
	[CREATE_DT] [datetime] NULL,
	[CREATE_USERID] [varchar](25) NULL,
 CONSTRAINT [PK_ATTAINS_REPORT] PRIMARY KEY CLUSTERED  ([ATTAINS_REPORT_IDX]),
 FOREIGN KEY (ORG_ID) references T_WQX_ORGANIZATION (ORG_ID) ON UPDATE CASCADE ON DELETE CASCADE, 
) ON [PRIMARY];


CREATE TABLE [dbo].[T_ATTAINS_REF_WATER_TYPE](
	[WATER_TYPE_CODE] [varchar](40) NOT NULL,
	[CREATE_DT] [datetime] NULL,
	[CREATE_USERID] [varchar](25) NULL,
 CONSTRAINT [PK_ATTAINS_REF_WATER_TYPE] PRIMARY KEY CLUSTERED  ([WATER_TYPE_CODE])
) ON [PRIMARY];


CREATE TABLE [dbo].[T_ATTAINS_ASSESS_UNITS](
	[ATTAINS_ASSESS_UNIT_IDX] [int] IDENTITY(1,1) NOT NULL,
	[ATTAINS_REPORT_IDX] [int] NOT NULL,
	[ASSESS_UNIT_ID] [varchar](50) NOT NULL,
	[ASSESS_UNIT_NAME] [varchar](255) NULL,
	[LOCATION_DESC] [varchar](2000) NULL,
	[AGENCY_CODE] [varchar](1) NULL,
	[STATE_CODE] [varchar](2) NULL,
	[ACT_IND] [varchar](1) NULL,
	[WATER_TYPE_CODE] [varchar](40) NULL,
	[WATER_SIZE] [decimal](18,4) NULL,
	[WATER_UNIT_CODE] [varchar](15) NULL,
	[USE_CLASS_CODE] [varchar](15) NULL,
	[USE_CLASS_NAME] [varchar](50) NULL,
	[CREATE_DT] [datetime] NULL,
	[CREATE_USERID] [varchar](25) NULL,
	[MODIFY_DT] [datetime] NULL,
	[MODIFY_USERID] [varchar](25) NULL,
 CONSTRAINT [PK_ATTAINS_ASSESS_UNIT] PRIMARY KEY CLUSTERED  ([ATTAINS_ASSESS_UNIT_IDX]),
 FOREIGN KEY ([ATTAINS_REPORT_IDX]) references [T_ATTAINS_REPORT] ([ATTAINS_REPORT_IDX]) ON UPDATE CASCADE ON DELETE CASCADE, 
 FOREIGN KEY ([WATER_TYPE_CODE]) references [T_ATTAINS_REF_WATER_TYPE] ([WATER_TYPE_CODE]) ON UPDATE CASCADE ON DELETE CASCADE
) ON [PRIMARY];


CREATE TABLE [dbo].[T_ATTAINS_ASSESS_UNITS_MLOC](
	[ATTAINS_ASSESS_UNIT_IDX] [int] NOT NULL,
	[MONLOC_IDX] [int] NOT NULL,
	[CREATE_DT] [datetime] NULL,
	[CREATE_USERID] [varchar](25) NULL,
	[MODIFY_DT] [datetime] NULL,
	[MODIFY_USERID] [varchar](25) NULL,
 CONSTRAINT [PK_ATTAINS_ASSESS_UNIT_MLOC] PRIMARY KEY CLUSTERED  ([ATTAINS_ASSESS_UNIT_IDX],[MONLOC_IDX]),
 FOREIGN KEY ([ATTAINS_ASSESS_UNIT_IDX]) references [T_ATTAINS_ASSESS_UNITS] ([ATTAINS_ASSESS_UNIT_IDX]) ON UPDATE CASCADE ON DELETE CASCADE, 
 FOREIGN KEY ([MONLOC_IDX]) references [T_WQX_MONLOC] ([MONLOC_IDX]) 
) ON [PRIMARY];



CREATE TABLE [dbo].[T_ATTAINS_ASSESS](
	[ATTAINS_ASSESS_IDX] [int] IDENTITY(1,1) NOT NULL,
	[REPORTING_CYCLE] [varchar](4) NOT NULL,
	[REPORT_STATUS] [varchar](30) NOT NULL,
	[ATTAINS_ASSESS_UNIT_IDX] [int] NOT NULL,
	[AGENCY_CODE] [varchar](1) NULL,
	[CYCLE_LAST_ASSESSED] [varchar](4) NULL,
	[CYCLE_LAST_MONITORED] [varchar](4) NULL,
	[TROPHIC_STATUS_CODE] [varchar](30) NULL,
	[CREATE_DT] [datetime] NULL,
	[CREATE_USERID] [varchar](25) NULL,
	[MODIFY_DT] [datetime] NULL,
	[MODIFY_USERID] [varchar](25) NULL,
 CONSTRAINT [PK_ATTAINS_ASSESS] PRIMARY KEY CLUSTERED  ([ATTAINS_ASSESS_IDX]),
 FOREIGN KEY ([ATTAINS_ASSESS_UNIT_IDX]) references [T_ATTAINS_ASSESS_UNITS] ([ATTAINS_ASSESS_UNIT_IDX]) ON UPDATE CASCADE ON DELETE CASCADE
) ON [PRIMARY];

CREATE TABLE [dbo].[T_ATTAINS_ASSESS_USE](
	[ATTAINS_ASSESS_USE_IDX] [int] IDENTITY(1,1) NOT NULL,
	[ATTAINS_ASSESS_IDX] [int] NOT NULL,
	[USE_NAME] [varchar](255) NULL,
	[USE_ATTAINMENT_CODE] [varchar](1) NULL,
	[THREATENED_IND] [varchar](1) NULL,
	[TREND_CODE] [varchar](25) NULL,
	[IR_CAT_CODE] [varchar](5) NULL,
	[IR_CAT_DESC] [varchar](255) NULL,
	[ASSESS_BASIS] [varchar](30) NULL,
	[ASSESS_TYPE] [varchar](30) NULL,
	[ASSESS_CONFIDENCE] [varchar](30) NULL,
	[MON_DATE_START] [datetime] NULL,
	[MON_DATE_END] [datetime] NULL,
	[ASSESS_DATE] [datetime] NULL,
	[ASSESSOR_NAME] [varchar](80) NULL,
	[CREATE_DT] [datetime] NULL,
	[CREATE_USERID] [varchar](25) NULL,
	[MODIFY_DT] [datetime] NULL,
	[MODIFY_USERID] [varchar](25) NULL,
 CONSTRAINT [PK_ATTAINS_ASSESS_USE] PRIMARY KEY CLUSTERED  ([ATTAINS_ASSESS_USE_IDX]),
 FOREIGN KEY ([ATTAINS_ASSESS_IDX]) references [T_ATTAINS_ASSESS] ([ATTAINS_ASSESS_IDX]) ON UPDATE CASCADE ON DELETE CASCADE
) ON [PRIMARY];


CREATE TABLE [dbo].[T_ATTAINS_ASSESS_USE_PAR](
	[ATTAINS_ASSESS_USE_PAR_IDX] [int] IDENTITY(1,1) NOT NULL,
	[ATTAINS_ASSESS_USE_IDX] [int] NOT NULL,
	[PARAM_NAME] [varchar](240) NULL,
	[PARAM_ATTAINMENT_CODE] [varchar](1) NULL,
	[TREND_CODE] [varchar](25) NULL,
	[PARAM_COMMENT] [varchar](1000) NULL,
	[CREATE_DT] [datetime] NULL,
	[CREATE_USERID] [varchar](25) NULL,
	[MODIFY_DT] [datetime] NULL,
	[MODIFY_USERID] [varchar](25) NULL,
 CONSTRAINT [PK_ATTAINS_ASSESS_USE_PAR] PRIMARY KEY CLUSTERED  ([ATTAINS_ASSESS_USE_PAR_IDX]),
 FOREIGN KEY ([ATTAINS_ASSESS_USE_IDX]) references [T_ATTAINS_ASSESS_USE] ([ATTAINS_ASSESS_USE_IDX]) ON UPDATE CASCADE ON DELETE CASCADE
) ON [PRIMARY];


CREATE TABLE [dbo].[T_ATTAINS_ASSESS_CAUSE](
	[ATTAINS_ASSESS_CAUSE_IDX] [int] IDENTITY(1,1) NOT NULL,
	[ATTAINS_ASSESS_IDX] [int] NOT NULL,
	[CAUSE_NAME] [varchar](240) NULL,
	[POLLUTANT_IND] [varchar](1) NULL,
	[AGENCY_CODE] [varchar](1) NULL,
	[CYCLE_FIRST_LISTED] [varchar](4) NULL,
	[CYCLE_SCHED_TMDL] [varchar](4) NULL,
	[TMDL_PRIORITY_NAME] [varchar](25) NULL,
	[CONSENT_DECREE_CYCLE] [varchar](4) NULL,
	[TMDL_CAUSE_REPORT_ID] [varchar](45) NULL,
	[CYCLE_EXPECTED_ATTAIN] [varchar](4) NULL,
	[CAUSE_COMMENT] [varchar](1000) NULL,
	[CREATE_DT] [datetime] NULL,
	[CREATE_USERID] [varchar](25) NULL,
	[MODIFY_DT] [datetime] NULL,
	[MODIFY_USERID] [varchar](25) NULL,
 CONSTRAINT [PK_ATTAINS_ASSESS_CAUSE] PRIMARY KEY CLUSTERED  ([ATTAINS_ASSESS_CAUSE_IDX]),
 FOREIGN KEY ([ATTAINS_ASSESS_IDX]) references [T_ATTAINS_ASSESS] ([ATTAINS_ASSESS_IDX]) ON UPDATE CASCADE ON DELETE CASCADE
) ON [PRIMARY];



CREATE TABLE [dbo].[T_ATTAINS_REPORT_LOG](
	[ATTAINS_LOG_IDX] [int] NOT NULL IDENTITY(1,1),
	[ATTAINS_REPORT_IDX] [int] NOT NULL,
	[SUBMIT_DT] [datetime] NOT NULL,
	[SUBMIT_FILE] [varchar](max) NULL,
	[RESPONSE_FILE] [varbinary](max) NULL,
	[RESPONSE_TXT] [varchar](max) NULL,
	[CDX_SUBMIT_TRANSID] [varchar](100) NULL,
	[CDX_SUBMIT_STATUS] [varchar](20) NULL,
    [ORG_ID] [varchar](30) NULL,
 CONSTRAINT [PK_ATTAINS_REPORT_LOG] PRIMARY KEY CLUSTERED ([ATTAINS_LOG_IDX] ASC),
 FOREIGN KEY ([ATTAINS_REPORT_IDX]) references [T_ATTAINS_REPORT] ([ATTAINS_REPORT_IDX]) ON UPDATE CASCADE ON DELETE CASCADE, 
) ON [PRIMARY]


GO




insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('BAY',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('BAY OR HARBOR',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('BAY/ESTUARY',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('BAYOU',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('BAYS AND HARBORS',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('BEACH',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('BLACKWATER SYSTEM',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('BRANCH',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('CANAL',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('CHANNEL',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('CIRQUE LAKE',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('COASTAL',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('COASTAL & BAY SHORELINE',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('COASTAL SHORELINES',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('COASTAL WATERS',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('COASTLINE',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('CONNECTING CHANNEL',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('CREEK',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('CREEK, INTERMITTENT',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('DAM',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('DITCH OR CANAL',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('DRAIN',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('EPHEMERAL STREAM',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('ESTUARY',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('ESTUARY (G)',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('FLOWAGE',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('FRESHWATER ESTUARY',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('FRESHWATER LAKE',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('FRESHWATER RESERVOIR',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('FRESHWATER STREAM',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('GREAT LAKES',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('GREAT LAKES BAYS AND HARBORS',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('GREAT LAKES BEACH',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('GREAT LAKES CONNECTING CHANNEL',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('GREAT LAKES OPEN WATER',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('GREAT LAKES SHORELINE',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('GULCH',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('GULF',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('HARBOR',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('HIGH ELEVATION LAKE',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('IMPOUNDMENT',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('INLAND LAKE SHORELINE',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('INLET',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('INTERMITTENT STREAM',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('ISLAND COASTAL WATERS',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('LAGOON',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('LAKE',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('LAKE & RESERVOIR',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('LAKE/RESERVOIR/POND',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('MARSH',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('OCEAN',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('OCEAN/NEAR COASTAL',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('PLAYA LAKE',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('POND',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('RESERVOIR',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('RIVER',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('RIVER & STREAM',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('RIVER INTERMITTENT',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('RIVER PERENNIAL',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('RIVERINE BACKWATER',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('SALINE LAKE',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('SINK HOLE',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('SOUND',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('SPRING',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('SPRINGS-LAKE',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('STREAM',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('STREAM INTERMITTENT',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('STREAM PERENNIAL',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('STREAM/CREEK/RIVER',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('TIDAL RIVER',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('TIDAL STREAM',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('UNKNOWN',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('WASH',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('WATERSHED',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('WETLAND',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('WETLANDS, FRESHWATER',GetDate(),'system');
insert into T_ATTAINS_REF_WATER_TYPE (WATER_TYPE_CODE, CREATE_DT, CREATE_USERID) values ('WETLANDS, TIDAL',GetDate(),'system');

GO

CREATE PROCEDURE [dbo].[GenATTAINSXML]
  @ReportIDX int
AS
BEGIN
	/*
	DESCRIPTION: RETURNS ATTAINS XML FILE FOR A GIVEN REPORT ID

	CHANGE LOG: 10/23/2016 DOUG TIMMS, OPEN-ENVIRONMENT.ORG
	*/
	SET NOCOUNT ON;

	DECLARE @strXML varchar(max);       --output XML

	set @strXML = 
	(select rtrim(O.ORG_ID) as "OrganizationIdentifier",
	rtrim(O.ORG_FORMAL_NAME) as "OrganizationName",
	'Tribe' as "OrganizationType",
	'Assessment' as "OrganizationContact/ContactType",
	'www.open-waters.com' as "OrganizationContact/WebURLText",
	rtrim(O.MAILING_ADDRESS) as "OrganizationContact/MailingAddress/MailingAddressText",
	rtrim(O.MAILING_ADD_CITY) as "OrganizationContact/MailingAddress/MailingAddressCityName",
	rtrim(O.MAILING_ADD_STATE) as "OrganizationContact/MailingAddress/MailingAddressStateUSPSCode",
	rtrim(O.MAILING_ADD_ZIP) as "OrganizationContact/MailingAddress/MailingAddressZIPCode",
		--ASSESSMENT UNITS
		(select rtrim(AU.ASSESS_UNIT_ID) as "AssessmentUnitIdentifier",
		rtrim(AU.ASSESS_UNIT_NAME) as "AssessmentUnitName",		 
		rtrim(AU.LOCATION_DESC) as "LocationDescriptionText",		 
		rtrim(AU.AGENCY_CODE) as "AgencyCode",		 
		rtrim(AU.STATE_CODE) as "StateCode",		 
		rtrim(AU.ACT_IND) as "StatusIndicator",		 
		rtrim(AU.WATER_TYPE_CODE) as "WaterType/WaterTypeCode",		 
		rtrim(AU.WATER_SIZE) as "WaterType/WaterSizeNumber",		 
		rtrim(AU.WATER_UNIT_CODE) as "WaterType/UnitsCode",

			--ASSESSMENT UNITS MONITORING LOCATIONS
			(select case when nullif(O.ORG_FORMAL_NAME,'') is not null then rtrim(O.ORG_FORMAL_NAME) end as "MonitoringOrganizationIdentifier",
			case when nullif(M.MONLOC_ID,'') is not null then rtrim(M.MONLOC_ID) end as "MonitoringLocationIdentifier"
			from T_ATTAINS_ASSESS_UNITS_MLOC AUM, T_WQX_MONLOC M where AU.ATTAINS_ASSESS_UNIT_IDX = AUM.ATTAINS_ASSESS_UNIT_IDX and AUM.MONLOC_IDX = M.MONLOC_IDX
			for xml path('MonitoringStation'), root('AssociatedMonitoringStation'), type),

		case when nullif(AU.USE_CLASS_CODE,'') is not null then rtrim(O.ORG_ID) end as "UseClass/UseClassContext" ,
		case when nullif(AU.USE_CLASS_CODE,'') is not null then rtrim(AU.USE_CLASS_CODE) end as "UseClass/UseClassCode",
		case when nullif(AU.USE_CLASS_CODE,'') is not null then rtrim(AU.USE_CLASS_NAME) end as "UseClass/UseClassName"
		from T_ATTAINS_ASSESS_UNITS AU where AU.ATTAINS_REPORT_IDX = R.ATTAINS_REPORT_IDX
		for xml path('AssessmentUnit'),root('DefinedAssessmentUnits'),type), 	

		--ASSESSMENTS
		(select rtrim(A.REPORTING_CYCLE) as "ReportingCycleText",
		rtrim(A.REPORT_STATUS) as "ReportStatusCode",

			--ASSESSED WATER
			(select 
			rtrim(AU.ASSESS_UNIT_ID) as "AssessmentUnitIdentifier",
			rtrim(A.AGENCY_CODE) as "AgencyCode",
			rtrim(A.CYCLE_LAST_ASSESSED) as "CycleLastAssessedText",
			rtrim(A.CYCLE_LAST_MONITORED) as "YearLastMonitoredText",

				--USE ATTAINMENTS
				(select rtrim(ASSU.USE_NAME) as "UseName",
				rtrim(ASSU.USE_ATTAINMENT_CODE) as "UseAttainmentCode",
				rtrim(ASSU.TREND_CODE) as "TrendCode",
				rtrim(A.AGENCY_CODE) as "AgencyCode",
				rtrim(ASSU.IR_CAT_CODE) as "StateIntergratedReportingCategory/StateIRCategoryCode",
				rtrim(ASSU.IR_CAT_DESC) as "StateIntergratedReportingCategory/StateCategoryDescriptionText",
			
					--ASSESS METADATA
					(select
					rtrim(ASSU.ASSESS_TYPE) as "AssessmentTypes/AssessmentTypeCode",
					rtrim(ASSU.ASSESS_CONFIDENCE) as "AssessmentTypes/AssessmentConfidenceCode",
					LEFT(CONVERT(VARCHAR, ASSU.MON_DATE_START, 120), 10) as "MonitoringActivity/MonitoringStartDate",
					LEFT(CONVERT(VARCHAR, ASSU.MON_DATE_END, 120), 10) as "MonitoringActivity/MonitoringEndDate",

						--ASSESSMENT ACTIVITY				
						(select
						LEFT(CONVERT(VARCHAR, ASSU.ASSESS_DATE, 120), 10) as "AssessmentDate",
						rtrim(ASSU.ASSESSOR_NAME) as "AssessorName",

							--PARAMS ASSESSED
							(select 
							rtrim(PAR.PARAM_NAME) as "ParameterName",
							rtrim(PAR.PARAM_ATTAINMENT_CODE) as "ParameterAttainmentCode",
							rtrim(PAR.TREND_CODE) as "TrendCode",
							rtrim(PAR.PARAM_COMMENT) as "ParameterCommentText"
							from T_ATTAINS_ASSESS_USE_PAR PAR where PAR.ATTAINS_ASSESS_USE_IDX = ASSU.ATTAINS_ASSESS_USE_IDX
							for xml path('ParametersDetails'), root('ParametersAssessed'), type)

						for xml path('AssessmentActivity'), type)  

					for xml path('AssessmentMetadata'), type)  
				
				from T_ATTAINS_ASSESS_USE ASSU where ASSU.ATTAINS_ASSESS_IDX = A.ATTAINS_ASSESS_IDX
				for xml path('UseAttainments'), root('Uses'), type), 	

				--CAUSES
				(select rtrim(C.CAUSE_NAME) as "CauseName",
				rtrim(C.POLLUTANT_IND) as "PollutantIndicator",
				rtrim(C.AGENCY_CODE) as "AgencyCode",
				rtrim(C.CYCLE_FIRST_LISTED) as "ListingInformation/CycleFirstListedText",
				rtrim(C.CYCLE_SCHED_TMDL) as "ListingInformation/CycleScheduledForTMDLText",
	--			rtrim(C.TMDL_PRIORITY_NAME) as "ListingInformation/TMDLPriorityName",
	--			rtrim(C.CONSENT_DECREE_CYCLE) as "ListingInformation/ConsentDecreeCycleText",
	--			rtrim(C.TMDL_CAUSE_REPORT_ID) as "ListingInformation/TMDLCauseReportIdentifier",
				rtrim(C.CAUSE_COMMENT) as "CauseCommentText"
				from T_ATTAINS_ASSESS_CAUSE C where C.ATTAINS_ASSESS_IDX = A.ATTAINS_ASSESS_IDX
				for xml path('CauseDetails'), root('Causes'), type) 	


			for xml path('AssessedWater'), root('CycleAssessments'), type) 	

		from T_ATTAINS_ASSESS_UNITS AU, T_ATTAINS_ASSESS A where AU.ATTAINS_REPORT_IDX = R.ATTAINS_REPORT_IDX and A.ATTAINS_ASSESS_UNIT_IDX = AU.ATTAINS_ASSESS_UNIT_IDX
		for xml path('Assessments'),type) 	

	from T_WQX_ORGANIZATION O, T_ATTAINS_REPORT R
	where O.ORG_ID = R.ORG_ID
	and R.ATTAINS_REPORT_IDX = @ReportIDX
	for xml path('Organization'), root('ATTAINS'));

	select @strXML;

END

