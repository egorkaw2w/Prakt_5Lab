using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для OrderStatusPage.xaml
    /// </summary>
    public partial class OrderStatusPage : Page
    {
        Order_statusTableAdapter status = new Order_statusTableAdapter();
        public OrderStatusPage()
        {
            InitializeComponent();
            StatusGrid.ItemsSource = status.GetData();
        }
        private void StatusGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StatusGrid.SelectedItem != null)
            {
                var sel = (StatusGrid.SelectedItem as DataRowView);

                StatusNameArea.Text = sel[1].ToString();
            }
        }
        private void StatusNameArea_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox; string text = textBox.Text;
            foreach (char c in text)
            {
                if (!char.IsLetter(c))
                {
                    textBox.Text = text.Remove(text.Length - 1);
                    textBox.SelectionStart = textBox.Text.Length; return;
                }
            }
        }
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (StatusGrid.SelectedItem != null)
            {
                var sel = (StatusGrid.SelectedItem as DataRowView).Row[0];
                status.UpdateQuery(StatusNameArea.Text, (int)sel);
                StatusGrid.ItemsSource = status.GetData();
            }
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (StatusNameArea.Text != "")
            {

            var sel = (StatusGrid.SelectedItem as DataRowView);

            status.Insert(StatusNameArea.Text.ToString());
            StatusGrid.ItemsSource = status.GetData();
            }

        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (StatusGrid.SelectedItem != null)
            {
                var sel = (StatusGrid.SelectedItem as DataRowView);

                status.DeleteQuery((int)sel[0]);
                StatusGrid.ItemsSource = status.GetData();
            }


        }


    }
}
