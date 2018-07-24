
Imports System.Globalization

Partial Class Order_Home
    Inherits System.Web.UI.Page

    Public ShowUPScolumn As Boolean

    Private Sub Order_Home_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not User.Identity.IsAuthenticated Then
            Response.Redirect("~")
        End If

        If Not IsPostBack Then

            If (Not DAModel.GetUserGroup(User.Identity.Name) = "CPI" And Not DAModel.GetUserGroup(User.Identity.Name) = "RRD") Then
                FormsAuthentication.SignOut()
                Session.Abandon()
                Response.Redirect("~")
            End If

        Else
            'IIf(ShowUPScolumn <> "", True, False)

        End If

        LoadOrders()
    End Sub

    Protected Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles OrderListGrid.RowDataBound

        'look for delivered flag by using the value in tracking no
        'Try
        '    If e.Row.Cells(3).Text = "" Then

        '    End If
        'Catch ex As Exception

        'End Try

    End Sub

    Sub LoadOrders()
        Dim db As New DataModel.PortalContext("portal")

        Dim SelectedOrderDate As Date = Today

        Dim OrderIDSearch As Integer = 0

        Try
            OrderIDSearch = Val(OrderIDbx.Text)
        Catch ex As Exception
            OrderIDSearch = 0
        End Try

        If OrderIDSearch = 0 Then
            OrderListGrid.DataSource = (From u In db.OrderHeaders).ToList
        Else
            OrderListGrid.DataSource = (From u In db.OrderHeaders Where u.ID = OrderIDSearch Or u.PONumber.Contains(OrderIDSearch)).ToList
        End If


        OrderListGrid.DataBind()
    End Sub

    Sub SearchByID() Handles SearchOrderID.Click
        LoadOrders()
    End Sub


    Protected Sub lnkEdit_Click(ByVal sender As Object, ByVal e As EventArgs)

        Dim btnsubmit As LinkButton = TryCast(sender, LinkButton)
        Dim gRow As GridViewRow = DirectCast(btnsubmit.NamingContainer, GridViewRow)
        Dim jobno As String = ""
        jobno = gRow.Cells(0).Text
        Response.Redirect("~/Order/NewOrder?OrderID=" & jobno)

    End Sub

    Protected Sub CheckAll_Click()
        'Enumerate each GridViewRow
        For Each gvr As GridViewRow In OrderListGrid.Rows
            'Programmatically access the CheckBox from the TemplateField
            Dim cb As CheckBox = CType(gvr.FindControl("UpdateChk"), CheckBox)

            'Check it!
            cb.Checked = True
        Next
    End Sub

End Class
