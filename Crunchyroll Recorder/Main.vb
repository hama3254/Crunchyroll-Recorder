Option Strict On

Imports System.IO
Imports System.Threading
Imports MetroFramework.Forms
Imports MetroFramework
Imports MetroFramework.Components
Imports System.Runtime.InteropServices
Imports MyProvider.MyProvider
Imports System.Globalization

Public Class Main
    Inherits MetroForm


    Public CR_JS_mod As New List(Of ServerResponseCache)
    Public CR_JS_modShort As New List(Of String)


    Public Manager As New MetroStyleManager
    Public DarkModeValue As Boolean = False
    Public invalids As Char() = System.IO.Path.GetInvalidFileNameChars()

    Public KodiNaming As Boolean = False

    Public ListBoxList As New List(Of String)

    Public RunningDownloads As Integer = 0
    Public UseQueue As Boolean = False
    Public ResoAvalibe As String = Nothing
    Public ResoSearchRunning As Boolean = False
    Public UsedMap As String = Nothing
    Public Debug1 As Boolean = False
    Public Debug2 As Boolean = False
    Public LogBrowserData As Boolean = False
    Public Thumbnail As String = Nothing
    Public MergeSubs As Boolean = False
    Public IgnoreSeason As Integer = 0



    Public VideoFormat As String = ".mp4"
    Public MergeSubsFormat As String = "mov_text"

    Public DialogTaskString As String

    Public UserCloseDialog As Boolean = False
    Dim Aktuell As String
    Dim Gesamt As String
    Public LabelUpdate As String = "Status: idle"
    Public LabelEpisode As String = "..."
    Public b As Boolean
    Public LoginOnly As String = "False"
    Public Pfad As String = My.Computer.FileSystem.CurrentDirectory
    Public TempFolder As String = Pfad
    Public ProfileFolder As String = Path.Combine(Application.StartupPath, "CRD-Profile") 'Path.Combine(My.Computer.FileSystem.SpecialDirectories.MyDocuments, "CRD-Profile")
    Public ffmpeg_command As String = " -c copy -bsf:a aac_adtstoasc" '" -c:v hevc_nvenc -preset fast -b:v 6M -bsf:a aac_adtstoasc " 
    Public Reso As Integer
    Public Season_Prefix As String = "[default season prefix]"
    Public Season_PrefixDefault As String = "[default season prefix]"
    Public Episode_Prefix As String = "[default episode prefix]"
    Public Episode_PrefixDefault As String = "[default episode prefix]"

    Public ResoSave As String = "6666x6666"
    Public ResoFunBackup As String = "6666x6666"

    Public LangValueEnum As New List(Of NameValuePair)
    Public DubSprache As NameValuePair = New NameValuePair("Japanese", "ja-JP", Nothing)
    Public SubSprache As NameValuePair = New NameValuePair("[ null ]", "", Nothing)

    Public SoftSubs As New List(Of String)
    Public IncludeLangName As Boolean = False
    Public LangNameType As Integer = 0
    Public HybridThread As Integer = CInt(Environment.ProcessorCount / 2 - 1)
    Public TempSoftSubs As New List(Of String)
    Public AbourtList As New List(Of String)
    Public watingList As New List(Of String)
    Public GeckoLogFile As String = Nothing
    Dim SoftSubsString As String
    Dim CR_Unlock_Error As String
    Dim SubSprache2 As String
    'Dim URL_DL As String
    'Dim Pfad_DL As String
    Public Grapp_RDY As Boolean = True
    Public Funimation_Grapp_RDY As Boolean = True
    Public Grapp_non_cr_RDY As Boolean = True
    Public Grapp_Abord As Boolean = False
    Public NameBuilder As String = ""
    Public LeadingZero As Integer = 1
    Public MaxDL As Integer
    Public ResoNotFoundString As String
    Public ResoBackString As String
    Public WebbrowserHeadText As String = Nothing
    Public WebbrowserSoftSubURL As String = Nothing
    Public WebbrowserURL As String = Nothing
    Public SystemWebBrowserCookie As String = Nothing
    Public WebbrowserText As String = Nothing
    Public WebbrowserTitle As String = Nothing
    Public WebbrowserCookie As String = Nothing
    Public UserBowser As Boolean = False
    Public DefaultSubCR As String = "Disabled"
    Public DubMode As Boolean = True
    Public CR_Chapters As Boolean = False

