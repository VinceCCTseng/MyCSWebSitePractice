﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Product_ShoppingCart : System.Web.UI.Page
{
    string _currentShoppingList = "CurShoppingList";
    string _totalCost = "TotalCost";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session[_currentShoppingList] != null)
            {
                BindData();
                CalTotalPrice(this,null);
                TblDetailCost.Visible = true;
                BtnCheckout.Visible = true;
            }
            else
            {
                LiteralMessages.Text = "There is not any item in the cart :(";
                TblDetailCost.Visible = false;
                BtnCheckout.Visible = false;
            }            
        }
        else
        { }
    }
    // remove item and update the session
    public void lkbtnremove_onclick(object sender, EventArgs e)
    {
        try
        {
            // 1. get control - get row and index
            LinkButton myButton = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)myButton.NamingContainer;
            int productIndex = gvr.RowIndex;
            //int productIndex = (gvShopping.PageIndex * gvShopping.PageSize + gvr.RowIndex);

            // 2. get item info
            ourProductlist aproductlist = (ourProductlist)Session[_currentShoppingList];
            ourProduct aproduct = aproductlist.getProductdetail(productIndex);
            //LiteralMessages.Text = "[Debug2][name][" + aproduct.Name + "]"; // ToDO: remove it
            ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:removeStarChangingColour(this);", true);
            // 3. check and add to session

            if (Session[_currentShoppingList] != null)
            {
                ourProductlist ashoppinglist = (ourProductlist)Session[_currentShoppingList];
                ashoppinglist.removeProduct(aproduct);
                if (ashoppinglist.getProductCount() != 0)
                    Session[_currentShoppingList] = ashoppinglist;
                else
                    Session[_currentShoppingList] = null;
                BindData();
                CalTotalPrice(this, null);
                // decrease the badges (14/04/2016)
                Master.UpdateLoginStatus();
                int numberOfGoodInCart = ashoppinglist.getProductCount();
                Session["CurNumberOfItems"] = numberOfGoodInCart;
                Master.UpdateNumberOfItems(numberOfGoodInCart);
            }
            else
            {
                { LiteralMessages.Text = "There is not any item in the cart :("; }
            }
        }
        catch 
        { }
    }
    //
    public void CalTotalPrice(object sender, EventArgs e)
    {
        int numberofProduct = 0, totalcost = 0;
        ourProductlist ashoppinglist = (ourProductlist)Session[_currentShoppingList];
        numberofProduct = ashoppinglist.getProductCount();

        for (int i = 0; i < numberofProduct; i++)
        {
            TextBox tb = (TextBox)gvShopping.Rows[i].Cells[7].FindControl("TextBoxQTY");
            // prevent negative value.
            if (int.Parse(tb.Text) < 1)
                tb.Text = "1";

            totalcost += int.Parse(gvShopping.DataKeys[i].Values["Price"].ToString()) * int.Parse(tb.Text.ToString());
        }
        Session[_totalCost] = totalcost;
        Literal literalTotalPrice = (Literal)gvShopping.FooterRow.FindControl("LiteralTotalPrice");
        literalTotalPrice.Text = "Inc. GST $ " + totalcost.ToString() + " AUD";  
        // II. Show GST 0.909 + shipping +

        // III. Voucher
    }
    //
    protected void gvShopping_QTYUpdated(object sender, EventArgs e)
    {
        CalTotalPrice(this, null);
    }
    // 
    protected void OnClick_Checkout(object sender, EventArgs e)
    {

    }
    // bind fucntion
    public void BindData()
    {
        if (Session[_currentShoppingList] != null)
        {
            ourProductlist aProductlist = (ourProductlist)Session[_currentShoppingList];
            gvShopping.DataSource = aProductlist.getProductlist();
            gvShopping.DataBind();
        }
        else
        {
            //to do clean session
            gvShopping.DataSource = null;
            gvShopping.DataBind();
            Master.UpdateLoginStatus();
            LiteralMessages.Text = "There is not any item in the cart :( ";
        }

    }
}