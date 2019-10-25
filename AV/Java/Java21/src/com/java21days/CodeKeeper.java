//*********************
// Author:          Rogers Cadenhead / Duane Billue
// Date:            2019-10-25
// Description:     Chapter 8 - Data Structures - Iterator using array list
//*********************
package com.java21days;

import java.util.*;

public class CodeKeeper
{
    ArrayList list;
    String[] codes = { "alpha", "lambda", "gamma", "delta", "zeta" };
    
    public CodeKeeper(String[] usercodes)
    {
        list = new ArrayList();
        for(int i = 0; i < codes.length; i++)
        {
            addCode(codes[i]);
        }
        
        for(int i = 0; i < usercodes.length; i++)
        {
            addCode(usercodes[i]);
        }
        
        for(Iterator ite = list.iterator(); ite.hasNext();)
        {
            String output = (String) ite.next();
            System.out.println(output);
        }
    }
    
    private void addCode(String code)
    {
        if(!list.contains(code))
        {
            list.add(code);
        }
    }
    
    public static void main(String[] arguments)
    {
        CodeKeeper keeper = new CodeKeeper(arguments);
    }
}