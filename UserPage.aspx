<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="UserPage.aspx.cs" Inherits="UserPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="container">
        <div class="jumbotron">
                <h1>Store Page</h1>
                <p>Take your time to enjoy it</p> 
        </div>
        <h2></h2>
        <div>
            <h2><asp:Label ID="LabelGreeting" runat="server" Text="Hello, "></asp:Label>
            <asp:Literal ID="LiteralMemberID" runat="server"></asp:Literal></h2>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="pbsa" Runat="Server">
</asp:Content>

