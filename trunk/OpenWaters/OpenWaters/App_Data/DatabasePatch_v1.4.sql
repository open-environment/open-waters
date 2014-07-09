--run this script to apply changes for Open Waters v1.4 patch (upgrading from v1.3)

INSERT INTO [OpenEnvironment].[dbo].[T_OE_APP_SETTINGS] ([SETTING_NAME],[SETTING_VALUE],[MODIFY_USERID],[MODIFY_DT])
     VALUES ('Default State','OK','SYSTEM',GetDate());

