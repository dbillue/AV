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


























