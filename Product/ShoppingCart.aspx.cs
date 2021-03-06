﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
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
                CalTotalPrice(this, null);
                TblDetailCost.Visible = true;
                BtnCheckout.Visible = true;
                for (int i = 0; i < 12; i++)
                {
                    DropDownListYear.Items.Insert(i, new ListItem((2016 + i).ToString(), (2016 + i).ToString()));
                    DropDownListMonth.Items.Insert(i, new ListItem((1 + i).ToString(), (1 + i).ToString()));
                }
            }
            else
            {
                LiteralMessages.Text = "There is not any item in the cart :(";
                TblDetailCost.Visible = false;
                BtnCheckout.Visible = false;
            }            
        }

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
        int numberofProduct = 0;
        double totalcost = 0;
        ourProductlist ashoppinglist = (ourProductlist)Session[_currentShoppingList];
        numberofProduct = ashoppinglist.getProductCount();

        for (int i = 0; i < numberofProduct; i++)
        {
            TextBox tb = (TextBox)gvShopping.Rows[i].Cells[7].FindControl("TextBoxQTY");
            // prevent negative value.
            if (int.Parse(tb.Text) < 1)
                tb.Text = "1";
            // 19082016 update the qty
            ashoppinglist.setQty(i, int.Parse(tb.Text));
            totalcost += double.Parse(gvShopping.DataKeys[i].Values["Price"].ToString()) * int.Parse(tb.Text.ToString());
        }
        Session[_totalCost] = totalcost;
        Label LblTotalPrice = (Label)gvShopping.FooterRow.FindControl("LblTotalPrice");
        LblTotalPrice.Text =  totalcost.ToString();

        // II. Show GST 0.909 + shipping +
        LbGST.Text = (totalcost * 0.091).ToString();
        LbExGST.Text = (totalcost * 0.909).ToString();
        LbDeliFee.Text = "20.00";
        LbGrandTotal.Text = (totalcost + double.Parse(LbDeliFee.Text)).ToString();

        // III. update the currentShoppingList
        Session[_currentShoppingList] = ashoppinglist;
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
            //to do clean session // fix the problem when all item remove the table, checkout button still exist 
            gvShopping.DataSource = null;
            gvShopping.DataBind();
            Master.UpdateLoginStatus();
            LiteralMessages.Text = "There is not any item in the cart :( ";
            TblDetailCost.Visible = false;
            BtnCheckout.Visible = false;
        }

    }

    public void startPurchase(object sender, EventArgs e)
    {
        int errorcode = 0;
        //data vaildataion
        //1. name
        if (Regex.IsMatch(holderName.Text, @"^[a-zA-Z\s]+$") && holderName.Text.Length > 5 && holderName.Text.Length < 100)
        { }
        else
        {
            errorcode = 1;
            goto ErrorProgress;
        }
        //2. card
        if (Regex.IsMatch(cardNumber.Text, @"^[0-9]+$") && cardNumber.Text.Length == 16)
        { }
        else
        {
            errorcode = 2;
            goto ErrorProgress;
        }
        //3. CVV
        if (Regex.IsMatch(cVVNumber.Text, @"^[0-9]+$") && cVVNumber.Text.Length == 3)
        { }
        else
        {
            errorcode = 4;
            goto ErrorProgress;
        }
        // Step 2: try to auth
        errorcode = 0;//mockApiConnectAndPay(RBLPaymenttype.SelectedValue.ToString(), holderName.Text, cardNumber.Text, cVVNumber.Text, DropDownListMonth.Text, DropDownListYear.Text, double.Parse(LbGrandTotal.Text));

        // Step 3:Redirection to complete page without error code, otherwise go back and show error.
        if (errorcode == 0)
        {
            int oId = -1, err = 0;
            CustomerOrder customerOrder = new CustomerOrder();
            DateTime today = DateTime.Today;
            ourProductlist ashoppinglist = (ourProductlist)Session[_currentShoppingList];
            // Update into Database
            //1. insert into order 19082016
            customerOrder.OrderDate = today.Year.ToString()+"-"+ today.Month.ToString()+"-"+ today.Day.ToString();
            customerOrder.OrderName = TextBoxCustomerName.Text.ToString();
            customerOrder.Phone = TextBoxPhone.Text.ToString();
            customerOrder.Address = TextBoxAddress.Text.ToString();
            customerOrder.TotalCost = double.Parse(LbGrandTotal.Text.ToString());
            customerOrder.PostFee = double.Parse(LbDeliFee.Text.ToString());

            sqlAccess sqlInsertOrder = new sqlAccess(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            try
            {
                //accsql
                oId = sqlInsertOrder.InsertAnOrder(customerOrder.OrderDate, customerOrder.OrderName, customerOrder.Phone, customerOrder.Address, customerOrder.PostFee, customerOrder.TotalCost);
            }
            catch
            {
               //errorhandeler
            }
            //2. check the order id 
            for (int i = 0; i < ashoppinglist.getProductCount(); i++)
            {
                try
                {
                    //ourProduct tempproduct = (ourProduct) ashoppinglist.getProductdetail(i);
                    sqlInsertOrder.InsertAProOfAnOrd(oId, ashoppinglist.getProductdetail(i).Id, ashoppinglist.getProductdetail(i).Qty);
                }
                catch
                { }
            }
            if(err==0)
            {
                ashoppinglist = null;
                Session[_currentShoppingList] = ashoppinglist;
                Master.showMessages("Message", "The process has been done! :)");
                //Response.Redirect("~", true);
            }                                   
        }
        // exception
        ErrorProgress:
        // 1: name, 2: card number, 3: 
        if(errorcode != 0)
            Master.showMessages("Error Message", "Error code:\t"+ errorcode);
    }
    public int mockApiConnectAndPay(string _type,string _name, string _cardnumber, string _cvv, string _month, string _year, double _totalfee)
    {
        int retCode = 0;
        // 8： card info error, 16: purchase deny, 32: transaction fail
        //---------------------hard code test
        int[] sampleerrorcode = new int[] {0, 8, 16, 32};
        Random rnd = new Random();
        retCode = sampleerrorcode[rnd.Next(0, 3)];
        //---------------------
        return retCode;
    }
}