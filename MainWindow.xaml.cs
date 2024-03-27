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
         /*   LoginArea.Text = "admin";
            PassArea.Password = "admin";*/

        }


        private void Register_Click(object sender, RoutedEventArgs e)
        {

        }

        private void login_Click(object sender, RoutedEventArgs e)
        {

            var allLogins = userProfile.GetData().Rows;
            for (int i = 0; i < allLogins.Count; i++)
            {
                if (allLogins[i][2].ToString() == LoginArea.Text &&
                    allLogins[i][3].ToString() == PassArea.Password)
                {
                    int roleId = (int)allLogins[i][4];
                    switch (roleId)
                    {
                        case 1:
                            SellerWindow sellerWindow = new SellerWindow();
                            sellerWindow.Show();
                            Close();
                            return;
                            break;
                        case 2:
                            UserWindow userWindow = new UserWindow();
                            userWindow.Show();
                            Close();
                            return;
                            break;
                        case 3:
                            AdminWindow admin = new AdminWindow();
                            admin.Show();
                            Close();
                            return;
                            break;
                    }
                }
            }


            MessageBox.Show("Неверный логин или пароль");
            return;


        }




        private void LoginArea_GotFocus(object sender, RoutedEventArgs e)
        {
            LoginArea.Text = "";
        }

        private void PassArea_GotFocus(object sender, RoutedEventArgs e)
        {

        }

    }
}
