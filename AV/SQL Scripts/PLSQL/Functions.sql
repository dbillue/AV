/*****************************
Author:       Duane Billue
Date:         2019-10-31
Description:  Functions
*****************************/

-- Environment Variables
-- SET SERVEROUTPUT ON;
-- SET AUTOPRINT ON;

-- PL/SQL SubPrograms --

-- Functions --
-- DROP FUNCTION employee_full_name;
CREATE OR REPLACE FUNCTION employee_full_name (v_emp_id employees.employee_id%TYPE) RETURN VARCHAR2 IS
    v_first_name employees.first_name%TYPE;
    v_last_name employees.last_name%TYPE;
    v_full_name VARCHAR2(250);
BEGIN
    SELECT first_name, last_name INTO v_first_name, v_last_name FROM employees WHERE employee_id IN (v_emp_id);
    v_full_name := v_first_name || ' ' || v_last_name;
    RETURN v_full_name;
END;
/

DECLARE
    err_code NUMBER;
    err_msg VARCHAR2(1000);
    v_emp_full_name varchar2(250);
BEGIN
	 -- Call function employee_full_name(n)
     v_emp_full_name := employee_full_name (106);
	 
     INSERT INTO employee_names (full_name) VALUES (v_emp_full_name);
     EXCEPTION
        WHEN OTHERS THEN
            err_code := SQLCODE;
            err_msg := SQLERRM; 
            INSERT INTO errors (e_user, e_date, error_code, error_message) VALUES (USER, SYSDATE, err_code, err_msg); 
END;
/

SELECT * FROM errors;
SELECT * FROM employees;
SELECT * FROM employee_names;

--========================================--
Create Or Replace Function Tax
(
  p_id in Employees.Employee_id%TYPE
)
Return Number Is
  v_sal employees.salary%TYPE;
Begin
  Select Salary Into V_Sal From Employees Where Employee_Id In (P_Id);
  RETURN (V_sal * 0.08);
End Tax;
/

-- Call function Tax(n)
SELECT employee_id, last_name, salary, Tax(110) "Tax Amount" FROM employees WHERE department_id IN (100);