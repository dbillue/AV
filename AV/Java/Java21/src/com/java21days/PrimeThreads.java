//*********************
// Author:          Rogers Cadenhead / Duane Billue
// Date:            2019-10-09
// Description:     Chapter 7 - Threads and Exceptions
//*********************

package com.java21days;

public class PrimeThreads
{  
    public static void main(String[] arguments)
    {
       PrimeThreads pt = new PrimeThreads(arguments); 
    }
    
    public PrimeThreads(String[] arguments)
    {
        try
        {
            PrimeFinder[] finder = new PrimeFinder[arguments.length];
            for(int i = 0; i < arguments.length; i++)
            {
                try
                {
                    long count = Long.parseLong(arguments[i]);
                    finder[i] = new PrimeFinder(count);
                    System.out.println("Looking for prime: " + count);
                } catch (NumberFormatException nfe) {
                    System.out.println("Error: " + nfe.getMessage());
                }
            }

            boolean complete = false;
            while(!complete)
            {
                complete = true;
                for(int j = 0; j < finder.length; j++)
                {
                    if(finder[j] == null) continue;
                    if(finder[j].finished) 
                    { 
                        complete = false;
                    } else {
                        displayResult(finder[j]);
                        finder[j] = null;
                    }

                }
            }
        } catch (Exception ex) {
            System.out.println(ex.getMessage());
        }
    }
    
    private void displayResult(PrimeFinder finder)
    {
        System.out.println("Prime: " + finder.target + " is " + finder.prime);
    }
}