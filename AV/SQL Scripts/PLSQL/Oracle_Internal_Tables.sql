/*****************************
Author:       Duane Billue
Date:         2019-10-14
Description:  Oracle Internal Tables
*****************************/

SET SERVEROUTPUT ON;
SET AUTOPRINT ON;

/**********
V$VERSION;
**********/
desc v$version;
SELECT banner FROM v$version WHERE ROWNUM = 1;

/**********
V$DATABASE;
**********/
desc v$database;
SELECT name, cdb, con_id FROM v$database;

/**********
V$INSTANCE;
**********/
desc v$instance;
SELECT name, cdb, con_id FROM v$instance;

/****
ALL_PLSQL_OBJECT_SETTINGS
****/
DESCRIBE ALL_PLSQL_OBJECT_SETTINGS;
SELECT * FROM ALL_PLSQL_OBJECT_SETTINGS;

/****
USER_PLSQL_OBJECT_SETTINGS
****/
desc USER_PLSQL_OBJECT_SETTINGS;
SELECT * FROM user_plsql_object_settings;

/****
USER_OBJECT
****/
desc user_object;
SELECT * FROM user_object
WHERE
  1 = 1
  --AND Object_name = 'objectname'
  
/****
USER_SOURCE
****/
DESC user_source;
SELECT * FROM user_source
WHERE
  1 = 1
  --AND name = 'objectname'

/****
USER_TABLES
****/ 
DESC user_tables;
SELECT table_name FROM user_tables;
SELECT * FROM user_tables;

/****
dba_users
****/
DESC dba_users;
SELECT * FROM dba_users;

/****
USER_SYS_PRIVS
****/
DESC USER_SYS_PRIVS;
SELECT * from user_sys_privs;

/****
USER_DEPENDENCIES
****/
DESC USER_DEPENDENCIES;
SELECT name, type, referenced_name, referenced_type
FROM user_dependencies
WHERE referenced_name IN ('Employees');

/****
USER_COLL_TYPES
****/
DESC USER_COLL_TYPES;
SELECT * from USER_COLL_TYPES;

/****
ALL_DIRECTORIES
****/
DESC ALL_DIRECTORIES;
SELECT * from ALL_DIRECTORIES;
