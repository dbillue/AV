/*****************************
Author:       Duane Billue
Date:         2019-11-12
Description:  DBMS_MetaData
*****************************/

SELECT DBMS_METADATA.get_ddl ('TABLE', 'ORDERS', 'OE') FROM dual;