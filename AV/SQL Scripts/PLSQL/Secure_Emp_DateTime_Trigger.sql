/*****************************
Author:       Duane Billue
Date:         2019-10-29
Description:  D80170GC20 :: Chapter 9 Triggers
*****************************/

SET SERVEROUTPUT ON
SET AUTOPRINT ON

CREATE OR REPLACE TRIGGER secure_emp
BEFORE INSERT ON employees
BEGIN
  IF (TO_CHAR(SYSDATE, 'DY') IN ('SAT', 'SUN')) OR (TO_CHAR(SYSDATE, 'HH24:MI') NOT BETWEEN '08:00' AND '18:00') THEN
    RAISE_APPLICATION_ERROR(-20000, 'You may insert into employees tables during normal business hours');
  END IF;
END;
