/*****************************
Author:       Duane Billue
Date:         2019-10-31
Description:  Procedures
*****************************/

-- Environment Variables
-- SET SERVEROUTPUT ON;
-- SET AUTOPRINT ON;

-- PL/SQL SubPrograms --

Create Or Replace Procedure Hiya As
BEGIN
  DBMS_OUTPUT.PUT_LINE('Hiya');
END Hiya;

--EXECUTE Hiya

--=================================--
--    Standard Proc w/ IN param    --
--=================================--
Create Or Replace Procedure Query_Emp
(
  Emp_Id In Employees.Employee_Id%Type
) As
  V_Name Employees.Last_Name%Type;
  V_Salary Employees.Salary%Type;
Begin
    Select Last_Name, Salary Into V_Name, V_Salary From Employees
  Where 1 = 1
    And Employee_Id In (Emp_Id);
    DBMS_OUTPUT.PUT_LINE('Employee: ' || V_Name || ' makes $' || V_Salary);
End Query_Emp;

Begin
  Query_Emp(110);
End;

--=================================--
--    Standard Proc w/ NO param    --
--=================================--
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

--=====================================--
--  Standard Proc w/ IN and OUT param  --
--=====================================--
Create Or Replace Procedure Emp_Profile
(
  V_Emp_Id In Employees.Employee_Id%Type,
  V_Emp_Record OUT EMPLOYEES%Rowtype
) As
Begin
  SELECT * INTO V_Emp_Record FROM Employees WHERE employee_id IN (V_Emp_Id);
End Emp_Profile;

-- Call proc Emp_Profile
Declare
  v_emp_record EMPLOYEES%ROWTYPE;
Begin
  Emp_Profile(110, V_Emp_Record);
  Dbms_Output.Put_Line(v_emp_record.First_Name || ' ' || v_emp_record.Last_Name);
End;
/

--=================================--
--  Standard Proc w/ IN OUT param  --
--=================================--
Create Or Replace Procedure Emp_Phone
(
  v_phone_number IN VARCHAR2,
  v_formatted_phone OUT VARCHAR2
)
IS
Begin
  v_formatted_phone := '(' || SUBSTR(V_Phone_Number, 1, 3) || ')' 
                        || ' ' || SUBSTR(V_Phone_Number, 4, 3) || '-'
                        || SUBSTR(V_Phone_Number, 7);
End Emp_Phone;

-- Call proc Emp_Phone (parameter order)
Declare
  v_formatted_phone_no VARCHAR2(50);
Begin
  Emp_Phone('4046491322', v_formatted_phone_no);
  DBMS_OUTPUT.PUT_LINE(v_formatted_phone_no);
End;
/

-- Call proc Emp_Phone
Declare
  v_formatted_phone VARCHAR2(50);
BEGIN
  Emp_Phone('8004109874', v_formatted_phone);
  DBMS_OUTPUT.PUT_LINE(v_formatted_phone);
END;
/

--=================================--
--  Standard Proc w/ NO param      --
--=================================--
Create Or Replace Procedure Upd_Job
(
  V_Id Jobs.Job_Id%Type,
  v_title jobs.job_title%TYPE
) 
As
Begin
  Update Jobs Set Job_Title = V_Title Where Job_Id In (V_Id);
  If Sql%Notfound Then
    RAISE_APPLICATION_ERROR(-20203, 'Error updating title for job id ' || v_Id);
  End If;
End Upd_Job;

execute upd_job('IT_WEB', 'WEB MASTER');


--=================================--
--  Standard Proc w/ IN param      --
--=================================--
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