/******************
 Author:        Duane Billue
 Date:          2020-06-05
 Description:   Basic TypeScript Object Inheritance
 ******************/

console.clear();

// Create interface
interface ICard {
    cardType:string;
    cardValue:string;
    showCard: ()=> string;
}

// Create object
var card:ICard = {
    cardType:"Diamonds",
    cardValue:"Ace",
    showCard: ():string => { return "Hold for one more round..." }
}

console.log("Card Object / Interface\n-------------------");
console.log("Card Type: " + card.cardType);
console.log("Card Value: " + card.cardValue);
console.log(card.showCard());

/************************************/
/***************<^><^>***************/
// Simple Object Inheritance

class Instrument {
	_instrumentName:string
    _instumentClass:string
}

class Musician extends Instrument {
    stageName:string
    bandName:string
    position:string

	constructor(instrumentName:string, instumentClass:string) {
        super();
		this._instrumentName = instrumentName;
		this._instumentClass = instumentClass;
	}    
}

let musician = new Musician("keyboards", "electrophones");

musician.bandName = "Greatest Story Ever Told";
musician.stageName = "Ace";

console.log(`Band Name: ${musician.bandName}`);
console.log(`Artist Stage Name: ${musician.stageName}`);
console.log(`Instrument: ${musician._instrumentName}`);
console.log(`Instrument Class: ${musician._instumentClass}`);
