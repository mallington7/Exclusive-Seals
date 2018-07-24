<%@ Page Language="VB" AutoEventWireup="false" CodeFile="NewOrder.aspx.vb"    MasterPageFile="~/Site.Master" Inherits="Order_Home" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
       <div class="btn-group">

    <asp:Button runat="server" ID ="Backbtn" Text="< Back" CssClass="btn-link" />
           </div>

    
       <articleb id ="confirmDiv" runat="server">
              <h2>Confirm Order</h2>
                     <asp:GridView ID="OrderListGrid"   runat="server" AllowPaging="False"  CssClass="table table-hover table-striped" GridLines="None" 
 AutoGenerateColumns="False"              
                AllowSorting="True" CellSpacing="3" PageSize="100">
            <Columns>
                    
                                 <%--<asp:BoundField DataField="OrderHeaderID" visible="true" HeaderText="CPI Order ID" />--%>
                                <asp:BoundField DataField="ReqNo" visible="true" HeaderText="Requisition No." />
                        <asp:BoundField DataField="FileName" HeaderText="FileName"  />
                    <asp:BoundField DataField="Copies" HeaderText="Copies"  />
                 <asp:BoundField DataField="ProofCopies" HeaderText="ProofCopies"  />
					<asp:TemplateField HeaderText="">
					<ItemTemplate>
		<asp:LinkButton ID="lnkEdit" Text="Delete"  OnClick="lnkEdit_Click"  runat="server"></asp:LinkButton>
	</ItemTemplate>
	</asp:TemplateField>
                </Columns>
            </asp:GridView>

               <div class="btn-group">
                            <asp:Button runat="server" ID="ConfirmOrderbtn" OnClick="ConfirmOrder" Text="Confirm" CssClass="btn btn-primary" />
                        </div>

              </articleb>

    <articleb>

       <h2>Order Header</h2>
	
    <div class="form-group">
		<asp:TextBox class="form-control" id="RequisitionNumberbx" readonly="true" runat ="server" name= "RequisitionNumberbx" type="text" placeholder="Requisition Number"/>
   </div>

          <div class="form-group">
                    <asp:DropDownList ID="TypeDD" runat="server" class = "form-control" onchange ="ShowHideMSNFileList()">
    <asp:ListItem Text="Boeing" Value="Boeing"></asp:ListItem>
   <asp:ListItem Text="Airbus" Value="Airbus"></asp:ListItem></asp:DropDownList>
              </div>
           <div class="form-group" id="FileDiv" style="display:none" runat ="server">
                     <asp:DropDownList  ID="ddlItems" runat="server"   class = "form-control" >

    </asp:DropDownList>
               </div>
                   <div class="form-group" id="MsnDiv" style="display:none;" runat ="server">
                		<asp:TextBox class="form-control" id="MSNBx" readonly="false"  onkeyup = "FilterItems(this.value)" runat ="server" type="text" placeholder="MSN No."/>
            </div>

 <div class="form-group" id="MSNFileDiv" style="display:none" runat ="server">

                     <asp:DropDownList  ID="MSNFilesDD" runat="server"   class = "form-control" >

    </asp:DropDownList>
         <asp:Label ID="lblMessage" class = "label label-default" runat="server" ></asp:Label>
               </div>


            <div class="form-group">

     
                </div>


        <div class="form-group">
        <asp:TextBox class="form-control" id="DeliveryDatebx" required="true" name="DeliveryDatebx"  runat ="server"  type="text" onfocus="(this.type='date')" placeholder="Delivery Date"/>
           </div>

         


          <div class="form-group">
     <asp:DropDownList ID="AddressDetailsdd" required="true"  runat="server" class = "form-control" >
    </asp:DropDownList>
           </div>
       
            </articleb>

    
    <articleb>
       <h2>Order Details</h2>
     
      
        <div class="form-group">
                     <asp:TextBox class="form-control" id="Copiesbx"  name="Copiesbx"  runat ="server" min="1" type="number" placeholder="Copies" />
            </div>
        <div class="form-group">
              <asp:TextBox class="form-control" id="ProofCopiesbx" name="ProofCopiesbx"  runat ="server" min ="0"  type="number" placeholder="Proof Copies"/>
            </div>
              
      <div class="form-group form-inline">
    <asp:TextBox ID="txtSearch" placeholder="MSN No"  visible="false" class = "form-control" runat="server"

         onkeyup = "FilterItems(this.value)"></asp:TextBox><br />




            <asp:Label ID="lblMesg" class = "label label-default" runat="server" ></asp:Label>
</div>

         <div class="btn-group">
                            <asp:Button runat="server" ID="createbtn" OnClick="CreateOrder" Text="Add" CssClass="btn btn-primary" />
                        </div>

          </articleb>  

	
    </asp:Content>

