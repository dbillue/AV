//*********************
// Author:          Rogers Cadenhead / Duane Billue
// Date:            2019-10-09
// Description:     Chapter 5 - Creating Classes and Methods
//*********************

package org.avstore.ecommerce;

public class GiftShop
{
    public static void main(String[] arguments)
    {
        StoreFront storeFront = new StoreFront();
        storeFront.addItem("AV01", "Mug", "5.00", "10");
        storeFront.addItem("AV02", "Magnet", "4.00", "5");
        storeFront.addItem("AV03", "Necklace", "155.00", "10");
        storeFront.addItem("AV04", "Brush", "7.00", "25");
        storeFront.sort();
        
        for(int i = 0; i < storeFront.getSize(); i++)
        {
            Item showItem = (Item) storeFront.getItem(i);
            
            System.out.println("\nItem: " + showItem.getId() + 
                    "\nName: " + showItem.getName() +
                    "\nRetail Price: $" + showItem.getRetail() +
                    "\nPrice: $" + showItem.getPrice() +
                    "\nQty: " + showItem.getQuantity());
        }
    }
}