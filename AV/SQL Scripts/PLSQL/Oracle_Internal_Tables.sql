/*****************************
Author:       Duane Billue
Date:         2019-10-14
Description:  Oracle Internal Tables
*****************************/

SET SERVEROUTPUT ON;
SET AUTOPRINT ON;

/****
USER_OBJECT
****/
SELECT * FROM user_object
WHERE
	1 = 1
	--AND Object_name = 'objectname'
	
/****
USER_SOURCE
****/
SELECT * FROM user_source
WHERE
	1 = 1
	--AND name = 'objectname'