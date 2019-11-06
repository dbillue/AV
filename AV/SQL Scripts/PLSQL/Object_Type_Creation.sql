/*****************************
Author:       Duane Billue / Oracle
Date:         2019-11-05
Description:  Object Type Creation
*****************************/

CREATE OR REPLACE TYPE typ_cr_card AS OBJECT
(
  card_type   VARCHAR2(25),
  card_num    NUMBER
)
/

CREATE OR REPLACE TYPE typ_cr_card_nst AS TABLE OF typ_cr_card
/

-- Add column of type nested table
/*
ALTER TABLE customers ADD
(credit_cards typ_cr_card_nst)
  NESTED TABLE credit_cards STORE AS c_c_store_tab;
*/
