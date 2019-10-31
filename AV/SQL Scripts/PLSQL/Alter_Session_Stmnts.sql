/*****************************
Author:       Duane Billue
Date:         2019-10-31
Description:  ALTER Session Statements
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
--ALTER SESSION SET CURRENT_SCHEMA = <schema name>

/*==========================--
Unlock User (performed by user SYS) / Reset Password
--==========================*/
--ALTER USER <user> ACCOUNT UNLOCK
--ALTER USER <user> IDENTIFIED BY <password>