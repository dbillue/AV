/*****************************
Author:       Duane Billue
Date:         2019-10-29
Description:  Triggers
*****************************/

-- Environment Variables
--SET SERVEROUTPUT ON
--SET AUTOPRINT ON

--=================================--
--          Trigger                --
--=================================--
CREATE OR REPLACE TRIGGER secure_emp
BEFORE INSERT ON employees
BEGIN
  IF (TO_CHAR(SYSDATE, 'DY') IN ('SAT', 'SUN')) OR (TO_CHAR(SYSDATE, 'HH24:MI') NOT BETWEEN '08:00' AND '18:00') THEN
    RAISE_APPLICATION_ERROR(-20000, 'You may insert into employees tables during normal business hours');
  END IF;
END;

--=================================--
--     Trigger WHEN Clause         --
--=================================--
CREATE OR REPLACE TRIGGER derive_comm_pct
BEFORE INSERT OR UPDATE OF salary ON employees
FOR EACH ROW
WHEN (NEW.job_id = 'SA_REP')
BEGIN
  IF INSERTING THEN
    :NEW.commission_pct := 0;
  ELSIF :OLD.commission_pct IS NULL THEN
    :NEW.commission_pct := 0;
  ELSE
    :NEW.commission_pct := :OLD.commission_pct + 0.05;
  END IF;
END;

--=================================--
--     Trigger For Each Row        --
--=================================--
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

--=================================--
--     Trigger For Each Row        --
--	 UPDATE On Explicit Column     --
--=================================--
CREATE OR REPLACE TRIGGER restrict_salary
BEFORE INSERT OR UPDATE OF salary ON employees
FOR EACH ROW
BEGIN
  IF NOT (:NEW.job_id IN ('AD_PRES', 'AD_VP')) AND :NEW.salary > 15000 THEN
    RAISE_APPLICATION_ERROR(-20000, 'Employee cannot earn more then $15K salary');
  END IF;
END;

/*
  -- SELECT * FROM Employees ORDER BY employee_id DESC
  -- UPDATE Employees SET job_id = 'AD_VP' WHERE employee_id IN (226);
*/

--=================================--
--     Trigger Conditional         --
--=================================--
CREATE OR REPLACE TRIGGER secure_emp_expanded 
BEFORE INSERT OR UPDATE OR DELETE on employees
BEGIN
  IF (TO_CHAR(SYSDATE, 'DY') IN ('SAT', 'SUN')) OR (TO_CHAR(SYSDATE, 'HH24:MI') NOT BETWEEN '08:00' AND '18:00') THEN
    IF DELETING THEN 
      RAISE_APPLICATION_ERROR(-20000, 'You may delete from the employees table only during normal business hours');
    ELSIF INSERTING THEN
      RAISE_APPLICATION_ERROR(-20001, 'You may insert into the employees table during normal business hours');
    ELSIF UPDATING ('SALARY') THEN
      RAISE_APPLICATION_ERROR(-20002, 'You may update the salary during normal business hours');
    ELSE
      RAISE_APPLICATION_ERROR(-20003, 'You may update the employees table during normal business hours');
    END IF;
  END IF;
END;