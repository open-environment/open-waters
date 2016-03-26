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
*/


  ALTER TABLE T_WQX_REF_CHAR_ORG ADD DEFAULT_LOWER_QUANT_LIMIT varchar(12) NULL;
  ALTER TABLE T_WQX_REF_CHAR_ORG ADD DEFAULT_UPPER_QUANT_LIMIT varchar(12) NULL;
