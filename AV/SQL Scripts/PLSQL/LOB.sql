/*****************************
Author:       Duane Billue / Oracle
Date:         2019-11-06
Description:  Oracle LOB / DBMS_LOB.*
*****************************/

SET SERVEROUTPUT ON;

---------------------------
-- Add column of CLOB and BLOB
ALTER TABLE customers
  ADD(resume CLOB, picture BLOB);
  
---------------------------
-- Create TABLESPACE to hold LOB data
-- Execute as SYSDBA
CREATE TABLESPACE lob_tbs1
  2  DATAFILE 'lob_tbs1.dbf' SIZE 800M REUSE
  3  EXTENT MANAGEMENT LOCAL
  4  UNIFORM SIZE 64M
  5  SEGMENT SPACE MANAGEMENT AUTO;
  
---------------------------
-- Create Table Profiles using tablespace to hold BLOB data
CREATE TABLE customer_profiles
(
  id          NUMBER,
  full_name   VARCHAR2(45),
  resume      CLOB DEFAULT EMPTY_CLOB(),
  picture     BLOB DEFAULT EMPTY_BLOB())
  LOB(picture) STORE AS BASICFILE
      (TABLESPACE lob_tbs1);
      
DROP TABLE customer_profiles;
      
---------------------------
-- Insert record using EMPTY_BLOB() function ex 1
INSERT INTO customer_profiles
(id, full_name, resume, picture)
VALUES
(164, 'Charlotte Kazan', EMPTY_CLOB(), NULL);

-- Update record using EMPTY_BLOB() function ex 2
UPDATE customer_profiles 
SET resume = 'DOB: 1972-11-20', picture = EMPTY_BLOB()
WHERE id = 164;

--SELECT * FROM customer_profiles;

--============================????????
-- Procedure loadLOBFromBFILE_proc
CREATE OR REPLACE PROCEDURE loadLOBFromBFILE_proc
(
  p_dest_loc IN OUT BLOB,
  p_file_name IN VARCHAR2,
  p_file_dir IN VARCHAR2
) IS
v_src_loc BFILE   := BFILENAME(p_file_dir, p_file_name);
v_amount  INTEGER := 300000;
offset    INTEGER := 1;
BEGIN
    DBMS_LOB.OPEN(v_src_loc, DBMS_LOB.LOB_READONLY);
    v_amount := DBMS_LOB.GETLENGTH(v_src_loc);
    DBMS_LOB.LOADBLOBFROMFILE(p_dest_loc, v_src_loc, v_amount, offset, offset);
    DBMS_LOB.CLOSE(v_src_loc);
END loadLOBFromBFILE_proc;
/

---------------------------
-- Procedure write_lob
CREATE OR REPLACE PROCEDURE write_lob
(
  p_file IN VARCHAR2,
  p_dir IN VARCHAR2
) IS
v_charPos NUMBER;
i         NUMBER;
v_fn      VARCHAR2(15);
v_ln      VARCHAR2(40);
v_b       BLOB;
BEGIN
  DBMS_OUTPUT.ENABLE;
  DBMS_OUTPUT.PUT_LINE('Begin inserting rows...');
  FOR i IN 1..30 LOOP
    v_fn := SUBSTR(p_file, 1, INSTR(p_file, '.') - 1);
    v_ln := SUBSTR(p_file, INSTR(p_file, '.') + 1, LENGTH(p_file) - INSTR(p_file,'.') - 4);
    INSERT INTO customer_profiles VALUES (i, v_fn || ' ' || v_ln, EMPTY_CLOB(), EMPTY_BLOB())
      RETURNING picture INTO v_b;
    loadLOBFromBFILE_proc(v_b, p_file, p_dir);
    DBMS_OUTPUT.PUT_LINE('Row ' || i || ' inserted.');
  END LOOP;
  COMMIT;
END write_lob;
/

EXECUTE write_lob ('david.sloan.rtf', 'RESUME_FILES');
EXECUTE write_lob ('Jason.Sterling.rtf', 'RESUME_FILES');
EXECUTE write_lob ('Monica.Peters.rtf', 'RESUME_FILES');

--SELECT * FROM customer_profiles;
--TRUNCATE TABLE customer_profiles;

---------------------------
-- Procedure lob_txt (Read and insert CLOB)
CREATE OR REPLACE PROCEDURE lob_txt
(
  file_name       VARCHAR2, 
  p_dir           VARCHAR2 DEFAULT 'PLSQL_DIR'
) IS
c               clob := NULL;
byte_count      PLS_INTEGER;
v_dest_offset   INTEGER := 1;
v_src_offset    INTEGER := 1;
v_lang_context  INTEGER := 0;
v_warning       INTEGER := 1;
fil             BFILE := BFILENAME(p_dir, file_name);
BEGIN
  c := TO_CLOB(' ');
  IF DBMS_LOB.FILEEXISTS(fil) = 1 THEN
    DBMS_LOB.FILEOPEN(fil, DBMS_LOB.FILE_READONLY);
    byte_count := DBMS_LOB.GETLENGTH(fil);
    DBMS_OUTPUT.PUT_LINE('The length of file ' || file_name || ' is ' || byte_count || ' bytes.');
    DBMS_LOB.LOADCLOBFROMFILE
    (
      dest_lob => c, 
      src_bfile => fil, 
      amount => byte_count,
      dest_offset => v_dest_offset,
      src_offset => v_src_offset,
      bfile_csid => 0,
      lang_context => v_lang_context,
      warning => v_warning
    );
    DBMS_LOB.FILECLOSEALL;
    INSERT INTO lob_text VALUES (LOB_SEQUENCE.NEXTVAL, c);
    COMMIT;
  ELSE
    DBMS_OUTPUT.PUT_LINE('The file does not exists');
  END IF;
END lob_txt;
/

--exec lob_txt('david.sloan.rtf', 'RESUME_FILES');
--select * from LOB_TEXT;

---------------------------
-- UPDATE LOB file (append text)
DECLARE
  v_lobloc  CLOB;
  v_text    VARCHAR2(50) := 'Resigned on 31 August 2018';
  v_amount  NUMBER;
  v_offset  INTEGER;
BEGIN
  SELECT resume INTO v_lobloc FROM customer_profiles
  WHERE id = 2 FOR UPDATE;
  v_offset := DBMS_LOB.GETLENGTH(v_lobloc) + 2;
  v_amount := length(v_text);
  DBMS_LOB.WRITE (v_lobloc, v_amount, v_offset, v_text);
END;
/

--SELECT * FROM customer_profiles WHERE id = 2

---------------------------
-- Reading CLOB using DBMS_LOB.n
SELECT DBMS_LOB.SUBSTR(text, 10, 500) FROM lob_text;

---------------------------
-- Procedure is_templob_open :: CREATE a Temp LOB (CLOB, BLOB, NCLOB)
CREATE OR REPLACE PROCEDURE is_templob_open
(
  p_lob IN OUT BLOB,
  p_retval OUT INTEGER
) IS
BEGIN
  -- Create temp LOB type
  DBMS_LOB.CREATETEMPORARY(p_lob, TRUE);
  -- Check if LOB is open.
  p_retval := DBMS_LOB.ISTEMPORARY(p_lob);
  IF p_retval = 1 THEN
    DBMS_OUTPUT.PUT_LINE('You have created a temporary LOB in the PLSQL block procedure');
    DBMS_LOB.FREETEMPORARY(p_lob);
  ELSE
    DBMS_OUTPUT.PUT_LINE('No temp LOB created');
  END IF;
END is_templob_open;
/
