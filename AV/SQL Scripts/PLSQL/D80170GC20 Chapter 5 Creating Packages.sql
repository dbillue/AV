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
*/

--<><><><><><><><><*>--~
--<*><><><><><><><><><>

/*
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
*/

--=====================================--
--===============<><>==================--

CREATE OR REPLACE PACKAGE emp_pkg IS
PROCEDURE print_employee
(
  emp_record EMPLOYEES%ROWTYPE
);
PROCEDURE add_employee
(
  p_first_name employees.first_name%TYPE,
  p_last_name employees.last_name%TYPE,
  p_email employees.email%TYPE,
  p_job employees.job_id%TYPE DEFAULT 'IT',
  p_mgr employees.manager_id%TYPE DEFAULT 145,
  p_sal employees.salary%TYPE DEFAULT 1000,
  p_comm employees.commission_pct%TYPE DEFAULT 0,
  p_deptid employees.department_id%TYPE DEFAULT 60
);
PROCEDURE add_employee
(
  p_first_name employees.first_name%TYPE,
  p_last_name employees.last_name%TYPE,
  p_dept_id employees.department_id%TYPE
);
PROCEDURE get_employee
(
  p_empid IN employees.employee_id%TYPE,
  p_sal OUT employees.salary%TYPE,
  p_job OUT employees.job_id%TYPE
);
FUNCTION get_employee (p_emp_id IN employees.employee_id%TYPE) RETURN EMPLOYEES%ROWTYPE;
FUNCTION get_employee (p_family_name IN employees.last_name%TYPE) RETURN EMPLOYEES%ROWTYPE;
FUNCTION valid_deptid(p_deptid IN departments.department_id%TYPE) RETURN BOOLEAN;
END emp_pkg;
/

CREATE OR REPLACE PACKAGE BODY emp_pkg IS
  FUNCTION valid_deptid
  (
    p_deptid IN departments.department_id%TYPE
  ) RETURN BOOLEAN IS v_dummy PLS_INTEGER;
  BEGIN
    SELECT 1 INTO v_dummy FROM departments WHERE Department_Id IN (p_deptid);
    RETURN TRUE;
    EXCEPTION
      WHEN NO_DATA_FOUND THEN
        RETURN FALSE;
  END valid_deptid;
  
  PROCEDURE add_employee
  (
    p_first_name employees.first_name%TYPE,
    p_last_name employees.last_name%TYPE,
    p_email employees.email%TYPE,
    p_job employees.job_id%TYPE DEFAULT 'IT',
    p_mgr employees.manager_id%TYPE DEFAULT 145,
    p_sal employees.salary%TYPE DEFAULT 1000,
    p_comm employees.commission_pct%TYPE DEFAULT 0,
    p_deptid employees.department_id%TYPE DEFAULT 60
  ) IS
  BEGIN
    /*
    Dbms_Output.Put_Line('add_employee.H2 ' || p_first_name);
    Dbms_Output.Put_Line('add_employee.H3 ' || p_last_name);
    Dbms_Output.Put_Line('add_employee.H4 ' || p_email);
    Dbms_Output.Put_Line('add_employee.H5 ' || p_job);
    */
    IF valid_deptid(p_deptid) THEN
      INSERT INTO employees 
        (employee_id, first_name, last_name, email, job_id, manager_id, hire_date, salary, commission_pct, department_id) 
      VALUES
        (employees_seq.NEXTVAL, p_first_name, p_last_name, p_email, p_job, p_mgr, TRUNC(SYSDATE), p_sal, p_comm, p_deptid);
    ELSE
      Dbms_Output.Put_Line('Invalid department ID.  Try again.');
    END IF;
  END add_employee;
  
  PROCEDURE add_employee
  (
    p_first_name employees.first_name%TYPE,
    p_last_name employees.last_name%TYPE,
    p_dept_id employees.department_id%TYPE
  ) IS
    v_email employees.email%TYPE;
  BEGIN
    v_email := SUBSTR(INITCAP(p_first_name), 1, 1) || SUBSTR(UPPER(p_last_name), 1, 7);
    --Dbms_Output.Put_Line('add_employee.H1 ' || v_email);
    emp_pkg.add_employee
    (
      p_first_name,
      p_last_name,
      v_email
    );
  END add_employee;  
  
  PROCEDURE get_employee
  (
    p_empid IN employees.employee_id%TYPE,
    p_sal OUT employees.salary%TYPE,
    p_job OUT employees.job_id%TYPE
  ) IS
  BEGIN
    SELECT salary, job_id INTO p_sal, p_job FROM employees WHERE employee_id = p_empid;
  END get_employee;
  
  FUNCTION get_employee
  (
    p_emp_id employees.employee_id%TYPE
  ) RETURN Employees%Rowtype IS rec_employee Employees%ROWTYPE;
  BEGIN
    SELECT * INTO rec_employee FROM employees WHERE employee_id IN (p_emp_id);
    RETURN rec_employee;
  END get_employee;
  
  FUNCTION get_employee
  (
    p_family_name employees.last_name%TYPE
  )
  RETURN EMPLOYEES%ROWTYPE IS emp_record EMPLOYEES%ROWTYPE;
  BEGIN
    SELECT * INTO emp_record FROM employees WHERE last_name IN (p_family_name);
    RETURN emp_record;
  END get_employee;
  
  PROCEDURE print_employee
  (
    emp_record EMPLOYEES%ROWTYPE
  )
  IS
  BEGIN
    Dbms_Output.Put_Line(emp_record.department_id || ', ' 
                        || emp_record.employee_id || ', '
                        || emp_record.first_name || ', '
                        || emp_record.last_name || ', '
                        || emp_record.job_id || ', '
                        || emp_record.salary);
  END;  
  
END emp_pkg;
/

--=====================================--
--===============<><>==================--

/*
--Persistent State of a Package using a private cursor
CREATE OR REPLACE PACKAGE curs_pkg IS
  PROCEDURE open;
  FUNCTION next(p_n NUMBER := 1) RETURN BOOLEAN;
  PROCEDURE close;
END curs_pkg;

CREATE OR REPLACE PACKAGE BODY curs_pkg IS
  -- private cursor
  CURSOR cur_c IS
    SELECT employee_id FROM employees;
  PROCEDURE open IS
  BEGIN
    IF NOT cur_c%ISOPEN THEN
      OPEN cur_c;
    END IF;
  END open;
  FUNCTION next(p_n NUMBER := 1) RETURN BOOLEAN IS 
    v_emp_id employees.employee_id%TYPE;
    BEGIN
      FOR count IN 1 .. p_n LOOP
        FETCH cur_c INTO v_emp_id;
        EXIT WHEN cur_c%NOTFOUND;
        Dbms_Output.Put_Line('Employee Id: '|| v_emp_id);
      END LOOP;
      RETURN cur_c%FOUND;
    END next;
    PROCEDURE close IS
    BEGIN
      IF cur_c%ISOPEN THEN
        CLOSE cur_c;
      END IF;
    END close;
END curs_pkg;

-- call package curs_pkg
SET SERVEROUTPUT ON
EXECUTE curs_pkg.open;
DECLARE
  v_more BOOLEAN := curs_pkg.next(3);
BEGIN
  IF NOT v_more THEN
    curs_pkg.close;
  END IF;
END;
/
*/