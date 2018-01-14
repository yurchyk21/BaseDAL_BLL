using BLL.Concrete;
using BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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
using System.Collections.ObjectModel;

namespace WpfApp_classwork2712
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // private readonly RoleRepository _roleRepository;
        enum Views
        {
            UserView = 1, WorkerView, RoleView
        }

        private Views CurrentView;
        private readonly SqlConnection _con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        private ShopWorkManager _shopWorkManager;
        private RoleManager _roleManager;

        public MainWindow()
        {
            _con.Open();
            _roleManager = new RoleManager(_con);
            _shopWorkManager = new ShopWorkManager(_con);

            InitializeComponent();
            datagrid.AutoGenerateColumns = false;
        }

        private void AddRoleBtn_Click(object sender, RoutedEventArgs e)
        {
            AddRole _windowAddRole = new AddRole(_roleManager);
            _windowAddRole.Show();
            LockBtns();
            UpdtBtn_Click(null, null);

        }

        private void AddUserBtn_Click(object sender, RoutedEventArgs e)
        {
            AddUser _windowAddUser = new AddUser(_shopWorkManager);
            _windowAddUser.Show();
            LockBtns();
            UpdtBtn_Click(null, null);

        }

        private void DelUserBtn_Click(object sender, RoutedEventArgs e)
        {
            if (datagrid.SelectedIndex != 0)
            {
                foreach (var user in datagrid.SelectedItems)
                {
                    _shopWorkManager.Delete(user);
                }
            }
            LockBtns();
            UpdtBtn_Click(null, null);

        }

        private void SetUserAsWorkerBtn_Click(object sender, RoutedEventArgs e)
        {
            if (datagrid.SelectedIndex != 0)
            {
                DateTime _dateTime = new DateTime();
                bool _isLocked = new bool();
                SetWorkerWindow _setWorkerWindow = new SetWorkerWindow(_dateTime, _isLocked);
                if (_setWorkerWindow.ShowDialog() == true)
                {
                    foreach (var user in datagrid.SelectedItems)
                    {
                        _shopWorkManager.SetWorker(user, _dateTime, _isLocked);
                    }
                }
            }
            LockBtns();
            UpdtBtn_Click(null, null);

        }

        private void UsersBtn_Click(object sender, RoutedEventArgs e)
        {

            datagrid.Columns.Clear();
            datagrid.ItemsSource = _shopWorkManager.Users;
            datagrid.Columns.Add(new DataGridTextColumn() { Header = "First Name", Binding = new Binding("FirstName") });
            datagrid.Columns.Add(new DataGridTextColumn() { Header = "Last Name", Binding = new Binding("LastName") });
            datagrid.Columns.Add(new DataGridTextColumn() { Header = "Email Name", Binding = new Binding("Email") });
            CurrentView = Views.UserView;
            LockBtns();
        }

        private void WorkersBtn_Click(object sender, RoutedEventArgs e)
        {

            datagrid.Columns.Clear();
            datagrid.ItemsSource = _shopWorkManager.Workers;
            datagrid.Columns.Add(new DataGridTextColumn() { Header = "First Name", Binding = new Binding("FirstName") });
            datagrid.Columns.Add(new DataGridTextColumn() { Header = "Last Name", Binding = new Binding("LastName") });
            datagrid.Columns.Add(new DataGridTextColumn() { Header = "Email", Binding = new Binding("Email") });
            datagrid.Columns.Add(new DataGridTextColumn() { Header = "Hired Date", Binding = new Binding("HiredDate") });
            datagrid.Columns.Add(new DataGridCheckBoxColumn() { Header = "Is Locked", Binding = new Binding("IsLocked") });
            CurrentView = Views.WorkerView;
            LockBtns();
        }
        private void RolesBtn_Click(object sender, RoutedEventArgs e)
        {

            datagrid.Columns.Clear();
            datagrid.ItemsSource = _roleManager.Roles;
            datagrid.Columns.Add(new DataGridTextColumn() { Header = "Id", Binding = new Binding("Id") });
            datagrid.Columns.Add(new DataGridTextColumn() { Header = "Role Name", Binding = new Binding("Name") });
            CurrentView = Views.RoleView;
            LockBtns();

        }
        private void LockBtns()
        {
            DelUserBtn.IsEnabled = false;
            SetUserAsWorkerBtn.IsEnabled = false;
            LockWorkerBtn.IsEnabled = false;

        }


        private void Datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (CurrentView)
            {
                case Views.UserView:
                    DelUserBtn.IsEnabled = true;
                    SetUserAsWorkerBtn.IsEnabled = true;
                    break;
                case Views.WorkerView:
                    LockWorkerBtn.IsEnabled = true;
                    break;
                default:
                    break;
            }
        }

        private void UpdtBtn_Click(object sender, RoutedEventArgs e)
        {
            switch (CurrentView)
            {
                case Views.UserView:
                    UsersBtn_Click(null,null);
                    break;
                case Views.WorkerView:
                    WorkersBtn_Click(null, null);
                    break;
                case Views.RoleView:
                    RolesBtn_Click(null, null);
                    break;
                default:
                    break;
            }
        }

        private void LockWorkerBtn_Click(object sender, RoutedEventArgs e)
        {
            if (datagrid.SelectedIndex != 0)
            {
                    foreach (var worker in datagrid.SelectedItems)
                    {
                        _shopWorkManager.LockChanger(worker);
                    }
            }
            LockBtns();
            UpdtBtn_Click(null, null);
        }
    }
}
