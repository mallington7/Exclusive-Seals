<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CPIOrdersHome.aspx.vb"   MasterPageFile="~/Site.Master" Inherits="Order_Home" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
      <articleb>
       <h2>Filter</h2>
    <asp:RadioButtonList ID="FilterRadioList" autopostback="true"   RepeatLayout="Flow" RepeatDirection="Horizontal" runat="server">
                        <asp:ListItem class="radio-inline" Value="0" Text="All" Selected="True"></asp:ListItem>
                        <asp:ListItem class="radio-inline" Value="1" Text="Confirmed"></asp:ListItem>
                         <asp:ListItem class="radio-inline" Value="2" Text="Printed"></asp:ListItem>
                     <asp:ListItem class="radio-inline" Value="3" Text="Finished"></asp:ListItem>
                     <asp:ListItem class="radio-inline" Value="4" Text="ToDespatch"></asp:ListItem>
                     <asp:ListItem class="radio-inline" Value="5" Text="Despatched"></asp:ListItem>
                    </asp:RadioButtonList>
     <div class="input-group">
                       <asp:TextBox class="form-control" id="PONumberbx" name="PONumberbx"  runat ="server"  autopostback="true" type="text" placeholder="Filter by OrderID"/>
         </div>
                 </articleb>

        <articleb ID="DespatchDetailsPod" visible="false" runat="server">
       <h2>Despatch Details</h2>
	       
               <div class="form-group">
          </div>    
            <asp:RadioButtonList ID="CarrierRadioList"   RepeatLayout="Flow" RepeatDirection="Horizontal" runat="server">
                        <asp:ListItem class="radio-inline" Value="0" Text="UPS" Selected="True"></asp:ListItem>
                        <asp:ListItem class="radio-inline" Value="1" Text="Other"></asp:ListItem>                
                    </asp:RadioButtonList>
                <div class="input-group">
       
                       <asp:TextBox class="form-control" id="BoxQtybx" name="BoxQtybx"  runat ="server" required="true"  type="text" placeholder="No. of Boxes"/>
                <span class="input-group-btn">
                    <asp:Button   margin-left= "10px" runat="server" ID ="Despatchbtn"  Text="Despatch" CssClass="btn btn-primary" />
                    </span>
                    </div>
            </articleb>

       <articleb>


       <h2>Orders</h2>
	       
   
           
                     <asp:GridView ID="OrderListGrid"   runat="server" AllowPaging="True"  CssClass="table table-hover table-striped" GridLines="None" 
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
                  <asp:BoundField DataField="ReqNo" HeaderText="ReqNo"  />   
                  <asp:BoundField DataField="MSNNo" HeaderText="MSNNo"  />   
                         <asp:BoundField DataField="Type" HeaderText="Type"  />     

					<asp:TemplateField HeaderText="View Order"  visible="false">

					<ItemTemplate>
		<asp:LinkButton ID="lnkEdit" Text="View"  OnClick="lnkEdit_Click" visible="false" runat="server"></asp:LinkButton>
	</ItemTemplate>



	</asp:TemplateField>


                
                  <asp:TemplateField HeaderText="Trap Order">
					<ItemTemplate >
		<asp:LinkButton runat="server" ID="lnktrap"  Text="Trap"  OnClick="lnkEdit_ClickTrap"></asp:LinkButton>
	</ItemTemplate> 
	</asp:TemplateField>
                </Columns>
            </asp:GridView>

                     
                     <asp:GridView ID="DespatchingGrid" visible="false"  runat="server" AllowPaging="True"  CssClass="table table-hover table-striped" GridLines="None" 
 AutoGenerateColumns="False"              
                AllowSorting="True" CellSpacing="3" PageSize="100">
             <Columns>
                    
                                 <asp:BoundField DataField="ID" visible="true" HeaderText="CPI Order ID" />
                             <asp:BoundField DataField="OrderStatus" visible="true" HeaderText="Order Status" />
                         <asp:BoundField DataField="Created" HeaderText="Created" DataFormatString="{0:dd-MMM-yyyy}" />
                               <asp:BoundField DataField="DeliveryDate" HeaderText="DeliveryDate" DataFormatString="{0:dd-MMM-yyyy}" />
                     <asp:BoundField DataField="Price" HeaderText="Price"  />
                   <asp:BoundField DataField="TotalQty" HeaderText="Order Qty"  />
                 
                     <asp:BoundField DataField="PONumber" HeaderText="PONumber"  />   
                  <asp:BoundField DataField="ReqNo" HeaderText="ReqNo"  />   
                  <asp:BoundField DataField="MSNNo" HeaderText="MSNNo"  />   
                         <asp:BoundField DataField="Type" HeaderText="Type"  />     
 
					<asp:TemplateField HeaderText="Despatch"  visible="True">

                     <ItemTemplate>
                                <asp:CheckBox ID="DespatchChk" runat="server" checked ="false"  />
                            </ItemTemplate> 


	</asp:TemplateField>

                </Columns>
            </asp:GridView>

            
                     <asp:GridView ID="DespatchedGrid" visible="false"  runat="server" AllowPaging="True"  CssClass="table table-hover table-striped" GridLines="None" 
 AutoGenerateColumns="False"              
                AllowSorting="True" CellSpacing="3" PageSize="100">
             <Columns>
                    
                                 <asp:BoundField DataField="ID" visible="true" HeaderText="CPI Order ID" />
                             <asp:BoundField DataField="OrderStatus" visible="true" HeaderText="Order Status" />
                         <asp:BoundField DataField="Created" HeaderText="Created" DataFormatString="{0:dd-MMM-yyyy}" />
                               <asp:BoundField DataField="DeliveryDate" HeaderText="DeliveryDate" DataFormatString="{0:dd-MMM-yyyy}" />
                     <asp:BoundField DataField="Price" HeaderText="Price"  />
                   <asp:BoundField DataField="TotalQty" HeaderText="Order Qty"  />
                 
                     <asp:BoundField DataField="PONumber" HeaderText="PONumber"  />   
                  <asp:BoundField DataField="ReqNo" HeaderText="ReqNo"  />   
                  <asp:BoundField DataField="MSNNo" HeaderText="MSNNo"  />   
                         <asp:BoundField DataField="Type" HeaderText="Type"  />     
                                <asp:BoundField DataField="TrackingNo" HeaderText="TrackingNo" visible ='true' /> 
                                     <asp:BoundField DataField="BoxQty" HeaderText="Box Qty" visible ='true' /> 
					<asp:TemplateField HeaderText="View Order"  visible="false">

                        
					<ItemTemplate>
		<asp:LinkButton ID="lnkEdit" Text="View"  OnClick="lnkEdit_Click" visible="false" runat="server"></asp:LinkButton>
	</ItemTemplate>



	</asp:TemplateField>


                </Columns>
            </asp:GridView>

</articleb>
    </asp:Content>
