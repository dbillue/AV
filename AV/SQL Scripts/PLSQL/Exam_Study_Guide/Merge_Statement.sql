/*******************
Author:       Matt Morris / Duane Billue
Date:         2020-01-06
Description:  Exam Study Guide Merge Statement
*******************/

SELECT * FROM employees ORDER BY emp_id;

-- Merge Statement
MERGE INTO employees_bkup emp_bu
USING (SELECT * FROM employees) emp
  ON (emp_bu.emp_id = emp.emp_id)
WHEN MATCHED THEN
  UPDATE SET
    emp_bu.afl_id = emp.afl_id,
    emp_bu.emp_first = emp.emp_first,
    emp_bu.emp_last = emp.emp_last,
    emp_bu.emp_job = emp.emp_job,
    emp_bu.emp_supervisor = emp.emp_supervisor,
    emp_bu.salary = emp.salary,
    emp_bu.start_date = emp.start_date
WHEN NOT MATCHED THEN
  INSERT VALUES (emp.emp_id, emp.afl_id, emp.emp_first, emp.emp_last, emp.emp_job, emp.emp_supervisor, emp.salary, emp.start_date);
  
SELECT * FROM employees_bkup ORDER BY emp_id;