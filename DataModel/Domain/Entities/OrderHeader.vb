Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("OrderHeader")>
Partial Public Class OrderHeader

    Public Property ID As Integer
    Public Property ReqNo As String
    Public Property DeliveryDate As Date
    Public Property OrderedBy As String
    Public Property DeliveryAddressID As Integer
    Public Property Price As Double?
    Public Property OrderStatus As String
    Public Property PONumber As String
    Public Property UserID As Integer
    Public Property Created As Date
    Public Property TotalQty As Integer
    Public Property Type As String
    Public Property MSNNo As String
    Public Property TrackingNo As String
    Public Property BoxQty As Integer?
    Public Property ConsolidationID As Integer?
    Public Property ReadyForDespatch As Boolean?

End Class
