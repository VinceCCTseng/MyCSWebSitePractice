using MyWebSitePractice1;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RecorderViewer : System.Web.UI.Page
{
    string _memberid;
    ourCustomerList _aSearchList;

    protected void Page_Load(object sender, EventArgs e)
    {
        //targetid
        try
        {
            _memberid = Session.Contents["targetid"].ToString();            
            LiteralMemberId.Text = _memberid;
            updatedetail();
        }
        catch
        {
            Response.Redirect("~/Default.aspx");
        }
    }

    protected void updatedetail()
    {
        try
        {
            sqlAccess sqlgetadetail = new sqlAccess(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            _aSearchList = sqlgetadetail.SearchRecord(sqlAccess.MEMBERCARD, _memberid);
            gvRecord.DataSource = _aSearchList.Getcustomerlist();
            gvRecord.DataBind();
        }
        catch
        {
            //Todo
        }
    }
}