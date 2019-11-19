/*****************************
Author:       Duane Billue
Date:         2019-11-18
Description:  Bulk Collect w/ UTL_FILE output
*****************************/

--SET SERVEROUTPUT ON
--SELECT * from ALL_DIRECTORIES;

/*
SELECT * FROM sales_report;
SELECT * FROM orders;
SELECT * FROM order_items;
SELECT * FROM product_information;
SELECT * FROM order_import;
*/

DECLARE
  -- UTIL File
  v_fle UTL_FILE.FILE_TYPE;
  v_fileName VARCHAR2(50);
  v_dirName VARCHAR2(50);
  
  -- Cursor
  CURSOR cur_closed_orders IS
    SELECT CAST(o.order_id AS VARCHAR(10)) order_id, 
          CAST(pi.product_name AS VARCHAR(100)) product_name, 
          oi.product_id, 
          oi.quantity, 
          TO_DATE(CAST(o.order_date AS DATE), 'DD-MON-YYYY') AS order_date
    FROM 
      orders o join order_items oi ON o.order_id = oi.order_id
      join product_information pi ON pi.product_id = oi.product_id
    WHERE o.order_status IN (1)
    ORDER BY o.order_id, oi.line_item_id;
    
  -- Rowset type
  TYPE order_dataset IS TABLE OF cur_closed_orders%ROWTYPE;
  v_dataset order_dataset;
BEGIN
  v_fileName := 'Closed_Orders.csv';
  v_dirName := 'ORDERS';
  v_fle := UTL_FILE.FOPEN(v_dirName, v_fileName, 'W');
  
  SELECT CAST(o.order_id AS VARCHAR(10)) order_id, 
        pi.product_name , 
        oi.product_id, 
        oi.quantity, 
        TO_DATE(CAST(o.order_date AS DATE), 'DD-MON-YYYY') AS order_date
  BULK COLLECT INTO v_dataset
  FROM orders o join order_items oi ON o.order_id = oi.order_id
  JOIN product_information pi ON pi.product_id = oi.product_id
  WHERE o.order_status IN (1)
  ORDER BY o.order_id, oi.line_item_id;
  
  UTL_FILE.PUT_LINE(v_fle, 'REPORT GENERATED ON ' || SYSDATE);
  UTL_FILE.NEW_LINE(v_fle);                    
  
  FOR i IN v_dataset.FIRST .. v_dataset.LAST
  LOOP
    DBMS_OUTPUT.PUT_LINE(v_dataset(i).product_name);
    UTL_FILE.PUT_LINE(v_fle,
                      v_dataset(i).order_id || ',' ||
                      v_dataset(i).product_name || ',' ||
                      v_dataset(i).product_id || ',' ||
                      v_dataset(i).quantity || ',' ||
                      v_dataset(i).order_date);
  END LOOP;
  
  UTL_FILE.NEW_LINE(v_fle);
  UTL_FILE.PUT_LINE(v_fle, '*** END OF REPORT ***');
  UTL_FILE.FCLOSE(v_fle);  
END;