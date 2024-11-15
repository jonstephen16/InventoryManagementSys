﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim ChartArea3 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend3 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series5 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series6 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.chartByCategory = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.labelRoleName = New System.Windows.Forms.Label()
        Me.linkReports = New System.Windows.Forms.LinkLabel()
        Me.linkSettings = New System.Windows.Forms.LinkLabel()
        Me.linkPurchases = New System.Windows.Forms.LinkLabel()
        Me.labelEmail = New System.Windows.Forms.Label()
        Me.salesLink = New System.Windows.Forms.LinkLabel()
        Me.productLink = New System.Windows.Forms.LinkLabel()
        Me.linkLogout = New System.Windows.Forms.LinkLabel()
        Me.welcomeText = New System.Windows.Forms.Label()
        Me.sessionUser = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.ImageList2 = New System.Windows.Forms.ImageList(Me.components)
        Me.ImageList3 = New System.Windows.Forms.ImageList(Me.components)
        Me.ImageList4 = New System.Windows.Forms.ImageList(Me.components)
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblTotalProd = New System.Windows.Forms.Label()
        Me.lblLowStock = New System.Windows.Forms.Label()
        Me.lblOutStock = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.Timer3 = New System.Windows.Forms.Timer(Me.components)
        Me.Panel2.SuspendLayout()
        CType(Me.chartByCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.White
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.chartByCategory)
        Me.Panel2.Location = New System.Drawing.Point(15, 100)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(331, 281)
        Me.Panel2.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label1.Location = New System.Drawing.Point(69, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(187, 16)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "Top 5 Orders By Category"
        '
        'chartByCategory
        '
        ChartArea3.AxisX.Interval = 1.0R
        ChartArea3.AxisX.IsLabelAutoFit = False
        ChartArea3.AxisX.LabelStyle.Angle = -90
        ChartArea3.Name = "ChartArea1"
        Me.chartByCategory.ChartAreas.Add(ChartArea3)
        Legend3.Name = "Legend1"
        Me.chartByCategory.Legends.Add(Legend3)
        Me.chartByCategory.Location = New System.Drawing.Point(7, 25)
        Me.chartByCategory.Name = "chartByCategory"
        Series5.ChartArea = "ChartArea1"
        Series5.Legend = "Legend1"
        Series5.Name = "Purchases"
        Series6.ChartArea = "ChartArea1"
        Series6.Legend = "Legend1"
        Series6.Name = "Sales"
        Me.chartByCategory.Series.Add(Series5)
        Me.chartByCategory.Series.Add(Series6)
        Me.chartByCategory.Size = New System.Drawing.Size(316, 253)
        Me.chartByCategory.TabIndex = 0
        Me.chartByCategory.Text = "Chart1"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(16, Byte), Integer), CType(CType(87, Byte), Integer))
        Me.Panel3.Controls.Add(Me.labelRoleName)
        Me.Panel3.Controls.Add(Me.linkReports)
        Me.Panel3.Controls.Add(Me.linkSettings)
        Me.Panel3.Controls.Add(Me.linkPurchases)
        Me.Panel3.Controls.Add(Me.labelEmail)
        Me.Panel3.Controls.Add(Me.salesLink)
        Me.Panel3.Controls.Add(Me.productLink)
        Me.Panel3.Controls.Add(Me.linkLogout)
        Me.Panel3.Controls.Add(Me.welcomeText)
        Me.Panel3.Controls.Add(Me.sessionUser)
        Me.Panel3.Controls.Add(Me.PictureBox1)
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(752, 86)
        Me.Panel3.TabIndex = 4
        '
        'labelRoleName
        '
        Me.labelRoleName.AutoSize = True
        Me.labelRoleName.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.labelRoleName.Location = New System.Drawing.Point(147, 23)
        Me.labelRoleName.Name = "labelRoleName"
        Me.labelRoleName.Size = New System.Drawing.Size(39, 13)
        Me.labelRoleName.TabIndex = 10
        Me.labelRoleName.Text = "Lorem,"
        '
        'linkReports
        '
        Me.linkReports.AutoSize = True
        Me.linkReports.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.linkReports.LinkColor = System.Drawing.Color.White
        Me.linkReports.Location = New System.Drawing.Point(527, 36)
        Me.linkReports.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.linkReports.Name = "linkReports"
        Me.linkReports.Size = New System.Drawing.Size(51, 13)
        Me.linkReports.TabIndex = 9
        Me.linkReports.TabStop = True
        Me.linkReports.Text = "Reports"
        '
        'linkSettings
        '
        Me.linkSettings.AutoSize = True
        Me.linkSettings.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.linkSettings.LinkColor = System.Drawing.Color.White
        Me.linkSettings.Location = New System.Drawing.Point(598, 36)
        Me.linkSettings.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.linkSettings.Name = "linkSettings"
        Me.linkSettings.Size = New System.Drawing.Size(53, 13)
        Me.linkSettings.TabIndex = 8
        Me.linkSettings.TabStop = True
        Me.linkSettings.Text = "Settings"
        '
        'linkPurchases
        '
        Me.linkPurchases.AutoSize = True
        Me.linkPurchases.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.linkPurchases.LinkColor = System.Drawing.Color.White
        Me.linkPurchases.Location = New System.Drawing.Point(305, 36)
        Me.linkPurchases.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.linkPurchases.Name = "linkPurchases"
        Me.linkPurchases.Size = New System.Drawing.Size(66, 13)
        Me.linkPurchases.TabIndex = 7
        Me.linkPurchases.TabStop = True
        Me.linkPurchases.Text = "Purchases"
        '
        'labelEmail
        '
        Me.labelEmail.AutoSize = True
        Me.labelEmail.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labelEmail.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.labelEmail.Location = New System.Drawing.Point(99, 49)
        Me.labelEmail.Name = "labelEmail"
        Me.labelEmail.Size = New System.Drawing.Size(89, 13)
        Me.labelEmail.TabIndex = 6
        Me.labelEmail.Text = "email@email.com"
        '
        'salesLink
        '
        Me.salesLink.AutoSize = True
        Me.salesLink.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.salesLink.LinkColor = System.Drawing.Color.White
        Me.salesLink.Location = New System.Drawing.Point(392, 36)
        Me.salesLink.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.salesLink.Name = "salesLink"
        Me.salesLink.Size = New System.Drawing.Size(38, 13)
        Me.salesLink.TabIndex = 5
        Me.salesLink.TabStop = True
        Me.salesLink.Text = "Sales"
        '
        'productLink
        '
        Me.productLink.AutoSize = True
        Me.productLink.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.productLink.LinkColor = System.Drawing.Color.White
        Me.productLink.Location = New System.Drawing.Point(450, 36)
        Me.productLink.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.productLink.Name = "productLink"
        Me.productLink.Size = New System.Drawing.Size(57, 13)
        Me.productLink.TabIndex = 4
        Me.productLink.TabStop = True
        Me.productLink.Text = "Products"
        '
        'linkLogout
        '
        Me.linkLogout.AutoSize = True
        Me.linkLogout.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.linkLogout.LinkColor = System.Drawing.Color.White
        Me.linkLogout.Location = New System.Drawing.Point(679, 36)
        Me.linkLogout.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.linkLogout.Name = "linkLogout"
        Me.linkLogout.Size = New System.Drawing.Size(46, 13)
        Me.linkLogout.TabIndex = 3
        Me.linkLogout.TabStop = True
        Me.linkLogout.Text = "Logout"
        '
        'welcomeText
        '
        Me.welcomeText.AutoSize = True
        Me.welcomeText.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.welcomeText.Location = New System.Drawing.Point(99, 23)
        Me.welcomeText.Name = "welcomeText"
        Me.welcomeText.Size = New System.Drawing.Size(52, 13)
        Me.welcomeText.TabIndex = 2
        Me.welcomeText.Text = "Welcome"
        '
        'sessionUser
        '
        Me.sessionUser.AutoSize = True
        Me.sessionUser.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.sessionUser.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.sessionUser.Location = New System.Drawing.Point(98, 36)
        Me.sessionUser.Name = "sessionUser"
        Me.sessionUser.Size = New System.Drawing.Size(45, 13)
        Me.sessionUser.TabIndex = 1
        Me.sessionUser.Text = "Label1"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.InventoryManagementSys.My.Resources.Resources.user
        Me.PictureBox1.Location = New System.Drawing.Point(28, 17)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(50, 50)
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'ImageList1
        '
        Me.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.ImageList1.ImageSize = New System.Drawing.Size(16, 16)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        '
        'ImageList2
        '
        Me.ImageList2.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.ImageList2.ImageSize = New System.Drawing.Size(16, 16)
        Me.ImageList2.TransparentColor = System.Drawing.Color.Transparent
        '
        'ImageList3
        '
        Me.ImageList3.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.ImageList3.ImageSize = New System.Drawing.Size(16, 16)
        Me.ImageList3.TransparentColor = System.Drawing.Color.Transparent
        '
        'ImageList4
        '
        Me.ImageList4.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.ImageList4.ImageSize = New System.Drawing.Size(16, 16)
        Me.ImageList4.TransparentColor = System.Drawing.Color.Transparent
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(16, Byte), Integer), CType(CType(87, Byte), Integer))
        Me.Panel4.Controls.Add(Me.Label5)
        Me.Panel4.Location = New System.Drawing.Point(0, 397)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(752, 29)
        Me.Panel4.TabIndex = 5
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.AppWorkspace
        Me.Label5.Location = New System.Drawing.Point(671, 7)
        Me.Label5.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(69, 13)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "Version 1.0.0"
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.White
        Me.Panel5.Controls.Add(Me.lblTotalProd)
        Me.Panel5.Controls.Add(Me.Label2)
        Me.Panel5.Location = New System.Drawing.Point(363, 100)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(112, 68)
        Me.Panel5.TabIndex = 6
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label2.Location = New System.Drawing.Point(17, 47)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(78, 12)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "Total Products"
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.White
        Me.Panel6.Controls.Add(Me.lblLowStock)
        Me.Panel6.Controls.Add(Me.Label3)
        Me.Panel6.Location = New System.Drawing.Point(493, 100)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(112, 68)
        Me.Panel6.TabIndex = 7
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.White
        Me.Panel7.Controls.Add(Me.lblOutStock)
        Me.Panel7.Controls.Add(Me.Label4)
        Me.Panel7.Location = New System.Drawing.Point(625, 100)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(112, 68)
        Me.Panel7.TabIndex = 8
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Goldenrod
        Me.Label3.Location = New System.Drawing.Point(17, 47)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(58, 12)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "Low Stock"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Maroon
        Me.Label4.Location = New System.Drawing.Point(18, 47)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(68, 12)
        Me.Label4.TabIndex = 14
        Me.Label4.Text = "Out of Stock"
        '
        'lblTotalProd
        '
        Me.lblTotalProd.AutoSize = True
        Me.lblTotalProd.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalProd.ForeColor = System.Drawing.Color.MidnightBlue
        Me.lblTotalProd.Location = New System.Drawing.Point(13, 16)
        Me.lblTotalProd.Name = "lblTotalProd"
        Me.lblTotalProd.Size = New System.Drawing.Size(46, 31)
        Me.lblTotalProd.TabIndex = 13
        Me.lblTotalProd.Text = "99"
        '
        'lblLowStock
        '
        Me.lblLowStock.AutoSize = True
        Me.lblLowStock.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLowStock.ForeColor = System.Drawing.Color.Goldenrod
        Me.lblLowStock.Location = New System.Drawing.Point(13, 16)
        Me.lblLowStock.Name = "lblLowStock"
        Me.lblLowStock.Size = New System.Drawing.Size(46, 31)
        Me.lblLowStock.TabIndex = 14
        Me.lblLowStock.Text = "99"
        '
        'lblOutStock
        '
        Me.lblOutStock.AutoSize = True
        Me.lblOutStock.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOutStock.ForeColor = System.Drawing.Color.Maroon
        Me.lblOutStock.Location = New System.Drawing.Point(13, 16)
        Me.lblOutStock.Name = "lblOutStock"
        Me.lblOutStock.Size = New System.Drawing.Size(46, 31)
        Me.lblOutStock.TabIndex = 15
        Me.lblOutStock.Text = "99"
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(219, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(749, 426)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmMain"
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.chartByCategory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents ImageList2 As ImageList
    Friend WithEvents ImageList3 As ImageList
    Friend WithEvents ImageList4 As ImageList
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents sessionUser As Label
    Friend WithEvents welcomeText As Label
    Friend WithEvents linkLogout As LinkLabel
    Friend WithEvents salesLink As LinkLabel
    Friend WithEvents productLink As LinkLabel
    Friend WithEvents labelEmail As Label
    Friend WithEvents labelRoleName As Label
    Friend WithEvents linkReports As LinkLabel
    Friend WithEvents linkSettings As LinkLabel
    Friend WithEvents linkPurchases As LinkLabel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Label5 As Label
    Friend WithEvents chartByCategory As DataVisualization.Charting.Chart
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents lblTotalProd As Label
    Friend WithEvents lblLowStock As Label
    Friend WithEvents lblOutStock As Label
    Friend WithEvents Timer1 As Timer
    Friend WithEvents Timer2 As Timer
    Friend WithEvents Timer3 As Timer
End Class
