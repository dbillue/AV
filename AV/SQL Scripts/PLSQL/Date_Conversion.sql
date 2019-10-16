/*****************************
Author:       Duane Billue
Date:         2019-10-15
Description:  Date and Time Conversion
*****************************/

--SET SERVEROUTPUT ON;
--SET AUTOPRINT ON;
--CREATE TABLE employees AS SELECT * FROM hr.employees;
--CREATE TABLE orders AS SELECT * FROM oe.orders;
--SELECT * FROM V$TIMEZONE_NAMES WHERE TZABBREV IN ('EST', 'CST', 'MST', 'PST');
--SELECT TZ_OFFSET('US/Eastern'), TZ_OFFSET('US/CENTRAL'), TZ_OFFSET('US/MOUNTAIN'), TZ_OFFSET('US/PACIFIC') FROM DUAL;
--SELECT DBTIMEZONE FROM DUAL;

-- Set time zone to US/EASTERM
ALTER SESSION SET TIME_ZONE = '-04:00';
ALTER SESSION SET NLS_DATE_FORMAT = 'DD-MON-YYYY HH24:MI:SS';
DECLARE
    v_current_date DATE;
    v_sysdate DATE;
    v_systimestamp TIMESTAMP;
    v_current_timestamp TIMESTAMP WITH TIME ZONE;
    v_session_timezone TIMESTAMP WITH TIME ZONE;
    v_db_timezone TIMESTAMP WITH TIME ZONE;
BEGIN
    DBMS_OUTPUT.PUT_LINE('CURRENT_DATE: ' || CURRENT_DATE);
    DBMS_OUTPUT.PUT_LINE('SYSDATE: ' || SYSDATE);
    DBMS_OUTPUT.PUT_LINE('SYSTIMESTAMP: ' || SYSTIMESTAMP);
    DBMS_OUTPUT.PUT_LINE('CURRENT_TIMESTAMP: ' || CURRENT_TIMESTAMP);
    DBMS_OUTPUT.PUT_LINE('SESSIONTIMEZONE: ' || SESSIONTIMEZONE);
    DBMS_OUTPUT.PUT_LINE('DBTIMEZONE: ' || DBTIMEZONE);
    
    SELECT CURRENT_DATE, SYSDATE, SYSTIMESTAMP, CURRENT_TIMESTAMP 
    INTO v_current_date, v_sysdate, v_systimestamp, v_current_timestamp 
    FROM DUAL;
    
    DBMS_OUTPUT.PUT_LINE('----------------------------');
    
    DBMS_OUTPUT.PUT_LINE('DUAL.CURRENT_DATE: ' || v_current_date);
    DBMS_OUTPUT.PUT_LINE('DUAL.SYSDATE: ' || v_sysdate);
    DBMS_OUTPUT.PUT_LINE('DUAL.SYSTIMESTAMP: ' || v_systimestamp);
    DBMS_OUTPUT.PUT_LINE('DUAL.CURRENT_TIMESTAMP: ' || v_current_timestamp);
END;
/

-- <><><><><><><><><><*>--<

-- ADD_MONTHS --
ALTER SESSION SET NLS_DATE_FORMAT = 'YYYY.MON.DD';
DECLARE
    v_orig_hire_date employees.hire_date%TYPE;
    v_hire_date_plus_month employees.hire_date%TYPE;
    
    CURSOR c_hire_date_cursor IS
    SELECT hire_date, ADD_MONTHS(hire_date, 1)
    FROM employees;
BEGIN
    DBMS_OUTPUT.PUT_LINE('-------ADD_MONTHS----------');
    IF NOT c_hire_date_cursor%ISOPEN THEN
        OPEN c_hire_date_cursor;
    END IF;
    
    LOOP
        FETCH c_hire_date_cursor INTO v_orig_hire_date, v_hire_date_plus_month;
        EXIT WHEN c_hire_date_cursor%NOTFOUND;
        DBMS_OUTPUT.PUT_LINE('Original Hire Date: ' || v_orig_hire_date || ' :: Next Month: ' || v_hire_date_plus_month);
    END LOOP;
    CLOSE c_hire_date_cursor;
