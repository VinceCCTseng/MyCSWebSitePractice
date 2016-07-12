using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using MyWebSitePractice1;
using System.Web.Security;
using System.Data;

public partial class _Default : Page
{
    private string _MemberID; // temp
    private bool _SuperMode; // temp should using cookie

    ourCustomerList _customerlist;
    protected void Page_Load(object sender, EventArgs e)
    {
        showloginitem();
        _MemberID = null;
        _SuperMode = false;
        
    }

    protected void btnSubmit_Click(object sender, EventArgs e)  
    {

        if (txtAcc.Text.Length < 5 || txtAcc.Text.Length > 20)
            ltrMEssages.Text = "The user name was not correct";
        else if (txtPsw.Text.Length < 5 || txtPsw.Text.Length > 20)
            ltrMEssages.Text = "The password was not correct";
        else
        {

            sqlAccess sqllogin = new sqlAccess(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            try
            {
                string result = sqllogin.LoginCheck(txtAcc.Text, txtPsw.Text);
                _MemberID = result.Substring(2, result.Length - 2);
                switch (result.Substring(0, 1))
                {
                    case "2":
                        ltrMEssages.Text = "Admin mode";
                        panelupdate(true);
                        updatecustomerdatagrid(this, null);
                        break;
                    case "1":
                        ltrMEssages.Text = "Usermode";
                        panelupdate(false);
                        updatecustomerdatagrid(this, null);
                        break;
                    default:
                        ltrMEssages.Text = "The account does not exists!";
                        break;
                }
            }
            catch
            { }

        }        
    }
    protected void panelupdate(bool SuperMode)
    {
        if (_MemberID != null)
        { 
            LabelMemberID.Text = "[" + _MemberID + "]" + (SuperMode?"\t[S Mode]":"");
            showlogoutitem();
        }
        if (SuperMode == true)
            _SuperMode = true;
    }
    private void showloginitem()
    {
        if (_MemberID == null)
        {
            //
            LabelMember.Visible = false;
            LabelMemberID.Visible = false;
            GridViewData.Visible = false;
            btnLogout.Visible = false;
            //
            txtAcc.Visible = true;
            txtPsw.Visible = true;
            LabelPassword.Visible = true;
            LabelUsername.Visible = true;
            btnSubmit.Visible = true;
            // Clear
            ltrMEssages.Visible = false;
            ltrMEssages.Text = null;
            LabelIndex.Visible = false;
        }
    }
    private void showlogoutitem()
    {
        // vis
        LabelMember.Visible = true;
        LabelMemberID.Visible = true;
        GridViewData.Visible = true;
        btnLogout.Visible = true;
        // disvis
        txtAcc.Visible = false;
        txtPsw.Visible = false;
        LabelPassword.Visible = false;
        LabelUsername.Visible = false;
        btnSubmit.Visible = false;
        //
        ltrMEssages.Visible = true;
        //if (_SuperMode == true)
            LabelIndex.Visible = true;
    }
    protected void btnsubmitajax_Click(object sender, EventArgs e)
    {
    }

    protected void btnlogout_Click(object sender, EventArgs e)
    {
        _MemberID = null;
        showloginitem();
    }
    private void updatecustomerdatagrid(object sender, EventArgs e)
    {
        try
        {
            sqlAccess sqlcustomerlist = new sqlAccess();
            _customerlist = sqlcustomerlist.RetrievalCustomerList();
            //this.GridViewData.AutoGenerateColumns = false;
            GridViewData.DataSource = _customerlist.Getcustomerlist();
            GridViewData.DataBind();
        }
        catch
        {
            //error
        }
    }
    protected void GridViewData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridViewRow row = GridViewData.SelectedRow;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.textDecoration='underline';this.style.color='red';";
            e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';";

            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(GridViewData, "Select$" + e.Row.RowIndex);
            //e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.GridViewData, "Select$" + e.Row.RowIndex);
            //e.Row.Attributes["onclick"] = ClientScript.GetPostBackEventReference(this.showindex(), "Select$", RowIndex.ToString());
        }
    }


    protected void GridViewData_SelectedIndexChanged(object sender, EventArgs e)
    {
        int index = GridViewData.SelectedRow.RowIndex;
        string name = GridViewData.SelectedRow.Cells[0].Text;
        string country = GridViewData.SelectedRow.Cells[1].Text;
        string message = "Row Index: " + index + "\\nName: " + name + "\\nCountry: " + country;
        LabelIndex.Text = index.ToString();
        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
    }

}