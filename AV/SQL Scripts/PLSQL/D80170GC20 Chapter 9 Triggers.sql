/*****************************
Author:       Duane Billue
Date:         2019-10-29
Description:  D80170GC20 :: Chapter 9 Triggers
*****************************/

SET SERVEROUTPUT ON
SET AUTOPRINT ON

CREATE OR REPLACE PROCEDURE check_salary
(
  p_emp_job_id employees.job_id%TYPE,
  p_emp_salary employees.salary%TYPE
) IS
v_min_sal jobs.min_salary%TYPE;
v_max_sal jobs.max_salary%TYPE;
e_sal_range EXCEPTION;
PRAGMA EXCEPTION_INIT(e_sal_range, -22001);
BEGIN
  SELECT min_salary, max_salary INTO v_min_sal, v_max_sal 
  FROM jobs WHERE job_id IN (p_emp_job_id);
  -- Debug.
  DBMS_OUTPUT.PUT_LINE('p_emp_job_id: ' || p_emp_job_id);
  DBMS_OUTPUT.PUT_LINE('p_emp_salary: ' || p_emp_salary);
  DBMS_OUTPUT.PUT_LINE('jobs.min_salary: ' || v_min_sal);
  DBMS_OUTPUT.PUT_LINE('jobs.v_max_sal: ' || v_max_sal);
  
  IF p_emp_salary < v_min_sal or p_emp_salary > v_max_sal THEN
    RAISE e_sal_range;
  END IF;
  
  EXCEPTION
  WHEN e_sal_range THEN
    DBMS_OUTPUT.PUT_LINE('Invalid salary ' || p_emp_salary || 
                        '. Salaries for job ' || p_emp_job_id ||
                        ' must be between ' || v_min_sal || ' and ' || v_max_sal);
END check_salary;
/

/*
BEGIN
  -- check_salary ('IT_PROG', 95000);
  -- DELETE FROM employees WHERE employee_id IN (222, 223, 224, 216, 217);
  INSERT INTO employees (employee_id, first_name, last_name, email, hire_date, job_id, salary) 
  VALUES 
  (employees_seq.NEXTVAL, 'Barney', 'Rubble', 'BRUBBLE', SYSDATE, 'SA_REP', 115000);
  -- SELECT * FROM Employees ORDER BY employee_id DESC;
  -- SELECT * FROM Jobs;
  execute emp_pkg.add_employee('Ellenor', 'Able', 'FI_ACCOUNT', 110, 30);
END;
*/