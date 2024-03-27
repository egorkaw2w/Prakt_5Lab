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
using Prakt_5Lab.DataSet1TableAdapters;

namespace Prakt_5Lab
{
    /// <summary>
    /// Логика взаимодействия для ElseWindow.xaml
    /// </summary>
    public partial class ElseWindow : Window
    {
        QueriesTableAdapter querry = new QueriesTableAdapter();
        public ElseWindow()
        {
            InitializeComponent();
            
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow admin = new AdminWindow();
            admin.Show();
            Close();
        }

        private void PostButton_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow admin = new AdminWindow();
            admin.Show();
            admin.adminFrame.Content = new PostPage();
            Close();
        }

        private void EmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow admin = new AdminWindow();
            admin.Show();
            admin.adminFrame.Content = new EmployeePage();
            Close();
        }

        private void RateButton_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow admin = new AdminWindow();
            admin.Show();
            admin.adminFrame.Content = new RateWindow();
            Close();
        }

        private void UserProfileButton_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow admin = new AdminWindow();
            admin.Show();
            admin.adminFrame.Content = new UserProfileWindow();
            Close();
        }
    

        private void ClientButton_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow admin = new AdminWindow();
            admin.Show();
            admin.adminFrame.Content = new CLientPage();
            Close();
        }

        private void OrderStatusButton_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow admin = new AdminWindow();
            admin.Show();
            admin.adminFrame.Content = new OrderStatusPage();
            Close();
        }

        private void GoodsButton_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow admin = new AdminWindow();
            admin.Show();
            admin.adminFrame.Content = new GoodsPage();
            Close();
        }

        private void GoodType_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow admin = new AdminWindow();
            admin.Show();
            admin.adminFrame.Content = new GoodsTypePage();
            Close();
        }

        private void materialButton_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow admin = new AdminWindow();
            admin.Show();
            admin.adminFrame.Content = new MaterialPage();
            Close();
        }

        private void BackUp_Click(object sender, RoutedEventArgs e)
        {
            querry.BackupYandexMarketDatabase();
            MessageBox.Show("Бэкап успешно создан");
        }
    }
}
