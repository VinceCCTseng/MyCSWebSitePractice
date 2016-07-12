using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyWebSitePractice1;

public partial class Default2 : System.Web.UI.Page
{
    ourCustomerList _customerlist;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session.Contents["memberid"] != null)
        {
            if (Session.Contents["mode"].ToString().CompareTo("admin") == 0)
                Response.Redirect("~/AdminPage.aspx");
            else
                Response.Redirect("~/UserPage.aspx");                
        }
        else
        {            
            LiteralMessages.Text = "Please, log in and continues.";
        }
    }
}