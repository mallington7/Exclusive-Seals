Imports DataModel
Imports CPI.Services.Email.Sender


Module Module1

    Sub Main()


        'Dim QtyInBox As Integer = 10
        'Dim BoxesPerPallet As Integer = 40
        'Dim OverallQty As Integer = 505
        'Dim NoOfPallets As Integer = 0
        'Dim FullPallet As Integer = BoxesPerPallet * QtyInBox
        'Dim QtyOnThisPallet = FullPallet

        'NoOfPallets = (Math.Ceiling(Math.Ceiling(OverallQty / QtyInBox) / BoxesPerPallet))

        'If NoOfPallets < 2 Then
        '    'then just  overall qty is on pallet
        '    'create
        'Else

        '    For x = 1 To NoOfPallets
        '        'Fill the pallet if possible
        '        'createPallet(QtyOnThisPallet)
        '        QtyOnThisPallet -= OverallQty

        '    Next
        'End If


        'For x = 1 To NoOfPallets
        '    'Fill the pallet if possible
        '    'create 1st pallet

        '    QtyOnThisPallet -= FullPallet
        'Next
        ''  CreateUser()

        ''  SendEmail()

        UploadSpreadsheet()
    End Sub

    'Sub SendEmail()
    '    Dim toemail As New List(Of String)
    '    Dim db As New DataModel.PortalContext("portal")

    '    Dim orderheader = (From oh In db.OrderHeaders Where oh.ID = 82).FirstOrDefault

    '    Dim orderlines = (From ol In db.Orders Where ol.OrderHeaderID = 82).ToList()

    '    Dim data As New CPI.Services.Email.Models.OrderPromptRRD

    '    toemail.Add("mallington7@gmail.com")
    '    toemail.Add("mallington@cpibooks.co.uk")

    '    data.Order = orderheader
    '    data.OrderLines = orderlines
    '    data.PromptType = "Order Confirmation"

    '    SendEmailUpdate(String.Format("{0} {1}", data.PromptType, data.Order.ReqNo), toemail, toemail, toemail, Nothing, data, "")
    'End Sub

    Sub UploadSpreadsheet()

        Dim SS As String()

        SS = System.IO.File.ReadAllLines("C:\temp\qrh.csv")

        Dim db As New DataModel.PortalContext("portal")

        For Each title In SS

            Dim ass = New Assets
            ass.AssetName = title.Split(",")(0)
            ass.PageCount = title.Split(",")(1)
            ass.Tabs = title.Split(",")(2)
            ass.Boeing = False
            db.Assets.Add(ass)
        Next

        db.SaveChanges()

    End Sub
    Sub CreateUser()

        Dim db As New DataModel.PortalContext("portal")

        Dim oheader = New OrderHeader

        oheader.DeliveryAddressID = 56
        oheader.DeliveryDate = Date.Now
        db.OrderHeaders.Add(oheader)

        Dim us As New Users

        us.UserName = "ctaylor@cpibooks.co.uk"
        us.Password = DataModel.util.ReturnEncodedPassword("cp1p0rtal")
        us.LastLoggedInTime = DateTime.Now
        us.UserGroup = "CPI"
        us.UserLevel = 10
        us.ClientIP = "::1"
        us.Status = 0


        db.Users.Add(us)

        db.SaveChanges()



    End Sub



End Module
