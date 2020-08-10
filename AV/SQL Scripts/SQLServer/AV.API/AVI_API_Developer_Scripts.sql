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
	AND PersonId IN ('fb57dfe0-29cd-4b60-b54b-9343ec8a91dc')
	--AND LastName <> 'Billue'

SELECT person.*, bs.[State], pet.*
FROM persons person LEFT OUTER JOIN pets pet ON person.PersonId = pet.PersonId
	INNER JOIN birthstate bs ON bs.StateId = person.StateId
ORDER BY person.LastName, person.FirstName, pet.Name

INSERT INTO Persons (FirstName, MIddleName, LastName, Gender, Age, Country, City, StateId, DateOfBirth)
VALUES ('Allison', 'Karly', 'Hope', 'Female', 34, 'USA', 'Unknown', 5, '1986-07-05 00:00:00')

DELETE FROM Persons
WHERE 1 = 1
	AND PersonId IN ('490B6892-DD11-4451-C3A8-08D829D6DD05')
	--AND FirstName IN ('Alex', 'asda')

-- DELETE FROM Persons WHERE PersonId IN ('D4803493-168C-409A-9D50-08D82A51E516')

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

-- DELETE FROM Pets WHERE PetId IN ('4F433572-A144-4E05-64D0-08D82A51E544')


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
	