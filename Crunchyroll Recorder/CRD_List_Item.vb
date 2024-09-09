Option Strict On

Imports System.Net
Imports System.Text
Imports System.IO
Imports System.Threading
Imports Microsoft.Win32
Imports System.ComponentModel
Imports MetroFramework
Imports MetroFramework.Components
Imports MetroFramework.Forms
Imports System.Security.Policy
Imports WindowsDesktop
Imports Microsoft.VisualBasic.FileIO
Imports System.Net.WebRequestMethods
Imports System.Runtime.Remoting.Contexts

Public Class CRD_List_Item
    Inherits Controls.MetroUserControl

    Dim LogText As New List(Of String)

    Dim ZeitGesamtInteger As Integer = 0
    Dim ListOfStreams As New List(Of String)
    Dim Finished As Boolean = False


    Dim Profile As String = TempProfile()
    Dim ThumbnailSource As String = ""
    Dim VideoTime As String = ""
    Dim VideoDuration As String = ""
    Dim RoustPreciseDuration As Double = 0
    Dim OBS_Proc As Process
    Dim BrowserProc As Process
    Dim TotalTime As TimeSpan
    Dim VideoPath As String = SpecialDirectories.MyDocuments
    Dim VideoFilePath As String = VideoPath + "\" + TempProfile() + ".mkv"
    Dim VideoFile As String = ""
    Dim RecPath As String = ""
    Dim FileTime As Double = 0
    Dim v As VirtualDesktop = VirtualDesktop.Create()



    Public Function TempProfile() As String
        Dim rnd As New Random
        Dim possible As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"
        Dim HWID As String = Nothing

        For i As Integer = 0 To 15
            Dim ZufallsZahl As Integer = rnd.Next(1, 33)
            HWID = HWID + possible(ZufallsZahl)
        Next
        Return HWID
    End Function

#Region "UI"

    Private Sub CRD_List_Item_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        bt_del.SetBounds(775, 10, 35, 29)
        bt_pause.SetBounds(740, 15, 25, 20)
        PB_Thumbnail.SetBounds(11, 20, 168, 95)
        PB_Thumbnail.BringToFront()
        Label_website.Location = New Point(195, 12)
        Label_Anime.Location = New Point(195, 40)
        Label_Reso.Location = New Point(195, 97)
        Label_Hardsub.Location = New Point(255, 97)
        Label_percent.SetBounds(Me.Width - 400, 97, 378, 27)
        Label_percent.AutoSize = False
        ProgressBar1.SetBounds(195, 70, 601, 20)
        PictureBox5.Location = New Point(0, 136)
        PictureBox5.Height = 6

        'If Service = "FM" Then
        '    MetroStyleManager1.Style = MetroColorStyle.DarkPurple
        'Else
        '    MetroStyleManager1.Style = MetroColorStyle.Orange
        'End If
        MetroStyleManager1.Style = MetroColorStyle.Orange
        MetroStyleManager1.Theme = Main.Manager.Theme
        MetroStyleManager1.Owner = Me
        Me.StyleManager = MetroStyleManager1


        PictureBox5.Width = Me.Width - 40

        bt_del.Location = New Point(Me.Width - 63, 10)
        bt_pause.Location = New Point(Me.Width - 98, 15)

        ProgressBar1.Width = Me.Width - 223

    End Sub

    Public Function GetIsStatusFinished() As Boolean
        Return False
    End Function
    Public Sub SetLabelWebsite(ByVal Text As String)
        Label_website.Text = Text

    End Sub

    Public Sub SetTheme(ByVal Theme As MetroThemeStyle)
        MetroStyleManager1.Theme = Theme
    End Sub

    Public Sub SetLabelAnimeTitel(ByVal Text As String)
        Label_Anime.Text = Text

    End Sub
    Public Sub SetLabelResolution(ByVal Text As String)
        Label_Reso.Text = Text
    End Sub
    Public Sub SetLabelHardsub(ByVal Text As String)
        Label_Hardsub.Text = Text
    End Sub
    Public Sub SetLabelPercent(ByVal Text As String)
        Label_percent.Text = Text
    End Sub
    Public Sub SetThumbnailImage(ByVal ThumbnialURL As String)
        ThumbnailSource = ThumbnialURL
        Dim Thumbnail As Image = My.Resources.main_del
        Debug.WriteLine("ThumbnialURL: " + ThumbnialURL)
        Try
            Dim wc As New WebClient()
            Dim bytes As Byte() = wc.DownloadData(ThumbnialURL)
            Dim ms As New MemoryStream(bytes)
            Thumbnail = System.Drawing.Image.FromStream(ms)


        Catch ex As Exception
            Debug.WriteLine(ex.ToString)
        End Try
        If InvokeRequired = True Then
            Me.Invoke(New Action(Function() As Object
                                     PB_Thumbnail.BackgroundImage = Thumbnail
                                     Return Nothing
                                 End Function))
        Else
            PB_Thumbnail.BackgroundImage = Thumbnail
        End If


    End Sub
