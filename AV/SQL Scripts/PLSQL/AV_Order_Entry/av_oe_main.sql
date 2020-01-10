/****************
Author:			Duane Billue
Date:			11-25-2019
Description:	AV Order Entry Main / Create 
****************/
--
SET ECHO OFF

REM PROMPT 
REM PROMPT specify password for AV as parameter 1:
REM DEFINE pass     = &1
DEFINE pass = "Alli3";
REM PROMPT 
REM PROMPT specify default tablespeace for AV as parameter 2:
REM DEFINE tbs      = &2
DEFINE tbs = "user";
REM PROMPT 
REM PROMPT specify temporary tablespace for OE as parameter 3:
REM DEFINE ttbs     = &3
DEFINE ttbs = "temp";
REM PROMPT 
REM PROMPT specify password for HR as parameter 4:
REM DEFINE passhr   = &4
REM PROMPT 
REM PROMPT specify password for SYS as parameter 5:
REM DEFINE pass_sys = &5
DEFINE pass_sys = "Spindle";
REM PROMPT 
REM PROMPT specify directory path for the data files as parameter 6:
REM DEFINE data_path = &6
DEFINE data_path = "C:\Data\AV_Order_Entry\";
REM PROMPT
REM PROMPT writeable directory path for the log files as parameter 7:
REM DEFINE log_path = &7
DEFINE log_path = "C:\Logs\AV_Order_Entry\";
REM PROMPT
REM PROMPT specify version as parameter 8:
REM DEFINE vrs = &8
REM PROMPT
REM PROMPT specify connect string as parameter 9:
REM DEFINE connect_string     = &9
DEFINE connect_string = "orclpdb";
REM PROMPT

-- The first dot in the spool command below is 
-- the SQL*Plus concatenation character

DEFINE spool_file = &log_path.av_oe_oc..log
SPOOL &spool_file

-- Dropping the user with all its objects

DROP USER av CASCADE;

REM =======================================================
REM create user
REM 
REM The user is assigned tablespaces and quota in separate
REM ALTER USER statements so that the CREATE USER statement
REM will succeed even if the demo and temp tablespaces do
REM not exist.
REM =======================================================

CREATE USER av IDENTIFIED BY &pass;

ALTER USER av DEFAULT TABLESPACE &tbs QUOTA UNLIMITED ON &tbs;

ALTER USER av TEMPORARY TABLESPACE &ttbs;

GRANT CREATE SESSION, CREATE SYNONYM, CREATE VIEW TO av;
GRANT CREATE DATABASE LINK, ALTER SESSION TO av;
GRANT RESOURCE , UNLIMITED TABLESPACE TO av;
GRANT CREATE MATERIALIZED VIEW  TO av;
GRANT QUERY REWRITE             TO av;

REM =======================================================
REM grants from sys schema
REM =======================================================

CONNECT sys/&pass_sys@&connect_string AS SYSDBA;  
GRANT execute ON sys.dbms_stats TO av;


REM =======================================================
REM create av schema (av order entry)
REM =======================================================

CONNECT av/&pass@&connect_string
ALTER SESSION SET NLS_LANGUAGE=American;
ALTER SESSION SET NLS_TERRITORY=America;

--
-- call script to create OE objects
--

DEFINE vscript = C:\Source\AV\SQL Scripts\PLSQL\AV_Order_Entry\av_oe_main.sql
REM @&vscript --&vrs &pass &pass_sys &connect_string


-- oe_analz invalidates the coe public synonyms - recreate them
CONNECT sys/&pass_sys@&connect_string AS SYSDBA;  
CREATE OR REPLACE PUBLIC SYNONYM COE_CONFIGURATION FOR COE_CONFIGURATION;
CREATE OR REPLACE PUBLIC SYNONYM COE_NAMESPACES FOR COE_NAMESPACES;
CREATE OR REPLACE PUBLIC SYNONYM COE_DOM_HELPER FOR COE_DOM_HELPER;
CREATE OR REPLACE PUBLIC SYNONYM COE_UTILITIES FOR COE_UTILITIES;
CREATE OR REPLACE PUBLIC SYNONYM COE_TOOLS FOR COE_TOOLS;

spool off
