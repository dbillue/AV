/*****************************
Author:       Duane Billue
Date:         2019-10-28
Description:  Dynamic SQL
*****************************/

SET SERVEROUTPUT ON
SET AUTOPRINT ON

CREATE OR REPLACE PACKAGE TABLE_PKG IS
  PROCEDURE execute_stmnt(v_sql_stmt VARCHAR2);
  PROCEDURE make(p_table_name VARCHAR2, p_col_specs VARCHAR2);
  PROCEDURE add_row(p_table_name VARCHAR2, p_col_values VARCHAR2, p_cols VARCHAR2 := NULL);
  PROCEDURE upd_row(p_table_name VARCHAR2, p_set_values VARCHAR2, p_conditions VARCHAR2 := NULL);
  PROCEDURE del_row(p_table_name VARCHAR2, p_conditions VARCHAR2 := NULL);
  PROCEDURE remove(p_table_name VARCHAR2);
END TABLE_PKG;
/

CREATE OR REPLACE PACKAGE BODY TABLE_PKG IS
  -- PROCEDURE execute_stmnt
  PROCEDURE execute_stmnt
  (
    v_sql_stmt VARCHAR2
  ) IS
  BEGIN
    DBMS_OUTPUT.PUT_LINE(v_sql_stmt);
    EXECUTE IMMEDIATE v_sql_stmt;
  END execute_stmnt;
  
  -- PROCEDURE make
  PROCEDURE make
  (
    p_table_name VARCHAR2, 
    p_col_specs VARCHAR2
  ) IS
  v_sql_stmt VARCHAR2(1000);
  BEGIN
    v_sql_stmt := 'CREATE TABLE ' || p_table_name || '(' ||
                  p_col_specs || ')';
    execute_stmnt(v_sql_stmt);
  END make;
  
  -- PROCEDURE add_row
  PROCEDURE add_row
  (
    p_table_name VARCHAR2, 
    p_col_values VARCHAR2, 
    p_cols VARCHAR2 := NULL
  ) IS
  v_sql_stmt VARCHAR2(1000);
  BEGIN
    v_sql_stmt := 'INSERT INTO ' || p_table_name || 
                  '(' || p_cols || ') VALUES (' || p_col_values || ')';
    --v_sql_stmt := 'INSERT INTO my_contacts (id, name) VALUES (500, ''OT'')';
    execute_stmnt(v_sql_stmt);
  END add_row;
  
  -- PROCEDURE upd_row
  PROCEDURE upd_row
  (
    p_table_name VARCHAR2, 
    p_set_values VARCHAR2, 
    p_conditions VARCHAR2 := NULL
  ) IS
  v_sql_stmt VARCHAR2(1000);
  BEGIN
    v_sql_stmt := 'UPDATE ' || p_table_name || ' SET ' || p_set_values ||
                  ' WHERE ' || p_conditions;
    --execute_stmnt(v_sql_stmt);
  END upd_row;
  
  -- PROCEDURE del_row
  PROCEDURE del_row
  (
    p_table_name VARCHAR2,
    p_conditions VARCHAR2 := NULL
  ) IS
  v_sql_stmt VARCHAR2(1000);
  BEGIN
    v_sql_stmt := 'DELETE FROM ' || p_table_name || ' WHERE ' || p_conditions;
    --execute_stmnt(v_sql_stmt);
  END del_row;
  
  -- PROCEDURE remove
  PROCEDURE remove
  (
    p_table_name VARCHAR2
  ) IS
    v_sql_stmt VARCHAR2(500);
    v_cur_id INTEGER;
  BEGIN
    v_sql_stmt := 'DROP TABLE ' || p_table_name;
    v_cur_id := DBMS_SQL.OPEN_CURSOR;
    DBMS_SQL.PARSE(v_cur_id, v_sql_stmt, DBMS_SQL.NATIVE);
    DBMS_SQL.CLOSE_CURSOR(v_cur_id);
  END remove;
END TABLE_PKG;
/