#Region "Sprachen Vairablen"
    Public URL_Invaild As String = "something is wrong here..."
    Dim DL_Path_String As String = "Please choose download directory."
    Public No_Stream As String = "Please make sure that the URL is correct or check if the Anime is available in your country."
    Dim TaskNotCompleed As String = "Please wait until the current task is completed."
    Dim Premium_Stream As String = "For Premium episodes you need a premium membership and be logged in the Downloader."
    Public LoginReminder As String = "Please make sure that you logged in."
    Dim Error_Mass_DL As String = "We run into a problem here." + vbNewLine + "You can try to download every episode individually."
    Dim User_Fault_NoName As String = "no name, fallback solution : "
    Dim Sub_language_NotFound As String = "Could not find the sub language" + vbNewLine + "please make sure the language is available: "
    Dim Resolution_NotFound As String = "Could not find any resolution."
    Dim Error_unknown As String = "We run into a unknown problem here." + vbNewLine + "Do you like to send an Bug report?"
    Dim ErrorNoPermisson As String = "Access is denied."
    'UI Variablen
    Public GB_Resolution_Text As String = "Resolution"
    Public GB_SubLanguage_Text As String = "Hardsub language"
    Public GB_Sub_Path_Text As String = "Sub directory"
    Public RBAnime_Text As String = "series name"
    Public RBStaffel_Text As String = "series name + season"
    Public NewStart_String As String = "to adopt all the settings, a restart is necessary."
    Public DL_Count_simultaneousText As String = "Simultaneous Downloads"
    Public GB_Sub_FormatText As String = "extended Sub Settings"
    Public LabelResoNotFoundText As String = "resolution not found" + vbNewLine + "Select another one below"
    Public LabelLangNotFoundText As String = "subtitle language not found" + vbNewLine + "Select another one below"
    Public ButtonResoNotFoundText As String = "Submit"
    'Public CB_SuB_Nothing As String = "[ null ]"
    Dim StatusToolTip As ToolTip = New ToolTip()
    Dim StatusToolTipText As String


#End Region

