//*********************
// Author:          Rogers Cadenhead / Duane Billue
// Date:            2019-10-09
// Description:     Chapter 1 - Getting Started
//*********************

package com.java21days;

import java.awt.Point;

public class PointSetter
{
    public static void main(String[] arguments)
    {
        Point location = new Point(4, 50);
        System.out.println("X: " + location.x);
        System.out.println("Y: " + location.y);
        
        location.x = 400; location.y = 500;
        System.out.println("\nX: " + location.x);
        System.out.println("Y: " + location.y);   
    }
}