using DevExpress.Mvvm;
using DevExpress.Xpf.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;

namespace LockOnRowEdit_CodeBehind {
    public class DataItem : BindableBase {
        public int Id { get => GetValue<int>(); set => SetValue(value); }
        public string Name { get => GetValue<string>(); set => SetValue(value); }
        public int Value { get => GetValue<int>(); set => SetValue(value); }
        public bool ShouldUpdate { get => GetValue<bool>(); set => SetValue(value); }

        public DataItem(Random random, int id) {
            Id = id;
            Name = $"Item {id}";
            Value = random.Next(1, 100);
            ShouldUpdate = true;
        }
    }
    public partial class MainWindow : Window {
        List<DataItem> data;

        Timer updateTimer;

        volatile bool updatesLocker;

        Random random;

        public MainWindow() {
            InitializeComponent();
            random = new Random();
            grid.ItemsSource = data = new List<DataItem>(Enumerable.Range(0, 20).Select(i => new DataItem(random, i)));
            updateTimer = new Timer(UpdateRows, null, 0, 1);
        }

        private void OnRowEditStarted(object sender, RowEditStartedEventArgs e) {
            updatesLocker = true;
        }

        private void OnRowEditFinished(object sender, RowEditFinishedEventArgs e) {
            updatesLocker = false;
        }

        void UpdateRows(object parameter) {
            if(!updatesLocker) {
                var row = data[random.Next(0, data.Count - 1)];
                if(row.ShouldUpdate) {
                    row.Value = random.Next(1, 100);
                }
            }
        }
    }
}