#End Region
#Region "Get Variables"

    Public Function GetThumbnailSource() As String
        Try
            Return ThumbnailSource
        Catch ex As Exception
            Return "0"
        End Try

    End Function
    Public Function GetLabelPercent() As String
        Try
            Return Label_percent.Text
        Catch ex As Exception
            Return "0"
        End Try

    End Function
    Public Function GetPercentValue() As Integer
        Try
            Return ProgressBar1.Value
        Catch ex As Exception

            Return 0
        End Try

    End Function
    Public Function GetNameAnime() As String
        Try
            Return Label_Anime.Text
        Catch ex As Exception
            Return "error"
        End Try

    End Function
#End Region



    Private Sub BT_del_MouseEnter(sender As Object, e As EventArgs) Handles bt_del.MouseEnter

        bt_del.BackgroundImage = My.Resources.main_del
    End Sub

    Private Sub BT_del_MouseLeave(sender As Object, e As EventArgs) Handles bt_del.MouseLeave

        bt_del.BackgroundImage = My.Resources.main_del
    End Sub


    Private Sub BT_pause_Click(sender As Object, e As EventArgs) Handles bt_pause.Click


    End Sub
    Private Sub Item_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.ContextMenuStrip = ContextMenuStrip1 '.ContextMenu

        'TN_DL.Enabled = True

    End Sub

    Public Function GetTextBound() As Rectangle
        'Return Label_website.Location.Y
        'Return bt_del.Size.Height
        Return Me.Bounds()
    End Function


