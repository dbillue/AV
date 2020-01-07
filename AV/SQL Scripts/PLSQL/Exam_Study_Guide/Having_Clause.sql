/*******************
Author:       Matt Morris
Date:         2020-01-01
Description:  Exam Study Guide Having Clause Section
*******************/

SELECT * FROM employees ORDER BY emp_job, emp_last;

-- All filters in HAVING clause must be present in SELECT clause.
SELECT emp_job, MAX(salary)
FROM employees
GROUP BY emp_job
HAVING MAX(salary) > 111500 AND emp_job != 'CEO'
ORDER BY emp_job;

-- Same statement but will error due to missing filter in SELECT clause.
SELECT emp_job, MAX(salary)
FROM employees
GROUP BY emp_job
HAVING MAX(salary) > 111500 AND emp_last != 'Boss'
ORDER BY emp_job;

-- Same statement as above but using WHERE clause.
SELECT emp_job, MAX(salary)
FROM employees
WHERE emp_job != 'CEO'
GROUP BY emp_job
HAVING MAX(salary) > 111500
ORDER BY emp_job;
