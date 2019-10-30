/*****************************
Author:       Duane Billue
Date:         2019-10-28
Description:  D80170GC20 :: Chapter 7 Oracle Supplied Packages
*****************************/

SET SERVEROUTPUT ON
SET AUTOPRINT ON

CREATE OR REPLACE PROCEDURE employee_report
(
  p_dir IN VARCHAR2,
  p_file_name IN VARCHAR2
) IS 
f UTL_FILE.FILE_TYPE;
CURSOR cur_avg IS
  SELECT last_name, department_id, salary 
  FROM employees 
  OUTER WHERE 
    salary > (SELECT AVG(salary) FROM employees INNER WHERE department_id = outer.department_id)
    ORDER BY department_id;
BEGIN
  f := UTL_FILE.FOPEN(p_dir, p_file_name, 'W');
  UTL_FILE.PUT_LINE(f, 'REPORT GENERATED ON ' || SYSDATE);
  UTL_FILE.NEW_LINE(f);
  FOR emp IN cur_avg
  LOOP
    UTL_FILE.PUT_LINE(f, 
                      RPAD(emp.last_name, 30) || ' ' ||
                      LPAD(NVL(TO_CHAR(emp.department_id, '9999'),'-'), 5) || ' ' ||
                      LPAD(TO_CHAR(emp.salary, '$99,9999.00'), 12));
  END LOOP;
  UTL_FILE.NEW_LINE(f);
  UTL_FILE.PUT_LINE(f, '*** END OF REPORT ***');
  UTL_FILE.FCLOSE(f);
END employee_report;
/

--EXECUTE employee_report('REPORTS_DIR', 'sal_rpt71.txt');
