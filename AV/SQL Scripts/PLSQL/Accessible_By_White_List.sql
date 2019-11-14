/*****************************
Author:       Duane Billue / Oracle
Date:         2019-11-13
Description:  Accessible By (White List)
*****************************/

SET SERVEROUTPUT ON;
SET AUTOPRINT ON;
SET TIMING ON;

---------------------------------------
--  Accessible clause in procedures  --
---------------------------------------
CREATE OR REPLACE PROCEDURE top_protected_proc 
  ACCESSIBLE BY (PROCEDURE top_trusted_proc) AS   -- Accessible clause
BEGIN
  DBMS_OUTPUT.PUT_LINE('Executed top_protected_proc');
END top_protected_proc;
/

CREATE OR REPLACE PROCEDURE top_trusted_proc AS
BEGIN
  --DBMS_OUTPUT.PUT_LINE('top_trusted_proc calls top_protected_proc');
  --top_protected_proc;
  DBMS_OUTPUT.PUT_LINE('top_trusted_proc calls protected_pkg.private_proc');
  protected_pkg.private_proc;
END top_trusted_proc;
/

--EXEC top_trusted_proc;

---------------------------------------
--  Accessible clause in packages    --
---------------------------------------
CREATE OR REPLACE PACKAGE protected_pkg AS
  PROCEDURE public_proc;
  PROCEDURE private_proc ACCESSIBLE BY(PROCEDURE top_trusted_proc);
END protected_pkg;
/

CREATE OR REPLACE PACKAGE BODY protected_pkg
AS
  PROCEDURE public_proc AS
  BEGIN
    DBMS_OUTPUT.PUT_LINE('Executed protected_pkg.public_proc');
  END;
  PROCEDURE private_proc ACCESSIBLE BY (PROCEDURE top_trusted_proc) AS
  BEGIN
    DBMS_OUTPUT.PUT_LINE('Executed protected_pkg.private_proc');
  END;
END protected_pkg;
/

































