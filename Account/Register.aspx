<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Account_Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="container">
        <div class="jumbotron">
            <div class="row">
                <h1>Register</h1>
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table>
                    <tr><td>Register Form</td></tr>
                    <tr>
                        <td></td>
                        <td>User Name:</td>                        
                        <td>&nbsp;&nbsp;<asp:TextBox ID="TextBoxUsr" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>Password:  </td>
                        <td>&nbsp;&nbsp;<asp:TextBox ID="TextBoxPsw" runat="server" TextMode="Password"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>First Name:  </td>
                        <td>&nbsp;&nbsp;<asp:TextBox ID="TextBoxFstNam" runat="server" ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>Last Name:  </td>
                        <td>&nbsp;&nbsp;<asp:TextBox ID="TextBoxLstNam" runat="server" ></asp:TextBox></td>
                    </tr>                    
                    <tr>
                        <td></td>
                        <td>Mobile:  </td>
                        <td>&nbsp;&nbsp;<asp:TextBox ID="TextBoxmobile" runat="server" ></asp:TextBox></td>
                    </tr>                    
                    <tr>
                        <td></td>
                        <td></td>
                        <td>&nbsp;&nbsp;<asp:Button ID="BtnSubmit" runat="server" Text="Submit" OnClick="BtnSubmit_Click"/><asp:Button ID="BtnCancel" runat="server" Text="Cancel" /></td>
                    </tr>                                        
                </table>
                <asp:Literal ID="LiteralMessages" runat="server"></asp:Literal>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pbsa" Runat="Server">
</asp:Content>

