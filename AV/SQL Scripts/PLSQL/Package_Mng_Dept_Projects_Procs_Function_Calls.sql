/*****************************
Author:       Duane Billue / Oracle
Date:         2019-11-05
Description:  PACKAGE manage_dept_proj proc and function calls
*****************************/

-- Query nested table using TABLE function
SELECT t1.DEPT_ID, t1.NAME, t1.BUDGET, t2.* 
FROM DEPARTMENT t1, 
TABLE(t1.Projects) t2 ORDER BY t1.DEPT_ID, t2.PROJECT_NO;

--===================================
-- Procedure allocate_new_proj_list
-- EXECUTE allocate_new_proj_list (25, 'HR', 5000000);  ##Reference procedure for code##

--===================================
-- Function get_dept_project
/*
DECLARE
  v_proj_list type_projectlist;
BEGIN
  v_proj_list := get_dept_project(20);
  FOR i IN 1..v_proj_list.LAST LOOP
    DBMS_OUTPUT.PUT_LINE('Project Number: ' || v_proj_list(i).project_no);
    DBMS_OUTPUT.PUT_LINE('Project Title: ' || v_proj_list(i).title);
    DBMS_OUTPUT.PUT_LINE('Project Cost: ' || v_proj_list(i).cost);
  END LOOP;
END;
*/

--===================================
-- Function manipulate_project
/*
DECLARE
  v_projs type_projectlist;
BEGIN
  v_projs := manipulate_project(20);
  FOR i IN 1..v_projs.LAST LOOP
    DBMS_OUTPUT.PUT_LINE('Project Number: ' || v_projs(i).project_no);
    DBMS_OUTPUT.PUT_LINE('Project Title: ' || v_projs(i).title);
    DBMS_OUTPUT.PUT_LINE('Project Cost: ' || v_projs(i).cost);
  END LOOP;
END;
*/

--===================================
-- Function check_costs
/*
DECLARE
 v_project_list type_projectlist;
BEGIN
  v_project_list := manage_dept_proj.manipulate_project(25);
  IF manage_dept_proj.check_costs(v_project_list) THEN
    DBMS_OUTPUT.PUT_LINE('At least one project exceeded budget.');
  ELSE
    DBMS_OUTPUT.PUT_LINE('All projects within budget....excellent.');
  END IF;
END;
*/

--===================================
-- Procedure update_a_project
/*
DECLARE
  v_project type_project;
BEGIN
  v_project := type_project(4, 'End of Year Employee Tax Forms', 25000);
  update_a_project (25, v_project, 2);
END;
*/