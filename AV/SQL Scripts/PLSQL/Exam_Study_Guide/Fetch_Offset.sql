/*******************
Author:       Matt Morris / Duane Billue
Date:         2020-01-07
Description:  Exam Study Guide FETCH, OFFSET and WITH TIES statement
*******************/

-- Use Rownum but unreliable.
SELECT apt_name, apt_abbr, act_name
FROM aircraft_fleet_v
WHERE ROWNUM < 6
ORDER BY apt_name, act_name;

-- Fetch clause.
SELECT apt_name, apt_abbr, act_name
FROM aircraft_fleet_v
ORDER BY apt_name, act_name
FETCH FIRST 5 ROWS ONLY;

-- Offset clause.
SELECT apt_name, apt_abbr, act_name
FROM aircraft_fleet_v
ORDER BY apt_name, act_name
OFFSET 5 ROWS FETCH NEXT 5 ROWS ONLY;

-- FETCH / WITH TIES
SELECT apt_name, apt_abbr, act_name
FROM aircraft_fleet_v
ORDER BY apt_name, act_name
FETCH FIRST 5 ROWS WITH TIES;

-- FETCH / PERCENT
SELECT apt_name, apt_abbr, act_name
FROM aircraft_fleet_v
ORDER BY apt_name, act_name
FETCH FIRST 50 PERCENT ROWS ONLY;

