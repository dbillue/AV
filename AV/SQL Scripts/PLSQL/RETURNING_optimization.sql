/*****************************
Author:       Duane Billue
Date:         2019-11-11
Description:  RETURNING statement for optimization
*****************************/

SET TIMING ON

CREATE OR REPLACE PROCEDURE change_credit
(
  p_in_id IN customers.customer_id%TYPE,
  o_credit OUT NUMBER
)
IS
BEGIN
  UPDATE customers
  SET credit_limit = credit_limit * 1.10
  WHERE customer_id = p_in_id
  RETURNING credit_limit INTO o_credit;
END change_credit;
/
VARIABLE g_credit NUMBER
EXECUTE change_credit(109, :g_credit);
PRINT g_credit;