﻿'------------------------------------------------------------------------------
' <auto-generated>
'     Dieser Code wurde von einem Tool generiert.
'     Laufzeitversion:4.0.30319.42000
'
'     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
'     der Code erneut generiert wird.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Namespace My
    
    <Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(),  _
     Global.System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.6.0.0"),  _
     Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
    Partial Friend NotInheritable Class MySettings
        Inherits Global.System.Configuration.ApplicationSettingsBase
        
        Private Shared defaultInstance As MySettings = CType(Global.System.Configuration.ApplicationSettingsBase.Synchronized(New MySettings()),MySettings)
        
#Region "Automatische My.Settings-Speicherfunktion"
#If _MyType = "WindowsForms" Then
    Private Shared addedHandler As Boolean

    Private Shared addedHandlerLockObject As New Object

    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)> _
    Private Shared Sub AutoSaveSettings(sender As Global.System.Object, e As Global.System.EventArgs)
        If My.Application.SaveMySettingsOnExit Then
            My.Settings.Save()
        End If
    End Sub
#End If
#End Region
        
        Public Shared ReadOnly Property [Default]() As MySettings
            Get
                
#If _MyType = "WindowsForms" Then
               If Not addedHandler Then
                    SyncLock addedHandlerLockObject
                        If Not addedHandler Then
                            AddHandler My.Application.Shutdown, AddressOf AutoSaveSettings
                            addedHandler = True
                        End If
                    End SyncLock
                End If
#End If
                Return defaultInstance
            End Get
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property DarkModeValue() As Boolean
            Get
                Return CType(Me("DarkModeValue"),Boolean)
            End Get
            Set
                Me("DarkModeValue") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property Pfad() As String
            Get
                Return CType(Me("Pfad"),String)
            End Get
            Set
                Me("Pfad") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute(" -c copy -c:a copy -bsf:a aac_adtstoasc")>  _
        Public Property ffmpeg_command() As String
            Get
                Return CType(Me("ffmpeg_command"),String)
            End Get
            Set
                Me("ffmpeg_command") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("None")>  _
        Public Property AddedSubs() As String
            Get
                Return CType(Me("AddedSubs"),String)
            End Get
            Set
                Me("AddedSubs") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("[ ignore subfolder ]")>  _
        Public Property SubFolder_Value() As String
            Get
                Return CType(Me("SubFolder_Value"),String)
            End Get
            Set
                Me("SubFolder_Value") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property TempFolder() As String
            Get
                Return CType(Me("TempFolder"),String)
            End Get
            Set
                Me("TempFolder") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("https://www.crunchyroll.com/")>  _
        Public Property Startseite() As String
            Get
                Return CType(Me("Startseite"),String)
            End Get
            Set
                Me("Startseite") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("[default season prefix]")>  _
        Public Property Prefix_S() As String
            Get
                Return CType(Me("Prefix_S"),String)
            End Get
            Set
                Me("Prefix_S") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("[default episode prefix]")>  _
        Public Property Prefix_E() As String
            Get
                Return CType(Me("Prefix_E"),String)
            End Get
            Set
                Me("Prefix_E") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property DefaultSubCR() As String
            Get
                Return CType(Me("DefaultSubCR"),String)
            End Get
            Set
                Me("DefaultSubCR") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute(".mp4")>  _
        Public Property VideoFormat() As String
            Get
                Return CType(Me("VideoFormat"),String)
            End Get
            Set
                Me("VideoFormat") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("[merge disabled]")>  _
        Public Property MergeSubs() As String
            Get
                Return CType(Me("MergeSubs"),String)
            End Get
            Set
                Me("MergeSubs") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property KodiSupport() As Boolean
            Get
                Return CType(Me("KodiSupport"),Boolean)
            End Get
            Set
                Me("KodiSupport") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property Dark_Mode() As Boolean
            Get
                Return CType(Me("Dark_Mode"),Boolean)
            End Get
            Set
                Me("Dark_Mode") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property QueueMode() As Boolean
            Get
                Return CType(Me("QueueMode"),Boolean)
            End Get
            Set
                Me("QueueMode") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property IncludeLangName() As Boolean
            Get
                Return CType(Me("IncludeLangName"),Boolean)
            End Get
            Set
                Me("IncludeLangName") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property CR_Chapters() As Boolean
            Get
                Return CType(Me("CR_Chapters"),Boolean)
            End Get
            Set
                Me("CR_Chapters") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property DubMode() As Boolean
            Get
                Return CType(Me("DubMode"),Boolean)
            End Get
            Set
                Me("DubMode") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0")>  _
        Public Property LangNameType() As Integer
            Get
                Return CType(Me("LangNameType"),Integer)
            End Get
            Set
                Me("LangNameType") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("1080")>  _
        Public Property Reso() As Integer
            Get
                Return CType(Me("Reso"),Integer)
            End Get
            Set
                Me("Reso") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0")>  _
        Public Property CR_NameMethode() As Integer
            Get
                Return CType(Me("CR_NameMethode"),Integer)
            End Get
            Set
                Me("CR_NameMethode") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0")>  _
        Public Property LeadingZero() As Integer
            Get
                Return CType(Me("LeadingZero"),Integer)
            End Get
            Set
                Me("LeadingZero") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0")>  _
        Public Property IgnoreSeason() As Integer
            Get
                Return CType(Me("IgnoreSeason"),Integer)
            End Get
            Set
                Me("IgnoreSeason") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("None")>  _
        Public Property Subtitle() As String
            Get
                Return CType(Me("Subtitle"),String)
            End Get
            Set
                Me("Subtitle") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("Unused")>  _
        Public Property NameTemplate() As String
            Get
                Return CType(Me("NameTemplate"),String)
            End Get
            Set
                Me("NameTemplate") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("ja-JP")>  _
        Public Property CR_Dub() As String
            Get
                Return CType(Me("CR_Dub"),String)
            End Get
            Set
                Me("CR_Dub") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("null")>  _
        Public Property ffmpeg_command_override() As String
            Get
                Return CType(Me("ffmpeg_command_override"),String)
            End Get
            Set
                Me("ffmpeg_command_override") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property Captions() As Boolean
            Get
                Return CType(Me("Captions"),Boolean)
            End Get
            Set
                Me("Captions") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("""User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML,"& _ 
            " like Gecko) Chrome/112.0.0.0 Safari/537.36 Edg/112.0.1722.34""")>  _
        Public Property User_Agend() As String
            Get
                Return CType(Me("User_Agend"),String)
            End Get
            Set
                Me("User_Agend") = value
            End Set
        End Property
    End Class
End Namespace

Namespace My
    
    <Global.Microsoft.VisualBasic.HideModuleNameAttribute(),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute()>  _
    Friend Module MySettingsProperty
        
        <Global.System.ComponentModel.Design.HelpKeywordAttribute("My.Settings")>  _
        Friend ReadOnly Property Settings() As Global.Crunchyroll_Recorder.My.MySettings
            Get
                Return Global.Crunchyroll_Recorder.My.MySettings.Default
            End Get
        End Property
    End Module
End Namespace
