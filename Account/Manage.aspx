<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Manage.aspx.cs" Inherits="Account_Manage" %>
<%@ MasterType VirtualPath="~/Site.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="container">
        <div class="jumbotron">
            <h1>User Information</h1>
            <p>Detail about customer</p> 
        </div>
        <div>
            <asp:UpdatePanel ID="udpDetail" runat="server">
                <ContentTemplate>
<%--                    <asp:Literal ID="LiteralMessages" runat="server"></asp:Literal>--%>
                    <%-- new version detailview --%>
                    <asp:DetailsView ID="dvAccountDetail" runat="server" AutoGenerateRows="False" HeaderText="Personal Information" CssClass="table table-hover">
                        <AlternatingRowStyle BackColor="White" />
                        <EditRowStyle />
                        <Fields>

                         <asp:TemplateField>
                               <HeaderTemplate>CustomerID</HeaderTemplate>
                               <ItemTemplate>
                                   <%#Eval("customerid") %>
                               </ItemTemplate>
                           </asp:TemplateField>

                            <asp:TemplateField>
                               <HeaderTemplate>FirstName</HeaderTemplate>
                               <ItemTemplate>
                                   <%#Eval("firstname") %>
                               </ItemTemplate>
                               <EditItemTemplate>                                                       
                                   <asp:TextBox ID="txtfirstname" runat="server"  Text='<%#Eval("firstname") %>' CssClass="form-control"/>
                               </EditItemTemplate>
                           </asp:TemplateField>

                            <asp:TemplateField>
                               <HeaderTemplate>LastName</HeaderTemplate>
                               <ItemTemplate>
                                   <%#Eval("lastname") %>
                               </ItemTemplate>
                               <EditItemTemplate>
                                   <asp:TextBox ID="txtlastname" runat="server"  Text='<%#Eval("lastname") %>' CssClass="form-control"/>
                               </EditItemTemplate>
                           </asp:TemplateField>

                           <asp:TemplateField>
                               <HeaderTemplate>Email</HeaderTemplate>
                               <ItemTemplate>
                                   <%#Eval("email") %>
                               </ItemTemplate>
                               <EditItemTemplate>
                                   <asp:TextBox ID="txtemail" runat="server"  Text='<%#Eval("email") %>' CssClass="form-control"/>
                               </EditItemTemplate>
                           </asp:TemplateField>

                           <asp:TemplateField>
                               <HeaderTemplate>WebSite</HeaderTemplate>
                               <ItemTemplate>
                                   <%#Eval("website") %>
                               </ItemTemplate>
                               <EditItemTemplate>
                                   <asp:TextBox ID="txtwebsite" runat="server"  Text='<%#Eval("website") %>' CssClass="form-control"/>
                               </EditItemTemplate>
                           </asp:TemplateField>

                           <asp:TemplateField>
                               <HeaderTemplate>Date of Birth</HeaderTemplate>
                               <ItemTemplate>
                                   <%#Eval("dob") %>
                               </ItemTemplate>
                               <EditItemTemplate>
                                   <asp:TextBox ID="txtdob" runat="server"  text='<%#Eval("dob") %>' CssClass="form-control" ClientIDMode="Static"/>                                   
                               </EditItemTemplate>
                           </asp:TemplateField>

                            <asp:TemplateField>
                               <HeaderTemplate>MemberCard</HeaderTemplate>
                               <ItemTemplate>
                                   <%#Eval("membercard") %>
                               </ItemTemplate>
                            </asp:TemplateField>         

                            <asp:TemplateField>
                               <HeaderTemplate>LoyaltyMember</HeaderTemplate>
                               <ItemTemplate>
                                   <asp:CheckBox ID="CheckBoxLoyaltyMember" runat="server" Enabled="false" Checked='<%# Convert.ToBoolean(Eval("loyaltymember"))%>' />
                               </ItemTemplate>
                            </asp:TemplateField>   

                            <asp:TemplateField>
                               <HeaderTemplate>Phone</HeaderTemplate>
                               <ItemTemplate>
                                   <%#Eval("phone") %>
                               </ItemTemplate>
                               <EditItemTemplate>
                                   <asp:TextBox ID="txtphone" runat="server"  Text='<%#Eval("phone") %>' CssClass="form-control"/>
                               </EditItemTemplate>
                            </asp:TemplateField>   

                           <asp:TemplateField>
                               <HeaderTemplate>Mobile</HeaderTemplate>
                               <ItemTemplate>
                                    <%#Eval("mobile") %>
                               </ItemTemplate>
                               <EditItemTemplate>
                                    <asp:TextBox ID="txtmobile" runat="server"  Text='<%#Eval("mobile") %>' CssClass="form-control"/>
                               </EditItemTemplate>
                           </asp:TemplateField>   

                            <asp:TemplateField>
                               <HeaderTemplate>Fax</HeaderTemplate>
                               <ItemTemplate>
                                   <%#Eval("fax") %>
                               </ItemTemplate>
                               <EditItemTemplate>
                                   <asp:TextBox ID="txtfax" runat="server"  Text='<%#Eval("fax") %>' CssClass="form-control"/>
                               </EditItemTemplate>
                            </asp:TemplateField>     

                           <asp:TemplateField>
                               <ItemTemplate>
                                   <asp:LinkButton ID="LinkButtonEdit" runat="server" OnClick="dvEditRecord_click"><span class="glyphicon glyphicon-pencil"></span></asp:LinkButton>                                                     
                               </ItemTemplate>
                               <EditItemTemplate>
                                   <asp:LinkButton ID="LinkButtonUpdate" runat="server" OnClick="dvEditUpdate_click"><span class="glyphicon glyphicon-ok"></span></asp:LinkButton> <asp:LinkButton ID="LinkCancel" runat="server" OnClick="dvEditCancel_click"><span class="glyphicon glyphicon-remove"></span></asp:LinkButton>                                                     
                               </EditItemTemplate>
                           </asp:TemplateField>

                        </Fields>
                        <FooterStyle BackColor="#CCCC99" />
                        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                        <RowStyle BackColor="#F7F7DE" />
                    </asp:DetailsView>

                    <%-- old version gridview --%>                    
                    <asp:GridView ID="gvAccountDetail" runat="server" Width="100%"
                         AutoGenerateColumns="False" CssClass="table table-responsive" DataKeyNames="customerid"  >
                        <%--OnRowEditing="EditRecord_click" OnRowCancelingEdit="EditCancel_click" OnRowUpdating="EditUpdate_click"--%>
                        <Columns>
                           <asp:TemplateField>
                               <HeaderTemplate>CustomerID</HeaderTemplate>
                               <ItemTemplate>
                                   <%#Eval("customerid") %>
                               </ItemTemplate>
                           </asp:TemplateField>

                            <asp:TemplateField>
                               <HeaderTemplate>FirstName</HeaderTemplate>
                               <ItemTemplate>
                                   <%#Eval("firstname") %>
                               </ItemTemplate>
                               <EditItemTemplate>                                                       
                                   <asp:TextBox ID="txtfirstname" runat="server"  Text='<%#Eval("firstname") %>' Width=""/>
                               </EditItemTemplate>
                           </asp:TemplateField>

                            <asp:TemplateField>
                               <HeaderTemplate>LastName</HeaderTemplate>
                               <ItemTemplate>
                                   <%#Eval("lastname") %>
                               </ItemTemplate>
                               <EditItemTemplate>
                                   <asp:TextBox ID="txtlastname" runat="server"  Text='<%#Eval("lastname") %>' Width=""/>
                               </EditItemTemplate>
                           </asp:TemplateField>

                           <asp:TemplateField>
                               <HeaderTemplate>Email</HeaderTemplate>
                               <ItemTemplate>
                                   <%#Eval("email") %>
                               </ItemTemplate>
                               <EditItemTemplate>
                                   <asp:TextBox ID="txtemail" runat="server"  Text='<%#Eval("email") %>' Width=""/>
                               </EditItemTemplate>
                           </asp:TemplateField>

                           <asp:TemplateField>
                               <HeaderTemplate>WebSite</HeaderTemplate>
                               <ItemTemplate>
                                   <%#Eval("website") %>
                               </ItemTemplate>
                               <EditItemTemplate>
                                   <asp:TextBox ID="txtwebsite" runat="server"  Text='<%#Eval("website") %>' Width=""/>
                               </EditItemTemplate>
                           </asp:TemplateField>

                           <asp:TemplateField>
                               <HeaderTemplate>Date of Birth</HeaderTemplate>
                               <ItemTemplate>
                                   <%#Eval("dob") %>
                               </ItemTemplate>
                               <EditItemTemplate>
                                   <asp:TextBox ID="gvtxtdob" runat="server"  Text='<%#Eval("dob") %>' Width=""/>
                               </EditItemTemplate>
                           </asp:TemplateField>

                            <asp:TemplateField>
                               <HeaderTemplate>MemberCard</HeaderTemplate>
                               <ItemTemplate>
                                   <%#Eval("membercard") %>
                               </ItemTemplate>
                            </asp:TemplateField>         

                            <asp:TemplateField>
                               <HeaderTemplate>LoyaltyMember</HeaderTemplate>
                               <ItemTemplate>
                                   <asp:CheckBox ID="CheckBoxLoyaltyMember" runat="server" Enabled="false" Checked='<%# Convert.ToBoolean(Eval("loyaltymember"))%>' />
                               </ItemTemplate>
                            </asp:TemplateField>   

                            <asp:TemplateField>
                               <HeaderTemplate>Phone</HeaderTemplate>
                               <ItemTemplate>
                                   <%#Eval("phone") %>
                               </ItemTemplate>
                               <EditItemTemplate>
                                   <asp:TextBox ID="txtphone" runat="server"  Text='<%#Eval("phone") %>' Width=""/>
                               </EditItemTemplate>
                            </asp:TemplateField>   

                           <asp:TemplateField>
                               <HeaderTemplate>Mobile</HeaderTemplate>
                               <ItemTemplate>
                                    <%#Eval("mobile") %>
                               </ItemTemplate>
                               <EditItemTemplate>
                                    <asp:TextBox ID="txtmobile" runat="server"  Text='<%#Eval("mobile") %>' Width=""/>
                               </EditItemTemplate>
                           </asp:TemplateField>   

                            <asp:TemplateField>
                               <HeaderTemplate>Fax</HeaderTemplate>
                               <ItemTemplate>
                                   <%#Eval("fax") %>
                               </ItemTemplate>
                               <EditItemTemplate>
                                   <asp:TextBox ID="txtfax" runat="server"  Text='<%#Eval("fax") %>' Width=""/>
                               </EditItemTemplate>
                            </asp:TemplateField>                           
                           <asp:TemplateField>
                               <ItemTemplate>
                                   <asp:LinkButton ID="LinkButtonEdit" runat="server" OnClick="EditRecord_click"><span class="glyphicon glyphicon-pencil"></span></asp:LinkButton>                                                     
                               </ItemTemplate>
                               <EditItemTemplate>
                                   <asp:LinkButton ID="LinkButtonUpdate" runat="server" OnClick="EditUpdate_click"><span class="glyphicon glyphicon-ok"></span></asp:LinkButton> <asp:LinkButton ID="LinkCancel" runat="server" OnClick="EditCancel_click"><span class="glyphicon glyphicon-remove"></span></asp:LinkButton>                                                     
                               </EditItemTemplate>
                           </asp:TemplateField>

                        </Columns>
                    </asp:GridView>
                    <%--<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:customerdatabaseConnectionString %>" SelectCommand="SELECT * FROM [customer]"></asp:SqlDataSource>--%>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pbsa" Runat="Server">
</asp:Content>

