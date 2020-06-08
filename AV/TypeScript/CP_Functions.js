/******************
 Author:        Duane Billue
 Date:          2020-06-05
 Description:   Basic TypeScript functions and arrays
 ******************/
// Function with optional parameter
function DisplayUserNames(id, userName, enabled) {
    if (id == null && userName == null) {
        console.log("Please enter the userId and userName.");
    }
    // String interpolation (use tick marks!!!)
    console.log("userId: " + id + "\n" + ("userName: " + userName));
}
DisplayUserNames(1, 'asmith');
// Function with Rest
function restFunct() {
    var cnt = [];
    for (var _i = 0; _i < arguments.length; _i++) {
        cnt[_i] = arguments[_i];
    }
    var i;
    var sum = 0;
    for (i = 0; i < cnt.length; i++) {
        sum = sum + cnt[i];
    }
    console.log("Total sum of rest parameter: " + sum);
}
restFunct(1, 2, 3);
// Function with default parameters
function calcPrice(price, qty) {
    if (qty === void 0) { qty = 5; }
    if (price == null || qty == null) {
        console.log("Please enter values to calculate price.");
    }
    console.log("Total cost for " + qty + " items: " + price * qty);
}
calcPrice(50.25);
calcPrice(50.25, 50000);
// Anonymous function
var ReservationsCount = function (persons) {
    return persons.length;
};
var reservedPeople = ["Dave", "Jason", "Jaimie", "Addison"];
console.log("Number of reservations: " + ReservationsCount(reservedPeople));
// Simple Lambda
var StreetAddress = function (houseNumber, streetName) {
    var streetAddress = houseNumber + ' ' + streetName;
    console.log("Street address: " + streetAddress);
};
StreetAddress(1058, 'Gilbert St');
// Parameter type inference
var ParamTypeFunc = function (param1) {
    var msg;
    if (param1 == null) {
        msg = "Parameter is empty :-<>";
    }
    if (typeof param1 == "string") {
        msg = "Parameter is of type string";
    }
    else {
        if (typeof param1 == "number") {
            msg = "Parameter is of type number";
        }
    }
    console.log(msg);
};
ParamTypeFunc("hey hey hey");
ParamTypeFunc(10);
ParamTypeFunc(undefined);
function ShowAmount(amnt, qty, available) {
    console.log('amnt: ' + amnt);
    if (qty != null) {
        console.log('qty: ' + qty);
    }
    if (available != null) {
        console.log('available: ' + available);
    }
}
ShowAmount(2);
ShowAmount(2, 5);
ShowAmount(2, null, true);
// Arrays (for good measure) <*><*>
var container;
var containers = ['Rail Car', 'Truck Trailer', 'Truck Trailer Two Way', 'Airplane', 'Ship'];
for (container in containers) {
    console.log('-' + containers[container]);
}
var icnt;
for (icnt = 0; icnt <= containers.length - 1; icnt++) {
    console.log('<<>>' + containers[icnt]);
}
