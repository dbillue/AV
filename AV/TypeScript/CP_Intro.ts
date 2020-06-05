/******************
 Author:        Duane Billue
 Date:          2020-06-05
 Description:   Basic TypeScript variables, object and statements.
 ******************/

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
console.log("Data type: " + typeof(str2));

// CP_Conditional_Loops
var cnt:number = 10;
var totalCnt:number = 15;
// While Loop
while (cnt <= totalCnt) {
    console.log("Current number in while loop: " + cnt);
    cnt += 1;
}

cnt = 5;
// Do While Loop
do {
    console.log("Current number in do while loop: " + cnt);
    cnt += 1;
 } while (cnt <= totalCnt);

 // While Loop w/ Break statement
 cnt = 1;
 while (cnt <= totalCnt) {
     if(cnt == (totalCnt / 3)) {
        console.log("Break statement reached: " + cnt);
         break;
     }
     cnt++;
}

// For Next Loop
for(cnt = 0; cnt <= totalCnt; cnt++)
{
    if(cnt <= (totalCnt / 3)) {
        console.log("Continue statement in effect: " + cnt);
        continue;
    }
}


// THIS IS FUN!!!!