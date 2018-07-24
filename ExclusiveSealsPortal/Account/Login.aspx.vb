Imports System.Linq
Imports System.Web
Imports System.Web.UI


Partial Public Class Account_Login
    Inherits Page
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        '' RegisterHyperLink.NavigateUrl = "Register"
        '' OpenAuthLogin.ReturnUrl = Request.QueryString("ReturnUrl")
        'Dim returnUrl = HttpUtility.UrlEncode(Request.QueryString("ReturnUrl"))
        'If Not [String].IsNullOrEmpty(returnUrl) Then
        '    '     RegisterHyperLink.NavigateUrl += "?ReturnUrl=" & returnUrl
        'End If

        Response.Cache.SetNoStore()
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetNoServerCaching()
        Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches)
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1))
        Response.Cache.SetValidUntilExpires(False)

        Response.AddHeader("Pragma", "no-cache")

    End Sub


    Protected Sub LogIn(sender As Object, e As EventArgs)
        If IsValid Then
            ' Validate the user password
            '    Dim manager = New UserManager()
            Dim Portaluser As DataModel.Users = DAModel.GetUser(UserName.Text, Password.Text, Request.UserHostAddress)
            If Portaluser IsNot Nothing Then


                '   IdentityHelper.SignIn(manager, User, RememberMe.Checked)

                'IdentityHelper.RedirectToReturnUrl(Request.QueryString("ReturnUrl"), Response)
                Dim strEncTicket As String
                Dim ticket As New FormsAuthenticationTicket(1,
                                       UserName.Text,
                                        DateTime.Now,
                                        DateTime.Now.AddHours(480),
                                        False,
                                        "FMWeb" & "|" & UserName.Text & "|2|SomeOtherPara1m" & "|SomeOtherPara2m" & "|SomeOtherPara3m" & "|SomeOtherPara4m" & "|SomeOtherPara5m" & "|SomeOtherPar6am" & "|SomeOtherPa7ram")
                strEncTicket = FormsAuthentication.Encrypt(ticket)
                Dim faCookie As New HttpCookie(FormsAuthentication.FormsCookieName, strEncTicket)
                '   faCookie .Item ("MARK") = "mm"
                Response.Cookies.Add(faCookie)
                If Portaluser.Status = -1 Then
                    Response.Redirect("~/Account/Confirm")
                Else
                    Select Case Portaluser.UserGroup
                        Case "RRD"
                            Response.Redirect("~/Order/OrdersPOUpdate")
                        Case "BA"
                            Response.Redirect("~/Order/ProvisionalOrdersHome")
                        Case "CPI"
                            Response.Redirect("~/Order/CPIOrdersHome?Status=Provisional")
                    End Select
                End If


            Else
                FailureText.Text = "Invalid username or password."
                ErrorMessage.Visible = True
            End If
        End If
    End Sub
End Class
