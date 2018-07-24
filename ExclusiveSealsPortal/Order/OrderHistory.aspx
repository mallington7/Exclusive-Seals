<%@ Page Language="VB" AutoEventWireup="false" CodeFile="OrderHistory.aspx.vb"   MasterPageFile="~/Site.Master" Inherits="Order_Home" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

          <articleb>
       <h2>Filter</h2>           
                <div class="input-group">

                       <asp:TextBox class="form-control" id="OrderIDbx" name="OrderIDbx"  runat ="server"   type="text" placeholder="Order ID"/>
                <span class="input-group-btn">
                    <asp:Button   margin-left= "10px" runat="server" ID ="SearchOrderID"  Text="Search" CssClass="btn btn-primary" />
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
                   <asp:BoundField DataField="TotalQty" HeaderText="Order Qty"  />
                 
                     <asp:BoundField DataField="PONumber" HeaderText="PONumber"  />   
                  <%--<asp:BoundField DataField="ReqNo" HeaderText="ReqNo"  />--%>   
                  <asp:BoundField DataField="MSNNo" HeaderText="MSNNo"  />   
                         <asp:BoundField DataField="Type" HeaderText="Type"  />     
                                <asp:BoundField DataField="TrackingNo" HeaderText="TrackingNo" visible ='true' /> 
					<asp:TemplateField HeaderText="View Order"  visible="false">

					<ItemTemplate>
		<asp:LinkButton ID="lnkEdit" Text="View"  OnClick="lnkEdit_Click" visible="false" runat="server"></asp:LinkButton>
	</ItemTemplate>
	</asp:TemplateField>
                  <%--<asp:TemplateField HeaderText="Update?">
                        <ItemTemplate>
                            <asp:checkbox ID="UpdateChk" checked ="true"  runat="server" Visible ="false" Width="20px"  />
                        </ItemTemplate>
                    </asp:TemplateField>--%>

                </Columns>
            </asp:GridView>
   </articleb>
	
    </asp:Content>
