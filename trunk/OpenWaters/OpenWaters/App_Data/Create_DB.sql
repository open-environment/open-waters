/***************************************************************** */
/*************DROP EXISTING DATAABSE (only use if refreshing DB*** */
/***************************************************************** */
/*
	  EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'OpenEnvironment'
	  GO
	  USE [master]
	  GO
	  ALTER DATABASE [OpenEnvironment] SET  SINGLE_USER WITH ROLLBACK IMMEDIATE
	  GO
	  USE [master]
	  GO
	  DROP DATABASE [OpenEnvironment]
	  GO
*/

/************************************************************ */
/*************CREATE DATABASE******************************** */
/************************************************************ */
CREATE DATABASE [OpenEnvironment]
GO

/************************************************************************* */
/*************CREATE USER AND GRANT RIGHTS******************************** */
/************************************************************************* */
IF EXISTS (SELECT * FROM sys.server_principals WHERE name = N'oe_login')
DROP LOGIN [oe_login]


use [OpenEnvironment]
Create login oe_login with password='R!j23@pLZ88$e';
EXEC sp_defaultdb @loginame='oe_login', @defdb='OpenEnvironment' 
Create user [oe_user] for login [oe_login]; 
exec sp_addrolemember 'db_owner', 'oe_user'; 


/************************************************************ */
/*************CREATE TABLES  ******************************** */
/************************************************************ */
Use [OpenEnvironment] 
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[T_OE_APP_SETTINGS](
	[SETTING_IDX] [int] IDENTITY(1,1) NOT NULL,
	[SETTING_NAME] [varchar](100) NOT NULL,
	[SETTING_VALUE] [varchar](200) NULL,
	[ENCRYPT_IND] [bit] NULL,
	[SETTING_VALUE_SALT] [varchar](100) NULL,
	[MODIFY_USERID] [varchar](25) NULL,
	[MODIFY_DT] [datetime2](0) NULL,
 CONSTRAINT [PK_T_OE_APP_SETTINGS] PRIMARY KEY CLUSTERED ([SETTING_IDX] ASC)
WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


INSERT INTO [OpenEnvironment].[dbo].[T_OE_APP_SETTINGS] ([SETTING_NAME],[SETTING_VALUE],[MODIFY_USERID],[MODIFY_DT])
     VALUES ('Default Org ID','','SYSTEM',GetDate());
INSERT INTO [OpenEnvironment].[dbo].[T_OE_APP_SETTINGS] ([SETTING_NAME],[SETTING_VALUE],[MODIFY_USERID],[MODIFY_DT])
     VALUES ('CDX Submission URL','https://testngn.epacdxnode.net/ngn-enws20/services/NetworkNode2ServiceConditionalMTOM','SYSTEM',GetDate());
INSERT INTO [OpenEnvironment].[dbo].[T_OE_APP_SETTINGS] ([SETTING_NAME],[SETTING_VALUE],[MODIFY_USERID],[MODIFY_DT])
     VALUES ('CDX Ref Data URL','http://cdx.epa.gov/WQXWeb/services.asmx','SYSTEM',GetDate());
INSERT INTO [OpenEnvironment].[dbo].[T_OE_APP_SETTINGS] ([SETTING_NAME],[SETTING_VALUE],[MODIFY_USERID],[MODIFY_DT])
     VALUES ('CDX Submitter','','SYSTEM',GetDate());
INSERT INTO [OpenEnvironment].[dbo].[T_OE_APP_SETTINGS] ([SETTING_NAME],[SETTING_VALUE],[ENCRYPT_IND],[SETTING_VALUE_SALT],[MODIFY_USERID],[MODIFY_DT])
     VALUES ('CDX Submitter Password','',1,'','SYSTEM',GetDate());
INSERT INTO [OpenEnvironment].[dbo].[T_OE_APP_SETTINGS] ([SETTING_NAME],[SETTING_VALUE],[MODIFY_USERID],[MODIFY_DT])
     VALUES ('Default Timezone','','SYSTEM',GetDate());
INSERT INTO [OpenEnvironment].[dbo].[T_OE_APP_SETTINGS] ([SETTING_NAME],[SETTING_VALUE],[MODIFY_USERID],[MODIFY_DT])
     VALUES ('Log Level','ERROR','SYSTEM',GetDate());
INSERT INTO [OpenEnvironment].[dbo].[T_OE_APP_SETTINGS] ([SETTING_NAME],[SETTING_VALUE],[MODIFY_USERID],[MODIFY_DT])
     VALUES ('EMAIL FROM','OpenWaters@open-environment.org','SYSTEM',GetDate());
INSERT INTO [OpenEnvironment].[dbo].[T_OE_APP_SETTINGS] ([SETTING_NAME],[SETTING_VALUE],[MODIFY_USERID],[MODIFY_DT])
     VALUES ('EMAIL SERVER','localhost','SYSTEM',GetDate());
	 
	 

/****** Object:  Table [dbo].[T_OE_APP_TASKS]    ******/
CREATE TABLE [dbo].[T_OE_APP_TASKS](
	[TASK_IDX] [int] IDENTITY(1,1) NOT NULL,
	[TASK_NAME] [varchar](30) NOT NULL,
	[TASK_DESC] [varchar](100) NOT NULL,
	[TASK_STATUS] [varchar](10) NOT NULL,
	[TASK_FREQ_MS] [int] NOT NULL,
	[MODIFY_USERID] [varchar](25) NULL,
	[MODIFY_DT] [datetime2](0) NULL,
 CONSTRAINT [PK_T_OE_APP_TASKS] PRIMARY KEY CLUSTERED ([TASK_IDX] ASC)
) ON [PRIMARY]

INSERT INTO [T_OE_APP_TASKS] ([TASK_NAME],[TASK_DESC],[TASK_STATUS],[TASK_FREQ_MS],[MODIFY_USERID],[MODIFY_DT])
    VALUES ('WQXSubmit','Submit WQX data to EPA','STOPPED',10000,'SYSTEM',GetDate());



/****** Object:  Table [dbo].[T_OE_USERS]    ******/
CREATE TABLE [dbo].[T_OE_USERS](
	[USER_IDX] [int] IDENTITY(1,1) NOT NULL,
	[USER_ID] [varchar](25) NOT NULL,
	[PWD_HASH] [varchar](100) NOT NULL,
	[PWD_SALT] [varchar](100) NOT NULL,
	[FNAME] [varchar](40) NOT NULL,
	[LNAME] [varchar](40) NOT NULL,
	[EMAIL] [varchar](150) NULL,
	[INITAL_PWD_FLAG] [bit] NOT NULL,
	[EFFECTIVE_DT] [datetime2](0) NOT NULL,
	[LASTLOGIN_DT] [datetime2](0) NULL,
	[PHONE] [varchar](12) NULL,
	[PHONE_EXT] [varchar](4) NULL,
	[ACT_IND] [bit] NOT NULL,
	[CREATE_USERID] [varchar](25) NULL,
	[CREATE_DT] [datetime2](0) NULL,
	[MODIFY_USERID] [varchar](25) NULL,
	[MODIFY_DT] [datetime2](0) NULL,
 CONSTRAINT [PK_T_OE_USERS] PRIMARY KEY CLUSTERED ( [USER_IDX] ASC)
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[T_OE_ROLES](
	[ROLE_IDX] [int] IDENTITY(1,1) NOT NULL,
	[ROLE_NAME] [varchar](25) NOT NULL,
	[ROLE_DESC] [varchar](100) NOT NULL,
	[CREATE_USERID] [varchar](25) NULL,
	[CREATE_DT] [datetime2](0) NULL,
	[MODIFY_USERID] [varchar](25) NULL,
	[MODIFY_DT] [datetime2](0) NULL,
 CONSTRAINT [PK_T_OE_ROLES] PRIMARY KEY CLUSTERED  ([ROLE_IDX] ASC)
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[T_OE_USER_ROLES](
	[USER_ROLE_IDX] [int] IDENTITY(1,1) NOT NULL,
	[USER_IDX] [int] NOT NULL,
	[ROLE_IDX] [int] NOT NULL,
	[CREATE_USERID] [varchar](25) NULL,
	[CREATE_DT] [datetime2](0) NULL,
 CONSTRAINT [PK_T_OE_USER_ROLES] PRIMARY KEY CLUSTERED ([USER_ROLE_IDX] ASC),
 CONSTRAINT [UK_T_OE_USER_ROLES] UNIQUE NONCLUSTERED (	[USER_IDX] ASC,	[ROLE_IDX] ASC),
 FOREIGN KEY (ROLE_IDX) references T_OE_ROLES (ROLE_IDX) 
	ON UPDATE CASCADE 
	ON DELETE CASCADE, 
 FOREIGN KEY (USER_IDX) references T_OE_USERS (USER_IDX)
 	ON UPDATE CASCADE 
	ON DELETE CASCADE
) ON [PRIMARY]

GO


insert into T_OE_ROLES (ROLE_NAME, ROLE_DESC, CREATE_USERID, CREATE_DT) values ('USERS', 'Application users', 'system', GetDate());
insert into T_OE_ROLES (ROLE_NAME, ROLE_DESC, CREATE_USERID, CREATE_DT) values ('ADMINS', 'Application Administrator', 'system', GetDate());
insert into T_OE_ROLES (ROLE_NAME, ROLE_DESC, CREATE_USERID, CREATE_DT) values ('READONLY', 'People with this role cannot add or edit any data in the system', 'system', GetDate());

insert into T_OE_USERS ([USER_ID], PWD_HASH, PWD_SALT, FNAME, LNAME, ACT_IND, INITAL_PWD_FLAG, EFFECTIVE_DT, LASTLOGIN_DT, PHONE, PHONE_EXT,
 CREATE_USERID, CREATE_DT, MODIFY_USERID, MODIFY_DT)
values ('ADMIN', 'pwd','', 'Admin','Admin',1,1,GETDATE(),null, null, null, 'SYSTEM',GETDATE(),'SYSTEM',GETDATE());

insert into T_OE_USER_ROLES (USER_IDX, ROLE_IDX) values (1,1);
insert into T_OE_USER_ROLES (USER_IDX, ROLE_IDX) values (1,2);

--******************************************************************************************************************************************
--******************************************************************************************************************************************
--************************************************************WQX TABLES  ******************************************************************
--******************************************************************************************************************************************
--******************************************************************************************************************************************
CREATE TABLE [dbo].[T_WQX_REF_LAB](
	[LAB_IDX] [int] NOT NULL IDENTITY(1,1),
	[LAB_NAME] [varchar](60) NOT NULL,
	[LAB_ACCRED_IND] [char](1) NULL,
	[LAB_ACCRED_AUTHORITY] [varchar](20) NULL,
	[ACT_IND] [bit] NULL,
	[CREATE_DT] [datetime] NULL,
	[CREATE_USERID] [varchar](25) NULL,
	[UPDATE_DT] [datetime] NULL,
	[UPDATE_USERID] [varchar](25) NULL,
 CONSTRAINT [PK_WQX_REF_LAB] PRIMARY KEY CLUSTERED (LAB_IDX ASC)
) ON [PRIMARY]


CREATE TABLE [dbo].[T_WQX_REF_ANAL_METHOD](
	[ANALYTIC_METHOD_IDX] [int] NOT NULL IDENTITY(1,1),
	[ANALYTIC_METHOD_ID] [varchar](20) NOT NULL,
	[ANALYTIC_METHOD_CTX] [varchar](120) NOT NULL,
	[ANALYTIC_METHOD_NAME] [varchar](120) NULL,
	[ANALYTIC_METHOD_DESC] [varchar](4000) NULL,
	[ACT_IND] [bit] NULL,
	[UPDATE_DT] [datetime] NULL,
 CONSTRAINT [PK_WQX_REF_ANAL_METHOD] PRIMARY KEY CLUSTERED (ANALYTIC_METHOD_IDX ASC)
) ON [PRIMARY]



CREATE TABLE [dbo].[T_WQX_ORGANIZATION](
	[ORG_ID] [varchar](30) NOT NULL,
	[ORG_FORMAL_NAME] [varchar](120) NOT NULL,
	[ORG_DESC] [varchar](500) NULL,
	[TRIBAL_CODE] [varchar](3) NULL,
	[ELECTRONICADDRESS] [varchar](120) NOT NULL,
	[ELECTRONICADDRESSTYPE] [varchar](8) NULL,
	[TELEPHONE_NUM] [varchar](15) NOT NULL,
	[TELEPHONE_NUM_TYPE] [varchar](6) NULL,
	[TELEPHONE_EXT] [varchar](6) NULL,
	[CREATE_DT] [datetime] NULL,
	[CREATE_USERID] [varchar](25) NULL,
	[UPDATE_DT] [datetime] NULL,
	[UPDATE_USERID] [varchar](25) NULL,
 CONSTRAINT [PK_WQX_ORGANIZATION] PRIMARY KEY CLUSTERED ([ORG_ID] ASC)
) ON [PRIMARY]

CREATE TABLE [dbo].[T_WQX_ORG_ADDRESS](
	[ORG_ID] [varchar](30) NOT NULL,
	[ADDRESS_TYPE] [varchar](8) NOT NULL,
	[ADDRESS] [varchar](50) NOT NULL,
	[SUPP_ADDRESS] [varchar](120) NULL,
	[LOCALITY] [varchar](30) NULL,
	[STATE_CD] [varchar](2) NULL,
	[POSTAL_CD] [varchar](10) NULL,
	[COUNTRY_CD] [varchar](2) NULL,
	[COUNTY_CD] [varchar](3) NULL,
	[CREATE_DT] [datetime] NULL,
	[CREATE_USERID] [varchar](25) NULL,
	[UPDATE_DT] [datetime] NULL,
	[UPDATE_USERID] [varchar](25) NULL,
 CONSTRAINT [PK_WQX_ORG_ADDRESS] PRIMARY KEY CLUSTERED (ORG_ID,ADDRESS_TYPE ASC),
 FOREIGN KEY (ORG_ID) references T_WQX_ORGANIZATION (ORG_ID)  ON UPDATE CASCADE ON DELETE CASCADE
) ON [PRIMARY]


CREATE TABLE [dbo].[T_WQX_PROJECT](
	[PROJECT_IDX] [int] NOT NULL IDENTITY(1,1),
	[ORG_ID] [varchar](30) NOT NULL,
	[PROJECT_ID] [varchar](35) NOT NULL,
	[PROJECT_NAME] [varchar](120) NOT NULL,
	[PROJECT_DESC] [varchar](1999) NULL,
	[SAMP_DESIGN_TYPE_CD] [varchar](20) NULL,
	[QAPP_APPROVAL_IND] [bit] NULL,
	[QAPP_APPROVAL_AGENCY] [varchar](50) NULL,
	[WQX_IND] [bit] NULL,
	[WQX_SUBMIT_STATUS] [varchar](1) NULL,
	[WQX_UPDATE_DT] [datetime] NULL,
	[ACT_IND] [bit] NULL,
	[CREATE_DT] [datetime] NULL,
	[CREATE_USERID] [varchar](25) NULL,
	[UPDATE_DT] [datetime] NULL,
	[UPDATE_USERID] [varchar](25) NULL,
CONSTRAINT [PK_WQX_PROJECT] PRIMARY KEY CLUSTERED (PROJECT_IDX ASC),
 FOREIGN KEY (ORG_ID) references T_WQX_ORGANIZATION (ORG_ID)  ON UPDATE CASCADE ON DELETE CASCADE
) ON [PRIMARY]



CREATE TABLE [dbo].[T_WQX_MONLOC](
	[MONLOC_IDX] [int] NOT NULL IDENTITY(1,1),
	[ORG_ID] [varchar](30) NOT NULL,
	[MONLOC_ID] [varchar](35) NOT NULL,
	[MONLOC_NAME] [varchar](255) NOT NULL,
	[MONLOC_TYPE] [varchar](45) NOT NULL,
	[MONLOC_DESC] [varchar](1999) NULL,
	[HUC_EIGHT] [varchar](8) NULL,
	[HUC_TWELVE] [varchar](12) NULL,
	[TRIBAL_LAND_IND] [char](1) NULL,
	[TRIBAL_LAND_NAME] [varchar](200) NULL,
	[LATITUDE_MSR] [varchar](30) NOT NULL,
	[LONGITUDE_MSR] [varchar](30) NOT NULL,
	[SOURCE_MAP_SCALE] [int] NULL,
	[HORIZ_ACCURACY] [varchar](12) NULL,
	[HORIZ_ACCURACY_UNIT] [varchar](12) NULL,
	[HORIZ_COLL_METHOD] [varchar](150) NULL,
	[HORIZ_REF_DATUM] [varchar](6) NULL,
	[VERT_MEASURE] [varchar](12) NULL,
	[VERT_MEASURE_UNIT] [varchar](12) NULL,
	[VERT_COLL_METHOD] [varchar](50) NULL,
	[VERT_REF_DATUM] [varchar](10) NULL,
	[COUNTRY_CODE] [varchar](2) NULL,
	[STATE_CODE] [varchar](2) NULL,
	[COUNTY_CODE] [varchar](3) NULL,
	[WELL_TYPE] [varchar](255) NULL,
	[AQUIFER_NAME] [varchar](120) NULL,
	[FORMATION_TYPE] [varchar](50) NULL,
	[WELLHOLE_DEPTH_MSR] [varchar](12) NULL,
	[WELLHOLE_DEPTH_MSR_UNIT] [varchar](12) NULL,
	[WQX_IND] [bit] NULL,
	[WQX_SUBMIT_STATUS] [varchar](1) NULL,
	[WQX_UPDATE_DT] [datetime] NULL,
	[IMPORT_MONLOC_ID] [varchar](35) NULL,
	[ACT_IND] [bit] NULL,
	[CREATE_DT] [datetime] NULL,
	[CREATE_USERID] [varchar](25) NULL,
	[UPDATE_DT] [datetime] NULL,
	[UPDATE_USERID] [varchar](25) NULL,
 CONSTRAINT [PK_WQX_MONLOC] PRIMARY KEY CLUSTERED  (MONLOC_IDX ASC),
 FOREIGN KEY (ORG_ID) references T_WQX_ORGANIZATION (ORG_ID)  ON UPDATE CASCADE ON DELETE CASCADE
) ON [PRIMARY]




CREATE TABLE [dbo].[T_WQX_ACTIVITY](
	[ACTIVITY_IDX] [int] NOT NULL IDENTITY(1,1),
	[ORG_ID] [varchar](30) NOT NULL,
	[PROJECT_IDX] [int] NOT NULL,
	[MONLOC_IDX] [int] NULL,
	[ACTIVITY_ID] [varchar](35) NOT NULL,
	[ACT_TYPE] [varchar](70) NOT NULL,
	[ACT_MEDIA] [varchar](20) NOT NULL,
	[ACT_SUBMEDIA] [varchar](45) NULL,
	[ACT_START_DT] [datetime] NOT NULL,
	[ACT_END_DT] [datetime] NULL,
	[ACT_TIME_ZONE] [varchar](4) NULL,
	[RELATIVE_DEPTH_NAME] [varchar](15) NULL,
	[ACT_DEPTHHEIGHT_MSR] [varchar](12) NULL,
	[ACT_DEPTHHEIGHT_MSR_UNIT] [varchar](12) NULL,
	[TOP_DEPTHHEIGHT_MSR] [varchar](12) NULL,
	[TOP_DEPTHHEIGHT_MSR_UNIT] [varchar](12) NULL,
	[BOT_DEPTHHEIGHT_MSR] [varchar](12) NULL,
	[BOT_DEPTHHEIGHT_MSR_UNIT] [varchar](12) NULL,
	[DEPTH_REF_POINT] [varchar](125) NULL,
	[ACT_COMMENT] [varchar](4000) NULL,
	[BIO_ASSEMBLAGE_SAMPLED] [varchar](50) NULL,
	[BIO_DURATION_MSR] [varchar](12) NULL,
	[BIO_DURATION_MSR_UNIT] [varchar](12) NULL,
	[BIO_SAMP_COMPONENT] [varchar](15) NULL,
	[BIO_SAMP_COMPONENT_SEQ] [int] NULL,
	[BIO_REACH_LEN_MSR] [varchar](12) NULL,
	[BIO_REACH_LEN_MSR_UNIT] [varchar](12) NULL,
	[BIO_REACH_WID_MSR] [varchar](12) NULL,
	[BIO_REACH_WID_MSR_UNIT] [varchar](12) NULL,
	[BIO_PASS_COUNT] [int] NULL,
	[BIO_NET_TYPE] [varchar](30) NULL,
	[BIO_NET_AREA_MSR] [varchar](12) NULL,
	[BIO_NET_AREA_MSR_UNIT] [varchar](12) NULL,
	[BIO_NET_MESHSIZE_MSR] [varchar](12) NULL,
	[BIO_MESHSIZE_MSR_UNIT] [varchar](12) NULL,
	[BIO_BOAT_SPEED_MSR] [varchar](12) NULL,
	[BIO_BOAT_SPEED_MSR_UNIT] [varchar](12) NULL,
	[BIO_CURR_SPEED_MSR] [varchar](12) NULL,
	[BIO_CURR_SPEED_MSR_UNIT] [varchar](12) NULL,
	[BIO_TOXICITY_TEST_TYPE] [varchar](7) NULL,
	[SAMP_COLL_METHOD_IDX] [varchar](20) NULL,
	[SAMP_COLL_EQUIP] [varchar](40) NULL,
	[SAMP_COLL_EQUIP_COMMENT] [varchar](4000) NULL,
	[SAMP_PREP_IDX] [varchar](20) NULL,
	[SAMP_PREP_CONT_TYPE] [varchar](35) NULL,
	[SAMP_PREP_CONT_COLOR] [varchar](15) NULL,
	[SAMP_PREP_CHEM_PRESERV] [varchar](250) NULL,
	[SAMP_PREP_THERM_PRESERV] [varchar](25) NULL,
	[SAMP_PREP_STORAGE_DESC] [varchar](250) NULL,
	[WQX_IND] [bit] NULL,
	[WQX_SUBMIT_STATUS] [varchar](1) NULL,
	[WQX_UPDATE_DT] [datetime] NULL,
	[ACT_IND] [bit] NULL,
	[CREATE_DT] [datetime] NULL,
	[CREATE_USERID] [varchar](25) NULL,
	[UPDATE_DT] [datetime] NULL,
	[UPDATE_USERID] [varchar](25) NULL,
 CONSTRAINT [PK_WQX_ACTIVITY] PRIMARY KEY CLUSTERED ([ACTIVITY_IDX] ASC),
 FOREIGN KEY (PROJECT_IDX) references T_WQX_PROJECT (PROJECT_IDX) ,
 FOREIGN KEY (MONLOC_IDX) references T_WQX_MONLOC (MONLOC_IDX) ,
 FOREIGN KEY (ORG_ID) references T_WQX_ORGANIZATION (ORG_ID)  
) ON [PRIMARY]

CREATE TABLE [dbo].[T_WQX_BIO_HABITAT_INDEX](
	[BIO_HABITAT_INDEX_IDX] [int] NOT NULL IDENTITY(1,1),
	[ORG_ID] [varchar](30) NOT NULL,
	[MONLOC_IDX] [int] NULL,
	[INDEX_ID] [varchar](35) NOT NULL,
	[INDEX_TYPE_ID] [varchar](35) NOT NULL,
	[INDEX_TYPE_ID_CONTEXT] [varchar](50) NOT NULL,
	[INDEX_TYPE_NAME] [varchar](50) NOT NULL,
	[RESOURCE_TITLE] [varchar](120) NULL,
	[RESOURCE_CREATOR] [varchar](120) NULL,
	[RESOURCE_SUBJECT] [varchar](400) NULL,
	[RESOURCE_PUBLISHER] [varchar](60) NULL,
	[RESOURCE_DATE] [datetime] NULL,
	[RESOURCE_ID] [varchar](255) NULL,
	[INDEX_TYPE_SCALE] [varchar](50) NULL,
	[INDEX_SCORE] [varchar](10) NOT NULL,
	[INDEX_QUAL_CD] [varchar](5) NULL,
	[INDEX_COMMENT] [varchar](4000) NULL,
	[INDEX_CALC_DATE] [datetime] NULL,
	[WQX_IND] [bit] NULL,
	[WQX_SUBMIT_STATUS] [varchar](1) NULL,
	[WQX_UPDATE_DT] [datetime] NULL,
	[ACT_IND] [bit] NULL,
	[CREATE_DT] [datetime] NULL,
	[CREATE_USERID] [varchar](25) NULL,
	[UPDATE_DT] [datetime] NULL,
	[UPDATE_USERID] [varchar](25) NULL,
 CONSTRAINT [PK_T_WQX_BIO_HABITAT_INDEX] PRIMARY KEY CLUSTERED ([BIO_HABITAT_INDEX_IDX] ASC),
 FOREIGN KEY (MONLOC_IDX) references T_WQX_MONLOC (MONLOC_IDX),
 FOREIGN KEY (ORG_ID) references T_WQX_ORGANIZATION (ORG_ID)  
) ON [PRIMARY]


CREATE TABLE [dbo].[T_WQX_ACTIVITY_METRIC](
	[ACTIVITY_METRIC_IDX] [int] NOT NULL IDENTITY(1,1),
	[ACTIVITY_IDX] [int] NOT NULL,
	[METRIC_TYPE_ID] [varchar](35) NOT NULL,
	[METRIC_TYPE_ID_CONTEXT] [varchar](50) NOT NULL,
	[METRIC_TYPE_NAME] [varchar](50) NULL,
	[CITATION_TITLE] [varchar](120) NULL,
	[CITATION_CREATOR] [varchar](120) NULL,
	[CITATION_SUBJECT] [varchar](500) NULL,
	[CITATION_PUBLISHER] [varchar](60) NULL,
	[CITATION_DATE] [datetime] NULL,
	[CITATION_ID] [varchar](255) NULL,
	[METRIC_SCALE] [varchar](50) NULL,
	[METRIC_FORMULA_DESC] [varchar](50) NULL,
	[METRIC_VALUE_MSR] [varchar](12) NULL,
	[METRIC_VALUE_MSR_UNIT] [varchar](12) NULL,
	[METRIC_SCORE] [varchar](10) NOT NULL,
	[METRIC_COMMENT] [varchar](4000) NULL,
	[BIO_HABITAT_INDEX_IDX] [int] NULL,
	[WQX_IND] [bit] NULL,
	[WQX_SUBMIT_STATUS] [varchar](1) NULL,
	[WQX_UPDATE_DT] [datetime] NULL,
	[ACT_IND] [bit] NULL,
	[CREATE_DT] [datetime] NULL,
	[CREATE_USERID] [varchar](25) NULL,
	[UPDATE_DT] [datetime] NULL,
	[UPDATE_USERID] [varchar](25) NULL,
 CONSTRAINT [PK_WQX_ACTIVITYMETRIC] PRIMARY KEY CLUSTERED ([ACTIVITY_METRIC_IDX] ASC),
 FOREIGN KEY (ACTIVITY_IDX) references T_WQX_ACTIVITY (ACTIVITY_IDX)  ON UPDATE CASCADE ON DELETE CASCADE, 
 FOREIGN KEY (BIO_HABITAT_INDEX_IDX) references T_WQX_BIO_HABITAT_INDEX (BIO_HABITAT_INDEX_IDX)  ON UPDATE CASCADE ON DELETE CASCADE 
) ON [PRIMARY]





CREATE TABLE [dbo].[T_WQX_RESULT](
	[RESULT_IDX] [int] NOT NULL IDENTITY(1,1),
	[ACTIVITY_IDX] [int] NOT NULL,
	[DATA_LOGGER_LINE] [varchar](15) NULL,
	[RESULT_DETECT_CONDITION] [varchar](35) NULL,
	[CHAR_NAME] [varchar](120) NULL,
	[METHOD_SPECIATION_NAME] [varchar](20) NULL,
	[RESULT_SAMP_FRACTION] [varchar](25) NULL,
	[RESULT_MSR] [varchar](60) NULL,
	[RESULT_MSR_UNIT] [varchar](12) NULL,
	[RESULT_MSR_QUAL] [varchar](5) NULL,
	[RESULT_STATUS] [varchar](12) NULL,
	[STATISTIC_BASE_CODE] [varchar](25) NULL,
	[RESULT_VALUE_TYPE] [varchar](20) NULL,
	[WEIGHT_BASIS] [varchar](15) NULL,
	[TIME_BASIS] [varchar](12) NULL,
	[TEMP_BASIS] [varchar](12) NULL,
	[PARTICLESIZE_BASIS] [varchar](15) NULL,
	[PRECISION_VALUE] [varchar](60) NULL,
	[BIAS_VALUE] [varchar](60) NULL,
	[CONFIDENCE_INTERVAL_VALUE] [varchar](15) NULL,
	[UPPER_CONFIDENCE_LIMIT] [varchar](15) NULL,
	[LOWER_CONFIDENCE_LIMIT] [varchar](15) NULL,
	[RESULT_COMMENT] [varchar](4000) NULL,
	[DEPTH_HEIGHT_MSR] [varchar](12) NULL,
	[DEPTH_HEIGHT_MSR_UNIT] [varchar](12) NULL,
	[DEPTHALTITUDEREFPOINT] [varchar](125) NULL,
	[RESULT_SAMP_POINT] [varchar](12) NULL,
	[BIO_INTENT_NAME] [varchar](35) NULL,
	[BIO_INDIVIDUAL_ID] [varchar](4) NULL,
	[BIO_SUBJECT_TAXONOMY] [varchar](120) NULL,
	[BIO_UNIDENTIFIED_SPECIES_ID] [varchar](120) NULL,
	[BIO_SAMPLE_TISSUE_ANATOMY] [varchar](30) NULL,
	[GRP_SUMM_COUNT_WEIGHT_MSR] [varchar](12) NULL,
	[GRP_SUMM_COUNT_WEIGHT_MSR_UNIT] [varchar](12) NULL,
	[TAX_DTL_CELL_FORM] [varchar](11) NULL,
	[TAX_DTL_CELL_SHAPE] [varchar](18) NULL,
	[TAX_DTL_HABIT] [varchar](15) NULL,
	[TAX_DTL_VOLTINISM] [varchar](25) NULL,
	[TAX_DTL_POLL_TOLERANCE] [varchar](4) NULL,
	[TAX_DTL_POLL_TOLERANCE_SCALE] [varchar](50) NULL,
	[TAX_DTL_TROPHIC_LEVEL] [varchar](4) NULL,
	[TAX_DTL_FUNC_FEEDING_GROUP1] [varchar](6) NULL,
	[TAX_DTL_FUNC_FEEDING_GROUP2] [varchar](6) NULL,
	[TAX_DTL_FUNC_FEEDING_GROUP3] [varchar](6) NULL,
	[ANALYTIC_METHOD_IDX] [int] NULL,
	[LAB_IDX] [int] NULL,
	[LAB_ANALYSIS_START_DT] [datetime] NULL,
	[LAB_ANALYSIS_END_DT] [datetime] NULL,
	[LAB_ANALYSIS_TIMEZONE] [varchar](4) NULL,
	[RESULT_LAB_COMMENT_CODE] [varchar](3) NULL,
	[DETECTION_LIMIT_TYPE] [varchar](35) NULL,
	[DETECTION_LIMIT] [varchar](12) NULL,
	[LAB_TAXON_ACCRED_IND] [char](1) NULL,
	[LAB_TAXON_ACCRED_AUTHORITY] [varchar](20) NULL,
 CONSTRAINT [PK_WQX_RESULT] PRIMARY KEY CLUSTERED ([RESULT_IDX] ASC),
  FOREIGN KEY (ACTIVITY_IDX) references T_WQX_ACTIVITY (ACTIVITY_IDX) ON UPDATE CASCADE ON DELETE CASCADE,
  FOREIGN KEY (LAB_IDX) references T_WQX_REF_LAB (LAB_IDX) ON UPDATE CASCADE ON DELETE CASCADE,
  FOREIGN KEY (ANALYTIC_METHOD_IDX) references T_WQX_REF_ANAL_METHOD (ANALYTIC_METHOD_IDX) ON UPDATE CASCADE ON DELETE CASCADE
) ON [PRIMARY]




CREATE TABLE [dbo].[T_WQX_REF_DATA](
	[REF_DATA_IDX] [int] NOT NULL IDENTITY(1,1),
	[TABLE] [varchar](45) NOT NULL,
	[VALUE] [varchar](100) NOT NULL,
	[TEXT] [varchar](200) NOT NULL,
	[ACT_IND] [bit] NULL,
	[USED_IND] [bit] NULL,
	[UPDATE_DT] [datetime] NULL,
 CONSTRAINT [PK_WQX_REF_DATA] PRIMARY KEY CLUSTERED (REF_DATA_IDX ASC)
) ON [PRIMARY]

CREATE TABLE [dbo].[T_WQX_REF_CHARACTERISTIC](
	[CHAR_NAME] [varchar](120) NOT NULL,
	[DEFAULT_DETECT_LIMIT] [decimal](12,5) NULL,
	[DEFAULT_UNIT] [varchar](12) NULL,
	[USED_IND] [bit] NULL,
	[ACT_IND] [bit] NULL,
	[UPDATE_DT] [datetime] NULL,
 CONSTRAINT [PK_T_WQX_REF_CHARACTERISTIC] PRIMARY KEY CLUSTERED (CHAR_NAME ASC)
) ON [PRIMARY]

CREATE TABLE [dbo].[T_WQX_REF_CHAR_LIMITS](
	[CHAR_NAME] [varchar](120) NOT NULL,
	[UNIT_NAME] [varchar](12) NOT NULL,
	[LOWER_BOUND] [decimal] NULL,
	[UPPER_BOUND] [decimal] NULL,
	[ACT_IND] [bit] NULL,
	[CREATE_DT] [datetime] NULL,
	[CREATE_USERID] [varchar](25) NULL,
	[UPDATE_DT] [datetime] NULL,
	[UPDATE_USERID] [varchar](25) NULL,
 CONSTRAINT [PK_T_WQX_REF_CHAR_LIMITS] PRIMARY KEY CLUSTERED (CHAR_NAME ASC, UNIT_NAME ASC),
 FOREIGN KEY (CHAR_NAME) references T_WQX_REF_CHARACTERISTIC (CHAR_NAME) ON UPDATE CASCADE ON DELETE CASCADE
) ON [PRIMARY]


CREATE TABLE [dbo].[T_WQX_TRANSACTION_LOG](
	[LOG_ID] [int] NOT NULL IDENTITY(1,1),
	[TABLE_CD] [varchar](5) NOT NULL,
	[TABLE_IDX] [int] NOT NULL,
	[SUBMIT_DT] [datetime] NOT NULL,
	[SUBMIT_TYPE] [varchar](1) NOT NULL,
	[RESPONSE_FILE] [varbinary](max) NULL,
	[RESPONSE_TXT] [varchar](max) NULL,
	[CDX_SUBMIT_TRANSID] [varchar](100) NULL,
	[CDX_SUBMIT_STATUS] [varchar](20) NULL,
 CONSTRAINT [PK_WQX_TRANSACTION_LOG] PRIMARY KEY CLUSTERED (LOG_ID ASC)
) ON [PRIMARY]

CREATE TABLE [dbo].[T_WQX_IMPORT_LOG](
	[IMPORT_ID] [int] NOT NULL IDENTITY(1,1),
	[ORG_ID] [varchar](30) NOT NULL,
	[TYPE_CD] [varchar](5) NOT NULL,
	[FILE_NAME] [varchar](150) NOT NULL,
	[FILE_SIZE] [int] NOT NULL,
	[IMPORT_STATUS] [varchar](15) NULL,
	[IMPORT_FILE] [varbinary](max) NULL,
	[CREATE_DT] [datetime] NULL,
	[CREATE_USERID] [varchar](25) NULL,
 CONSTRAINT [PK_WQX_IMPORT_LOG] PRIMARY KEY CLUSTERED (IMPORT_ID ASC),
 FOREIGN KEY (ORG_ID) references T_WQX_ORGANIZATION (ORG_ID) ON UPDATE CASCADE ON DELETE CASCADE
) ON [PRIMARY]

GO





-- ****************************************************************************************************************************************
-- ************************* [VIEWS]                      *********************************************************************************
-- ****************************************************************************************************************************************
CREATE VIEW V_WQX_TRANSACTION_LOG
AS
SELECT L.*,
      isnull(isnull((select MONLOC_ID from T_WQX_MONLOC M where M.MONLOC_IDX = L.TABLE_IDX and L.TABLE_CD='MLOC'),
                      (select PROJECT_ID from T_WQX_PROJECT P where P.PROJECT_IDX = L.TABLE_IDX)),
               (select ACTIVITY_ID from T_WQX_ACTIVITY A where A.ACTIVITY_IDX = L.TABLE_IDX)) as RECORD      
  FROM [OpenEnvironment].[dbo].[T_WQX_TRANSACTION_LOG] L;  



GO



-- ****************************************************************************************************************************************
-- ************************* [STORED PROCS]*********************************************************************************
-- ****************************************************************************************************************************************
CREATE PROCEDURE GenWQXXML
@TypeText varchar(4),
@RecordIDX int
AS
BEGIN
	/*
	DESCRIPTION: RETURNS WQX XML FILE CONTAINING ALL UPDATED RECORDS
	CHANGE LOG: 8/6/2012 DOUG TIMMS, OPEN-ENVIRONMENT.ORG
	9/22/2012 DOUG TIMMS, fix error with project sampling design type code
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
		where (M.WQX_SUBMIT_STATUS = 'U' and M.WQX_IND=1 and M.ACT_IND=1)
		or (P.WQX_SUBMIT_STATUS = 'U' and P.WQX_IND=1 and P.ACT_IND=1)
		or (A.WQX_SUBMIT_STATUS = 'U' and A.WQX_IND=1 and A.ACT_IND=1)
	  for xml path('')
	 )


	--****************************************************************
	--************PROJECT ********************************
	--****************************************************************
	set @strProj = ''

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
			   and (@TypeText='' or PROJECT_IDX = @RecordIDX)
			   for xml path('')
			)
	END


	--****************************************************************
	--************MONITORING_LOCATION ********************************
	--****************************************************************
	set @strMon = ''

	if (@TypeText='MLOC' or @TypeText='') 
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
			   ISNULL(HORIZ_REF_DATUM,'UNKWN') as "MonitoringLocation/MonitoringLocationGeospatial/HorizontalCoordinateReferenceSystemDatumName",			   case when nullif(VERT_MEASURE,'') is not null then rtrim(VERT_MEASURE) else null end as "MonitoringLocation/MonitoringLocationGeospatial/VerticalMeasure/MeasureValue",			   case when nullif(VERT_MEASURE,'') is not null then rtrim(VERT_MEASURE_UNIT) else null end as "MonitoringLocation/MonitoringLocationGeospatial/VerticalMeasure/MeasureUnitCode",			   case when nullif(VERT_MEASURE,'') is not null then ISNULL(VERT_COLL_METHOD,'Other') else null end as "MonitoringLocation/MonitoringLocationGeospatial/VerticalCollectionMethodName",
			   case when nullif(VERT_MEASURE,'') is not null then ISNULL(VERT_REF_DATUM,'OTHER') else null end as "MonitoringLocation/MonitoringLocationGeospatial/VerticalCoordinateReferenceSystemDatumName",			   ISNULL(rtrim(COUNTRY_CODE),'US') as  "MonitoringLocation/MonitoringLocationGeospatial/CountryCode", 
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
			   and (@TypeText='' or MONLOC_IDX = @RecordIDX)
			   for xml path('')
			)
	END

	--****************************************************************
	--************BIO INDICES ****************************************
	--****************************************************************
	set @strBioIndex = '';
	if (@TypeText='IDX' or @TypeText='') 
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
			   from T_WQX_BIO_HABITAT_INDEX B, T_WQX_MONLOC M
			   WHERE B.MONLOC_IDX = M.MONLOC_IDX
			   and B.WQX_SUBMIT_STATUS='U' 
			   and B.WQX_IND = 1
			   and B.ACT_IND = 1
			   and (@TypeText='' or B.BIO_HABITAT_INDEX_IDX = @RecordIDX)
			   for xml path('')
			)
	END


	--****************************************************************
	--************ACTIVITY *******************************************
	--****************************************************************
	set @strActivity = ''

	if (@TypeText='ACT' or @TypeText='') 
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
                    isnull(RESULT_STATUS,'Final') AS "ResultDescription/ResultStatusIdentifier",
                    rtrim(STATISTIC_BASE_CODE) AS "ResultDescription/StatisticalBaseCode",
                    isnull(RESULT_VALUE_TYPE,'Actual') AS "ResultDescription/ResultValueTypeName",
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

					case when (RESULT_MSR in ('ND','NR','PAQL','PBQL','DNQ') or nullif(R.DETECTION_LIMIT,'') is not null) then 
						case when nullif(R.DETECTION_LIMIT_TYPE,'') is not null then rtrim(R.DETECTION_LIMIT_TYPE) else 'Estimated Detection Level' end
					end AS "ResultLabInformation/ResultDetectionQuantitationLimit/DetectionQuantitationLimitTypeName",
					case when (RESULT_MSR in ('ND','NR','PAQL','PBQL','DNQ') or nullif(R.DETECTION_LIMIT,'') is not null) then 
						case when nullif(DETECTION_LIMIT,'') is not null then DETECTION_LIMIT ELSE 
							(select isnull(CONVERT(varchar,rc.DEFAULT_DETECT_LIMIT),'0') from T_WQX_REF_CHARACTERISTIC rc where rc.CHAR_NAME = R.CHAR_NAME) end end AS "ResultLabInformation/ResultDetectionQuantitationLimit/DetectionQuantitationLimitMeasure/MeasureValue",
					case when (RESULT_MSR in ('ND','NR','PAQL','PBQL','DNQ') or nullif(R.DETECTION_LIMIT,'') is not null) then 
						case when nullif(RESULT_MSR_UNIT,'') is not null then RESULT_MSR_UNIT ELSE 
							(select isnull(rc.DEFAULT_UNIT,'None') from T_WQX_REF_CHARACTERISTIC rc where rc.CHAR_NAME = R.CHAR_NAME) end end AS "ResultLabInformation/ResultDetectionQuantitationLimit/DetectionQuantitationLimitMeasure/MeasureUnitCode"

    				FROM T_WQX_RESULT R 
					LEFT JOIN T_WQX_REF_ANAL_METHOD AM on R.ANALYTIC_METHOD_IDX = AM.ANALYTIC_METHOD_IDX
					LEFT JOIN T_WQX_REF_LAB LL on R.LAB_IDX = LL.LAB_IDX
    				where R.ACTIVITY_IDX = T_WQX_ACTIVITY.ACTIVITY_IDX 
--					and (RESULT_MSR not in ('ND','NR','PAQL','PBQL','DNQ') or DETECTION_LIMIT is not null)
					for xml path('Result'),type ) 

			   from T_WQX_ACTIVITY
			   WHERE WQX_SUBMIT_STATUS='U' 
			   and WQX_IND = 1
			   and ACT_IND = 1
			   and (@TypeText = '' or ACTIVITY_IDX = @RecordIDX)
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


-- ****************************************************************************************************************************************
-- ************************* [WQX ANALYSIS]*********************************************************************************
-- ****************************************************************************************************************************************
CREATE PROCEDURE WQXAnalysis
@TypeText varchar(10),
@OrgID varchar(20),
@MonLocID varchar(20),
@CharName varchar(200),
@StartDt datetime,
@EndDt datetime
AS
BEGIN
	/*
	DESCRIPTION: RETURNS DATA FOR CHARTING
	CHANGE LOG: 6/29/2012 DOUG TIMMS, OPEN-ENVIRONMENT.ORG
	*/
	SET NOCOUNT ON;

	if @TypeText='SERIES' 
		BEGIN
			select case when min(M.MONLOC_NAME)<>MAX(M.MONLOC_NAME) then 'Multiple' else min(M.MONLOC_NAME) end as MONLOC_NAME, 
				MIN(R.CHAR_NAME) as CHAR_NAME,
				dateadd(dd, datediff(dd, 0, a.ACT_START_DT)+0, 0) as START_DT, 
				avg(cast(R.RESULT_MSR as decimal(18,2))) as RESULT_MSR, 
				min(R.RESULT_MSR_UNIT) as RESULT_MSR_UNIT,
				min(R.DETECTION_LIMIT) as DETECTION_LIMIT
			from T_WQX_RESULT R, T_WQX_ACTIVITY A, T_WQX_MONLOC M
			where A.ACTIVITY_IDX = R.ACTIVITY_IDX
			and M.MONLOC_IDX = A.MONLOC_IDX
			and R.CHAR_NAME = @CharName
			and (A.ACT_START_DT >= @StartDt or @StartDt is null)
			and (A.ACT_START_DT <= @EndDt or @EndDt is null)
			and (A.MONLOC_IDX = @MonLocID or @MonLocID = 0)
			group by dateadd(dd, datediff(dd, 0, a.ACT_START_DT)+0, 0) 
			order by dateadd(dd, datediff(dd, 0, a.ACT_START_DT)+0, 0)
		END
	
END
GO