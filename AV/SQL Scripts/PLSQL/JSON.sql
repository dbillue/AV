/*****************************
Author:       Duane Billue / Oracle
Date:         2019-11-07
Description:  Oracle JSON
*****************************/

------------------------
-- Create table
CREATE TABLE customer_followup AS 
(SELECT customer_id, cust_first_name, cust_last_name, cust_address, cust_email, account_mgr_id FROM customers);

-------------------------
-- Add column
ALTER TABLE customer_followup ADD (order_history VARCHAR2(4000));

--------------------------
-- Constraint
ALTER TABLE customer_followup
ADD CONSTRAINT json_check CHECK(order_history IS JSON);

--------------------------
-- Insert JSON data using JSON_OBJECT
INSERT INTO customer_followup 
(
  customer_id,
  cust_first_name, 
  cust_last_name, 
  cust_address,  
  cust_email,
  account_mgr_id,
  order_history
) SELECT 
    customer_id, 
    cust_first_name, 
    cust_last_name, 
    cust_address, 
    cust_email, 
    account_mgr_id,
    (
      SELECT JSON_OBJECT
      ('id' VALUE o.order_id, 'orderTotal' VALUE o.order_total, 'sales_rep_id' VALUE o.sales_rep_id) 
      FROM orders o 
      WHERE o.customer_id = co.customer_id)
  FROM customers co WHERE co.customer_id = 120;
  
-- SELECT * FROM orders WHERE customer_id IN (120);
-- SELECT * FROM customers WHERE customer_id = 120;
-- SELECT * FROM customer_followup WHERE customer_id = 120
-- DELETE FROM customer_followup WHERE customer_id IN (120)

--------------------------
-- SELECT JSON data using JSON_OBJECT
SELECT JSON_OBJECT
('ID' VALUE o.order_id, 'orderTotal' VALUE o.order_total, 'sales_rep_id' VALUE o.sales_rep_id) 
FROM orders o;

--------------------------
-- SELECT JSON data using JSON_ARRAY
SELECT JSON_ARRAY
(customer_id, sales_rep_id, order_total)
FROM orders o;

--------------------------
-- SELECT JSON data using JSON_OBJECTAGG
SELECT JSON_OBJECTAGG(product_name VALUE product_status) 
FROM product_information WHERE min_price > 2000;

--------------------------
-- SELECT JSON data using JSON_ARRAYAGG
SELECT JSON_ARRAYAGG(order_total) 
FROM orders WHERE sales_rep_id = 161;

--------------------------
-- SELECT JSON data using JSON_OBJECT and JSON_ARRAYAGG
SELECT JSON_OBJECT('customer_id' VALUE customer_id, 'number_of_orders' VALUE count(customer_id), 'orders' VALUE JSON_ARRAYAGG(order_total))
FROM orders
WHERE customer_id = 106 GROUP BY customer_id;

--------------------------
-- SELECT JSON data using dot(.) notation
SELECT cf.order_history.id FROM customer_followup cf WHERE customer_id IN (120);

--------------------------
-- SELECT JSON data using JSON function $ and dot notation
CREATE OR REPLACE FUNCTION get_order_value
(
  ord_his VARCHAR2
)
RETURN NUMBER AS
  BEGIN
    RETURN JSON_VALUE(ord_his, '$.orderTotal' RETURNING NUMBER);
    NULL;
  END get_order_value;
/
  
DECLARE
  jsonData  VARCHAR2(4000);
  ord_val   NUMBER;
BEGIN
  SELECT order_history INTO jsonData FROM customer_followup WHERE customer_id IN (120);
  ord_val := get_order_value(jsonData);
  DBMS_OUTPUT.PUT_LINE('The old order value is ' || ord_val);
  ord_val := ord_val + (.01 * ord_val);
  DBMS_OUTPUT.PUT_LINE('The new order value is ' || ord_val);
END;

--------------------------
-- SELECT JSON data using JSON data types JSON_ELEMENT_T and JSON_OBJECT_T
-- Functions: parse, is_Object, PUT
CREATE OR REPLACE PROCEDURE JSON_PUT_METHOD AS
  je JSON_ELEMENT_T;
  jo JSON_OBJECT_T;
BEGIN
  je := JSON_ELEMENT_T.parse('{"product_name": "GP 1280x1024"}');
  IF(je.is_Object) THEN
    jo := treat(je AS JSON_OBJECT_T);
    jo.put('price', 149.99);
  END IF;
  DBMS_OUTPUT.PUT_LINE(je.to_string);
END JSON_PUT_METHOD;
/

EXEC JSON_PUT_METHOD;

--------------------------
-- SELECT JSON using data type JSON_OBJECT_T
-- Functions: get_object, PUT, get_number, to_string
CREATE OR REPLACE PROCEDURE JSON_GET_METHODS AS
  in_data JSON_OBJECT_T;
  address JSON_OBJECT_T;
  zip NUMBER;
BEGIN
  in_data := new JSON_OBJECT_T('{"first_name" : "John", "last_name" : "Doe", "address" : {"country" : "USA", "zip" : "94065"}}');
  address := in_data.get_object('address');
  DBMS_OUTPUT.PUT_LINE(address.to_string);
  zip := address.get_number('zip');
  DBMS_OUTPUT.PUT_LINE(zip);
  DBMS_OUTPUT.PUT_LINE(address.to_string);
  address.PUT('zip', 12345);
  address.PUT('street', 'Detour Road');
  DBMS_OUTPUT.PUT_LINE(address.to_string);
END JSON_GET_METHODS;
/

EXEC JSON_GET_METHODS;

























