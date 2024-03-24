using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
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
using Prakt_5Lab.DataSet1TableAdapters;

namespace Prakt_5Lab
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PostTableAdapter post = new PostTableAdapter();
        EmployeeRoleTableAdapter employeeRole = new EmployeeRoleTableAdapter();
        User_ProfileTableAdapter userProfile = new User_ProfileTableAdapter();
        EmployeeTableAdapter employee = new EmployeeTableAdapter();

        public MainWindow()
        {
            InitializeComponent();
            LoginArea.Text = "admin";
            PassArea.Text = "admin";


        }


        private void Register_Click(object sender, RoutedEventArgs e)
        {

        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
           
            
                UserWindow window = new UserWindow();
                window.Show();
                Close();
            
           

        }




    private void LoginArea_GotFocus(object sender, RoutedEventArgs e)
    {
               PassArea.Text = "";
    }

    private void PassArea_GotFocus(object sender, RoutedEventArgs e)
    {
        PassArea.Text = "";
    }

}
}
