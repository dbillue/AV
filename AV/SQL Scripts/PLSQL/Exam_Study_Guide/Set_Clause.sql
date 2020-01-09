/*******************
Author:       Matt Morris / Duane Billue
Date:         2020-01-09
Description:  Exam Study Guide SET Clause
*******************/

SELECT col1 FROM table_setA;
SELECT col1 FROM table_setB;

-- UNION :: Returns distinct matching records from both tables.
SELECT col1
FROM table_setA
UNION
SELECT col1
FROM table_setB;

-- UNION ALL :: Return ALL records all records from both tables.
SELECT col1
FROM table_setA
UNION ALL
SELECT col1
FROM table_setB;

-- INTERSECT :: Return matching records from both tables.
SELECT col1
FROM table_setA
INTERSECT
SELECT col1
FROM table_setB;

-- MINUS :: Return matching records from first table / query only.
SELECT col1
FROM table_setA
MINUS
SELECT col1
FROM table_setB;