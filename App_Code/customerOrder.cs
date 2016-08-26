using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// CO is mainly for the orders, which has already done. (retrieve usage)
/// </summary>
public class CustomerOrder 
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

    public CustomerOrder()
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

    public Boolean removeItemtoOrder(ourProduct aproduct)
    {
        _ourshoppinglist.removeProduct(aproduct);
        return true;
    }

    public string OrderDate  //              + Not Null
    {
        get
        { return _orderDate; }
        set
        { _orderDate = value; }
    }

    public string OrderName// 50 chars     + Not Null  
    {
        get { return _orderName; }
        set {
            if(value.Length <=50 && value !=null)
            _orderName = value;
        }
    }
    public string MemberID   // 20 chars 
    {
        get { return _memberID; }
        set { } // ToDo
    }

    public string Phone// 10 chars     + Not Null
    {
        get { return _phone; }
        set {
            if (value.Length == 10 && value != null)
                _phone = value;
        }
    }

    public string Address    // 100 chars    + Not Null
    {
        get { return _address; }
        set {
            if (value.Length <= 100 && value != null)
                _address = value;
        }
    }
    
    public int TotalCost     //              + Not Null
    {
        get { return _totalCost; }
        set {
            if (value >0 && value<65535)
                _totalCost = value;
        }
    }

    public int PostFee       // ""
    {
        get { return _postFee; }
        set {
            if (value > 0 && value < 65535)
                _postFee = value;
            }
    }

    public string PostDate   // Date
    {
        get { return _postDate; }
        set {
            if (value != null)
                _postDate = value;
            }
    }
    public string Commend
    {
        get { return _commend; }
        set {
            if (value != null)
                _commend = value;
            }
    }
}