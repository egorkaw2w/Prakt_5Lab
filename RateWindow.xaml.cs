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
    /// Логика взаимодействия для RateWindow.xaml
    /// </summary>
    public partial class RateWindow : Page
    {
        RateTableAdapter rate = new RateTableAdapter();
        public RateWindow()
        {
            InitializeComponent();
            RateGrid.ItemsSource = rate.GetData();
        }

        private void PostGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RateGrid.SelectedItem != null)
            {
                var sel = (RateGrid.SelectedItem as DataRowView);
                RateNameArea.Text = sel[1].ToString();
                RateDescArea.Text = sel[2].ToString();
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (RateGrid.SelectedItem != null)
            {
                var sel = (RateGrid.SelectedItem as DataRowView);

                rate.UpdateQuery(RateNameArea.Text, RateDescArea.Text, (int)sel[0]);
                RateGrid.ItemsSource = rate.GetData();

            }
            else
            {
                MessageBox.Show("Выберите что меняете");
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (RateNameArea.Text != ""
                &&RateDescArea.Text !="")
            {
                rate.Insert(RateNameArea.Text, RateDescArea.Text);
                RateGrid.ItemsSource = rate.GetData();
            }
            else
            {
                MessageBox.Show("Заполните поля");
            }
          
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (RateGrid.SelectedItem != null)
            {
                var sel = (RateGrid.SelectedItem as DataRowView);
                rate.DeleteQuery((int)sel[0]);
                RateGrid.ItemsSource = rate.GetData();
            }
            else
            {
                MessageBox.Show("Выберите что удаляте");
            }
        }

        private void RateNameArea_TextChanged(object sender, TextChangedEventArgs e)
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
    }
}
