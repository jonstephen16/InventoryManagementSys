Imports System.Linq
Imports MySql.Data.MySqlClient
Public Class frmViewPurchase
    Public action As String = "add"
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        action = "add"
        frmAddPurchase.Show()
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        action = "update"
        frmAddPurchase.Show()
    End Sub

    Private Sub frmViewPurchase_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadData()
    End Sub

    Private Sub LoadData(Optional searchQuery As String = "")
        Try
            searchQuery = txtSearch.Text.Trim
            MyCon.Open()
            Dim command As New MySqlCommand("SELECT a.PurchaseID as `PURCHASE ID`, a.ReferenceNo as `REFERENCE NO`, Count(b.ID) as `NO. OF PRODUCTS`, a.Amount as `TOTAL AMOUNT`, a.Description as `DESCRIPTION`, IF(a.UpdatedBy IS NULL, c.Username, d.Username) as `UPDATED BY`, IF(a.DateUpdated IS NULL, a.DateCreated, a.DateUpdated) as `LAST DATE UPDATED` FROM `purchases` as a LEFT JOIN purchase_products as b ON b.PurchaseID=a.PurchaseID LEFT JOIN users as c ON c.UserID=a.CreatedBy LEFT JOIN users as d ON d.UserID=a.UpdatedBy WHERE a.ReferenceNo Like @search GROUP BY a.PurchaseID;", MyCon)
            command.Parameters.AddWithValue("@search", "%" & searchQuery & "%")
            Dim adapter As New MySqlDataAdapter(command)
            Dim table As New DataTable()
            adapter.Fill(table)
            DataGridView1.DataSource = table
            MyCon.Close()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try

    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        LoadData()
    End Sub

    Private Sub btnView_Click(sender As Object, e As EventArgs) Handles btnView.Click
        action = "view"
        frmAddPurchase.Show()
    End Sub
End Class