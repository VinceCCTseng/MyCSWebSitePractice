<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Product.aspx.cs" Inherits="Product_Product" %>
<%@ MasterType VirtualPath="~/Site.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <header>
        <style>
            .left-half {
                /*background-color: #ff9e2c;*/
                float: left;
                width: 50%;
            }
            .right-half {
                /*background-color: #b6701e;*/
                float: left;
                width: 50%;
            }
        </style>
    </header>
    
    <div class="container">
        <div class="jumbotron">
             <h1>Product</h1>
              <p>List</p> 
        </div>
         <div class="row"> 
                 <asp:UpdatePanel ID="udpProduct" runat="server">
        <ContentTemplate>
            <div></div>
            <asp:Literal ID="LiteralDBGMessages" runat="server"></asp:Literal>
            <div></div>
            <asp:Literal ID="LiteralMessages" runat="server"></asp:Literal>
            <div></div>

                <div class="container">
                    <div class="left-half">
                        <asp:GridView ID="gvProduct" CssClass="table table-striped" runat="server" AutoGenerateColumns="False"  AllowPaging="True" OnPageIndexChanging="gvProduct_PageIndexChanging" DataKeyNames="SerialNumber,Name" BorderColor="#000066" BorderWidth="2">
                            <Columns>                    
                                <asp:TemplateField>
                                    <ItemTemplate>
                                    <%--<asp:LinkButton ID="lkbtnadd" runat="server" OnClientClick="javascript:myFunction(this); return;" OnClick="lkbtnadd_AddProducttToCart"><span aria-hidden="true" class="glyphicon glyphicon-plus"></span></asp:LinkButton>--%>
                                    <%--<asp:LinkButton ID="lkbtnadd" runat="server" OnClientClick="myFunction(this)" OnClick="lkbtnadd_AddProducttToCart"><span aria-hidden="true" class="glyphicon glyphicon-plus"></span></asp:LinkButton>--%>
                                    <asp:LinkButton ID="lkbtnadd" runat="server" OnClick="lkbtnadd_AddProducttToCart"><span aria-hidden="true" class="glyphicon glyphicon-plus"></span></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>Name</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnProduct" runat="server" OnClick="lbtnProduct_OnClick"><%#Eval("Name") %></asp:LinkButton>                                
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>Price</HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("Price") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>Image</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Image ID="Image1" runat="server" Height="50px" Width="50px" ImageUrl= '<%#Eval("Imagelink") != null ? Eval("Imagelink") : "~/Image/na.jpg" %>'  />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        </div>
                    <div class="right-half">
            <asp:DetailsView ID="dvProduct" CssClass="table table-striped" runat="server"  HeaderText="Detail Information of Product" AutoGenerateRows="False" HeaderStyle-Font-Bold="true" BorderWidth="2" BorderColor="#003300">
                <Fields>
                    <asp:TemplateField>
                        <HeaderTemplate>Product SN</HeaderTemplate>               
                        <ItemTemplate>
                            <%#Eval("SerialNumber") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>Product Name</HeaderTemplate>    
                        <ItemTemplate>
                            <%#Eval("Name") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                    <HeaderTemplate>Product Category</HeaderTemplate>  
                        <ItemTemplate>
                            <%#Eval("Category") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                    <HeaderTemplate>Product Color</HeaderTemplate>  
                        <ItemTemplate>
                            <%#Eval("Color") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                    <HeaderTemplate>Comment</HeaderTemplate>  
                        <ItemTemplate>
                            <%#Eval("Comment") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Fields>
            </asp:DetailsView>
            </div>
        </div>

        </ContentTemplate>
    </asp:UpdatePanel>
        </div>
    </div>


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="pbsa" Runat="Server">

</asp:Content>

