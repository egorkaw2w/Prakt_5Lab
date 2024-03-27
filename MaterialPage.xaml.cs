using Prakt_5Lab.DataSet1TableAdapters;
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

namespace Prakt_5Lab
{
    /// <summary>
    /// Логика взаимодействия для MaterialPage.xaml
    /// </summary>
    public partial class MaterialPage : Page
    {
        MaterialTableAdapter material = new MaterialTableAdapter();

        public MaterialPage()
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
            if (materialNameArea.Text != "")
            {
                material.InsertQuery(materialNameArea.Text);
                MaterialGrid.ItemsSource = material.GetData();
            }
            

        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if(MaterialGrid.SelectedItem != null)
            {
                var sel = MaterialGrid.SelectedItem as DataRowView;
                material.UpdateQuery(materialNameArea.Text, (int)sel[0]);
                MaterialGrid.ItemsSource = material.GetData();

            }

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (MaterialGrid.SelectedItem != null)
            {

            var sel = MaterialGrid.SelectedItem as DataRowView;
            material.DeleteQuery((int)sel[0]);
            MaterialGrid.ItemsSource = material.GetData();

            }
        }

        private void MaterialGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MaterialGrid.SelectedItem != null)
            {
                var sel = MaterialGrid.SelectedItem as DataRowView;
                materialNameArea.Text = sel[1].ToString();
            }
        }
    }
}