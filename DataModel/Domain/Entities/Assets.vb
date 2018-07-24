Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("Assets")>
Partial Public Class Assets

    Public Property ID As Integer
    Public Property AssetName As String
    Public Property PageCount As Integer
    Public Property Status As Integer
    Public Property Tabs As Integer
    Public Property AssetSize As Double
    Public Property Boeing As Boolean?

End Class
