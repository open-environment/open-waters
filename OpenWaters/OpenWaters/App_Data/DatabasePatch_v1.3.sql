--run this script to apply changes for Open Waters v1.3 patch (upgrading from v1.2 or earlier)
--this patch assumes an organization record has already been created in the database. 

alter table T_OE_USERS add DEFAULT_ORG_ID varchar(30);

GO

update T_OE_USERS set DEFAULT_ORG_ID = (select top 1 org_id from T_WQX_ORGANIZATION);

GO

CREATE TABLE [dbo].[T_WQX_USER_ORGS](
	[USER_IDX] [int] NOT NULL,
	[ORG_ID] [varchar](30) NOT NULL,
	[ROLE_CD] [varchar](2) NULL,
	[CREATE_USERID] [varchar](25) NULL,
	[CREATE_DT] [datetime2](0) NULL,
 CONSTRAINT [PK_T_OE_USER_ORGS] PRIMARY KEY CLUSTERED (USER_IDX, ORG_ID),
 FOREIGN KEY (ORG_ID) references T_WQX_ORGANIZATION (ORG_ID) 
	ON UPDATE CASCADE 
	ON DELETE CASCADE, 
 FOREIGN KEY (USER_IDX) references T_OE_USERS (USER_IDX)
 	ON UPDATE CASCADE 
	ON DELETE CASCADE
) ON [PRIMARY]

GO


insert into T_WQX_USER_ORGS (USER_IDX, ORG_ID, ROLE_CD, CREATE_DT) 
select USER_IDX, DEFAULT_ORG_ID, 'U', GETDATE() from T_OE_USERS;


CREATE TABLE [dbo].[T_WQX_REF_CHAR_ORG](
	[CHAR_NAME] [varchar](120) NOT NULL,
	[ORG_ID] [varchar](30) NOT NULL,
	[CREATE_USERID] [varchar](25) NULL,
	[CREATE_DT] [datetime2](0) NULL,
 CONSTRAINT [PK_T_WQX_REF_CHAR_ORG] PRIMARY KEY CLUSTERED (ORG_ID, CHAR_NAME),
 FOREIGN KEY (ORG_ID) references T_WQX_ORGANIZATION (ORG_ID) 
	ON UPDATE CASCADE 
	ON DELETE CASCADE, 
 FOREIGN KEY (CHAR_NAME) references T_WQX_REF_CHARACTERISTIC (CHAR_NAME)
 	ON UPDATE CASCADE 
	ON DELETE CASCADE
) ON [PRIMARY]

GO


insert into T_WQX_REF_CHAR_ORG (CHAR_NAME, ORG_ID, CREATE_DT)
select CHAR_NAME, (select top 1 org_id from T_WQX_ORGANIZATION), GETDATE() from T_WQX_REF_CHARACTERISTIC 
where USED_IND = 1


