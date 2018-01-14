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
    /// Interaction logic for SetWorkerWindow.xaml
    /// </summary>
    public partial class SetWorkerWindow : Window
    {
        DateTime? _dateTime;
        bool? _locked;
        public SetWorkerWindow(DateTime date, bool locked)
        {
            InitializeComponent();
            _dateTime = date;
            _locked = locked;
        }


        private void DatePick_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            SetBtn.IsEnabled = true;
            IsLockedChecker.IsEnabled = true;
        }

        private void SetBtn_Click(object sender, RoutedEventArgs e)
        {
         if(DatePick.SelectedDate != null)
            {
                _dateTime = DatePick.SelectedDate;
                if (IsLockedChecker.IsChecked != null)
                {
                    _locked = IsLockedChecker.IsChecked;
                }
                else
                {
                    _locked = false;
                }
                DialogResult = true;
                this.Close();
            }   
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }
    }
}
