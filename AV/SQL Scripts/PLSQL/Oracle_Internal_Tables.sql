/*****************************
Author:       Duane Billue
Date:         2019-10-14
Description:  Oracle Internal Tables
*****************************/

SET SERVEROUTPUT ON;
SET AUTOPRINT ON;
SET TIMING ON;

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
SELECT status, database_status from v$instance;

/**********
V$PARAMETER;
**********/
desc V$PARAMETER;
SELECT name, value FROM v$parameter;

/**********
v$result_cache_statitics
**********/
desc v$result_cache_statitics;
SELECT * FROM v$result_cache_statitics;

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
USER_ARGUMENTS
****/
DESC USER_ARGUMENTS;
SELECT * FROM USER_ARGUMENTS

/****
USER_PROCEDURES
****/
DESC USER_PROCEDURES;
SELECT * FROM USER_PROCEDURES

/****
USER_TABLES
****/ 
DESC user_tables;
SELECT table_name FROM user_tables;
SELECT * FROM user_tables;

/****
USER_IDENTIFIERS
****/ 
DESC USER_IDENTIFIERS;
SELECT * FROM USER_IDENTIFIERS
WHERE 1 = 1
  AND USAGE = 'DECLARATION'
ORDER BY OBJECT_TYPE, USAGE_ID

/****
USER_DEPENDENCIES
****/
DESC USER_DEPENDENCIES;
SELECT name, type, referenced_name, referenced_type
FROM user_dependencies
WHERE referenced_name IN ('Employees');

/****
USER_POLICIES
****/ 
DESC USER_POLICIES;
SELECT * FROM USER_POLICIES

/****
DBA_ROLES
****/
DESC DBA_ROLES;
SELECT * FROM DBA_ROLES;

/****
DBA_USERS
****/
DESC dba_users;
SELECT * FROM dba_users;

/****
DBA_ROLE_PRIVS
****/
DESC DBA_ROLE_PRIVS;
SELECT * from DBA_ROLE_PRIVS;

/****
DBA_POLICIES
****/
DESC DBA_POLICIES;
SELECT * from DBA_POLICIES;

/****
DBA_CONTEXT
****/
DESC DBA_CONTEXT;
SELECT * from DBA_CONTEXT;

/****
USER_SYS_PRIVS
****/
DESC USER_SYS_PRIVS;
SELECT * from user_sys_privs;

/****
USER_COLL_TYPES
****/
DESC USER_COLL_TYPES;
SELECT * from USER_COLL_TYPES;

/****
SESSION_ROLES
****/
DESC SESSION_ROLES;
SELECT * from SESSION_ROLES;

/****
ALL_DIRECTORIES
****/
DESC ALL_DIRECTORIES;
SELECT * from ALL_DIRECTORIES;

/****
ALL_POLICIES
****/
DESC ALL_POLICIES;
SELECT * from ALL_POLICIES;

/****
ALL_ARGUMENTS
****/
DESC ALL_ARGUMENTS;
SELECT * from ALL_ARGUMENTS
WHERE 1 = 1
  AND package_name IN ('CREDIT_CARD_PKG');

SELECT DISTINCT(PACKAGE_NAME) FROM ALL_ARGUMENTS
ORDER BY 1

/****
ALL_CONTEXT
****/
DESC ALL_CONTEXT;
SELECT * from ALL_CONTEXT;
