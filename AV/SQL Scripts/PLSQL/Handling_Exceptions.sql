/*****************************
Author:       Duane Billue
Date:         2019-10-11
Description:  Handling Exceptions
*****************************/
-- Environment Variables
-- SET SERVEROUTPUT ON;
-- SET AUTOPRINT ON;

--DESC employees;

--========================================--
-- Basic Exception
DECLARE
  v_last_name employees.last_name%TYPE := 'Smith';
  v_emp_no employees.employee_id%TYPE;
BEGIN
  SELECT employee_id INTO v_emp_no FROM employees WHERE last_name = v_last_name;
  EXCEPTION
    WHEN TOO_MANY_ROWS THEN
    DBMS_OUTPUT.PUT_LINE('To many rows returned...use a cursor.');
END;
/

--========================================--
-- Internally defined exception trapping --
DECLARE
  e_insert_excep EXCEPTION;
  PRAGMA EXCEPTION_INIT(e_insert_excep, -01400);
BEGIN
  INSERT INTO departments (department_id, department_name) VALUES (412, NULL);
  EXCEPTION
    WHEN e_insert_excep THEN
    DBMS_OUTPUT.PUT_LINE('Insert failed.');
END;
/

--========================================--
-- Trapping predefined exceptions --
drop table errors;
drop table cards;

create table cards
(
    card_name varchar2(50) CONSTRAINT card_name_nn_demo NOT NULL
);

create table errors
(
    e_user varchar2(250) CONSTRAINT e_user_nn_demo NOT NULL,
    e_date timestamp,
    error_code varchar2(50),
    error_message varchar2(1000)
);

-- INSERT INTO cards (card_name) VALUES ('Nine of Diamonds');

DECLARE
    err_code NUMBER;
    err_msg VARCHAR2(1000);
BEGIN
    INSERT INTO cards (card_name) VALUES ('Two of Hearts');
    INSERT INTO cards (card_name) VALUES (null);
    EXCEPTION
        WHEN ACCESS_INTO_NULL THEN
            DBMS_OUTPUT.PUT_LINE('Error inserting record due to NULL value');
        WHEN OTHERS THEN
            ROLLBACK;
            err_code := SQLCODE;
            err_msg := SQLERRM;
            INSERT INTO errors (e_user, e_date, error_code, error_message) VALUES (USER, SYSDATE, err_code, err_msg);
END;
/

SELECT * FROM cards;
SELECT * FROM errors;

--========================================--
-- Trapping user defined exceptions --
DECLARE
    v_name VARCHAR2(20) := 'Three of Clubs';
    e_invalid_card EXCEPTION;
BEGIN
    UPDATE cards SET
        card_name = v_name
    WHERE card_name = v_name;
    IF SQL%NOTFOUND THEN
        RAISE e_invalid_card;
    END IF;
    EXCEPTION
    WHEN e_invalid_card THEN
        INSERT INTO errors (e_user, e_date, error_code, error_message) VALUES (USER, SYSDATE, -20420, 'Invalid card name');
    WHEN OTHERS THEN
        NULL;
END;
/

SELECT * FROM cards;
SELECT * FROM errors;

--========================================--
-- Propagating exceptions in a Sub-Block --
DECLARE
    err_code NUMBER;
    err_msg VARCHAR2(1000);
    v_new_card_name cards.card_name%TYPE := 'Dia-';
    e_no_rows EXCEPTION;
    v_card_name cards.card_name%TYPE;
    PRAGMA EXCEPTION_INIT(e_no_rows, -2292);
    
    CURSOR c_emp_cursor IS
    SELECT card_name FROM cards;
BEGIN
    BEGIN
        IF NOT c_emp_cursor%ISOPEN THEN
            OPEN c_emp_cursor;
        END IF;
        
        LOOP
            FETCH c_emp_cursor INTO v_card_name;
            EXIT WHEN c_emp_cursor%NOTFOUND;
            
            v_new_card_name := v_new_card_name || v_card_name;
            
            BEGIN
                UPDATE cards SET
                   card_name = v_new_card_name
                WHERE card_name = v_card_name || 'GD';
                
                IF SQL%NOTFOUND THEN
                    RAISE e_no_rows;
                END IF;
            END;
        END LOOP;
        
        EXCEPTION
            WHEN e_no_rows THEN
                INSERT INTO errors (e_user, e_date, error_code, error_message) VALUES (USER, SYSDATE, -20421, 'Invalid card name...sub block to parent block');
            WHEN OTHERS THEN
                err_code := SQLCODE;
                err_msg := SQLERRM;
                INSERT INTO errors (e_user, e_date, error_code, error_message) VALUES (USER, SYSDATE, err_code, err_msg);
    END;
    
    INSERT INTO cards (card_name) VALUES ('Two of Diamonds');
END;
/

SELECT * FROM cards;
SELECT * FROM errors ORDER BY e_date;
DELETE FROM errors WHERE error_code in (-20420, -6502, -1722);
DELETE FROM cards WHERE card_name IN ('Ten of Diamonds');

--========================================--
-- Raise_Application_Error --
DECLARE
    err_code NUMBER;
    err_msg VARCHAR2(1000);
    v_card_name_incorrect VARCHAR2(100) := 'GD';
    e_update_err EXCEPTION;
BEGIN
    UPDATE cards SET
        card_name = 'King of Clubs'
    WHERE card_name = v_card_name_incorrect;
    IF SQL%NOTFOUND THEN
        RAISE_APPLICATION_ERROR(-20422, 'Update record error');
    END IF;
    
    EXCEPTION
        WHEN OTHERS THEN
            err_code := SQLCODE;
            err_msg := SQLERRM; 
            INSERT INTO errors (e_user, e_date, error_code, error_message) VALUES (USER, SYSDATE, err_code, err_msg);    
END;
/

SELECT * FROM cards;
SELECT * FROM errors ORDER BY e_date;
DELETE FROM errors WHERE error_code IN (-20422);
