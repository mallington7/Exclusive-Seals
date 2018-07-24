<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AssetAdmin.aspx.vb"   MasterPageFile="~/Site.Master" Inherits="Order_Home" %>


<%@ Register assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

          <articleb>
       <h2>File Admin</h2>
              <dx:ASPxGridView ID="FileListGrid"  SettingsSearchPanel-Visible="True" runat="server" KeyFieldName ="ID" OnRowDeleting="grid_RowDeleting" 

                            OnRowInserting="grid_RowInserting" OnRowUpdating="grid_RowUpdating" OnInitNewRow="grid_InitNewRow">
                    <Columns>

                   <dx:GridViewCommandColumn VisibleIndex="0" ShowSelectCheckbox="True" ShowEditButton="True" ShowDeleteButton ="True" ShowNewButton="True" ShowUpdateButton="True"  >
                <FooterTemplate>
                    <dx:ASPxButton ID="buttonDel" AutoPostBack="false" runat="server" Text="Delete">
                        <ClientSideEvents Click="OnClickButtonDel"/>
                    </dx:ASPxButton>
                </FooterTemplate>
            </dx:GridViewCommandColumn>
                           <dx:GridViewDataTextColumn FieldName="ID" ReadOnly="True" >
            </dx:GridViewDataTextColumn>

                           <dx:GridViewDataTextColumn FieldName="AssetName"  >
            </dx:GridViewDataTextColumn>
                                                   <dx:GridViewDataTextColumn FieldName="PageCount" >
            </dx:GridViewDataTextColumn>
                                                   <dx:GridViewDataTextColumn FieldName="Tabs" >
            </dx:GridViewDataTextColumn>
                                                   <dx:gridviewdatacheckcolumn FieldName="Boeing">
            </dx:gridviewdatacheckcolumn>
            

</Columns>
 <Settings ShowFooter="False" />

           </dx:ASPxGridView>
          
               <div class="form-group">

                 </articleb>
    
     
</asp:Content>
