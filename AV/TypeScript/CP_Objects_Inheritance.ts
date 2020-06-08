/******************
 Author:        Duane Billue
 Date:          2020-06-06
 Description:   Basic TypeScript class / inheritence.
 ******************/

class Tours {
    _date:Date
    _venue:string
    _city:string
    _showStartTime:Date
    _showEndTime:Date
}

class Instrument {
	_instrumentName:string
    _instumentClass:string
}

class Musician extends Instrument  {
    stageName:string
    bandName:string
    position:string

	constructor(instrumentName:string, instumentClass:string) {
        super();
		this._instrumentName = instrumentName;
		this._instumentClass = instumentClass;
    }
    
    showInstrument():void {
        console.log(`showInstrument() function: ${this._instrumentName}`);
    }
}

var musician = new Musician("keyboards", "electrophones");
musician.bandName = "Greatest Story Ever Told";
musician.stageName = "Ace";

// Function call
musician.showInstrument();

console.log(`Band Name: ${musician.bandName}`);
console.log(`Artist Stage Name: ${musician.stageName}`);
console.log(`Instrument: ${musician._instrumentName}`);
console.log(`Instrument Class: ${musician._instumentClass}`);


// Instanceof
class Crew { 
    title:string
}
let crewMember = new Crew();
let isCrewMember = crewMember instanceof Crew;
console.log(`variable isCrewMember is instance of Crew: ` + isCrewMember);

// Interface example
interface IHouse {
    _numberofRooms:number;
    squareFootage:number;
    floorType:string;
}

class Dwelling implements IHouse {
    _price:number;
    _occupied:boolean;
    _numberofRooms:number;

    squareFootage:number;
    floorType:string;    

    constructor(price:number, occupied:boolean, numberofRooms:number) {
        this._price = price;
        this._occupied = occupied;
        this._numberofRooms = numberofRooms;
    }
}

var objDwelling = new Dwelling(260000, true, 4);
console.log("Implmentation example of interfaces!!!\t\n" + objDwelling._price + "\t\n" + objDwelling._occupied + "\t\n" + objDwelling._numberofRooms);

// Regular TS object (object function as a parameter)
var computer = {
    make: "HP",
    model: "Pavillion"
};

var printer = {
    make: "HP",
    model: "Officejet 5232"
}

var invokeDevice = function (obj: { make:string, model:string }) {
    console.log("make: " + obj.make);
    console.log("make: " + obj.model);
}

invokeDevice(printer);