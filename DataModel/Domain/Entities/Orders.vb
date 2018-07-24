Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("Orders")>
Partial Public Class Orders

    Public Property ID As Integer
    Public Property Copies As Integer
    Public Property ProofCopies As Integer
    Public Property FileName As String
    Public Property OrderHeaderID As Integer
    Public Property ReqNo As String


End Class
