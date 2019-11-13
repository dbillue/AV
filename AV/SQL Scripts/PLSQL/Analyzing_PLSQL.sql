/*****************************
Author:       Duane Billue
Date:         2019-11-12
Description:  Analyzing PL/SQL 
*****************************/

-- Find all instance of CHAR in code
-- using data dictionary 'USER_SOURCE'
SELECT NAME, LINE, TEXT FROM user_source 
WHERE  INSTR(UPPER(text), ' CHAR') > 0
      OR INSTR(UPPER(text), 'CHAR(') > 0
      OR INSTR(UPPER(text), ' CHAR(') > 0;
      
--------------------------------
--------------------------------
      
CREATE OR REPLACE PACKAGE query_code_pkg 
AUTHID CURRENT_USER
IS
  PROCEDURE find_text_in_code (str IN VARCHAR2);
  PROCEDURE encap_compliance;
END query_code_pkg;
/

CREATE OR REPLACE PACKAGE BODY query_code_pkg IS
  PROCEDURE find_text_in_code 
  (str IN VARCHAR2)
  IS
    TYPE info_rt IS RECORD 
    (
      NAME user_source.name%TYPE, 
      text user_source.text%TYPE
    );
    TYPE info_aat IS TABLE OF info_rt INDEX BY PLS_INTEGER;
    info_aa info_aat;
  BEGIN
    SELECT NAME || '-' || line, text
    BULK COLLECT INTO info_aa FROM user_source
      WHERE UPPER(text) LIKE '%' || UPPER(str) || '%'
        AND name != 'VALSTD' AND name != 'ERRNUMS';
    
    DBMS_OUTPUT.PUT_LINE ('Checking for presence of ' || str || ':');
    
    FOR indx IN info_aa.FIRST .. info_aa.LAST LOOP
      DBMS_OUTPUT.PUT_LINE (info_aa(indx).Name || ',' || info_aa(indx).text);
    END LOOP;
  END find_text_in_code;
  
  PROCEDURE encap_compliance IS
  SUBTYPE qualified_name_t IS VARCHAR2(200);
  TYPE refby_rt IS RECORD 
    (
      NAME qualified_name_t,
      referenced_by qualified_name_t
    );
  TYPE refby_aat IS TABLE OF refby_rt INDEX BY PLS_INTEGER;
  refby_aa refby_aat;
  BEGIN
    SELECT owner || '.' || NAME refs_table,
            referenced_owner || '.' || referenced_name
            AS table_referenced
    BULK COLLECT INTO refby_aa
      FROM all_dependencies
      WHERE owner = user
        AND TYPE IN ('PACKAGE', 'PACKAGE_BODY', 'PROCEDURE', 'FUNCTION')
        AND referenced_type IN ('TABLE', 'VIEW')
        AND referenced_owner NOT IN ('SYS', 'SYSTEM')
      ORDER BY owner, name, referenced_owner, referenced_name;
      
      DBMS_OUTPUT.PUT_LINE('Programs that reference tables or views');
      
      FOR indx IN refby_aa.FIRST .. refby_aa.LAST LOOP
        DBMS_OUTPUT.PUT_LINE (refby_aa(indx).Name || ',' || refby_aa(indx).referenced_by);
      END LOOP;
  END encap_compliance;  
END query_code_pkg;

-- exec query_code_pkg.encap_compliance;
-- exec query_code_pkg.find_text_in_code('BEGIN');

------------------------------------
--   Report of USER_IDENTIFIERS   --
------------------------------------
ALTER SESSION SET PLSCOPE_SETTINGS = 'IDENTIFIERS: ALL';
ALTER PACKAGE credit_card_pkg COMPILE;

WITH v AS
(SELECT   line,
          col,
          INITCAP(name) name,
          LOWER(type) type,
          LOWER(usage) usage,
          usage_id, usage_context_id
  FROM USER_IDENTIFIERS
  WHERE Object_Name = 'Credit_Card_Package'
    AND Object_Type = 'Package Body')
    -- Inline view
    SELECT RPAD(LPAD(' ', 2*(Level-1)) ||
          Name, 20, '.' ) || ' ' ||
          RPAD(Type, 20) || RPAD(Usage, 20)
          IDENTIFIER_USAGE_CONTEXTS
    FROM v
    START WITH USAGE_CONTEXT_ID = 0
    CONNECT BY PRIOR USAGE_ID = USAGE_CONTEXT_ID
    ORDER SIBLINGS BY line, col;
          
    

