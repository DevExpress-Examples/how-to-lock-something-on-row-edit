Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.Xpf
Imports System
Imports System.Collections.ObjectModel
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

         ''' Cannot convert FieldDeclarationSyntax, System.NotSupportedException: VolatileKeyword is not supported!
'''    at ICSharpCode.CodeConverter.VB.SyntaxKindExtensions.ConvertToken(SyntaxKind t, TokenContext context)
'''    at ICSharpCode.CodeConverter.VB.CommonConversions.ConvertModifier(SyntaxToken m, TokenContext context)
'''    at ICSharpCode.CodeConverter.VB.CommonConversions.<>c__DisplayClass30_0.<ConvertModifiersCore>b__3(SyntaxToken x)
'''    at System.Linq.Enumerable.WhereSelectEnumerableIterator`2.MoveNext()
'''    at System.Linq.Enumerable.WhereSelectEnumerableIterator`2.MoveNext()
'''    at System.Collections.Generic.List`1..ctor(IEnumerable`1 collection)
'''    at System.Linq.Enumerable.ToList[TSource](IEnumerable`1 source)
'''    at ICSharpCode.CodeConverter.VB.CommonConversions.ConvertModifiersCore(IReadOnlyCollection`1 modifiers, TokenContext context, Boolean isConstructor, Boolean isNestedType)
'''    at ICSharpCode.CodeConverter.VB.NodesVisitor.VisitFieldDeclaration(FieldDeclarationSyntax node)
'''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.Visit(SyntaxNode node)
'''    at ICSharpCode.CodeConverter.VB.CommentConvertingVisitorWrapper`1.Accept(SyntaxNode csNode, Boolean addSourceMapping)
''' 
''' Input:
''' 
'''         volatile bool updatesLocker;
''' 
'''  Public Sub New()
            random = New Random()
            Data = New ObservableCollection(Of DataItem)(Enumerable.Range(0, 20).[Select](Function(i) New DataItem(random, i)))
            updateTimer = New Timer(AddressOf UpdateRows, Nothing, 0, 1)
        End Sub

        <Command>
        Public Sub LockUpdates(ByVal args As RowEditStartedArgs)
            CSharpImpl.__Assign(Me.updatesLocker, True)
        End Sub

        <Command>
        Public Sub UnlockUpdates(ByVal args As RowEditFinishedArgs)
            CSharpImpl.__Assign(Me.updatesLocker, False)
        End Sub

        Private Sub UpdateRows(ByVal parameter As Object)
            If Not Me.updatesLocker Then
                Dim row = Data(random.Next(0, Data.Count))
                If row.ShouldUpdate Then
                    row.Value = random.Next(1, 100)
                End If
            End If
        End Sub

        Private Class CSharpImpl

            <Obsolete("Please refactor calling code to use normal Visual Basic assignment")>
            Shared Function __Assign(Of T)(ByRef target As T, value As T) As T
                target = value
                Return value
            End Function
        End Class
    End Class
End Namespace