#Region "UI"
    Private Sub Main_TextChanged(sender As Object, e As EventArgs) Handles Me.TextChanged
        Me.Invalidate()




    End Sub

    Public CloseImg As Bitmap = My.Resources.main_del
    Public MinImg As Bitmap = My.Resources.main_mini
    Public BackColorValue As Color = Color.FromArgb(243, 243, 243)
    Public ForeColorValue As Color = SystemColors.WindowText
    Public Sub DarkMode()
        Panel1.BackColor = Color.FromArgb(50, 50, 50)
        CloseImg = My.Resources.main_close_dark
        MinImg = My.Resources.main_mini_dark
        Btn_min.Image = MinImg
        Btn_Close.Image = CloseImg
        BackColorValue = Color.FromArgb(50, 50, 50)
        ForeColorValue = Color.FromArgb(243, 243, 243)
    End Sub

    Public Sub LightMode()
        BackColorValue = Color.FromArgb(243, 243, 243)
        ForeColorValue = SystemColors.WindowText
        Panel1.BackColor = SystemColors.Control
        CloseImg = My.Resources.main_close
        MinImg = My.Resources.main_mini
        Btn_min.Image = MinImg
        Btn_Close.Image = CloseImg
    End Sub

    Dim ListViewHeightOffset As Integer = 7
    Private Sub Btn_add_MouseEnter(sender As Object, e As EventArgs) Handles Btn_add.MouseEnter, Btn_add.GotFocus
        If Manager.Theme = MetroThemeStyle.Dark Then
            Btn_add.Image = My.Resources.main_add_invert_dark
        Else
            Btn_add.Image = My.Resources.main_add_invert
        End If
    End Sub

    Private Sub Btn_add_MouseLeave(sender As Object, e As EventArgs) Handles Btn_add.MouseLeave, Btn_add.LostFocus

        Btn_add.Image = My.Resources.main_add
    End Sub

    Private Sub Btn_Browser_MouseEnter(sender As Object, e As EventArgs) Handles Btn_Browser.MouseEnter, Btn_Browser.GotFocus

        If Manager.Theme = MetroThemeStyle.Dark Then
            Btn_Browser.Image = My.Resources.main_browser_invert_dark
        Else
            Btn_Browser.Image = My.Resources.main_browser_invert
        End If
    End Sub

    Private Sub Btn_Browser_MouseLeave(sender As Object, e As EventArgs) Handles Btn_Browser.MouseLeave, Btn_Browser.LostFocus
        Btn_Browser.Image = My.Resources.main_browser
    End Sub

    Private Sub Btn_Settings_MouseEnter(sender As Object, e As EventArgs) Handles Btn_Settings.MouseEnter, Btn_Settings.GotFocus
        If Manager.Theme = MetroThemeStyle.Dark Then
            Btn_Settings.Image = My.Resources.main_setting_invert_dark
        Else
            Btn_Settings.Image = My.Resources.main_setting_invert
        End If
    End Sub

    Private Sub Btn_Settings_MouseLeave(sender As Object, e As EventArgs) Handles Btn_Settings.MouseLeave, Btn_Settings.LostFocus
        Btn_Settings.Image = My.Resources.main_settings
    End Sub

    Private Sub Btn_Queue_MouseEnter(sender As Object, e As EventArgs) Handles Btn_Queue.MouseEnter, Btn_Queue.GotFocus
        If Manager.Theme = MetroThemeStyle.Dark Then
            Btn_Queue.Image = My.Resources.main_queue_invert_dark
        Else
            Btn_Queue.Image = My.Resources.main_queue_invert
        End If
    End Sub

    Private Sub Btn_Queue_MouseLeave(sender As Object, e As EventArgs) Handles Btn_Queue.MouseLeave, Btn_Queue.LostFocus
        Btn_Queue.Image = My.Resources.main_queue
    End Sub



    Private Sub Btn_min_MouseEnter(sender As Object, e As EventArgs) Handles Btn_min.MouseEnter, Btn_min.GotFocus
        If Manager.Theme = MetroThemeStyle.Dark Then
            Btn_min.Image = My.Resources.main_mini_dark_hover
        Else
            Btn_min.Image = My.Resources.main_mini_red
        End If
    End Sub

    Private Sub Btn_min_MouseLeave(sender As Object, e As EventArgs) Handles Btn_min.MouseLeave, Btn_min.LostFocus
        Btn_min.Image = MinImg
    End Sub

    Private Sub Btn_Close_MouseEnter(sender As Object, e As EventArgs) Handles Btn_Close.MouseEnter, Btn_Close.GotFocus
        If Manager.Theme = MetroThemeStyle.Dark Then
            Btn_Close.Image = My.Resources.main_close_dark_hover
        Else
            Btn_Close.Image = My.Resources.main_close_hover
        End If
    End Sub

    Private Sub Btn_Close_MouseLeave(sender As Object, e As EventArgs) Handles Btn_Close.MouseLeave, Btn_Close.LostFocus
        Btn_Close.Image = CloseImg
    End Sub

    Private Sub ConsoleBar_Click(sender As Object, e As EventArgs) Handles ConsoleBar.Click
        If TheTextBox.Visible = True Then
            'TheTextBox.Lines = DebugList.ToArray
            TheTextBox.Visible = False
            ListViewHeightOffset = 7
            ConsoleBar.Location = New Point(0, Me.Height - ListViewHeightOffset)
            TheTextBox.Location = New Point(1, Me.Height - ListViewHeightOffset + 7)
            TheTextBox.Width = Me.Width - 2
        Else
            ListViewHeightOffset = 103
            TheTextBox.Visible = True
            ConsoleBar.Location = New Point(0, Me.Height - ListViewHeightOffset)
            TheTextBox.Location = New Point(1, Me.Height - ListViewHeightOffset + 7)
            TheTextBox.Width = Me.Width - 2
        End If
        Me.Height = Me.Height + 1
    End Sub

    Private Sub ConsoleBar_MouseEnter(sender As Object, e As EventArgs) Handles ConsoleBar.MouseEnter
        ConsoleBar.BackgroundImage = My.Resources.balken_console
    End Sub

    Private Sub ConsoleBar_MouseLeave(sender As Object, e As EventArgs) Handles ConsoleBar.MouseLeave
        ConsoleBar.BackgroundImage = My.Resources.balken
    End Sub

    Private Sub Main_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        Panel1.Width = Me.Width - 2
        Panel1.Height = Me.Height - 71 - ListViewHeightOffset
        PictureBox5.Width = Me.Width - 40
        ConsoleBar.Location = New Point(1, Me.Height - ListViewHeightOffset)
        ConsoleBar.Width = Me.Width - 40
        TheTextBox.Location = New Point(1, Me.Height - ListViewHeightOffset + 7)
        TheTextBox.Width = Me.Width - 2
        Btn_Close.Location = New Point(Me.Width - 36, 1)
        Btn_min.Location = New Point(Me.Width - 67, 1)
        Btn_Settings.Location = New Point(Me.Width - 165, 17)
        Btn_Queue.Location = New Point(Me.Width - 265, 17)
        Try
            Panel1.AutoScrollPosition = New Point(0, 0)

            Dim W As Integer = Panel1.Width
            If Panel1.Controls.Count * 142 > Panel1.Height Then
                W = Panel1.Width - SystemInformation.VerticalScrollBarWidth
            End If

            Dim Item As New List(Of CRD_List_Item)
            Item.AddRange(Panel1.Controls.OfType(Of CRD_List_Item))
            Item.Reverse()

            For s As Integer = 0 To Item.Count - 1
                Item(s).SetBounds(0, 142 * s, W - 2, 142)
                If Debug2 = True Then
                    Debug.WriteLine("Bounds: " + Item(s).GetTextBound.ToString)

                    Debug.WriteLine("Ist: " + Item(s).Location.Y.ToString)
                    Debug.WriteLine("Soll: " + (142 * s).ToString)
                End If
            Next
        Catch ex As Exception
            Debug.WriteLine(ex.ToString)
        End Try
    End Sub

