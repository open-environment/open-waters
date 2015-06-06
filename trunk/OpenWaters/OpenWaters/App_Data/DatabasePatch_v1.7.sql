/*
Only run this script if upgrading from v1.6.2 to v1.7x

After running this script it is recommended that you reimport the Characteristic Reference Table from EPA (go to Admin --> Data Synch screen)

Change Log
--------------------
MINOR ENHANCEMENTS: 
1. Sample result import validation enhancement: sample fraction required for certain characteristics
2. Better error message when creating new user and email server not configured properly
3. Admin --> Data Synch --> updated to new URL for pulling Org list from EPA
4. Bug fix: Fix collation error in create database script for REF_DATA table.
5. Fix display issues when using Internet Explorer Version 7 or Compatibility View
6. Fix monitoring location edit page to pull counties drop-down from county reference table 
7. Better error handling when importing crosstab and monitoring location is missing
8. Updated Installation/Users Guide 
----1.7.1 update
9. Fix bug on Activity Edit page
10. Add warning for users if they attempt to enter data before making the initial pull of reference data from EPA
----1.7.2 update
11. Added new data filter to Activity Page: WQX Submit status 
*/

ALTER TABLE T_WQX_REF_DATA ALTER COLUMN [Value] varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS
GO
ALTER TABLE T_WQX_REF_CHARACTERISTIC add [SAMP_FRAC_REQ] varchar(1);
ALTER TABLE T_WQX_REF_CHARACTERISTIC add [PICK_LIST] varchar(1);
GO
UPDATE T_WQX_REF_CHARACTERISTIC set SAMP_FRAC_REQ = 'N', PICK_LIST = 'N';
