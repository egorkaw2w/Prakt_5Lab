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
    /// Логика взаимодействия для MaterialWIndow.xaml
    /// </summary>
    public partial class MaterialWIndow : Window
    {
        MaterialTableAdapter material = new MaterialTableAdapter();
        public MaterialWIndow()
        {
            InitializeComponent();
            MaterialGrid.ItemsSource = material.GetData();

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MaterialGrid.Columns[0].Visibility = Visibility.Collapsed;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (materialNameArea.Text!="")
            {
                material.InsertQuery(materialNameArea.Text);
                MaterialGrid.ItemsSource = material.GetData();
            }
            

        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            SellerWindow sellerWindow = new SellerWindow();
            sellerWindow.Show();
            Close();    
        }
    }
}
