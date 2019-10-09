//*********************
// Author:          Rogers Cadenhead / Duane Billue
// Date:            2019-10-09
// Description:     Chapter 5 - Creating Classes and Methods
//*********************

package org.avstore.ecommerce;

import java.util.*;

public class StoreFront
{
   private LinkedList catalog = new LinkedList();
   
   public void addItem(String id, String name, String price, String quantity)
   {
       Item item = new Item(id, name, price, quantity);
       catalog.add(item);
   }
   
   public Item getItem(int i)
   {
       return (Item) catalog.get(i);
   } 
   
   public int getSize()
   {
       return catalog.size();
   }
   
   public void sort()
   {
       Collections.sort(catalog);
   }
}