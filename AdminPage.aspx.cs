using MyWebSitePractice1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminPage : System.Web.UI.Page
{
    ourCustomerList _customerlist;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session.Contents["memberid"] != null)
        {
            UpdatePanelData.Visible = true;
            //call to update function
            UpdateUserGrid();
        }
        else
        {
            UpdatePanelData.Visible = false;
            LiteralMessages.Text = "Please, log in and continues.";
        }
    }

    protected void BtnRefreshUser_Click(object sender, EventArgs e)
    {
        UpdateUserGrid();
    }
    protected void BtnRefreshOrder_Click(object sender, EventArgs e)
    {
    }
    protected void UpdateUserGrid()
    {
        if (Session.Contents["mode"] != null)
        {
            if (Session.Contents["mode"].ToString().CompareTo("admin") == 0)
            {
                try
                {
                    sqlAccess sqlcustomerlist = new sqlAccess();
                    _customerlist = sqlcustomerlist.RetrievalCustomerList();
                    //this.GridViewData.AutoGenerateColumns = false;
                    GridViewUser.DataSource = _customerlist.Getcustomerlist();
                    GridViewUser.DataBind();
                }
                catch
                {
                    //to do error
                }
            }
            else if ((Session.Contents["mode"].ToString().CompareTo("user") == 0))
            {
                Response.Redirect("~/UserPage.aspx");
            }
            else // to do error 
            { }
        }
        else
        {
            Session.RemoveAll();
            Response.Redirect("~/Default.aspx");
        }
    }


    protected void GridViewData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.style.fontWeight = 'bold' ; this.style.color = 'red'  ;");
            e.Row.Attributes.Add("onmouseout", "this.style.fontWeight = 'normal' ; this.style.color = 'black'  ;");
            //test
            LinkButton BtnDbClick = (LinkButton)e.Row.Cells[0].Controls[0];
            string _jsSingle = ClientScript.GetPostBackClientHyperlink(BtnDbClick, "Select$" + e.Row.RowIndex);
            e.Row.Style["cursor"] = "hand";
            e.Row.Attributes["ondblclick"] = _jsSingle;
        }
    }

    protected void GridViewData_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow selectedRow = GridViewUser.SelectedRow;
        Session["targetid"] = selectedRow.Cells[1].Text;
        LiteralMessages.Text = Session.Contents["targetid"].ToString(); //test
        UpdatePanelAlertMessages.Update();             //test
        Response.Redirect("~/Account/RecorderViewer.aspx");

    }

    protected override void Render(HtmlTextWriter writer)
    {
        foreach (GridViewRow row in GridViewUser.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                ClientScript.RegisterForEventValidation(((LinkButton)row.Cells[0].Controls[0]).UniqueID, "Select$" + row.RowIndex);
            }
        }
        base.Render(writer);
    }
}