/*******************
Author:       Matt Morris
Date:         2020-01-01
Description:  Exam Study Guide
*******************/

SELECT emp_job, MAX(Salary)
FROM employees
GROUP BY emp_job
HAVING MAX(salary) > 111500 AND emp_job != 'CEO'
ORDER BY emp_job;