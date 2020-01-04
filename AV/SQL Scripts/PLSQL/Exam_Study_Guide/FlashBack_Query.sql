/*******************
Author:       Matt Morris / Duane Billue
Date:         2020-01-04
Description:  Exam Study Guide Flash Back Query
*******************/

SELECT * FROM employees ORDER BY emp_id;

-- Insert rec.
INSERT INTO employees (emp_id, afl_id, emp_first, emp_last, emp_job, emp_supervisor, salary, start_date) 
VALUES (19, 0, 'Duane', 'Billue', 'Worker Bee', 9, 10000, '04-JAN-20');

SELECT * FROM employees WHERE emp_id IN (19);

-- Update
UPDATE employees SET afl_id = NULL WHERE emp_id IN (19);

-- Delete
DELETE FROM employees WHERE emp_id IN (19);

COMMIT;

-- Flashback query (regular)
SELECT *
FROM employees
AS OF TIMESTAMP TO_TIMESTAMP('04-JAN-20 04.25.00 PM', 'DD-MON-YY HH:MI:SS AM')
WHERE 1 = 1
  AND emp_id = 19
ORDER BY emp_id;

-- Flashback query insert (regular)
INSERT INTO employees
(SELECT * FROM employees 
AS OF TIMESTAMP TO_TIMESTAMP('04-JAN-20 03.25.00 PM', 'DD-MON-YY HH:MI:SS AM')
WHERE 1 = 1
  AND emp_id = 19);

  
-- =========================== --
-- =========================== --
-- Flashback Query (Version)

SELECT versions_starttime, versions_endtime, versions_xid, versions_operation AS OP
FROM employees
VERSIONS BETWEEN TIMESTAMP
  TO_TIMESTAMP('04-JAN-20 04.20.00 PM', 'DD-MON-YY HH:MI:SS AM')
  AND 
  TO_TIMESTAMP('04-JAN-20 04.30.00 PM', 'DD-MON-YY HH:MI:SS AM')
WHERE 1 = 1
  AND emp_id = 19
ORDER BY versions_xid, versions_starttime;


-- =========================== --
-- =========================== --
-- Flashback Query (Transaction)
-- ! Does not work due to container PDB database architecture ! --

SELECT * FROM flashback_transaction_query 
WHERE xid = HEXTORAW('0A000500B80A0000');
