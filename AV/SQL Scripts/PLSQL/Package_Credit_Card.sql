/*****************************
Author:       Duane Billue / Oracle
Date:         2019-11-05
Description:  Package Credit Card Package w/ Nested Table
*****************************/

CREATE OR REPLACE PACKAGE credit_card_pkg IS
  FUNCTION cust_card_info
  (
    p_cust_id NUMBER, 
    p_card_info IN OUT typ_cr_card_nst 
  ) RETURN BOOLEAN;
  PROCEDURE update_card_info
  (
    p_cust_id NUMBER,
    p_card_type VARCHAR2,
    p_card_no VARCHAR2
  );
  PROCEDURE display_card_info
  (
    p_cust_id NUMBER
  );
END credit_card_pkg;
/

CREATE OR REPLACE PACKAGE BODY credit_card_pkg IS
  FUNCTION cust_card_info
  (
    p_cust_id NUMBER, 
    p_card_info IN OUT typ_cr_card_nst
  ) RETURN BOOLEAN 
  IS
    v_card_info_exists BOOLEAN;
  BEGIN
      SELECT credit_cards INTO p_card_info
      FROM customers
      WHERE customer_id = p_cust_id;
      
      IF p_card_info.EXISTS(1) THEN
        v_card_info_exists := TRUE;
      ELSE
        v_card_info_exists := FALSE;
      END IF;
      
      RETURN v_card_info_exists;
  END cust_card_info;
  
  PROCEDURE update_card_info
  (
    p_cust_id NUMBER,
    p_card_type VARCHAR2,
    p_card_no VARCHAR2
  ) IS
    v_card_info typ_cr_card_nst;
    i SIMPLE_INTEGER := 0;
  BEGIN
    IF cust_card_info(p_cust_id, v_card_info) THEN
      i := v_card_info.LAST;
      v_card_info.EXTEND(1);
      v_card_info(i+1) := typ_cr_card(p_card_type, p_card_no);
      
      UPDATE customers
        SET credit_cards = v_card_info
        WHERE customer_id = p_cust_id;
    ELSE
      UPDATE customers
        SET credit_cards = typ_cr_card_nst(
                                typ_cr_card(p_card_type, p_card_no))
      WHERE customer_id = p_cust_id;
    
    END IF;
  END update_card_info;
  
  PROCEDURE display_card_info
  (
    p_cust_id NUMBER
  ) IS
    v_card_info typ_cr_card_nst;
    i SIMPLE_INTEGER := 0;
  BEGIN
    IF cust_card_info(p_cust_id, v_card_info) THEN
        FOR idx IN v_card_info.FIRST .. v_card_info.LAST LOOP
        DBMS_OUTPUT.PUT_LINE('Card Type: ' || v_card_info(idx).card_type ||
                              ' Card No: ' || v_card_info(idx).card_num);
        END LOOP;
    ELSE
      DBMS_OUTPUT.PUT_LINE('Customer has no credit card on file');
    END IF;
  END display_card_info;
END credit_card_pkg;
/

/*****
Call statements
*****/
--EXECUTE credit_card_pkg.display_card_info(120);
--EXECUTE credit_card_pkg.update_card_info(120, 'VISA', '01010101');
--EXECUTE credit_card_pkg.update_card_info(120, 'MC', 2323322);
--SELECT t1.customer_id, t1.cust_last_name, t2.card_type, t2.card_num FROM customers t1, TABLE(t1.credit_cards) t2;