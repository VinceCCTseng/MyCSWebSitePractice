<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default_old1.aspx.cs" Inherits="_Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1> IT Support Centre</h1>
        <p class="lead">Login</p>
        <%--<p><a href="http://www.asp.net" class="btn btn-primary btn-lg">Learn more &raquo;</a></p>--%>
    </div>
    
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <div class="row">
            <div class="col-md=12">
                <h2>
                    <asp:Label ID="LabelUsername" runat="server" Text="Username"></asp:Label>
                </h2>
                <asp:TextBox ID="txtAcc" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                <h2>
                    <asp:Label ID="LabelPassword" runat="server" Text="Password"></asp:Label>
                </h2>
                <asp:TextBox ID="txtPsw" runat="server" CssClass="form-control" MaxLength="100" TextMode="Password"></asp:TextBox>
                <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="btnSubmit_Click" />
                <asp:Button ID="btnLogout" runat="server" CssClass="btn btn-primary" Text="Logout" OnClick="btnlogout_Click" />
                <div class="alert alert-info">
                    <span class="glyphicon glyphicon-info-sign"></span>
                    <asp:Literal ID="ltrMEssages" runat="server" />
                </div>
                <h2>
                    <asp:Label ID="LabelMember" runat="server" Text="Member:"></asp:Label>
                    <asp:Label ID="LabelMemberID" runat="server" Text=""></asp:Label></h2>
                <h2></h2>

                <asp:GridView ID="GridViewData" runat="server" AutoGenerateColumns="false" autopostback ="true" Width="100%" OnRowDataBound="GridViewData_RowDataBound" OnSelectedIndexChanged="GridViewData_SelectedIndexChanged">
                    
                    <Columns>
                        <asp:BoundField HeaderText="Customer ID" DataField="customerId" SortExpression="customerId" />
                        <asp:BoundField HeaderText="First Name" DataField="FirstName" SortExpression="FirstName" />
                        <asp:BoundField HeaderText="Last Name" DataField="lastName" SortExpression="lastName" />
                        <asp:BoundField HeaderText="Email" DataField="email" SortExpression="email" />
                        <asp:BoundField HeaderText="Mobile" DataField="mobile" SortExpression="mobile" />
                        <asp:BoundField HeaderText="LoyaltyMember" DataField="loyaltyMember" SortExpression="loyaltyMember" />
                    </Columns>
                </asp:GridView>
                <h2><asp:Label ID="LabelIndex" runat="server" Text="?"></asp:Label></h2>
                
                <p>
                    <asp:Login ID="Login1" runat="server"></asp:Login>
                    <asp:LoginName ID="LoginName1" runat="server" />
                    <asp:LoginStatus ID="LoginStatus1" runat="server" />
                </p>
            </div>
        </div>    

<%--        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click">
            </asp:AsyncPostBackTrigger>
        </Triggers>--%>
    </ContentTemplate>
    </asp:UpdatePanel>

    <div class="row">
        <div class="col-md-4">
            <h2>[1]</h2>
            <p>
                []
            </p>
            <p>
                []
            </p>
        </div>
        <div class="col-md-4">
            <h2>[2]</h2>
            <p>
                []
            </p>
            <p>
                []
            </p>
        </div>
        
        <div class="col-md-4">
            <h2>[3]</h2>
            <p>
                []
            </p>
            <p>
               []
            </p>
        </div>
    </div>
</asp:Content>

<asp:Content ID="myPlaceHolder" ContentPlaceHolderID="pbsa" runat="server">

    <h2>Hello this is the main page</h2>
</asp:Content>