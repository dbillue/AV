// CP_Intro
console.clear();
var speedMPH:number = 25;
var distanceMileInMiles:number = 3;
console.log("Distance traveled: " + (speedMPH * distanceMileInMiles) + " miles.");

// CP_Class
class Distance {
    speedMilePerHour:number = 10;
    distanceMiles:number = 10;

    DetermineDistance():void {
        console.log("Distance traveled: " + (this.speedMilePerHour * this.distanceMiles) + " miles.");
    }
}

let distance = new Distance();
distance.DetermineDistance();

// CP_Type_Assertion
var str = 5;
var str2:number = <number> <any> str;
console.log(typeof(str2));


// THIS IS FUN!!!!