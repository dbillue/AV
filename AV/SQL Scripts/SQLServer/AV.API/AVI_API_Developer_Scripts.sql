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
sp_helpindex Persons

SELECT * FROM Persons ORDER BY LastName, FirstName

SELECT * FROM Persons
WHERE 1 = 1
	--AND PersonId IN ('8C11AFEA-F999-450E-A497-2F5545D038E7')
	AND LastName <> 'Billue'

SELECT person.*, bs.[State], pet.*
FROM persons person LEFT OUTER JOIN pets pet ON person.PersonId = pet.PersonId
	INNER JOIN birthstate bs ON bs.StateId = person.StateId
ORDER BY person.FirstName, pet.Name

INSERT INTO Persons (FirstName, MIddleName, LastName, Gender, Age, Country, City, StateId, DateOfBirth)
VALUES ('Allison', 'Karly', 'Hope', 'Female', 34, 'USA', 'Unknown', 5, '1986-07-05 00:00:00')

DELETE FROM Persons
WHERE 1 = 1
	AND PersonId IN ('FCE84A41-5FD2-4253-82C9-8E152429ECA4', '998FB1EF-AF15-4032-AF53-B15B587B9142')
	--AND FirstName IN ('A', 'J')

-- DELETE FROM Persons WHERE PersonId IN ('2D603475-2D0A-449B-1152-08D821BE6067')

-- DROP TABLE Family_Seed


/************
Pets
************/
sp_help Pets
sp_helpindex Pets

SELECT * FROM Pets ORDER BY Name

SELECT pet.*, pettype.[Type]
FROM pets pet INNER JOIN PetTypes pettype ON pet.PetTypeId = pettype.PetTypeId
ORDER BY pettype.Type, pet.Name

INSERT INTO Pets (Name, NickName, PetTypeId, PersonId)
VALUES ('MountainBelly', 'BellyBaby', 2, 'A3442212-DA30-4ECF-A730-005014A63C9F')

-- DELETE FROM Pets WHERE PetId IN ('41E11BBA-77DA-43CD-ADD8-F91FAF9C2184')


/************
PetTypes
************/
sp_help PetTypes
sp_helpindex PetTypes

SELECT * FROM PetTypes ORDER BY Type


/************
BirthState
************/
sp_help BirthState
sp_helpindex BirthState

SELECT * FROM BirthState ORDER BY State

TRUNCATE TABLE BirthState


/************
WeatherForecast
************/
sp_help WeatherForecast
sp_helpindex WeatherForecast

SELECT * FROM WeatherForecast

INSERT INTO WeatherForecast ([Date], TemperatureC, TemperatureF, Summary, UserName)
SELECT 
	GetDate(), 37, 99, 'Hot!', 'me => DB'

--UPDATE WeatherForecast SET
--	UserName = 'duanebillue@yahoo.com'
--WHERE
--	Id = 1

DELETE FROM WeatherForecast
	