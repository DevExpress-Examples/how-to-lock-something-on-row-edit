Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.Xpf
Imports System
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Linq
Imports System.Threading

Namespace LockOnRowEdit_MVVM

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

    Public Class MainViewModel
        Inherits ViewModelBase

        Public Property Data As ObservableCollection(Of DataItem)
            Get
                Return GetValue(Of ObservableCollection(Of DataItem))()
            End Get

            Set(ByVal value As ObservableCollection(Of DataItem))
                SetValue(value)
            End Set
        End Property

        Private random As Random

        Private updateTimer As Timer

        Private updatesLocker As Boolean

        Public Sub New()
            random = New Random()
            Data = New ObservableCollection(Of DataItem)(Enumerable.Range(0, 20).[Select](Function(i) New DataItem(random, i)))
            updateTimer = New Timer(AddressOf UpdateRows, Nothing, 0, 1)
        End Sub

        <Command>
        Public Sub LockUpdates(ByVal args As RowEditStartedArgs)
            Volatile.Write(updatesLocker, True)
        End Sub

        <Command>
        Public Sub UnlockUpdates(ByVal args As RowEditFinishedArgs)
            Volatile.Write(updatesLocker, False)
        End Sub

        Private Sub UpdateRows(ByVal parameter As Object)
            If Not Volatile.Read(updatesLocker) Then
                Dim row = Data(random.Next(0, Data.Count))
                If row.ShouldUpdate Then
                    row.Value = random.Next(1, 100)
                End If
            End If
        End Sub
    End Class
End Namespace
