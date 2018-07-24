Imports CPI.Services.Email
Imports DataModel
Imports CPI.Services.Email.Sender


Public Class Utils

    Sub SendEmail(OrderHeaderID As Integer, EmailType As String)

        Try
            Dim db As New DataModel.PortalContext("portal")
            Dim trig As New Triggers

            trig.Action = "SendEmail"
            trig.Created = DateTime.Now
            trig.OrderHeaderID = OrderHeaderID
            trig.Arg1 = EmailType
            trig.TriggerStatus = 0
            db.Triggers.Add(trig)
            db.SaveChanges()
        Catch ex As Exception

        End Try



    End Sub


End Class
