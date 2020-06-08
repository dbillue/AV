/******************
 Author:        Duane Billue
 Date:          2020-06-08
 Description:   TypeScript namespace OOP
 ******************/

/// <reference path="IShape.ts" />
namespace Drawing {
    export class Triangle implements IShape {
        public drawing() {
            console.log("triangle drawn");
        }
    }
}