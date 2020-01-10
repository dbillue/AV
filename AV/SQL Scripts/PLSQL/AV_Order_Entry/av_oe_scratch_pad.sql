/*****************************
Author:       Duane Billue
Date:         2019-11-23
Description:  AV Main
*****************************/

/*********
Orders
*********/
DESC orders;

SELECT * FROM orders;

INSERT INTO orders (order_date, order_status, order_total) VALUES (sysdate, 1, 105.99);

-- DROP TABLE orders;
-- TRUNCATE TABLE orders;


/*********
OrderItems
*********/
DESC orderitems;

SELECT * FROM orderitems;

INSERT INTO orderitems (order_id, orderitems_json_list) VALUES (1,'name:CD-Rom');

-- DROP TABLE orderitems;
-- TRUNCATE TABLE orderitems;


/*********
Products
*********/
DESC Products;

SELECT * FROM Products;

INSERT INTO Products (product_image, product_values) VALUES (EMPTY_BLOB(), EMPTY_CLOB());

-- DROP TABLE orderitems;
-- TRUNCATE TABLE orderitems;

