/*****************************
Author:       Duane Billue
Date:         2019-10-14
Description:  D80182GC20_ag1 :: Chapter 10 Creating Procedures
*****************************/
-- Environment Variables
-- SET SERVEROUTPUT ON;
-- SET AUTOPRINT ON;

-- SELECT * FROM cards;
-- SELECT * FROM errors;
-- CREATE TABLE departments AS SELECT * FROM hr.departments;
-- CREATE TABLE employees AS SELECT * FROM hr.employees;
-- CREATE TABLE employee_names (full_name VARCHAR2(250));
-- DROP TABLE departments;
-- DROP TABLE employees;

-- PL/SQL SubPrograms --

-- Procedures --
-- DROP PROCEDURE department_i;
CREATE OR REPLACE PROCEDURE department_i IS
    v_dept_id departments.department_id%TYPE;
    v_department_name departments.department_name%TYPE;
BEGIN
    v_dept_id := 280;
    v_department_name := 'Accounting';
    
    DELETE FROM departments WHERE department_id IN (v_dept_id);
    INSERT INTO departments (department_id, department_name) VAlUES (v_dept_id, v_department_name);
END;
/

BEGIN
    department_i;
END;
/

SELECT * FROM departments ORDER BY department_id;


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
