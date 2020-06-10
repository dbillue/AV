/******************
 Author:        Duane Billue
 Date:          2020-06-08
 Description:   TypeScript namespace OOP
 ******************/

/// <reference path="IShape.ts" />
/// <reference path="Circle.ts" />
/// <reference path="Triangle.ts" />
function drawAllShapes(shape:Drawing.IShape) {
    shape.drawing();
}

drawAllShapes(new Drawing.Circle());
drawAllShapes(new Drawing.Triangle());