END;
/

--SELECT * FROM employees;

-- <><><><><><><><><><*>--<

-- EXTRACT --
SELECT 
    order_id, order_date,
    EXTRACT(YEAR FROM order_date) "Order Year",
    EXTRACT(MONTH FROM order_date) "Order Month",
    EXTRACT(DAY FROM order_date) "Order Day",
    EXTRACT(HOUR FROM order_date) "Order Hour",
    EXTRACT(MINUTE FROM order_date) "Order Minute",
    EXTRACT(SECOND FROM order_date) "Order Second",
    EXTRACT(TIMEZONE_ABBR FROM order_date) "Order TMZ ABBR",
    EXTRACT(TIMEZONE_REGION FROM order_date) "Order TMZ Region"
FROM orders;

-- <><><><><><><><><><*>--<

-- LAST_DAY --
ALTER SESSION SET NLS_DATE_FORMAT = 'YYYY.MON.DD';
SELECT LAST_DAY(order_date) "Last Day of Order Date" FROM orders;

-- <><><><><><><><><><*>--<

-- MONTHS_BETWEEN --
SELECT order_id, order_date, MONTHS_BETWEEN(SYSDATE, order_date) FROM orders;

-- <><><><><><><><><><*>--<

-- NEW_TIME --
ALTER SESSION SET NLS_DATE_FORMAT = 'DD-MON-YYYY HH24:MI:SS';
SELECT order_id, order_date, NEW_TIME(order_date, 'EST', 'CST') FROM orders;

-- <><><><><><><><><><*>--<

-- NEXT_DAY --
SELECT order_id, order_date, NEXT_DAY(order_date, 'Monday') FROM orders;

-- <><><><><><><><><><*>--<

-- NUMTODSINTERVAL --
ALTER SESSION SET NLS_DATE_FORMAT = 'DD-MON-YY';
SELECT sales_rep_id, TO_DATE(CAST(order_date AS DATE), 'DD-MON-YY'), count(*) 
    OVER (PARTITION BY sales_rep_id ORDER BY TO_DATE(CAST(order_date AS DATE), 'DD-MON-YY')
    RANGE NUMTODSINTERVAL(100, 'DAY') PRECEDING) AS sales_count
FROM orders ORDER BY sales_rep_id

SELECT sales_rep_id, COUNT(sales_rep_id) "Order Count" FROM orders
GROUP BY sales_rep_id
ORDER BY "Order Count";

-- <><><><><><><><><><*>--<

-- NUMTOYMINTERVAL --
ALTER SESSION SET NLS_DATE_FORMAT = 'DD-MON-YY';
SELECT sales_rep_id, TO_DATE(CAST(order_date AS DATE), 'DD-MON-YY') Order_Date, sum(order_total) 
    OVER (ORDER BY TO_DATE(CAST(order_date AS DATE), 'DD-MON-YY')
    RANGE NUMTOYMINTERVAL(1, 'YEAR') PRECEDING) AS Total_Sales_Amnt
FROM orders ORDER BY sales_rep_id, Order_Date

-- <><><><><><><><><><*>--<

-- TO_DATE (using CAST) --
ALTER SESSION SET NLS_DATE_FORMAT = 'DD-MON-YYYY';
SELECT sales_rep_id, TO_DATE(CAST(order_date AS DATE), 'DD-MON-YYYY') FROM orders
ORDER BY sales_rep_id;

-- <><><><><><><><><><*>--<

-- ROUND --
ALTER SESSION SET NLS_DATE_FORMAT = 'DD-MON-YYYY';
SELECT ROUND(TO_DATE('17-JAN-2019'), 'MONTH') "Round Up Month" FROM dual;
SELECT ROUND(TO_DATE('14-JAN-2019'), 'MONTH') "Round Down Month" FROM dual;
SELECT ROUND(TO_DATE('17-OCT-2001'), 'YEAR') "Round Up Year" FROM dual;
SELECT ROUND(TO_DATE('17-MAR-2001'), 'YEAR') "Round Down Year" FROM dual;

--SELECT * FROM orders;
--DESCRIBE orders;