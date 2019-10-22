/*****************************
Author:       Duane Billue
Date:         2019-10-21
Description:  Employees Report (2005+ Hirees )
*****************************/

--SET SERVEROUTPUT ON;
ALTER SESSION SET TIME_ZONE = '-04:00';
ALTER SESSION SET NLS_DATE_FORMAT = 'DD-MON-YYYY';

DECLARE
  v_emp_id employees.employee_id%TYPE;
  v_last_name employees.last_name%TYPE;
  v_first_name employees.first_name%TYPE;
  v_hire_date employees.hire_date%TYPE;
  v_dept_name departments.department_name%TYPE;
  v_current_date DATE := '01-JAN-2005';
  
  CURSOR c_emp_cursor IS
  SELECT e.employee_id, e.last_name, e.first_name, e.hire_date, d.department_name
  FROM employees e JOIN departments d ON e.department_id = d.department_id
  WHERE 1 = 1
    AND v_current_date > e.hire_date
  ORDER BY d.department_id, e.hire_date, e.last_name;
    
BEGIN
  IF NOT c_emp_cursor%ISOPEN THEN
    OPEN c_emp_cursor;
  END IF;
  
  -- Debug
  -- DBMS_OUTPUT.PUT_LINE('v_current_date: ' || v_current_date);
  -- DBMS_OUTPUT.PUT_LINE('H1');
  
  LOOP
    FETCH c_emp_cursor INTO v_emp_id, v_last_name, v_first_name, v_hire_date, v_dept_name;
    EXIT WHEN c_emp_cursor%NOTFOUND;
    
    DBMS_OUTPUT.PUT_LINE(v_emp_id || ',' || v_first_name || ' ' || v_last_name || ', ' || v_hire_date || ', ' || v_dept_name);
  END LOOP;
  
  CLOSE c_emp_cursor;
END;
/