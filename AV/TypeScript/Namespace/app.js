/******************
 Author:        Duane Billue
 Date:          2020-06-08
 Description:   TypeScript namespace OOP
 ******************/
/******************
 Author:        Duane Billue
 Date:          2020-06-08
 Description:   TypeScript namespace OOP
 ******************/
/// <reference path="IShape.ts" />
var Drawing;
(function (Drawing) {
    var Circle = /** @class */ (function () {
        function Circle() {
        }
        Circle.prototype.drawing = function () {
            console.log("circle drawn");
        };
        return Circle;
    }());
    Drawing.Circle = Circle;
})(Drawing || (Drawing = {}));
/******************
 Author:        Duane Billue
 Date:          2020-06-08
 Description:   TypeScript namespace OOP
 ******************/
/// <reference path="IShape.ts" />
var Drawing;
(function (Drawing) {
    var Triangle = /** @class */ (function () {
        function Triangle() {
        }
        Triangle.prototype.drawing = function () {
            console.log("triangle drawn");
        };
        return Triangle;
    }());
    Drawing.Triangle = Triangle;
})(Drawing || (Drawing = {}));
/******************
 Author:        Duane Billue
 Date:          2020-06-08
 Description:   TypeScript namespace OOP
 ******************/
/// <reference path="IShape.ts" />
/// <reference path="Circle.ts" />
/// <reference path="Triangle.ts" />
function drawAllShapes(shape) {
    shape.drawing();
}
drawAllShapes(new Drawing.Circle());
drawAllShapes(new Drawing.Triangle());
