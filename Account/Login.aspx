<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master"  AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Account_Login" %>
<%@ MasterType VirtualPath="~/Site.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="container">
        <div class="jumbotron">
            <div class="row">
                <h1>Login</h1>
            </div>
            <asp:UpdatePanel ID="udplogin" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                <ContentTemplate>
                    
                    <table>
                        <tr>
                            <td><asp:Label ID="LabelUsrName" runat="server" Text="UserName "></asp:Label>    </td>
                            <td>&nbsp;&nbsp;<asp:TextBox ID="TextBoxUsrName" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr><td></td></tr>
                        <tr>
                            <td><asp:Label ID="LabelUsrPsw" runat="server" Text="Password  "></asp:Label>    </td>
                            <td>&nbsp;&nbsp;<asp:TextBox ID="TextBoxUsrPsw" runat="server" TextMode="Password"></asp:TextBox></td>
                        </tr>
                        <tr>
                                <td><span style="color:red"><asp:Literal ID="ltrMessages" runat="server"></asp:Literal></span></td>
                        </tr>
                        <tr>
                                <td><asp:Button ID="BtnSubmit" runat="server" CssClass="btn btn-success" Text="Submit" OnClick="BtnSubmit_Click" /></td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pbsa" Runat="Server">
    Login V2.0
</asp:Content>

