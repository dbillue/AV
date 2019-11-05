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
-- Associative Array (INDEX BY PLS_INTEGER)
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
-- Associative Array (INDEX BY PLS_INTEGER)
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

--=============================================================--
-- Associative Array (INDEX BY record.column w/ Collection Method)
CREATE OR REPLACE PROCEDURE report_credit
(
  p_email         customers.cust_email%TYPE,
  p_credit_limit  customers.credit_limit%TYPE
) IS
  TYPE typ_name IS TABLE OF customers%ROWTYPE INDEX BY customers.cust_email%TYPE;
  v_by_cust_email typ_name;
  v_cust_name VARCHAR2(200);
  i VARCHAR2(50);

  -- Collection Method
  PROCEDURE load_arrays IS
  BEGIN
    FOR rec IN (SELECT * FROM customers WHERE cust_email IS NOT NULL) 
    LOOP
      v_by_cust_email(rec.cust_email) := rec;
    END LOOP;
  END;
BEGIN
  load_arrays;
  --DBMS_OUTPUT.PUT_LINE('H1: ' || v_by_cust_email(p_email).credit_limit);
  --DBMS_OUTPUT.PUT_LINE('H2: ' || v_cust_name);
  --DBMS_OUTPUT.PUT_LINE('For credit amount of: ' || p_credit_limit);
  v_cust_name := v_by_cust_email(p_email).cust_first_name || ' ' || v_by_cust_email(p_email).cust_last_name;
  IF v_by_cust_email(p_email).credit_limit > p_credit_limit THEN
    DBMS_OUTPUT.PUT('Customer ' || v_cust_name  || ' has a credit limit of ' || v_by_cust_email(p_email).credit_limit || '.');
  END IF;
END report_credit;
/

--=============================================================--
-- Nested Table
CREATE TYPE type_item AS OBJECT -- Create object
(
  prodid  NUMBER(5),
  price   NUMBER(7,2)
)
/

CREATE TYPE type_item_nst -- Define nested table type
AS TABLE of type_item
/

CREATE TABLE pOrder -- DDL statement w/ Nested table
(
  ordid     NUMBER(5),
  supplier    NUMBER(5),
  requester   NUMBER(4),
  ordered     DATE,
  items       type_item_nst)
  NESTED TABLE items STORE AS item_stor_tab
/

--=================
-- Insert statement for nested table
/*
INSERT INTO pOrder
VALUES (500, 50, 5000, sysdate, type_item_nst(
                                  type_item(55, 555),
                                  type_item(56, 566),
                                  type_item(57, 577)));
                                  
INSERT INTO pOrder
VALUES (501, 80, 8000, (sysdate + 1), type_item_nst(
                                  type_item(58, 588)));                    
*/

/*
--=================
-- Select statement for nested table
SELECT * FROM pOrder;

SELECT t1.*, t2.* FROM pOrder t1, TABLE(t1.items) t2 ORDER BY t1.ordid;

SELECT t1.ordid, t1.supplier, t1.requester, t1.ORDERED, t2.*
FROM pOrder t1, 
  TABLE(t1.items) t2 
ORDER BY t1.ordid;
*/

--=================
-- Call Procedure w/ nested object parameter
DECLARE
v_item type_item_nst := type_item_nst();
BEGIN
  v_item.EXTEND(4);
  v_item(1) := type_item(59, 599);
  v_item(2) := type_item(60, 608);
  v_item(3) := type_item(61, 619);
  v_item(4) := type_item(62, 630);
  add_other_items(501, v_item);
END;

--=============================================================--
-- VARRAY 
CREATE TYPE type_Project AS OBJECT
(
  project_no  NUMBER(4),
  title       VARCHAR2(50),
  cost        NUMBER(12,2)
)
/

CREATE TYPE type_ProjectList AS VARRAY(50) OF type_Project
/

CREATE TABLE department
(
  dept_id   NUMBER(2),
  name      VARCHAR2(50),
  budget    NUMBER(12, 2),
  projects  type_ProjectList
)
/

/*
--=================
-- Insert statement for VARRAY
INSERT INTO department
VALUES (20, 'Marketing', 200000, type_ProjectList(
                                  type_Project(1, 'New Branding Campaign', 70000),
                                  type_Project(2, 'Fall Merchadise Line', 50000)));
*/
 
/* 
--=================
-- Select statement for VARRAY
SELECT * FROM department;

SELECT t1.*, t2.* FROM department t1, TABLE(t1.projects) t2;

SELECT t1.dept_Id, t1.name, t1.budget, t2.project_no, t2.title, t2.cost 
FROM department t1, 
  TABLE(t1.projects) t2 
ORDER BY t1.dept_Id, t2.project_no;
*/
