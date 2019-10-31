/*****************************
Author:       Duane Billue
Date:         2019-10-08
Description:  Composite Data Types
*****************************/
-- Environment Variables
SET SERVEROUTPUT ON
SET AUTOPRINT ON

--========================================--
-- Record
DECLARE
  v_countries_record countries%ROWTYPE;
  v_countryid VARCHAR2(2) := 'CA';
BEGIN <<CompositeRecordType>>
  SELECT * INTO v_countries_record FROM countries WHERE country_id = v_countryid;
  DBMS_OUTPUT.PUT_LINE('Country : ' || v_countries_record.country_id  ||
                  ' Name: ' || v_countries_record.country_name || 
                  ' Region: ' || v_countries_record.region_id);
END;
/

--========================================--
-- Associative Array
DECLARE
  TYPE dept_table_type IS TABLE OF departments.department_name%TYPE INDEX BY PLS_INTEGER;
  my_dept_table dept_table_type;
  f_loop_count NUMBER := 10;
  v_deptno NUMBER := 0;
BEGIN <<AssocArray>>
  FOR i IN 1..f_loop_count
  LOOP
    v_deptno := v_deptno + 10;
    --DBMS_OUTPUT.PUT_LINE(v_deptno);
    SELECT department_name INTO my_dept_table(i)
    FROM departments WHERE department_id = v_deptno;
  END LOOP;
  
  FOR i IN 1..f_loop_count
  LOOP
    DBMS_OUTPUT.PUT_LINE(my_dept_table(i));
  END LOOP;
END;

--========================================--
-- Associative Array
DECLARE
  TYPE dept_table IS TABLE OF departments%ROWTYPE INDEX BY PLS_INTEGER;
  my_dept_recs dept_table;
  f_loop_count NUMBER := 10;
  v_deptno NUMBER := 0;
BEGIN <<AssocArrayRecord>>
  FOR i IN 1..f_loop_count
  LOOP
    v_deptno := v_deptno + 10;
    SELECT * INTO my_dept_recs(i) FROM departments
    WHERE department_id = v_deptno;
  END LOOP;
  
  FOR i IN 1..f_loop_count
  LOOP
    DBMS_OUTPUT.PUT_LINE('ID: ' || my_dept_recs(i).department_id ||
                        ' Name: ' || my_dept_recs(i).department_name ||
                        ' Manager: ' || my_dept_recs(i).manager_id ||
                        ' Location: ' || my_dept_recs(i).location_id);
  END LOOP;
END;
/
