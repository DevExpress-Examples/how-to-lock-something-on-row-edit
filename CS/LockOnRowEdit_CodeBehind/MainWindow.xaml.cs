using DevExpress.Mvvm;
using DevExpress.Xpf.Grid;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LockOnRowEdit_CodeBehind {
    public class Employee : BindableBase {
        public int Id { get => GetValue<int>(); set => SetValue(value); }
        public int ParentId { get => GetValue<int>(); set => SetValue(value); }
        public string Name { get => GetValue<string>(); set => SetValue(value); }
        public string Position { get => GetValue<string>(); set => SetValue(value); }
        public string Department { get => GetValue<string>(); set => SetValue(value); }
    }

    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            grid.ItemsSource = new ObservableCollection<Employee>(GetEmployees());
        }

        IEnumerable<Employee> GetEmployees() {
            yield return new Employee() { Id = 1, ParentId = 0, Name = "Gregory S. Price", Department = "", Position = "President" };
            yield return new Employee() { Id = 2, ParentId = 1, Name = "Irma R. Marshall", Department = "Marketing", Position = "Vice President" };
            yield return new Employee() { Id = 3, ParentId = 1, Name = "John C. Powell", Department = "Operations", Position = "Vice President" };
            yield return new Employee() { Id = 4, ParentId = 1, Name = "Christian P. Laclair", Department = "Production", Position = "Vice President" };
            yield return new Employee() { Id = 5, ParentId = 1, Name = "Karen J. Kelly", Department = "Finance", Position = "Vice President" };

            yield return new Employee() { Id = 6, ParentId = 2, Name = "Brian C. Cowling", Department = "Marketing", Position = "Manager" };
            yield return new Employee() { Id = 7, ParentId = 2, Name = "Thomas C. Dawson", Department = "Marketing", Position = "Manager" };
            yield return new Employee() { Id = 8, ParentId = 2, Name = "Angel M. Wilson", Department = "Marketing", Position = "Manager" };
            yield return new Employee() { Id = 9, ParentId = 2, Name = "Bryan R. Henderson", Department = "Marketing", Position = "Manager" };

            yield return new Employee() { Id = 10, ParentId = 3, Name = "Harold S. Brandes", Department = "Operations", Position = "Manager" };
            yield return new Employee() { Id = 11, ParentId = 3, Name = "Michael S. Blevins", Department = "Operations", Position = "Manager" };
            yield return new Employee() { Id = 12, ParentId = 3, Name = "Jan K. Sisk", Department = "Operations", Position = "Manager" };
            yield return new Employee() { Id = 13, ParentId = 3, Name = "Sidney L. Holder", Department = "Operations", Position = "Manager" };

            yield return new Employee() { Id = 14, ParentId = 4, Name = "James L. Kelsey", Department = "Production", Position = "Manager" };
            yield return new Employee() { Id = 15, ParentId = 4, Name = "Howard M. Carpenter", Department = "Production", Position = "Manager" };
            yield return new Employee() { Id = 16, ParentId = 4, Name = "Jennifer T. Tapia", Department = "Production", Position = "Manager" };

            yield return new Employee() { Id = 17, ParentId = 5, Name = "Judith P. Underhill", Department = "Finance", Position = "Manager" };
            yield return new Employee() { Id = 18, ParentId = 5, Name = "Russell E. Belton", Department = "Finance", Position = "Manager" };
        }

        private void TableView_RowEditStarted(object sender, RowEditStartedEventArgs e) {
            //view.FixRow(e.RowHandle, FixedRowPosition.Top);
        }

        private void TableView_RowEditFinished(object sender, RowEditFinishedEventArgs e) {
            //view.FixedTopRows.RemoveAt(0);
        }
    }
}
