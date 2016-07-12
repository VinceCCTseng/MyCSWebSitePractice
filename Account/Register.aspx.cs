using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Account_Register : System.Web.UI.Page
{
    const int _RetryTimes=10;
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        if (UserInfoValidate())
        {
            if (addACustomer())
                TextBoxClear();
            //LiteralMessages.Text += "[Pass]";
        }
        else
        {
            //LiteralMessages.Text += "[Fail]";
        }
    }
    //
    protected bool addACustomer()
    {
        ourCustomerList acustomerlist = new ourCustomerList();
        ourCustomer acustomer = new ourCustomer();
        int createtime = 0;
        bool IDexist = true, statusflag=false;
        string newmemberID=null;
        //LiteralMessages.Text += "["+createNewMemberID()+"]";
        // I. add info and sql insert
        // 1. create a MemberID and check
        while (IDexist)
        {
            newmemberID = createNewMemberID();
            try
            {
                sqlAccess checkIDUsed = new sqlAccess();
                acustomerlist = checkIDUsed.SearchRecord(sqlAccess.MEMBERCARD, newmemberID);
            }
            catch
            {
                LiteralMessages.Text = "Sql Check ID failed!";
                return false;
            }
            if (acustomerlist.Getlistcount() == 0)
                IDexist = false;
            else
            {
                createtime++;
                IDexist = true;
                if (createtime == _RetryTimes)
                goto CreateMemberIDError;
            }
        }

        acustomer.FirstName = TextBoxFstNam.Text;
        acustomer.LastName = TextBoxLstNam.Text;
        acustomer.MemberCard = newmemberID;
        acustomer.Mobile = TextBoxmobile.Text;

        // 2. insert the customer
        try
        {
            sqlAccess insertacustomer = new sqlAccess();
            statusflag = insertacustomer.SaveRecord(acustomer.FirstName,acustomer.LastName,acustomer.Email,acustomer.Website,acustomer.DOB,acustomer.MemberCard,acustomer.LoyaltyMember,acustomer.Phone,acustomer.Mobile,acustomer.Fax);          
        }
        catch
        {
            LiteralMessages.Text = "Sql insert a customer failed!";
            return false;
        }

        // II. insert useraccount
        //CreateUserAccount
        try
        {
            sqlAccess insertAUserAccount = new sqlAccess();
            statusflag = insertAUserAccount.CreateUserAccount(TextBoxUsr.Text,TextBoxPsw.Text, false, acustomer.MemberCard);
        }
        catch
        {
            LiteralMessages.Text = "Sql insert a user account failed!";
            return false;
        }

        // III. The final check about status flag
        if (statusflag)
        {
            LiteralMessages.Text = "Welcome to use this system, " + acustomer.FirstName + "["+acustomer.MemberCard+ "]. The registration was done!";
            return true;
        }          
        else
        {
            LiteralMessages.Text = "Faild to create a new customer/user account!";
            return false;
        }


        // Avoid infinite loop
        CreateMemberIDError:
        LiteralMessages.Text = "Generate Member ID failed!";
        return false;

    }
    // User Information Validation
    protected bool UserInfoValidate()
    {
        string error = null;

        // 1. User name check
        if (TextBoxUsr.Text != null)
        {
            if((TextBoxUsr.Text.Length < 5 || TextBoxUsr.Text.Length > 50))
                error += "[Uesr name needs more than 5 char, or less than 50 char!]\r\n";
        }
        else
            error += "[Uesr name is necessary!]\r\n";

        // 2. password name check
        if (TextBoxPsw.Text != null)
        {
            if ((TextBoxPsw.Text.Length < 5 || TextBoxUsr.Text.Length > 50))
                error += "[Password needs more than 5 char, or less than 50 char]\r\n";
        }
        else
            error += "[Password is necessary!]\r\n";

        // 3. FirstNmae check
        if (TextBoxFstNam.Text != null)
        {
            if ((TextBoxFstNam.Text.Length > 25))
                error += "[FirstNmae needs less than 25 char]\r\n";
        }
        else
            error += "[FirstNmae is necessary!]\r\n";

        // 4. Last Name check
        if (TextBoxLstNam.Text != null)
        {
            if ((TextBoxLstNam.Text.Length > 25))
                error += "[Last Name needs less than 25 char]\r\n";
        }
        else
            error += "[Last Name is necessary!]\r\n";

        // 5. Moblie check
        if (TextBoxmobile.Text != null)
        {
            if ((TextBoxmobile.Text.Length != 10))
                error += "[Moblie needs 10 Number]\r\n";
        }
        else
            error += "[Moblie is necessary!]\r\n";

        LiteralMessages.Text = error;

        if (error == null)
            return true;
        else
            return false;
    }
    public string createNewMemberID()
    {
        string randomchar = "ABCDEFGHIJKLMNOPQRSTUVWXYZ01234567890", MID=null;
        Random rand = new Random();
        for (int i = 0; i < 20; i++)
        {
            int rindex = rand.Next(0, 35);
            MID += randomchar[rindex];
        }
        return MID;
    }

    public void TextBoxClear()
    {
        TextBoxFstNam.Text = "";
        TextBoxLstNam.Text = "";
        TextBoxmobile.Text = "";
        TextBoxPsw.Text = "";
        TextBoxUsr.Text = "";
            
    }
}