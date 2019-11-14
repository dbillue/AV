/*****************************
Author:       Duane Billue / Oracle
Date:         2019-11-13
Description:  Policy Functions
*****************************/

SET SERVEROUTPUT ON;
SET AUTOPRINT ON;
SET TIMING ON;

CREATE OR REPLACE FUNCTION auth_orders
(
  schema_var IN VARCHAR2,
  table_var IN VARCHAR2
)
RETURN VARCHAR2
IS
  return_val VARCHAR2(400);
BEGIN
  return_val := 'SALES_REP_ID = 159';
  RETURN return_val;
END auth_orders;
/

-----------------------------
--   DBMS_RLS.ADD_POLICY   --
-----------------------------
DECLARE
BEGIN DBMS_RLS.ADD_POLICY
(
  object_schema   => 'oe',
  object_name     => 'orders',
  policy_name     => 'orders_policy',
  function_schema => 'sys',
  policy_function => 'auth_orders',
  statement_types => 'select'
);
END;
/

