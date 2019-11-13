/*****************************
Author:       Duane Billue
Date:         2019-11-12
Description:  DBMS_DESCRIBE Utility
*****************************/

CREATE OR REPLACE PACKAGE use_dbms_describe IS
  PROCEDURE get_data
  (
    p_obj_name VARCHAR2
  );
END use_dbms_describe;
/

CREATE OR REPLACE PACKAGE BODY use_dbms_descibe IS
  PROCEDURE get_data
  (
    p_obj_name VARCHAR2
  ) IS
  v_overload      DBMS_DESCRIBE.NUMBER_TABLE;
  v_position      DBMS_DESCRIBE.NUMBER_TABLE;
  v_level         DBMS_DESCRIBE.NUMBER_TABLE;
  v_arg_name      DBMS_DESCRIBE.NUMBER_TABLE;
  v_datatype      DBMS_DESCRIBE.NUMBER_TABLE;
  v_def_value     DBMS_DESCRIBE.NUMBER_TABLE;
  v_in_out        DBMS_DESCRIBE.NUMBER_TABLE;
  v_length        DBMS_DESCRIBE.NUMBER_TABLE;
  v_precision     DBMS_DESCRIBE.NUMBER_TABLE;
  v_scale         DBMS_DESCRIBE.NUMBER_TABLE;
  v_radix         DBMS_DESCRIBE.NUMBER_TABLE;
  v_spare         DBMS_DESCRIBE.NUMBER_TABLE;
  BEGIN
    DBMS_DESCRIBE.DESCRIBE_PROCEDURE
    (
      p_obj_name, null, null, -- three in params
      v_overload, v_position, v_level, v_arg_name, v_datatype, v_def_value, v_in_out, v_length, 
      v_precision, v_scale, v_radix, v_spare, null
    );
    IF v_in_out.FIRST IS NULL THEN
      DBMS_OUTPUT.PUT_LINE ('No arguments to report');
    ELSE
      DBMS_OUTPUT.PUT('Name                          Mode');
      DBMS_OUTPUT.PUT_LINE ('  Position    Datatype  ');
      FOR i IN v_arg_name.FIRST .. v_arg_name.LAST LOOP
        IF v_position(i) = 0 THEN
          DBMS_OUTPUT.PUT('Return data for function: ');
        ELSE
          DBMS_OUTPUT.PUT(rpad(v_arg_name(i), LENGTH(v_arg_name(i)) + 42-LENGTH(v_arg_name(i)), ' '));
        END IF;
        DBMS_OUTPUT.PUT( '       ' ||
                        v_in_out(i) ||
                        '        ' ||
                        v_position(i) ||
                        '        ' ||
                        v_datatype(i));
        DBMS_OUTPUT.NEW_LINE;
      END LOOP;
    END IF;
  END get_data;
END use_dbms_describe;
/
