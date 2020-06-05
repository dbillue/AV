/******************
 Author:        Duane Billue
 Date:          2020-06-05
 Description:   Basic TypeScript functions.
 ******************/

 // Function with optional parameter
 function DisplayUserNames(id:number,userName:string,enabled?:boolean) {
     if(id == null && userName == null) {
         console.log("Please enter the userId and userName.");
     }

     // String interpolation (use tick marks!!!)
     console.log(`userId: ${id}\n` + `userName: ${userName}`)
 }

 DisplayUserNames(1,'asmith');

 // Function with Rest
 function restFunct(...cnt:number[]) {
     let i;
     let sum:number = 0;

     for(i=0;i<cnt.length;i++) {
         sum = sum + cnt[i];
     }

     console.log("Total sum of rest parameter: " + sum);
 }

 restFunct(1,2,3);

 // Function with default parameters
 function calcPrice(price:number, qty:number=5) {
    if(price == null || qty == null) {
        console.log("Please enter values to calculate price.");
    }

    console.log(`Total cost for ${qty} items: ${price * qty}`);
 }

 calcPrice(50.25);
 calcPrice(50.25,50000);

 // Anonymous function
 var ReservationsCount = function(persons:string[]) {
     return persons.length;
 }

 let reservedPeople:string[] = ["Dave", "Jason", "Jaimie", "Addison"];
 console.log("Number of reservations: " + ReservationsCount(reservedPeople));

 // Simple Lambda
 var StreetAddress = (houseNumber:number, streetName:string) => {
    let streetAddress = houseNumber + ' ' + streetName;
    console.log(`Street address: ${streetAddress}`);
 };

 StreetAddress(1058, 'Gilbert St');

// Parameter type inference
var ParamTypeFunc = (param1) => {
    let msg:string;
    if(param1 == null) {
        msg = "Parameter is empty :-<>";
    }
    if(typeof param1 == "string") {
        msg = "Parameter is of type string";
    } else {
        if(typeof param1 == "number") {
            msg = "Parameter is of type number";
        }
    }

    console.log(msg);
}

ParamTypeFunc("hey hey hey");
ParamTypeFunc(10);
ParamTypeFunc(undefined);

// Function overloading
function ShowAmount(amnt:number):void;
function ShowAmount(amnt:number,qty:number):void;
function ShowAmount(amnt:number,qty:number,available:boolean):void;

function ShowAmount(amnt:any,qty?:any,available?:any):void {
    console.log('amnt: ' + amnt);
    if(qty != null) {
        console.log('qty: '  + qty);
    }
    if(available != null) {
        console.log('available: ' + available);
    }
}

ShowAmount(2);ShowAmount(2,5);ShowAmount(2,null,true);