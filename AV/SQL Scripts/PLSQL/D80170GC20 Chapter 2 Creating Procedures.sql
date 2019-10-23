/*****************************
Author:       Duane Billue
Date:         2019-10-22
Description:  D80170GC20 :: Chapter 2 Creating Procedures
*****************************/
-- Environment Variables
SET SERVEROUTPUT ON
SET AUTOPRINT ON


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

-- Call proc Upd_Job
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
