using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// CO is mainly for the orders, which has already done. (retrieve usage)
/// </summary>
public class customerOrder
{
    // internal variables - for 
    private int _orderId;       //              + Not Null
    private string _orderDate;  //              + Not Null
    private string _orderName;  // 50 chars     + Not Null  
    private string _memberID;   // 20 chars 
    private string _phone;      // 10 chars     + Not Null
    private string _address;    // 100 chars    + Not Null
    private int _totalCost;     //              + Not Null
    private int _postFee;       // ""
    private string _postDate;   // Date
    private string _commend;   
    private ourProductlist _ourshoppinglist;

    public customerOrder()
    {
        // Constructor: set up default value
        _orderId = 0;
        _orderDate = "01-01-1900";
        _orderName = "unknown";
        _memberID = null;
        _phone = "0000000000";
        _address = "unknown road";
        _totalCost = 0; _postFee = 0;
        _postDate = null;
        _commend = null;
        _ourshoppinglist = new ourProductlist();
    }

    // 1. add a item from the shopping cart to order
    public Boolean addItemtoOrder(ourProduct aproduct)
    {
        _ourshoppinglist.addProduct(aproduct);
        return true;
    }



}