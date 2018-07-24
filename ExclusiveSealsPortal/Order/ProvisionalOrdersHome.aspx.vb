
Partial Class Order_Home
    Inherits System.Web.UI.Page

    Private Sub Order_Home_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not User.Identity.IsAuthenticated Then
            Response.Redirect("~")
        End If

        If (Not DAModel.GetUserGroup(User.Identity.Name) = "CPI" And Not DAModel.GetUserGroup(User.Identity.Name) = "BA") Then
            FormsAuthentication.SignOut()
            Session.Abandon()
            Response.Redirect("~")
        End If

        Dim db As New DataModel.PortalContext("portal")

        OrderListGrid.DataSource = (From u In db.OrderHeaders Where u.OrderStatus = "Provisional" Order By u.ID Descending).ToList
        OrderListGrid.DataBind()

    End Sub

    Protected Sub lnkEdit_Click(ByVal sender As Object, ByVal e As EventArgs)

        Dim btnsubmit As LinkButton = TryCast(sender, LinkButton)
        Dim gRow As GridViewRow = DirectCast(btnsubmit.NamingContainer, GridViewRow)
        Dim jobno As String = ""
        jobno = gRow.Cells(0).Text
        Response.Redirect("~/Order/NewOrder?OrderID=" & jobno)

    End Sub
End Class
