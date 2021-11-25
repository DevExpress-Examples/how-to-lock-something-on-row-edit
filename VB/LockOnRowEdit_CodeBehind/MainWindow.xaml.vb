Imports DevExpress.Mvvm
Imports DevExpress.Xpf.Grid
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Threading
Imports System.Windows

Namespace LockOnRowEdit_CodeBehind

    Public Class DataItem
        Inherits BindableBase

        Public Property Id As Integer
            Get
                Return GetValue(Of Integer)()
            End Get

            Set(ByVal value As Integer)
                SetValue(value)
            End Set
        End Property

        Public Property Name As String
            Get
                Return GetValue(Of String)()
            End Get

            Set(ByVal value As String)
                SetValue(value)
            End Set
        End Property

        Public Property Value As Integer
            Get
                Return GetValue(Of Integer)()
            End Get

            Set(ByVal value As Integer)
                SetValue(value)
            End Set
        End Property

        Public Property ShouldUpdate As Boolean
            Get
                Return GetValue(Of Boolean)()
            End Get

            Set(ByVal value As Boolean)
                SetValue(value)
            End Set
        End Property

        Public Sub New(ByVal random As Random, ByVal id As Integer)
            Me.Id = id
            Name = $"Item {id}"
            Value = random.Next(1, 100)
            ShouldUpdate = True
        End Sub
    End Class

    Public Partial Class MainWindow
        Inherits Window

        Private data As List(Of DataItem)

        Private updateTimer As Timer

        Private updatesLocker As Boolean

        Private random As Random

        Public Sub New()
            Me.InitializeComponent()
            random = New Random()
            data = New List(Of DataItem)(Enumerable.Range(0, 20).[Select](Function(i) New DataItem(random, i)))
            Me.grid.ItemsSource = data
            updateTimer = New Timer(AddressOf UpdateRows, Nothing, 0, 1)
        End Sub

        Private Sub OnRowEditStarted(ByVal sender As Object, ByVal e As RowEditStartedEventArgs)
            Volatile.Write(updatesLocker, True)
        End Sub

        Private Sub OnRowEditFinished(ByVal sender As Object, ByVal e As RowEditFinishedEventArgs)
            Volatile.Write(updatesLocker, False)
        End Sub

        Private Sub UpdateRows(ByVal parameter As Object)
            If Not Volatile.Read(updatesLocker) Then
                Dim row = data(random.Next(0, data.Count))
                If row.ShouldUpdate Then
                    row.Value = random.Next(1, 100)
                End If
            End If
        End Sub
    End Class
End Namespace
