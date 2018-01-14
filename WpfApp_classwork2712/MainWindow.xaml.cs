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

        private readonly SqlConnection _con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        private ShopWorkManager _shopWorkManager;
        private RoleManager _roleManager;

        public MainWindow()
        {
            _con.Open();
            _roleManager = new RoleManager(_con);
            _shopWorkManager = new ShopWorkManager(_con);

            InitializeComponent();
            datagrid.ItemsSource = _shopWorkManager.Users;
            datagrid.AutoGenerateColumns = false;
        }

        private void AddRoleBtn_Click(object sender, RoutedEventArgs e)
        {
            AddRole _windowAddRole = new AddRole(_roleManager);
            _windowAddRole.Show();
        }

        private void AddUserBtn_Click(object sender, RoutedEventArgs e)
        {
            AddUser _windowAddUser = new AddUser(_shopWorkManager);
            _windowAddUser.Show();
        }

        private void DelUserBtn_Click(object sender, RoutedEventArgs e)
        {
            if (datagrid.SelectedIndex != 0) {
                foreach(var user in datagrid.SelectedItems)
                {
                    _shopWorkManager.Delete(user);
                }
                    }
        }
    }
}
