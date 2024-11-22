Imports MySql.Data.MySqlClient
Public Class Form1
    Public Property sessionUser As New Dictionary(Of String, Object)
    Public Shared ReadOnly roles() As String = {"", "Admin", "Manager", "Staff", "Owner"}
    Public status() As String = {"", "Active", "Inactive", "Deleted", "Pending", "Received", "Cancelled", "Completed"}

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Application.Exit()
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        If MyCon.State = ConnectionState.Open Then
            MyCon.Close()
        End If
        Try
            Static Dim counter As Integer
            If (txtUser.Text = "") Then
                MsgBox("Enter username.", MsgBoxStyle.Critical, "Required")
                txtUser.BackColor = Color.FromArgb(240, 240, 20)
                txtUser.Select()

                Exit Sub
            ElseIf (txtPassword.Text = "") Then
                MsgBox("Enter password.", MsgBoxStyle.Critical, "Required")
                txtPassword.BackColor = Color.FromArgb(240, 240, 20)
                txtPassword.Select()
                Exit Sub
            Else
                counter = counter + 1 '3
                MyCon.Open()
                MyCommand.Connection = MyCon
                MyCommand.CommandText = "SELECT *  FROM users WHERE Username=@username AND Password=@password"
                MyCommand.Parameters.Clear()
                MyCommand.Parameters.AddWithValue("@username", txtUser.Text.Trim)
                MyCommand.Parameters.AddWithValue("@password", getSHA1Hash(txtPassword.Text))
                MyAdapter.SelectCommand = MyCommand
                Dim MySQLData As MySqlDataReader = MyCommand.ExecuteReader

                If MySQLData.HasRows = 0 Then '1


                    If counter = 3 Then
                        MessageBox.Show("You have reached your maximum login attempts. The program will now end.", "Error Login!", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Application.Exit()
                    End If
                    MessageBox.Show("Invalid Username or Password!", "Error message.",
                    MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub

                Else
                    While MySQLData.Read()
                        If (Not sessionUser.ContainsKey("UserID")) Then
                            sessionUser.Add("UserID", MySQLData("UserID").ToString())
                            sessionUser.Add("Firstname", MySQLData("Firstname").ToString())
                            sessionUser.Add("Lastname", MySQLData("Lastname").ToString())
                            sessionUser.Add("Email", MySQLData("Email").ToString())
                            sessionUser.Add("UserRoleID", MySQLData("UserRoleID").ToString())
                        Else
                            sessionUser("UserID") = MySQLData("UserID").ToString()
                            sessionUser("Firstname") = MySQLData("Firstname").ToString()
                            sessionUser("Lastname") = MySQLData("Lastname").ToString()
                            sessionUser("Email") = MySQLData("Email").ToString()
                            sessionUser("UserRoleID") = MySQLData("UserRoleID").ToString()
                        End If

                    End While

                    If (MySQLData("Status").ToString() = 1) Then

                        'clear login form
                        txtUser.Text = ""
                        txtPassword.Text = ""
                        txtUser.Select()

                        MsgBox("You have successfully logged in.", MsgBoxStyle.Information, "Success")
                        Dim frm = New frmMain()
                        Me.Hide()
                        frm.Show()
                    Else
                        MessageBox.Show("Inactive user!", "Error message.",
                        MessageBoxButtons.OK, MessageBoxIcon.Error)

                    End If
                End If

                MyCon.Close()

            End If
        Catch ex As Exception
            MsgBox(ex.Message)

        End Try

    End Sub

    Function getSHA1Hash(ByVal strToHash As String) As String

        Dim sha1Obj As New Security.Cryptography.SHA1CryptoServiceProvider
        Dim bytesToHash() As Byte = System.Text.Encoding.ASCII.GetBytes(strToHash)

        bytesToHash = sha1Obj.ComputeHash(bytesToHash)

        Dim strResult As String = ""

        For Each b As Byte In bytesToHash
            strResult += b.ToString("x2")
        Next

        Return strResult

    End Function

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtUser.Select()

    End Sub

    Private Sub txtPassword_KeyDown(sender As Object, e As KeyEventArgs) Handles txtPassword.KeyDown
        If e.KeyCode = Keys.Enter Then
            btnLogin_Click(Nothing, Nothing)
        Else
            Exit Sub
        End If
        e.SuppressKeyPress = True
    End Sub
End Class
