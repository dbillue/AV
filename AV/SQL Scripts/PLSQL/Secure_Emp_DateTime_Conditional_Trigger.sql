/*****************************
Author:       Duane Billue
Date:         2019-10-29
Description:  D80170GC20 :: Chapter 9 Triggers
*****************************/

SET SERVEROUTPUT ON
SET AUTOPRINT ON

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
      RAISE_APPLICATION_ERROR(-20001, 'You may update the employees table during normal business hours');
    END IF;
  END IF;
END;