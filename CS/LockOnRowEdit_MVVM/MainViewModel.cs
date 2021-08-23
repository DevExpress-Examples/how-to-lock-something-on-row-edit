﻿using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.Xpf;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;

namespace LockOnRowEdit_MVVM {
    public class DataItem : BindableBase {
        public int Id { get => GetValue<int>(); set => SetValue(value); }
        public string Name { get => GetValue<string>(); set => SetValue(value);}
        public int Value { get => GetValue<int>(); set => SetValue(value); }
        public bool ShouldUpdate { get => GetValue<bool>(); set => SetValue(value); }

        public DataItem(Random random, int id) {
            Id = id;
            Name = $"Item {id}";
            Value = random.Next(1, 100);
            ShouldUpdate = true;
        }
    }
    public class MainViewModel : ViewModelBase {
        public ObservableCollection<DataItem> Data { get => GetValue<ObservableCollection<DataItem>>(); set => SetValue(value); }

        Random random;

        Timer updateTimer;

        volatile bool updatesLocker;

        public MainViewModel() {
            random = new Random();
            Data = new ObservableCollection<DataItem>(Enumerable.Range(0, 20).Select(i => new DataItem(random, i)));
            updateTimer = new Timer(UpdateRows, null, 0, 1);
        }

        [Command]
        public void LockUpdates(RowEditStartedArgs args) => updatesLocker = true;

        [Command]
        public void UnlockUpdates(RowEditFinishedArgs args) => updatesLocker = false;

        void UpdateRows(object parameter) {
            if(!updatesLocker) {
                var row = Data[random.Next(0, Data.Count - 1)];
                if(row.ShouldUpdate) {
                    row.Value = random.Next(1, 100);
                }
            }
        }
    }
}
