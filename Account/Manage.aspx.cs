using MyWebSitePractice1;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Account_Manage : System.Web.UI.Page
{
    //
    string _memberid;
    //ourCustomerList _aSearchList;
    //
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session.Contents["memberid"] != null)
            {
                _memberid = Session.Contents["memberid"].ToString();
                updatedetail();
                BindData();
            }
            else
                Response.Redirect("~/Default.aspx");
        }
        else
        {
            
        }
    }
       
    //
    protected void updatedetail()
    {
        try
        {
            ourCustomerList _aSearchList;
            sqlAccess sqlgetadetail = new sqlAccess(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            _aSearchList = sqlgetadetail.SearchRecord(sqlAccess.MEMBERCARD, Session.Contents["memberid"].ToString());
            Session["CurSearchList"] = _aSearchList;
        }
        catch
        {
            Master.showMessages("Error Message", "Access failed!");
        }        
    }

    // new dv
    protected void dvEditRecord_click(object sender, EventArgs e)
    {
        dvAccountDetail.ChangeMode(DetailsViewMode.Edit);
        BindData();
    }

    // old gv
    protected void EditRecord_click(object sender, EventArgs e)
    {

        LinkButton myButton = (LinkButton)sender;
        string myId = myButton.Attributes["clientId"];
        // get row
        GridViewRow gvr = (GridViewRow)myButton.NamingContainer;
        gvAccountDetail.EditIndex = gvr.RowIndex;
        BindData();
    }

    // new dv
    protected void dvEditUpdate_click(object sender, EventArgs e)
    {
        ourCustomerList aCustomerList = (ourCustomerList)Session["CurSearchList"];
        int index = aCustomerList.Getlistcount() - 1;
        bool updateStatus = true;
        bool changed = false;
        ourCustomer aCustomer = aCustomerList.Getacustomer(index);
        //LiteralMessages.Text = null;
        customerTools checkdata = new customerTools();

        try
        {
            sqlAccess updateCustomerInfo = new sqlAccess();

            // 1. Check and update firstname
            TextBox tbFstName = (TextBox)dvAccountDetail.FindControl("txtfirstname");
            if (aCustomer.FirstName.CompareTo(tbFstName.Text) != 0)
            {
                if (checkdata.customerDataValidation(sqlAccess.UFIRSTNAME, tbFstName.Text))
                {
                    updateStatus = updateCustomerInfo.SaveRecord(aCustomer.CustomerId, sqlAccess.UFIRSTNAME, tbFstName.Text);
                    changed = true;
                }
                else
                {

                    Master.showMessages("Error Message", "The length of the first name should be from 1 character to 25 characters.");
                    goto EndofUpdate;
                }
            }

            // 2. Check and update lasttname
            TextBox tbLstName = (TextBox)dvAccountDetail.FindControl("txtlastname");
            if (aCustomer.LastName.CompareTo(tbLstName.Text) != 0)
            {
                if (checkdata.customerDataValidation(sqlAccess.ULASTNAME, tbLstName.Text))
                { 
                    updateStatus = updateCustomerInfo.SaveRecord(aCustomer.CustomerId, sqlAccess.ULASTNAME, tbLstName.Text);
                    changed = true;                
                }
                else
                {
                    Master.showMessages("Error Message", "The length of the last name should be from 1 character to 25 characters.");
                    goto EndofUpdate;
                }
            }

            // 3. Check and update email
            TextBox tbemail = (TextBox)dvAccountDetail.FindControl("txtemail");
            if (aCustomer.Email.CompareTo(tbemail.Text) != 0)
            {
                if (checkdata.customerDataValidation(sqlAccess.UEMAIL, tbemail.Text))
                { 
                    updateStatus = updateCustomerInfo.SaveRecord(aCustomer.CustomerId, sqlAccess.UEMAIL, tbemail.Text);
                    changed = true;
                }
            else
            {
                Master.showMessages("Error Message", "The email address is invalid.");
                goto EndofUpdate;
            }
            }
            // 4. Check and update website
            TextBox tbweb = (TextBox)dvAccountDetail.FindControl("txtwebsite");
            if (aCustomer.Website.CompareTo(tbweb.Text) != 0)
            {
                if (checkdata.customerDataValidation(sqlAccess.UWEBSITE, tbweb.Text))
                {
                    updateStatus = updateCustomerInfo.SaveRecord(aCustomer.CustomerId, sqlAccess.UWEBSITE, tbweb.Text);
                    changed = true;
                }
                else
                {
                    Master.showMessages("Error Message", "The length of the last name should be from 3 character to 200 characters.");
                    goto EndofUpdate;
                }
            }

            // 5. check and update Date of birth
            TextBox tbdob = (TextBox)dvAccountDetail.FindControl("txtdob");
            if (aCustomer.DOB.CompareTo(tbdob.Text) != 0)
            {
                if (checkdata.customerDataValidation(sqlAccess.UDOB, tbdob.Text))
                {
                    updateStatus = updateCustomerInfo.SaveRecord(aCustomer.CustomerId, sqlAccess.UDOB, tbdob.Text);
                    changed = true;
                }
                else
                {
                    Master.showMessages("Error Message", "The date format should be correct!");
                    goto EndofUpdate;
                }
            }

            // 6. Check and update phone
            TextBox tbphone = (TextBox)dvAccountDetail.FindControl("txtphone");
            if (aCustomer.Phone.CompareTo(tbphone.Text) != 0)
            {
                if (checkdata.customerDataValidation(sqlAccess.UPHONE, tbphone.Text))
                {
                    updateStatus = updateCustomerInfo.SaveRecord(aCustomer.CustomerId, sqlAccess.UPHONE, tbphone.Text);
                    changed = true;
                }
                else
                {
                    Master.showMessages("Error Message", "The phone number should be 10 digis.");
                    goto EndofUpdate;
                }
            }

            // 7. Check and update mobile
            TextBox tbmobile = (TextBox)dvAccountDetail.FindControl("txtmobile");
            if (aCustomer.Mobile.CompareTo(tbmobile.Text) != 0)
            {
                if (checkdata.customerDataValidation(sqlAccess.UMOBILE, tbmobile.Text))
                {
                    updateStatus = updateCustomerInfo.SaveRecord(aCustomer.CustomerId, sqlAccess.UMOBILE, tbmobile.Text);
                    changed = true;
                }
                else
                {
                    Master.showMessages("Error Message", "The mobile number should be 10 digis.");
                    goto EndofUpdate;
                }
            }

            // 8. Check and update fax
            TextBox tbfax = (TextBox)dvAccountDetail.FindControl("txtfax");
            if (aCustomer.Fax.CompareTo(tbfax.Text) != 0)
            {
                if (checkdata.customerDataValidation(sqlAccess.UFAX, tbfax.Text))
                {
                    updateStatus = updateCustomerInfo.SaveRecord(aCustomer.CustomerId, sqlAccess.UFAX, tbfax.Text);
                    changed = true;
                }
                else
                {
                    Master.showMessages("Error Message", "The fax number should be 10 digis.");
                    goto EndofUpdate;
                }
            }

            // Show the update status
            if (updateStatus)
            {
                //LiteralMessages.Text = "Update was done"; // ToDo: Disable it
                if(changed)
                    Master.showMessages("Customer Update", "Record sucessfully changed");
                else
                    Master.showMessages("Customer Update", "There was nothing changed.");
            }
            else
            {
                //LiteralMessages.Text += "[Error] sqlaccess went wrong during update information!";
                Master.showMessages("Customer Update", "Problems: ");
            }

            // End of edit mode
            EndofUpdate:
            updateCustomerInfo.sqldisconnect();
            updatedetail();
            dvAccountDetail.ChangeMode(DetailsViewMode.ReadOnly);
            BindData();

        }
        catch
        {
            Master.showMessages("Error Message", "Access failed!");
        }
    }

    // old gv
    // EditUpdate Function : To Do: data vaildation
    protected void EditUpdate_click(object sender, EventArgs e)
    {
        ourCustomerList aCustomerList = (ourCustomerList)Session["CurSearchList"];
        int index = aCustomerList.Getlistcount() - 1;
        bool updateStatus = false;
        ourCustomer aCustomer = aCustomerList.Getacustomer(index);
        //LiteralMessages.Text = null;
        customerTools checkdata = new customerTools();


        try
        {
            sqlAccess updateCustomerInfo = new sqlAccess();

            // 1. Check and update firstname
            TextBox tbFstName = (TextBox)gvAccountDetail.Rows[index].FindControl("txtfirstname");
            if (aCustomer.FirstName.CompareTo(tbFstName.Text) != 0)
            {
                if (checkdata.customerDataValidation(sqlAccess.UFIRSTNAME, tbFstName.Text))
                    updateStatus = updateCustomerInfo.SaveRecord(aCustomer.CustomerId, sqlAccess.UFIRSTNAME, tbFstName.Text);
                else
                {

                    Master.showMessages("Error Message", "The length of the first name should be from 1 character to 25 characters.");
                    goto EndofUpdate;
                }
            }

            // 2. Check and update lasttname
            TextBox tbLstName = (TextBox)gvAccountDetail.Rows[index].FindControl("txtlastname");
            if (aCustomer.LastName.CompareTo(tbLstName.Text) != 0)
            {
                if (checkdata.customerDataValidation(sqlAccess.ULASTNAME, tbLstName.Text))
                    updateStatus = updateCustomerInfo.SaveRecord(aCustomer.CustomerId, sqlAccess.ULASTNAME, tbLstName.Text);
                else
                {
                    Master.showMessages("Error Message", "The length of the last name should be from 1 character to 25 characters.");
                    goto EndofUpdate;
                }
            }

            // 3. Check and update email
            TextBox tbemail = (TextBox)gvAccountDetail.Rows[index].FindControl("txtemail");
            if (aCustomer.Email.CompareTo(tbemail.Text) != 0)
            {
                if (checkdata.customerDataValidation(sqlAccess.UEMAIL, tbemail.Text))
                    updateStatus = updateCustomerInfo.SaveRecord(aCustomer.CustomerId, sqlAccess.UEMAIL, tbemail.Text);
                else
                {
                    Master.showMessages("Error Message","The email address is invalid.");
                    goto EndofUpdate;
                }
            }
            // 4. Check and update website
            TextBox tbweb = (TextBox)gvAccountDetail.Rows[index].FindControl("txtwebsite");
            if (aCustomer.Website.CompareTo(tbweb.Text) != 0)
            {
                if (checkdata.customerDataValidation(sqlAccess.UWEBSITE, tbweb.Text))
                    updateStatus = updateCustomerInfo.SaveRecord(aCustomer.CustomerId, sqlAccess.UWEBSITE, tbweb.Text);
                else
                {
                    Master.showMessages("Error Message","The length of the last name should be from 3 character to 200 characters.");
                    goto EndofUpdate;
                }
            }

            // 5. check and update Date of birth
            TextBox tbdob = (TextBox)gvAccountDetail.Rows[index].FindControl("txtdob");
            if (aCustomer.DOB.CompareTo(tbdob.Text) != 0)
            {
                if (checkdata.customerDataValidation(sqlAccess.UDOB, tbdob.Text))
                    updateStatus = updateCustomerInfo.SaveRecord(aCustomer.CustomerId, sqlAccess.UDOB, tbdob.Text);
                else
                {
                    Master.showMessages("Error Message", "The date format should be correct!");
                    goto EndofUpdate;
                }
            }

            // 6. Check and update phone
            TextBox tbphone = (TextBox)gvAccountDetail.Rows[index].FindControl("txtphone");
            if (aCustomer.Phone.CompareTo(tbphone.Text) != 0)
            {
                if (checkdata.customerDataValidation(sqlAccess.UPHONE, tbphone.Text))
                    updateStatus = updateCustomerInfo.SaveRecord(aCustomer.CustomerId, sqlAccess.UPHONE, tbphone.Text);
                else
                {
                    Master.showMessages("Error Message",  "The phone number should be 10 digis.");
                    goto EndofUpdate;
                }
            }

            // 7. Check and update mobile
            TextBox tbmobile = (TextBox)gvAccountDetail.Rows[index].FindControl("txtmobile");
            if (aCustomer.Mobile.CompareTo(tbmobile.Text) != 0)
            {
                if (checkdata.customerDataValidation(sqlAccess.UMOBILE, tbmobile.Text))
                    updateStatus = updateCustomerInfo.SaveRecord(aCustomer.CustomerId, sqlAccess.UMOBILE, tbmobile.Text);
                else
                {
                    Master.showMessages("Error Message","The mobile number should be 10 digis.");
                    goto EndofUpdate;
                }
            }

            // 8. Check and update fax
            TextBox tbfax = (TextBox)gvAccountDetail.Rows[index].FindControl("txtfax");
            if (aCustomer.Fax.CompareTo(tbfax.Text) != 0)
            {
                if (checkdata.customerDataValidation(sqlAccess.UFAX, tbfax.Text))
                    updateStatus = updateCustomerInfo.SaveRecord(aCustomer.CustomerId, sqlAccess.UFAX, tbfax.Text);
                else
                {
                    Master.showMessages("Error Message", "The fax number should be 10 digis.");
                    goto EndofUpdate;
                }
            }

            // Show the update status
            if (updateStatus)
            {
                //LiteralMessages.Text = "Update was done"; // ToDo: Disable it
                Master.showMessages("Customer Update", "Record sucessfully changed");
            }
                
            else
            {
                //LiteralMessages.Text += "[Error] sqlaccess went wrong during update information!";
                Master.showMessages("Customer Update", "Problems: ");
            }

            // End of edit mode
            EndofUpdate:
            updateCustomerInfo.sqldisconnect();            
            updatedetail();
            gvAccountDetail.EditIndex = -1;
            BindData();

        }
        catch
        {
            Master.showMessages("Error Message", "Access failed!" );
        }
    }

    // new dv
    protected void dvEditCancel_click(object sender, EventArgs e)
    {
        dvAccountDetail.ChangeMode(DetailsViewMode.ReadOnly);
        BindData();
    }

    // old gv
    protected void EditCancel_click(object sender, EventArgs e)
    {   
        gvAccountDetail.EditIndex = -1;
        BindData();
    }

    private void BindData()
    {
        if (Session["CurSearchList"] != null)
        {
            ourCustomerList _aSearchList = (ourCustomerList)Session["CurSearchList"];
            // detail view
            dvAccountDetail.DataSource = _aSearchList.Getcustomerlist();
            dvAccountDetail.DataBind();
            // grid view
            //gvAccountDetail.DataSource = _aSearchList.Getcustomerlist();
            //gvAccountDetail.DataBind();
        }
        else
        {
            Response.Redirect("~/Deafult.aspx");
        }
    }


}