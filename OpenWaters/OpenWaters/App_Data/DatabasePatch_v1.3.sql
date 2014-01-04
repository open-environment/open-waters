--run this script to apply changes for Open Waters v1.3 patch (upgrading from v1.2 or earlier)

alter table T_OE_USERS add DEFAULT_ORG_ID varchar(30);

update T_OE_USERS set DEFAULT_ORG_ID = (select top 1 org_id from T_WQX_ORGANIZATION);

