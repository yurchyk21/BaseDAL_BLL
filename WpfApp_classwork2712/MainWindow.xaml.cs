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
            //  datagrid.ItemsSource = _roleRepository.Roles();


            ShopWorkerAddViewModel shopWorker = new ShopWorkerAddViewModel
            {
                FirstName = "shopworkname",
                Email = "shopworkemail",
                LastName= "shopworksurname",
                Password = "123456",
                IsLocked = true


            };
            _shopWorkManager.Add(shopWorker);

        }

        private void AddRoleBtn_Click(object sender, RoutedEventArgs e)
        {
            AddRole _windowAddRole = new AddRole(_roleManager);
            _windowAddRole.Show();
        }
    }
}
