using BLL.Concrete;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace WpfApp_classwork2712
{
    /// <summary>
    /// Interaction logic for AddRole.xaml
    /// </summary>
    public partial class AddRole : Window
    {
        private RoleManager _roleManager;
        public AddRole(RoleManager roleManager)
        {
            InitializeComponent();
            _roleManager = roleManager;
        }

        private void CancelAddRoleBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddNewRoleBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(RoleNameTxtBox.Text))
                if (_roleManager.Add(RoleNameTxtBox.Text) == 0)
                {
                    MessageBox.Show("Role exists", "Role exists", MessageBoxButton.OK);
                }
                else
                    MessageBox.Show("Role added successfull", "Role added", MessageBoxButton.OK);

            else
                MessageBox.Show("Wrong text in field", "Error", MessageBoxButton.OK);

        }
    }
}
