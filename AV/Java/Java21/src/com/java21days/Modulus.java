//*********************
// Author:          Rogers Cadenhead / Duane Billue
// Date:            2019-10-09
// Description:     Chapter 1 - Getting Started
//*********************

package com.java21days;

public class Modulus
{
    public static void main(String[] arguments)
    {
       int value1 = 10_000, value2 = 12;
       double finalValue = 0, remainder = 0;
       
       finalValue = value1 / value2;
       remainder = value1 % value2;
       
       System.out.println("Final Values: " + finalValue + "\t" + remainder);
    }
}