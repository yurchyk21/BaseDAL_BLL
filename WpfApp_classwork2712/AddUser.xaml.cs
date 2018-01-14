using BLL.Concrete;
using BLL.ViewModels;
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
    /// Interaction logic for AddUser.xaml
    /// </summary>
    public partial class AddUser : Window
    {
        public AddUser()
        {
            InitializeComponent();
        }
        private ShopWorkManager _userManager;
        public AddUser(ShopWorkManager userManager)
        {
            InitializeComponent();
           _userManager = userManager;
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddNewUserBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(UserFirstNameTxtBox.Text) && !string.IsNullOrEmpty(UserLastNameTxtBox.Text) && !string.IsNullOrEmpty(EmailTxtBox.Text) && !string.IsNullOrEmpty(PasswordTxtBox.Password))
            {
                if (_userManager.Add(new ShopWorkerAddViewModel() { FirstName = UserFirstNameTxtBox.Text, LastName = UserLastNameTxtBox.Text, Email = EmailTxtBox.Text, IsLocked = false, Password = PasswordTxtBox.Password }) == 0)
                {
                    MessageBox.Show("User exists", "User exists", MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show("User added successfull", "User added", MessageBoxButton.OK);
                }
                this.Close();
            }
            else
                MessageBox.Show("Wrong text in field", "Error", MessageBoxButton.OK);

        }
    }
}
