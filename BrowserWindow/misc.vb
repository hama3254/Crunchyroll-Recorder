Public Class misc




End Class
Public Class ServerResponse

    Public Type As String
    Public Content As String
    Public Sub New(ByVal Content As String, ByVal Type As String)
        Me.Content = Content
        Me.Type = Type

    End Sub

    Public Overrides Function ToString() As String
        Return String.Format("{0}, {1}", Me.Content, Me.Type)
    End Function
End Class

Public Class ServerResponseCache

    Public Url As String
    Public Content As String
    Public Sub New(ByVal Content As String, ByVal Url As String)
        Me.Content = Content
        Me.Url = Url

    End Sub

    Public Overrides Function ToString() As String
        Return String.Format("{0}, {1}", Me.Content, Me.Url)
    End Function
End Class
