<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AssetUpload.aspx.vb"   MasterPageFile="~/Site.Master" Inherits="Order_Home" %>


<%@ Register assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
         


                 <dx:ASPxFileManager ID="PortalFileUpload" runat="server">
                     <Settings RootFolder="~/NewAssets/" ThumbnailFolder="~/Thumb/" />
                     <SettingsFileList View="Details">
                     </SettingsFileList>
                     <SettingsEditing AllowCreate="True" AllowDownload="True" AllowCopy="True" AllowDelete="True" AllowMove="True" AllowRename="True" />
                     <SettingsUpload AutoStartUpload="True">
                         <AdvancedModeSettings EnableMultiSelect="True">
                         </AdvancedModeSettings>
                     </SettingsUpload>
          </dx:ASPxFileManager>
            


         
    
     
</asp:Content>
