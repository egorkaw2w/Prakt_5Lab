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
    /// Логика взаимодействия для PostPage.xaml
    /// </summary>
    public partial class PostPage : Page
    {
        PostTableAdapter post = new PostTableAdapter();
        public PostPage()
        {
            InitializeComponent();
            PostGrid.ItemsSource = post.GetData();
        }

        private void PostGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PostGrid.SelectedItem != null)
            {
                var sel = (PostGrid.SelectedItem as DataRowView);

                PostNameArea.Text = sel[1].ToString();
                SalaryArea.Text = sel[2].ToString();
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (PostNameArea.Text != "" && SalaryArea.Text != "")
            {
                post.Insert(PostNameArea.Text, Convert.ToDecimal(SalaryArea.Text));
                PostGrid.ItemsSource = post.GetData();
            }


        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (PostGrid.SelectedItem != null)
            {
                if (PostNameArea.Text != "" && SalaryArea.Text != "")
                {

                    var sel = (PostGrid.SelectedItem as DataRowView).Row[0];
                    post.UpdateQuery(PostNameArea.Text, Convert.ToDecimal(SalaryArea.Text), Convert.ToInt32(sel));
                    PostGrid.ItemsSource = post.GetData();
                }
                else
                {
                    MessageBox.Show("Введите значения!");
                }
            }
            else
            {
                MessageBox.Show("Выберите что меняете");
            }

        }


        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (PostGrid.SelectedItem != null)
            {
                var sel = (PostGrid.SelectedItem as DataRowView).Row[0];

                post.DeleteQuery((int)sel);
                PostGrid.ItemsSource = post.GetData();
            }


        }

        private void PostNameArea_TextChanged(object sender, TextChangedEventArgs e)
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

        private void SalaryArea_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox; string text = textBox.Text;
            foreach (char c in text)
            {
                if (!char.IsDigit(c))
                {
                    textBox.Text = text.Remove(text.Length - 1);
                    textBox.SelectionStart = textBox.Text.Length; return;
                }
            }
        }
    }
}
