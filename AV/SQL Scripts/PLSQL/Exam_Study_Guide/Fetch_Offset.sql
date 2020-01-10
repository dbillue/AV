/*******************
Author:       Matt Morris / Duane Billue
Date:         2020-01-07
Description:  Exam Study Guide FETCH, OFFSET and WITH TIES statement
*******************/

SELECT * FROM aircraft_fleet_v ORDER BY apt_name, act_name;
DESC aircraft_fleet_v;

-- Use Rownum but unreliable.
SELECT apt_name, apt_abbr, act_name
FROM aircraft_fleet_v
WHERE ROWNUM < 6
ORDER BY apt_name, act_name;

-- FETCH clause.
SELECT apt_name, apt_abbr, act_name
FROM aircraft_fleet_v
ORDER BY apt_name, act_name
FETCH FIRST 5 ROWS ONLY;

-- OFFSET clause.
SELECT apt_name, apt_abbr, act_name
FROM aircraft_fleet_v
ORDER BY apt_name, act_name
OFFSET 5 ROWS FETCH NEXT 5 ROWS ONLY;

-- FETCH / WITH TIES :: Returns records with same values in ORDER BY clause.
SELECT apt_name, apt_abbr, act_name
FROM aircraft_fleet_v
ORDER BY apt_name, act_name
FETCH FIRST 5 ROWS WITH TIES;

-- FETCH / PERCENT
SELECT apt_name, apt_abbr, act_name
FROM aircraft_fleet_v
ORDER BY apt_name, act_name
FETCH FIRST 75 PERCENT ROWS ONLY;
