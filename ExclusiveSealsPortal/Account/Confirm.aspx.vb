Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.AspNet.Identity.Owin
Imports System
Imports System.Linq
Imports System.Web
Imports System.Web.UI

Public Partial Class Account_Register
    Inherits Page

    Private Sub AccountConfirm_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not User.Identity.IsAuthenticated Then
            Response.Redirect("~")
        End If
        UserName.Text = Context.User.Identity.Name
        UserName.Enabled = False
    End Sub
    Protected Sub CreateUser_Click(sender As Object, e As EventArgs)
        Dim manager = New UserManager()
        Dim user = New ApplicationUser() With {.UserName = UserName.Text}
        Dim result = manager.Create(user, Password.Text)
        If result.Succeeded Then
            'IdentityHelper.SignIn(manager, user, isPersistent:=False)
            'IdentityHelper.RedirectToReturnUrl(Request.QueryString("ReturnUrl"), Response)
            DAModel.UpdateUserPassword(UserName.Text, Password.Text, Request.UserHostAddress)
            FormsAuthentication.SignOut()
            Session.Abandon()
            Response.Redirect("~")
        Else
            If result.Errors.FirstOrDefault().Contains("taken") Then
                ErrorMessage.Text = "Account already confirmed"
            Else
                ErrorMessage.Text = result.Errors.FirstOrDefault()
            End If

        End If

        'If IsValid Then
        '    Dim manager = New UserManager()
        '    Dim result As IdentityResult = manager.ChangePassword(Context.User.Identity.Name, "mnimaih2c", "mnimaih2c!")
        '    If result.Succeeded Then
        '        Dim userInfo = manager.FindById(User.Identity.GetUserId())
        '        IdentityHelper.SignIn(manager, userInfo, isPersistent:=False)
        '        Response.Redirect("~/Account/AccountConfirm?m=ChangePwdSuccess")
        '    Else
        '        Response.Redirect("~/Account/AccountConfirm?m=ChangePwdSuccess")
        '        ErrorMessage.Text = result.Errors.FirstOrDefault()
        '    End If
        'End If
    End Sub
End Class
