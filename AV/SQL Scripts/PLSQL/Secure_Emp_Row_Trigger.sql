/*****************************
Author:       Duane Billue
Date:         2019-10-29
Description:  D80170GC20 :: Chapter 9 Triggers
*****************************/

SET SERVEROUTPUT ON
SET AUTOPRINT ON

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