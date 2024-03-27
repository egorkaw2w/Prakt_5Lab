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
using static System.Net.Mime.MediaTypeNames;

namespace Prakt_5Lab
{
    public partial class CLientPage : Page
    {
        ClientTableAdapter client = new ClientTableAdapter();
        User_ProfileTableAdapter profile = new User_ProfileTableAdapter();
        ClientViewTableAdapter clientview = new ClientViewTableAdapter();
        public CLientPage()
        {
            InitializeComponent();
            ClientGrid.ItemsSource = client.GetData();
            ProfileArea.ItemsSource = profile.GetData();
            ProfileArea.DisplayMemberPath = "Profile_login";
        }
        private void PostGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ClientGrid.SelectedItem != null)
            {
                var sel = (ClientGrid.SelectedItem as DataRowView);

                NameArea.Text = sel[1].ToString();
                SurnameArea.Text = sel[2].ToString();
                MiddleNameArea.Text = sel[3].ToString();

                foreach (var item in profile.GetData())
                {
                    if ((ClientGrid.SelectedItem as DataRowView).Row[4].ToString() == item.Profile_login)
                    {
                        foreach (var i in ProfileArea.Items)
                        {
                            if ((int)(i as DataRowView).Row[0] == item.Id_profile)
                            {
                                ProfileArea.SelectedItem = i;
                            }
                        }
                    }
                }
            }

        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (ClientGrid.SelectedItem != null)
            {
                if (NameArea.Text != ""
                  && SurnameArea.Text != ""
                  && MiddleNameArea.Text != ""
                  && ProfileArea.SelectedItem != null
                  )
                {
                    var sel = (ClientGrid.SelectedItem as DataRowView);
                    var profid = (ProfileArea.SelectedItem as DataRowView).Row[0];

                    client.UpdateQuery(NameArea.Text, SurnameArea.Text, MiddleNameArea.Text, (int)profid, (int)sel[0]);
                    ClientGrid.ItemsSource = client.GetData();
                }

            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (ClientGrid.SelectedItem != null)
            {

                if (NameArea.Text != ""
                    && SurnameArea.Text != ""
                    && MiddleNameArea.Text != ""
                    && ProfileArea.SelectedItem != null
                    )
                {
                    var profid = (ProfileArea.SelectedItem as DataRowView).Row[0];
                    client.Insert(NameArea.Text, SurnameArea.Text, MiddleNameArea.Text, (int)profid);
                    ClientGrid.ItemsSource = client.GetData();
                }

            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (ClientGrid.SelectedItem != null)
            {

                var sel = (ClientGrid.SelectedItem as DataRowView).Row[0];
                client.DeleteQuery((int)sel);
                ClientGrid.ItemsSource = client.GetData();
            }

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void NameArea_TextChanged(object sender, TextChangedEventArgs e)
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

        private void SurnameArea_TextChanged(object sender, TextChangedEventArgs e)
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

        private void MiddleNameArea_TextChanged(object sender, TextChangedEventArgs e)
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