#Region "Download + Update UI"

    Public Sub StartDownload(ByVal selectedDeviceKey As String, ByVal DL_Pfad As String, ByVal Filename As String, ByVal DownloadHybridMode As Boolean, ByVal TempFolder As String)
        'MsgBox(Filename)
        Me.StyleManager = MetroStyleManager1

        RecPath = TempFolder
        VideoPath = DL_Pfad
        'Directory.SetCurrentDirectory("C:\Projecte\WinForm_cli\WinForm_cli\bin\Debug")

        Dim BrowserArg As String = Chr(34) + Label_website.Text + Chr(34) + " " + Chr(34) + Filename + Chr(34) + " " + Chr(34) + "True" + Chr(34) 'we don't have any softsubs so CR Default subs it is!. "--profile " + Chr(34) + Profile + Chr(34) + " --collection " + Chr(34) + Profile + Chr(34) + " --startrecording" ' --minimize-to-tray



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
        AddHandler BrowserProc.OutputDataReceived, AddressOf LogOut
        AddHandler BrowserProc.ErrorDataReceived, AddressOf LogOut

        BrowserProc.StartInfo = BrowserStartinfo

        BrowserProc.Start()

        BrowserProc.BeginOutputReadLine()
        BrowserProc.BeginErrorReadLine()

        'OBS_Proc

        'Exit Sub


        Dim appdataPath As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
        Dim scenefolderPath As String = IO.Path.Combine(appdataPath, "obs-studio\basic\scenes")
        Dim profilefolderPath As String = IO.Path.Combine(appdataPath, "obs-studio\basic\profiles", Profile)




        If Not System.IO.Directory.Exists(profilefolderPath) Then
            System.IO.Directory.CreateDirectory(profilefolderPath)
        End If

        'Recoder json
        Dim recordEncoderPath As String = Application.StartupPath & "\OBS-Presets\Default\recordEncoder.json"
        Dim recordEncoder() As String = System.IO.File.ReadAllLines(recordEncoderPath)
        System.IO.File.WriteAllLines(Path.Combine(profilefolderPath, "recordEncoder.json"), recordEncoder)

        'basic.ini
        Dim basic_iniPath As String = Application.StartupPath & "\OBS-Presets\Default\basic.ini"
        Dim basic_ini As String = System.IO.File.ReadAllText(basic_iniPath) ' todo edit it
        basic_ini = basic_ini.Replace("[ProName]", Profile)
        basic_ini = basic_ini.Replace("[RecLoc]", TempFolder.Replace("\", "\\")) 'obs uses two \ for whatever reason
        System.IO.File.WriteAllText(Path.Combine(profilefolderPath, "basic.ini"), basic_ini)

        'Default.json
        Dim scenesPath As String = Application.StartupPath & "\OBS-Presets\Default.json"
        Dim scenes_json As String = System.IO.File.ReadAllText(scenesPath) ' todo edit it
        scenes_json = scenes_json.Replace("[RecTitle]", Profile)
        scenes_json = scenes_json.Replace("[RecDev]", selectedDeviceKey)
        System.IO.File.WriteAllText(Path.Combine(scenefolderPath, Profile + ".json"), scenes_json)





        ' Pause(10)
        'System.Threading.Thread.Sleep(5000)

        'Dim v As VirtualDesktop = VirtualDesktop.Create()

        'Dim hWnd As IntPtr = FindWindow(vbNullString, BrowserProc.MainWindowTitle.ToString) 'FindWindow(vbNullString, driver.Title + " - Google Chrome")
        'VirtualDesktopHelper.MoveToDesktop(hWnd, v)


        'Dim hWndOBS As IntPtr = FindWindow(vbNullString, OBS_Proc.MainWindowTitle.ToString) 'FindWindow(vbNullString, driver.Title + " - Google Chrome")
        'VirtualDesktopHelper.MoveToDesktop(hWndOBS, v)

        'Pause(10)
        'Timer2.Enabled = True
        'GetVideoLength()
        Exit Sub



    End Sub



    Private Sub BT_del_Click(sender As Object, e As EventArgs) Handles bt_del.Click

    End Sub



    Sub LogOut(ByVal sender As Object, ByVal e As DataReceivedEventArgs)
        Try
            'MsgBox(e.Data.ToString)
            If e.Data.ToString = "Alles klar, Kinder?" Then
                Directory.SetCurrentDirectory("C:\Program Files\obs-studio\bin\64bit\")

                Dim arg As String = "--profile " + Chr(34) + Profile + Chr(34) + " --collection " + Chr(34) + Profile + Chr(34) + " --startrecording --disable-updater --disable-shutdown-check" ' --minimize-to-tray
                Dim OBS_exe As String = "obs64.exe"

                Dim startinfo As New System.Diagnostics.ProcessStartInfo

                startinfo.FileName = OBS_exe
                startinfo.Arguments = arg
                startinfo.UseShellExecute = False
                startinfo.WindowStyle = ProcessWindowStyle.Normal
                startinfo.RedirectStandardError = True
                startinfo.RedirectStandardInput = True
                startinfo.RedirectStandardOutput = True
                startinfo.CreateNoWindow = True
                OBS_Proc = New Process
                OBS_Proc.StartInfo = startinfo

                OBS_Proc.Start()

            ElseIf e.Data.ToString = "Dann schwingt euch an Deck und kommt ja nicht zu spät!" Then
                Pause(2)
                Dim hWnd As IntPtr = FindWindow(vbNullString, BrowserProc.MainWindowTitle.ToString) 'FindWindow(vbNullString, driver.Title + " - Google Chrome")
                VirtualDesktopHelper.MoveToDesktop(hWnd, v)


                Dim hWndOBS As IntPtr = FindWindow(vbNullString, OBS_Proc.MainWindowTitle.ToString) 'FindWindow(vbNullString, driver.Title + " - Google Chrome")
                VirtualDesktopHelper.MoveToDesktop(hWndOBS, v)
                '"
            ElseIf CBool(InStr(e.Data.ToString, "<--")) Then


                Dim JS As String = e.Data.ToString.Replace("<--", "").Replace("-->", "")
                Dim JS_Cleaned() As String = JS.Split(New String() {"."}, System.StringSplitOptions.RemoveEmptyEntries)

                'Console.WriteLine("JS Time: " + JS)
                Dim totalSeconds As Integer = CInt(JS_Cleaned(0))

                If totalSeconds < 1 Then
                    Exit Sub
                End If

                Dim timeSpan As TimeSpan = TimeSpan.FromSeconds(totalSeconds)
                Dim formattedTime As String = timeSpan.ToString("hh\:mm\:ss")
                Console.WriteLine("aktuell Zeit: " & formattedTime)

                Dim percent As Double = (timeSpan.TotalSeconds / TotalTime.TotalSeconds) * 100

                Me.Invoke(New Action(Function() As Object
                                         ProgressBar1.Value = CInt(percent)
                                         Label_percent.Text = formattedTime + "/" + VideoDuration
                                         Return Nothing
                                     End Function))

            ElseIf CBool(InStr(e.Data.ToString, "<-")) Then

                Dim JS As String = e.Data.ToString.Replace("<-", "").Replace("->", "")

                RoustPreciseDuration = Math.Round(CDbl(JS), 3, MidpointRounding.AwayFromZero)


                Dim JS_Cleaned() As String = JS.Split(New String() {"."}, System.StringSplitOptions.RemoveEmptyEntries)

                Dim totalSeconds As Integer = CInt(JS_Cleaned(0))
                Dim timeSpan As TimeSpan = TimeSpan.FromSeconds(totalSeconds)
                Dim formattedTime As String = timeSpan.ToString("hh\:mm\:ss")
                Console.WriteLine("Gesamt Zeit: " & formattedTime)
                TotalTime = timeSpan
                VideoDuration = formattedTime
                Me.Invoke(New Action(Function() As Object
                                         Label_percent.Text = formattedTime
                                         Return Nothing
                                     End Function))


            ElseIf CBool(InStr(e.Data.ToString, ">-Fine-<")) Then
                Dim RecLength As TimeSpan = OBS_Proc.StartTime.Subtract(Date.Now)

                BrowserProc.CancelErrorRead()
                BrowserProc.CancelOutputRead()
                OBS_Proc.Kill()
                BrowserProc.Kill()
                v.Remove()

                Dim TTms As Integer = CInt(RecLength.TotalMilliseconds)
                Dim TTs As Double = Math.Abs(TTms) / 1000
                Debug.WriteLine("TTS: " + TTs.ToString)

                Dim Start As Double = TTs - RoustPreciseDuration
                Dim StartString As String = "00:00:0" + CStr(Math.Round(Start, 0, MidpointRounding.AwayFromZero))
                If Start >= 10 Then
                    StartString = "00:00:" + CStr(Math.Round(Start, 0, MidpointRounding.AwayFromZero))
                End If
                Debug.WriteLine("StartString: " + StartString)
                Pause(5)

                Dim appdataPath As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
                Dim scenefolderPath As String = IO.Path.Combine(appdataPath, "obs-studio\basic\scenes")
                Dim profilefolderPath As String = IO.Path.Combine(appdataPath, "obs-studio\basic\profiles", Profile)

                System.IO.File.Delete(Path.Combine(profilefolderPath, "recordEncoder.json"))
                System.IO.File.Delete(Path.Combine(profilefolderPath, "basic.ini"))
                'System.IO.File.Delete(Path.Combine(scenefolderPath, Profile + ".json"))
                System.IO.File.Delete(scenefolderPath + "\" + Profile + ".json")
                Try
                    'System.IO.File.Delete(Path.Combine(scenefolderPath, Profile + ".json.bak"))
                    System.IO.File.Delete(scenefolderPath + "\" + Profile + ".json.bak") 'obs needs backup for some reasons?
                Catch ex As Exception
                End Try

                Pause(5) ' not too fast
                System.IO.Directory.Delete(profilefolderPath, False) 'cleanup the folder but the files manually just to be safe .

                FFMPEG_Cut(StartString, RecPath + "\" + Profile + ".mkv")




            ElseIf CBool(InStr(e.Data.ToString, "||")) Then
                Dim ObjectFileName As String = e.Data.ToString.Replace("||", "")

                If InvokeRequired = True Then
                    Me.Invoke(New Action(Function() As Object
                                             Label_Anime.Text = ObjectFileName ' e.Data.ToString.Replace("||", "")
                                             VideoFilePath = VideoPath + "\" + ObjectFileName + ".mkv"
                                             VideoFile = ObjectFileName + ".mkv"
                                             Return Nothing
                                         End Function))
                Else
                    VideoFilePath = VideoPath + "\" + ObjectFileName + ".mkv"
                    Label_Anime.Text = ObjectFileName ' e.Data.ToString.Replace("||", "")
                    VideoFile = ObjectFileName + ".mkv"
                End If




                Debug.WriteLine(e.Data.ToString)
                'Label_Anime
            ElseIf CBool(InStr(e.Data.ToString, "Thumb:")) Then
                Dim JS As String = e.Data.ToString.Replace("Thumb:", "")
                SetThumbnailImage(JS)
            ElseIf CBool(InStr(e.Data.ToString, "[DEB: ]")) Then
                Debug.WriteLine(e.Data.ToString)
            End If


        Catch ex As Exception
            Debug.WriteLine(ex.ToString)
        End Try
    End Sub




#End Region


#Region "ffmpeg"
    ', ByVal TotalTime As String
    Public Sub FFMPEG_Cut(ByVal SeekTime As String, ByVal OBS_Out As String)
        Debug.WriteLine("Cut: " + SeekTime + " File: " + OBS_Out)
        Dim proc As New Process
        Dim exepath As String = Application.StartupPath + "\lib\ffmpeg.exe"
        Dim startinfo As New System.Diagnostics.ProcessStartInfo
        'Dim cmd As String = "-ss " + SeekTime + " -to " + TotalTime + " -i " + Chr(34) + OBS_Out + Chr(34) + " -c copy" + VideoFilePath 'start ffmpeg with command strFFCMD string
        Dim cmd As String = "-ss " + SeekTime + " -i " + Chr(34) + OBS_Out + Chr(34) + " -c copy " + Chr(34) + VideoFilePath + Chr(34)  'start ffmpeg with command strFFCMD string
        LogText.Add(Date.Now.ToString + " " + cmd)
        'all parameters required to run the process
        startinfo.FileName = exepath
        startinfo.Arguments = cmd
        startinfo.UseShellExecute = False
        startinfo.WindowStyle = ProcessWindowStyle.Normal
        startinfo.RedirectStandardError = True
        startinfo.RedirectStandardOutput = True
        startinfo.CreateNoWindow = True
        proc.EnableRaisingEvents = True
        AddHandler proc.ErrorDataReceived, AddressOf FFMPEGOutput
        AddHandler proc.OutputDataReceived, AddressOf FFMPEGOutput
        proc.StartInfo = startinfo
        proc.Start() ' start the process
        proc.BeginOutputReadLine()
        proc.BeginErrorReadLine()

    End Sub

    Sub FFMPEGOutput(ByVal sender As Object, ByVal e As DataReceivedEventArgs)
        Try
            LogText.Add(Date.Now.ToString + " " + e.Data)
            'My.Computer.FileSystem.WriteAllText(GlobalLogfile, Date.Now.ToString + " " + e.Data, True)
            'Dim Log As String = ""
            'Dim logfile As String = DownloadPfad.Replace(Main.VideoFormat, ".log").Replace(Chr(34), "")

            'For i As Integer = 1 To LogText.Count - 1
            '    Log = Log + vbNewLine
            '    Log = Log + LogText.Item(i)
            'Next
            'WriteText(logfile, Log)
        Catch ex As Exception
            Debug.WriteLine("FFMPEGOutput: " + ex.ToString)
        End Try

        'Dim TH As String = "FFMPEGOutput_ID: " + Thread.CurrentThread.Name
        'Debug.WriteLine(TH)

        'My.Computer.FileSystem.WriteAllText(GlobalLogfile, TH, True)
        'My.Computer.FileSystem.WriteAllText(GlobalLogfile, vbNewLine, True)
        'Thread.CurrentThread.Name = "FFMPEGOutput"

#Region "Detect Auto resolution"
        Try


            If CBool(InStr(e.Data, "Stream #")) And CBool(InStr(e.Data, "Video")) = True Then
                    'MsgBox(True.ToString + vbNewLine + e.Data)
                    'MsgBox(InStr(e.Data, "Stream #").ToString + vbNewLine + CBool(InStr(e.Data, "Video").ToString)

                    'MsgBox("with CBool" + vbNewLine + CBool(InStr(e.Data, "Stream #")).ToString + vbNewLine + CBool(InStr(e.Data, "Video")).ToString)

                    ListOfStreams.Add(e.Data)
                End If
                If CBool(InStr(e.Data, "Stream #")) And CBool(InStr(e.Data, " -> ")) Then
                    'UsesStreams.Add(e.Data)
                    'MsgBox(e.Data)
                    Dim StreamSearch() As String = e.Data.Split(New String() {" -> "}, System.StringSplitOptions.RemoveEmptyEntries)
                    Dim StreamSearch2 As String = StreamSearch(0) + ":"
                    For i As Integer = 0 To ListOfStreams.Count - 1
                        If CBool(InStr(ListOfStreams(i), StreamSearch2)) Then 'And CBool(InStr(ListOfStreams(i), " Video:")) Then
                            'MsgBox(ListOfStreams(i))
                            Dim ResoSearch() As String = ListOfStreams(i).Split(New String() {"x"}, System.StringSplitOptions.RemoveEmptyEntries)
                            'MsgBox(ResoSearch(1))
                            If CBool(InStr(ResoSearch(2), " [")) = True Then
                                Dim ResoSearch2() As String = ResoSearch(2).Split(New String() {" ["}, System.StringSplitOptions.RemoveEmptyEntries)
                                Me.Invoke(New Action(Function() As Object
                                                         If Label_Reso.Text = "1080p+" Then
                                                         Else
                                                             Label_Reso.Text = ResoSearch2(0) + "p"
                                                         End If

                                                         Return Nothing
                                                     End Function))
                            End If
                        End If
                    Next
                End If

#End Region

                If CBool(InStr(e.Data, "Duration: N/A, bitrate: N/A")) Then

            ElseIf Finished = True Then

            ElseIf CBool(InStr(e.Data, "Duration: ")) Then
                Dim ZeitGesamt As String() = e.Data.Split(New String() {"Duration: "}, System.StringSplitOptions.RemoveEmptyEntries)
                Dim ZeitGesamt2 As String() = ZeitGesamt(1).Split(New [Char]() {System.Convert.ToChar(".")})
                Dim ZeitGesamtSplit() As String = ZeitGesamt2(0).Split(New [Char]() {System.Convert.ToChar(":")})
                'MsgBox(ZeitGesamt2(0))
                ZeitGesamtInteger = CInt(ZeitGesamtSplit(0)) * 3600 + CInt(ZeitGesamtSplit(1)) * 60 + CInt(ZeitGesamtSplit(2))



            ElseIf CBool(InStr(e.Data, " time=")) Then
                'MsgBox(e.Data)
                Dim ZeitFertig As String() = e.Data.Split(New String() {" time="}, System.StringSplitOptions.RemoveEmptyEntries)
                Dim ZeitFertig2 As String() = ZeitFertig(1).Split(New [Char]() {System.Convert.ToChar(".")})
                Dim ZeitFertigSplit() As String = ZeitFertig2(0).Split(New [Char]() {System.Convert.ToChar(":")})
                Dim ZeitFertigInteger As Integer = CInt(ZeitFertigSplit(0)) * 3600 + CInt(ZeitFertigSplit(1)) * 60 + CInt(ZeitFertigSplit(2))
                Dim bitrate3 As String = "0"
                If CBool(InStr(e.Data, "bitrate=")) Then
                    Dim bitrate As String() = e.Data.Split(New String() {"bitrate="}, System.StringSplitOptions.RemoveEmptyEntries)
                    Dim bitrate2 As String() = bitrate(1).Split(New String() {"kbits/s"}, System.StringSplitOptions.RemoveEmptyEntries)

                    If CBool(InStr(bitrate2(0), ".")) Then
                        Dim bitrateTemo As String() = bitrate2(0).Split(New String() {"."}, System.StringSplitOptions.RemoveEmptyEntries)
                        bitrate3 = bitrateTemo(0)
                    ElseIf CBool(InStr(bitrate2(0), ",")) Then
                        Dim bitrateTemo As String() = bitrate2(0).Split(New String() {","}, System.StringSplitOptions.RemoveEmptyEntries)
                        bitrate3 = bitrateTemo(0)
                    End If
                End If
                Dim bitrateInt As Double = CInt(bitrate3) / 1024
                Dim FileSize As Double = ZeitGesamtInteger * bitrateInt / 8
                Dim DownloadFinished As Double = ZeitFertigInteger * bitrateInt / 8
                Dim percent As Integer = CInt(ZeitFertigInteger / ZeitGesamtInteger * 100)
                Me.Invoke(New Action(Function() As Object
                                         If percent > 100 Then
                                             percent = 100
                                         End If
                                         ProgressBar1.Value = percent
                                         Label_percent.Text = Math.Round(DownloadFinished, 2, MidpointRounding.AwayFromZero).ToString + "MB/" + Math.Round(FileSize, 2, MidpointRounding.AwayFromZero).ToString + "MB " + percent.ToString + "%"
                                         Return Nothing
                                     End Function))

            ElseIf CBool(InStr(e.Data, "muxing overhead:")) Then
                Finished = True
                Me.Invoke(New Action(Function() As Object

                                         ProgressBar1.Value = ProgressBar1.Maximum
                                         Dim Done As String() = Label_percent.Text.Split(New String() {"MB"}, System.StringSplitOptions.RemoveEmptyEntries)
                                         Label_percent.Text = "Finished - " + Done(0) + "MB"
                                         Pause(5)
                                         'System.IO.File.Delete(RecPath + "\" + Profile + ".mkv") ' keeping the file for testing
                                         Microsoft.VisualBasic.FileSystem.Rename(RecPath + "\" + Profile + ".mkv", VideoFilePath.Replace(".mkv", ".rec.mkv"))
                                         Return Nothing
                                     End Function))

            End If

        Catch ex As Exception
            Debug.WriteLine(ex.ToString)
        End Try

    End Sub

#Region "not in use"

    Function ConvertFrom_ffmpegTime(ByVal HH_MM_SS_MS As String) As Double

        Dim TimeSplit() As String = HH_MM_SS_MS.Split(New String() {":"}, System.StringSplitOptions.RemoveEmptyEntries)
        Dim TimeSplit2() As String = TimeSplit(2).Split(New String() {"."}, System.StringSplitOptions.RemoveEmptyEntries)

        Dim H_To_S As Integer = CInt(TimeSplit(0)) * 3600
        Dim M_To_S As Integer = CInt(TimeSplit(1)) * 60
        Dim S_To_IntS As Integer = CInt(TimeSplit2(0))
        Dim MS_To_DoubleS As Double = CDbl("0." + TimeSplit2(1))

        Dim TotalTimeInSeconds As Double = 0

        Try
            TotalTimeInSeconds = H_To_S + M_To_S + S_To_IntS + MS_To_DoubleS
        Catch ex As Exception

        End Try

        Return TotalTimeInSeconds
    End Function
    Sub FFMPEG_Duration_Process(ByVal sender As Object, ByVal e As DataReceivedEventArgs)
        If CBool(InStr(e.Data, "Duration: ")) Then
            Dim DurLine() As String = e.Data.Split(New String() {"Duration: "}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim DurLin2() As String = DurLine(0).Split(New String() {", start:"}, System.StringSplitOptions.RemoveEmptyEntries)
            FileTime = ConvertFrom_ffmpegTime(DurLin2(0))
        End If
    End Sub


    Public Sub FFMPEG_Duration(ByVal OBS_Out As String)

        Dim proc As New Process
        Dim exepath As String = Application.StartupPath + "\lib\ffmpeg.exe"
        Dim startinfo As New System.Diagnostics.ProcessStartInfo
        Dim cmd As String = "-i " + Chr(34) + OBS_Out + Chr(34) 'start ffmpeg with command strFFCMD string
        Dim ffmpegOutput As String = Nothing
        Dim ffmpegOutput2 As String = Nothing
        'all parameters required to run the process
        startinfo.FileName = exepath
        startinfo.Arguments = cmd
        startinfo.UseShellExecute = False
        startinfo.WindowStyle = ProcessWindowStyle.Hidden
        startinfo.RedirectStandardError = True
        startinfo.RedirectStandardOutput = True
        startinfo.CreateNoWindow = True
        AddHandler proc.ErrorDataReceived, AddressOf FFMPEG_Duration_Process
        AddHandler proc.OutputDataReceived, AddressOf FFMPEG_Duration_Process
        proc.StartInfo = startinfo
        proc.Start() ' start the process
        proc.BeginOutputReadLine()
        proc.BeginErrorReadLine()

    End Sub



#End Region

#End Region




    Private Sub SaveToFile_Click(sender As Object, e As EventArgs) Handles SaveToFile.Click
        Try

            Dim logfile As String = VideoFilePath.Replace(".mkv", ".log").Replace(Chr(34), "")

            Using sw As StreamWriter = IO.File.AppendText(logfile)
                sw.Write(LogText.Item(0))
                sw.Write(vbNewLine)
                For i As Integer = 1 To LogText.Count - 1
                    sw.Write(vbNewLine)
                    sw.Write(LogText.Item(i))
                Next

            End Using

        Catch ex As Exception
            MsgBox(ex.ToString)
            'Error_msg.ShowErrorDia(ex.ToString, "Unable to write logfile", "None")

        End Try
    End Sub

    Private Sub LogTocClipboard_Click(sender As Object, e As EventArgs) Handles LogTocClipboard.Click
        Try
            Dim Text As String = LogText.Item(0) + vbNewLine
            For i As Integer = 1 To LogText.Count - 1
                Text = Text + vbNewLine + LogText.Item(i)
            Next
            My.Computer.Clipboard.SetText(Text)
        Catch ex As Exception
        End Try

    End Sub

    Private Sub ViewInExplorerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewInExplorerToolStripMenuItem.Click
        Process.Start(Path.GetDirectoryName(VideoFilePath.Replace(Chr(34), "")))
    End Sub
End Class


