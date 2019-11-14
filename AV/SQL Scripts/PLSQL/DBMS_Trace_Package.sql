/*****************************
Author:       Duane Billue
Date:         2019-11-13
Description:  DBMS_Trace Package
*****************************/

SET SERVEROUTPUT ON;
SET AUTOPRINT ON;
SET TIMING ON;

-- GRANT SELECT ON plsql_trace_runs TO OE;

SELECT * FROM sys.plsql_trace_runs;
SELECT * FROM sys.plsql_trace_events;