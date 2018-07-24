<%@ Page Title="Contact" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Contact.aspx.vb" Inherits="Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <h3>Your contact page.</h3>
    <address>
       Your address or CPI return
    </address>

    <address>
        <strong>Support:</strong>   <a href="mailto:mallington@cpibooks.co.uk">mallington@cpibooks.co.uk</a><br />
        <strong>Marketing:</strong> <a href="mailto:ctaylor@cpibooks.co.uk">ctaylor@cpibooks.co.uk</a>
    </address>
</asp:Content>
