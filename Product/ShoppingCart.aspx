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
                        <%-- make it easy to retrive total cost --%>
                        <%--<asp:Label ID="LblHiddenPrice" ClientIDMode ="Static" runat="server" Text="" ></asp:Label>--%>
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
                                        <div> Inc. GST $<asp:Label ID="LblTotalPrice" runat="server" Text="" ClientIDMode="Static"></asp:Label> AUD </div>                                        
                                    </FooterTemplate>                        
                                </asp:TemplateField>
<%--                                <asp:TemplateField>
                                    <ItemTemplate>

                                        <button  onclick="javascript:window.document.getElementById('LblTotalPrice').innerText = '1000'" >Click</button>
                                    </ItemTemplate>

                                </asp:TemplateField>--%>

                            </Columns>
                        </asp:GridView>
                        
                        <asp:Table ID="TblDetailCost" runat="server" CssClass="table table-hover" >
                            <asp:TableRow ID="Gst">
                                <asp:TableCell>GST:</asp:TableCell>
                                <asp:TableCell>$ <asp:Label ID="LbGST" runat="server" Text=""></asp:Label></asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow ID="excGst">
                                <asp:TableCell>Exc. GST</asp:TableCell>
                                <asp:TableCell>$ <asp:Label ID="LbExGST" runat="server" Text=""></asp:Label></asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow ID="DeliFee">
                                <asp:TableCell>Delivery Fee</asp:TableCell>
                                <asp:TableCell>$ <asp:Label ID="LbDeliFee" runat="server" Text=""></asp:Label></asp:TableCell>
                            </asp:TableRow>
                            <%-- Future feature (Voucher)  --%>
                            <asp:TableRow ID="Grand">
                                <asp:TableCell>GrandTotal</asp:TableCell>
                                <asp:TableCell>$ <asp:Label ID="LbGrandTotal" runat="server" Text=""></asp:Label></asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>

                        <%-- knockout version (but not working properly.
                        <asp:Table ID="Table1" runat="server" CssClass="table table-hover" >
                            <asp:TableRow ID="Gst">
                                <asp:TableCell>GST:</asp:TableCell>
                                <asp:TableCell>$ <span data-bind="text: showGst"></span></asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow ID="excGst">
                                <asp:TableCell>Exc. GST</asp:TableCell>
                                <asp:TableCell>$ <span data-bind="text: showExcGstTotal"></span></asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow ID="DeliFee">
                                <asp:TableCell>Delivery Fee</asp:TableCell>
                                <asp:TableCell>$ <span data-bind="text: showDeliverFee"></span></asp:TableCell>
                            </asp:TableRow>
                            <%-- Future feature (Voucher) 
                            <asp:TableRow ID="Grand">
                                <asp:TableCell>GrandTotal</asp:TableCell>
                                <asp:TableCell>$ <span data-bind="text: showGrandTotal"></span></asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>--%>

                        <asp:Button ID="BtnCheckout" CssClass="btn btn-primary" runat="server" Text="Checkout"/>                          
                        <%-- popup --%>
                        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtenderCheckout" runat="server"
                            TargetControlID ="BtnCheckout"
                            PopupControlID = "PopPanel"
                            BackgroundCssClass = "modalBackground"
                            DropShadow="true"                                                        
                            PopupDragHandleControlID="PopPanelHandle" />

                        <asp:Panel ID="PopPanel" runat="server" CssClass="mymodal" role="dialog" Style="display: none">
                            <div class="container">
                                <div class="jumbotron">
                                    <h1><asp:Label ID="LblPaymentMsg" runat="server" Text="Payment Summary"></asp:Label></h1>
                                </div>                                
                                <div class="container">
                                    <asp:Label ID="LabelMsgPop" runat="server"><f class="double"><%="Total cost: $ "+LbGrandTotal.Text.ToString()%></f></asp:Label>
                                    <table class="payinfo">
                                        <tr>
                                            <th>
                                                <asp:Label ID="LblCustomerName" runat="server" Text="Name"></asp:Label>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="TextBoxCustomerName" runat="server"></asp:TextBox>
                                            </th>
                                        </tr>
                                        <tr>
                                            <th>
                                                <asp:Label ID="LblCustomerAddress" runat="server" Text="Address"></asp:Label>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="TextBoxAddress" runat="server"></asp:TextBox>
                                            </th>
                                        </tr>
                                        <tr>
                                            <th>
                                                <asp:Label ID="LblPhone" runat="server" Text="Phone"></asp:Label>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="TextBoxPhone" runat="server"></asp:TextBox>
                                            </th>
                                        </tr>
                                    </table>
                                    <table class="payinfo">
                                        <tr>                                            
                                            <th>
                                                <asp:RadioButtonList ID="RBLPaymenttype" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Selected="True" Text="Master" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="VISA" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="AMX" Value="2"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </th>
                                            <th></th>
                                        </tr>
                                        <tr>
                                            <th>
                                                <asp:Label ID="LblHN" runat="server" Text="Card holder"></asp:Label>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="holderName" runat="server" ></asp:TextBox>
                                            </th>
                                        </tr>
                                        <tr>
                                            <th>
                                                <asp:Label ID="LblCN" runat="server" Text="Card number"></asp:Label>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="cardNumber" runat="server"></asp:TextBox>
                                            </th>
                                        </tr>
                                        <tr>
                                            <th>
                                                <asp:Label ID="LblSC" runat="server" Text="CVV/CSC"></asp:Label>
                                            </th>
                                            <th>
                                                <asp:TextBox ID="cVVNumber"  runat="server"></asp:TextBox>
                                            </th>
                                        </tr>
                                        <tr>   
                                            <th>                        
                                                <asp:Label ID="LblExprie" runat="server" Text="Expiry date"></asp:Label>
                                            </th>
                                            <th>
                                                <asp:DropDownList ID="DropDownListMonth" runat="server" ></asp:DropDownList><asp:DropDownList ID="DropDownListYear" runat="server"></asp:DropDownList>
                                            </th>
                                        </tr>                                        
                                    </table>
                                    <div><asp:Label ID="LabelNoteMsg" runat="server" Text=""></asp:Label>                            </div>
                                    <div>
                                        <asp:Button ID="OkBtn" runat="server" Text="OK" OnClick= "startPurchase" class="btn btn-success" />
                                        <asp:Button ID="CancelBtn" runat="server" Text="Cancel" class="btn btn-success"/>
                                    </div>
                                    <div>
                                    </div>
                                </div>
                             </div>
                            <div></div>
                        </asp:Panel>

                        <%-- popup --%>

                    </ContentTemplate>
                 </asp:UpdatePanel>
             
         </div>

        </div>


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="pbsa" Runat="Server">
</asp:Content>

