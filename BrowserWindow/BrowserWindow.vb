Option Strict On

Imports CoreAudio
Imports Microsoft.Web.WebView2.Core
Imports Newtonsoft.Json.Linq
Imports System.Globalization
Imports System.IO
Imports System.Runtime.InteropServices.ComTypes
Imports System.ServiceModel.Security
Imports System.Text

Public Class SubBrowserWindow

    Public Sub Pause(ByVal pau As Single)

        'Programmausführung verzögern *******************************************************

        Dim start, finish As Single
        start = CSng(Microsoft.VisualBasic.DateAndTime.Timer)

        finish = start + pau
        Do While Microsoft.VisualBasic.DateAndTime.Timer < finish
            Application.DoEvents()
        Loop

    End Sub
    Public Sub Pause_ms(ByVal TTK As Integer)

        'Programmausführung verzögern *******************************************************

        Dim Start As Integer = Date.Now.Second * 1000 + Date.Now.Millisecond
        Dim Finish As Integer = Start + TTK


        Do While Date.Now.Second * 1000 + Date.Now.Millisecond < Finish
            Application.DoEvents()
        Loop

    End Sub

    Public cl_a As String() = Nothing
    Public Startseite As String = "https://www.crunchyroll.com/"
    Public HardSub As Boolean = False

    Public CR_JS_mod As New List(Of ServerResponseCache)
    Public CR_JS_modShort As New List(Of String)
    Public GUID As String = "DemoString"
    Public WebsiteUrl As String = "DemoString"
    Dim CR_FilenName As String = Nothing


    Dim VideoTime As String = ""
    Dim VideoDuration As String = ""

    Dim MicPer As Boolean = False

    Private Sub BrowserWindow_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'using "." instead of "," for everyone
        Dim cultureInfo As New CultureInfo("en-US")
        CultureInfo.DefaultThreadCurrentCulture = cultureInfo
        CultureInfo.DefaultThreadCurrentUICulture = cultureInfo

        cl_a = Environment.GetCommandLineArgs()
        Try
            Me.Text = cl_a(1)
            Startseite = cl_a(2)
            HardSub = CBool(cl_a(3))
        Catch ex As Exception
            Me.Close()
        End Try


        If Me.Text = "Setup" Then
            Startup()
        Else
            Me.FormBorderStyle = FormBorderStyle.None
            Me.Width = 1941
            Me.Height = 1080
            Me.Location = New Point(0, 0)
            Startup()
        End If

    End Sub

    Dim ReloadCount As Integer = 3
    Dim GotIt As Boolean = False
    Dim Done As Boolean = False
    Async Sub Startup()
        Dim WVOptions As New CoreWebView2EnvironmentOptions()

        WVOptions.AdditionalBrowserArguments = "--disable-web-security" + " " + "--disable-cache" + " " + "--disk-cache-size=1" + " " + "--disable-gpu" '+ '" " + "--use-fake-device-for-media-stream" "--disable-web-security" + " " + 
        Dim Env As CoreWebView2Environment = Await CoreWebView2Environment.CreateAsync(Nothing, Nothing, WVOptions)
        Await WebView2.EnsureCoreWebView2Async(Env)

    End Sub

    Private Sub WebView2_CoreWebView2InitializationCompleted(sender As Object, e As CoreWebView2InitializationCompletedEventArgs) Handles WebView2.CoreWebView2InitializationCompleted
        WebView2.CoreWebView2.AddWebResourceRequestedFilter("*.crunchyroll.com/*", CoreWebView2WebResourceContext.All)
        WebView2.CoreWebView2.AddWebResourceRequestedFilter("*.cr-play-service.prd.crunchyrollsvc.com/*", CoreWebView2WebResourceContext.All)
        WebView2.CoreWebView2.AddWebResourceRequestedFilter("*.vrv.co/*", CoreWebView2WebResourceContext.All) ' filter subs to disable player subs
        'WebView2.CoreWebView2.AddWebResourceRequestedFilter("https://www.youtube.com/", CoreWebView2WebResourceContext.All) ' filter subs to disable player subs

        '
        'WebView2.CoreWebView2.AddWebResourceRequestedFilter("https://static.crunchyroll.com/*", CoreWebView2WebResourceContext.All)

        AddHandler WebView2.CoreWebView2.WebResourceResponseReceived, AddressOf ObserveResponse
        AddHandler WebView2.CoreWebView2.WebResourceRequested, AddressOf ObserveHttp


        WebView2.CoreWebView2.CallDevToolsProtocolMethodAsync("Network.clearBrowserCache", "{}")

        If WebView2.CoreWebView2.Source = "about:blank" Or WebView2.CoreWebView2.Source = Nothing Then
            WebView2.CoreWebView2.Navigate(Startseite)
        End If


    End Sub


    Public Function StringToStream(input As String, enc As Encoding) As Stream
        Dim memoryStream = New MemoryStream()
        Dim streamWriter = New StreamWriter(memoryStream, enc)
        streamWriter.Write(input)
        streamWriter.Flush()
        memoryStream.Position = 0
        Return memoryStream
    End Function




    'Private Sub ObserveResponse(ByVal sender As Object, ByVal e As CoreWebView2WebResourceResponseReceivedEventArgs)
    '    If CBool(InStr(e.Request.Uri, "player.html")) Then
    '        Pause(5)
    '        ProcessCR()
    '        Console.Write("Processing...")
    '    End If
    'End Sub
    '

    Private Async Sub ObserveResponse(ByVal sender As Object, ByVal e As CoreWebView2WebResourceResponseReceivedEventArgs)
        Try


            If CBool(InStr(e.Request.Uri, "/manifest/")) And GotIt = True And Done = False Then
                Pause(5)
                ProcessCR()
                Console.WriteLine("Processing...")

            End If

            If CBool(InStr(e.Request.Uri, ".js")) And GotIt = False Then
                'Console.WriteLine(e.Request.Uri)
                ' Console.WriteLine(e.Response.StatusCode)
                Dim Content As Stream = Await e.Response.GetContentAsync
                Dim ContentString As String = Nothing
                Dim reader As New StreamReader(Content)
                ContentString = reader.ReadToEnd
                If CBool(InStr(ContentString, "autoplay; fullscreen; encrypted-media *")) = True And CBool(InStr(ContentString, "camera *;microphone *;")) = False Then
                    Console.WriteLine(e.Request.Uri)
                    ContentString = ContentString.Replace("autoplay; fullscreen; encrypted-media *", "camera *; microphone *; autoplay; fullscreen; encrypted-media *")
                    CR_JS_mod.Add(New ServerResponseCache(ContentString, e.Request.Uri))
                    CR_JS_modShort.Add(e.Request.Uri)
                    GotIt = True
                    Console.WriteLine("got it!! " + e.Request.Uri)
                End If


            End If

            'If CBool(InStr(e.Request.Uri, GUID)) Then
            '    Console.WriteLine("[DEB: ]" + e.Request.Uri)
            'End If

            If CBool(InStr(e.Request.Uri, "/objects/")) And CR_FilenName = Nothing And CBool(InStr(e.Request.Uri, GUID)) Then
                Console.WriteLine("[DEB: ]" + "anime json")

                Dim Content As Stream = Await e.Response.GetContentAsync
                Dim ContentString As String = Nothing
                Dim reader As New StreamReader(Content)
                ContentString = reader.ReadToEnd
                ProcessObjectJson(ContentString)
            ElseIf CBool(InStr(e.Request.Uri, "v2/music/music_videos")) And CR_FilenName = Nothing And CBool(InStr(e.Request.Uri, GUID)) Then
                Console.WriteLine("[DEB: ]" + "Music Video json")

                Dim Content As Stream = Await e.Response.GetContentAsync
                Dim ContentString As String = Nothing
                Dim reader As New StreamReader(Content)
                ContentString = reader.ReadToEnd
                ProcessObjectJson(ContentString)
            ElseIf CBool(InStr(e.Request.Uri, "v2/music/concerts")) And CR_FilenName = Nothing And CBool(InStr(e.Request.Uri, GUID)) Then
                Console.WriteLine("[DEB: ]" + "concert json")

                Dim Content As Stream = Await e.Response.GetContentAsync
                Dim ContentString As String = Nothing
                Dim reader As New StreamReader(Content)
                ContentString = reader.ReadToEnd
                ProcessObjectJson(ContentString)
            End If
        Catch ex As Exception
            'MsgBox(ex.ToString)
        End Try
    End Sub


    Private Sub ObserveHttp(ByVal sender As Object, ByVal e As CoreWebView2WebResourceRequestedEventArgs) 'Handles RequestResource.GetUrl

        If CBool(InStr(e.Request.Uri, "crunchyroll.com")) And Me.Text = "Setup" And MicPer = False Then
            e.Response = WebView2.CoreWebView2.Environment.CreateWebResourceResponse(StringToStream(File.ReadAllText(Application.StartupPath + "\settings.html"), Encoding.UTF8), 200, "Setup Page", "content-type: text/html")

            Exit Sub
        End If



        If CBool(InStr(e.Request.Uri, "playheads?")) Then ' Neustart 
            e.Response = WebView2.CoreWebView2.Environment.CreateWebResourceResponse(StringToStream("", Encoding.UTF8), 204, "Basst scho", "content-type: application/json")
        ElseIf CBool(InStr(e.Request.Uri, "skip-events")) Or CBool(InStr(e.Request.Uri, "datalab-intro-v2")) Then ' skip CR skips 
            e.Response = WebView2.CoreWebView2.Environment.CreateWebResourceResponse(StringToStream("", Encoding.UTF8), 204, "Basst scho", "content-type: application/json")
        ElseIf CBool(InStr(e.Request.Uri, "width=800")) And CBool(InStr(e.Request.Uri, ".jpe")) Then
            Console.WriteLine("Thumb:" + e.Request.Uri)
        ElseIf CR_JS_modShort.Contains(e.Request.Uri) Then '

            For i As Integer = 0 To CR_JS_mod.Count - 1
                If CR_JS_mod(i).Url = e.Request.Uri Then
                    e.Response = WebView2.CoreWebView2.Environment.CreateWebResourceResponse(StringToStream(CR_JS_mod(i).Content, Encoding.UTF8), 200, "Not found", "content-type: application/javascript")
                    Exit For
                End If
            Next

        End If


    End Sub

    Private Sub WebView2_NavigationCompleted(sender As Object, e As CoreWebView2NavigationCompletedEventArgs) Handles WebView2.NavigationCompleted

        If CBool(InStr(WebView2.CoreWebView2.Source, "/concert/")) Then
            WebsiteUrl = WebView2.CoreWebView2.Source
            Dim NewAPI_0 As String() = WebsiteUrl.Replace("/concert", "").Split(New String() {"watch/"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim NewAPI_1 As String() = NewAPI_0(1).Split(New String() {"/"}, System.StringSplitOptions.RemoveEmptyEntries)
            GUID = NewAPI_1(0)
        ElseIf CBool(InStr(WebView2.CoreWebView2.Source, "/musicvideo/")) Then
            WebsiteUrl = WebView2.CoreWebView2.Source
            Dim NewAPI_0 As String() = WebsiteUrl.Replace("/musicvideo", "").Split(New String() {"watch/"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim NewAPI_1 As String() = NewAPI_0(1).Split(New String() {"/"}, System.StringSplitOptions.RemoveEmptyEntries)
            GUID = NewAPI_1(0)
        ElseIf CBool(InStr(WebView2.CoreWebView2.Source, "watch/")) Then
            WebsiteUrl = WebView2.CoreWebView2.Source
            Dim NewAPI_0 As String() = WebView2.CoreWebView2.Source.Split(New String() {"watch/"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim NewAPI_1 As String() = NewAPI_0(1).Split(New String() {"/"}, System.StringSplitOptions.RemoveEmptyEntries)
            GUID = NewAPI_1(0)
        ElseIf CBool(InStr(WebView2.CoreWebView2.Source, "/series/")) Then
            Me.Close()
        End If



        'Pause(5)
        'ProcessCR()
        'If GotIt = True Then
        '    Pause(5)
        '    ProcessCR()
        'End If
    End Sub

    Sub Reload()
        WebView2.CoreWebView2.CallDevToolsProtocolMethodAsync("Network.clearBrowserCache", "{}")
        WebView2.CoreWebView2.Navigate(Startseite)
    End Sub
    Async Sub ProcessCR()


        Dim AllowStatus As String = Await WebView2.CoreWebView2.ExecuteScriptAsync("function iframe_allow(){ 
        var iframe = document.querySelector('iframe')
        return iframe.allow
        }
        iframe_allow()
        ")
        Console.WriteLine(AllowStatus)
        If CBool(InStr(AllowStatus, "microphone *;")) = False And ReloadCount > 0 Then
            Reload()
            ReloadCount = ReloadCount - 1
            Exit Sub
        ElseIf CBool(InStr(AllowStatus, "microphone *;")) = False Then
            MsgBox("Realod error!")
            Exit Sub
        End If

        Console.WriteLine("Alles klar, Kinder?")
        Done = True

        Await WebView2.CoreWebView2.ExecuteScriptAsync("var iframe = document.querySelector('iframe');
        iframe.contentWindow.navigator.mediaDevices.enumerateDevices()
                .then(function(devices) {
                 	var audioDevices = devices.filter(device => device.kind === 'audiooutput');
                	var audioDevicesUse = devices.filter(device => device.label=== '" + Me.Text + "');
                	var video = iframe.contentWindow.document.querySelector('video');
                    var p2 = new Promise(
                    // Resolver-Funktion kann den Promise sowohl auflösen als auch verwerfen
                    // reject the promise
                    function(resolve, reject) {       
                      video.setSinkId(audioDevicesUse[0].deviceId)
                    });

                  p2.then(
                	console.log('the end')

                   );
                })
                .catch(function(err) {
                  console.log(err.name + ': ' + err.message);
                });")
        Pause(3)

        Dim out As String = Await WebView2.CoreWebView2.ExecuteScriptAsync("document.getElementsByClassName('erc-large-header')[0].remove();
var iframe = document.querySelector('iframe');
iframe.classList.remove('video-player');
iframe.height = 1084;
iframe.width = 1926;
var video = iframe.contentWindow.document.querySelector('video');
video.play();
video.volume = 1.0; 
video.currentTime  = 0;


") ' video.playbackRate = 2.0;
        Console.WriteLine("Dann schwingt euch an Deck und kommt ja nicht zu spät!")
        'iframe.requestFullscreen();
        'GetVideoLength()

        VideoDuration = Await Me.WebView2.CoreWebView2.ExecuteScriptAsync("function GetTime(){ 
var iframe = document.querySelector('iframe');
var video = iframe.contentWindow.document.querySelector('video');
return video.duration ;
}
GetTime()
")
        Pause(3)
        Console.WriteLine("<-" + VideoDuration + "->")


        'iframe.height = 1080;
        'iframe.width = 1920;
        '
        '        Await WebView2.CoreWebView2.ExecuteScriptAsync("
        'var iframe = document.querySelector('iframe');
        'var iframestyle = document.createElement('style');
        'iframestyle.type = 'text/css';
        'iframestyle.innerHTML = '.cssClass { border: none; }';
        'document.getElementsByTagName('head')[0].appendChild(iframestyle);
        'iframe.className = 'cssClass';
        'console.log('donec css');
        '")
        '        Console.WriteLine("----JS----")
        '        Console.WriteLine(out)
        '        Console.WriteLine("----JS-END----")
        '        Pause(2)
        '        Await WebView2.CoreWebView2.ExecuteScriptAsync("document.querySelector('body').style.overflow='scroll';
        'var style=document.createElement('style');
        'style.type='text/css';
        'style.innerHTML='::-webkit-scrollbar{display:none}';
        'document.getElementsByTagName('body')[0].appendChild(style);


        '")

        'Pause(10)

        'GetVideoLength()
        TimeTimer.Start()
    End Sub


    Async Sub GetCurrentTime()

        VideoTime = Await Me.WebView2.CoreWebView2.ExecuteScriptAsync("function GetTime(){ 
var iframe = document.querySelector('iframe')
var video = iframe.contentWindow.document.querySelector('video');

return video.currentTime ;
}
GetTime()
")

        Console.WriteLine("<--" + VideoTime + "-->")


        Dim WastedTime As Double = Math.Round(CDbl(VideoTime), 3, MidpointRounding.AwayFromZero)
        Dim AbsoluteTime As Double = Math.Round(CDbl(VideoDuration), 3, MidpointRounding.AwayFromZero)

        Console.WriteLine("[DEB: ]" + WastedTime.ToString + " _ " + AbsoluteTime.ToString)

        If WastedTime + CDbl(3) > AbsoluteTime Then
            TimeTimer.Enabled = False
            Console.WriteLine("<--" + VideoDuration + "-->")

            Pause_ms(CInt((AbsoluteTime - WastedTime) * 1000) + 1000)
            Console.WriteLine(">-Fine-<")
        End If
        '
        'Dim Total_JS_Cleaned() As String = VideoDuration.Split(New String() {"."}, System.StringSplitOptions.RemoveEmptyEntries)

        'Dim Current_JS_Cleaned() As String = VideoTime.Split(New String() {"."}, System.StringSplitOptions.RemoveEmptyEntries)

        'If CInt(Current_JS_Cleaned(0)) + 3 > CInt(Total_JS_Cleaned(0)) Then
        '    TimeTimer.Enabled = False
        '    Console.WriteLine("<--" + VideoDuration + "-->")
        '    Pause(3)
        '    Console.WriteLine(">-Fine-<")
        'End If



    End Sub

    Async Sub GetVideoLength()

        VideoDuration = Await Me.WebView2.CoreWebView2.ExecuteScriptAsync(" 
var iframe = document.querySelector('iframe');
var video = iframe.contentWindow.document.querySelector('video');
return video.duration ;

")
        Console.WriteLine("<-" + VideoDuration + "->")
    End Sub

    Async Sub GetVideoLength2()

        VideoDuration = Await Me.WebView2.CoreWebView2.ExecuteScriptAsync("function GetTime(){ 
var iframe = document.querySelector('iframe');
var video = iframe.contentWindow.document.querySelector('video');
return video.duration ;
}
GetTime()
")
        Console.WriteLine("<-" + VideoDuration + "->")
    End Sub


    Private Sub TimeTimer_Tick(sender As Object, e As EventArgs) Handles TimeTimer.Tick
        GetCurrentTime()

    End Sub



    Private Sub WebView2_NavigationStarting(sender As Object, e As CoreWebView2NavigationStartingEventArgs) Handles WebView2.NavigationStarting

        If CBool(InStr(e.Uri, "LTfcnZPdgeM")) And Me.Text = "Setup" Then ' perm granted
            'e.Response = WebView2.CoreWebView2.Environment.CreateWebResourceResponse(StringToStream(File.ReadAllText(Application.StartupPath + "\lib\settings.html"), Encoding.UTF8), 200, "Setup Page", "content-type: text/html")
            Me.Invoke(New Action(Function() As Object
                                     WebView2.CoreWebView2.Navigate("https://www.crunchyroll.com/")
                                     MicPer = True
                                     'MsgBox(True)
                                     Return Nothing
                                 End Function))
            Exit Sub
        End If

        If CBool(InStr(e.Uri, "kibMJYxeR1s")) And Me.Text = "Setup" Then ' perm not granted 
            Me.Invoke(New Action(Function() As Object
                                     WebView2.CoreWebView2.Navigate("https://www.crunchyroll.com/")
                                     MicPer = False
                                     Return Nothing
                                 End Function))
            Exit Sub
        End If
    End Sub



    Sub ProcessObjectJson(ByVal ObjectJson As String)
#Region "Name"

        Dim CR_MetadataUsage As Boolean = False
        Dim CR_series_title As String = Nothing
        Dim CR_season_number As String = Nothing
        Dim CR_FolderSeason As String = Nothing
        Dim CR_episode As String = Nothing
        Dim CR_episode_duration_ms As String = "60000000"
        Dim CR_episode2 As String = Nothing
        Dim CR_Anime_Staffel_int As String = Nothing
        Dim CR_episode_int As String = Nothing
        Dim CR_title As String = Nothing
        Dim CR_audio_locale As String = "ja-JP"
        Dim TextBox2_Text As String = Nothing
#Region "Parse Json"

        If CBool(InStr(WebsiteUrl, "musicvideo")) = True Then
            'TextBox2_Text to bypasss name for now


            Dim Title() As String = ObjectJson.Split(New String() {Chr(34) + "title" + Chr(34) + ":" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim Title2() As String = Title(1).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)

            Dim Arti() As String = ObjectJson.Split(New String() {Chr(34) + "name" + Chr(34) + ":" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim Arti2() As String = Arti(1).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)

            TextBox2_Text = Arti2(0) + " - " + Title2(0)

        ElseIf CBool(InStr(WebsiteUrl, "/concert/")) = True Then


            Dim Title() As String = ObjectJson.Split(New String() {Chr(34) + "title" + Chr(34) + ":" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim Title2() As String = Title(1).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)

            Dim Arti() As String = ObjectJson.Split(New String() {Chr(34) + "name" + Chr(34) + ":" + Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim Arti2() As String = Arti(1).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)


            'MsgBox(Arti2(0))
            'MsgBox(Title2(0))


            TextBox2_Text = Arti2(0) + " - " + Title2(0)

#End Region

        Else ' Not needed for Music or concerts


            Dim ser As JObject = JObject.Parse(ObjectJson)
            Dim data As List(Of JToken) = ser.Children().ToList

            For Each item As JProperty In data
                item.CreateReader()
                Select Case item.Name

                    Case "data" 'each record is inside the entries array
                        For Each Entry As JObject In item.Values
                            Try
                                Dim Title As String = Entry("title").ToString
                                CR_title = String.Join(" ", Title.Split(invalids, StringSplitOptions.RemoveEmptyEntries)).TrimEnd("."c).Replace(Chr(34), "").Replace("\", "").Replace("/", "").Replace(":", "")
                                Debug.WriteLine(Date.Now.ToString + " CR-Title: " + CR_title)
                            Catch ex As Exception
                            End Try
                            Dim SubData As List(Of JToken) = Entry.Children().ToList
                            For Each SubItem As JProperty In SubData
                                'SubItem.CreateReader()
                                Select Case SubItem.Name
                                    Case "episode_metadata"
                                        For Each SubEntry As JProperty In SubItem.Values
                                            Select Case SubEntry.Name
                                                Case "series_title"
                                                    CR_series_title = SubEntry.Value.ToString.Replace(Chr(34), "").Replace("\", "").Replace("/", "").Replace(":", "")
                                                    'Case "season_title"
                                                    '    CR_season_title = SubEntry.Value.ToString
                                                Case "season_number"
                                                    CR_season_number = SubEntry.Value.ToString.Replace(Chr(34), "").Replace("\", "").Replace("/", "").Replace(":", "")
                                                Case "episode_number"
                                                    CR_episode2 = SubEntry.Value.ToString.Replace(Chr(34), "").Replace("\", "").Replace("/", "").Replace(":", "")
                                                Case "episode"
                                                    CR_episode = SubEntry.Value.ToString.Replace(Chr(34), "").Replace("\", "").Replace("/", "").Replace(":", "")
                                                Case "duration_ms"
                                                    CR_episode_duration_ms = SubEntry.Value.ToString.Replace(Chr(34), "").Replace("\", "").Replace("/", "").Replace(":", "")
                                                Case "audio_locale"
                                                    CR_audio_locale = SubEntry.Value.ToString.Replace(Chr(34), "").Replace("\", "").Replace("/", "").Replace(":", "")
                                            End Select
                                        Next '
                                End Select
                            Next
                        Next
                End Select
            Next

        End If

#End Region
#Region "Name Gen"
        If TextBox2_Text = Nothing Then
            Dim MergeSearch As String = "if not changed no Merch possible, i hope..."
            If CR_episode = Nothing Or CR_episode = "" And CR_episode2 = Nothing Then
                CR_episode_int = "0"
            ElseIf CR_episode IsNot Nothing And CR_episode IsNot "" Then
                CR_episode_int = CR_episode
            ElseIf CR_episode2 IsNot Nothing Then
                CR_episode_int = CR_episode2
            End If
            CR_Anime_Staffel_int = CR_season_number
            If CR_season_number = "1" Or CR_season_number = "0" Then
                CR_season_number = Nothing
            End If


            If CR_episode = Nothing And CR_episode2 = Nothing Then 'no episode number means most likey a movie 
                CR_season_number = Nothing
            ElseIf CR_season_number = Nothing Then
            Else
                CR_season_number = "Season " + CR_season_number
            End If


            CR_FolderSeason = CR_season_number



            If CR_episode = Nothing Or CR_episode = "" And CR_episode2 = Nothing Then
                CR_episode = CR_title
            ElseIf CR_episode IsNot Nothing And CR_episode IsNot "" Then
                CR_episode = "Episode " + AddLeadingZeros(CR_episode)
            ElseIf CR_episode2 IsNot Nothing Then
                CR_episode = "Episode " + AddLeadingZeros(CR_episode2)
            End If
            'CR_episode = "Episode " + AddLeadingZeros(CR_episode)

            Dim NameBuilder As String = "AnimeTitle;Season;EpisodeNR;"
            Dim NameParts As String() = NameBuilder.Split(New String() {";"}, System.StringSplitOptions.RemoveEmptyEntries)
            For i As Integer = 0 To NameParts.Count - 1
                If NameParts(i) = "AnimeTitle" Then
                    CR_FilenName = CR_FilenName + " " + CR_series_title
                    MergeSearch = MergeSearch + " " + CR_series_title
                ElseIf NameParts(i) = "Season" Then
                    CR_FilenName = CR_FilenName + " " + CR_season_number
                    MergeSearch = MergeSearch + " " + CR_season_number
                ElseIf NameParts(i) = "EpisodeNR" Then
                    CR_FilenName = CR_FilenName + " " + CR_episode
                    MergeSearch = MergeSearch + " " + CR_episode
                ElseIf NameParts(i) = "EpisodeName" Then
                    CR_FilenName = CR_FilenName + " " + CR_title
                ElseIf NameParts(i) = "AnimeDub" Then
                    CR_FilenName = CR_FilenName + " " + ConvertSubValue(CR_audio_locale, ConvertSubsEnum.DisplayText)
                ElseIf NameParts(i) = "AnimeSub" Then
                    ' CR_FilenName = CR_FilenName + " RepSub" 'to be done
                End If
            Next

#End Region

            'If KodiNaming = True Then
            Dim KodiString As String = "[S"
            If CR_Anime_Staffel_int = "0" Then
                CR_Anime_Staffel_int = "01"
            Else
                CR_Anime_Staffel_int = "0" + CR_Anime_Staffel_int
            End If

            KodiString = KodiString + CR_Anime_Staffel_int + " E" + AddLeadingZeros(CR_episode_int) ' CR_episode_nr
            KodiString = KodiString + "] "
            CR_FilenName = KodiString + CR_FilenName
            ' End If
        Else
            CR_FilenName = RemoveExtraSpaces(String.Join(" ", TextBox2_Text.Split(invalids, StringSplitOptions.RemoveEmptyEntries)).TrimEnd("."c)).Replace(Chr(34), "").Replace("\", "").Replace("/", "") 'System.Text.RegularExpressions.Regex.Replace(TextBox2_Text, "[^\w\\-]", " "))
        End If
        Debug.WriteLine(CR_FilenName)

        CR_FilenName = String.Join(" ", CR_FilenName.Split(invalids, StringSplitOptions.RemoveEmptyEntries)).TrimEnd("."c).Replace(Chr(34), "").Replace("\", "").Replace("/", "") 'System.Text.RegularExpressions.Regex.Replace(CR_FilenName, "[^\w\\-]", " ")
        CR_FilenName = RemoveExtraSpaces(CR_FilenName)
        Console.WriteLine("||" + CR_FilenName + "||")
        '#End Region


    End Sub

    Public invalids As Char() = System.IO.Path.GetInvalidFileNameChars()
    Public Function RemoveExtraSpaces(input_text As String) As String
        Dim rsRegEx As System.Text.RegularExpressions.Regex
        rsRegEx = New System.Text.RegularExpressions.Regex("\s+")
        Return rsRegEx.Replace(input_text, " ").Trim()
    End Function

    Public LeadingZero As Integer = 1 'Who needs settins? 
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

    Public Enum ConvertSubsEnum
        DisplayText = 0
        MP4CC_ISO_639_2 = 1
        Both = 2
    End Enum

#Region "Convert Subs"

    Public Function ConvertSubValue(ByVal HardSub As String, ByVal Output As Integer) As String
        ' 0 = DisplayText ; 1 = MP4CC/ISO-639-2 ; 2 = Both 

        HardSub = HardSub.Replace(Chr(34), "") 'clean up any mess just in case... 

        For i As Integer = 0 To LangValueEnum.Count - 1
            If LangValueEnum(i).CR_Value = HardSub Or LangValueEnum(i).FM_Value = HardSub Then

                If Output = 1 Then ' MP4CC/ISO-639-2 replacing the old ConvertSubValue version |   'Return ConvertSubValue(HardSub)
                    Return LangValueEnum(i).MP4CC
                ElseIf Output = 2 Then '; 2 = Both replacing the old GetSubFileLangName funktion 
                    Dim RS As String = LangValueEnum(i).DisplayText + "." + LangValueEnum(i).MP4CC
                    Return RS
                Else
                    Return LangValueEnum(i).DisplayText
                End If

            End If
        Next

        Return HardSub + "-not-found"

    End Function

    Public LangValueEnum As New List(Of NameValuePair)
    Sub FillArray() '

        LangValueEnum.Add(New NameValuePair("[ null ]", "", "null", Nothing))

        LangValueEnum.Add(New NameValuePair("Deutsch", "ger", "de-DE", Nothing)) '

        LangValueEnum.Add(New NameValuePair("English", "eng", "en-US", "en"))

        LangValueEnum.Add(New NameValuePair("Português (Brasil)", "por", "pt-BR", "pt"))

        LangValueEnum.Add(New NameValuePair("Português (Portugal)", "por", "pt-PT", Nothing))

        LangValueEnum.Add(New NameValuePair("Español (LA)", "spa", "es-419", "es"))

        LangValueEnum.Add(New NameValuePair("Français (France)", "fre", "fr-FR", Nothing))

        LangValueEnum.Add(New NameValuePair("العربية (Arabic)", "ara", "ar-SA", Nothing))

        LangValueEnum.Add(New NameValuePair("Polski", "pol", "pl-PL", Nothing))

        LangValueEnum.Add(New NameValuePair("Русский (Russian)", "rus", "ru-RU", Nothing))

        LangValueEnum.Add(New NameValuePair("Italiano (Italian)", "ita", "it-IT", Nothing))

        LangValueEnum.Add(New NameValuePair("Español (España)", "spa", "es-ES", Nothing))

        LangValueEnum.Add(New NameValuePair("Türkçe", "tur", "tr-TR", Nothing))

        LangValueEnum.Add(New NameValuePair("Bahasa Indonesia", "ind", "id-ID", Nothing))

        LangValueEnum.Add(New NameValuePair("Bahasa Melayu", "msa", "ms-MY", Nothing))

        LangValueEnum.Add(New NameValuePair("Català", "cat", "ca-ES", Nothing))

        LangValueEnum.Add(New NameValuePair("Tiếng Việt", "vie", "vi-VN", Nothing))

        LangValueEnum.Add(New NameValuePair("English (India)", "eng", "en-IN", Nothing))

        LangValueEnum.Add(New NameValuePair("తెలుగు (Telegu)", "tel", "te-IN", Nothing))

        LangValueEnum.Add(New NameValuePair("हिंदी (Hindi)", "hin", "hi-IN", Nothing))

        LangValueEnum.Add(New NameValuePair("தமிழ் (Tamil)", "tam", "ta-IN", Nothing))

        LangValueEnum.Add(New NameValuePair("中文 (中国)", "zho", "zh-CN", Nothing))

        LangValueEnum.Add(New NameValuePair("中文 (台灣)", "zho", "zh-TW", Nothing))

        LangValueEnum.Add(New NameValuePair("한국어", "kor", "ko-KR", Nothing))

        LangValueEnum.Add(New NameValuePair("ไทย", "tha", "th-TH", Nothing))

        LangValueEnum.Add(New NameValuePair("Japanese", "jpn", "ja-JP", Nothing))

    End Sub

    Private Sub WebView2_Click(sender As Object, e As EventArgs) Handles WebView2.Click

    End Sub


#End Region


End Class
Public Class NameValuePair

    Public DisplayText As String
    Public MP4CC As String
    Public CR_Value As String
    Public FM_Value As String
    Public Sub New(ByVal DisplayText As String, ByVal MP4CC As String, ByVal CR_Value As String, ByVal FM_Value As String)
        Me.MP4CC = MP4CC
        Me.DisplayText = DisplayText
        Me.CR_Value = CR_Value
        Me.FM_Value = FM_Value
    End Sub

    Public Overrides Function ToString() As String
        Return String.Format("{0}, {1}", Me.DisplayText, Me.MP4CC, Me.CR_Value, Me.FM_Value)
    End Function
End Class