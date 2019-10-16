/*****************************
Author:       Duane Billue
Date:         2019-10-16
Description:  Character Functions
*****************************/
-- Environment Variables
--SET SERVEROUTPUT ON
--SET AUTOPRINT ON

--CREATE TABLE orders AS SELECT * FROM oe.orders;
--CREATE TABLE employees AS SELECT * FROM hr.employees;

-- CHR --
SELECT CHR(68) || CHR(117) || CHR(97) || CHR(110) || CHR(101) "Name" FROM dual;

-- <><><><><><><><><><*>--<

-- CONCAT --
SELECT CONCAT(first_name, ' ' || last_name) "Full Name" FROM employees ORDER BY last_name;

-- <><><><><><><><><><*>--<

-- INITCAP --
DECLARE
    v_city_name VARCHAR2(250);
    v_city_name_2 VARCHAR2(250);
BEGIN
    v_city_name := 'atlanta';
    SELECT INITCAP(v_city_name) INTO v_city_name_2 FROM dual;
    DBMS_OUTPUT.PUT_LINE(v_city_name_2);
END;
/
 
-- <><><><><><><><><><*>--<

-- LOWER --
SELECT LOWER(last_name) FROM employees;

-- <><><><><><><><><><*>--<

-- UPPER --
SELECT UPPER(first_name) FROM employees;

-- <><><><><><><><><><*>--<

-- LPAD --
SELECT LPAD('<*>--<', 30, '<><>') FROM dual;

-- <><><><><><><><><><*>--<

-- LTRIM --
SELECT LTRIM('    Spaces Removed', ' ') FROM dual;
SELECT LTRIM('<><><><><><><*>--<', '<>') FROM dual;

-- <><><><><><><><><><*>--<

-- REPLACE --
SELECT REPLACE('ABC123', '123', 'DEF') FROM dual;

-- <><><><><><><><><><*>--<

-- RPAD --
SELECT RPAD('<><><><><><><><><><><><>', 30, '<*>--<') FROM dual;

-- <><><><><><><><><><*>--<

-- RTRIM --
SELECT RTRIM('Spaces Removed     ', ' ') FROM dual;
SELECT RTRIM('~--<*><><><><><><><>', '<->') FROM dual;

-- <><><><><><><><><><*>--<

-- SUBSTR --
SELECT SUBSTR('>--<*><><><><><><><>', 1, 12) FROM dual; 
SELECT SUBSTR('<><><><><><><><*>--<', -12, 12) FROM dual;

-- <><><><><><><><><><*>--<

-- TRANSLATE --
SELECT TRANSLATE('1058 Gilbert St', ' ', '-') FROM dual;

-- <><><><><><><><><><*>--<

-- TRIM --
SELECT TRIM(LEADING '*' FROM '**ORACLE**') FROM dual;
SELECT TRIM(TRAILING '*' FROM '**ORACLE**') FROM dual;
SELECT TRIM(BOTH '*' FROM '**ORACLE**') FROM dual;

-- <><><><><><><><><><*>--<

-- LENGTH --
SELECT 
    CONCAT(first_name, ' ' || last_name) "Full Name", 
    LENGTH(CONCAT(first_name, ' ' || last_name)) "Name Length" 
FROM employees ORDER BY last_name;

-- <><><><><><><><><><*>--<

-- INSTR --
SELECT INSTR('Coca Cola', 'Co', 1, 1) FROM dual;
SELECT INSTR('Coca Cola', 'Co', -1, 1) FROM dual;

SELECT * FROM orders;