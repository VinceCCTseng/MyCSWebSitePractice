<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="RecorderViewer.aspx.cs" Inherits="RecorderViewer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="container">
        <div class="jumbotron">
            <h1>Viewer</h1>            
        </div>
        <div><h2><asp:Label ID="LabelMemberID" runat="server" Text="MemberID:"></asp:Label>
        <asp:Literal ID="LiteralMemberId" runat="server"></asp:Literal></h2></div>
        <div>
            <asp:UpdatePanel ID="udpRecord" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="gvRecord" runat="server" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical">
                        <Columns>
                            <asp:BoundField DataField="customerid" HeaderText="CustomerID" ReadOnly="True" SortExpression="customerid" InsertVisible="False" />
                            <asp:BoundField DataField="firstname" HeaderText="FirstName" SortExpression="firstname" />
                            <asp:BoundField DataField="lastname" HeaderText="LastName" SortExpression="lastname" />
                            <asp:BoundField DataField="email" HeaderText="Email" SortExpression="email" />
                            <asp:BoundField DataField="website" HeaderText="WebSite" SortExpression="website" />
                            <asp:BoundField DataField="dob" HeaderText="Date of Birth" SortExpression="dob" />
                            <asp:BoundField DataField="membercard" HeaderText="MemberCard" ReadOnly="True" SortExpression="membercard" />
                            <asp:CheckBoxField DataField="loyaltymember" HeaderText="LoyaltyMember" ReadOnly="True" SortExpression="loyaltymember" />
                            <asp:BoundField DataField="phone" HeaderText="Phone" SortExpression="phone" />
                            <asp:BoundField DataField="mobile" HeaderText="Mobile" SortExpression="mobile" />
                            <asp:BoundField DataField="fax" HeaderText="Fax" SortExpression="fax" />
                        </Columns>
                        <AlternatingRowStyle BackColor="White" />
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


    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pbsa" Runat="Server">
</asp:Content>

