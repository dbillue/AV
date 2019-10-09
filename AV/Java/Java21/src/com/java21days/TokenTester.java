//*********************
// Author:          Rogers Cadenhead / Duane Billue
// Date:            2019-10-09
// Description:     Chapter 1 - Getting Started
//*********************

package com.java21days;

import java.util.StringTokenizer;

public class TokenTester
{
    public static void main(String[] arguments)
    {
        StringTokenizer st1 = null, st2 = null;
        
        String quote1 = "539.00 GOOG -9.78";
        st1 = new StringTokenizer(quote1);
        System.out.println("Token 1: " + st1.nextToken());
        System.out.println("Token 1: " + st1.nextToken());
        System.out.println("Token 1: " + st1.nextToken());
        
        String quote2 = "139.00@MSFT@5.25";
        st2 = new StringTokenizer(quote2, "@");
        System.out.println("\nToken 1: " + st2.nextToken());
        System.out.println("Token 1: " + st2.nextToken());
        System.out.println("Token 1: " + st2.nextToken());
    }
}