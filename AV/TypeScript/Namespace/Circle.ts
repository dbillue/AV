/******************
 Author:        Duane Billue
 Date:          2020-06-08
 Description:   TypeScript namespace OOP
 ******************/

/// <reference path="IShape.ts" />
namespace Drawing {
    export class Circle implements IShape {
        drawing() {
            console.log("circle drawn");
        }
    }
}