Imports System.ComponentModel.Design
Imports Google.Protobuf.WellKnownTypes
Imports System.Xml
Imports MySql.Data.MySqlClient

Public Class frmSales
    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub frmSales_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim salesID As Integer = 0
        displaySku()
        displayProduct()
        txtID.Enabled = False
        txtCategory.Enabled = False
        txtUnit.Enabled = False
        btnDelete.Enabled = False
        txtTotalAmount.Enabled = False
        txtStock.Enabled = False
        Label1.Text = Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(frmViewSales.action) & " " & Label1.Text
        If frmViewSales.action = "add" Then
            txtID.Text = Convert.ToInt32(getMaxNumberSales()) + 1
            generateRefNo()
            cboStatus.SelectedIndex = cboStatus.FindStringExact("Completed")
        ElseIf frmViewSales.action = "view" Then
            Integer.TryParse(frmViewSales.salesID, salesID)
            txtID.Text = frmViewSales.salesID
            disableFields()
            loadSalesOrder(salesID)
            lblStatus.Visible = False
            cboStatus.Visible = False
        ElseIf frmViewSales.action = "update" Then
            Integer.TryParse(frmViewSales.salesID, salesID)
            txtID.Text = frmViewSales.salesID
            loadSalesOrder(salesID)
        ElseIf frmViewSales.action = "update status" Then
            Integer.TryParse(frmViewSales.salesID, salesID)
            txtID.Text = frmViewSales.salesID
            loadSalesOrder(salesID)
            disableFields()
            btnSave.Enabled = True
        End If
    End Sub
    Private Sub loadSalesOrder(id As Integer)
        'MessageBox.Show(id)
        MyCon.Open()
        MyCommand.Connection = MyCon
        MyCommand.CommandText = "SELECT a.*, b.* FROM sales as a JOIN status as b ON b.StatusID = a.Status WHERE SalesID=@id LIMIT 1"
        MyCommand.Parameters.Clear()
        MyCommand.Parameters.AddWithValue("@id", id)
        MyAdapter.SelectCommand = MyCommand
        Using MySQLData As MySqlDataReader = MyCommand.ExecuteReader
            While MySQLData.Read()
                txtDescription.Text = MySQLData("Description").ToString()
                txtRefNo.Text = MySQLData("ReferenceNo").ToString()
                txtTotalAmount.Text = MySQLData("Amount").ToString()
                cboStatus.SelectedIndex = cboStatus.FindStringExact(MySQLData("Name").ToString())
            End While
        End Using
        MyCon.Close()

        MyCon.Open()
        Dim command As New MySqlCommand("SELECT 
                a.ProductID, b.Sku, b.Name, b.Category, a.Quantity, b.Unit, a.UnitPrice 
                FROM sales_products as a INNER JOIN products as b ON a.ProductID=b.ProductID WHERE a.SalesID = @id", MyCon)
        command.Parameters.Clear()
        command.Parameters.AddWithValue("@id", id)
        MyAdapter.SelectCommand = command
        Using MySQLData As MySqlDataReader = command.ExecuteReader
            While MySQLData.Read()
                Me.DataGridView1.Rows.Add(
                    MySQLData("ProductID").ToString(),
                    MySQLData("Sku").ToString(),
                    MySQLData("Name").ToString(),
                    MySQLData("Category").ToString(),
                    MySQLData("Quantity").ToString(),
                    MySQLData("Unit").ToString(),
                    MySQLData("UnitPrice").ToString())
                calculateTotalAmount()
            End While
        End Using
        MyCon.Close()

    End Sub

    Private Sub disableFields()
        txtRefNo.Enabled = False
        txtDescription.Enabled = False
        cboSku.Enabled = False
        cboProdName.Enabled = False
        txtQuantity.Enabled = False
        txtPrice.Enabled = False
        btnAdd.Enabled = False
        btnDelete.Enabled = False
        btnSave.Enabled = False
    End Sub

    Private Sub displaySku()
        Try
            MyCon.Open()
            MyCommand.Connection = MyCon
            MyCommand.CommandText = "SELECT Sku FROM products WHERE Status='1' ORDER BY Sku ASC"
            MyAdapter.SelectCommand = MyCommand
            Using MySQLData As MySqlDataReader = MyCommand.ExecuteReader
                While MySQLData.Read()
                    If Not cboSku.Items.Contains(MySQLData("Sku").ToString()) Then
                        cboSku.Items.Add(MySQLData("Sku").ToString())
                    End If
                End While
            End Using
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
        Dim sellingPrice As String = ""
        Dim stock As String = ""
        Try
            If MyCon.State = ConnectionState.Open Then
                MyCon.Close()
            End If
            MyCon.Open()
            MyCommand.Connection = MyCon
            MyCommand.CommandText = "SELECT P.*, IF(S.Quantity IS NULL, 0, S.Quantity) as Stock FROM products as P LEFT JOIN stocks as S ON S.ProductID=P.ProductID WHERE P.Status='1' AND P.Sku=@sku LIMIT 1"
            MyCommand.Parameters.Clear()
            MyCommand.Parameters.AddWithValue("@sku", sku)
            MyAdapter.SelectCommand = MyCommand
            Using MySQLData As MySqlDataReader = MyCommand.ExecuteReader
                While MySQLData.Read()
                    category = MySQLData("Category").ToString()
                    unit = MySQLData("Unit").ToString()
                    prodName = MySQLData("Name").ToString()
                    sellingPrice = MySQLData("SellingPrice").ToString()
                    stock = MySQLData("Stock").ToString()
                End While
            End Using
            MyCon.Close()

            txtCategory.Text = category
            txtUnit.Text = unit
            cboProdName.SelectedIndex = cboProdName.FindStringExact(prodName)
            txtPrice.Text = sellingPrice
            txtStock.Text = stock
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub cboProdName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboProdName.SelectedIndexChanged
        Dim prodName As String = cboProdName.Text
        Dim sku As String = ""
        Dim category As String = ""
        Dim unit As String = ""
        Dim sellingPrice As String = ""
        Dim stock As String = ""
        Try
            If MyCon.State = ConnectionState.Open Then
                MyCon.Close()
            End If
            MyCon.Open()
            MyCommand.Connection = MyCon
            MyCommand.CommandText = "SELECT P.*, IF(S.Quantity IS NULL, 0, S.Quantity) as Stock FROM products as P LEFT JOIN stocks as S ON S.ProductID=P.ProductID WHERE P.Status='1' AND P.Name=@prodName LIMIT 1"
            MyCommand.Parameters.Clear()
            MyCommand.Parameters.AddWithValue("@prodName", prodName)
            MyAdapter.SelectCommand = MyCommand
            Using MySQLData As MySqlDataReader = MyCommand.ExecuteReader
                While MySQLData.Read()
                    category = MySQLData("Category").ToString()
                    unit = MySQLData("Unit").ToString()
                    sku = MySQLData("Sku").ToString()
                    sellingPrice = MySQLData("SellingPrice").ToString()
                    stock = MySQLData("Stock").ToString()
                End While
            End Using

            MyCon.Close()

            txtCategory.Text = category
            txtUnit.Text = unit
            cboSku.SelectedIndex = cboSku.FindStringExact(sku)
            txtPrice.Text = sellingPrice
            txtStock.Text = stock
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim prodID As Integer = 0
        Dim qty As Integer = 0
        Dim stock As Integer = 0
        If validateProdFields() Then
            Integer.TryParse(txtQuantity.Text, qty)
            Integer.TryParse(txtStock.Text, stock)
            If qty > stock Then
                MessageBox.Show("Quantity should not be greater than the available stock", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else
                MyCon.Open()
                MyCommand.Connection = MyCon
                MyCommand.CommandText = "SELECT ProductID FROM products WHERE Status='1' AND Sku=@sku LIMIT 1"
                MyCommand.Parameters.Clear()
                MyCommand.Parameters.AddWithValue("@sku", cboSku.Text)
                MyAdapter.SelectCommand = MyCommand
                Using MySQLData As MySqlDataReader = MyCommand.ExecuteReader
                    While MySQLData.Read()
                        prodID = MySQLData("ProductID").ToString()
                    End While
                End Using

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
        If Not (frmViewSales.action = "view" Or frmViewSales.action = "update status") Then
            btnDelete.Enabled = True
        End If
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
            MessageBox.Show("Sales ID is empty", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        ElseIf String.IsNullOrEmpty(txtRefNo.Text) Then
            MessageBox.Show("Reference No is empty", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        ElseIf DataGridView1.RowCount = 0 Then
            MessageBox.Show("Product Table is empty", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            If frmViewSales.action = "add" Then
                If MyCon.State = ConnectionState.Open Then
                    MyCon.Close()
                End If

                'check ref no exist
                Dim refExist As Boolean = False
                MyCon.Open()
                Dim command As New MySqlCommand("SELECT ReferenceNo FROM sales WHERE ReferenceNo = @refno", MyCon)
                command.Parameters.Clear()
                command.Parameters.AddWithValue("@id", txtID.Text)
                command.Parameters.AddWithValue("@refno", txtRefNo.Text)
                MyAdapter.SelectCommand = command
                Using MySQLData As MySqlDataReader = command.ExecuteReader
                    If MySQLData.HasRows Then
                        refExist = True
                    End If
                End Using
                MyCon.Close()

                If refExist Then
                    MessageBox.Show("Reference No is already exists", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                Dim status As Int32 = Array.IndexOf(Form1.status, cboStatus.Text)
                MyCon.Open()
                MyCommand.Connection = MyCon
                MyCommand.CommandText = "INSERT INTO `sales` ( ReferenceNo, Amount, Description, Status, CreatedBy) VALUES (@refno, @amount, @description, @status, @createdby)"
                MyCommand.Parameters.Clear()
                'MyCommand.Parameters.AddWithValue("@id", txtID.Text)
                MyCommand.Parameters.AddWithValue("@refno", txtRefNo.Text.Trim)
                MyCommand.Parameters.AddWithValue("@amount", txtTotalAmount.Text.Trim)
                MyCommand.Parameters.AddWithValue("@description", txtDescription.Text.Trim)
                MyCommand.Parameters.AddWithValue("@status", status)
                MyCommand.Parameters.AddWithValue("@createdby", Form1.sessionUser("UserID"))
                MyAdapter.InsertCommand = MyCommand
                Dim result As Integer = MyCommand.ExecuteNonQuery()
                MyCon.Close()
                If result Then
                    MessageBox.Show("Sales Order has been saved to the database.", "Save Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

                For Each row In DataGridView1.Rows
                    Dim quantity As Integer = 0
                    Dim priceAmt As Double = 0
                    Dim totalAmount As Double = 0
                    Integer.TryParse(row.Cells(4).Value, quantity)
                    Double.TryParse(row.Cells(6).Value, priceAmt)
                    totalAmount = quantity * priceAmt

                    If Not MyCon.State = ConnectionState.Open Then
                        MyCon.Open()
                    End If
                    MyCommand.CommandText = "INSERT INTO `sales_products` ( SalesID, ProductID, Quantity, UnitPrice, TotalAmount, CreatedBy) 
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
                    MyCon.Close()
                Next
            ElseIf frmViewSales.action = "update" Then
                'Process update sales 

                'check ref no exist
                Dim refExist As Boolean = False
                MyCon.Open()
                Dim command As New MySqlCommand("SELECT ReferenceNo FROM sales WHERE ReferenceNo = @refno AND SalesID!=@id", MyCon)
                command.Parameters.Clear()
                command.Parameters.AddWithValue("@id", txtID.Text)
                command.Parameters.AddWithValue("@refno", txtRefNo.Text)
                MyAdapter.SelectCommand = command
                Using MySQLData As MySqlDataReader = command.ExecuteReader
                    If MySQLData.HasRows Then
                        refExist = True
                    End If
                End Using
                MyCon.Close()

                'if oldstatus not completed and updated is completed check stocks


                If String.IsNullOrEmpty(txtRefNo.Text) Then
                    MessageBox.Show("Reference No is empty", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                ElseIf refExist Then
                    MessageBox.Show("Reference No is already exist", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                ElseIf DataGridView1.RowCount = 0 Then
                    MessageBox.Show("Product Table is empty", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Else

                    Dim oldstatus As String = ""
                    Dim oldstatusID As Integer = 0
                    MyCon.Open()
                    Dim command1 As New MySqlCommand("SELECT Status FROM sales WHERE SalesID=@id", MyCon)
                    command1.Parameters.Clear()
                    command1.Parameters.AddWithValue("@id", txtID.Text)
                    MyAdapter.SelectCommand = command1
                    Using MySQLData As MySqlDataReader = command1.ExecuteReader
                        While MySQLData.Read()
                            Integer.TryParse(MySQLData("Status").ToString(), oldstatusID)
                            oldstatus = Form1.status(oldstatusID)
                        End While
                    End Using
                    MyCon.Close()

                    Dim toReturnStock As New List(Of Dictionary(Of String, String))()
                    If oldstatus = "Completed" Then
                        MyCon.Open()
                        Dim commandS As New MySqlCommand("SELECT * FROM sales_products WHERE SalesID=@id", MyCon)
                        commandS.Parameters.Clear()
                        commandS.Parameters.AddWithValue("@id", txtID.Text)
                        MyAdapter.SelectCommand = commandS
                        Using MySQLData As MySqlDataReader = commandS.ExecuteReader
                            While MySQLData.Read()
                                toReturnStock.Add(New Dictionary(Of String, String)() From {
                                    {"id", MySQLData("ProductID").ToString()},
                                    {"qty", MySQLData("Quantity").ToString()}
                                })
                            End While
                        End Using
                        MyCon.Close()
                    End If

                    For Each value As Dictionary(Of String, String) In toReturnStock
                        Dim id As String = value("id")
                        Dim qty As String = value("qty")
                        returnStocks(id, qty)
                    Next

                    Dim status As Int32 = Array.IndexOf(Form1.status, cboStatus.Text)
                    MyCon.Open()
                    MyCommand.Connection = MyCon
                    MyCommand.CommandText = "UPDATE `sales` SET ReferenceNo = @refno, Amount = @amount, Description = @desc, Status = @status, UpdatedBy = @updatedby WHERE SalesID = @id"
                    MyCommand.Parameters.Clear()
                    MyCommand.Parameters.AddWithValue("@id", txtID.Text)
                    MyCommand.Parameters.AddWithValue("@refno", txtRefNo.Text.Trim)
                    MyCommand.Parameters.AddWithValue("@amount", txtTotalAmount.Text.Trim)
                    MyCommand.Parameters.AddWithValue("@desc", txtDescription.Text.Trim)
                    MyCommand.Parameters.AddWithValue("@status", status)
                    MyCommand.Parameters.AddWithValue("@updatedby", Form1.sessionUser("UserID"))
                    MyAdapter.UpdateCommand = MyCommand
                    Dim result As Integer = MyCommand.ExecuteNonQuery()
                    MyCon.Close()

                    If result Then
                        Dim newProdID() As String = {}
                        Dim prodNotIn As String = "("
                        For Each row In DataGridView1.Rows
                            newProdID = newProdID.Concat({row.Cells(0).Value}).ToArray
                            Dim quantity As Integer = 0
                            Dim priceAmt As Double = 0
                            Dim totalAmount As Double = 0
                            Integer.TryParse(row.Cells(4).Value, quantity)
                            Double.TryParse(row.Cells(6).Value, priceAmt)
                            totalAmount = quantity * priceAmt

                            If Not MyCon.State = ConnectionState.Open Then
                                MyCon.Open()
                            End If

                            Dim prodExist As Boolean = False
                            Dim commandPE As New MySqlCommand("SELECT SalesID, ProductID FROM sales_products WHERE SalesID = @id AND ProductID = @prodid", MyCon)
                            commandPE.Parameters.Clear()
                            commandPE.Parameters.AddWithValue("@id", txtID.Text)
                            commandPE.Parameters.AddWithValue("@prodid", row.Cells(0).Value)
                            MyAdapter.SelectCommand = commandPE
                            Using MySQLData As MySqlDataReader = commandPE.ExecuteReader
                                If MySQLData.HasRows Then
                                    prodExist = True
                                End If
                            End Using
                            MyCon.Close()

                            If prodExist Then
                                'update if exist
                                If Not MyCon.State = ConnectionState.Open Then
                                    MyCon.Open()
                                End If
                                MyCommand.CommandText = "UPDATE `sales_products` SET Quantity = @qty, UnitPrice = @price, TotalAmount = @total, UpdatedBy = @updatedby WHERE SalesID = @id AND ProductID = @prodid"
                                MyCommand.Parameters.Clear()
                                MyCommand.Parameters.AddWithValue("@id", txtID.Text)
                                MyCommand.Parameters.AddWithValue("@prodid", row.Cells(0).Value)
                                MyCommand.Parameters.AddWithValue("@qty", row.Cells(4).Value)
                                MyCommand.Parameters.AddWithValue("@price", row.Cells(6).Value)
                                MyCommand.Parameters.AddWithValue("@total", totalAmount)
                                MyCommand.Parameters.AddWithValue("@updatedby", Form1.sessionUser("UserID"))
                                MyAdapter.UpdateCommand = MyCommand
                                MyCommand.ExecuteNonQuery()
                                MyCon.Close()
                            Else
                                If Not MyCon.State = ConnectionState.Open Then
                                    MyCon.Open()
                                End If
                                'Insert if not exist
                                MyCommand.CommandText = "INSERT INTO `sales_products` ( SalesID, ProductID, Quantity, UnitPrice, TotalAmount, CreatedBy) 
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
                                MyCon.Close()
                            End If

                            If cboStatus.Text = "Completed" Then
                                Dim existStock As Boolean = False
                                If Not MyCon.State = ConnectionState.Open Then
                                    MyCon.Open()
                                End If
                                MyCommand.CommandText = "SELECT * FROM stocks WHERE ProductID = @prodid LIMIT 1"
                                MyCommand.Parameters.Clear()
                                MyCommand.Parameters.AddWithValue("@prodid", row.Cells(0).Value)
                                MyAdapter.SelectCommand = MyCommand
                                Using MySQLData As MySqlDataReader = MyCommand.ExecuteReader
                                    If MySQLData.HasRows Then
                                        existStock = True
                                    End If
                                End Using
                                MyCon.Close()

                                If existStock Then
                                    MyCon.Open()
                                    MyCommand.CommandText = "UPDATE stocks SET Quantity = Quantity - @qty, UpdatedBy=@updatedby WHERE ProductID = @prodid"
                                    MyCommand.Parameters.Clear()
                                    MyCommand.Parameters.AddWithValue("@qty", row.Cells(4).Value)
                                    MyCommand.Parameters.AddWithValue("@updatedby", Form1.sessionUser("UserID"))
                                    MyCommand.Parameters.AddWithValue("@prodid", row.Cells(0).Value)
                                    MyAdapter.UpdateCommand = MyCommand
                                    MyCommand.ExecuteNonQuery()
                                    MyCon.Close()
                                Else
                                    MessageBox.Show("Unable to update product stocks.", "Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                End If
                            End If
                        Next

                        For Each id In newProdID
                            If prodNotIn = "(" Then
                                prodNotIn = prodNotIn & id
                            Else
                                prodNotIn = prodNotIn & "," & id
                            End If
                        Next
                        prodNotIn = prodNotIn & ")"

                        'delete old sales products 
                        If prodNotIn <> "()" Then
                            MessageBox.Show(prodNotIn)
                            MyCon.Open()
                            MyCommand.CommandText = "DELETE FROM sales_products WHERE SalesID = '" & txtID.Text & "' AND ProductID NOT IN " & prodNotIn
                            MyAdapter.DeleteCommand = MyCommand
                            MyCommand.ExecuteNonQuery()
                            MyCon.Close()
                        End If

                        MessageBox.Show("Sales Order has been updated.", "Save Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    End If
                End If

            ElseIf frmViewSales.action = "update status" Then

                If cboStatus.Text = "Received" Then
                    For Each row In DataGridView1.Rows
                        Dim quantity As Integer = 0
                        'add to product stocks
                        Dim existStock As Boolean = False
                        If Not MyCon.State = ConnectionState.Open Then
                            MyCon.Open()
                        End If
                        MyCommand.CommandText = "SELECT * FROM stocks WHERE ProductID = @prodid LIMIT 1"
                        MyCommand.Parameters.Clear()
                        MyCommand.Parameters.AddWithValue("@prodid", row.Cells(0).Value)
                        MyAdapter.SelectCommand = MyCommand
                        Using MySQLData As MySqlDataReader = MyCommand.ExecuteReader
                            If MySQLData.HasRows Then
                                existStock = True
                            End If
                        End Using
                        MyCon.Close()

                        If existStock Then
                            MyCon.Open()
                            MyCommand.CommandText = "UPDATE stocks SET Quantity = Quantity - @qty, UpdatedBy=@updatedby WHERE ProductID = @prodid"
                            MyCommand.Parameters.Clear()
                            MyCommand.Parameters.AddWithValue("@qty", row.Cells(4).Value)
                            MyCommand.Parameters.AddWithValue("@updatedby", Form1.sessionUser("UserID"))
                            MyCommand.Parameters.AddWithValue("@prodid", row.Cells(0).Value)
                            MyAdapter.UpdateCommand = MyCommand
                            MyCommand.ExecuteNonQuery()
                            MyCon.Close()
                        Else

                        End If
                    Next
                End If

                MyCon.Open()
                MyCommand.CommandText = "UPDATE sales as P JOIN status as S ON (S.Name=@status) SET P.Status=S.StatusID, P.UpdatedBy=@updatedby WHERE P.SalesID=@id"
                MyCommand.Parameters.Clear()
                MyCommand.Parameters.AddWithValue("@id", txtID.Text)
                MyCommand.Parameters.AddWithValue("@status", cboStatus.Text)
                MyCommand.Parameters.AddWithValue("@updatedby", Form1.sessionUser("UserID"))
                MyAdapter.UpdateCommand = MyCommand
                Dim result As Integer = MyCommand.ExecuteNonQuery()
                If result Then
                    MessageBox.Show("Status successfully updated.", "Save Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
                MyCon.Close()
            End If


            frmViewSales.LoadData()
            Me.Close()

        End If

    End Sub

    Private Sub generateRefNo()
        Try
            Dim num As Integer
            Dim idnum As String
            Dim countNum As Integer
            Dim appendNum As String

            Dim ddate As String = Date.Today.Year & Date.Today.Month
            num = Convert.ToInt32(getMaxNumberSales()) + 1 '1
            Dim a As Byte
            countNum = num.ToString.Length
            appendNum = ""
            For a = countNum To 3
                appendNum = appendNum & "0"
            Next
            idnum = "SO-" & ddate & "-" & appendNum & num
            txtRefNo.Text = idnum
            txtID.Text = num
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function returnStocks(prodid As String, qty As String) As Boolean
        Dim success As Boolean = True
        Try
            MyCon.Open()
            Integer.TryParse(qty, qty)
            MyCommand.CommandText = "UPDATE stocks SET Quantity = Quantity + @qty, UpdatedBy = @updatedby WHERE ProductID=@prodid"
            MyCommand.Parameters.Clear()
            MyCommand.Parameters.AddWithValue("@prodid", prodid)
            MyCommand.Parameters.AddWithValue("@qty", qty)
            MyCommand.Parameters.AddWithValue("@updatedby", Form1.sessionUser("UserID"))
            MyAdapter.UpdateCommand = MyCommand
            Dim result As Integer = MyCommand.ExecuteNonQuery()
            If result Then
            Else
                MessageBox.Show("Failed to update stock. " & prodid & " - " & qty, "Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                success = False
            End If
            MyCon.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Return success
    End Function

End Class