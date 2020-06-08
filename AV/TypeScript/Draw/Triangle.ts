/******************
 Author:        Duane Billue
 Date:          2020-06-08
 Description:   TypeScript namespace OOP
 ******************/

namespace Drawing {
    export class Triangle implements IShape {
        public draw() {
            console.log("triangle drawn");
        }
    }
}