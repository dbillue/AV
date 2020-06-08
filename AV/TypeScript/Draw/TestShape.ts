/******************
 Author:        Duane Billue
 Date:          2020-06-08
 Description:   TypeScript namespace OOP
 ******************/

function drawAllShapes(shape:Drawing.IShape) {
    shape.draw();
}

drawAllShapes(new Drawing.Circle());
drawAllShapes(new Drawing.Triangle());