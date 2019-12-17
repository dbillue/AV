CREATE USER oe IDENTIFIED BY Spindel;
ALTER USER oe DEFAULT TABLESPACE users QUOTA UNLIMITED ON users;
ALTER USER oe TEMPORARY TABLESPACE temp;
GRANT CREATE SESSION, CREATE SYNONYM, CREATE VIEW TO oe;
GRANT CREATE DATABASE LINK, ALTER SESSION TO oe;
GRANT RESOURCE , UNLIMITED TABLESPACE TO oe;
GRANT CREATE MATERIALIZED VIEW  TO oe;
GRANT QUERY REWRITE             TO oe;