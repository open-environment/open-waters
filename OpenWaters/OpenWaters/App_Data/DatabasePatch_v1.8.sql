/*
Only run this script if upgrading from v1.7x to v1.8x

Version 1.8x Change Log
--------------------
ENHANCEMENTS: 
1. Added import translations: now before importing data you can define a mapping of your data to EPA acceptable data. This is helpful in cases where you receive 
data from labs using codes different from EPA and don't want to have to update the import spreadsheet every time.
2. Additional code cleanup
*/

CREATE TABLE [dbo].[T_WQX_IMPORT_TRANSLATE](
	[TRANSLATE_IDX] [int] IDENTITY(1,1) NOT NULL,
	[ORG_ID] [varchar](30) NOT NULL,
	[COL_NAME] [varchar](50) NOT NULL,
	[DATA_FROM] [varchar](150) NOT NULL,
	[DATA_TO] [varchar](150) NULL,
	[CREATE_DT] [datetime] NULL,
	[CREATE_USERID] [varchar](25) NULL,
 CONSTRAINT [PK_WQX_IMPORT_TRANSLATE] PRIMARY KEY CLUSTERED  ([TRANSLATE_IDX] ),
 FOREIGN KEY (ORG_ID) references T_WQX_ORGANIZATION (ORG_ID) 
	ON UPDATE CASCADE 
	ON DELETE CASCADE, 
) ON [PRIMARY];


