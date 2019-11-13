/*****************************
Author:       Duane Billue
Date:         2019-11-12
Description:  DBMS_Utility
*****************************/

SET TIMING ON
SET SERVEROUTPUT ON
SET AUTOPRINT ON

CREATE OR REPLACE PROCEDURE first_one IS
BEGIN
  DBMS_OUTPUT.PUT_LINE(SUBSTR(DBMS_UTILITY.FORMAT_CALL_STACK, 1, 255));
END first_one;
/

CREATE OR REPLACE PROCEDURE second_one IS
BEGIN
  null;
  first_one;
END second_one;
/

CREATE OR REPLACE PROCEDURE third_one IS
BEGIN
  NULL;
  NULL;
  second_one;
END third_one;
/

-- EXECUTE third_one;