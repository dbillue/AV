/*****************************
Author:       Duane Billue
Date:         2019-10-08
Description:  D80182GC20_ag1 :: Chapter 8 Using Explicit Cursors
*****************************/
-- Environment Variables
SET SERVEROUTPUT ON
SET AUTOPRINT ON

--desc employees

DECLARE
  v_deptno NUMBER := 80;
  v_last_name employees.last_name%TYPE;
  v_salary employees.salary%TYPE;
  v_manager_id employees.manager_id%TYPE;
  
  CURSOR c_emp_cursor IS
  SELECT last_name, salary, manager_id FROM employees WHERE department_id IN (v_deptno) ORDER BY last_name;
BEGIN
  OPEN c_emp_cursor;
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

--SELECT * FROM employees ORDER BY last_name;