/*****************************
Author:       Duane Billue
Date:         2019-10-08
Description:  D80182GC20_ag1 :: Chapter 6 Control Structs
*****************************/
-- Environment Variables
SET SERVEROUTPUT ON
SET AUTOPRINT ON

/*
CREATE TABLE messages 
(
  results number default 0
)
*/

BEGIN
  FOR i IN 1..10
  LOOP
    IF i = 6 or i = 8 THEN
      NULL;
    ELSE
      INSERT INTO messages (results) VALUES (i);
    END IF;
  END LOOP;
  COMMIT;
END;
/

SELECT * FROM messages;
TRUNCATE TABLE messages;

--***************************************************--
--***************************************************--

--CREATE TABLE emps AS SELECT * FROM employees;
--ALTER TABLE emps ADD (stars VARCHAR2(50));
--DROP TABLE emps;

DECLARE
  v_empno emps.employee_id%TYPE := 176;
  v_asterisk emps.stars%TYPE := NULL;
  v_sal emps.salary%TYPE;
BEGIN
  SELECT NVL(ROUND(salary/1000), 0) INTO v_sal FROM emps WHERE employee_id = v_empno;
  DBMS_OUTPUT.PUT_LINE(v_sal);
  
  FOR i IN 1..TO_NUMBER(v_sal)
  LOOP
    v_asterisk := v_asterisk || '*';
    --DBMS_OUTPUT.PUT_LINE(v_asterisk);
  END LOOP;
  
  UPDATE emps SET stars = v_asterisk WHERE employee_id = v_empno;
  COMMIT;
END;
/

SELECT * FROM emps WHERE employee_id = 176;