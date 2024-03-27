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
    /// Логика взаимодействия для GoodsPage.xaml
    /// </summary>
    public partial class GoodsPage : Page
    {

        GoodInfoTableAdapter goodinfo = new GoodInfoTableAdapter();
        Good_TypeTableAdapter type = new Good_TypeTableAdapter();
        MaterialTableAdapter material = new MaterialTableAdapter();
        Order_statusTableAdapter status = new Order_statusTableAdapter();
        EmployeeTableAdapter employee = new EmployeeTableAdapter();
        GoodsTableAdapter goods = new GoodsTableAdapter();
        public GoodsPage()
        {
            InitializeComponent();

            SellerGrid.ItemsSource = goodinfo.GetData();
            TypeArea.ItemsSource = type.GetData();
            TypeArea.DisplayMemberPath = "Good_type_name";
            MaterialArea.ItemsSource = material.GetData();
            MaterialArea.DisplayMemberPath = "Material_name";
            StatusArea.ItemsSource = status.GetData();
            StatusArea.DisplayMemberPath = "Status_name";
            SellerName.ItemsSource = employee.GetData();
            SellerName.DisplayMemberPath = "Employee_name";
        }

        private void SellerGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SellerGrid.SelectedItem != null)
            {
                var selected = (SellerGrid.SelectedItem as DataRowView);

                NameArea.Text = selected[1].ToString();
                DescArea.Text = selected[2].ToString();

                CountArea.Text = selected[4].ToString();
                WeightArea.Text = selected[5].ToString();
                CostArea.Text = selected[6].ToString();


                SellerName.Text = selected[9].ToString();
                OrderCode.Text = selected[10].ToString();



                foreach (var item in type.GetData())
                {
                    if ((SellerGrid.SelectedItem as DataRowView).Row[3].ToString() == item.Good_type_name)
                    {
                        foreach (var i in TypeArea.Items)
                        {

                            if ((int)(i as DataRowView).Row[0] == item.Id_Type)
                            {
                                TypeArea.SelectedItem = i;
                            }
                        }
                    }
                }
                foreach (var item in material.GetData())
                {
                    if ((SellerGrid.SelectedItem as DataRowView).Row[7].ToString() == item.Material_name)
                    {
                        foreach (var i in MaterialArea.Items)
                        {

                            if ((int)(i as DataRowView).Row[0] == item.Id_material)
                            {
                                MaterialArea.SelectedItem = i;
                            }
                        }
                    }
                }
                foreach (var item in status.GetData())
                {
                    if ((SellerGrid.SelectedItem as DataRowView).Row[8].ToString() == item.Status_name)
                    {
                        foreach (var i in StatusArea.Items)
                        {

                            if ((int)(i as DataRowView).Row[0] == item.Id_order)
                            {
                                StatusArea.SelectedItem = i;
                            }
                        }
                    }
                }
                foreach (var item in employee.GetData())
                {
                    if ((SellerGrid.SelectedItem as DataRowView).Row[9].ToString() == item.Employee_name)
                    {
                        foreach (var i in SellerName.Items)
                        {
                            if ((int)(i as DataRowView).Row[0] == item.Id_employee)
                            {
                                SellerName.SelectedItem = i;
                            }
                        }
                    }
                }

            }



        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SellerGrid.Columns[0].Visibility = Visibility.Collapsed;
        }

        private void Updater_Click(object sender, RoutedEventArgs e)
        {
            SellerGrid.Columns[0].Visibility = Visibility.Collapsed;

            if (SellerGrid.SelectedItem != null)
            {
                var selected = (SellerGrid.SelectedItem as DataRowView).Row[0];
                goods.FullUpdate(NameArea.Text, DescArea.Text, Convert.ToInt32(WeightArea.Text), Convert.ToDecimal(CostArea.Text), Convert.ToInt32(CountArea.Text), OrderCode.Text, Convert.ToInt32(selected));

            }
            SellerGrid.ItemsSource = goodinfo.GetData();
            SellerGrid.Columns[0].Visibility = Visibility.Collapsed;

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if ( SellerGrid.SelectedItem != null)
            {
                if (NameArea.Text != ""
                    && DescArea.Text != ""
                    && TypeArea.SelectedItem != null
                    && WeightArea.Text != ""
                    && CostArea.Text != ""
                    && CountArea.Text != ""
                    && MaterialArea.SelectedItem != null
                    && StatusArea.SelectedItem != null
                    && SellerName.SelectedIndex != null
                    )
                {



                    var typeid = (TypeArea.SelectedItem as DataRowView).Row[0];
                    var materialid = (MaterialArea.SelectedItem as DataRowView).Row[0];
                    var statusid = (StatusArea.SelectedItem as DataRowView).Row[0];
                    var sellername = (SellerName.SelectedItem as DataRowView).Row[0];

                    goods.InsertQuery(NameArea.Text,
                        DescArea.Text,
                        (int)typeid,
                        Convert.ToInt32(WeightArea.Text),
                        Convert.ToDecimal(CostArea.Text),
                        Convert.ToInt32(CountArea.Text),
                        Convert.ToInt32(materialid),
                        Convert.ToInt32(statusid),
                        Convert.ToInt32(sellername),
                        OrderCode.Text);

                    /*       catch
                           {
                               MessageBox.Show("Мимо");
                           }*/
                    SellerGrid.ItemsSource = goodinfo.GetData();
                    SellerGrid.Columns[0].Visibility = Visibility.Collapsed;
                }
            }
        }

        private void OrderCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(OrderCode.Text, "[^0-9]"))
            {

                OrderCode.Text = OrderCode.Text.Remove(OrderCode.Text.Length - 1);
            }
        }

        private void SellerName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Deleter_Click(object sender, RoutedEventArgs e)
        {
            if (SellerGrid.SelectedItem != null)
            {

                goods.DeleteQuery(Convert.ToInt32((SellerGrid.SelectedItem as DataRowView)[0]));
                SellerGrid.ItemsSource = goodinfo.GetData();
                SellerGrid.Columns[0].Visibility = Visibility.Collapsed;

            }
        }

        private void CountArea_TextChanged(object sender, TextChangedEventArgs e)
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

        private void WeightArea_TextChanged(object sender, TextChangedEventArgs e)
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

        private void CostArea_TextChanged(object sender, TextChangedEventArgs e)
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

        private void OrderCode_TextChanged(object sender, TextChangedEventArgs e)
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