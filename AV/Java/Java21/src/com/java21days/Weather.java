//*********************
// Author:          Rogers Cadenhead / Duane Billue
// Date:            2019-10-09
// Description:     Chapter 1 - Getting Started
//*********************

package com.java21days;

public class Weather
{
    public static void main(String[] arguments)
    {
        float fah = 86;
        System.out.println("fah: " + fah);
        
        // Convert to celsius
        fah = fah - 32;
        fah = fah / 9;
        fah = fah * 5;
        
        System.out.println("cel: " + fah);
    }
}