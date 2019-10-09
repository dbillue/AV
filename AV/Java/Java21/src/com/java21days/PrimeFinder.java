//*********************
// Author:          Rogers Cadenhead / Duane Billue
// Date:            2019-10-09
// Description:     Chapter 7 - Threads and Exceptions
//*********************

package com.java21days;

public class PrimeFinder implements Runnable
{
    public long target;
    public long prime;
    public boolean finished = false;
    private Thread runner;
    
    PrimeFinder(long inTarget) throws NumberFormatException
    {
        // Check for non-negative number.
        if (inTarget < 0) 
        { 
            throw new NumberFormatException("Negative number(s) not supported."); 
        } 
        
        target = inTarget;
        if(runner == null) { runner = new Thread(this); runner.start(); }
    }
    
    public void run()
    {
        long numPrimes = 0, candidate = 2;
        
        while(numPrimes < target)
        {
            if(isPrime(candidate))
            {
                numPrimes++;
                prime = candidate;
            }
            candidate++;
        }
        finished = true;
    }
    
    boolean isPrime(long checkNumber)
    {
        double root = Math.sqrt(checkNumber);
        for(int i = 2; i <= root; i++)
        {
          if(checkNumber % i == 0)
          {
              return false;
          }
        }
        return true;
    }
}