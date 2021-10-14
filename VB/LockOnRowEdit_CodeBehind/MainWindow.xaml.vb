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
'''  Private random As Random

        Public Sub New()
            InitializeComponent()
            random = New Random()
            data = New List(Of DataItem)(Enumerable.Range(0, 20).[Select](Function(i) New DataItem(random, i)))
            grid.ItemsSource = data
            updateTimer = New Timer(AddressOf UpdateRows, Nothing, 0, 1)
        End Sub

        Private Sub OnRowEditStarted(ByVal sender As Object, ByVal e As RowEditStartedEventArgs)
            CSharpImpl.__Assign(Me.updatesLocker, True)
        End Sub

        Private Sub OnRowEditFinished(ByVal sender As Object, ByVal e As RowEditFinishedEventArgs)
            CSharpImpl.__Assign(Me.updatesLocker, False)
        End Sub

        Private Sub UpdateRows(ByVal parameter As Object)
            If Not Me.updatesLocker Then
                Dim row = data(random.Next(0, data.Count))
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
