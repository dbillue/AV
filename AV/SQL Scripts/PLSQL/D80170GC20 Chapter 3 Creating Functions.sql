/*****************************
Author:       Duane Billue
Date:         2019-10-22
Description:  D80170GC20 :: Chapter 3 Creating Functions
*****************************/

-- Environment Variables
Set Serveroutput On
Set Autoprint On

-- Function Tax
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

SELECT employee_id, last_name, salary, Tax(110) "Tax Amount" FROM employees WHERE department_id IN (100);