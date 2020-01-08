/*******************
Author:       Matt Morris
Date:         2020-01-01
Description:  Exam Study Guide Having Clause Section
*******************/

SELECT * FROM aircraft_types;

-- LAG :: Begin row processing at specified value.
SELECT act_name, act_body_style, act_seats,
LAG(act_seats, 1) OVER (ORDER BY act_seats) 
FROM aircraft_types;

-- PERCENTILE_CONT
SELECT act_body_style, act_seats,
PERCENTILE_CONT(0.5) WITHIN GROUP (ORDER BY act_seats) OVER (PARTITION BY act_body_style) PC_BY_STYLE
FROM aircraft_types;

-- STDDEV
SELECT act_body_style, act_seats,
STDDEV(act_seats) OVER (PARTITION BY act_body_style) STDV_BY_STYLE
FROM aircraft_types;

-- Analytic AVG using sliding window
SELECT act_name, act_body_style, act_seats,
AVG(act_seats) OVER (PARTITION BY act_body_style ORDER BY act_seats) AS AVG_BY_STYLE
from aircraft_types
WHERE ACT_BODY_STYLE = 'Narrow';

-- LEAD
SELECT act_body_style, act_seats,
LEAD(act_seats, 1, 0) OVER (ORDER BY act_seats) LEAD_BY_STYLE
FROM aircraft_types;

-- LISTAGG
SELECT LISTAGG(act_name, ': ')
  WITHIN GROUP (ORDER BY act_seats, act_name) AIRCRAFT
FROM aircraft_types;