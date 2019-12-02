/*****************************
Author:       Duane Billue
Date:         2019-12-02
Description:  User and Roles
*****************************/

-- Create an application role.
CREATE ROLE AV_REPORT_ROLE;
GRANT SELECT ON av.orderitems TO AV_REPORT_ROLE;
GRANT SELECT ON av.orders TO AV_REPORT_ROLE;
GRANT SELECT ON av.products TO AV_REPORT_ROLE;

-- Create a user role.
CREATE USER AV_REPORT 
    IDENTIFIED BY Alli3 
    DEFAULT TABLESPACE "USERS" 
    QUOTA 10M ON "USERS" 
    TEMPORARY TABLESPACE temp
    QUOTA 5M ON system 
    PASSWORD EXPIRE;

GRANT AV_REPORT_ROLE TO AV_REPORT;
GRANT CREATE SESSION TO AV_REPORT;

-- DROP ROLE AV_REPORT_ROLE
-- DROP USER AV_REPORT;