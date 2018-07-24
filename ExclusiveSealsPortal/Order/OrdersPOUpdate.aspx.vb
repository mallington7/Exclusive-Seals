
Imports System.Globalization

Partial Class Order_Home
    Inherits System.Web.UI.Page

    Private Sub Order_Home_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not User.Identity.IsAuthenticated Then
            Response.Redirect("~")
        End If


        If (Not DAModel.GetUserGroup(User.Identity.Name) = "CPI" And Not DAModel.GetUserGroup(User.Identity.Name) = "RRD") Then
            FormsAuthentication.SignOut()
            Session.Abandon()
            Response.Redirect("~")
        End If


        If Not IsPostBack Then

            LoadOrders()
        Else

            '  LoadOrders(False)
        End If


    End Sub

    Sub LoadOrders(Optional firstload As Boolean = True)
        Dim db As New DataModel.PortalContext("portal")

        Dim SelectedOrderDate As Date = Today
        SelectedOrderDate = DateTime.Now.AddMonths(6)
        'If firstload Then
        '    SelectedOrderDate = DateTime.Now.AddMonths(6)
        'Else
        '    Try
        '        SelectedOrderDate = Date.ParseExact(CreatedDateSelection.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture)
        '    Catch ex As Exception

        '    End Try
        'End If



        OrderListGrid.DataSource = (From u In db.OrderHeaders Where u.OrderStatus = "Confirmed" And u.PONumber = Nothing And u.Created <= SelectedOrderDate Order By u.ID Descending).ToList
        OrderListGrid.DataBind()
    End Sub
    Sub update() Handles UpdateOrderbtn.Click

        Dim db As New DataModel.PortalContext("portal")

        For Each PORow As GridViewRow In OrderListGrid.Rows

            Dim CB_Control As CheckBox = CType(PORow.FindControl("CheckPO"), CheckBox)

            If (CB_Control.Checked) Then

                Dim OrderHeader As Integer = 0
                OrderHeader = Val(PORow.Cells(0).Text)

                Dim OrderLine = (From oh In db.OrderHeaders Where oh.ID = OrderHeader).FirstOrDefault

                OrderLine.PONumber = PONumberbx.Text
            End If




            db.SaveChanges()



        Next

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
