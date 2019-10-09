//*********************
// Author:          Rogers Cadenhead / Duane Billue
// Date:            2019-10-09
// Description:     Chapter 1 - Getting Started
//*********************

package com.java21days;

public class Investor
{
    public static void main(String[] arguments)
    {
        int investAmount = 14_000;
        double profit = 0;
        double firstYearReturn = 0, secondYearReturn = 0, thirdYearReturn = 0;
        
        profit = .40;
        firstYearReturn = investAmount + (investAmount * profit);
        System.out.println("First Year Return: $" + firstYearReturn + "\n" + "--------");
        
        profit = -1500;
        secondYearReturn = firstYearReturn + profit;
        System.out.println("Second Year Return: $" + secondYearReturn + "\n" + "--------");
        
        profit = .12;
        thirdYearReturn = secondYearReturn + (secondYearReturn * profit);
        System.out.println("Third Year Return: $" + thirdYearReturn);
    } 
}