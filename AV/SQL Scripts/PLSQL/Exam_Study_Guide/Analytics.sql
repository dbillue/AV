/*******************
Author:       Matt Morris
Date:         2020-01-01
Description:  Exam Study Guide Having Clause Section
*******************/

SELECT * FROM aircraft_types;

SELECT act_name, act_body_style, act_seats,
LAG(act_seats, 1) OVER (ORDER BY act_seats) 
FROM aircraft_types;

SELECT act_name, act_body_style, act_seats,
AVG(act_seats) OVER (PARTITION BY act_body_style ORDER BY act_seats) AS AVG_BY_STYLE
from aircraft_types
WHERE ACT_BODY_STYLE = 'Narrow';