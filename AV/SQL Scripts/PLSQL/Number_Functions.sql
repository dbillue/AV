/*****************************
Author:       Duane Billue
Date:         2019-10-16
Description:  Number Functions
*****************************/
-- Environment Variables
--SET SERVEROUTPUT ON
--SET AUTOPRINT ON

-- CREATE TABLE orders AS SELECT * FROM oe.orders;

-- ABS (Absolute) --
SELECT ABS(-4015) FROM dual;
SELECT order_total, ABS(order_total), "ABS order_total" FROM orders ORDER BY order_total;

-- <><><><><><><><><><*>--<

-- ACOS (Cosine) --
SELECT ACOS(.4015) FROM dual;

-- <><><><><><><><><><*>--<

-- ASIN (ARC Sine) --
SELECT ASIN(.4015) FROM dual;

-- <><><><><><><><><><*>--<

-- ATAN (ARC Tangent) --
SELECT ATAN(.4015) FROM dual;

-- <><><><><><><><><><*>--<

-- ATAN2 (ARC Tangent n1, n2) --
SELECT ATAN2(4015, 1058) FROM dual;

-- <><><><><><><><><><*>--<

-- CEIL --
SELECT order_total, CEIL(order_total) FROM orders WHERE order_id IN (2434);

-- <><><><><><><><><><*>--<

-- EXP --
SELECT EXP(5) "e to the 5th power" FROM dual;

-- <><><><><><><><><><*>--<

-- FLOOR --
SELECT FLOOR(4015.2), FLOOR(-1058.9) FROM dual;

-- <><><><><><><><><><*>--<

-- LN --
SELECT LN(25) FROM dual;

-- <><><><><><><><><><*>--<

-- MOD (remainder floored) --
SELECT order_id, order_total, MOD(order_total, order_id) "MOD total / id" FROM orders ORDER BY order_id;

-- <><><><><><><><><><*>--<

-- NAN --
SELECT NANVL(sales_rep_id, 0) FROM orders ORDER BY order_id;

-- <><><><><><><><><><*>--<

-- POWER --
SELECT POWER(3,3) "Raised" FROM dual;
SELECT order_id, POWER(order_id, 2) "Raised to Power 2" FROM orders ORDER BY order_id;

-- <><><><><><><><><><*>--<

-- REMAINDER (rounded) --
SELECT REMAINDER(100,21) FROM dual;
SELECT order_id, order_total, REMAINDER(order_total, order_id) "Remainder" FROM orders ORDER BY order_id;

-- <><><><><><><><><><*>--<

-- ROUND --
SELECT order_id, order_total, ROUND(order_total) FROM orders ORDER BY order_id;

-- <><><><><><><><><><*>--<

-- SIGN --
SELECT SIGN(-4015) FROM dual;
SELECT order_id, order_total, SIGN(order_total) FROM orders ORDER BY order_id;

-- <><><><><><><><><><*>--<

-- SQRT --
SELECT SQRT(100), SQRT(4015), ROUND(SQRT(4015)) FROM dual;
SELECT order_id, order_total, SQRT(order_total), ROUND(SQRT(order_total)) FROM orders ORDER BY order_id;

-- <><><><><><><><><><*>--<

-- TRUNC --
SELECT TRUNC(4015.1058, 3) FROM dual;
SELECT order_id, order_total, TRUNC(SQRT(order_total), 2) FROM orders ORDER BY order_id;

SELECT * FROM orders ORDER BY order_id;