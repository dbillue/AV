/*****************************
Author:       Duane Billue
Date:         2019-10-22
Description:  Joins
*****************************/

SET SERVEROUTPUT ON;
ALTER SESSION SET TIME_ZONE = '-04:00';
ALTER SESSION SET NLS_DATE_FORMAT = 'DD-MON-YYYY';

-- NATURAL JOIN --
SELECT first_name, last_name, department_name
FROM employees NATURAL JOIN departments;
-- uses department_id from both tables

-- EQUIJOINS --
SELECT e.first_name, e.last_name, d.department_name
FROM employees e JOIN departments d 
	ON e.department_id = d.department_id;
	
-- NONEQUIJOINS --
SELECT e.employee_id, e.first_name, e.last_name
FROM employees e
WHERE 1 = 1
	AND e.employee_id BETWEEN 100 AND 125;
	
-- USING --
SELECT e.first_name, e.last_name, d.department_name
FROM employees e JOIN departments d 
USING (department_id);

-- LEFT OUTER --
SELECT e.first_name, e.last_name, d.department_name
FROM employees e LEFT OUTER JOIN departments d 
ON e.department_id = d.department_id;
-- returns 107...one employee table record has null for department_id

-- RIGHT OUTER --
SELECT d.department_id, d.department_name, l.location_id, l.city
FROM departments d RIGHT OUTER JOIN locations l	
	ON (d.location_id = l.location_id);
	
-- FULL OUTER JOIN --
SELECT e.last_name, d.department_id, d.manager_id, d.department_name
FROM employees e FULL OUTER JOIN departments d
	ON (e.manager_id = d.manager_id);
	
-- SELF JOIN --
SELECT worker.last_name || ' works for ' || manager.last_name
FROM employees worker JOIN employees manager
	ON (worker.manager_id = manager.employee_id);
	
-- CROSS JOIN --
SELECT department_name, city
FROM departments CROSS JOIN locations;