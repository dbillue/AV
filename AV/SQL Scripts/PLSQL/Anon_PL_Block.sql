/*****************************
Author:       Duane Billue
Date:         2019-10-08
Description:  PL/SQL Blocks
*****************************/

-- Environment Variables
SET SERVEROUTPUT ON
SET AUTOPRINT ON

--========================================--
DECLARE
	v_msg VARCHAR2(50) := '';
BEGIN
	DBMS_OUTPUT.PUT_LINE(v_msg);
END;


--========================================--
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

--========================================--
/*
CREATE TABLE messages 
(
  results number default 0
)
*/

BEGIN
  FOR i IN 1..10
  LOOP
    IF i = 6 or i = 8 THEN
      NULL;
    ELSE
      INSERT INTO messages (results) VALUES (i);
    END IF;
  END LOOP;
  COMMIT;
END;
/

SELECT * FROM messages;
TRUNCATE TABLE messages;

--========================================--
--CREATE TABLE emps AS SELECT * FROM employees;
--ALTER TABLE emps ADD (stars VARCHAR2(50));
--DROP TABLE emps;

DECLARE
  v_empno emps.employee_id%TYPE := 176;
  v_asterisk emps.stars%TYPE := NULL;
  v_sal emps.salary%TYPE;
BEGIN
  SELECT NVL(ROUND(salary/1000), 0) INTO v_sal FROM emps WHERE employee_id = v_empno;
  DBMS_OUTPUT.PUT_LINE(v_sal);
  
  FOR i IN 1..TO_NUMBER(v_sal)
  LOOP
    v_asterisk := v_asterisk || '*';
    --DBMS_OUTPUT.PUT_LINE(v_asterisk);
  END LOOP;
  
  UPDATE emps SET stars = v_asterisk WHERE employee_id = v_empno;
  COMMIT;
END;
/

SELECT * FROM emps WHERE employee_id = 176;