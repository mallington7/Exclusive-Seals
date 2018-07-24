
Imports System.Globalization
Imports DataModel

Partial Class Order_Home
    Inherits System.Web.UI.Page

    Private OrderID As Integer = 0

    Private Sub Order_Home_Load(sender As Object, e As EventArgs) Handles Me.Load

        Backbtn.Attributes.Add("onClick", "javascript:history.back(); return false;")
        Try


            If Not User.Identity.IsAuthenticated Then
                Response.Redirect("~")
            End If

            If (Not DAModel.GetUserGroup(User.Identity.Name) = "CPI" And Not DAModel.GetUserGroup(User.Identity.Name) = "BA" And Not DAModel.GetUserGroup(User.Identity.Name) = "RRD") Then
                FormsAuthentication.SignOut()
                Session.Abandon()
                Response.Redirect("~")
            End If


            Dim db As New DataModel.PortalContext("portal")
            OrderID = Val(Request.Params("OrderID"))

            If OrderID > 0 Then

                ConfirmOrderbtn.Visible = True
                confirmdiv.visible = True
            Else
                ConfirmOrderbtn.Visible = False
                confirmDiv.Visible = False
            End If

            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Script", "StartUpPortal()", True)
            Dim Script = String.Format("<script>var lblMessage = '{0}';var ddlItems = '{1}';var TypeDD = '{2}';var MSNBx = '{3}';var MsnDiv = '{4}';var FileDiv = '{5}';var MSNFileDiv = '{6}';var MSNFilesDD = '{7}';</script>", lblMessage.ClientID, ddlItems.ClientID, TypeDD.ClientID, MSNBx.ClientID, FileDiv.ClientID, MsnDiv.ClientID, MSNFileDiv.ClientID, MSNFilesDD.ClientID)
            If Not Page.ClientScript.IsClientScriptBlockRegistered("myGlobalVariables") Then

                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "myGlobalVariables", Script)
            End If

            If Not IsPostBack Then
                ddlItems.DataSource = (From es In db.Assets Where es.Boeing = True).ToList
                ddlItems.DataTextField = "AssetName"
                ddlItems.DataValueField = "ID"
                ddlItems.DataBind()


                MSNFilesDD.DataSource = (From es In db.Assets Where es.Boeing = False).ToList
                MSNFilesDD.DataTextField = "AssetName"
                MSNFilesDD.DataValueField = "ID"
                MSNFilesDD.DataBind()



                AddressDetailsdd.DataSource = (From es In db.Addresses).ToList
                AddressDetailsdd.DataTextField = "AddressLine1"
                AddressDetailsdd.DataValueField = "ID"
                AddressDetailsdd.DataBind()
                loadOrderLines()

            End If


        Catch ex As Exception

        End Try
    End Sub

    Sub ConfirmOrder()

        Try


            Dim db As New DataModel.PortalContext("portal")
            Dim oheader As New OrderHeader

            If OrderID > 0 Then
                oheader = (From u In db.OrderHeaders Where u.ID = OrderID Select u).FirstOrDefault

                If oheader.OrderStatus <> "Confirmed" Then
                    Dim uti As New Utils
                    uti.SendEmail(oheader.ID, "Order Confirmation")
                End If
                oheader.OrderStatus = "Confirmed"
                oheader.DeliveryAddressID = AddressDetailsdd.SelectedValue
                db.SaveChanges()
                If oheader.OrderStatus = "Confirmed" Then
                    ConfirmOrderbtn.Enabled = False
                    Response.Redirect("~/Order/ProvisionalOrdersHome")
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub
    Sub loadOrderLines()

        Try

            Dim db As New DataModel.PortalContext("portal")
            OrderListGrid.DataSource = (From u In db.Orders Where u.OrderHeaderID = OrderID Order By u.OrderHeaderID Ascending).ToList
            OrderListGrid.DataBind()

            Dim orderheader = (From u In db.OrderHeaders Where u.ID = OrderID).FirstOrDefault


            RequisitionNumberbx.Text = orderheader.ReqNo
            DeliveryDatebx.Text = Date.ParseExact(orderheader.DeliveryDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)
            DeliveryDatebx.TextMode = TextBoxMode.DateTime
            AddressDetailsdd.SelectedValue = orderheader.DeliveryAddressID
            '  OrderStatusdd.SelectedValue = orderheader.OrderStatus
            TypeDD.SelectedValue = orderheader.Type
            MSNBx.Text = orderheader.MSNNo
            RequisitionNumberbx.ReadOnly = True
            DeliveryDatebx.ReadOnly = True
            'MSNBx.ReadOnly = True

            If orderheader.Type = "Boeing" Then
                TypeDD.Items.Remove("Airbus")
            Else
                TypeDD.Items.Remove("Boeing")
            End If


            If orderheader.OrderStatus = "Confirmed" Then
                ConfirmOrderbtn.Enabled = False
                AddressDetailsdd.Enabled = False
                '   OrderStatusdd.Enabled = False
                createbtn.Enabled = False
            End If

        Catch ex As Exception

        End Try

    End Sub
    Sub CreateOrder()


        Dim db As New DataModel.PortalContext("portal")
        lblMessage.Text = ""
        Dim AssetList

        If TypeDD.SelectedValue = "Boeing" Then
            AssetList = (From u In db.Assets Where u.id  = ddlItems.SelectedValue).FirstOrDefault

        Else
            AssetList = (From u In db.Assets Where u.ID = MSNFilesDD.SelectedValue).FirstOrDefault

        End If


        If (AssetList Is Nothing) Then
            lblMessage.Text = "Asset not found, order creation failed!"
            Exit Sub
        End If
        Dim oheader = New OrderHeader

        Dim userLoggedIN = (From u In db.Users Where u.UserName = User.Identity.Name.ToString).FirstOrDefault


        If OrderID > 0 Then
            oheader = (From u In db.OrderHeaders Where u.ID = OrderID).FirstOrDefault
        Else

            ' onblur="(this.type='text')"
            oheader.DeliveryDate = Date.Parse(DeliveryDatebx.Text)

            '  oheader.DeliveryDate = Date.ParseExact(DeliveryDatebx.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture)
            'oheader.ReqNo = RequisitionNumberbx.Text
            oheader.DeliveryAddressID = AddressDetailsdd.SelectedValue
            oheader.OrderStatus = "Provisional"
            oheader.Created = DateTime.Now
            oheader.UserID = userLoggedIN.ID
            oheader.MSNNo = MSNBx.Text
            oheader.Type = TypeDD.SelectedValue
            db.OrderHeaders.Add(oheader)
            db.SaveChanges()
            DeliveryDatebx.ReadOnly = True
        End If



        Dim Order As New Orders

        If Val(Copiesbx.Text) = 0 Then
            Copiesbx.Text = 1
        End If



        oheader.ReqNo = String.Format("{0}-{1}", oheader.ID, DateTime.Now.ToString("yy"))
        Order.Copies = Copiesbx.Text
        Order.ProofCopies = Val(ProofCopiesbx.Text)
        Order.OrderHeaderID = oheader.ID
        Order.FileName = AssetList.AssetName
        Order.ReqNo = oheader.ReqNo
        db.Orders.Add(Order)

        db.SaveChanges()
        CalculatePrice(oheader.ID)
        Response.Redirect("~/Order/NewOrder?OrderID=" & oheader.ID)

    End Sub


    Sub CalculatePrice(orderIDin As Integer)

        '        Print price incl Paper	0.07
        'Tril, Drill & Coil Bind	1.40
        'Insert Dividers	0.15
        'Remove Dividers	0.15
        'Extra for sample proof	40.00
        'Delivery    7.5

        Try

            Dim db As New DataModel.PortalContext("portal")
            Dim Orders = (From u In db.Orders Where u.OrderHeaderID = orderIDin).ToList
            Dim RunningPrice As Double = 0.0
            Dim TotalQty As Integer = 0

            For Each order In Orders
                Dim file = (From u In db.Assets Where u.AssetName = order.FileName).FirstOrDefault
                RunningPrice += Val(file.Tabs) * 0.45 'Insert/Remove Dividers
                RunningPrice += Val(file.PageCount) * 0.07 ' Print price incl Paper	0.07
                RunningPrice += 1.4 'Tril, Drill & Coil Bind
                RunningPrice += 7.5 'Delivery Price
                RunningPrice = RunningPrice * Val(order.Copies)
                RunningPrice += Val(order.ProofCopies) * 40.0
                TotalQty += Val(order.ProofCopies) + Val(order.Copies)
            Next

            If orderIDin > 0 Then
                Dim oheader = (From u In db.OrderHeaders Where u.ID = orderIDin).FirstOrDefault

                oheader.Price = RunningPrice
                oheader.TotalQty = TotalQty

            End If

            db.SaveChanges()

        Catch ex As Exception

        End Try
    End Sub


    Sub lnkEdit_Click(ByVal sender As Object, ByVal e As EventArgs)

        Dim OrderID = Val(Request.Params("OrderID"))
        Dim db As New DataModel.PortalContext("portal")

        Dim OrderHeader = (From u In db.OrderHeaders Where u.ID = OrderID).FirstOrDefault

        If Not OrderHeader.OrderStatus = "Confirmed" Then
            Dim btnsubmit As LinkButton = TryCast(sender, LinkButton)
            Dim gRow As GridViewRow = DirectCast(btnsubmit.NamingContainer, GridViewRow)
            Dim OrderLineID As Integer = 0
            OrderLineID = Val(gRow.Cells(0).Text)

            Dim Orders = (From u In db.Orders Where u.ID = OrderLineID).FirstOrDefault
            db.Orders.Remove(Orders)
            db.SaveChanges()

            CalculatePrice(OrderID)
            Response.Redirect("~/Order/NewOrder?OrderID=" & OrderID)

        End If
    End Sub
    Public LinkIsVisible As Boolean = False

    Sub HideLinkButton()
   
        Dim OrderID = Val(Request.Params("OrderID"))
        Dim db As New DataModel.PortalContext("portal")

        Dim Orders = (From u In db.OrderHeaders Where u.ID = OrderID).ToList

        For Each order In Orders
            If order.OrderStatus <> "Confirmed" Then
                LinkIsVisible = True
            End If
        Next

        LinkIsVisible = False

    End Sub


End Class
