/*****************************
Author:       Duane Billue / Oracle
Date:         2019-11-05
Description:  PACKAGE manage_dept_proj
*****************************/

--====================================
-- PACKAGE manage_dept_proj Definition
CREATE OR REPLACE PACKAGE manage_dept_proj
AS
  PROCEDURE allocate_new_proj_list
  (
    p_dept_id NUMBER, 
    p_name VARCHAR2, 
    p_budget NUMBER
  );
  FUNCTION get_dept_project
  (p_dept_id NUMBER) RETURN type_projectlist;
  PROCEDURE update_a_project
  (
    p_deptno NUMBER,
    p_new_project type_project,
    p_position NUMBER
  );
  FUNCTION manipulate_project
  (p_dept_id NUMBER) RETURN type_projectlist;
  FUNCTION check_costs
  (p_project_list type_projectlist) RETURN BOOLEAN;
END manage_dept_proj;
/

--====================================
-- PACKAGE manage_dept_proj Body
CREATE OR REPLACE PACKAGE BODY manage_dept_proj
AS
  PROCEDURE allocate_new_proj_list
  (
    p_dept_id NUMBER,
    p_name VARCHAR2,
    p_budget NUMBER
  )IS
    v_accounting_project type_projectlist;
  BEGIN
    -- DEBUG
    DBMS_OUTPUT.PUT_LINE('Params:');
    DBMS_OUTPUT.PUT_LINE('p_dept_id: ' || p_dept_id ||
                        ' p_name: ' || p_name ||
                        ' p_budget: ' || p_budget);
    DBMS_OUTPUT.PUT_LINE('--------------------------');
  
    v_accounting_project := type_projectlist
                            (
                                type_project(1, 'Dsgn New Expense Rpt', 3250),
                                type_project(2, 'Outsource Payroll', 12350),
                                type_project(3, 'Audit Accounts Payable', 1425)
                            );
                            
    INSERT INTO department VALUES
      (p_dept_id, p_name, p_budget, v_accounting_project);
  END allocate_new_proj_list;
  
  FUNCTION get_dept_project
  (
    p_dept_id NUMBER
  ) RETURN type_projectlist
  IS
    v_accounting_project type_projectlist;
  BEGIN
    -- DEBUG
    DBMS_OUTPUT.PUT_LINE('Params:');
    DBMS_OUTPUT.PUT_LINE('p_dept_id: ' || p_dept_id);
    DBMS_OUTPUT.PUT_LINE('--------------------------');
    
    SELECT projects INTO v_accounting_project FROM department
    WHERE dept_id = p_dept_id;
    RETURN v_accounting_project;
  END get_dept_project;
  
  PROCEDURE update_a_project
  (
      p_deptno NUMBER,
      p_new_project type_project,
      p_position NUMBER
  ) IS
    v_my_projects type_projectlist;
  BEGIN
    -- DEBUG
    DBMS_OUTPUT.PUT_LINE('Params:');
    DBMS_OUTPUT.PUT_LINE('p_deptno: ' || p_deptno ||
                        --' p_new_project: ' || p_new_project ||
                        ' p_position: ' || p_position);
    DBMS_OUTPUT.PUT_LINE('--------------------------');
    
    v_my_projects := get_dept_project(p_deptno);
    v_my_projects.EXTEND;
    FOR i IN REVERSE p_position..v_my_projects.LAST - 1 LOOP
      v_my_projects(i + 1) := v_my_projects(i);
    END LOOP;
    v_my_projects(p_position) := p_new_project;
    
    UPDATE department SET projects = v_my_projects WHERE dept_id IN (p_deptno);
  END update_a_project;
  
  FUNCTION manipulate_project
  (p_dept_id NUMBER) 
    RETURN type_projectlist
  IS
    v_accounting_project type_projectlist;
    v_changed_list type_projectlist;  -- is this variable neccessary?
  BEGIN
    -- DEBUG
    DBMS_OUTPUT.PUT_LINE('Params:');
    DBMS_OUTPUT.PUT_LINE('p_dept_id: ' || p_dept_id);
    DBMS_OUTPUT.PUT_LINE('--------------------------');
    SELECT projects
      INTO v_accounting_project
      FROM department
      WHERE dept_id = p_dept_id;
      
    v_changed_list := v_accounting_project;
    RETURN v_changed_list;
  END manipulate_project;
  
  FUNCTION check_costs
  (p_project_list type_projectlist)
    RETURN BOOLEAN
  IS
    c_max_allowed   NUMBER := 1000000;
    i               INTEGER;
    v_flag          BOOLEAN := FALSE;
  BEGIN
    i := p_project_list.FIRST;
    WHILE i IS NOT NULL LOOP
      IF p_project_list(i).cost > c_max_allowed THEN
        v_flag := true;
        DBMS_OUTPUT.PUT_LINE (p_project_list(i).title || ' exceeded allowed budget.');
        RETURN TRUE;
      ELSE
        DBMS_OUTPUT.PUT_LINE (p_project_list(i).title || ' is within allowed budget.');
      END IF;
      i := p_project_list.NEXT(i);
    END LOOP;
    RETURN NULL;
  END check_costs;
  
END manage_dept_proj;
/