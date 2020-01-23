/*******************
Author:			Duane Billue
Date:			2020-01-22
Description:	Developer SQL scripts for AV
*******************/
SELECT @@VERSION
sp_who2
sp_tables

-- kill nnn
-- drop table xx

USE [AV]

/************
Persons
************/
sp_help Persons

SELECT * FROM Persons ORDER BY LastName


/************
Pets
************/
sp_help Pets

SELECT * FROM Pets ORDER BY Name


/************
PetTypes
************/
sp_help PetTypes

SELECT * FROM PetTypes ORDER BY Type


/************
States
************/
sp_help States

SELECT * FROM States ORDER BY State
