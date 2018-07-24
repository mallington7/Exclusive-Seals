Imports System.Configuration
Imports System.IO
Imports System.Xml
Imports System.Data.Common
Imports System.Data
Imports System.Data.SqlClient
Imports System
Imports System.Security.Cryptography
Imports System.Web.Script.Serialization


Public Class DAModel

    Shared Function GetUser(username As String, password As String, IPAddress As String) As DataModel.Users


        Dim db As New DataModel.PortalContext("portal")
        Dim user As New DataModel.Users
        password = DataModel.util.ReturnEncodedPassword(password)

        user = (From u In db.Users Where u.UserName = username And u.Password = password).FirstOrDefault

        If user Is Nothing Then

            user = (From u In db.Users Where u.UserName = username).FirstOrDefault
            If Not user Is Nothing Then
                'Getting password incorrect, could be a brute force attack
                user.Status += 1
                db.SaveChanges()
                user = Nothing

            End If

        Else
            user.LastLoggedInTime = Date.Now
            user.ClientIP = IPAddress
            If user.Status <> -1 Then
                user.Status = 0
            End If

            db.SaveChanges()
        End If
        Return user

    End Function


    Shared Function GetAuthenticatedUser(username As String) As DataModel.Users


        Dim db As New DataModel.PortalContext("portal")
        Dim user As New DataModel.Users

        user = (From u In db.Users Where u.UserName = username).FirstOrDefault

        Return user

    End Function


    Shared Sub UpdateUserPassword(username As String, password As String, IPAddress As String)


        Dim db As New DataModel.PortalContext("portal")
        Dim user As New DataModel.Users
        password = DataModel.util.ReturnEncodedPassword(password)

        user = (From u In db.Users Where u.UserName = username).FirstOrDefault

        If user IsNot Nothing Then
            user.LastLoggedInTime = Date.Now
            user.ClientIP = IPAddress
            user.Status = 0
            user.Password = password
            db.SaveChanges()

        End If

    End Sub

    Shared Function GetUserGroup(username As String) As String


        Dim db As New DataModel.PortalContext("portal")
        Dim group As String = (From u In db.Users Where u.UserName = username And u.Status = 0).FirstOrDefault.UserGroup

        Return group

    End Function


    Shared Function GetUserLevel(username As String) As String


        Dim db As New DataModel.PortalContext("portal")
        Dim UserLevel As String = (From u In db.Users Where u.UserName = username And u.Status = 0).FirstOrDefault.UserLevel

        Return UserLevel

    End Function



End Class






