Imports System
Imports System.Data.Entity
Imports System.Data.Entity.ModelConfiguration.Conventions
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Linq

Partial Public Class PortalContext
    Inherits DbContext


    Public Sub New()

        MyBase.New("name=portal")

    End Sub

    Public Sub New(connection As String)
        MyBase.New(connection)
    End Sub

    Public Overridable Property OrderHeaders As DbSet(Of OrderHeader)
    Public Overridable Property Orders As DbSet(Of Orders)
    Public Overridable Property Addresses As DbSet(Of Addresses)
    Public Overridable Property Users As DbSet(Of Users)
    Public Overridable Property Assets As DbSet(Of Assets)
    Public Overridable Property Triggers As DbSet(Of Triggers)
    Public Overridable Property Country As DbSet(Of Countries)

    Protected Overrides Sub OnModelCreating(ByVal modelBuilder As DbModelBuilder)


    End Sub


End Class
