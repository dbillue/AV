/*****************************
Author:       Duane Billue
Date:         2019-10-08
Description:  Explicit Cursors
*****************************/
-- Environment Variables
SET SERVEROUTPUT ON
SET AUTOPRINT ON

--DESC employees

DECLARE
  v_deptno NUMBER := 10;
  v_last_name employees.last_name%TYPE;
  v_salary employees.salary%TYPE;
  v_manager_id employees.manager_id%TYPE;
  v_cursor_is_open BOOLEAN DEFAULT FALSE;
  
  CURSOR c_emp_cursor IS
  SELECT last_name, salary, manager_id FROM employees WHERE department_id IN (v_deptno) ORDER BY last_name;
  
BEGIN
  IF NOT c_emp_cursor%ISOPEN THEN
    OPEN c_emp_cursor;
  END IF;
  
  LOOP
    FETCH c_emp_cursor INTO v_last_name, v_salary, v_manager_id;
    EXIT WHEN c_emp_cursor%NOTFOUND;
    
    IF v_salary < 5000 THEN
      CASE v_manager_id
        WHEN 101 THEN DBMS_OUTPUT.PUT_LINE(v_last_name || ' is due for a raise!');
        WHEN 124 THEN DBMS_OUTPUT.PUT_LINE(v_last_name || ' is due for a raise!');
        ELSE NULL;
      END CASE;
    ELSE
      DBMS_OUTPUT.PUT_LINE(v_last_name || ' is not due for a raise!');
    END IF;
  END LOOP;
END;
/

--SELECT * FROM employees ORDER BY last_name

--<><><><><><><><><><><><><><><><><*>--<~
--<><><><><><><><><><><><><><><><><*>--<~

-- DESC departments;
-- DESC employees;

DECLARE
  v_dept_id departments.department_id%TYPE;
  v_dept_name departments.department_name%TYPE;
  v_emp_last_name employees.last_name%TYPE;
  v_emp_job_id employees.job_id%TYPE;
  v_emp_hire_date employees.hire_date%TYPE;
  v_emp_salary employees.salary%TYPE;

  CURSOR c_dept_cursor IS
    SELECT department_id, department_name 
    FROM departments 
    WHERE department_id < 100 
    ORDER BY department_id;
  
  CURSOR c_emp_cursor (v_dept_id NUMBER) IS
    SELECT e.last_name, e.job_id, e.hire_date, e.salary 
    FROM departments d JOIN employees e ON d.department_id = e.department_id 
    WHERE e.employee_id < 120 AND d.department_id = v_dept_id;
  
BEGIN
  IF NOT c_dept_cursor%ISOPEN THEN
    OPEN c_dept_cursor;
  END IF;
  
  LOOP <<department>>
    FETCH c_dept_cursor INTO v_dept_id, v_dept_name;
    EXIT WHEN c_dept_cursor%NOTFOUND;
    DBMS_OUTPUT.PUT_LINE('Dept Id: ' || v_dept_id || ' Dept Name: ' || v_dept_name);
  
    LOOP
      IF NOT c_emp_cursor%ISOPEN THEN
        OPEN c_emp_cursor (v_dept_id);
      END IF;
      
      FETCH c_emp_cursor INTO v_emp_last_name, v_emp_job_id, v_emp_hire_date, v_emp_salary;
      EXIT WHEN c_emp_cursor%NOTFOUND;
      DBMS_OUTPUT.PUT_LINE('Last Name: ' || v_emp_last_name || ' Job Id: ' || v_emp_job_id || ' Hire Date: ' || v_emp_hire_date || ' Emp Salary: ' || v_emp_salary);
    END LOOP;
    DBMS_OUTPUT.PUT_LINE('-----------------------------------------');
    CLOSE c_emp_cursor;
  END LOOP;
  CLOSE c_dept_cursor;
END;
/

--===========================--
--     Reference Cursors     --
--===========================--
DECLARE
  TYPE EmpCurType IS REF CURSOR;      -- weak REFERENCE cursor
  emp_cv EmpCurType;                  -- cursor variable
  emp_rec employees%ROWTYPE;          -- conposite variable
  sql_stmt VARCHAR2(200);
  my_job VARCHAR2(10) := 'ST_CLERK';  -- to beused as bind variable
BEGIN
	sql_stmt :=   'SELECT * FROM employees 
                WHERE job_id = :j';
  
  -- Open cursor
  OPEN emp_cv FOR sql_stmt USING my_job;  -- OPEN - FOR statement
  LOOP
    FETCH emp_cv INTO emp_rec;
    EXIT WHEN emp_cv%NOTFOUND;
      dbms_output.put_line(emp_rec.employee_id || ', ' || emp_rec.first_name || emp_rec.last_name || ', ' || emp_rec.department_id);
  END LOOP;
  CLOSE emp_cv;
END;
/