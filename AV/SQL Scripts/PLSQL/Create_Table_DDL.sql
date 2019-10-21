/*****************************
Author:       Duane Billue
Date:         2019-10-21
Description:  Table creation using sequences for primary key
*****************************/

-- Create table DDL --
CREATE TABLE Person
(
	ID				NUMBER(10) NOT NULL,
	NAME			VARCHAR2(250) NOT NULL,
	AGE				NUMBER(3) DEFAULT 0 NOT NULL,
	SKILL_LEVEL		NUMBER(2) DEFAULT 1 NOT NULL
);

-- Alter table DDL --
ALTER TABLE Person ADD
(
	CONSTRAINT person_pk PRIMARY KEY (ID),
	CONSTRAINT check_skill CHECK (SKILL_LEVEL BETWEEN 1 AND 10)
);

-- Create sequence DDL --
CREATE SEQUENCE person_sequence START WITH 1;

-- Create trigger DDL --
CREATE OR REPLACE TRIGGER person_trigger
BEFORE INSERT ON Person
FOR EACH ROW

BEGIN
	SELECT person_sequence.NEXTVAL 
	INTO :new.ID
	FROM dual;
END;
/