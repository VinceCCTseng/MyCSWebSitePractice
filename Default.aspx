<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="container">
  <div class="jumbotron">
    <h1>Order system</h1>
    <p>Login and gusest page</p> 
  </div>
  <div class="row">      
    <asp:UpdatePanel ID="UpdatePanelAlertMessages" UpdateMode="Conditional" RenderMode="Inline" runat="server">
        <ContentTemplate>
            <div class="alert alert-info">
                <h2><asp:Literal ID="LiteralMessages" runat="server"></asp:Literal></h2>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
  </div>
  <div class="row">      
  </div>
  <div class="row">    
  </div>
</div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pbsa" Runat="Server">
    Main Page V2.0
</asp:Content>

