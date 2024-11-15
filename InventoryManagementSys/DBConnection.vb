Imports MySql.Data.MySqlClient
Module DBConnection
    Public newrecord As Boolean
    Public MyAdapter As New MySqlDataAdapter
    Public dt As New DataTable
    Public num As Integer
    Public MyCommand As New MySqlCommand
    Public strsql As String
    Public command As New MySqlCommand
    Public reader As MySqlDataReader
    Public sqlAdapter As New MySqlDataAdapter
    Public query As String
    Public ds As New DataSet
    Public MyCon As MySqlConnection = New MySqlConnection("server=localhost;username=root; password='';Database=inventorymanagementsys;")

    Public Sub ExecButton(ByVal query)
        Try
            If (MyCon.State = ConnectionState.Open) Then
                MyCon.Close()
            End If
            MyCon.Open()

            MyCommand.Connection = MyCon
            MyCommand.CommandText = query
            MyAdapter.SelectCommand = MyCommand
            Dim MyData As MySqlDataReader = MyCommand.ExecuteReader
            MyCon.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Public Sub createNoMsg(ByVal sql As String)
        Try
            If MyCon.State = ConnectionState.Open Then
                MyCon.Close()
            End If
            MyCon.Open()
            MyCommand.Connection = MyCon
            MyCommand.CommandText = sql
            MyCommand.ExecuteNonQuery() ' Execute INSERT/UPDATE/DELETE commands
        Catch ex As Exception
            ' Handle the exception silently or log it, as needed
        Finally
            MyCon.Close()
        End Try
    End Sub


    Public Function getMaxNumber()
        Try
            If (MyCon.State = ConnectionState.Open) Then
                MyCon.Close()
            End If
            MyCon.Open()
            MyCommand.Connection = MyCon
            MyCommand.CommandText = "SELECT MAX(ProductID) AS MAX_NUM FROM products"
            MyAdapter.SelectCommand = MyCommand
            Dim MYSQLData As MySqlDataReader = MyCommand.ExecuteReader
            MYSQLData.Read()
            If Not MYSQLData.IsDBNull(0) Then
                num = Convert.ToInt32(MYSQLData("MAX_NUM"))
            Else
                num = 0
            End If
            MyCon.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)

        End Try

        If num = 1 Then
            getMaxNumber = 1
        Else
            'getMaxNumber = ((num - 1) + 1)
            getMaxNumber = num
        End If
    End Function

    Public Function getMaxNumberPurchase()
        Try
            If (MyCon.State = ConnectionState.Open) Then
                MyCon.Close()
            End If
            MyCon.Open()
            MyCommand.Connection = MyCon
            MyCommand.CommandText = "SELECT MAX(PurchaseID) AS MAX_NUM FROM purchases"
            MyAdapter.SelectCommand = MyCommand
            Dim MYSQLData As MySqlDataReader = MyCommand.ExecuteReader
            MYSQLData.Read()
            If Not MYSQLData.IsDBNull(0) Then
                num = Convert.ToInt32(MYSQLData("MAX_NUM"))
            Else
                num = 0
            End If
            MyCon.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)

        End Try

        If num = 1 Then
            getMaxNumberPurchase = 1
        Else
            'getMaxNumber = ((num - 1) + 1)
            getMaxNumberPurchase = num
        End If
    End Function



End Module
