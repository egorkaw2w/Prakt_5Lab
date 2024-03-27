using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
    /// Логика взаимодействия для EmployeePage.xaml
    /// </summary>
    public partial class EmployeePage : Page
    {
        EmployeeTableAdapter employee = new EmployeeTableAdapter();
        PostTableAdapter post = new PostTableAdapter();
        PasportTableAdapter pasport = new PasportTableAdapter();
        RateTableAdapter rate = new RateTableAdapter();
        User_ProfileTableAdapter profile = new User_ProfileTableAdapter();
        public EmployeePage()
        {
            InitializeComponent();
            PostGrid.ItemsSource = employee.GetData();

            EmpPost.ItemsSource = post.GetData();
            EmpPost.DisplayMemberPath = "Post_name";
            pasportId.ItemsSource = pasport.GetData();
            pasportId.DisplayMemberPath = "Number";
            EmpRate.ItemsSource = rate.GetData();
            EmpRate.DisplayMemberPath = "Rate_name";
            EmpProfile.ItemsSource = profile.GetData();
            EmpProfile.DisplayMemberPath = "Profile_login";
        } 

        private void PostGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PostGrid.SelectedItem != null)
            {
                var sel = (PostGrid.SelectedItem as DataRowView);

                EmpName.Text = sel[1].ToString();
                EmpSurname.Text = sel[2].ToString();
                EmpMiddle.Text = sel[3].ToString();
                EmpAge.Text = sel[4].ToString();
                EmpPost.SelectedItem = sel[5].ToString();
                pasportId.SelectedItem = sel[6].ToString();
                EmpRate.SelectedItem = sel[7].ToString();
                EmpProfile.SelectedItem = sel[8].ToString();

                foreach (var item in post.GetData())
                {
                    if ((PostGrid.SelectedItem as DataRowView).Row[5].ToString() == item.Post_name)
                    {
                        foreach (var i in EmpPost.Items)
                        {
                            if ((int)(i as DataRowView).Row[0] == item.Id_Post)
                            {
                                EmpPost.SelectedItem = i;
                            }
                        }
                    }
                }
                foreach (var item in pasport.GetData())
                {
                    if (Convert.ToInt32((PostGrid.SelectedItem as DataRowView).Row[6].ToString()) == item.Number)
                    {
                        foreach (var i in pasportId.Items)
                        {
                            if ((int)(i as DataRowView).Row[0] == item.Id_pasp)
                            {
                                pasportId.SelectedItem = i;
                            }
                        }
                    }
                }
                foreach (var item in rate.GetData())
                {
                    if ((PostGrid.SelectedItem as DataRowView).Row[7].ToString() == item.Rate_name)
                    {
                        foreach (var i in EmpRate.Items)
                        {
                            if ((int)(i as DataRowView).Row[0] == item.Id_rate)
                            {
                                EmpRate.SelectedItem = i;
                            }
                        }
                    }
                }
                foreach (var item in profile.GetData())
                {
                    if ((PostGrid.SelectedItem as DataRowView).Row[8].ToString() == item.Profile_login)
                    {
                        foreach (var i in EmpProfile.Items)
                        {
                            if ((int)(i as DataRowView).Row[0] == item.Id_profile)
                            {
                                EmpProfile.SelectedItem = i;
                            }
                        }
                    }
                }
            }
        }
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (PostGrid.SelectedItem != null)
            {
                if (EmpName.Text != "" 
                    && EmpSurname.Text != ""
                    && EmpMiddle.Text != "" 
                    && EmpPost.SelectedItem != null
                    && EmpProfile.SelectedItem != null
                    && EmpRate.SelectedItem != null
                    && pasportId.SelectedItem != null) 

                {
                    var postid = (EmpPost.SelectedItem as DataRowView).Row[0];
                    var pasportid = (pasportId.SelectedItem as DataRowView).Row[0];
                    var rateid = (EmpRate.SelectedItem as DataRowView).Row[0];
                    var profid = (EmpProfile.SelectedItem as DataRowView).Row[0];
                    var sel = (PostGrid.SelectedItem as DataRowView).Row[0];
                    employee.UpdateQuery(
                        EmpName.Text,
                        EmpSurname.Text,
                        EmpMiddle.Text,
                        int.Parse(EmpAge.Text.ToString()),
                        int.Parse(postid.ToString()),
                        int.Parse(pasportid.ToString()),
                        int.Parse(rateid.ToString()),
                        int.Parse(profid.ToString()),
                        int.Parse(sel.ToString())
                        );
                }
                else
                {
                    MessageBox.Show("Выберите что меняете");
                }
            }

        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (EmpName.Text != ""
                   && EmpSurname.Text != ""
                   && EmpMiddle.Text != ""
                   && EmpPost.SelectedItem != null
                   && EmpProfile.SelectedItem != null
                   && EmpRate.SelectedItem != null
                   && pasportId.SelectedItem != null)

            {
                var postid = (EmpPost.SelectedItem as DataRowView).Row[0];
                var pasportid = (pasportId.SelectedItem as DataRowView).Row[0];
                var rateid = (EmpRate.SelectedItem as DataRowView).Row[0];
                var profid = (EmpProfile.SelectedItem as DataRowView).Row[0];
                employee.Insert(EmpName.Text, EmpSurname.Text, EmpMiddle.Text, Convert.ToInt32(EmpAge.Text), (int)postid, (int)pasportid, (int)rateid, (int)profid);
                PostGrid.ItemsSource = employee.GetData();
            }
            else
            {
                MessageBox.Show("Заполните поля");
            }

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (PostGrid.SelectedItem != null)
            {
                employee.DeleteQuery(Convert.ToInt32((PostGrid.SelectedItem as DataRowView)[0]));
                PostGrid.ItemsSource = employee.GetData();
            }
            else
            {
                MessageBox.Show("Выберите что удаляете");
            }
            
        }

        private void EmpRate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ToPasport_Click(object sender, RoutedEventArgs e)
        {
            
            PasportWindow window = new PasportWindow(); 
            window.Show();
            
            
        }

        private void EmpName_TextChanged(object sender, TextChangedEventArgs e)
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

        private void EmpSurname_TextChanged(object sender, TextChangedEventArgs e)
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

        private void EmpMiddle_TextChanged(object sender, TextChangedEventArgs e)
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

        private void EmpAge_TextChanged(object sender, TextChangedEventArgs e)
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
