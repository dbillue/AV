/*****************************
Author:       Duane Billue / Oracle
Date:         2019-11-07
Description:  Oracle LOB / DBMS_LOB.*
*****************************/

CREATE TABLE personnel
(
  id NUMBER(6) CONSTRAINT personnel_id_key PRIMARY KEY,
  last_name VARCHAR2(35),
  review CLOB,
  picture BLOB
);

-- INSERT INTO personnel VALUES (2034, 'Allen', empty_clob(), NULL);
-- INSERT INTO personnel VALUES (2035, 'Bond', empty_clob(), NULL);
-- SELECT * FROM personnel;

CREATE TABLE review_table
(
  employee_id   NUMBER,
  ann_review    VARCHAR2(2000)
);

-- INSERT INTO review_table VALUES (2034, 'Very good performance this year. ' || ' Recommended to increase salary by $500');
-- INSERT INTO review_table VALUES (2035, 'Excellent good performance this year. ' || ' Recommended to increase salary by $1000');

----------------------------
-- Create document repository (exe as sys)
CREATE OR REPLACE DIRECTORY product_pic AS 'C:\data\product_pic';

----------------------------
-- Grant priviledges to user OE
GRANT READ ON DIRECTORY resume_files TO OE;

-----------------------------
-- Update CLOB
UPDATE personnel
  SET review = (SELECT ann_review FROM review_table WHERE employee_id IN (2034))
WHERE last_name = 'Allen';

UPDATE personnel
  SET review = (SELECT ann_review FROM review_table WHERE employee_id IN (2035))
WHERE last_name = 'Bond';

-----------------------------
-- ALTER table BFILE column
ALTER TABLE product_information ADD (picture BFILE);

-----------------------------
-- Procedure load_product_image
CREATE OR REPLACE PROCEDURE load_product_image
(
  p_dir IN VARCHAR2
)
IS
  v_file        BFILE;
  v_filename    VARCHAR2(40);
  v_rec_number  NUMBER;
  v_file_exists BOOLEAN;
  CURSOR product_csr IS
    SELECT * FROM product_information WHERE category_id IN (12) FOR UPDATE;
BEGIN
  DBMS_OUTPUT.PUT_LINE('Loading locators to images');
  FOR rec IN product_csr LOOP
    v_filename := rec.product_id || '.gif';
    v_file := BFILENAME(p_dir, v_filename);
    v_file_exists := (DBMS_LOB.FILEEXISTS(v_file) = 1);
    IF v_file_exists THEN
      DBMS_LOB.FILEOPEN(v_file);
      UPDATE product_information
        SET picture = v_file
        WHERE CURRENT OF product_csr;
      DBMS_OUTPUT.PUT_LINE('Set locator to file: ' || v_filename || ' Size: ' || DBMS_LOB.GETLENGTH(v_file));
      DBMS_LOB.FILECLOSE(v_file);
      v_rec_number := product_csr%ROWCOUNT;
    ELSE
      DBMS_OUTPUT.PUT_LINE('File ' || v_filename || ' does not exist');
    END IF;
  END LOOP;
  DBMS_OUTPUT.PUT_LINE('Total files updated: ' || v_rec_number);
  EXCEPTION
    WHEN OTHERS THEN
      DBMS_LOB.FILECLOSE(v_file);
      DBMS_OUTPUT.PUT_LINE('Error Code: ' || TO_CHAR(SQLCODE));
      DBMS_OUTPUT.PUT_LINE('Error Msg: ' || SQLERRM);
END load_product_image;
/

-- Call procedure with data repository parameter
EXEC load_product_image('PRODUCT_PIC');

---------------------????????
CREATE OR REPLACE PROCEDURE check_space
IS
  l_fs1_bytes             NUMBER;
  l_fs2_bytes             NUMBER;
  l_fs3_bytes             NUMBER;
  l_fs4_bytes             NUMBER;
  l_fs1_blocks            NUMBER;
  l_fs2_blocks            NUMBER;
  l_fs3_blocks            NUMBER;
  l_fs4_blocks            NUMBER;
  l_full_bytes            NUMBER;
  l_full_blocks           NUMBER;
  l_unformatted_bytes     NUMBER;
  l_unformatted_blocks    NUMBER;
BEGIN
  DBMS_SPACE.SPACE_USAGE
  (
    segment_owner       => 'OE',
    segment_name        => 'PRODUCT_INFORMATION',
    segment_type        => 'TABLE',
    fs1_bytes           => l_fs1_bytes,
    fs1_blocks          => l_fs1_blocks,
    --fs2_bytes           => l_fs2_bytes,
    --fs2_blocks          => l_fs2_blocks,
    --fs3_bytes           => l_fs3_bytes,
    --fs3_blocks          => l_fs3_blocks,
    --fs4_bytes           => l_fs4_bytes,
    --fs4_blocks          => l_fs4_blocks,
    full_bytes          => l_full_bytes,
    full_blocks         => l_full_blocks,
    unformatted_bytes   => l_unformatted_blocks,
    unformatted_bytes   => l_unformatted_bytes
  );
 DBMS_OUTPUT.ENABLE;
 DBMS_OUTPUT.PUT_LINE(' FS1 Blocks = ' || l_fs1_blocks || ' FS1 Bytes: ' || l_fs1_bytes);
 DBMS_OUTPUT.PUT_LINE(' FS2 Blocks = ' || l_fs2_blocks || ' FS2 Bytes: ' || l_fs2_bytes);
 DBMS_OUTPUT.PUT_LINE(' FS3 Blocks = ' || l_fs3_blocks || ' FS3 Bytes: ' || l_fs3_bytes);
 DBMS_OUTPUT.PUT_LINE(' FS4 Blocks = ' || l_fs4_blocks || ' FS4 Bytes: ' || l_fs4_bytes);
 DBMS_OUTPUT.PUT_LINE(' Full Blocks = ' || l_full_blocks || ' Bytes: ' || l_full_bytes);
 DBMS_OUTPUT.PUT_LINE('========================================');
 DBMS_OUTPUT.PUT_LINE('Total Blocks = ' || TO_CHAR(l_fs1_blocks + l_fs2_blocks + l_fs3_blocks + l_fs4_blocks + l_full_blocks) || 
                      ' Total Bytes = ' || TO_CHAR(l_fs1_bytes + l_fs2_bytes + l_fs3_bytes + l_fs4_bytes));
END check_space;
/
