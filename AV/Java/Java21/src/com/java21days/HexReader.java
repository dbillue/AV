//*********************
// Author:          Rogers Cadenhead / Duane Billue
// Date:            2019-10-09
// Description:     Chapter 7 - Managing Exceptions
//*********************

package com.java21days;

public class HexReader
{
    static String[] inputHeader = { "000A110D1D260219 ",
        "78700F1318141E0C ",
        "6A197D45B0FFFFFF " };
    
    public static void main(String[] arguments)
    {
        //HexReader hex = new HexReader();
        for(int i = 0; i < inputHeader.length; i++)
        {
            readLine(inputHeader[i]);
        }
    }
    
    static void readLine(String code)
    {
        try
        {
            for(int j = 0; j + 1 < code.length(); j += 2)
            {
                String sub = code.substring(j, j + 2);
                int num = Integer.parseInt(sub, 16);
                if(num == 255)
                {
                    return;
                }
                System.out.print(num + " ");
            }
        } finally {
            System.out.println("**");
        }
        return;
    }
}