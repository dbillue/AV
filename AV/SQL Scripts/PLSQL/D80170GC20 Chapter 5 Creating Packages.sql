/*****************************
Author:       Duane Billue
Date:         2019-10-23
Description:  D80170GC20 :: Chapter 5 Creating Functions
*****************************/
/*
-- Package comm_pkg specification
CREATE OR REPLACE PACKAGE comm_pkg IS
  FUNCTION vali(p_comm NUMBER) Return Number Is v_sal employees.salary%TYPE;
  v_std_comm NUMBER := 0.10;
  PROCEDURE reset_comm(p_new_comm NUMBER);
END comm_pkg;
/

CREATE OR REPLACE PACKAGE BODY comm_pkg IS

  CREATE OR REPLACE FUNCTION vali
  (
    p_comm NUMBER
  )
  RETURN BOOLEAN IS
    v_max_comm employees.commission_pct%TYPE;
  BEGIN
    SELECT MAX(Commission_Pct) INTO v_max_comm
    FROM Employees;
    RETURN (p_comm BETWEEN 0.0 AND v_max_comm);
  END vali;
  /
  
  CREATE OR REPLACE PROCEDURE reset_comm
  (
    p_new_comm NUMBER := 10
  ) AS
  BEGIN
    If vali(p_new_comm) Then
      v_std_comm := p_new_comm;
    ELSE
      RAISE_APPLICATION_ERROR(-20210, 'Bad Commision');
    END IF;
  END reset_comm;
  /  

END comm_pkg;
/
*/

/*
-- Package glb_consts
CREATE OR REPLACE PACKAGE glb_consts IS
  c_mile_2_kilo   CONSTANT NUMBER := 1.6093;
  c_kilo_2_mile   CONSTANT NUMBER := 0.6214;
  c_yard_2_meter  CONSTANT NUMBER := 0.9144;
  c_meter_2_yard  CONSTANT NUMBER := 1.0936;
END glb_consts;
*/

SET SERVEROUTPUT ON;
BEGIN
  Dbms_Output.Put_Line('16 miles to kilometers: ' || ROUND(16 * Glb_Consts.c_mile_2_kilo));
END;
/

CREATE OR REPLACE FUNCTION meter_to_yard 
(
  meter NUMBER
)
RETURN NUMBER IS
  yards NUMBER;
BEGIN
  IF meter > 0 THEN
    yards := ROUND(meter * Glb_Consts.c_meter_2_yard);
  ELSE
    yards := -1;
  END IF;
  RETURN yards;
END meter_to_yard;

SELECT meter_to_yard(5) "Meter to Yard" from dual;

--<><><><><><><><><*>--~
--<*><><><><><><><><><>

-- Package job_pkg declaration
create or replace 
PACKAGE job_pkg IS
  PROCEDURE add_job 
    (v_id Jobs.Job_Id%Type, v_title Jobs.Job_Title%Type);
  PROCEDURE del_job (p_jobid jobs.job_id%TYPE);
  FUNCTION get_job (p_jobid jobs.job_id%TYPE) RETURN jobs.job_title%TYPE;
  PROCEDURE upd_job (p_jobid jobs.job_id%TYPE, P_jobtitle IN jobs.job_title%TYPE);
END job_pkg;

-- Package job_pkg body
create or replace 
PACKAGE BODY job_pkg IS
  PROCEDURE add_job
  (
    v_id Jobs.Job_Id%Type,
    v_title Jobs.Job_Title%Type
  )
  IS
  BEGIN
    INSERT INTO Jobs (Job_Id, Job_Title, Min_Salary, Max_Salary) VALUES (v_id, v_title, NULL, NULL);
  END Add_Job;
  
  PROCEDURE del_job(p_jobid jobs.job_id%TYPE) IS
    BEGIN
      DELETE FROM jobs WHERE job_id = p_jobid;
      IF SQL%NOTFOUND THEN
        RAISE_APPLICATION_ERROR(-20203, 'No jobs deleted');
      END IF;
    END del_job;
    
  FUNCTION get_job(p_jobid jobs.job_id%TYPE)
    RETURN jobs.job_title%TYPE IS
    v_title jobs.job_title%TYPE;
    BEGIN
      SELECT job_title INTO v_title FROM jobs WHERE job_id = p_jobid;
      RETURN v_title;
    END get_job;
  
  PROCEDURE upd_job
  (
    p_jobid IN jobs.job_id%TYPE, 
    p_jobtitle IN jobs.job_title%TYPE
  ) IS
  BEGIN
    UPDATE jobs
    SET job_title = p_jobtitle
    WHERE job_id = p_jobid;
    IF SQL%NOTFOUND THEN
      Dbms_Output.Put_Line('No job updated');
    END IF;
  END upd_job;
    
END job_pkg;