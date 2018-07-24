Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("Country")>
Partial Public Class Countries
    <Key>
    Public Property CountryId As Integer
    Public Property Name As String
    Public Property IsoAlpha2 As String
    Public Property IsoAlpha3 As String
    Public Property IsoNumeric As String



End Class
