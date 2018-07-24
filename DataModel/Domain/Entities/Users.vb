Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("Users")>
Partial Public Class Users

    Public Property ID As Integer
    Public Property UserName As String
    Public Property Password As String
    Public Property UserGroup As String
    Public Property UserLevel As String
    Public Property Status As Integer
    Public Property LastLoggedInTime As DateTime
    Public Property ClientIP As String
    Public Property FirstName As String
    Public Property LastName As String



End Class
