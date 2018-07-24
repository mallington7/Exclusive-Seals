<%@ Page Language="VB" AutoEventWireup="false" CodeFile="NewOrdersHome.aspx.vb"   MasterPageFile="~/Site.Master" Inherits="Order_Home" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
       <articleb>
       <h2>Orders</h2>
	       
    
                     <asp:GridView ID="OrderListGrid"    runat="server" AllowPaging="True"  CssClass="table table-hover table-striped" GridLines="None" 
 AutoGenerateColumns="False"              
                AllowSorting="True" CellSpacing="3" PageSize="100">
             <Columns>
                    
                                 <asp:BoundField DataField="ID" visible="true" HeaderText="CPI Order ID" />
                                    <asp:BoundField DataField="ReqNo" visible="true" HeaderText="Requisition No." />
                               <asp:BoundField DataField="DeliveryDate" HeaderText="DeliveryDate" DataFormatString="{0:dd-MMM-yyyy}" />
                     <asp:BoundField DataField="Price" HeaderText="Price"  />
					<asp:TemplateField HeaderText="View Order">
					<ItemTemplate>
		<asp:LinkButton ID="lnkEdit" Text="View"  OnClick="lnkEdit_Click" runat="server"></asp:LinkButton>
	</ItemTemplate>
	</asp:TemplateField>
                </Columns>
            </asp:GridView>
   </articleb>
	
    </asp:Content>
