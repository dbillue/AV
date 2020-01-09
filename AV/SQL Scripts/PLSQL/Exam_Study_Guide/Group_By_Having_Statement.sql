/*******************
Author:       Matt Morris / Duane Billue
Date:         2020-01-09
Description:  Exam Study Guide GROUP BY and HAVING Clause
*******************/

-- Simple Aggregate w/ Group By.
SELECT emp_job, MAX(salary)
FROM employees
GROUP BY emp_job;

-- Add two more row groups (apt_name, act_name).
SELECT apt_name, act_name, SUM(act_seats)
FROM AIRCRAFT_FLEET_V
GROUP BY apt_name, act_name
ORDER BY apt_name, act_name;

-- Maximum of two aggragate functions allowed.
SELECT MAX(AVG(salary))
FROM employees
GROUP BY EMP_JOB;

-- Three aggregate functions, returns an error.
SELECT COUNT(MAX(AVG(salary)))
FROM employees
GROUP BY EMP_JOB;

-- Use HAVING clause to filter grouped record sets.
SELECT emp_job, MAX(Salary)
FROM employees
GROUP BY emp_job
HAVING MAX(salary) >= 140000 and emp_job != 'CEO'
ORDER BY emp_job;

-- Use WHERE clause to filter records before grouping.
SELECT emp_job, MAX(salary)
FROM employees
WHERE emp_last != 'Stoner'
GROUP BY emp_job
HAVING MAX(salary) > 11000
ORDER BY emp_job;