#End Region
    Public Declare Function waveOutSetVolume Lib "winmm.dll" (ByVal uDeviceID As Integer, ByVal dwVolume As Integer) As Integer
    <FlagsAttribute()>
    Public Enum EXECUTION_STATE As UInteger
        ES_SYSTEM_REQUIRED = &H1
        ES_DISPLAY_REQUIRED = &H2
        ES_CONTINUOUS = &H80000000UI
    End Enum
    <DllImport("Kernel32.DLL", CharSet:=CharSet.Auto, SetLastError:=True)>
    Public Shared Function SetThreadExecutionState(ByVal state As EXECUTION_STATE) As EXECUTION_STATE
    End Function
    Public Sub SetSettingsTheme()
        Einstellungen.Theme = Manager.Theme
    End Sub





    Function AddLeadingZeros(ByVal txt As String) As String

        txt = txt.Replace(",", ".")
        Dim Post As String = Nothing
        If CBool(InStr(txt, ".")) = True Then
            Dim txt_split As String() = txt.Split(New String() {"."}, System.StringSplitOptions.RemoveEmptyEntries)
            txt = txt_split(0)
            Post = "." + txt_split(1)
        End If

        For i As Integer = 0 To LeadingZero + 1
            If txt.Count = LeadingZero + 1 Or txt.Count > LeadingZero + 1 Then
                Exit For
            Else
                txt = "0" + txt
            End If
        Next

        Dim Output As String = txt + Post

        Return Output
    End Function


    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'using "." instead of "," for everyone
        Dim cultureInfo As New CultureInfo("en-US")
        CultureInfo.DefaultThreadCurrentCulture = cultureInfo
        CultureInfo.DefaultThreadCurrentUICulture = cultureInfo

        ' MsgBox(System.Windows.Forms.Application.UserAppDataPath)
        FillArray()

#Region "settings path"

        Dim mySettings As New DirectorySettings
        mySettings.DirectoryName = Application.StartupPath
        mySettings.FileName = "User.config.dat"
        mySettings.Save() ' muss explizit gepeichert werden...

