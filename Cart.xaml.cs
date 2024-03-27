using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
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
    /// Логика взаимодействия для Cart.xaml
    /// </summary>
    public partial class Cart : Window
    {
        CartTableAdapter cart = new CartTableAdapter();
        CartInfoTableAdapter cartinfo = new CartInfoTableAdapter();
        CHECKITableAdapter checkit = new CHECKITableAdapter();
        int selected = 0;
        int totalcost = 0;
        int number;
        public Cart()
        {
            InitializeComponent();
            CartInfo.ItemsSource = cartinfo.GetData().ToList();
             
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            UserWindow window = new UserWindow();
            window.Show();
            Close();
        }

        private void Buy_Click(object sender, RoutedEventArgs e)
        {
            number = Convert.ToInt32(checkit.GetData().Id_CheckColumn.ToString().Last());
            int row = CartInfo.Items.Count;
            string text = "";
            number++;
            File.AppendAllText($"чек{number}.txt", "\nYandex Market \n");
            string line2 = "Чек #" + number + "\n";
            File.AppendAllText($"чек{number}.txt", line2);
            string line3 = "\n";
            for (int i = 0; i < row; i++)
            {
                var sel = cartinfo.GetData()[i];
                text = sel.Название_ + "\t-\t" + sel.Стоимость_ + "\n";
                File.AppendAllText($"чек{number}.txt", text);
                totalcost += Convert.ToInt32(sel.Стоимость_);
            }
            File.AppendAllText($"чек{number}.txt", "\t\tВсего: " + totalcost.ToString());
            checkit.Insert($"чек{number}.txt");
            totalcost = 0;
            cartinfo.DeleteQuery();
            CartInfo.ItemsSource = cartinfo.GetData();
        }

        private void CartInfo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
