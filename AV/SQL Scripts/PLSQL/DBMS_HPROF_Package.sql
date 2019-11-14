/*****************************
Author:       Duane Billue
Date:         2019-11-13
Description:  DBMS_HPROF Package
*****************************/

-----------------------------
--       DBMS_HPROF        --
-----------------------------

-- GRANT EXECUTE ON sys.dbms_hprof TO OE;  --run as sys :-)

-- Start profiling
BEGIN
 DBMS_HPROF.START_PROFILING('RESUME_FILES', 'pd_cc_pkg.txt'); 
END;

DECLARE
  v_card_info typ_cr_card_nst;
BEGIN
  credit_card_pkg.update_card_info(154, 'Discover', '123456789', v_card_info);
END;

-- Stop profiling.
BEGIN
  DBMS_HPROF.STOP_PROFILING;
END;

------------------------------
--    DBMS_HPROF.analyze    --
------------------------------
DBMS_HPROF.analyze
(
  location      IN VARCHAR2,
  filename      IN VARCHAR2,
  summary_mode  IN BOOLEAN DEFAULT FALSE,
  trace         IN VARCHAR2 DEFAULT NULL,
  skip          IN PLS_INTEGER DEFAULT 0,
  collect       IN PLS_INTEGER DEFAULT NULL,
  run_comment   IN VARCHAR2 DEFAULT NULL
)
RETURN NUMBER;

-- Import raw trace file
DECLARE 
  v_runid NUMBER;
BEGIN
  v_runid := DBMS_HPROF.analyze (LOCATION => 'RESUME_FILES', FILENAME => 'pd_cc_pkg.txt');
  DBMS_OUTPUT.PUT_LINE('RUN ID: ' || v_runid);
END;

-- Review imported trace data
-- Table Dbmshp_Runs
DESC Dbmshp_runs;
SELECT runid, run_timestamp, total_elapsed_time, run_comment
FROM Dbmshp_Runs
WHERE runid = 1;

-- Table Dbmshp_Functions_Info
DESC dbmshp_function_info;
SELECT owner, module, type, line#, namespace, calls, function_elapsed_time
FROM dbmshp_function_info
WHERE runid = 1;

----------------------------
--   plshprof Utility     -- 
----------------------------
plshprof -output trace pd_cc_pkg.txt
