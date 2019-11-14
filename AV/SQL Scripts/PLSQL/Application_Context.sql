/*****************************
Author:       Duane Billue / Oracle
Date:         2019-11-13
Description:  Application Context
*****************************/

SET SERVEROUTPUT ON;
SET AUTOPRINT ON;
SET TIMING ON;

-- Grant statement
GRANT CREATE ANY CONTEXT TO OE;

SELECT SYS_CONTEXT ('USERENV', 'SESSION_USER') FROM DUAL;
SELECT SYS_CONTEXT ('USERENV', 'DB_NAME') FROM DUAL;


----------------------------------
--  Create Application Context  --
----------------------------------
CREATE OR REPLACE CONTEXT order_ctx USING oe.orders_app_pkg;


-- Package orders_app_pkg
CREATE OR REPLACE PACKAGE orders_app_pkg IS
  PROCEDURE show_app_context;
  PROCEDURE set_app_context;
  FUNCTION the_predicate
  (p_schema VARCHAR2, p_name VARCHAR2) RETURN VARCHAR2;
END orders_app_pkg;
/

CREATE OR REPLACE PACKAGE BODY orders_app_pkg IS
  c_context CONSTANT VARCHAR2(30) := 'ORDER_CTX';
  c_attrib CONSTANT VARCHAR2(30) := 'ACCOUNT_MGR';
  
  PROCEDURE show_app_context IS
  BEGIN
    DBMS_OUTPUT.PUT_LINE('TYPE: ' || c_attrib || ' - ' || SYS_CONTEXT(c_context, c_attrib));
  END show_app_context;
  
  PROCEDURE set_app_context IS
    v_user VARCHAR2(30);
  BEGIN
    SELECT user INTO v_user FROM DUAL;
    DBMS_SESSION.SET_CONTEXT(c_context, 'ACCOUNT_MGR', v_user);
  END;
  
  FUNCTION the_predicate
  (
    p_schema VARCHAR2, 
    p_name VARCHAR2
  )
  RETURN VARCHAR2
  IS
    v_context_value VARCHAR2(100) := SYS_CONTEXT(c_context, c_attrib);
    v_restriction VARCHAR2(2000);
  BEGIN
    IF v_context_value LIKE 'AM%' THEN
      v_restriction := 'ACCOUNT_MGR_ID = SUBSTR(''' || v_context_value || ''', 3, 3)';
    ELSE
      v_restriction := NULL;
    END IF;
    RETURN v_restriction;
  END;
  
END orders_app_pkg;
/

--SELECT SYS_CONTEXT ('order_ctx', 'Account_Manager') FROM DUAL;

-----------------------------
--   DBMS_RLS.ADD_POLICY   --
-----------------------------
DECLARE
BEGIN DBMS_RLS.ADD_POLICY
(
  'oe',
  'Customers',
  'OE_Access_Policy',
  'sys',
  'orders_app_pkg.the_predicate',
  'select, update, delete',
  FALSE,
  TRUE
);
END;
/

-----------------------
--   Logon Trigger   --
-----------------------
CREATE OR REPLACE TRIGGER set_id_on_logon
AFTER logon ON DATABASE
BEGIN
  oe.orders_app_pkg.set_app_context;
END;
/




