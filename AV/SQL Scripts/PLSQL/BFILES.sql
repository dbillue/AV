/*****************************
Author:       Duane Billue / Oracle
Date:         2019-11-06
Description:  Oracle BFILE / DBMS_LOB.*
*****************************/

SET SERVEROUTPUT ON;

---------------------------
-- Add column of BFILE type
ALTER TABLE customers ADD video BFILE;

---------------------------
-- Use BFILENAME function
UPDATE customers SET video = BFILENAME('SUBDIR','WIN_20191106_08_07_18_Pro.mp4')
WHERE customer_id IN (448);

--SELECT * FROM customers WHERE customer_id IN (448);
--UPDATE customers SET video = NULL WHERE customer_id IN (448);

--===========================
-- PROCEDURE set_video
CREATE OR REPLACE PROCEDURE set_video
(
  dir_alias VARCHAR2,
  filename VARCHAR2,
  custid NUMBER
) IS
file_ptr BFILE;
CURSOR cust_csr IS
  SELECT cust_first_name FROM customers
  WHERE customer_id = custid FOR UPDATE;
BEGIN
  FOR rec IN cust_csr LOOP
    file_ptr := BFILENAME(dir_alias, filename);
    DBMS_LOB.FILEOPEN(file_ptr);
    UPDATE customers SET video = file_ptr WHERE CURRENT OF cust_csr;
    DBMS_OUTPUT.PUT_LINE('File: ' || filename || ' Size: ' || DBMS_LOB.GETLENGTH(file_ptr));
    DBMS_LOB.FILECLOSE(file_ptr);
  END LOOP;
END set_video;
/

--EXECUTE set_video('SUBDIR', 'WIN_20191106_08_07_18_Pro.mp4', 448);

--===========================
-- PROCEDURE get_filesize
CREATE OR REPLACE FUNCTION get_filesize
(
  p_file_ptr IN OUT BFILE
) RETURN NUMBER IS
v_file_exists BOOLEAN;
v_length NUMBER := -1;
BEGIN
  v_file_exists := DBMS_LOB.FILEEXISTS(p_file_ptr) = 1;
  IF v_file_exists THEN
    DBMS_LOB.FILEOPEN(p_file_ptr);
    v_length := DBMS_LOB.GETLENGTH(p_file_ptr);
    DBMS_LOB.FILECLOSE(p_file_ptr);
  END IF;
  RETURN v_length;
END get_filesize;
/

/*
DECLARE
  v_ptr BFILE;
  fle_size NUMBER;
BEGIN
  v_ptr := BFILENAME('SUBDIR', 'WIN_20191106_08_07_18_Pro.mp4');
  fle_size := get_filesize(v_ptr);
  DBMS_OUTPUT.PUT_LINE('File Size: ' || fle_size);
END;
*/