#End Region

        Me.ContextMenuStrip = ContextMenuStrip1
        Dim tbtl As TextBoxTraceListener = New TextBoxTraceListener(TheTextBox)
        Trace.Listeners.Add(tbtl)
        b = True
        Thread.CurrentThread.Name = "Main"
        Debug.WriteLine("v" + Application.ProductVersion)
        Debug.WriteLine("Thread Name: " + Thread.CurrentThread.Name)


        DarkModeValue = My.Settings.DarkModeValue


        Manager.Style = MetroColorStyle.Orange
        If DarkModeValue = True Then
            Manager.Theme = MetroThemeStyle.Dark
            DarkMode()
        Else
            Manager.Theme = MetroThemeStyle.Light
            LightMode()
        End If
        Me.StyleManager = Manager
        Manager.Owner = Me




        waveOutSetVolume(0, 0)

        'ServicePointManager.Expect100Continue = True
        'ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

        StatusToolTip.Active = True

        Try
            Me.Icon = My.Resources.icon
        Catch ex As Exception
        End Try


        If My.Settings.Pfad = Nothing Then
            Pfad = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        Else
            Pfad = My.Settings.Pfad
        End If

        If My.Settings.TempFolder = Nothing Then
            TempFolder = Pfad
        Else
            TempFolder = My.Settings.TempFolder
        End If

        Episode_Prefix = My.Settings.Prefix_E
        Season_Prefix = My.Settings.Prefix_S
        DefaultSubCR = My.Settings.DefaultSubCR
        UseQueue = My.Settings.QueueMode
        KodiNaming = My.Settings.KodiSupport
        DubMode = My.Settings.DubMode
        CR_Chapters = My.Settings.CR_Chapters
        ffmpeg_command = My.Settings.ffmpeg_command

        If My.Settings.ffmpeg_command_override = "null" Then
        Else
            ffmpeg_command = My.Settings.ffmpeg_command_override
        End If

        Reso = My.Settings.Reso
        LeadingZero = My.Settings.LeadingZero


        For i As Integer = 0 To LangValueEnum.Count - 1
            If LangValueEnum(i).CR_Value = My.Settings.Subtitle Then
                'MsgBox(My.Settings.Subtitle)
                SubSprache = LangValueEnum(i)
                Exit For
            End If
        Next

        For i As Integer = 0 To LangValueEnum.Count - 1
            If LangValueEnum(i).CR_Value = My.Settings.CR_Dub Then
                DubSprache = LangValueEnum(i)
                Exit For
            End If
        Next




        SubFolder_Value = My.Settings.SubFolder_Value

        MaxDL = 1 'should be enough

        ' 
        If My.Settings.NameTemplate = "Unused" Then 'convert old stlye
            If My.Settings.CR_NameMethode = 0 Then
                NameBuilder = "AnimeTitle;Season;EpisodeNR;"
            ElseIf My.Settings.CR_NameMethode = 1 Then
                NameBuilder = "AnimeTitle;Season;EpisodeName;"
            ElseIf My.Settings.CR_NameMethode = 2 Then
                NameBuilder = "AnimeTitle;Season;EpisodeNR;EpisodeName;"
            ElseIf My.Settings.CR_NameMethode = 3 Then
                NameBuilder = "AnimeTitle;Season;EpisodeName;EpisodeNR;"
            End If
        Else
            NameBuilder = My.Settings.NameTemplate
        End If

        IncludeLangName = My.Settings.IncludeLangName
        LangNameType = My.Settings.LangNameType
        IgnoreSeason = My.Settings.IgnoreSeason
        SoftSubsString = My.Settings.AddedSubs

        If SoftSubsString = "None" Then
        Else
            Dim SoftSubsStringSplit() As String = SoftSubsString.Split(New String() {","}, System.StringSplitOptions.RemoveEmptyEntries)
            For i As Integer = 0 To SoftSubsStringSplit.Count - 1
                SoftSubs.Add(SoftSubsStringSplit(i))
            Next
        End If


        MergeSubsFormat = My.Settings.MergeSubs


        If MergeSubsFormat = "[merge disabled]" Then
            MergeSubs = False
        Else
            MergeSubs = True
        End If


        VideoFormat = My.Settings.VideoFormat






    End Sub



    Public Sub ListItemAdd(ByVal NameKomplett As String, ByVal NameP1 As String, ByVal NameP2 As String, ByVal Reso As String, ByVal HardSub As String, ByVal ThumbnialURL As String, ByVal URL_DL As String, ByVal Pfad_DL As String, Optional Service As String = "CR") ', ByVal AudioLang As String)

        'With ListView1.Items.Add("0")
        'For i As Integer = 0 To 10
        ItemConstructor(NameKomplett, NameP1, NameP2, Reso, HardSub, ThumbnialURL, URL_DL, Pfad_DL, Service)

        'Next
        'End With
    End Sub

    Public Sub ItemConstructor(ByVal NameKomplett As String, ByVal NameP1 As String, ByVal NameP2 As String, ByVal DisplayReso As String, ByVal HardSub As String, ByVal ThumbnialURL As String, ByVal URL_DL As String, ByVal Pfad_DL As String, ByVal Service As String)
        Dim Item As New CRD_List_Item
        Item.Visible = False


