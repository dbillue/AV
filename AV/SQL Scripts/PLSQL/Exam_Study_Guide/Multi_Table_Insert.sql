/*******************
Author:       Matt Morris / Duane Billue
Date:         2020-01-06
Description:  Exam Study Guide Multi Table Insert
*******************/

SELECT * FROM employees ORDER BY emp_id;

-- INSERT FIRST
INSERT FIRST
  WHEN MONTHS_BETWEEN(SYSDATE, start_date) >= 306 AND MONTHS_BETWEEN(SYSDATE, start_date) <= 335 THEN
    INTO emps_25
    VALUES (emp_id, emp_first, emp_last, start_date)
  WHEN MONTHS_BETWEEN(SYSDATE, start_date) >= 247 AND MONTHS_BETWEEN(SYSDATE, start_date) <= 305 THEN
    INTO emps_20
    VALUES (emp_id, emp_first, emp_last, start_date)
  WHEN MONTHS_BETWEEN(SYSDATE, start_date) >= 185 AND MONTHS_BETWEEN(SYSDATE, start_date) <= 246 THEN
    INTO emps_15
    VALUES (emp_id, emp_first, emp_last, start_date)
  WHEN MONTHS_BETWEEN(SYSDATE, start_date) <= 1 THEN
    INTO emps_1
    VALUES (emp_id, emp_first, emp_last, start_date)
  WHEN start_date IS NULL THEN
    INTO emps_unknown
    VALUES (emp_id, emp_first, emp_last, start_date)
SELECT emp_id, emp_first, emp_last, start_date FROM employees;

SELECT * FROM emps_25 ORDER BY emp_id;
SELECT * FROM emps_20 ORDER BY emp_id;
SELECT * FROM emps_15 ORDER BY emp_id;
SELECT * FROM emps_1 ORDER BY emp_id;
SELECT * FROM emps_unknown ORDER BY emp_id;

TRUNCATE TABLE emps_25;
TRUNCATE TABLE emps_20;
TRUNCATE TABLE emps_15;
TRUNCATE TABLE emps_1;
TRUNCATE TABLE emps_unknown;

SELECT SYSDATE FROM dual;

SELECT emp_first || ' ' || emp_last AS "Name", 
      TRUNC(MONTHS_BETWEEN(SYSDATE, start_date), 0) AS "Months_Employed",
      TRUNC(MONTHS_BETWEEN(SYSDATE, start_date)/12, 0) AS "Years_Employed"
FROM employees ORDER BY "Months_Employed";

/*
CREATE TABLE EMPS_1
(
  EMP_ID NUMBER,
  EMP_FIRST VARCHAR2(50),
  EMP_LAST VARCHAR2(50),
  START_DATE DATE
);

CREATE TABLE EMPS_15
(
  EMP_ID NUMBER,
  EMP_FIRST VARCHAR2(50),
  EMP_LAST VARCHAR2(50),
  START_DATE DATE
);

CREATE TABLE EMPS_20
(
  EMP_ID NUMBER,
  EMP_FIRST VARCHAR2(50),
  EMP_LAST VARCHAR2(50),
  START_DATE DATE
);

CREATE TABLE EMPS_25
(
  EMP_ID NUMBER,
  EMP_FIRST VARCHAR2(50),
  EMP_LAST VARCHAR2(50),
  START_DATE DATE
);

CREATE TABLE EMPS_UNKNOWN
(
  EMP_ID NUMBER,
  EMP_FIRST VARCHAR2(50),
  EMP_LAST VARCHAR2(50),
  START_DATE DATE
);

DROP TABLE EMPS_1;
DROP TABLE EMPS_15;
DROP TABLE EMPS_20;
DROP TABLE EMPS_25;
DROP TABLE EMPS_UNKNOWN;
*/

--============================--
--============================--
-- Unconditional Insert All

SELECT * FROM hr.employees;
DESC hr.employees;

INSERT ALL
  INTO emp_jobs (employee_id, job_id)
  VALUES (employee_id, job_id)
  INTO emp_email (employee_id, email)
  VALUES (employee_id, email)
SELECT employee_id, job_id, email FROM hr.employees;
  
SELECT * FROM emp_jobs;
SELECT * FROM emp_email;

SELECT t1.employee_id, t1.job_id, t2.email
FROM emp_jobs t1 INNER JOIN emp_email t2 ON t1.employee_id = t2.employee_id
ORDER BY t1.employee_id;

TRUNCATE TABLE emp_email;
TRUNCATE TABLE emp_jobs;

/*
CREATE TABLE emp_jobs
(
  employee_id NUMBER(6,0),
  job_id      VARCHAR2(10)
)

DROP TABLE emp_jobs;
*/


--============================--
--============================--
-- Conditional Insert All

SELECT * FROM employees;

INSERT ALL
  WHEN MOD(rownum, 2) = 1 THEN
  INTO emp_shirts (emp_id, emp_first, emp_last)
  VALUES (emp_id, emp_first, emp_last)
  WHEN MOD(rownum, 2) = 0 THEN
  INTO emp_skins (emp_id, emp_first, emp_last)
  VALUES (emp_id, emp_first, emp_last)
SELECT emp_id, emp_first, emp_last FROM employees ORDER BY emp_id;

SELECT * FROM emp_shirts;
SELECT * FROM emp_skins;

TRUNCATE TABLE emp_shirts;
TRUNCATE TABLE emp_skins;

--============================--
--============================--
-- Pivot Insert

SELECT * FROM sales_by_fy;

INSERT ALL
  INTO sales_by_quarter VALUES (fiscal_year, 1, q1_sales)
  INTO sales_by_quarter VALUES (fiscal_year, 2, q2_sales)
  INTO sales_by_quarter VALUES (fiscal_year, 3, q3_sales)
  INTO sales_by_quarter VALUES (fiscal_year, 4, q4_sales)
SELECT fiscal_year, q1_sales, q2_sales, q3_sales, q4_sales FROM sales_by_fy;

SELECT * FROM sales_by_quarter ORDER BY fiscal_year, quarter;

TRUNCATE TABLE sales_by_quarter;