/*******************
Author:       Matt Morris / Duane Billue
Date:         2020-01-09
Description:  Exam Study Guide WITH Clause
*******************/

WITH 
  dept_costs AS 
    (SELECT department_name, SUM(salary) dept_total
    FROM hr.employees e INNER JOIN hr.departments d ON e.department_id = d.department_id 
    GROUP BY department_name),
  avg_cost AS
    (SELECT SUM(dept_total)/COUNT(*) avrg
    FROM dept_costs)
SELECT * FROM dept_costs
WHERE dept_total > (SELECT avrg FROM avg_cost)
ORDER BY department_name;
  
    