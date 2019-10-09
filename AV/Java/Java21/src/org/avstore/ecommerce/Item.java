//*********************
// Author:          Rogers Cadenhead / Duane Billue
// Date:            2019-10-09
// Description:     Chapter 5 - Creating Classes and Methods
//*********************

package org.avstore.ecommerce;

public class Item implements Comparable
{
    private String _id;
    private String _name;
    private double _retail;
    private int _quantity;
    private double _price;
    
    // CTOR.
    Item(String id, String name, String retail, String quantity)
    {
        _id = id; 
        _name = name; 
        _retail = Double.parseDouble(retail);
        _quantity = Integer.parseInt(quantity);
        
        if(_quantity > 400)
        {
            _price = _retail * .5D;
        } else if(_quantity > 200) {
            _price = _retail * .6D;
        } else {
            _price = _retail * .7D;
            _price = Math.floor(_price * 100 + .5) / 100;
        }
    }
    
    public int compareTo(Object obj)
    {
        Item temp = (Item) obj;
        if(this._price < temp._price)
        {
            return 1;
        } else if (this._price > temp._price) {
            return -1;
        }
        return 0;
    }
    
    public String getId()
    {
        return _id;
    }
    
    public String getName()
    {
        return _name;
    }
    
    public double getRetail()
    {
        return _retail;
    }
    
    public int getQuantity()
    {
        return _quantity;
    }
    
    public double getPrice()
    {
        return _price;
    }
}