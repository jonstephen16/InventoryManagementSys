Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports Google.Protobuf.WellKnownTypes
Imports MySql.Data.MySqlClient

Public Class frmAddPurchase
    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub frmAddPurchase_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        displaySku()
        displayProduct()
        txtID.Enabled = False
        txtCategory.Enabled = False
        txtUnit.Enabled = False
        btnDelete.Enabled = False
        txtTotalAmount.Enabled = False
        Label1.Text = UppercaseFirstLetter(frmViewPurchase.action) & " " & Label1.Text
        If frmViewPurchase.action = "add" Then
            txtID.Text = Convert.ToInt32(getMaxNumberPurchase()) + 1
        ElseIf frmViewPurchase.action = "view" Then
            btnAdd.Enabled = False
            btnDelete.Enabled = False
            btnSave.Enabled = False
        End If
    End Sub

    Private Sub displaySku()
        Try
            MyCon.Open()
            MyCommand.Connection = MyCon
            MyCommand.CommandText = "SELECT Sku FROM products WHERE Status='1' ORDER BY Sku ASC"
            MyAdapter.SelectCommand = MyCommand
            Dim MySQLData As MySqlDataReader = MyCommand.ExecuteReader
            While MySQLData.Read()
                If Not cboSku.Items.Contains(MySQLData("Sku").ToString()) Then
                    cboSku.Items.Add(MySQLData("Sku").ToString())
                End If
            End While
            MyCon.Close()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub displayProduct()
        Try
            MyCon.Open()
            MyCommand.Connection = MyCon
            MyCommand.CommandText = "SELECT Name FROM products WHERE Status='1' ORDER BY Name ASC"
            MyAdapter.SelectCommand = MyCommand
            Dim MySQLData As MySqlDataReader = MyCommand.ExecuteReader
            While MySQLData.Read()
                If Not cboProdName.Items.Contains(MySQLData("Name").ToString()) Then
                    cboProdName.Items.Add(MySQLData("Name").ToString())
                End If
            End While
            MyCon.Close()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub cboSku_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSku.SelectedIndexChanged
        Dim sku As String = cboSku.Text
        Dim prodName As String = ""
        Dim category As String = ""
        Dim unit As String = ""
        Try
            If MyCon.State = ConnectionState.Open Then
                MyCon.Close()
            End If
            MyCon.Open()
            MyCommand.Connection = MyCon
            MyCommand.CommandText = "SELECT * FROM products WHERE Status='1' AND Sku='" & sku & "' LIMIT 1"
            MyAdapter.SelectCommand = MyCommand
            Dim MySQLData As MySqlDataReader = MyCommand.ExecuteReader
            While MySQLData.Read()
                category = MySQLData("Category").ToString()
                unit = MySQLData("Unit").ToString()
                prodName = MySQLData("Name").ToString()
            End While
            MyCon.Close()

            txtCategory.Text = category
            txtUnit.Text = unit
            cboProdName.SelectedIndex = cboProdName.FindStringExact(prodName)
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub cboProdName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboProdName.SelectedIndexChanged
        Dim prodName As String = cboProdName.Text
        Dim sku As String = ""
        Dim category As String = ""
        Dim unit As String = ""
        Try
            If MyCon.State = ConnectionState.Open Then
                MyCon.Close()
            End If
            MyCon.Open()
            MyCommand.Connection = MyCon
            MyCommand.CommandText = "SELECT * FROM products WHERE Status='1' AND Name='" & prodName & "' LIMIT 1"
            MyAdapter.SelectCommand = MyCommand
            Dim MySQLData As MySqlDataReader = MyCommand.ExecuteReader
            While MySQLData.Read()
                category = MySQLData("Category").ToString()
                unit = MySQLData("Unit").ToString()
                sku = MySQLData("Sku").ToString()
                'cboSku.SelectedIndex = cboSku.FindStringExact()
            End While
            MyCon.Close()

            txtCategory.Text = category
            txtUnit.Text = unit
            cboSku.SelectedIndex = cboSku.FindStringExact(sku)

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim prodID As Integer = 0
        If validateProdFields() Then
            MyCon.Open()
            MyCommand.Connection = MyCon
            MyCommand.CommandText = "SELECT ProductID FROM products WHERE Status='1' AND Sku='" & cboSku.Text & "' LIMIT 1"
            MyAdapter.SelectCommand = MyCommand
            Dim MySQLData As MySqlDataReader = MyCommand.ExecuteReader
            While MySQLData.Read()
                prodID = MySQLData("ProductID").ToString()
            End While
            MyCon.Close()

            Dim price As Decimal = txtPrice.Text
            price = price.ToString("F2")
            If prodID > 0 Then
                If checkExistTable() Then
                    Me.DataGridView1.Rows.Add(prodID, cboSku.Text.Trim, cboProdName.Text.Trim, txtCategory.Text, txtQuantity.Text, txtUnit.Text, price)
                    clearProdFields()
                    calculateTotalAmount()
                Else
                    MessageBox.Show("Product is already exist", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
            End If
        Else
            MessageBox.Show("Please fill in all required fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Private Sub calculateTotalAmount()
        Dim quantity As Integer = 0
        Dim priceAmt As Double = 0
        Dim totalAmount As Double = 0

        For Each row In DataGridView1.Rows

            Integer.TryParse(row.Cells(4).Value, quantity)
            Double.TryParse(row.Cells(6).Value, priceAmt)

            totalAmount = totalAmount + (quantity * priceAmt)
        Next

        txtTotalAmount.Text = totalAmount.ToString("F2")
    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        btnDelete.Enabled = True
    End Sub

    Private Function validateProdFields() As Boolean
        Return Not (
            cboSku.SelectedItem Is Nothing Or
            cboProdName.SelectedItem Is Nothing Or
            String.IsNullOrEmpty(txtCategory.Text) Or
            String.IsNullOrEmpty(txtQuantity.Text) Or
            String.IsNullOrEmpty(txtUnit.Text) Or
            String.IsNullOrEmpty(txtPrice.Text)
        )
    End Function

    Private Sub clearProdFields()
        cboProdName.SelectedIndex = -1
        cboSku.SelectedIndex = -1
        txtCategory.Text = ""
        txtQuantity.Text = ""
        txtUnit.Text = ""
        txtPrice.Text = ""
    End Sub

    Private Function checkExistTable() As Boolean
        Dim exist As Boolean = False
        For Each row In DataGridView1.Rows
            If cboSku.Text = row.Cells(1).Value Then
                exist = True
                Exit For
            End If
        Next

        Return Not exist
    End Function

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        For Each row As DataGridViewRow In DataGridView1.SelectedRows
            DataGridView1.Rows.Remove(row)
        Next
        calculateTotalAmount()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If String.IsNullOrEmpty(txtID.Text) Then
            MessageBox.Show("Purchase ID is empty", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        ElseIf String.IsNullOrEmpty(txtRefNo.Text) Then
            MessageBox.Show("Reference No is empty", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        ElseIf DataGridView1.RowCount = 0 Then
            MessageBox.Show("Product Table is empty", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            If frmViewPurchase.action = "add" Then
                MyCon.Open()
                MyCommand.Connection = MyCon
                MyCommand.CommandText = "INSERT INTO `purchases` ( PurchaseID, ReferenceNo, Amount, Description, CreatedBy) VALUES (@id, @refno, @amount, @description, @createdby)"
                MyCommand.Parameters.AddWithValue("@id", txtID.Text)
                MyCommand.Parameters.AddWithValue("@refno", txtRefNo.Text.Trim)
                MyCommand.Parameters.AddWithValue("@amount", txtTotalAmount.Text.Trim)
                MyCommand.Parameters.AddWithValue("@description", txtDescription.Text.Trim)
                MyCommand.Parameters.AddWithValue("@createdby", Form1.sessionUser("UserID"))
                MyAdapter.InsertCommand = MyCommand
                Dim result As Integer = MyCommand.ExecuteNonQuery()

                If result Then
                    MessageBox.Show("Purchase Order has been saved to the database.", "Save Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

                For Each row In DataGridView1.Rows
                    Dim quantity As Integer = 0
                    Dim priceAmt As Double = 0
                    Dim totalAmount As Double = 0
                    Integer.TryParse(row.Cells(4).Value, quantity)
                    Double.TryParse(row.Cells(6).Value, priceAmt)
                    totalAmount = quantity * priceAmt

                    MyCommand.CommandText = "INSERT INTO `purchase_products` ( PurchaseID, ProductID, Quantity, UnitPrice, TotalAmount, CreatedBy) 
                                            VALUES (@id, @prodid, @qty, @price, @total, @createdby)"
                    MyCommand.Parameters.Clear()
                    MyCommand.Parameters.AddWithValue("@id", txtID.Text)
                    MyCommand.Parameters.AddWithValue("@prodid", row.Cells(0).Value)
                    MyCommand.Parameters.AddWithValue("@qty", row.Cells(4).Value)
                    MyCommand.Parameters.AddWithValue("@price", row.Cells(6).Value)
                    MyCommand.Parameters.AddWithValue("@total", totalAmount)
                    MyCommand.Parameters.AddWithValue("@createdby", Form1.sessionUser("UserID"))
                    MyAdapter.InsertCommand = MyCommand
                    MyCommand.ExecuteNonQuery()
                Next

                MyCon.Close()
                Me.Close()
            ElseIf frmViewPurchase.action = "update" Then
                MessageBox.Show("Process update")
            End If


        End If

    End Sub

    Function UppercaseFirstLetter(ByVal val As String) As String
        ' Test for nothing or empty.
        If String.IsNullOrEmpty(val) Then
            Return val
        End If

        ' Convert to character array.
        Dim array() As Char = val.ToCharArray

        ' Uppercase first character.
        array(0) = Char.ToUpper(array(0))

        ' Return new string.
        Return New String(array)
    End Function
End Class