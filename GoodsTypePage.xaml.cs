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
    /// Логика взаимодействия для GoodsTypePage.xaml
    /// </summary>
    public partial class GoodsTypePage : Page
    {
        Good_TypeTableAdapter type = new Good_TypeTableAdapter();
        public GoodsTypePage()
        {
            InitializeComponent();
            TypeGrid.ItemsSource = type.GetData();
        }

        private void PostGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TypeGrid.SelectedItem != null)
            {
                var sel = (TypeGrid.SelectedItem as DataRowView);
                TypeNameArea.Text = sel[1].ToString();
            }

        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (TypeGrid.SelectedItems != null)
            {
                if (TypeNameArea.Text != "")
                {
                    var sel = (TypeGrid.SelectedItem as DataRowView);

                    type.UpdateQuery(TypeNameArea.Text, (int)sel[0]);
                    TypeGrid.ItemsSource = type.GetData();
                }
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (TypeNameArea.Text != null)
            {
                if (TypeNameArea.Text != "")
                {
                    type.Insert(TypeNameArea.Text);
                    TypeGrid.ItemsSource = type.GetData();
                }

            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (TypeGrid.SelectedItem != null)
                {
                    var sel = (TypeGrid.SelectedItem as DataRowView).Row[0];
                    type.DeleteQuery((int)sel);
                    TypeGrid.ItemsSource = type.GetData();

                }
            }
            catch
            {
                MessageBox.Show("Нельзя удалять, используемые в других таблицах, значения");
            }
        }

        private void TypeNameArea_TextChanged(object sender, TextChangedEventArgs e)
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
