/*****************************
Author:       Duane Billue / Oracle
Date:         2019-11-08
Description:  Oracle JSON 2
*****************************/

------------------------
-- Create table
CREATE TABLE sales_report
(
  sales_rep_id      NUMBER(6, 0) PRIMARY KEY,
  sales_rep_fname   VARCHAR2(20),
  sales_rep_lname   VARCHAR2(20),
  order_details     VARCHAR2(4000)
);

-- DROP TABLE sales_report;
-- TRUNCATE TABLE sales_report;

------------------------
ALTER TABLE sales_report
ADD CONSTRAINT json_sales_report_json_check CHECK(order_details IS JSON);

------------------------
-- Populate sales_report table
INSERT INTO sales_report 
(
  sales_rep_id,
  sales_rep_fname, 
  sales_rep_lname,
  order_details
)
SELECT 
  DISTINCT o.sales_rep_id,
  e.First_Name, 
  e.Last_Name,
  (SELECT JSON_OBJECT('ID' VALUE 0) FROM DUAL)
FROM Orders o, HR.Employees e WHERE o.sales_rep_id = e.employee_id
ORDER BY o.sales_rep_id;

------------------------
-- Populate sales_report table w/ JSON array or orders per sales rep
UPDATE sales_report sr
SET ORDER_DETAILS = 
  (SELECT JSON_ARRAYAGG(JSON_OBJECT('order_id' VALUE o.order_id, 'customer_id' VALUE o.customer_id, 'order_value' VALUE o.order_total))
  FROM orders o
  WHERE o.sales_rep_id = sr.sales_rep_id
  GROUP BY sr.sales_rep_id);


--SELECT * FROM orders
--SELECT * FROM sales_report;
--SELECT JSON_OBJECT('ID' VALUE 0) FROM DUAL









