using MyWebSitePractice1;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Product_Product : System.Web.UI.Page
{
    string _currentProductList = "CurProductList", _currentShoppingList = "CurShoppingList", _currentNumberOfItems = "CurNumberOfItems";
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
            //if (Session.Contents["memberid"] != null)
            //{
                UpdateProductlist();
                BindData();
                
            //}
            //else //enable for non-memger to shopping (21/04/2016)
                //Response.Redirect("~/Default.aspx");
        }
    }
    public void UpdateProductlist()
    {
        try
        {
            sqlAccess sqlgetproductlist = new sqlAccess(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            ourProductlist _productlist = (ourProductlist)sqlgetproductlist.RetrievalProductList();
            Session["CurProductList"] = _productlist; 
        }
        catch
        {
            LiteralDBGMessages.Text = "[Error] happenen at"+ System.Reflection.MethodBase.GetCurrentMethod().Name;//Todo
        }    
    }

    public void BindData()
    {
        if (Session["CurProductList"] != null)
        {
            ourProductlist aProductlist = (ourProductlist)Session["CurProductList"]; 
            gvProduct.DataSource = aProductlist.getProductlist(); 
            gvProduct.DataBind();
        }
        else
        {
            //to do clean session
            Response.Redirect("~/Deafult.aspx");
        }
    }

    protected void gvProduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvProduct.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void lkbtnadd_AddProducttToCart(object sender, EventArgs e)
    {
        try
        {
            // 1. get control - get row and index
            LinkButton myButton = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)myButton.NamingContainer;
            int productIndex = (gvProduct.PageIndex * gvProduct.PageSize + gvr.RowIndex);
            int numberOfGoodInCart = 0;

            // (optional) debug messages ToDo: remove it
            string name = this.gvProduct.DataKeys[gvr.RowIndex].Values["Name"].ToString();            
            //LiteralDBGMessages.Text = "[Debug][" + productIndex.ToString() + "]["+ name + "]"; 

            // 2. get item info
            ourProductlist aproductlist = (ourProductlist)  Session["CurProductList"];
            ourProduct aproduct = aproductlist.getProductdetail(productIndex);
            //LiteralDBGMessages.Text = "[Debug2][name][" + aproduct.Name + "]"; // ToDO: remove it

            // 3. check and add to session
            
            if (Session[_currentShoppingList] != null)
            {
                // check the duplication of the product
                if (checkDuplicateProduct(aproduct))
                {//ture
                    Master.showMessages("Info", "The product already in the Shopping cart.");
                    
                    // [Bug] something in shopping cart, but click duplicated item, the number will show "0";
                    if (Session.Contents[_currentNumberOfItems] != null)
                        numberOfGoodInCart = (int) Session.Contents[_currentNumberOfItems];
                }
                else
                {//flase
                    ourProductlist ashoppinglist = (ourProductlist) Session[_currentShoppingList];
                    ashoppinglist.addProduct(aproduct);
                    Session[_currentShoppingList] = ashoppinglist;
                    numberOfGoodInCart = ashoppinglist.getProductCount();
                }
            }
            else
            {          
                    ourProductlist ashoppinglist = new ourProductlist();
                    ashoppinglist.addProduct(aproduct);
                    Session[_currentShoppingList] = ashoppinglist;
                    numberOfGoodInCart = ashoppinglist.getProductCount();
            }
            Master.UpdateLoginStatus();
            
            // star animation
            ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:addStarChangingColour(this);", true);
            // update the number of items in the shopping cart
            Session[_currentNumberOfItems] = numberOfGoodInCart;
            Master.UpdateNumberOfItems(numberOfGoodInCart);
        }
        catch
        {
            LiteralDBGMessages.Text = "[Error] get row data fail.";
        }
    }

    public void lbtnProduct_OnClick(object sender, EventArgs e)
    {
        try
        {
            // 1. get control - get row and index
            LinkButton myButton = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)myButton.NamingContainer;
            int productIndex = (gvProduct.PageIndex * gvProduct.PageSize + gvr.RowIndex);

            // 2. get item info
            ourProductlist curproductlist = (ourProductlist)Session["CurProductList"];
            ourProduct aproduct = curproductlist.getProductdetail(productIndex);
            var aproductlist = new List<ourProduct>();
            aproductlist.Add(aproduct);

            // 3. detail view
            dvProduct.DataSource = aproductlist;            
            dvProduct.DataBind();

        }
        catch
        {

        }
    }
    private bool checkDuplicateProduct(ourProduct aproduct)
    {
        int numProductInKart = 0;
        bool duplicationFlag = false;
        ourProductlist aproductlist = (ourProductlist) Session[_currentShoppingList];
        numProductInKart = aproductlist.getProductCount();
        
        for (int i=0; i< numProductInKart;i++)
        {
            ourProduct listproduct = aproductlist.getProductdetail(i);
            if (listproduct.Id.CompareTo(aproduct.Id) == 0)
                duplicationFlag = true;            
        }
        return duplicationFlag;
    }
}