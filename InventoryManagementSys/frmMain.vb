Imports MySql.Data.MySqlClient

Public Class frmMain
    Public todate As String = Date.Now().ToString("yyyy-MM-dd")
    Private Sub frmMain_Show(sender As Object, e As EventArgs) Handles MyBase.Shown
        sessionUser.Text = Form1.sessionUser("Firstname") & " " & Form1.sessionUser("Lastname")
        labelEmail.Text = Form1.sessionUser("Email")
        labelRoleName.Text = Form1.roles(Form1.sessionUser("UserRoleID")) & ","
    End Sub

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadTopCategory()
        loadProducts()
    End Sub

    Private Sub linkLogout_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles linkLogout.LinkClicked
        Dim Login As New Form1
        Login.Show()
        Me.Close()
        frmProd.Close()
        frmViewPurchase.Close()
        frmAddPurchase.Close()
        frmViewSales.Close()
        frmSales.Close()
    End Sub

    ' Private Sub frmMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
    'Dim terminate = MessageBox.Show("Are you sure you want to exit from the System?", "Confirmation Message.", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
    'If terminate = DialogResult.No Then
    '       e.Cancel = True
    'Else
    '        Application.Exit()
    'End If
    'End Sub

    Private Sub salesLink_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles salesLink.LinkClicked
        frmViewSales.Show()
    End Sub

    Private Sub productLink_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles productLink.LinkClicked
        frmProd.Show()
    End Sub

    Private Sub linkPurchases_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles linkPurchases.LinkClicked
        frmViewPurchase.Show()
    End Sub

    Private Sub loadTopCategory()
        Dim thisMonth As String = Date.Now().ToString("yyyy-MM")
        If MyCon.State = ConnectionState.Open Then
            MyCon.Close()
        End If
        MyCon.Open()
        MyCommand.Connection = MyCon
        MyCommand.CommandText = "SELECT PR.Category, SUM(PP.Quantity) as TotalQty FROM purchases as P JOIN purchase_products as PP ON PP.PurchaseID=P.PurchaseID JOIN products as PR ON PR.ProductID=PP.ProductID WHERE P.Status='5' AND P.DateUpdated LIKE @month GROUP BY PR.Category ORDER BY TotalQty DESC LIMIT 5;"
        MyCommand.Parameters.Clear()
        MyCommand.Parameters.AddWithValue("@month", "%" & thisMonth & "%")
        MyAdapter.SelectCommand = MyCommand
        Using MySQLData As MySqlDataReader = MyCommand.ExecuteReader
            While MySQLData.Read()
                chartByCategory.Series("Purchases").Points.AddXY(MySQLData.GetString("Category"), MySQLData.GetInt32("TotalQty"))
            End While
        End Using
        MyCon.Close()

        MyCon.Open()
        MyCommand.Connection = MyCon
        MyCommand.CommandText = "SELECT PR.Category, SUM(PP.Quantity) as TotalQty FROM sales as P JOIN sales_products as PP ON PP.SalesID=P.SalesID JOIN products as PR ON PR.ProductID=PP.ProductID WHERE P.Status='5' AND P.DateUpdated LIKE @month GROUP BY PR.Category ORDER BY TotalQty DESC LIMIT 5;"
        MyCommand.Parameters.Clear()
        MyCommand.Parameters.AddWithValue("@month", "%" & thisMonth & "%")
        MyAdapter.SelectCommand = MyCommand
        Using MySQLData As MySqlDataReader = MyCommand.ExecuteReader
            While MySQLData.Read()
                chartByCategory.Series("Sales").Points.AddXY(MySQLData.GetString("Category"), MySQLData.GetInt32("TotalQty"))
            End While
        End Using
        MyCon.Close()
    End Sub

    Private Sub loadProducts()
        If MyCon.State = ConnectionState.Open Then
            MyCon.Close()
        End If
        MyCon.Open()
        MyCommand.Connection = MyCon
        MyCommand.CommandText = "SELECT Count(ProductID) as TotalProd FROM products WHERE Status='1';"
        MyAdapter.SelectCommand = MyCommand
        Using MySQLData As MySqlDataReader = MyCommand.ExecuteReader
            While MySQLData.Read()
                lblTotalProd.Text = MySQLData.GetInt32("TotalProd")
            End While
        End Using
        MyCon.Close()

        If MyCon.State = ConnectionState.Open Then
            MyCon.Close()
        End If
        MyCon.Open()
        MyCommand.Connection = MyCon
        MyCommand.CommandText = "SELECT Count(products.ProductID) as LowStock FROM products LEFT JOIN stocks ON products.ProductID=stocks.ProductID WHERE products.Status='1' AND stocks.Quantity < 10 ;"
        MyAdapter.SelectCommand = MyCommand
        Using MySQLData As MySqlDataReader = MyCommand.ExecuteReader
            While MySQLData.Read()
                lblLowStock.Text = MySQLData.GetInt32("LowStock")
            End While
        End Using
        MyCon.Close()

        If MyCon.State = ConnectionState.Open Then
            MyCon.Close()
        End If
        MyCon.Open()
        MyCommand.Connection = MyCon
        MyCommand.CommandText = "SELECT Count(products.ProductID) as OutStock FROM products LEFT JOIN stocks ON products.ProductID=stocks.ProductID WHERE products.Status='1' AND (stocks.Quantity = 0 OR stocks.Quantity IS NULL);"
        MyAdapter.SelectCommand = MyCommand
        Using MySQLData As MySqlDataReader = MyCommand.ExecuteReader
            While MySQLData.Read()
                lblOutStock.Text = MySQLData.GetInt32("OutStock")
            End While
        End Using
        MyCon.Close()
    End Sub
End Class