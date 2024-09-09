﻿Option Strict On

Imports Microsoft.Win32
Imports System.Net
Imports System.IO
Imports System.Text
Imports System.Threading
Imports MetroFramework.Forms
Imports MetroFramework
Imports MetroFramework.Components
Imports System.Text.RegularExpressions

Public Class Einstellungen
    Inherits MetroForm

    Dim Manager As MetroStyleManager = Main.Manager
    Dim LastVersionString As String = "v3.8-Beta"

    Public CR_SoftSubsTemp As New List(Of String)


    Private Sub Einstellungen_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Label6.Text = "You have: v" + Application.ProductVersion.ToString '+ " WebView2_Test"

        BackgroundWorker1.RunWorkerAsync()


        'CR_Anime_Folge = CR_Name_Staffel0_Folge1(1)
        'If GitHubLastTag1(0)
        CR_SoftSubsTemp.AddRange(Main.SoftSubs)

        Manager.Owner = Me
        Me.StyleManager = Manager


        CB_Cap.Checked = My.Settings.Captions

        TempTB.Text = Main.TempFolder
        LeadingZeroDD.SelectedIndex = Main.LeadingZero


        CB_Ignore.SelectedIndex = Main.IgnoreSeason


        If Main.IncludeLangName = True Then
            CB_SoftSubSettings.SelectedIndex = 1
        Else
            CB_SoftSubSettings.SelectedIndex = 0
        End If

        If Main.LangNameType = 1 Then
            LangNameType_DD.SelectedIndex = 1
        ElseIf Main.LangNameType = 2 Then
            LangNameType_DD.SelectedIndex = 2
        Else
            LangNameType_DD.SelectedIndex = 0
        End If

        If Main.KodiNaming = True Then
            CB_Kodi.Checked = True
        End If

        If Main.DubMode = True Then
            DubMode.Checked = True
        End If

        If Main.CR_Chapters = True Then
            ChB_Chapters.Checked = True
        End If



        If Main.DarkModeValue = True Then
            DarkMode.Checked = True
            GroupBoxColor(Color.FromArgb(150, 150, 150))
            pictureBox1.Image = Main.CloseImg
        Else
            GroupBoxColor(Color.FromArgb(0, 0, 0))
            DarkMode.Checked = False
        End If

        TabControl1.SelectedIndex = 0

#Region "sof subs"
        CR_SoftSubDefault.SelectedIndex = 0

        For i As Integer = 1 To Main.LangValueEnum.Count - 2 ' index 0 = 'null' | last index = jp

            If Main.SoftSubs.Contains(Main.LangValueEnum(i).CR_Value) Then
                CR_SoftSubDefault.Items.Add(Main.LangValueEnum(i).Name)

            End If

        Next
        CR_SoftSubs.SelectedIndex = 0

