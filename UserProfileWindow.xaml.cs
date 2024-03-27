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
    /// Логика взаимодействия для UserProfileWindow.xaml
    /// </summary>
    public partial class UserProfileWindow : Page
    {
        User_ProfileTableAdapter userprof = new User_ProfileTableAdapter();
        Quality_roleTableAdapter quality_Role = new Quality_roleTableAdapter();
        public UserProfileWindow()
        {
            InitializeComponent();
            GridProfile.ItemsSource = userprof.GetData();
            QualityRoleArea.ItemsSource = quality_Role.GetData();
            QualityRoleArea.DisplayMemberPath = "Quality_name";
        }

        private void PostGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var sel = (GridProfile.SelectedItem as DataRowView);
            BalanceArea.Text = sel[1].ToString();
            ProfileLoginArea.Text = sel[2].ToString();
            PasswordArea.Text = sel[3].ToString();

            foreach (var item in quality_Role.GetData())
            {
                if (Convert.ToInt32((GridProfile.SelectedItem as DataRowView).Row[4].ToString()) == (int)item.Quality_name)
                {
                    foreach (var i in QualityRoleArea.Items)
                    {
                        if ((int)(i as DataRowView).Row[0] == item.Id_role)
                        {
                            QualityRoleArea.SelectedItem = i;
                        }
                    }
                }
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (GridProfile.SelectedItem != null)
            {


                if (BalanceArea.Text != ""
                    && ProfileLoginArea.Text != ""
                    && PasswordArea.Text != ""
                    && QualityRoleArea.SelectedItem != null
                    )
                {
                    var role = (QualityRoleArea.SelectedItem as DataRowView).Row[0];
                    var sel = GridProfile.SelectedItem as DataRowView;

                    userprof.UpdateQuery(Convert.ToDecimal(BalanceArea.Text),
                        ProfileLoginArea.Text,
                        PasswordArea.Text,
                        (int)role,
                        (int)sel[0]);
                    GridProfile.ItemsSource = userprof.GetData();

                }
            }

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (BalanceArea.Text != ""
                    && ProfileLoginArea.Text != ""
                    && PasswordArea.Text != ""
                    && QualityRoleArea.SelectedItem != null
                    )
            {
                var role = (QualityRoleArea.SelectedItem as DataRowView).Row[0];

                userprof.Insert(Convert.ToDecimal(BalanceArea.Text),
                    ProfileLoginArea.Text,
                    PasswordArea.Text,
                    Convert.ToInt32(role)
                    );
                GridProfile.ItemsSource = userprof.GetData();
            }
               
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if(GridProfile.SelectedItem != null)
            {
               
                    var sel = GridProfile.SelectedItem as DataRowView;
                    userprof.DeleteQuery((int)sel[0]);
                    GridProfile.ItemsSource = userprof.GetData();
                
         
            }

        }

        private void QualityRoleArea_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BalanceArea_TextChanged(object sender, TextChangedEventArgs e)
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

        private void ProfileLoginArea_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
