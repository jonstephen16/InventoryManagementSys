Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MySql.Data.MySqlClient
Public Class frmProd
    Private isGenerateSku As Boolean = True
    Private action As String = "add"
    Private Sub btnAddProd_Click(sender As Object, e As EventArgs) Handles btnAddProd.Click
        If ValidateFields() Then
            If action = "add" Then
                Dim sql = "INSERT INTO `products` ( ProductID, Sku, Name, Description, Category, SellingPrice, Unit, CreatedBy) VALUES ('" & txtId.Text & "', '" & txtSku.Text.Trim & "','" & txtProdname.Text.Trim & "','" & txtDescription.Text.Trim & "','" & cboCategory.Text & "','" & txtPrice.Text & "','" & cboUnit.Text & "', '" & Form1.sessionUser("UserID") & "')"
                createNoMsg(sql)
                MessageBox.Show(txtProdname.Text & " has been saved to the database.", "Save Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)
                MyCon.Close()
            ElseIf action = "update" Then
                Dim sql = "UPDATE `products` SET Sku = '" & txtSku.Text.Trim & "', Name = '" & txtProdname.Text.Trim & "', Description = '" & txtDescription.Text.Trim & "', Category = '" & cboCategory.Text & "', SellingPrice = '" & txtPrice.Text & "', Unit = '" & cboUnit.Text & "', UpdatedBy = '" & Form1.sessionUser("UserID") & "' WHERE ProductID = '" & txtId.Text & "'"
                createNoMsg(sql)
                MessageBox.Show(txtProdname.Text & " has been saved to the database.", "Save Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)
                MyCon.Close()
            Else
                ''
            End If

            'Dim terminate = MessageBox.Show("Do you want to add another product?", "Confirm Message.", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            'If terminate = DialogResult.Yes Then
            'btnAddProd.PerformClick()
            'Else
            'End If
            ClearFields()
            clearText()
            generateSku()
            lockObject()
            LoadData()
        Else
            MessageBox.Show("Please fill in all required fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            LoadData()
        End If
    End Sub

    Private Function ValidateFields() As Boolean
        Return Not (String.IsNullOrEmpty(txtId.Text) Or String.IsNullOrEmpty(txtProdname.Text) Or String.IsNullOrEmpty(txtDescription.Text) Or cboCategory.SelectedItem Is Nothing Or String.IsNullOrEmpty(txtPrice.Text) Or cboUnit.SelectedItem Is Nothing)
    End Function

    Private Sub ClearFields()
        txtId.Clear()
        txtSku.Clear()
        txtProdname.Clear()
        txtDescription.Clear()
        txtPrice.Clear()
        cboCategory.SelectedIndex = -1
        cboUnit.SelectedIndex = -1
    End Sub

    Private Sub LoadData(Optional searchQuery As String = "")
        Try
            searchQuery = txtSearch.Text.Trim
            MyCon.Open()
            Dim command As New MySqlCommand("SELECT ProductID as `PRODUCT ID`, Sku as `SKU`, P.Name as `NAME`, Category as `CATEGORY`, SellingPrice as `SELLING PRICE`, Unit as `UNIT`, S.Name as `STATUS`, IF(DateUpdated IS NULL, DateCreated, DateUpdated) as `LAST DATE UPDATED` FROM `products` as P  INNER JOIN status as S ON S.StatusID = P.Status WHERE P.Name LIKE '%" & searchQuery & "%' OR P.ProductID = '" & searchQuery & "'", MyCon)
            Dim adapter As New MySqlDataAdapter(command)
            Dim table As New DataTable()
            adapter.Fill(table)
            DataGridView1.DataSource = table
            MyCon.Close()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try

    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Dim iRowIndex As Integer
        Dim productID As Integer

        For i As Integer = 0 To Me.DataGridView1.SelectedCells.Count - 1
            iRowIndex = Me.DataGridView1.SelectedCells.Item(i).RowIndex
            productID = DataGridView1.Item(0, iRowIndex).Value

            MyCon.Open()
            MyCommand.Connection = MyCon
            MyCommand.CommandText = "SELECT *  FROM products WHERE ProductID='" & productID & "'"
            MyAdapter.SelectCommand = MyCommand
            Dim MySQLData As MySqlDataReader = MyCommand.ExecuteReader
            While MySQLData.Read()
                txtId.Text = MySQLData("ProductID").ToString()
                txtSku.Text = MySQLData("Sku").ToString()
                txtProdname.Text = MySQLData("Name").ToString()
                txtDescription.Text = MySQLData("Description").ToString()
                cboCategory.SelectedIndex = cboCategory.FindStringExact(MySQLData("Category").ToString())
                txtPrice.Text = MySQLData("SellingPrice").ToString()
                cboUnit.SelectedIndex = cboUnit.FindStringExact(MySQLData("Unit").ToString())
            End While
            MyCon.Close()
        Next
    End Sub

    Public Function GetNextProductID() As Integer
        Dim nextID As Integer = 1
        Try
            If MyCon.State = ConnectionState.Open Then
                MyCon.Close()
            End If
            MyCon.Open()

            Dim query As String = "SELECT MAX(ProductID) FROM `products`"
            MyCommand = New MySqlCommand(query, MyCon)
            Dim result = MyCommand.ExecuteScalar()

            If IsDBNull(result) = False Then
                nextID = Convert.ToInt32(result) + 1
            End If

            MyCon.Close()
        Catch ex As Exception
            MsgBox("Error fetching next Product ID: " & ex.Message)
        End Try

        Return nextID
    End Function

    Private Sub frmProd_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Automatically set the Product ID textbox with the next available ID
        txtId.Text = GetNextProductID().ToString()
        ' Disable the textbox to prevent editing
        txtId.Enabled = False
        txtSku.Enabled = False
        txtProdname.Enabled = False
        txtDescription.Enabled = False
        cboCategory.Enabled = False
        txtPrice.Enabled = False
        cboUnit.Enabled = False
        btnAddProd.Enabled = False

        LoadData()
        displayCategory()
        displayUnit()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If (MyCon.State = ConnectionState.Open) Then MyCon.Close()
            Dim result = MessageBox.Show("Are you sure you want to delete this product?", "DELETE", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If result = DialogResult.No Then
                MessageBox.Show("Action Canceled.")

                Exit Sub

            ElseIf result = DialogResult.Yes Then
                strsql = "DELETE FROM `products` WHERE ProductID = '" & txtId.Text & "'"
                Dim da As New MySqlDataAdapter(strsql, MyCon)
                da.Fill(ds)
                MsgBox("Record was deleted successfully", MsgBoxStyle.Information, "Delete Product Success")
                MyCon.Close()
                DataGridView1.ClearSelection()
                txtId.Text = ""
                txtSku.Text = ""
                txtProdname.Text = ""
                txtDescription.Text = ""
                txtPrice.Text = ""
                cboCategory.SelectedIndex = -1
                cboUnit.SelectedIndex = -1

            End If
            LoadData()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        action = "add"
        generateSku()
        EnableFields()
        txtProdname.Text = ""
        txtDescription.Text = ""
        txtPrice.Text = ""
        cboCategory.SelectedIndex = -1
        cboUnit.SelectedIndex = -1
    End Sub

    Private Sub EnableFields()
        txtSku.Enabled = True
        txtProdname.Enabled = True
        txtDescription.Enabled = True
        cboCategory.Enabled = True
        txtPrice.Enabled = True
        cboUnit.Enabled = True
        btnAddProd.Enabled = True
    End Sub

    Private Sub generateSku()
        Try
            Dim num As Integer
            Dim idnum As String
            Dim countNum As Integer
            Dim appendNum As String

            Dim ddate As String = Date.Today.Year
            num = Convert.ToInt32(getMaxNumber()) + 1 '1
            Dim a As Byte
            countNum = num.ToString.Length
            appendNum = ""
            For a = countNum To 3
                appendNum = appendNum & "0"
            Next
            idnum = "PROD-" & ddate & "-" & appendNum & num
            txtSku.Text = idnum
            txtId.Text = num
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub clearText()
        txtId.Text = ""
        txtSku.Text = ""
        txtProdname.Text = ""
        txtDescription.Text = ""
        txtPrice.Text = ""
        cboCategory.SelectedIndex = -1
        cboUnit.SelectedIndex = -1
    End Sub

    Private Sub lockObject()
        txtId.Enabled = False
        txtSku.Enabled = False
        txtProdname.Enabled = False
        txtDescription.Enabled = False
        cboCategory.Enabled = False
        txtPrice.Enabled = False
        cboUnit.Enabled = False
        btnAddProd.Enabled = False
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
        frmMain.Focus()
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        LoadData()

    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        action = "update"
        EnableFields()
        txtSku.Enabled = False
    End Sub

    Private Sub displayCategory()
        MyCon.Open()
        MyCommand.Connection = MyCon
        MyCommand.CommandText = "SELECT *  FROM categories WHERE Status='1' ORDER BY Name ASC"
        MyAdapter.SelectCommand = MyCommand
        Dim MySQLData As MySqlDataReader = MyCommand.ExecuteReader
        While MySQLData.Read()
            If Not cboCategory.Items.Contains(MySQLData("Name").ToString()) Then
                cboCategory.Items.Add(MySQLData("Name").ToString())
            End If
        End While
        MyCon.Close()
    End Sub

    Private Sub displayUnit()
        MyCon.Open()
        MyCommand.Connection = MyCon
        MyCommand.CommandText = "SELECT *  FROM units WHERE Status='1' ORDER BY Name ASC"
        MyAdapter.SelectCommand = MyCommand
        Dim MySQLData As MySqlDataReader = MyCommand.ExecuteReader
        While MySQLData.Read()
            If Not cboUnit.Items.Contains(MySQLData("Name").ToString()) Then
                cboUnit.Items.Add(MySQLData("Name").ToString())
            End If
        End While
        MyCon.Close()
    End Sub
End Class