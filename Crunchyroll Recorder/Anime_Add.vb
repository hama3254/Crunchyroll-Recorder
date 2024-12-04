Option Strict On


Imports MetroFramework.Forms
Imports MetroFramework
Imports MetroFramework.Components
Imports CoreAudio
Public Class Anime_Add
    Public Mass_DL_Cancel As Boolean = False
    Public List_DL_Cancel As Boolean = False

    Dim Manager As MetroStyleManager = Main.Manager

    Dim AudioDevices As New List(Of ServerResponseCache)
    Dim selectedDevice As String
    Dim selectedDeviceKey As String

    Private Sub Anime_Add_Load(sender As Object, e As EventArgs) Handles MyBase.Load



        Manager.Owner = Me
        Me.StyleManager = Manager
        Btn_Close.Image = Main.CloseImg
        Btn_min.Image = Main.MinImg

        Try
            Me.Icon = My.Resources.icon
        Catch ex As Exception

        End Try


        Try
            Main.waveOutSetVolume(0, 0)
        Catch ex As Exception

        End Try
        Me.Location = New Point(CInt(Main.Location.X + Main.Width / 2 - Me.Width / 2), CInt(Main.Location.Y + Main.Height / 2 - Me.Height / 2))
        Pfad_tBox.Text = Main.Pfad


        'Try 'Computer\HKEY_LOCAL_MACHINE\SYSTEM\ControlSet001\Control\DeviceClasses\{e6327cad-dcec-4949-ae8a-991e976a79d2}
        '    Dim rkg As RegistryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\ControlSet001\Control\DeviceClasses\{e6327cad-dcec-4949-ae8a-991e976a79d2}")

        '    Dim S() As String = rkg.GetSubKeyNames()
        '    For i As Integer = 0 To S.Count - 1
        '        'ComboBox1.Items.Add(S(i))
        '        Dim SubKey As RegistryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\ControlSet001\Control\DeviceClasses\{e6327cad-dcec-4949-ae8a-991e976a79d2}\" + S(i) + "\#\Device Parameters")

        '        If AudioDevices.Contains(SubKey.GetValue("FriendlyName").ToString()) Then
        '        ElseIf CBool(InStr(SubKey.GetValue("FriendlyName").ToString(), "VB-Audio")) Then
        '            CB_AudioDevices.Items.Add(SubKey.GetValue("FriendlyName").ToString())

        '            AudioDevices.Add(SubKey.GetValue("FriendlyName").ToString())
        '        End If
        '    Next
        '    'Pfad = rkg.GetValue("Ordner").ToString
        'Catch ex As Exception

        'End Try

        Dim deviceManager As New MMDeviceEnumerator(Guid.NewGuid)

        ' Erhalte eine Liste aller Audiogerät
        Dim devices As MMDeviceCollection = deviceManager.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active)

        ' Durchlaufe jedes Gerät und drucke seine UUID
        For Each device As MMDevice In devices
            ' Debug.WriteLine("Gerätename: " & device.DeviceFriendlyName)


            Dim DeviceName As String = device.DeviceFriendlyName
            Dim DeviceID As String = device.ID.ToString()

            If CBool(InStr(DeviceName, "VB-Audio")) Then
                CB_AudioDevices.Items.Add(DeviceName)
                AudioDevices.Add(New ServerResponseCache(DeviceName, DeviceID))
                ' Debug.WriteLine("UUID: " & device.ID.ToString())
            End If
        Next

        If CB_AudioDevices.Items.Count > 0 Then
            CB_AudioDevices.SelectedIndex = 0
        Else
            MsgBox("Critical Error")
            Me.Close()
        End If
        'Timer3.Enabled = True

    End Sub



    Private Sub Btn_dl_Click(sender As Object, e As EventArgs) Handles btn_dl.Click



        Dim TN As String = "https://invalid.com/"
        Dim cmd As String = "-i " + Chr(34) + "https://invalid.com/" + Chr(34) + " -c copy "

        Main.ListItemAdd(Url_tBox.Text, selectedDevice, Name_tBox.Text, "9987p", "DE", TN, selectedDeviceKey, Main.Pfad)


        Exit Sub



    End Sub






    Private Sub Btn_dl_MouseEnter(sender As Object, e As EventArgs) Handles btn_dl.MouseEnter
        If Mass_DL_Cancel = True Then
            btn_dl.Text = "Cancel"
            btn_dl.BackgroundImage = My.Resources.main_button_download_hovert
        ElseIf List_DL_Cancel = True Then
            btn_dl.Text = "Cancel"
            btn_dl.BackgroundImage = My.Resources.main_button_download_hovert

        Else
            btn_dl.Text = "Download"
            btn_dl.BackgroundImage = My.Resources.main_button_download_hovert
        End If

    End Sub

    Private Sub Btn_dl_MouseLeave(sender As Object, e As EventArgs) Handles btn_dl.MouseLeave
        If Mass_DL_Cancel = True Then
            btn_dl.Text = "Cancel"
            btn_dl.BackgroundImage = My.Resources.main_button_download_hovert
        ElseIf List_DL_Cancel = True Then
            btn_dl.Text = "Cancel"
            btn_dl.BackgroundImage = My.Resources.main_button_download_hovert
        Else
            btn_dl.Text = "Download"
            btn_dl.BackgroundImage = My.Resources.main_button_download_default
        End If

    End Sub

    Private Sub TextBox1_Click(sender As Object, e As EventArgs) Handles Url_tBox.Click
        If Url_tBox.Text = "URL" Then
            Url_tBox.Text = Nothing
        End If
    End Sub


    Private Sub bt_Cancel_mass_Click(sender As Object, e As EventArgs) Handles bt_Cancel_mass.Click
        groupBox1.Visible = True
        groupBox2.Visible = False
    End Sub

    Private Sub bt_Cancel_mass_MouseEnter(sender As Object, e As EventArgs) Handles bt_Cancel_mass.MouseEnter

        bt_Cancel_mass.BackgroundImage = My.Resources.add_mass_cancel_hover


    End Sub
    Private Sub bt_Cancel_mass_MouseLeave(sender As Object, e As EventArgs) Handles bt_Cancel_mass.MouseLeave
        bt_Cancel_mass.BackgroundImage = My.Resources.add_mass_cancel

    End Sub




    Private Sub TextBox2_Click(sender As Object, e As EventArgs) Handles Name_tBox.Click
        If Name_tBox.Text = "Use Custom Name" Then
            Name_tBox.Text = Nothing

        End If
    End Sub


    Private Sub Anime_Add_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        Btn_Close.Location = New Point(Me.Width - 40, 1)
        Btn_min.Location = New Point(Me.Width - 68, 10)
    End Sub

    Private Sub Btn_min_Click(sender As Object, e As EventArgs) Handles Btn_min.Click
        Me.WindowState = System.Windows.Forms.FormWindowState.Minimized
    End Sub

    Private Sub Btn_Close_Click(sender As Object, e As EventArgs) Handles Btn_Close.Click

        Me.Close()
    End Sub

    Private Sub Btn_min_MouseEnter(sender As Object, e As EventArgs) Handles Btn_min.MouseEnter

        Btn_min.Image = My.Resources.main_mini_red
    End Sub

    Private Sub Btn_min_MouseLeave(sender As Object, e As EventArgs) Handles Btn_min.MouseLeave

        Btn_min.Image = Main.MinImg
    End Sub
    Private Sub Btn_Close_MouseEnter(sender As Object, e As EventArgs) Handles Btn_Close.MouseEnter

        Btn_Close.Image = My.Resources.main_del
    End Sub

    Private Sub Btn_Close_MouseLeave(sender As Object, e As EventArgs) Handles Btn_Close.MouseLeave

        Btn_Close.Image = Main.CloseImg
    End Sub

    Private Sub TextBox4_Click(sender As Object, e As EventArgs) Handles Pfad_tBox.Click
        Dim FolderBrowserDialog1 As New FolderBrowserDialog()
        FolderBrowserDialog1.RootFolder = Environment.SpecialFolder.MyComputer
        If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then

            Main.Pfad = FolderBrowserDialog1.SelectedPath
            My.Settings.Pfad = Main.Pfad
            My.Settings.Save()
            Pfad_tBox.Text = Main.Pfad
            If My.Settings.TempFolder = Nothing Then
                Main.TempFolder = Main.Pfad
            End If

        End If
    End Sub


    Private Sub GroupBox1_VisibleChanged(sender As Object, e As EventArgs) Handles groupBox1.VisibleChanged
        If Not Name_tBox.Text = "Use Custom Name" And CBool(InStr(Name_tBox.Text, "++")) = False Then
            Name_tBox.Text = "Use Custom Name"
        End If
    End Sub

    Private Sub CB_AudioDevices_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CB_AudioDevices.SelectedIndexChanged


        For i As Integer = 0 To AudioDevices.Count - 1
            If AudioDevices(i).Content = CB_AudioDevices.SelectedItem.ToString Then
                selectedDevice = AudioDevices(i).Content
                selectedDeviceKey = AudioDevices(i).Url
            End If
        Next

        'Try

        '    Dim rkg As RegistryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\ControlSet001\Control\DeviceClasses\{e6327cad-dcec-4949-ae8a-991e976a79d2}")

        '    Dim S() As String = rkg.GetSubKeyNames()
        '    For i As Integer = 0 To S.Count - 1
        '        'ComboBox1.Items.Add(S(i))
        '        Dim SubKey As RegistryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\ControlSet001\Control\DeviceClasses\{e6327cad-dcec-4949-ae8a-991e976a79d2}\" + S(i) + "\#\Device Parameters")
        '        If CB_AudioDevices.SelectedItem.ToString = SubKey.GetValue("FriendlyName").ToString() Then
        '            Dim DeviceKey() As String = S(i).Split(New String() {"#"}, System.StringSplitOptions.RemoveEmptyEntries)
        '            selectedDevice = SubKey.GetValue("FriendlyName").ToString()
        '            selectedDeviceKey = DeviceKey(3)
        '            'TextBox3.Text = DeviceKey(3)
        '            'pictureBox4.Enabled = True
        '            'pictureBox4.Image = My.Resources.main_button_download_default
        '        End If
        '    Next
        '    'Pfad = rkg.GetValue("Ordner").ToString
        'Catch ex As Exception
        'End Try



    End Sub
End Class

