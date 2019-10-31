/*****************************
Author:       Duane Billue
Date:         2019-10-31
Description:  Misc Statements
*****************************/

-- Environment Variables
-- SET SERVEROUTPUT ON;
-- SET AUTOPRINT ON;

-- SELECT * FROM cards;
-- SELECT * FROM errors;
-- CREATE TABLE departments AS SELECT * FROM hr.departments;
-- CREATE TABLE employees AS SELECT * FROM hr.employees;
-- CREATE TABLE employee_names (full_name VARCHAR2(250));
-- DROP TABLE departments;
-- DROP TABLE employees;

-- DBMS_DB_VERSION --
---------------------
SET SERVEROUTPUT ON
BEGIN
	DBMS_OUTPUT.PUT_LINE(DBMS_DB_VERSION.VERSION);
END;

-- ACID (Atomicity, Consistency, Isolation, and Durability) KEYWORDS --
------------------------------------------------------------------------
COMMIT
ROLLBACK
SAVEPOINT (name)
ROLLBACK TO SAVEPOINT (name)

-- PREPROCESSOR / CONDITIONAL COMPILE --
----------------------------------------
-- $IF, $THEN, $ELSE, $END

-- Obfuscated Code --
---------------------
DBMS_DDL.CREATE_WRAPPPED

-- Encrypted Code --
--------------------
WRAP utility --(11G only)

-- Database Job Start / Stop --
--------------------------------
DBMS_SCHEDULER.RUN_JOB('SCHEMA.JOB_NAME');
DBMS_SCHEDULER.STOP_JOB('SCHEMA.JOB_NAME');
DBMS_SCHEDULER.DROP_JOB('SCHEMA.JOB_NAME');


