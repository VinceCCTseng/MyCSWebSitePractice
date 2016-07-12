using MyWebSitePractice1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session.Contents["memberid"] != null)
                LiteralMemberID.Text = Session.Contents["cusname"].ToString();
            else
                Response.Redirect("~/Default.aspx");
        }
        catch
        {

        }
    }

    private void updatecustomerdatagrid(object sender, EventArgs e)
    {        
    }
}