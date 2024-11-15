Imports MySql.Data.MySqlClient

Public Class frmViewSales
    Public action As String = "add"
    Public salesID As String = ""
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        action = "add"
        frmSales.Show()
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        action = "update"
        getSalesID(True)
    End Sub

    Private Sub frmViewSales_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadData()
        cboStatus.Items.Add("Pending")
        cboStatus.Items.Add("Received")
        cboStatus.Items.Add("Cancelled")
    End Sub

    Public Sub LoadData(Optional searchQuery As String = "", Optional searchStatus As String = "")
        Try
            searchQuery = txtSearch.Text.Trim
            searchStatus = cboStatus.Text
            MyCon.Open()
            Dim query As String = "SELECT a.SalesID as `SALES ID`, a.ReferenceNo as `REFERENCE NO`, Count(b.ID) as `NO. OF PRODUCTS`, a.Amount as `TOTAL AMOUNT`, a.Description as `DESCRIPTION`, e.Name as `STATUS`, IF(a.UpdatedBy IS NULL, c.Username, d.Username) as `UPDATED BY`, IF(a.DateUpdated IS NULL, a.DateCreated, a.DateUpdated) as `LAST DATE UPDATED` FROM `sales` as a LEFT JOIN sales_products as b ON b.SalesID=a.SalesID LEFT JOIN users as c ON c.UserID=a.CreatedBy LEFT JOIN users as d ON d.UserID=a.UpdatedBy LEFT JOIN status as e ON e.StatusID=a.Status WHERE a.ReferenceNo Like @search"
            If Not cboStatus.SelectedItem Is Nothing Then
                query = query & " AND e.Name = @status "
            End If
            query = query & " GROUP BY a.SalesID"
            Dim command As New MySqlCommand(query, MyCon)
            command.Parameters.AddWithValue("@search", "%" & searchQuery & "%")
            If Not cboStatus.SelectedItem Is Nothing Then
                command.Parameters.AddWithValue("@status", searchStatus)
            End If
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
        getSalesID(True)
    End Sub

    Private Sub btnUpdateStatus_Click(sender As Object, e As EventArgs) Handles btnUpdateStatus.Click
        action = "update status"
        getSalesID(True)
    End Sub

    Private Sub getSalesID(showForm As Boolean)
        Dim iRowIndex As Integer
        Dim id As Integer
        Dim status As String = ""

        For i As Integer = 0 To Me.DataGridView1.SelectedCells.Count - 1
            iRowIndex = Me.DataGridView1.SelectedCells.Item(i).RowIndex
            id = DataGridView1.Item(0, iRowIndex).Value
            status = DataGridView1.Item(5, iRowIndex).Value
            salesID = id
        Next


        If ((status = "Pending") Or (status <> "Pending" And action = "view")) Then
            If showForm Then
                frmSales.Show()
            Else
                If action = "delete" Then
                    deleteSales()
                End If
            End If
        Else
            MessageBox.Show("Pending Sales Order is allowed to update", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        action = "delete"
        getSalesID(False)
    End Sub

    Private Sub deleteSales()
        If salesID <> "" Then
            Dim result = MessageBox.Show("Are you sure you want to delete this sales order?", "DELETE", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If result = DialogResult.No Then
                MessageBox.Show("Action Canceled.")
            ElseIf result = DialogResult.Yes Then
                MyCon.Open()
                MyCommand.CommandText = "UPDATE sales as P JOIN status as S ON (S.Name=@status) SET P.Status=S.StatusID, P.UpdatedBy=@updatedby WHERE P.salesID=@id"
                MyCommand.Parameters.Clear()
                MyCommand.Parameters.AddWithValue("@id", salesID)
                MyCommand.Parameters.AddWithValue("@status", "Deleted")
                MyCommand.Parameters.AddWithValue("@updatedby", Form1.sessionUser("UserID"))
                MyAdapter.UpdateCommand = MyCommand
                Dim success As Integer = MyCommand.ExecuteNonQuery()
                If success Then
                    MessageBox.Show("Sales Order status updated.", "Save Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
                MyCon.Close()
                LoadData()
            End If
        End If
    End Sub
End Class