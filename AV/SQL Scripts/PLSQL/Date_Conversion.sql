/*****************************
Author:       Duane Billue
Date:         2019-10-15
Description:  Date and Time Conversion
*****************************/

--SET SERVEROUTPUT ON;
--SET AUTOPRINT ON;
--CREATE TABLE employees AS SELECT * FROM hr.employees;
--CREATE TABLE orders AS SELECT * FROM oe.orders;

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

--SELECT * FROM V$TIMEZONE_NAMES;
--SELECT TZ_OFFSET('US/Eastern'), TZ_OFFSET('US/CENTRAL'), TZ_OFFSET('US/MOUNTAIN'), TZ_OFFSET('US/PACIFIC') FROM DUAL;
--SELECT DBTIMEZONE FROM DUAL;

-- <><><><><><><><><><*>--<

-- ADD_MONTHS --
ALTER SESSION SET NLS_DATE_FORMAT = 'YYYY.MON.DD HH:MI:SS';
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

--SELECT * FROM orders;
--DESCRIBE orders;