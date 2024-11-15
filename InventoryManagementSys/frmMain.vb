Public Class frmMain
    Private Sub frmMain_Show(sender As Object, e As EventArgs) Handles MyBase.Shown
        sessionUser.Text = Form1.sessionUser("Firstname") & " " & Form1.sessionUser("Lastname")
        labelEmail.Text = Form1.sessionUser("Email")
        labelRoleName.Text = Form1.roles(Form1.sessionUser("UserRoleID")) & ","
    End Sub

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    End Sub

    Private Sub linkLogout_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles linkLogout.LinkClicked
        Dim Login As New Form1
        Login.Show()
        Me.Close()
        frmProd.Close()
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
        frmSales.Show()
    End Sub

    Private Sub productLink_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles productLink.LinkClicked
        frmProd.Show()
    End Sub

    Private Sub linkPurchases_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles linkPurchases.LinkClicked
        frmViewPurchase.Show()
    End Sub


End Class