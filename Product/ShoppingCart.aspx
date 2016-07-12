<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ShoppingCart.aspx.cs" Inherits="Product_ShoppingCart" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ MasterType VirtualPath="~/Site.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
        <div class="container">
        <div class="jumbotron">
             <h1>Shopping Cart</h1>
              <p>List</p> 
        </div>
         <div class="row"> 
                 <asp:UpdatePanel ID="udpShoppingCart" runat="server">

                    <ContentTemplate>
                        <asp:Literal ID="LiteralMessages" runat="server"></asp:Literal>
                    <asp:GridView ID="gvShopping" runat="server" Width="100%" AutoGenerateColumns="False" ShowFooter="True"  DataKeyNames="Price" CssClass="table table-hover">
                            <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lkbtnremove" runat="server" OnClick="lkbtnremove_onclick"><span aria-hidden="true" class="glyphicon glyphicon-minus"></span></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>Serial_Number</HeaderTemplate>
                            <ItemTemplate>
                                <%#Eval("SerialNumber") %>
                            </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>Name</HeaderTemplate>
                            <ItemTemplate>
                                <%#Eval("Name") %>
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
                    <asp:TemplateField>
                    <HeaderTemplate>Color</HeaderTemplate>
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
                                <asp:TemplateField>
                                    <HeaderTemplate>QTY</HeaderTemplate>
                                        <ItemTemplate>
                                                <asp:TextBox ID="TextBoxQTY" runat="server" OnTextChanged="gvShopping_QTYUpdated" TextMode="Number" AutoPostBack="True">1</asp:TextBox>                                
                                        </ItemTemplate>
                                    <FooterTemplate>
                                        <div><asp:Literal ID="LiteralTotalPrice" runat="server" Text="n/a"></asp:Literal></div>                                        
                                    </FooterTemplate>                        
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:Table ID="TblDetailCost" runat="server" CssClass="table table-hover" >
                            <asp:TableRow ID="Gst">
                                <asp:TableCell>GST</asp:TableCell>
                                <asp:TableCell>$</asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow ID="excGst">
                                <asp:TableCell>Exc. GST</asp:TableCell>
                                <asp:TableCell>$</asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow ID="DeliFee">
                                <asp:TableCell>Delivery Fee</asp:TableCell>
                                <asp:TableCell>$</asp:TableCell>
                            </asp:TableRow>
                            <%-- Future feature (Voucher)  --%>
                            <asp:TableRow ID="Grand">
                                <asp:TableCell>GrandTotal</asp:TableCell>
                                <asp:TableCell>$</asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                        <asp:Button ID="BtnCheckout" CssClass="btn btn-primary" runat="server" Text="Checkout" OnClick="OnClick_Checkout"/>                          
                        <%-- popup --%>
<%--                    <ajaxToolkit:PopupControlExtender ID="PopupControlForMember" Position="Center" PopupControlID ="PopCtl" TargetControlID ="PopTag" runat="server">

                    </ajaxToolkit:PopupControlExtender>--%>
                        <%-- popup --%>

                    </ContentTemplate>
                 </asp:UpdatePanel>

         </div>

        </div>


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="pbsa" Runat="Server">
</asp:Content>

