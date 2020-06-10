/*******************
 Author:        Duane Billue
 Date:          2020-06-10
 Description:   Typescript namespace OOP artist example
 *******************/

/// <reference path="Musician.ts" />
/// <reference path="Instrument.ts" />
/// <reference path="Tours.ts" />

let instrument = new Artist.Instrument();
instrument.instrumentName = "Guitar";
instrument.instumentClass = "Strings";

let musician = new Artist.Musician("Ace", "Ace of Diamonds");
let tourDates = new Artist.Tours(musician._bandName);

console.log(`Leader of band \'${musician._bandName}\' is ${musician._stageName}`);
console.log(`${musician._stageName} instrument is ${instrument.instrumentName}`);
