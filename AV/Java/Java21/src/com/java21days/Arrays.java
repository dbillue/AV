//*********************
// Author:          Rogers Cadenhead / Duane Billue
// Date:            2019-10-09
// Description:     Chapter 4 - List, Logic and 
//*********************

package com.java21days;

public class Arrays
{
    public static void main(String[] arguments)
    {
        int[] denver = { 1_700_000, 4_600_000, 2_100_000 };
        int[] philadelphia = new int[denver.length];
        int[] total = new int[denver.length];
        int average = 0;
        
        // System.out.println("philadelphia: " + philadelphia.length);
        // System.out.println("denver: " + denver.length);
        
        philadelphia[0] = 1_800_000;
        philadelphia[1] = 5_000_000;
        philadelphia[2] = 2_500_000;
        
        total[0] = denver[0] + philadelphia[0];
        total[1] = denver[1] + philadelphia[1];
        total[2] = denver[2] + philadelphia[2];
        average = (total[0] + total[1] + total[2]) / 3;
        
        System.out.println("2012 Amnt: ");
        System.out.format("$%,d%n", total[0]);
        
        System.out.println("2013 Amnt: ");
        System.out.format("$%,d%n", total[1]);
        
        System.out.println("2014 Amnt: ");
        System.out.format("$%,d%n", total[2]);
        
        System.out.println("Average: ");
        System.out.format("$%,d%n", average);
    }
}