#End Region


        Me.Location = New Point(CInt(Main.Location.X + Main.Width / 2 - Me.Width / 2), CInt(Main.Location.Y + Main.Height / 2 - Me.Height / 2))
        Try
            Me.Icon = My.Resources.icon
        Catch ex As Exception

        End Try






        If Main.VideoFormat = ".mkv" Then
            CB_Format.SelectedItem = "MKV"
        ElseIf Main.VideoFormat = ".aac" Then
            CB_Format.SelectedItem = "AAC (Audio only)"
        Else
            CB_Format.SelectedItem = "MP4"
        End If



        Try
            GB_Resolution.Text = Main.GB_Resolution_Text
            GB_SubLanguage.Text = Main.GB_SubLanguage_Text
        Catch ex As Exception

        End Try

        If Main.Reso = 1080 Then
            A1080p.Checked = True
        ElseIf Main.Reso = 720 Then
            A720p.Checked = True
        ElseIf Main.Reso = 480 Then
            A480p.Checked = True
        ElseIf Main.Reso = 360 Then
            A360p.Checked = True
        ElseIf Main.Reso = 42 Then
            AAuto.Checked = True
        End If

        CB_CR_Harsubs.Items.Clear()

        For i As Integer = 0 To Main.LangValueEnum.Count - 1
            CB_CR_Harsubs.Items.Add(Main.LangValueEnum(i).Name)
            If Main.LangValueEnum(i).CR_Value = Main.SubSprache.CR_Value Then
                'MsgBox(CB_CR_Harsubs.Items.Count.ToString)
                'MsgBox(i.ToString)
                CB_CR_Harsubs.SelectedIndex = i
                'Exit For
            End If

        Next






        DD_Season_Prefix.Text = Main.Season_Prefix

        DD_Episode_Prefix.Text = Main.Episode_Prefix





        Try

            If CBool(InStr(Main.ffmpeg_command, "-c copy")) Then
                FFMPEG_CommandP1.Text = "-c copy"
                FFMPEG_CommandP2.Enabled = False
                FFMPEG_CommandP3.Enabled = False
                FFMPEG_CommandP4.Text = "-c:a copy -bsf:a aac_adtstoasc"
            ElseIf CBool(InStr(Main.ffmpeg_command, "-c:a copy ")) Then
                Dim ffmpegDisplayCurrent As String() = Main.ffmpeg_command.Split(New String() {" "}, System.StringSplitOptions.RemoveEmptyEntries)
                If ffmpegDisplayCurrent.Count > 8 Then
                    FFMPEG_CommandP1.Text = ffmpegDisplayCurrent(0) + " " + ffmpegDisplayCurrent(1)
                    FFMPEG_CommandP2.Text = ffmpegDisplayCurrent(2) + " " + ffmpegDisplayCurrent(3)
                    FFMPEG_CommandP3.Text = ffmpegDisplayCurrent(4) + " " + ffmpegDisplayCurrent(5)
                    FFMPEG_CommandP4.Text = "-c:a copy -bsf:a aac_adtstoasc"
                Else
                    FFMPEG_CommandP1.Text = ffmpegDisplayCurrent(0) + " " + ffmpegDisplayCurrent(1)
                    FFMPEG_CommandP2.Text = "[no Preset]"
                    FFMPEG_CommandP3.Text = ffmpegDisplayCurrent(2) + " " + ffmpegDisplayCurrent(3)
                    FFMPEG_CommandP4.Text = "-c:a copy -bsf:a aac_adtstoasc"
                End If


            Else

                Dim ffmpegDisplayCurrent As String() = Main.ffmpeg_command.Split(New String() {" "}, System.StringSplitOptions.RemoveEmptyEntries)
                FFMPEG_CommandP1.Text = ffmpegDisplayCurrent(0) + " " + ffmpegDisplayCurrent(1)
                FFMPEG_CommandP2.Text = ffmpegDisplayCurrent(2) + " " + ffmpegDisplayCurrent(3)
                FFMPEG_CommandP3.Text = ffmpegDisplayCurrent(4) + " " + ffmpegDisplayCurrent(5)
                FFMPEG_CommandP4.Text = "-c:a copy -bsf:a aac_adtstoasc"
            End If


            If FFMPEG_CommandP1.Text = "-c:v libsvtav1" And FFMPEG_CommandP2.Text = "[no Preset]" Then
                FFMPEG_CommandP2.Enabled = False
                FFMPEG_CommandP3.Enabled = True
            End If

        Catch ex As Exception
            MsgBox("Error processing ffmpeg command", MsgBoxStyle.Information)
        End Try
        ListViewAdd_True.Checked = Main.UseQueue









        If Main.DefaultSubCR = "de-DE" Then
            CR_SoftSubDefault.SelectedItem = "Deutsch"
        ElseIf Main.DefaultSubCR = "en-US" Then
            CR_SoftSubDefault.SelectedItem = "English"
        ElseIf Main.DefaultSubCR = "pt-BR" Then
            CR_SoftSubDefault.SelectedItem = "Português (Brasil)"
        ElseIf Main.DefaultSubCR = "es-419" Then
            CR_SoftSubDefault.SelectedItem = "Español (LA)"
        ElseIf Main.DefaultSubCR = "fr-FR" Then
            CR_SoftSubDefault.SelectedItem = "Français (France)"
        ElseIf Main.DefaultSubCR = "ar-SA" Then
            CR_SoftSubDefault.SelectedItem = "العربية (Arabic)"
        ElseIf Main.DefaultSubCR = "ru-RU" Then
            CR_SoftSubDefault.SelectedItem = "Русский (Russian)"
        ElseIf Main.DefaultSubCR = "it-IT" Then
            CR_SoftSubDefault.SelectedItem = "Italiano (Italian)"
        ElseIf Main.DefaultSubCR = "es-ES" Then
            CR_SoftSubDefault.SelectedItem = "Español (España)"
        Else
            CR_SoftSubDefault.SelectedItem = "[Disabled]"
        End If


        Dim NameParts As String() = Main.NameBuilder.Split(New String() {";"}, System.StringSplitOptions.RemoveEmptyEntries)



        For i As Integer = 0 To NameParts.Count - 1

            If NameParts(i) = "AnimeTitle" Then
                CB_Anime.Checked = True
            ElseIf NameParts(i) = "Season" Then
                CB_Season.Checked = True
            ElseIf NameParts(i) = "EpisodeNR" Then
                CB_EpisodeNR.Checked = True
            ElseIf NameParts(i) = "EpisodeName" Then
                CB_EpisodeName.Checked = True
            ElseIf NameParts(i) = "AnimeDub" Then
                CB_AnimeDub.Checked = True
            End If

        Next





    End Sub

    Private Sub Btn_Save_Click(sender As Object, e As EventArgs) Handles Btn_Save.Click
        Main.LeadingZero = LeadingZeroDD.SelectedIndex
        My.Settings.LeadingZero = LeadingZeroDD.SelectedIndex

        My.Settings.Captions = CB_Cap.Checked



        Main.IgnoreSeason = CB_Ignore.SelectedIndex
        My.Settings.IgnoreSeason = CB_Ignore.SelectedIndex


        If DubMode.Checked = True Then
            Main.DubMode = True
            My.Settings.DubMode = True
        Else
            Main.DubMode = False
            My.Settings.DubMode = False

        End If

        If ChB_Chapters.Checked = True Then
            Main.CR_Chapters = True
            My.Settings.CR_Chapters = True
        Else
            Main.CR_Chapters = False
            My.Settings.CR_Chapters = False
        End If


        If CB_Kodi.Checked = True Then
            Main.KodiNaming = True
            My.Settings.KodiSupport = True
        Else
            Main.KodiNaming = False
            My.Settings.KodiSupport = False
        End If




        If DD_Season_Prefix.Text IsNot "[default season prefix]" Then
            Main.Season_Prefix = DD_Season_Prefix.Text
            My.Settings.Prefix_S = Main.Season_Prefix
        End If

        If DD_Episode_Prefix.Text IsNot "[default episode prefix]" Then
            Main.Episode_Prefix = DD_Episode_Prefix.Text
            My.Settings.Prefix_E = Main.Episode_Prefix
        End If

        If A1080p.Checked Then
            Main.Reso = 1080
            My.Settings.Reso = Main.Reso
        ElseIf A720p.Checked Then
            Main.Reso = 720
            My.Settings.Reso = Main.Reso
        ElseIf A360p.Checked Then
            Main.Reso = 360
            My.Settings.Reso = Main.Reso
        ElseIf A480p.Checked Then
            Main.Reso = 480
            My.Settings.Reso = Main.Reso
        ElseIf AAuto.Checked Then
            Main.Reso = 42
            My.Settings.Reso = Main.Reso
        End If


        For i As Integer = 0 To Main.LangValueEnum.Count - 1

            If CB_CR_Harsubs.SelectedItem.ToString = Main.LangValueEnum(i).Name Then
                Main.SubSprache = Main.LangValueEnum(i)
                My.Settings.Subtitle = Main.SubSprache.CR_Value
                'MsgBox(Main.LangValueEnum(i).Name)
                'MsgBox(Main.LangValueEnum(i).CR_Value)
                Exit For
            End If

        Next




        If CR_SoftSubDefault.SelectedItem.ToString = "English" Then
            Main.DefaultSubCR = "en-US"
            My.Settings.DefaultSubCR = Main.DefaultSubCR
        ElseIf CR_SoftSubDefault.SelectedItem.ToString = "Deutsch" Then
            Main.DefaultSubCR = "de-DE"
            My.Settings.DefaultSubCR = Main.DefaultSubCR
        ElseIf CR_SoftSubDefault.SelectedItem.ToString = "Português (Brasil)" Then
            Main.DefaultSubCR = "pt-BR"
            My.Settings.DefaultSubCR = Main.DefaultSubCR
        ElseIf CR_SoftSubDefault.SelectedItem.ToString = "Español (LA)" Then
            Main.DefaultSubCR = "es-419"
            My.Settings.DefaultSubCR = Main.DefaultSubCR
        ElseIf CR_SoftSubDefault.SelectedItem.ToString = "Français (France)" Then
            Main.DefaultSubCR = "fr-FR"
            My.Settings.DefaultSubCR = Main.DefaultSubCR
        ElseIf CR_SoftSubDefault.SelectedItem.ToString = "العربية (Arabic)" Then
            Main.DefaultSubCR = "ar-SA"
            My.Settings.DefaultSubCR = Main.DefaultSubCR
        ElseIf CR_SoftSubDefault.SelectedItem.ToString = "Русский (Russian)" Then
            Main.DefaultSubCR = "ru-RU"
            My.Settings.DefaultSubCR = Main.DefaultSubCR
        ElseIf CR_SoftSubDefault.SelectedItem.ToString = "Italiano (Italian)" Then
            Main.DefaultSubCR = "it-IT"
            My.Settings.DefaultSubCR = Main.DefaultSubCR
        ElseIf CR_SoftSubDefault.SelectedItem.ToString = "Español (España)" Then
            Main.DefaultSubCR = "es-ES"
            My.Settings.DefaultSubCR = Main.DefaultSubCR
        ElseIf CR_SoftSubDefault.SelectedItem.ToString = "[Disabled]" Then
            Main.DefaultSubCR = "None"
            My.Settings.DefaultSubCR = Main.DefaultSubCR
        End If


        Main.NameBuilder = TB_NameString.Text

        My.Settings.NameTemplate = Main.NameBuilder


        If CB_Format.Text = "MKV" Then
            Main.VideoFormat = ".mkv"
            My.Settings.VideoFormat = Main.VideoFormat
        ElseIf CB_Format.Text = "AAC (Audio only)" Then
            Main.VideoFormat = ".aac"
            My.Settings.VideoFormat = Main.VideoFormat
        Else
            Main.VideoFormat = ".mp4"
            My.Settings.VideoFormat = Main.VideoFormat
        End If

        If CB_Merge.SelectedIndex > 0 Then
            Main.MergeSubs = True
            Main.MergeSubsFormat = CB_Merge.SelectedItem.ToString
            My.Settings.MergeSubs = Main.MergeSubsFormat

        Else
            Main.MergeSubsFormat = CB_Merge.SelectedItem.ToString
            Main.MergeSubs = False
            My.Settings.MergeSubs = Main.MergeSubsFormat
        End If








        Dim ffpmeg_cmd As String = Nothing
        If FFMPEG_CommandP1.Text = "-c copy" Then
            ffpmeg_cmd = " " + FFMPEG_CommandP1.Text + " " + FFMPEG_CommandP4.Text
        ElseIf FFMPEG_CommandP2.Text = "[no Preset]" Then

            ffpmeg_cmd = " " + FFMPEG_CommandP1.Text + " " + FFMPEG_CommandP3.Text + " " + FFMPEG_CommandP4.Text
        Else

            ffpmeg_cmd = " " + FFMPEG_CommandP1.Text + " " + FFMPEG_CommandP2.Text + " " + FFMPEG_CommandP3.Text + " " + FFMPEG_CommandP4.Text
        End If

        If Main.ffmpeg_command = My.Settings.ffmpeg_command_override Then
            'override should not get overwritten 

        Else
            Main.ffmpeg_command = ffpmeg_cmd
            My.Settings.ffmpeg_command = Main.ffmpeg_command
        End If



        'If CBool(InStr(FFMPEG_CommandP1.Text, "nvenc")) = True And CBool(Main.VideoFormat = ".aac") = False Then
        '    If NumericUpDown1.Value > 2 Then
        '        NumericUpDown1.Value = 2
        '    End If

        'ElseIf CBool(InStr(FFMPEG_CommandP1.Text, "libx26")) = True And CBool(Main.VideoFormat = ".aac") = False Then
        '    If NumericUpDown1.Value > 1 Then
        '        NumericUpDown1.Value = 1
        '    End If
        'End If

        If ListViewAdd_True.Checked = True Then
            Main.UseQueue = True
            My.Settings.QueueMode = Main.UseQueue
        ElseIf ListViewAdd_True.Checked = False Then
            Main.UseQueue = False
            My.Settings.QueueMode = Main.UseQueue
        End If


        Main.SoftSubs.Clear()
        Main.SoftSubs.AddRange(CR_SoftSubsTemp)


        Dim SaveString As String = Nothing
        For ii As Integer = 0 To Main.SoftSubs.Count - 1
            If SaveString = Nothing Then
                SaveString = Main.SoftSubs(ii)
            Else
                SaveString = SaveString + "," + Main.SoftSubs(ii)
            End If
        Next
        If SaveString = Nothing Then
            SaveString = "None"
        End If
        My.Settings.AddedSubs = SaveString




        If CB_SoftSubSettings.SelectedIndex = 0 Then
            Main.IncludeLangName = False
            My.Settings.IncludeLangName = Main.IncludeLangName
        Else
            Main.IncludeLangName = True
            My.Settings.IncludeLangName = Main.IncludeLangName
        End If

        If LangNameType_DD.SelectedIndex = 1 Then
            Main.LangNameType = 1
            My.Settings.LangNameType = Main.LangNameType
        ElseIf LangNameType_DD.SelectedIndex = 2 Then
            Main.LangNameType = 2
            My.Settings.LangNameType = Main.LangNameType
        Else
            Main.LangNameType = 0
            My.Settings.LangNameType = Main.LangNameType
        End If

        My.Settings.Save()

        Me.Close()
    End Sub







    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles pictureBox1.Click
        Me.Close()
    End Sub
