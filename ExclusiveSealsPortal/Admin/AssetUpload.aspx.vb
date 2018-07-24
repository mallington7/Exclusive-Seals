
Imports System.Data
Imports System.Globalization
Imports DataModel
Imports DevExpress.Web

Partial Class Order_Home
    Inherits System.Web.UI.Page

    Private Sub Order_Home_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not User.Identity.IsAuthenticated Then
            Response.Redirect("~")
        End If


        If (DAModel.GetUserGroup(User.Identity.Name) = "BA" Or DAModel.GetUserGroup(User.Identity.Name) = "CPI") Then
        Else
            FormsAuthentication.SignOut()
            Session.Abandon()
            Response.Redirect("~")
        End If



    End Sub




End Class
