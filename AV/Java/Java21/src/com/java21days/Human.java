//*********************
// Author:          Rogers Cadenhead / Duane Billue
// Date:            2019-10-09
// Description:     Chapter 5 - Creating Classes and Methods
//*********************

package com.java21days;

public class Human
{
    public static void main(String[] arguments)
    {
        Person helper1 = new Person();
        
        helper1.age = 44;
        helper1.height = 5.9;
        helper1.weight = 180;
        
        System.out.println("Age: " + helper1.age + 
                "\nHeight: " + helper1.height +
                "\nWeight: " + helper1.weight);
    }
}