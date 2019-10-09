//*********************
// Author:          Rogers Cadenhead / Duane Billue
// Date:            2019-10-09
// Description:     Chapter 5 - Creating Classes and Methods
//*********************

package com.java21days;

public class InstanceCounter
{
    private static int numInstances = 0;
    
    // CTOR
    InstanceCounter()
    {
        InstanceCounter.addInstance();
    }
    
    protected static int getInstances()
    {
        return numInstances;
    }
    
    protected static void addInstance()
    {
        numInstances++;
    }
    
    // Main ::-)
    public static void main(String[] arguments)
    {
        System.out.println("Starting with: " +
                InstanceCounter.getInstances() + " objects");
        
        for(int i = 0; i < 500; i++)
        {
            InstanceCounter.addInstance();
        }
        
        System.out.println("New number of instances: " +
                InstanceCounter.getInstances());
    }
}