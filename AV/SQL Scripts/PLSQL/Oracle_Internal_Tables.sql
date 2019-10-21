/*****************************
Author:       Duane Billue
Date:         2019-10-14
Description:  Oracle Internal Tables
*****************************/

SET SERVEROUTPUT ON;
SET AUTOPRINT ON;

/*==========================--
Alter Users Password
--==========================*/
--SHOW user;
-- *Change password* 
--ALTER USER sys IDENTIFIED BY <password>;

/*==========================--
Alter Session Container
--==========================*/
--ALTER SESSION SET CONTAINER = <container>
--ALTER SESSION SET CONTAINER = <orclpgdb>

/*==========================--
Alter Session Schema
--==========================*/
--ALTER SESSION SET CURRENT_SCHEMA = <schema name<

/*==========================--
Unlock User (performed by user SYS) / Reset Password
--==========================*/
--ALTER USER <user> ACCOUNT UNLOCK
--ALTER USER <user> IDENTIFIED BY <password>


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

/****
dba_users
****/
DESC dba_users;
SELECT * FROM dba_users;
