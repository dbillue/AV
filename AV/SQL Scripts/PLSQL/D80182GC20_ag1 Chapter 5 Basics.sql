/*****************************
Author:       Duane Billue
Date:         2019-10-08
Description:  D80182GC20_ag1 :: Chapter 5 Basics
*****************************/
-- Environment Variables
SET SERVEROUTPUT ON
SET AUTOPRINT ON

--DESC departments;

DECLARE 
  v_dept_id NUMBER := 0;
  v_max_dept_id departments.department_id%TYPE;
  v_dept_name departments.department_name%TYPE := 'Education';
BEGIN
  SELECT MAX(department_id) INTO v_max_dept_id from departments;
  dbms_output.put_line('Max dept id: ' || v_max_dept_id);
  
  v_dept_id := v_max_dept_id + 10;
  
  INSERT INTO departments (department_id, department_name, location_id)
            VALUES (v_dept_id, v_dept_name, null);
  
  BEGIN <<UpdateDeptRec>>
    UPDATE departments SET location_id = 3000 WHERE department_id = v_dept_id;
  END;
END;
/

--SELECT * FROM employees
SELECT * FROM departments ORDER BY department_id desc;
ROLLBACK;
