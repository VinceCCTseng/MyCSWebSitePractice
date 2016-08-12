using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyWebSitePractice1;
using System.Configuration;
using System.Security.Policy;
using System.Threading;

public partial class Account_Login : System.Web.UI.Page
{
    string _MemberID;
    bool debugmode = false; // Release version should be "false"
    MasterPage myMaster;
    protected void Page_Load(object sender, EventArgs e)
    {
        UpdateDisplay();

        if (debugmode)
        {
            TextBoxUsrName.Text = "v824675391";
            TextBoxUsrPsw.Text = "7298513aA-";
        }
    }

    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        if (TextBoxUsrName.Text.Length < 5 || TextBoxUsrName.Text.Length > 20)
            Master.showMessages("Error Message", "The user name was not correct");            
        else if (TextBoxUsrPsw.Text.Length < 5 || TextBoxUsrPsw.Text.Length > 20)
            Master.showMessages("Error Message", "The password was not correct");
        else
        {
            sqlAccess sqllogin = new sqlAccess(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            try
            {
                string result = sqllogin.LoginCheck(TextBoxUsrName.Text, TextBoxUsrPsw.Text);

                // check the account exist or not
                if (result.CompareTo("0") != 0)
                {   // the account exist
                    ourCustomerList acustomerlist = new ourCustomerList();
                    ourCustomer acustomer = new ourCustomer();
                    _MemberID = result.Substring(2, result.Length - 2);

                    acustomerlist = sqllogin.SearchRecord(sqlAccess.MEMBERCARD, _MemberID);
                    acustomer = acustomerlist.Getacustomer(acustomerlist.Getlistcount() - 1);

                    string customerFName = acustomer.FirstName;
                    // session available for 30 mins
                    Session.Timeout = 30;
                    Session["memberid"] = _MemberID;
                    Session["cusname"] = customerFName;

                    switch (result.Substring(0, 1))
                    {
                        case "2":                            
                            Session["mode"] = "admin";
                            Master.UpdateLoginStatus();
                            UpdateDisplay();
                            udplogin.Update();
                            break;
                        case "1":
                            Session["mode"] = "user";
                            Master.UpdateLoginStatus();
                            UpdateDisplay();
                            udplogin.Update();
                            break;
                        default:
                            Master.showMessages("Error Message", "Invalid result!");
                            break;
                    }
                }
                else
                {
                    Master.showMessages("Error Message", "The account does not exists!");
                }
          
            }
            catch
            {
                Master.showMessages("Error Message", "Sql access failed!");
            }

        }
    }
    protected void UpdateDisplay()
    {
        if (Session.Contents["memberid"] != null)
        {
            TextBoxUsrName.Visible = false;
            TextBoxUsrPsw.Visible = false;
            BtnSubmit.Visible = false;
            LabelUsrName.Visible = false;
            LabelUsrPsw.Visible = false;
            ltrMessages.Visible = false;
        }
        else
        {
            TextBoxUsrName.Visible = true;
            TextBoxUsrPsw.Visible = true;
            BtnSubmit.Visible = true;
            LabelUsrName.Visible = true;
            LabelUsrPsw.Visible = true;
            ltrMessages.Visible = true;
        }
    }
}