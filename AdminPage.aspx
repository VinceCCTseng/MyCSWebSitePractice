<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="AdminPage.aspx.cs" Inherits="AdminPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
        <div class="container">
  <div class="jumbotron">
    <h1>Admin Page</h1>
    <p>Manage your users and orders</p> 
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
      <asp:UpdatePanel ID="UpdatePanelData" runat="server">
          <ContentTemplate>

              <asp:Button ID="BtnRefreshUser" CssClass="btn btn-primary" runat="server" Text="RefreshUser" OnClick="BtnRefreshUser_Click" />
              <h2></h2>
              <asp:GridView ID="GridViewUser" runat="server"  AutoGenerateColumns="False" Width="100%" OnRowDataBound="GridViewData_RowDataBound" OnSelectedIndexChanged="GridViewData_SelectedIndexChanged" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" AllowPaging="False">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:ButtonField CommandName="Select" Visible="false" />  
                        <asp:BoundField HeaderText="MemberID" DataField="membercard" SortExpression="membercard" />
                        <asp:BoundField HeaderText="First Name" DataField="FirstName" SortExpression="FirstName" />
                        <asp:BoundField HeaderText="Last Name" DataField="lastName" SortExpression="lastName" />
                        <asp:BoundField HeaderText="Email" DataField="email" SortExpression="email" />
                        <asp:BoundField HeaderText="Mobile" DataField="mobile" SortExpression="mobile" />
                        <asp:BoundField HeaderText="LoyaltyMember" DataField="loyaltyMember" SortExpression="loyaltyMember" />
                    </Columns>
                    <FooterStyle BackColor="#CCCC99" />
                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                    <RowStyle BackColor="#F7F7DE" />
                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#FBFBF2" />
                    <SortedAscendingHeaderStyle BackColor="#848384" />
                    <SortedDescendingCellStyle BackColor="#EAEAD3" />
                    <SortedDescendingHeaderStyle BackColor="#575357" />
              </asp:GridView>
          </ContentTemplate>
      </asp:UpdatePanel>
  </div>
  <div class="row">
      <h2></h2>
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
              <asp:Button ID="BtnRefreshOrder" CssClass="btn btn-primary" runat="server" Text="RefreshOrder" OnClick="BtnRefreshOrder_Click" />
          </ContentTemplate>
      </asp:UpdatePanel>
      <h2></h2>
  </div>
</div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pbsa" Runat="Server">
</asp:Content>

