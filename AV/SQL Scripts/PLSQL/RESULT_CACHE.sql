/*****************************
Author:       Duane Billue
Date:         2019-11-11
Description:  RESULT_CACHE
*****************************/

SET TIMING ON
SET SERVEROUTPUT ON

CREATE OR REPLACE FUNCTION ord_count(cust_no number)
RETURN NUMBER
RESULT_CACHE RELIES_ON (orders)
IS
  v_count NUMBER;
BEGIN
  SELECT COUNT(*) INTO v_count FROM orders WHERE customer_id = cust_no;
  return v_count;
END;

SELECT cust_last_name, ord_count(customer_id) no_of_orders FROM customers
WHERE cust_last_name = 'MacGraw';


--============================--
--     Cache / Performance    --
--============================--
SELECT 
/*+ result_cache */ count(*), round(avg(quantity_on_hand)) AVG_AMT, product_id, product_name
--count(*), round(avg(quantity_on_hand)) AVG_AMT, product_id, product_name
FROM inventories NATURAL JOIN product_information
GROUP BY product_id, product_name;


/*---------------------------------*/
CREATE OR REPLACE TYPE list_typ IS TABLE OF VARCHAR2(35);
/

CREATE OR REPLACE FUNCTION get_warehouse_names
RETURN list_typ
RESULT_CACHE RELIES_ON (warehouses)
IS
  v_count BINARY_INTEGER;
  v_wh_names list_typ := list_typ();
BEGIN
  SELECT COUNT(*) INTO v_count FROM warehouses;
  v_wh_names.EXTEND(v_count);
  FOR i IN 1 ..v_count LOOP
    SELECT warehouse_name INTO v_wh_names(i) FROM warehouses WHERE warehouse_id = i;
  END LOOP;
  RETURN v_wh_names;
END get_warehouse_names;

-- SELECT * FROM TABLE(get_warehouse_names);
