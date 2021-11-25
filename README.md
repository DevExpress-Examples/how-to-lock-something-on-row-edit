<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/398997384/21.2.2%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T1042686)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
# Data Grid for WPF - How to Pause Data Updates in the Edit Form

This example illustartates how to pause data updates when a user edits a row in the Edit Form. To do that, handle the [RowEditStarted](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.TableView.RowEditStarted) and [RowEditFinished](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.TableView.RowEditFinished) events. Alternatively, you can create commands in a View Model and bind them to the [RowEditStartedCommand](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.TableView.RowEditStartedCommand) and [RowEditFinishedCommand](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.TableView.RowEditFinishedCommand) properties.

In this example, the [GridControl](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.GridControl) updates data on a timer. The `UpdateRows()` function updates data depending on the `updatesLocker` value. When a user starts to edit a row, the [RowEditStarted](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.TableView.RowEditStarted) event occurs. The event handler sets the `updatesLocker` value to **true** to lock data updates. When the user finished editing the row, [RowEditFinished](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.TableView.RowEditFinished) occurs and the event handler unlocks updates.

```cs
bool updatesLocker;
// ...
private void OnRowEditStarted(object sender, RowEditStartedEventArgs e) => Volatile.Write(ref updatesLocker, true);

private void OnRowEditFinished(object sender, RowEditFinishedEventArgs e) => Volatile.Write(ref updatesLocker, false);

void UpdateRows(object parameter) {
    if(!Volatile.Read(ref updatesLocker)) {
        var row = data[random.Next(0, data.Count)];
        if(row.ShouldUpdate) {
            row.Value = random.Next(1, 100);
        }
    }
}
```

In the [TreeListView](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.TreeListView), use the following events and properties: 
- [NodeEditStarted](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.TreeListView.NodeEditStarted)
- [NodeEditFinished](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.TreeListView.NodeEditFinished)
- [NodeEditStartedCommand](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.TreeListView.NodeEditStartedCommand)
- [NodeEditFinishedCommand](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.TreeListView.NodeEditFinishedCommand)

<!-- default file list -->

## Files to Look At

### Code-Behind
- [MainViewModel.xaml.cs](./CS/LockOnRowEdit_CodeBehind/MainWindow.xaml.cs#L39-L41) ([MainViewModel.xaml.vb](./VB/LockOnRowEdit_CodeBehind/MainWindow.xaml.vb#L81-L87))
- [MainWindow.xaml](./CS/LockOnRowEdit_CodeBehind/MainWindow.xaml#L13) ([MainWindow.xaml](./VB/LockOnRowEdit_CodeBehind/MainWindow.xaml#L13))

### MVVM Pattern
- [MainViewModel.cs](./CS/LockOnRowEdit_MVVM/MainViewModel.cs#L40-L44) ([MainViewModel.vb](./VB/LockOnRowEdit_MVVM/MainViewModel.vb#L88-L96))
- [MainWindow.xaml](./CS/LockOnRowEdit_MVVM/MainWindow.xaml#L17) ([MainWindow.xaml](./VB/LockOnRowEdit_MVVM/MainWindow.xaml#L17))

<!-- default file list end -->

## Documentation

- [Edit Form](https://docs.devexpress.com/WPF/403491/controls-and-libraries/data-grid/data-editing-and-validation/modify-cell-values/edit-form)
- [RowEditStarted](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.TableView.RowEditStarted) / [NodeEditStarted](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.TreeListView.NodeEditStarted)
- [RowEditFinished](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.TableView.RowEditFinished) / [NodeEditFinished](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.TreeListView.NodeEditFinished)
- [RowEditStartedCommand](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.TableView.RowEditStartedCommand) / [NodeEditStartedCommand](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.TreeListView.NodeEditStartedCommand)
- [RowEditFinishedCommand](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.TableView.RowEditFinishedCommand) / [NodeEditFinishedCommand](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.TreeListView.NodeEditFinishedCommand)

## More Examples

- [Data Grid for WPF - How to Process Related Cells in the Edit Form](https://github.com/DevExpress-Examples/wpf-data-grid-edit-form-related-cells)
- [Data Grid for WPF - How to Specify Edit Form Settings](https://github.com/DevExpress-Examples/wpf-data-grid-specify-edit-form-settings)
