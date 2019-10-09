//*********************
// Author:          Rogers Cadenhead / Duane Billue
// Date:            2019-10-09
// Description:     Chapter 1 - Getting Started
//*********************

package com.java21days;

public class Variables
{
    public static void main(String[] arguments)
    {
        final char UP = 'U';
        byte initiateLevel = 12;
        short location = 13_250;
        int score = 10_000;
        boolean newGame = false;
        
        System.out.println("Level: " + initiateLevel);
        System.out.println("Score: " + score);
        System.out.println("Up: " + UP);
    }
}