/*****************************
Author:       Duane Billue
Date:         2019-10-11
Description:  D80182GC20_ag1 :: Chapter 9 Handling Exceptions
*****************************/
-- Environment Variables
SET SERVEROUTPUT ON
SET AUTOPRINT ON

--DESC employees;

/*
DECLARE
  v_last_name employees.last_name%TYPE := 'Smith';
  v_emp_no employees.employee_id%TYPE;
BEGIN
  SELECT employee_id INTO v_emp_no FROM employees WHERE last_name = v_last_name;
  EXCEPTION
    WHEN TOO_MANY_ROWS THEN
    DBMS_OUTPUT.PUT_LINE('To many rows returned...use a cursor.');
END;
/
*/

-- Internally defined exception trapping --
DECLARE
  e_insert_excep EXCEPTION;
  PRAGMA EXCEPTION_INIT(e_insert_excep, -01400);
BEGIN
  INSERT INTO departments (department_id, department_name) VALUES (412, NULL);
  EXCEPTION
    WHEN e_insert_excep THEN
    DBMS_OUTPUT.PUT_LINE('Insert failed.');
END;
/

-- Trapping predefined exceptions --


-- Trapping user defined exceptions --


-- Propagating exceptions in a Sub-Block --


-- Raise_Application_Error --


