using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

/// <summary>
/// Version 2.0 add qty for the shopping cart (21042016)
/// Part 1 - ourProduct is for each product, including each detail
/// Part 2 - ourProductlist is a container to retrival whole products into it.
/// Part 3 - Order ...
/// product_ID int
/// product_SerialNumber 50
/// product_Name 50
/// product_Price int
/// product_Imagelink 100 Null
/// product_category 25
/// product_color 25 NULL
/// product_commend 100 NULL
/// </summary>
//
public class ourProduct
{
    private int _id, _qty; //int
    private string _serialNumber; //50
    private string _name; //50
    private double _price;
    private string _imagelink; //100 Null
    private string _category; //25
    private string _color; //25 NULL
    private string _comment; //100 NULL

    //Constructure without ID
    public ourProduct()
    {
        _id = 0; _qty=0;
        _serialNumber = "0";
        _name = "0";
        _price = 0.0;
        _imagelink = null;
        _category = "0";
        _color = null;
        _comment = null;
    }
    //Constructure with ID
    public ourProduct(int id)
    {
        _id = id;        
    }

    public int Id
    {
        get
        {
            return _id;
        }

    }
    //19082016 add for the order function
    public int Qty
    {
        get
        {
            return _qty;
        }
        set
        {
            _qty = value;
        }
    }
    public string SerialNumber
    {
        get
        { return _serialNumber; }
        set
        {
            if (!string.IsNullOrEmpty(value) && value.Length <= 50)
                { _serialNumber = value; }
        }
    }

    public string Name
    {
        get
        { return _name; }
        set
        {
            if (!string.IsNullOrEmpty(value) && value.Length <= 50)
                { _name = value; }
        }
    }

    public double Price
    {
        get
        { return _price; }
        set
        {
            if (value > 0.0 && value < 99999.9) // to do
                { _price = value; }
        }

    }

    public string Imagelink
    {
        get
        { return _imagelink; }
        set
        {
            if (!string.IsNullOrEmpty(value) && value.Length <= 50)
            { _imagelink = value; }
        }
    }

    public string Category
    {
        get
        { return _category; }
        set
        {
            if (!string.IsNullOrEmpty(value) && value.Length <= 50)
            { _category = value; }
        }
    }

    public string Color
    {
        get
        { return _color; }
        set
        {
            if (!string.IsNullOrEmpty(value) && value.Length <= 50)
            { _color = value; }
        }
    }

    public string Comment
    {
        get
        { return _comment; }
        set
        {
            if (!string.IsNullOrEmpty(value) && value.Length <= 50)
            { _comment = value; }
        }
    }
    // Todo Insert
    // Todo Update
}

public class ourProductlist :ourProduct
{
    private BindingList<ourProduct> _ourProductlist;
    //constructer

    public ourProductlist()
    {
        _ourProductlist = new BindingList<ourProduct>();
    }

    public Boolean addProduct(ourProduct aProduct)
    {
        _ourProductlist.Add(aProduct);
        return true;
    }

    public Boolean removeProduct(ourProduct aProduct)
    {
        _ourProductlist.Remove(aProduct);
        return true;
    }

    public ourProduct getProductdetail(int index)
    {
        return _ourProductlist[index];
    }

    public BindingList<ourProduct> getProductlist()
    { return _ourProductlist; }

    public int getProductCount()
    {
        return _ourProductlist.Count;
    }

    public void setQty(int _index,int  _qty)
    {
        _ourProductlist[_index].Qty = _qty;
    }
}
