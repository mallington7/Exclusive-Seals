<%@ Page Language="VB" AutoEventWireup="false" CodeFile="OrdersPOUpdate.aspx.vb"   MasterPageFile="~/Site.Master" Inherits="Order_Home" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

          <articleb>
       <h2>Update PO Numbers</h2>
               <div class="form-group">
          </div>
                <div class="input-group">

                       <asp:TextBox class="form-control" id="PONumberbx" name="PONumberbx"  runat ="server" required="true"  type="text" placeholder="PO Number"/>
                <span class="input-group-btn">
                    <asp:Button   margin-left= "10px" runat="server" ID ="UpdateOrderbtn"  Text="Update Orders" CssClass="btn btn-primary" />
                    </span>
                    </div>
                 </articleb>
    
       <articleb>
       <h2>Orders</h2>
	       
    
                     <asp:GridView ID="OrderListGrid"    runat="server" AllowPaging="True"  CssClass="table table-hover table-striped" GridLines="None" 
 AutoGenerateColumns="False"              
                AllowSorting="True" CellSpacing="3" PageSize="100">
             <Columns>
                    
                                 <asp:BoundField DataField="ID" visible="true" HeaderText="CPI Order ID" />
                          <asp:BoundField DataField="ReqNo" visible="true" HeaderText="Requisition No." />
                                 <asp:BoundField DataField="OrderStatus" visible="true" HeaderText="Order Status" />
                         <asp:BoundField DataField="Created" HeaderText="Created" DataFormatString="{0:dd-MMM-yyyy}" />
                               <asp:BoundField DataField="DeliveryDate" HeaderText="DeliveryDate" DataFormatString="{0:dd-MMM-yyyy}" />
                     <asp:BoundField DataField="Price" HeaderText="Price"  />

                    <asp:TemplateField >
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckPO" runat="server" checked ="true"  />
                            </ItemTemplate> 
                        </asp:TemplateField>
		
                </Columns>
            </asp:GridView>
   </articleb>
	
    </asp:Content>
