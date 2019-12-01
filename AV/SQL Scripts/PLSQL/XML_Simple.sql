/****************
Author:			  Duane Billue
Date:			    11-25-2019
Description:	XML type
****************/

/*
CREATE TABLE warehouses (
  warehouse_id  NUMBER(3),
  warehouse_spec  SYS.XMLTYPE,
  warehouse_name  VARCHAR2 (35),
  location_id  NUMBER(4));
*/

INSERT into warehouses (warehouse_id, warehouse_spec)
  VALUES (100, sys.XMLTYPE.createXML(
  '<Warehouse whNo="100">
    <Building>Owned</Building>
  </Warehouse>'));
  
SELECT
  w.warehouse_spec.extract('/Warehouse/Building/text()').getStringVal()
  "Building"
  FROM warehouses w;