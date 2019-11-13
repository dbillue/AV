/*****************************
Author:       Duane Billue
Date:         2019-11-11
Description:  SAVE_EXCEPTIONS statement
*****************************/

SET SERVEROUTPUT ON;
SET AUTOPRINT ON;
SET TIMING ON;

/*
DECLARE
  TYPE NumList IS TABLE OF NUMBER;
  num_tab NumList := NumList(100,0,110,300,199,200,0,400);
  bulk_errors EXCEPTION;
  PRAGMA EXCEPTION_INIT (bulk_errors, -24381);
BEGIN
  FORALL i IN num_tab.FIRST .. num_tab.LAST
  SAVE EXCEPTIONS
  DELETE FROM orders WHERE order_total < 500000/num_tab(i);
EXCEPTION WHEN bulk_errors THEN
  DBMS_OUTPUT.PUT_LINE('Number of errors is: ' || SQL%BULK_EXCEPTIONS.COUNT);
  FOR j IN 1..SQL%BULK_EXCEPTIONS.COUNT
  LOOP
    DBMS_OUTPUT.PUT_LINE(TO_CHAR(SQL%BULK_EXCEPTIONS(j).error_index || 
                          ' / ' || SQLERRM(-SQL%BULK_EXCEPTIONS(j).error_code)));
  END LOOP;
END;

CREATE TABLE card_table
(accepted_cards VARCHAR2(50) NOT NULL);
*/

DECLARE 
  TYPE typ_cards IS TABLE OF VARCHAR2(50);
  v_cards typ_cards := typ_cards('Citigroup Visa', 
                                'Citizens Visa', 
                                'International Discoverer', 
                                'United Diners Club');
  bulk_errors EXCEPTION;
  PRAGMA EXCEPTION_INIT (bulk_errors, -24381);
BEGIN
  v_cards.DELETE(3);
  v_cards.DELETE(6);
  FORALL j IN v_cards.FIRST..v_cards.LAST
  SAVE EXCEPTIONS
  EXECUTE IMMEDIATE 'INSERT INTO card_table (accepted_cards) VALUES (:the_card)'
  USING v_cards(j);
  
  EXCEPTION WHEN bulk_errors THEN
  DBMS_OUTPUT.PUT_LINE('Number of errors is: ' || SQL%BULK_EXCEPTIONS.COUNT);
  FOR j IN 1..SQL%BULK_EXCEPTIONS.COUNT
  LOOP
    DBMS_OUTPUT.PUT_LINE(TO_CHAR(SQL%BULK_EXCEPTIONS(j).error_index || 
                          ' / ' || SQLERRM(-SQL%BULK_EXCEPTIONS(j).error_code)));
  END LOOP;
END;
/
