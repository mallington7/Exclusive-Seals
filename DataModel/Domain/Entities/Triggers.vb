Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("Triggers")>
Partial Public Class Triggers

    Public Property ID As Integer
    Public Property Action As String
    Public Property OrderHeaderID As Integer
    Public Property TriggerStatus As Integer
    Public Property Created As DateTime
    Public Property Actioned As DateTime?
    Public Property Arg1 As String
    Public Property Arg2 As String
    Public Property Arg3 As String
    Public Property Arg4 As String


End Class
