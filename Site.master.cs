using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SiteMaster : MasterPage
{
    private const string AntiXsrfTokenKey = "__AntiXsrfToken";
    private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
    private string _antiXsrfTokenValue;
    private string _memberId;
    protected void Page_Init(object sender, EventArgs e)
    {
        // The code below helps to protect against XSRF attacks
        var requestCookie = Request.Cookies[AntiXsrfTokenKey];
        Guid requestCookieGuidValue;
        if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
        {
            // Use the Anti-XSRF token from the cookie
            _antiXsrfTokenValue = requestCookie.Value;
            Page.ViewStateUserKey = _antiXsrfTokenValue;
        }
        else
        {
            // Generate a new Anti-XSRF token and save to the cookie
            _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
            Page.ViewStateUserKey = _antiXsrfTokenValue;

            var responseCookie = new HttpCookie(AntiXsrfTokenKey)
            {
                HttpOnly = true,
                Value = _antiXsrfTokenValue
            };
            if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
            {
                responseCookie.Secure = true;
            }
            Response.Cookies.Set(responseCookie);
        }

        Page.PreLoad += master_Page_PreLoad;
    }

    protected void master_Page_PreLoad(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Set Anti-XSRF token
            ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
            ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            if (Session.Contents["CurNumberOfItems"] == null)
                LblNumberOfItems.Text = "?";
            else
                LblNumberOfItems.Text = Session.Contents["CurNumberOfItems"].ToString();
        }
        else
        {
            // Validate the Anti-XSRF token
            if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
            {
                throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        logdisplay();
        LiteralYear.Text = DateTime.Now.Year.ToString() +" - IT support ";
    }

    protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
    {
        Context.GetOwinContext().Authentication.SignOut();
    }

    protected void LinkBtnLogout_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Redirect("~/Default.aspx");
    }

    public void UpdateLoginStatus()
    {
        this.logdisplay();
        this.updLoginStatus.Update();
    }

    protected void logdisplay()
    {
        if (Session.Contents["memberid"] != null)
        {
            HyperLinkLogin.Visible = false;
            HyperLinkManage.Visible = true;
            LinkBtnLogout.Visible = true;
            HyperLinkRegister.Visible = false;

            if (Session.Contents["mode"].ToString().CompareTo("admin")==0)
                HyperLinkLabel.Text = "[ " + Session.Contents["cusname"].ToString() + " ] " + "logged in. [ " + Session.Contents["mode"].ToString() + " ]";
            else
                HyperLinkLabel.Text = "[ " + Session.Contents["cusname"].ToString() + " ] " + "logged in. ";
        }
        else
        {
            HyperLinkRegister.Visible = true;
            HyperLinkLogin.Visible = true;
            HyperLinkManage.Visible = false;
            LinkBtnLogout.Visible = false;
            HyperLinkCartNoticeEmpty.Visible = false;
            HyperLinkCartNotice.Visible = false;
        }

        // Moved outside for non member to shopping
        if (Session["CurShoppingList"] == null)
        {
            HyperLinkCartNoticeEmpty.Visible = true;
            HyperLinkCartNotice.Visible = false;
        }
        else
        {
            HyperLinkCartNoticeEmpty.Visible = false;
            HyperLinkCartNotice.Visible = true;

        }
    }

    public void showMessages(string subject, string message) {
        popInfo.Show();
        ltrInfoHeading.Text = subject;
        lblInformationMessage.Text = message;
        updIH.Update();
        updInfomation.Update();
    }

    public void btnModalClose_Onclick(object sender, EventArgs e)
    {
        popInfo.Hide();
    }
    public void UpdateNumberOfItems(int number)
    {
        LblNumberOfItems.Text = number.ToString();
    }
}

