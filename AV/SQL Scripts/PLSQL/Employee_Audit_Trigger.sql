/*****************************
Author:       Duane Billue
Date:         2019-10-29
Description:  D80170GC20 :: Chapter 9 Triggers
*****************************/

SET SERVEROUTPUT ON
SET AUTOPRINT ON

/*
CREATE TABLE audit_emp
(
  user_name     VARCHAR2(30),
  time_stamp    DATE,
  id            NUMBER(6),
  old_last_name VARCHAR2(25),
  new_last_name VARCHAR2(25),
  old_title     VARCHAR2(10),
  new_title     VARCHAR2(10),
  old_salary    NUMBER(8,2),
  new_salary    NUMBER(8,2)
)
*/

CREATE OR REPLACE TRIGGER audit_emp
AFTER DELETE OR INSERT OR UPDATE on employees
FOR EACH ROW
BEGIN
  INSERT INTO audit_emp (user_name, time_stamp, id, old_last_name, new_last_name, old_title, new_title, old_salary, new_salary)
  VALUES
  (USER, SYSDATE, :OLD.employee_id, :OLD.last_name, :NEW.last_name, :OLD.job_id, :NEW.job_id, :OLD.salary, :NEW.salary);
END;
