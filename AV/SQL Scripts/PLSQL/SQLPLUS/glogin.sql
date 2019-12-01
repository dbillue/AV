--
-- Copyright (c) 1988, 2005, Oracle.  All Rights Reserved.
--
-- NAME
--   glogin.sql
--
-- DESCRIPTION
--   SQL*Plus global login "site profile" file
--
--   Add any SQL*Plus commands here that are to be executed when a
--   user starts SQL*Plus, or uses the SQL*Plus CONNECT command.
--
-- USAGE
--   This script is automatically run
--
SET SERVEROUTPUT ON;
SET AUTOPRINT ON;
ALTER SESSION SET PLSQL_CODE_TYPE = NATIVE;
ALTER SESSION SET NLS_DATE_FORMAT = 'DD-MON-YYYY:HH:MM:SS';
SET UNDERLINE '=';
SET LINES 256;
SET TRIMOUT ON;
SET TAB OFF;
SET PAGESIZE 100;
SET COLSEP " | ";
SET RECSEP WRAPPED;
SET RECSEPCHAR "-";
--SET NUMFORMAT $99,999;