#Region "UI"

    Private Sub Btn_Close_MouseEnter(sender As Object, e As EventArgs) Handles pictureBox1.MouseEnter

        pictureBox1.Image = My.Resources.main_del
    End Sub

    Private Sub Btn_Close_MouseLeave(sender As Object, e As EventArgs) Handles pictureBox1.MouseLeave

        pictureBox1.Image = Main.CloseImg
    End Sub

    Private Sub Btn_Save_MouseEnter(sender As Object, e As EventArgs) Handles Btn_Save.MouseEnter, Btn_Save.GotFocus
        Btn_Save.Image = My.Resources.crdSettings_Button_SafeExit_hover
    End Sub

    Private Sub Btn_Save_MouseLeave(sender As Object, e As EventArgs) Handles Btn_Save.MouseLeave, Btn_Save.LostFocus
        Btn_Save.Image = My.Resources.crdSettings_Button_SafeExit
    End Sub


    Private Sub ComboBox1_DrawItem(sender As Object, e As DrawItemEventArgs) Handles CB_CR_Harsubs.DrawItem
        Dim CB As ComboBox = CType(sender, ComboBox)
        CB.BackColor = Color.White
        If e.Index >= 0 Then
            Using st As New StringFormat With {.Alignment = StringAlignment.Center}
                ' e.DrawBackground()
                ' e.DrawFocusRectangle()
                e.Graphics.FillRectangle(SystemBrushes.ControlLightLight, e.Bounds)
                e.Graphics.DrawString(CB.Items(e.Index).ToString, e.Font, Brushes.Black, e.Bounds, st)

            End Using
        End If
    End Sub






    Private Sub AAuto_Click(sender As Object, e As EventArgs) Handles AAuto.Click
        If CB_Merge.SelectedIndex > 0 Then
            If AAuto.Checked = True Then
                If MessageBox.Show("Resolution '[Auto]' and merge the subtitle with the video file will download all resolutions!" + vbNewLine + "Press 'Yes' to enable it anyway", "Prepare for unforeseen consequences.", MessageBoxButtons.YesNo) = DialogResult.Yes Then

                Else
                    AAuto.Checked = False
                    A360p.Checked = True
                End If
            End If
            'ElseIf HybridMode_CB.Checked = True Then
            '    If AAuto.Checked = True Then
            '        MsgBox("Resolution '[Auto]' and 'Hybride Mode' does not work together", MsgBoxStyle.Information)
            '        AAuto.Checked = False
            '        A1080p.Checked = True
            '    End If
        End If
    End Sub

    Private Sub MergeMP4_Click(sender As Object, e As EventArgs)
        If CB_Merge.SelectedIndex > 0 Then
            If AAuto.Checked = True Then
                If MessageBox.Show("Resolution '[Auto]' and merge the subtitle with the video file will download all resolutions!" + vbNewLine + "Press 'Yes' to enable it anyway", "Prepare for unforeseen consequences.", MessageBoxButtons.YesNo) = DialogResult.Yes Then

                Else
                    CB_Merge.SelectedIndex = 0
                End If
            End If
        End If
    End Sub












    Private Sub ListC1_Click(sender As Object, e As EventArgs) Handles copy.Click, nv_h264.Click, nv_hevc.Click, nv_AV1.Click, CPU_h264.Click, CPU_h265.Click, CPU_AV1.Click, AMD_h264.Click, AMD_hevc.Click, Intel_h264.Click, Intel_hevc.Click, Intel_AV1.Click
        Dim Button As ToolStripMenuItem = CType(sender, ToolStripMenuItem)

        If CBool(InStr(Button.Text, "av1")) Then
            If MessageBox.Show("The inculded ffmpeg version does not support any AV1 encoders." + vbNewLine + "The 'Help' button gets you to the ffmpeg download page.", "AV1 support", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, 0, "https://ffmpeg.org/download.html", "") = DialogResult.Cancel Then
                Exit Sub
            End If
        End If

        If Button.Text = "-c copy" Then
            FFMPEG_CommandP1.Text = "-c copy"
            FFMPEG_CommandP2.Enabled = False
            FFMPEG_CommandP3.Enabled = False

        ElseIf Button.Text = "-c:v libsvtav1" Then
            FFMPEG_CommandP1.Text = Button.Text
            FFMPEG_CommandP2.Text = "[no Preset]"
            FFMPEG_CommandP2.Enabled = False
            FFMPEG_CommandP3.Enabled = True
        Else
            FFMPEG_CommandP1.Text = Button.Text
            FFMPEG_CommandP2.Enabled = True
            FFMPEG_CommandP3.Enabled = True
        End If

    End Sub

    Private Sub ListP1_Click(sender As Object, e As EventArgs) Handles ListP1.Click, ListP2.Click, ListP3.Click
        Dim Button As ToolStripMenuItem = CType(sender, ToolStripMenuItem)
        FFMPEG_CommandP2.Text = Button.Text
        FFMPEG_CommandP2.Enabled = True
        FFMPEG_CommandP3.Enabled = True
    End Sub

    Private Sub ListBit1_Click(sender As Object, e As EventArgs) Handles ListBit_7000.Click, ListBit_6500.Click, ListBit_6000.Click, ListBit_5500.Click, ListBit_5000.Click, ListBit_4500.Click, ListBit_4000.Click, ListBit_3500.Click, ListBit_3000.Click, ListBit_2500.Click, ListBit_2000.Click, ListBit_1500.Click, ListBit_1000.Click
        Dim Button As ToolStripMenuItem = CType(sender, ToolStripMenuItem)
        FFMPEG_CommandP3.Text = Button.Text
        FFMPEG_CommandP2.Enabled = True
        FFMPEG_CommandP3.Enabled = True
    End Sub



    Private Sub Label7_Click(sender As Object, e As EventArgs)
        Process.Start("https://learn.microsoft.com/de-de/microsoft-edge/webview2/")
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs)
        Process.Start("https://www.ffmpeg.org/about.html")
    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs)
        Process.Start("https://github.com/hama3254/metroframework-modern-ui")
    End Sub



    Sub GroupBoxColor(ByVal color As Color)
        FFMPEG_CommandP1.ForeColor = color
        FFMPEG_CommandP2.ForeColor = color
        FFMPEG_CommandP3.ForeColor = color
        FFMPEG_CommandP4.ForeColor = color
        SoftSubs.ForeColor = color
        GB_SubLanguage.ForeColor = color
        GB_Resolution.ForeColor = color
        GB_Filename_Pre.ForeColor = color
        GroupBox2.ForeColor = color
        GroupBox3.ForeColor = color
        GroupBox4.ForeColor = color
        GroupBox5.ForeColor = color
        GroupBox12.ForeColor = color
        GroupBox14.ForeColor = color
        GroupBox16.ForeColor = color
        GroupBox17.ForeColor = color
        GroupBox18.ForeColor = color
        GroupBox20.ForeColor = color
    End Sub





    Private Sub DarkMode_CheckedChanged(sender As Object, e As EventArgs) Handles DarkMode.CheckedChanged

        If DarkMode.Checked = True Then
            My.Settings.DarkModeValue = True
            Manager.Theme = MetroThemeStyle.Dark
            GroupBoxColor(Color.FromArgb(150, 150, 150))

            Main.DarkMode()
            Main.DarkModeValue = True
            pictureBox1.Image = Main.CloseImg
        Else
            Main.DarkModeValue = False
            My.Settings.DarkModeValue = False

            Manager.Theme = MetroThemeStyle.Light
            Main.LightMode()
            GroupBoxColor(Color.FromArgb(0, 0, 0))

            pictureBox1.Image = Main.CloseImg
        End If
    End Sub


    Private Sub Server_Click(sender As Object, e As EventArgs)
        'If Server.Checked = True Then
        '    MsgBox("This feature requires a restart of the downloader", MsgBoxStyle.Information)
        'End If
    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Try
            Dim client0 As New WebClient
            client0.Encoding = Encoding.UTF8
            client0.Headers.Add(My.Settings.User_Agend.Replace(Chr(34), ""))

            Dim str0 As String = client0.DownloadString("https://api.github.com/repos/hama3254/Crunchyroll-Downloader-v3.0/releases")

            Dim GitHubLastIsPre() As String = str0.Split(New String() {Chr(34) + "prerelease" + Chr(34) + ": "}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim LastNonPreRelase As Integer = 0

            For i As Integer = 1 To GitHubLastIsPre.Count - 1
                Dim GitHubLastIsPre1() As String = GitHubLastIsPre(i).Split(New String() {","}, System.StringSplitOptions.RemoveEmptyEntries)

                If GitHubLastIsPre1(0) = "false" Then
                    LastNonPreRelase = i
                    Exit For
                End If
            Next

            Dim GitHubLastTag() As String = str0.Split(New String() {Chr(34) + "tag_name" + Chr(34) + ": " + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim GitHubLastTag1() As String = GitHubLastTag(LastNonPreRelase).Split(New String() {Chr(34) + ","}, System.StringSplitOptions.RemoveEmptyEntries)

            LastVersionString = GitHubLastTag1(0)

            'Debug.WriteLine(GitHubLastTag1(0))

        Catch ex As Exception
            Debug.WriteLine(ex.ToString)
        End Try
    End Sub


    Private Sub TabPage7_Enter(sender As Object, e As EventArgs) Handles TabPage7.Enter
        LastVersion.Text = "last release: " + LastVersionString
    End Sub

    Private Sub CB_Format_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CB_Format.SelectedIndexChanged
        If CB_Format.Text = "AAC (Audio only)" Then
            CB_Merge.Items.Clear()
            CB_Merge.Items.Add("[merge disabled]")
            CB_Merge.SelectedIndex = 0
            CB_Merge.Enabled = False
        ElseIf CB_Format.Text = "MP4" Then
            CB_Merge.Enabled = True
            CB_Merge.Items.Clear()
            CB_Merge.Items.Add("[merge disabled]")
            CB_Merge.Items.Add("mov_text")
            CB_Merge.SelectedIndex = 0
            'CB_Merge.Items.Add("srt")
            CB_Merge.SelectedItem = Main.MergeSubsFormat
        ElseIf CB_Format.Text = "MKV" Then
            CB_Merge.Enabled = True
            CB_Merge.Items.Clear()
            CB_Merge.Items.Add("[merge disabled]")
            CB_Merge.Items.Add("copy")
            CB_Merge.Items.Add("srt")
            CB_Merge.SelectedIndex = 0
            CB_Merge.SelectedItem = Main.MergeSubsFormat
        End If

    End Sub

    Private Sub MergeMP4_CheckedChanged(sender As Object, e As EventArgs)
        If CB_Format.Text = "AAC (Audio only)" Then
            If CB_Merge.SelectedIndex > 0 Then
                MsgBox("Merged subs are not avalible with audio only!", MsgBoxStyle.Information)
            End If
            CB_Merge.SelectedIndex = 0
        End If
    End Sub


    Private Sub TempTB_Click(sender As Object, e As EventArgs) Handles TempTB.Click

        Dim FolderBrowserDialog1 As New FolderBrowserDialog()
        FolderBrowserDialog1.RootFolder = Environment.SpecialFolder.MyComputer
        If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then

            Main.TempFolder = FolderBrowserDialog1.SelectedPath
            TempTB.Text = FolderBrowserDialog1.SelectedPath
            My.Settings.TempFolder = Main.TempFolder
        End If

    End Sub
#Region "Build Name String"

    Private Sub CB_Anime_CheckedChanged(sender As Object, e As EventArgs) Handles CB_Anime.CheckedChanged, CB_Season.CheckedChanged, CB_EpisodeNR.CheckedChanged, CB_EpisodeName.CheckedChanged, CB_AnimeDub.CheckedChanged, CB_Kodi.CheckedChanged
        If CB_Anime.Checked = True And CBool(InStr(TB_NameString.Text, "AnimeTitle;")) = False Then
            TB_NameString.AppendText("AnimeTitle;")
        ElseIf CB_Anime.Checked = False Then
            TB_NameString.Text = TB_NameString.Text.Replace("AnimeTitle;", "")
        End If

        If CB_Season.Checked = True And CBool(InStr(TB_NameString.Text, "Season;")) = False Then
            TB_NameString.AppendText("Season;")
        ElseIf CB_Season.Checked = False Then
            TB_NameString.Text = TB_NameString.Text.Replace("Season;", "")
        End If

        If CB_EpisodeNR.Checked = True And CBool(InStr(TB_NameString.Text, "EpisodeNR;")) = False Then
            TB_NameString.AppendText("EpisodeNR;")
        ElseIf CB_EpisodeNR.Checked = False Then
            TB_NameString.Text = TB_NameString.Text.Replace("EpisodeNR;", "")
        End If

        If CB_EpisodeName.Checked = True And CBool(InStr(TB_NameString.Text, "EpisodeName;")) = False Then
            TB_NameString.AppendText("EpisodeName;")
        ElseIf CB_EpisodeName.Checked = False Then
            TB_NameString.Text = TB_NameString.Text.Replace("EpisodeName;", "")
        End If

        If CB_AnimeDub.Checked = True And CBool(InStr(TB_NameString.Text, "AnimeDub;")) = False Then
            TB_NameString.AppendText("AnimeDub;")
        ElseIf CB_AnimeDub.Checked = False Then
            TB_NameString.Text = TB_NameString.Text.Replace("AnimeDub;", "")
        End If



    End Sub


    Private Sub DD_Season_Prefix_UserAction(sender As Object, e As EventArgs) Handles DD_Season_Prefix.Click, DD_Season_Prefix.GotFocus
        If DD_Season_Prefix.Text = Main.Season_PrefixDefault Then
            DD_Season_Prefix.Text = Nothing
        End If
    End Sub

    Private Sub DD_Season_Prefix_LostFocus(sender As Object, e As EventArgs) Handles DD_Season_Prefix.LostFocus
        If DD_Season_Prefix.Text = Nothing Then
            DD_Season_Prefix.Text = Main.Season_PrefixDefault
        End If
    End Sub


    Private Sub DD_Episode_Prefix_UserAction(sender As Object, e As EventArgs) Handles DD_Episode_Prefix.Click, DD_Episode_Prefix.GotFocus
        If DD_Episode_Prefix.Text = Main.Episode_PrefixDefault Then
            DD_Episode_Prefix.Text = Nothing
        End If
    End Sub

    Private Sub DD_Episode_Prefix_LostFocus(sender As Object, e As EventArgs) Handles DD_Episode_Prefix.LostFocus
        If DD_Episode_Prefix.Text = Nothing Then
            DD_Episode_Prefix.Text = Main.Episode_PrefixDefault
        End If
    End Sub

    Private Sub CR_SoftSubs_Change(sender As Object, e As EventArgs) Handles CR_SoftSubs.Click
        Dim Popup As New CheckBoxComboBox
        Popup.Text = "CR Sub selection"
        Popup.Show()
    End Sub



    'Private Sub CB_CR_Audio_Click(sender As Object, e As EventArgs) Handles CB_CR_Audio.Click
    '    Dim Popup As New CheckBoxComboBox
    '    Popup.Text = "CR Dub selection"
    '    Popup.Show()
    'End Sub





#End Region

#End Region


End Class