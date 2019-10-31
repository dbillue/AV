/*****************************
Author:       Duane Billue
Date:         2019-10-30
Description:  Oracle PL/SQL INIT Parameters
*****************************/

SET SERVEROUTPUT ON;
SET AUTOPRINT ON;

/*--======================
USER_PLSQL_OBJECT_SETTINGS
========================--*/
desc USER_PLSQL_OBJECT_SETTINGS;
SELECT * FROM user_plsql_object_settings;

/*--======================
PLSQL_Optimize_Level
========================--*/
ALTER SESSION SET PLSQL_OPTIMIZATION_LEVEL = 0;
{0, 1, 2 , 3}

/*--======================
PLSQL_Code_Type
========================--*/
ALTER SESSION SET PLSQL_CODE_TYPE = 'NATIVE';
ALTER SESSION SET PLSQL_CODE_TYPE = 'INTERPRETED';

/*--======================
PLSQL_WARNINGS
========================--*/
ALTER SESSION SET plsql_warnings = 'enable:severe',  'enable:performance', 'disable:informational';
SELECT value FROM v$parameter WHERE name = 'plsql_warnings';

SET SERVEROUTPUT ON
DECLARE
	s VARCHAR2(50);
BEGIN
	s:= dbms_warning.get_warning_setting_string();
	dbms_output.put_line(s);
END;

/*--======================
NLS_LENGTH_SEMANTICS
========================--*/

/*--======================
PLSQL_CCFLAGS (Conditional Compilation)
========================--*/
ALTER SESSION SET PLSQL_CCFLAGS = 'plsql_ccflags:true, debug:true, debug:0';