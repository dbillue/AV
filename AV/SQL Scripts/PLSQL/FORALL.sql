/*****************************
Author:       Duane Billue
Date:         2019-11-11
Description:  FORALL
*****************************/

SET TIMING ON

/*
CREATE TABLE bulk_bind_example_tbl
(
  num_col NUMBER,
  date_col DATE,
  char_col VARCHAR2(40)
);
*/

DECLARE
  TYPE typ_numlist IS TABLE OF NUMBER;
  TYPE typ_datelist IS TABLE OF DATE;
  TYPE typ_charlist IS TABLE OF VARCHAR2(40) INDEX BY PLS_INTEGER;
  n typ_numlist := typ_numlist();
  d typ_datelist := typ_datelist();
  c typ_charlist;
BEGIN
  FOR i IN 1 .. 50000 LOOP
    n.extend;
    n(i) := i;
    d.extend;
    d(i) := SYSDATE + 1;
    c(i) := LPAD(1, 40);
  END LOOP;
  /*
  FOR I in 1 .. 50000 LOOP
    INSERT INTO bulk_bind_example_tbl
    VALUES (n(i), d(i), c(i));
  END LOOP;
  */
  FORALL I in 1 .. 50000
    INSERT INTO bulk_bind_example_tbl
    VALUES (n(i), d(i), c(i));
END;

-- TRUNCATE TABLE bulk_bind_example_tbl
