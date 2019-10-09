//*********************
// Author:          Rogers Cadenhead / Duane Billue
// Date:            2019-10-09
// Description:     Chapter 5 - Creating Classes and Methods
//*********************

package com.java21days;

public class ApplicationParams
{
    public static void main(String[] arguments)
    {
       String out = "";
       
       if(arguments.length == 0)
       {
           System.out.println("Bummer, no arguments passed to application :(");
       }
       
       for(int i = 0; i <= arguments.length - 1; i++)
       {
           out = "Argument " + i + ": " + arguments[i];
           System.out.println(out);
       }
    }
}