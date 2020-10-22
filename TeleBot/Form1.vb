﻿Imports Telegram.Bot
Imports Telegram.Bot.Args
Imports Telegram.Bot.Types.ReplyMarkups

Public Class Form1

    Dim botClient As TelegramBotClient
    Dim conf_File As String
    Dim command_conf_File As String
    Dim Admin As String
    Dim command_Names As New List(Of String)
    Dim commands As New List(Of String)

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Text = Me.Text & " - v." & Application.ProductVersion


        'Config File
        conf_File = Application.StartupPath & "\TeleBot.conf"
        command_conf_File = Application.StartupPath & "\TeleBot_commands.conf"
        'HTTP BOT API
        Dim BOT_API As String = GET_CONF("HTTP BOT API=")
        'Admin User
        Admin = GET_CONF("Admin User=").ToLower
        'Get Commands
        GET_COMMANDS()

        'Bot API, create Bot Client
        botClient = New TelegramBotClient(BOT_API)
        Dim botResult = botClient.GetMeAsync().Result

        'botClient.OnMessage += Bot_OnMessage;

        AddHandler botClient.OnMessage, AddressOf Bot_OnMessage

        botClient.StartReceiving()

    End Sub

    Public Async Sub Bot_OnMessage(ByVal sender As Object, ByVal e As MessageEventArgs)
        If e.Message.Text <> Nothing Then

            Dim user As String = e.Message.From.Username.ToLower

            'write Log
            log("Received message - User: " & user & " - Text: " & e.Message.Text)

            'check if message from Admin
            If user = Admin Then
                If e.Message.Text.ToLower() = "/start" Then
                    'KeyBoard
                    Dim buttons As New List(Of KeyboardButton)
                    'commands
                    For Each command_Name As String In command_Names
                        buttons.Add(New KeyboardButton(command_Name))
                    Next
                    Dim ReplyKeyboard = New ReplyKeyboardMarkup(buttons)
                    'Send Message
                    Await botClient.SendTextMessageAsync(e.Message.Chat, "Choose:", Types.Enums.ParseMode.Default, False, False, 0, ReplyKeyboard)
                Else 'check if command received
                    For i As Integer = 0 To command_Names.Count - 1
                        If command_Names(i).ToLower = e.Message.Text.ToLower Then
                            Dim errMessage As String = ""
                            'start command
                            Try
                                Process.Start(commands(i))
                                Await botClient.SendTextMessageAsync(e.Message.Chat, "Fired command: " & command_Names(i))
                                'write log
                                log("Fired command: " & command_Names(i))
                                Exit For
                            Catch ex As Exception
                                errMessage = ex.Message
                            End Try
                            'error
                            If errMessage <> "" Then
                                Await botClient.SendTextMessageAsync(e.Message.Chat, "Unable to start process: " & command_Names(i) & " Error: " & errMessage)
                                'write log
                                log("Unable to start process: " & command_Names(i) & " Error: " & errMessage)
                                errMessage = ""
                            End If

                        End If
                    Next
                End If
            Else
                'write Log
                log("No Admin, skipped message from: " & user)
            End If

        End If
    End Sub


    Private Sub Form1_Clode(sender As Object, e As EventArgs) Handles MyBase.Closing
        botClient.StopReceiving()
    End Sub


    Private Sub log(ByVal logtext As String)

        'Time
        logtext = Now & " - " & logtext

        'Insert into Listbox
        ListBox.Invoke(New Action(Sub()
                                      ListBox.Items.Insert(0, logtext)
                                  End Sub))

        Dim logFile As String = Application.StartupPath & "\TeleBot.log"
        My.Computer.FileSystem.WriteAllText(logFile, logtext & vbCrLf, True)
    End Sub


    Public Function GET_CONF(ByVal Name As String) As String
        'Read file
        Dim conf As String = My.Computer.FileSystem.ReadAllText(conf_File)
        'Split lines
        Dim lines As String() = conf.Split(vbCrLf)

        Dim ret As String = ""
        'lines
        For Each line As String In lines
            If line.Replace(vbLf, "").Replace(vbCr, "").ToLower.StartsWith(Name.ToLower) Then
                'value in line
                Dim values As String() = line.Split("=")
                If values.Length = 2 Then
                    'value found
                    ret = values(1)
                    'write Log
                    log("Config found: " & Name & ret)
                    Exit For
                End If
            End If
        Next

        Return ret
    End Function
    Public Sub GET_COMMANDS()
        'Read file
        Dim conf As String = My.Computer.FileSystem.ReadAllText(command_conf_File)
        'Split lines
        Dim lines As String() = conf.Split(vbCrLf)

        'lines
        For Each line As String In lines
            line = line.Replace(vbLf, "").Replace(vbCr, "")
            Dim values As String() = line.Split(";")
            If values.Length = 2 Then
                'Name
                command_Names.Add(values(0))
                'command
                commands.Add(values(1))
                'write Log
                log("Command found: " & line)
            End If
        Next

    End Sub
End Class
