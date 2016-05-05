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
  4. Added ability to display custom message on registration page
  5. Added an option to have the site administrator get automatically emailed any time a new user registers an account
  6. Moved 3rd party javascript dependency (list.js) to local resource
  7. Added a message informing new users to contact STORET helpdesk if WQX organization ID cannot be found in list
*/


  ALTER TABLE T_WQX_REF_CHAR_ORG ADD DEFAULT_LOWER_QUANT_LIMIT varchar(12) NULL;
  ALTER TABLE T_WQX_REF_CHAR_ORG ADD DEFAULT_UPPER_QUANT_LIMIT varchar(12) NULL;


  --1.9.2
  INSERT INTO T_OE_APP_SETTINGS ([SETTING_NAME],[SETTING_VALUE],[SETTING_DESC],[MODIFY_USERID],[MODIFY_DT]) VALUES ('Hosting Org','Open Environment Software','The name of the organization hosting this installation of Open Waters. This is used on the Terms and Conditions.','SYSTEM',GetDate());
--  INSERT INTO T_OE_APP_SETTINGS ([SETTING_NAME],[SETTING_VALUE],[SETTING_DESC],[MODIFY_USERID],[MODIFY_DT]) VALUES ('Signup Message','If you are authorized to collect and submit WQX data, you can register for a FREE Open Waters account and submit up to 100 samples per year. ','An optional message that can be displayed on the account registration page.','SYSTEM',GetDate());
  INSERT INTO T_OE_APP_SETTINGS ([SETTING_NAME],[SETTING_VALUE],[SETTING_DESC],[MODIFY_USERID],[MODIFY_DT]) VALUES ('Signup Message','','An optional message that can be displayed on the account registration page.','SYSTEM',GetDate());
  INSERT INTO T_OE_APP_SETTINGS ([SETTING_NAME],[SETTING_VALUE],[SETTING_DESC],[MODIFY_USERID],[MODIFY_DT]) VALUES ('Notify Register','Y','When set to Y Open Waters will send a notification email to the site administrator any time a new user registers an account.','SYSTEM',GetDate());
  