#Region "Set Variables"
        Item.SetLabelWebsite(NameP1)
        Item.SetLabelAnimeTitel(NameP2)
        Item.SetLabelResolution(DisplayReso)
        Item.SetLabelHardsub(HardSub)
        Item.SetThumbnailImage(ThumbnialURL)
        Item.SetLabelPercent("0%")
#End Region




        Dim W As Integer = Panel1.Width
        If Panel1.Controls.Count * 142 > Panel1.Height Then
            W = Panel1.Width - SystemInformation.VerticalScrollBarWidth
        End If



        Item.SetBounds(0, 142 * Panel1.Controls.Count, W - 2, 142)


        Item.Parent = Panel1
        Panel1.Controls.Add(Item)

        Item.Visible = True


        If Pfad_DL.Length > 255 Then
            'MsgBox(Pfad_DL.Length.ToString)
            Pfad_DL = Chr(34) + "\\?\" + Pfad_DL.Replace(Chr(34), "") + Chr(34)
        End If

        'MsgBox(URL_DL + vbNewLine + Pfad_DL + vbNewLine + NameKomplett + vbNewLine + TempHybridMode.ToString)
        Item.StartDownload(URL_DL, Pfad_DL, NameKomplett, False, TempFolder)
    End Sub




#Region "Sub to display"


    Public Function GetSubFileLangName(ByVal HardSub As String) As String

        HardSub = HardSub.Replace(Chr(34), "")

        If LangNameType = 1 Then
            Return CCtoMP4CC(HardSub)
        ElseIf LangNameType = 2 Then
            Dim RS As String = HardSubValuesToDisplay(HardSub) + "." + CCtoMP4CC(HardSub)
            Return RS
        Else
            Return HardSubValuesToDisplay(HardSub)
        End If


    End Function
    Public Function HardSubValuesToDisplay(ByVal HardSub As String) As String

        For i As Integer = 0 To LangValueEnum.Count - 1
            If LangValueEnum(i).CR_Value = HardSub Or LangValueEnum(i).FM_Value = HardSub Then
                Return LangValueEnum(i).Name
                Exit Function
            End If
        Next

        Return "Error"

    End Function


    Public Function CCtoMP4CC(ByVal HardSub As String) As String
        Try
            If HardSub = "de-DE" Then
                Return "ger"
            ElseIf HardSub = "en-US" Or HardSub = "en" Then
                Return "eng"
            ElseIf HardSub = "pt-BR" Or HardSub = "pt" Then
                Return "por"
            ElseIf HardSub = "es" Or HardSub = "es-419" Then
                Return "spa"
            ElseIf HardSub = "fr-FR" Then
                Return "fre"
            ElseIf HardSub = "ar-SA" Then
                Return "ara"
            ElseIf HardSub = "ru-RU" Then
                Return "rus"
            ElseIf HardSub = "it-IT" Then
                Return "ita"
            ElseIf HardSub = "es-ES" Then
                Return "spa"
            ElseIf HardSub = "ja-JP" Then
                Return "jpn"
            Else
                Return "chi"
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
#End Region






    Function Convert_locale(ByVal locale As String) As String
        Try
            If locale = "de" Then
                Return "de-DE"
            ElseIf locale = "" Then
                Return "en-US"
            ElseIf locale = "pt-br" Then
                Return "pt-BR"
            ElseIf locale = "es" Then
                Return "es-419"
            ElseIf locale = "fr" Then
                Return "fr-FR"
            ElseIf locale = "ar" Then
                Return "ar-SA"
            ElseIf locale = "ru" Then
                Return "ru-RU"
            ElseIf locale = "it" Then
                Return "it-IT"
            ElseIf locale = "es-es" Then
                Return "es-ES"
            ElseIf locale = "pt-pt" Then
                Return "pt-PT"
            Else
                Return "en-US"
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function



    Private Sub Btn_Close_Click(sender As Object, e As EventArgs) Handles Btn_Close.Click

        Me.Close()

    End Sub



    Private Sub Btn_add_Click(sender As Object, e As EventArgs) Handles Btn_add.Click

        'If File.Exists("cookies.txt") = False Then
        '    If Application.OpenForms().OfType(Of Browser).Any = True Then
        '    Else
        '        UserBowser = False
        '        Browser.Show()
        '    End If
        'End If

        If Anime_Add.WindowState = System.Windows.Forms.FormWindowState.Minimized Then
            Anime_Add.WindowState = System.Windows.Forms.FormWindowState.Normal
        Else
            Anime_Add.Show()
        End If
    End Sub

    Private Sub Btn_Settings_Click(sender As Object, ByVal e As EventArgs) Handles Btn_Settings.Click
        Einstellungen.Show()
    End Sub

    Private Sub ToggleDebugModeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ToggleDebugModeToolStripMenuItem.Click
        If Debug2 = True Then
            Debug2 = False
            MsgBox("Debug Mode Disabled")
        Else
            Debug2 = True
            MsgBox("Debug Mode Enabled")
        End If
    End Sub

    Private Sub OpenSettingsToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Einstellungen.Show()
    End Sub

    Private Sub Btn_Settings_DoubleClick(sender As Object, e As EventArgs) Handles Btn_Settings.DoubleClick
        Einstellungen.Close()
        If Debug1 = True Then
            If Debug2 = True Then
                Einstellungen.Close()
                Try
                    My.Computer.Clipboard.SetText(WebbrowserText)
                    MsgBox("webbrowser text copyed to the clipboard")
                Catch ex As Exception
                End Try
            Else
                Debug2 = True
                Einstellungen.Close()
                MsgBox("Debug activated")
            End If
        Else
            Debug1 = True
            Einstellungen.Close()
            'MsgBox("Debug activated")
        End If
    End Sub
    Private Sub Btn_Browser_Click(sender As Object, e As EventArgs) Handles Btn_Browser.Click

        Dim BrowserProc As Process

        Dim BrowserArg As String = Chr(34) + "Setup" + Chr(34) + " " + Chr(34) + "https://www.crunchyroll.com/" + Chr(34) + " " + Chr(34) + "False" + Chr(34) '"--profile " + Chr(34) + Profile + Chr(34) + " --collection " + Chr(34) + Profile + Chr(34) + " --startrecording" ' --minimize-to-tray



        Dim Browser_exe As String = Application.StartupPath + "\lib\BrowserWindow.exe" '
        Dim exepath As String = Browser_exe

        Dim BrowserStartinfo As New System.Diagnostics.ProcessStartInfo

        BrowserStartinfo.FileName = exepath
        BrowserStartinfo.Arguments = BrowserArg
        BrowserStartinfo.UseShellExecute = False
        BrowserStartinfo.WindowStyle = ProcessWindowStyle.Normal
        BrowserStartinfo.RedirectStandardError = True
        BrowserStartinfo.RedirectStandardInput = True
        BrowserStartinfo.RedirectStandardOutput = True
        BrowserStartinfo.CreateNoWindow = True
        BrowserProc = New Process
        BrowserProc.EnableRaisingEvents = True
        'AddHandler BrowserProc.OutputDataReceived, AddressOf LogOut
        'AddHandler BrowserProc.ErrorDataReceived, AddressOf LogOut

        BrowserProc.StartInfo = BrowserStartinfo

        BrowserProc.Start()

        BrowserProc.BeginOutputReadLine()
        BrowserProc.BeginErrorReadLine()
        'If Application.OpenForms().OfType(Of Browser).Any = True Then
        '    Browser.Location = Me.Location
        'Else
        '    Browser.Location = Me.Location
        '    Browser.Show()
        'End If


    End Sub

    Public Function RemoveExtraSpaces(input_text As String) As String
        Dim rsRegEx As System.Text.RegularExpressions.Regex
        rsRegEx = New System.Text.RegularExpressions.Regex("\s+")
        Return rsRegEx.Replace(input_text, " ").Trim()
    End Function



    Private Sub Button1_Click(sender As Object, e As EventArgs)
        ErrorDialog.Show()
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs)
        ErrorDialog.ShowDialog()
    End Sub

    Private Sub Btn_min_Click(sender As Object, e As EventArgs) Handles Btn_min.Click
        Me.WindowState = System.Windows.Forms.FormWindowState.Minimized
    End Sub


    Private Sub Main_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Btn_add.Image = My.Resources.main_add
        Panel1.Select()

    End Sub


    Private Sub ItemBoundsToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Try

            For s As Integer = 0 To Panel1.Controls.Count - 1
                MsgBox(Panel1.Controls.Item(s).Bounds.ToString)
            Next
        Catch ex As Exception
        End Try
    End Sub




    Private Sub PanelControlRemoved(sender As Object, e As ControlEventArgs) Handles Panel1.ControlAdded, Panel1.ControlRemoved
        ItemBounds()
    End Sub

    Sub ItemBounds()
        Try
            Panel1.AutoScrollPosition = New Point(0, 0)
            Dim W As Integer = Panel1.Width
            If Panel1.Controls.Count * 142 > Panel1.Height Then
                W = Panel1.Width - SystemInformation.VerticalScrollBarWidth
            End If

            Dim Item As New List(Of CRD_List_Item)
            Item.AddRange(Panel1.Controls.OfType(Of CRD_List_Item))
            Item.Reverse()

            For s As Integer = 0 To Item.Count - 1
                Item(s).SetBounds(0, 142 * s, W - 2, 142)
                If Debug2 = True Then
                    Debug.WriteLine("Ist: " + Item(s).Location.Y.ToString)
                    Debug.WriteLine("Soll: " + (142 * s).ToString)
                End If
            Next


        Catch ex As Exception
            Debug.WriteLine(ex.ToString)
        End Try
    End Sub

    Private Sub DummyItemToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DummyItemToolStripMenuItem.Click
        Dim TN As String = "https://invalid.com/"
        Dim cmd As String = "-i " + Chr(34) + "https://invalid.com/" + Chr(34) + " -c copy "
        ListItemAdd("TestDL", "CR", "TestDL", "9987p", "DE", "None", TN, cmd, "E:\Test\RWBY\Testdl.mkv")


    End Sub



