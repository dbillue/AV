//*********************
// Author:          Rogers Cadenhead / Duane Billue
// Date:            2019-10-25
// Description:     Chapter 8 - Data Structure - BitSet
//*********************
package com.java21days;

import java.util.*;

public class HolidaySked
{
    BitSet sked;
    
    public HolidaySked()
    {
        sked = new BitSet(365);
        int[] holiday = { 1, 15, 50, 145, 185, 246, 281, 316, 326, 359 };
        for (int i = 0; i < holiday.length; i++)
        {
            addHoliday(holiday[i]);
        }
    }
    
    public void addHoliday(int dayToAdd)
    {
        sked.set(dayToAdd);
    }
    
    public boolean isHoliday(int dayToCheck)
    {
        boolean result = sked.get(dayToCheck);
        return result;
    }
    
    public static void main(String[] arguments)
    {
        HolidaySked cal = new HolidaySked();
        if (arguments.length > 0)
        {
            try
            {
                int whichDay = Integer.parseInt(arguments[0]);
                if (cal.isHoliday(whichDay))
                {
                    System.out.println("Day Num " + whichDay + " is a holiday");
                } else {
                    System.out.println("Day Num " + whichDay + " is not a holiday");
                }
            } catch (NumberFormatException nfe) {
                System.out.println("Error: " + nfe.getMessage());
            }
        } 
    }
}