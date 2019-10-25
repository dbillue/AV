//*********************
// Author:          Rogers Cadenhead / Duane Billue
// Date:            2019-10-25
// Description:     Chapter 8 - Data Structures - H A S H map
//*********************
package com.java21days;

import java.util.*;

public class ComicBooks
{
    // CTOR
    public ComicBooks()
    {}
    
    public static void main(String[] arguments)
    {
        HashMap quality = new HashMap();
        float price1 = 3.00F;
        quality.put("Mint", price1);
        float price2 = 2.00F;
        quality.put("Near Mint", price2);
        float price3 = 1.50F;
        quality.put("Very Fine", price3);
        float price4 = 1.00F;
        quality.put("Fine", price4);
        float price5 = 0.50F;
        quality.put("Good", price5);
        float price6 = 0.25F;
        quality.put("Poor", price6);
        Comic[] comix = new Comic[1];
        comix[0] = new Comic("Spider Man", "Mint", 25, 25);
        comix[0].setPrice(25);
        for(int i = 0; i < comix.length; i++)
        {
            System.out.println("Title: " + comix[0].title);
            System.out.println("Condition: " + comix[0].condition);
            System.out.println("Price: " + comix[0].price);
        }
    }
}

class Comic
{
    String title;
    String condition;
    float basePrice;
    float price;
    
    public Comic(String _title, String _condition, float _basePrice, float _price)
    {
      title = _title;
      condition = _condition;
      basePrice = _basePrice;
      price = _price;
    }
    
    void setPrice(float factor)
    {
        price = basePrice * factor;
    }
}
     