#Region "enum"

    Sub FillArray() '

        LangValueEnum.Add(New NameValuePair("[ null ]", "", Nothing))
        LangValueEnum.Add(New NameValuePair("Deutsch", "de-DE", Nothing)) '
        LangValueEnum.Add(New NameValuePair("English", "en-US", "en"))
        LangValueEnum.Add(New NameValuePair("Português (Brasil)", "pt-BR", "pt"))
        LangValueEnum.Add(New NameValuePair("Português (Portugal)", "pt-PT", Nothing))
        LangValueEnum.Add(New NameValuePair("Español (LA)", "es-419", "es"))
        LangValueEnum.Add(New NameValuePair("Français (France)", "fr-FR", Nothing))
        LangValueEnum.Add(New NameValuePair("العربية (Arabic)", "ar-SA", Nothing))
        LangValueEnum.Add(New NameValuePair("Русский (Russian)", "ru-RU", Nothing))
        LangValueEnum.Add(New NameValuePair("Italiano (Italian)", "it-IT", Nothing))
        LangValueEnum.Add(New NameValuePair("Español (España)", "es-ES", Nothing))
        LangValueEnum.Add(New NameValuePair("Japanese", "ja-JP", Nothing))

    End Sub

    Private Sub QueueToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles QueueToolStripMenuItem.Click
        'ffmpeg_options.ShowDialog()
        Dim newCmd As New ffmpeg_options
        newCmd.command = ffmpeg_command
        'MsgBox(newCmd.ShowDialog.ToString)
        If newCmd.ShowDialog = DialogResult.OK Then
            ffmpeg_command = newCmd.command
            My.Settings.ffmpeg_command_override = newCmd.command
        End If
    End Sub

    Private Sub Btn_Queue_Click(sender As Object, e As EventArgs) Handles Btn_Queue.Click


        If Queue.WindowState = System.Windows.Forms.FormWindowState.Minimized Then
            Queue.WindowState = System.Windows.Forms.FormWindowState.Normal
        Else
            Queue.Show()
        End If

    End Sub

#End Region
    Sub SetStatusLabel(ByVal txt As String)
        If Application.OpenForms().OfType(Of Anime_Add).Any = True Then
            Anime_Add.StatusLabel.Text = txt

        End If

    End Sub

    Private Sub TestTestToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TestTestToolStripMenuItem.Click

    End Sub
End Class


