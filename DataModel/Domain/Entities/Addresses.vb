Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("Addresses")>
Partial Public Class Addresses

    Public Property ID As Integer
    Public Property AddressLine1 As String
    Public Property AddressLine2 As String
    Public Property AddressLine3 As String
    Public Property AddressLine4 As String
    Public Property Town As String
    Public Property County As String
    Public Property Postcode As String
    Public Property Country As String


End Class
