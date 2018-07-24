Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports System
Imports System.Collections.Generic

Public Partial Class Account_Manage
    Inherits System.Web.UI.Page
    Protected Property SuccessMessageText() As String
        Get
            Return m_SuccessMessage
        End Get
        Private Set(value As String)
            m_SuccessMessage = value
        End Set
    End Property
    Private m_SuccessMessage As String

    Protected Property CanRemoveExternalLogins() As Boolean
        Get
            Return m_CanRemoveExternalLogins
        End Get
        Private Set(value As Boolean)
            m_CanRemoveExternalLogins = value
        End Set
    End Property
    Private m_CanRemoveExternalLogins As Boolean

    Private Function HasPassword(manager As UserManager) As Boolean
        Dim appUser = manager.FindById(User.Identity.GetUserId())
        Return (appUser IsNot Nothing AndAlso appUser.PasswordHash IsNot Nothing)
    End Function

    Function ResolveUserGroup(PersonaIndex As Integer) As String
        Select Case (PersonaIndex)
            Case 0
                Return ("CPI")
            Case 1
                Return ("RRD")
            Case 2
                Return ("BA")
            Case Else
                Return ("CPI")
        End Select
    End Function

    Function ResolveUserLevel(PersonaGroup As String) As Integer
        Select Case (PersonaGroup)
            Case "CPI"
                Return 0
            Case "RRD"
                Return (1)
            Case "BA"
                Return (2)
            Case Else
                Return (0)
        End Select
    End Function

    Protected Sub Page_Load() Handles Me.Load
        '0 CPI
        '1 RRD
        '2 BA

        If (Not DAModel.GetUserLevel(User.Identity.Name) = "10") Then
            Response.Redirect("~/Default.aspx")
        Else

        End If


        If Not IsPostBack Then

            Dim db As New DataModel.PortalContext("portal")
            Dim UserLevel As Integer = 0

            UserLevel = ResolveUserLevel((From ul In db.Users Where ul.UserName = User.Identity.Name).FirstOrDefault.UserGroup)

            RadioPersonaList.SelectedValue = UserLevel


            '' Determine the sections to render
            'Dim manager = New UserManager()
            'If HasPassword(manager) Then
            '    changePasswordHolder.Visible = True
            'Else
            '    setPassword.Visible = True
            '    changePasswordHolder.Visible = False
            'End If
            'CanRemoveExternalLogins = manager.GetLogins(User.Identity.GetUserId()).Count() > 1

            '' Render success message
            'Dim message = Request.QueryString("m")
            'If message IsNot Nothing Then
            '    ' Strip the query string from action
            '    Form.Action = ResolveUrl("~/Account/Manage")
            '    SuccessMessageText = If(message = "ChangePwdSuccess", "Your password has been changed.", If(message = "SetPwdSuccess", "Your password has been set.", If(message = "RemoveLoginSuccess", "The account was removed.", [String].Empty)))
            '    successMessage.Visible = Not [String].IsNullOrEmpty(SuccessMessageText)
            'End If
        End If
    End Sub

    Sub updatePersonaRole() Handles UpdatePersona.Click


        Dim db As New DataModel.PortalContext("portal")

        Dim Usergroup As String = ResolveUserGroup(RadioPersonaList.SelectedValue)

        Dim userAccount = (From ul In db.Users Where ul.UserName = User.Identity.Name).FirstOrDefault

        userAccount.UserGroup = Usergroup

        db.SaveChanges()

        Response.Redirect(HttpContext.Current.Request.Url.ToString(), True)

    End Sub



    'Protected Sub ChangePassword_Click(sender As Object, e As EventArgs)
    '    If IsValid Then
    '        Dim manager = New UserManager()
    '        Dim result As IdentityResult = manager.ChangePassword(User.Identity.GetUserId(), CurrentPassword.Text, NewPassword.Text)
    '        If result.Succeeded Then
    '            Dim userInfo = manager.FindById(User.Identity.GetUserId())
    '            IdentityHelper.SignIn(manager, userInfo, isPersistent:=False)
    '            Response.Redirect("~/Account/Manage?m=ChangePwdSuccess")
    '        Else
    '            AddErrors(result)
    '        End If
    '    End If
    'End Sub

    'Protected Sub SetPassword_Click(sender As Object, e As EventArgs)
    '    If IsValid Then
    '        ' Create the local login info and link the local account to the user
    '        Dim manager = New UserManager()
    '        Dim result As IdentityResult = manager.AddPassword(User.Identity.GetUserId(), password.Text)
    '        If result.Succeeded Then
    '            Response.Redirect("~/Account/Manage?m=SetPwdSuccess")
    '        Else
    '            AddErrors(result)
    '        End If
    '    End If
    'End Sub

    Public Function GetLogins() As IEnumerable(Of UserLoginInfo)
        'Dim manager = New UserManager()
        'Dim accounts = manager.GetLogins(User.Identity.GetUserId())
        'CanRemoveExternalLogins = accounts.Count() > 1 Or HasPassword(manager)
        'Return accounts
    End Function

    Public Sub RemoveLogin(loginProvider As String, providerKey As String)
        Dim manager = New UserManager()
        Dim result = manager.RemoveLogin(User.Identity.GetUserId(), New UserLoginInfo(loginProvider, providerKey))
        Dim msg As String = String.Empty
        If result.Succeeded Then
            Dim userInfo = manager.FindById(User.Identity.GetUserId())
            IdentityHelper.SignIn(manager, userInfo, isPersistent:=False)
            msg = "?m=RemoveLoginSuccess"
        End If
        Response.Redirect("~/Account/Manage" & msg)
    End Sub

    Private Sub AddErrors(result As IdentityResult)
        For Each [error] As String In result.Errors
            ModelState.AddModelError("", [error])
        Next
    End Sub
End Class
