/*******************
Author:       Matt Morris / Duane Billue
Date:         2020-01-07
Description:  Exam Study Guide TRUNC and DATE statement(s)
*******************/

-- TRUNC function against DAY
SELECT TO_CHAR(SYSDATE, 'DD-MON HH24:MI:SS') AS SYS_DATE,
  TO_CHAR(TRUNC(SYSDATE, 'DD'), 'DD-MON HH24:MI:SS') AS TRUNC_DAY
FROM DUAL;

-- TRUNC function against HOUR
SELECT TO_CHAR(SYSDATE, 'DD-MON HH24:MI:SS') AS SYS_DATE,
  TO_CHAR(TRUNC(SYSDATE, 'HH'), 'DD-MON HH24:MI:SS') AS TRUNC_HOUR
FROM DUAL;

-- TRUNC function against MONTH
SELECT TO_CHAR(SYSDATE, 'DD-MON HH24:MI:SS') AS SYS_DATE,
  TO_CHAR(TRUNC(SYSDATE, 'MM'), 'DD-MON HH24:MI:SS') AS TRUNC_MONTH
FROM DUAL;

-- TRUNC function against YEAR
SELECT TO_CHAR(SYSDATE, 'DD-MON HH24:MI:SS') AS SYS_DATE,
  TO_CHAR(TRUNC(SYSDATE, 'YYYY'), 'DD-MON-YYYY HH24:MI:SS') AS TRUNC_HOUR
FROM DUAL;