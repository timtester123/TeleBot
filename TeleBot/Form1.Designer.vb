﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
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

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.ListBox = New System.Windows.Forms.ListBox()
        Me.Timer_reconnect = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'ListBox
        '
        Me.ListBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListBox.FormattingEnabled = True
        Me.ListBox.Location = New System.Drawing.Point(0, 0)
        Me.ListBox.Name = "ListBox"
        Me.ListBox.Size = New System.Drawing.Size(862, 451)
        Me.ListBox.TabIndex = 0
        '
        'Timer_reconnect
        '
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(862, 451)
        Me.Controls.Add(Me.ListBox)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Form1"
        Me.Text = "TeleBot"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ListBox As ListBox
    Friend WithEvents Timer_reconnect As Timer
End Class
