
Imports DataModel

Partial Class Order_Home
    Inherits System.Web.UI.Page

    Public TrapOption As String = "Trap"

    Private Sub Order_Home_Load(sender As Object, e As EventArgs) Handles Me.Load


        Try



            If Not User.Identity.IsAuthenticated Then
                Response.Redirect("~")
            End If

            If (Not DAModel.GetUserGroup(User.Identity.Name) = "CPI" And Not DAModel.GetUserGroup(User.Identity.Name) = "RRD") Then
                FormsAuthentication.SignOut()
                Session.Abandon()
                Response.Redirect("~")
            End If


        Catch ex As Exception

        End Try

        If Not IsPostBack Then
            LoadOrders()
        End If



    End Sub




    Sub Despatch() Handles Despatchbtn.Click

        Dim db As New DataModel.PortalContext("portal")
        Dim ConsolidateID As Integer = 0

        For Each DespatchRow As GridViewRow In DespatchingGrid.Rows

            Dim CB_Control As CheckBox = CType(DespatchRow.FindControl("DespatchChk"), CheckBox)

            If (CB_Control.Checked) Then


                Dim OrderHeaderID As Integer = 0
                OrderHeaderID = Val(DespatchRow.Cells(0).Text)

                If ConsolidateID = 0 Then
                    ConsolidateID = OrderHeaderID
                End If

                Dim order = (From ord In db.OrderHeaders Where ord.ID = OrderHeaderID).FirstOrDefault

                order.OrderStatus = "Despatched"
                order.ConsolidationID = ConsolidateID
                order.BoxQty = Val(IIf(Val(BoxQtybx.Text) = 0, 1, Val(BoxQtybx.Text)))
                db.SaveChanges()

            End If


        Next

        If CarrierRadioList.SelectedItem.Text = "UPS" Then
            DespatchTrigger(ConsolidateID, True)
        Else
            DespatchTrigger(ConsolidateID, False)

        End If

        BoxQtybx.Text = ""

        LoadOrders()


    End Sub

    Sub LoadOrders()

        Dim OrderID = Val(Request.Params("OrderID"))
        Dim status = Request.Params("Status")
        OrderListGrid.Visible = True
        DespatchingGrid.Visible = False
        despatchedgrid.Visible = False
        DespatchDetailsPod.Visible = False

        If status = "" Then
            status = "All"
        End If
        Try
            Dim db As New DataModel.PortalContext("portal")

            If Not IsPostBack Then
                If OrderID = 0 Then
                    FilterRadioList.SelectedValue = ResolveStatusFromIndex(status)
                End If

            Else
                status = FilterRadioList.SelectedItem.Text
                FilterRadioList.SelectedValue = ResolveStatusFromIndex(status)
            End If



            If Not PONumberbx.Text = "" Then
                Dim orderidbx As String = ""
                orderidbx = PONumberbx.Text
                PONumberbx.Text = ""
                Dim HeaderID As Integer = Val(orderidbx)
                FilterRadioList.SelectedValue = ResolveStatusFromIndex((From u In db.OrderHeaders Where u.ID = HeaderID).FirstOrDefault.OrderStatus)
                Response.Redirect("~/Order/CPIOrdersHome?OrderID=" & orderidbx)

            End If


            If Val(OrderID) = 0 Then
                If status = "All" Then
                    OrderListGrid.DataSource = (From u In db.OrderHeaders Order By u.ID Descending).ToList
                Else
                    OrderListGrid.DataSource = (From u In db.OrderHeaders Where u.OrderStatus = status Order By u.ID Descending).ToList
                End If

            Else

                OrderListGrid.DataSource = (From u In db.OrderHeaders Where u.ID = OrderID Order By u.ID Descending).ToList
                FilterRadioList.SelectedValue = ResolveStatusFromIndex((From u In db.OrderHeaders Where u.ID = OrderID).FirstOrDefault.OrderStatus)
            End If


            If status = "ToDespatch" Then
                OrderListGrid.Visible = False
                DespatchingGrid.Visible = True
                DespatchedGrid.Visible = False
                DespatchDetailsPod.visible = True

                If Val(OrderID) = 0 Then
                    If status = "All" Then
                        DespatchingGrid.DataSource = (From u In db.OrderHeaders Order By u.ID Descending).ToList
                    Else
                        DespatchingGrid.DataSource = (From u In db.OrderHeaders Where u.OrderStatus = status Order By u.ID Descending).ToList
                    End If

                Else

                    DespatchingGrid.DataSource = (From u In db.OrderHeaders Where u.ID = OrderID Order By u.ID Descending).ToList
                    FilterRadioList.SelectedValue = ResolveStatusFromIndex((From u In db.OrderHeaders Where u.ID = OrderID).FirstOrDefault.OrderStatus)
                End If
                DespatchingGrid.DataBind()

            ElseIf status = "Despatched" Then
                OrderListGrid.Visible = False
                DespatchingGrid.Visible = False
                DespatchedGrid.Visible = True
                If Val(OrderID) = 0 Then
                    If status = "All" Then
                        DespatchedGrid.DataSource = (From u In db.OrderHeaders Order By u.ID Descending).ToList
                    Else
                        DespatchedGrid.DataSource = (From u In db.OrderHeaders Where u.OrderStatus = status Order By u.ID Descending).ToList
                    End If

                Else

                    DespatchedGrid.DataSource = (From u In db.OrderHeaders Where u.ID = OrderID Order By u.ID Descending).ToList
                    FilterRadioList.SelectedValue = ResolveStatusFromIndex((From u In db.OrderHeaders Where u.ID = OrderID).FirstOrDefault.OrderStatus)
                End If
                DespatchedGrid.DataBind()
            Else

                OrderListGrid.DataBind()

            End If
        Catch ex As Exception

        End Try

    End Sub

    Public Sub changedStatus() Handles FilterRadioList.SelectedIndexChanged

        Response.Redirect("~/Order/CPIOrdersHome?Status=" & FilterRadioList.SelectedItem.Text)
    End Sub

    Protected Sub lnkEdit_Click(ByVal sender As Object, ByVal e As EventArgs)

        Dim btnsubmit As LinkButton = TryCast(sender, LinkButton)
        Dim gRow As GridViewRow = DirectCast(btnsubmit.NamingContainer, GridViewRow)
        Dim jobno As String = ""
        jobno = gRow.Cells(0).Text
        Response.Redirect("~/Order/NewOrder?OrderID=" & jobno)

    End Sub

    Protected Sub lnkEdit_ClickTrap(ByVal sender As Object, ByVal e As EventArgs)

        Dim btnsubmit As LinkButton = TryCast(sender, LinkButton)
        Dim gRow As GridViewRow = DirectCast(btnsubmit.NamingContainer, GridViewRow)
        Dim OrderID As Integer = 0
        OrderID = Val(gRow.Cells(0).Text)

        Dim db As New DataModel.PortalContext("portal")

        Dim order = (From ord In db.OrderHeaders Where ord.ID = OrderID).FirstOrDefault
        Dim Status = gRow.Cells(2).Text


        Select Case order.OrderStatus

            Case "Finished"

                Try

                    Dim txtbox As TextBox = TryCast(sender, TextBox)
                    Dim qtyRow As GridViewRow = DirectCast(btnsubmit.NamingContainer, GridViewRow)

                    Dim tb As TextBox = qtyRow.FindControl("boxqtybx")
                    Dim boxqty = Val(tb.Text)
                    order.BoxQty = Val(IIf(boxqty > 0, boxqty, 1))

                Catch ex As Exception

                End Try


                'Let's dispatch this thing

            Case "Printed"
                'Mark As finished
                order.ReadyForDespatch = True
        End Select

        order.OrderStatus = GetNextStatus(Status)

        db.SaveChanges()

        LoadOrders()

    End Sub

    '
    Sub DespatchTrigger(OrderID As Integer, UPS As Boolean)

        Dim db As New DataModel.PortalContext("portal")
        Dim trig As New Triggers

        trig.Action = IIf(UPS, "UPSDespatch", "OtherDespatch")
        trig.Created = DateTime.Now
        trig.OrderHeaderID = OrderID
        trig.Arg1 = "11"
        trig.TriggerStatus = 0
        db.Triggers.Add(trig)
        db.SaveChanges()

    End Sub


    Function ResolveStatusbyIndex(statusIndex As Integer) As String
        Select Case (statusIndex)
            Case 0
                Return ("All")
            Case 1
                Return ("Confirmed")
            Case 2
                Return ("Printed")
            Case 3
                Return ("Finished")
            Case 4
                Return ("ToDespatch")
            Case 5
                Return ("Despatched")
            Case Else
                Return ("Provisional")
        End Select
    End Function

    Function ResolveStatusFromIndex(Status As String) As Integer
        Select Case (Status)
            Case "All"
                Return 0
            Case "Confirmed"
                Return 1
            Case "Printed"
                Return 2
            Case "Finished"
                Return 3
            Case "ToDespatch"
                Return 4
            Case "Despatched"
                Return 5
            Case Else
                Return 0
        End Select
    End Function

    Function GetNextStatus(Status As String) As String
        Select Case (Status)
            Case "Provisional"
                Return "Confirmed"
            Case "Confirmed"
                Return "Printed"
            Case "Printed"
                Return "Finished"
            Case "Finished"
                Return "ToDespatch"
            Case "ToDespatch"
                Return "Despatched"
            Case Else
                Return ""
        End Select
    End Function


End Class
