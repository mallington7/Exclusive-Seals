
Imports System.Data
Imports System.Globalization
Imports DataModel
Imports DevExpress.Web

Partial Class Order_Home
    Inherits System.Web.UI.Page

    Private Sub Order_Home_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not User.Identity.IsAuthenticated Then
            Response.Redirect("~")
        End If


        If (Not DAModel.GetUserGroup(User.Identity.Name) = "CPI") Then
            FormsAuthentication.SignOut()
            Session.Abandon()
            Response.Redirect("~")
        End If

        LoadFiles()




    End Sub

    'Populate Grid
    Sub LoadFiles()
        Dim db As New DataModel.PortalContext("portal")

        FileListGrid.DataSource = (From fi In db.Assets Order By fi.ID Descending).ToList
        FileListGrid.DataBind()

    End Sub

    'Delete selected Row

    Protected Sub grid_RowDeleting(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataDeletingEventArgs)

        Dim db As New DataModel.PortalContext("portal")
        Dim AssetId As Integer = Val(e.Keys(0))


        Dim AssetToDelete = (From Ass In db.Assets Where Ass.ID = AssetId).FirstOrDefault

        If Not AssetToDelete Is Nothing Then
            db.Assets.Remove(AssetToDelete)
            db.SaveChanges()
        End If

        LoadFiles()
        e.Cancel = True

    End Sub

    'Add New Row

    Protected Sub grid_RowInserting(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataInsertingEventArgs)

        Dim grid As ASPxGridView = TryCast(sender, ASPxGridView)

        Try

            Dim db As New DataModel.PortalContext("portal")
            Dim NewAsset As New Assets

            NewAsset.AssetName = e.NewValues("AssetName")
            NewAsset.PageCount = e.NewValues("PageCount")
            NewAsset.Tabs = e.NewValues("Tabs")
            NewAsset.Boeing = e.NewValues("Boeing")

            db.Assets.Add(NewAsset)

            db.SaveChanges()


        Catch ex As Exception
            Dim s = 5
        End Try

        LoadFiles()
        e.Cancel = True

        grid.CancelEdit()

    End Sub


    'Update selected Row
    Protected Sub grid_RowUpdating(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataUpdatingEventArgs)

        Dim grid As ASPxGridView = TryCast(sender, ASPxGridView)
        Dim AssetId As Integer = Val(e.Keys(0))

        Try

            Dim db As New DataModel.PortalContext("portal")

        Dim AssetToEdit = (From Ass In db.Assets Where Ass.ID = AssetId).FirstOrDefault

        If Not AssetToEdit Is Nothing Then

                Try
                    AssetToEdit.AssetName = e.NewValues("AssetName")
                    AssetToEdit.PageCount = e.NewValues("PageCount")
                    AssetToEdit.Tabs = e.NewValues("Tabs")
                    AssetToEdit.Boeing = e.NewValues("Boeing")


                Catch ex As Exception

                End Try

                db.SaveChanges()
            End If

            LoadFiles()
            e.Cancel = True

            grid.CancelEdit()


        Catch ex As Exception

        End Try

    End Sub

    'Init new row
    Protected Sub grid_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataInitNewRowEventArgs)

    End Sub


End Class
