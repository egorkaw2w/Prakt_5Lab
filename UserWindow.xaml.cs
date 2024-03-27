using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
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
    /// Логика взаимодействия для UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        GoodsTableAdapter goods = new GoodsTableAdapter();
        CartTableAdapter cart = new CartTableAdapter();
        MaterialTableAdapter material = new MaterialTableAdapter();
        GoodInfoTableAdapter good = new GoodInfoTableAdapter();
        DateTime time = DateTime.Now;
        List<string> eventList = new List<string>();
        bool isTradable;
        public UserWindow()
        {
            InitializeComponent();

            UserGrid.ItemsSource = good.GetData();
            ItemCount.ItemsSource = goods.GetData().Count.ToString();
            ItemCount.IsEnabled = false;

        }

        private void UserGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserGrid.Columns[0].Visibility = Visibility.Collapsed;

            if (UserGrid.SelectedItem != null)
            {
                ItemCount.IsEnabled = true;
                List<int> ints = new List<int>();

                var selected = (UserGrid.SelectedItem as DataRowView);

                int counter = Convert.ToInt32(selected[4].ToString());
                ItemName.Text = selected[1].ToString();
                ItemStatus.Text = selected[7].ToString();

                if (counter > 0)
                {
                    for (int i = 1; i <= counter; i++)
                    {
                        ints.Add(i);
                    }
                    ItemCount.ItemsSource = ints;
                    ItemCount.SelectedIndex = 0;
                    isTradable = true;
                }
                else
                {
                    eventList.Add("Товар закончился :(");
                    ItemCount.ItemsSource = eventList;
                    isTradable = false;
                }
                ItemCost.Text = selected[5].ToString();

            }
            else
            {
            }
        }

        private void ItemCount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*            var check = (UserGrid.SelectedItem as DataRowView).Row[3];
            */
            if (UserGrid.SelectedItem != null && int.TryParse(ItemCount.SelectedItem.ToString(), out int result))
            {
                ItemCost.Text = (Convert.ToInt32(ItemCount.SelectedItem) * Convert.ToInt32((UserGrid.SelectedItem as DataRowView).Row[5])).ToString();
            }

        }

        private void AddOrder_Click(object sender, RoutedEventArgs e)
        {
            string Time = time.ToString("f");

            if (UserGrid.SelectedItem != null)
            {
                var a = UserGrid.SelectedItem as DataRowView;
                if (UserGrid.SelectedItem != null && ItemCount.SelectedItem != null && isTradable == true)
                {
                    int counter = Convert.ToInt32(ItemCount.SelectedItem.ToString());
                    int First = Convert.ToInt32(a.Row[4]);
                    int result = First - counter;
                    string code = ((a.Row[10]).ToString());
                
                    if (UserGrid.SelectedItem != null)
                    {
                        foreach (var rowView in cart.GetData())
                        {

                            if (rowView.Item_id == (int)(a.Row[0]) && ((UserGrid.SelectedItem as DataRowView).Row[0]) != null)
                            {
                                goods.UpdateGoods(result, code);
                                cart.UpdateQuery(
                                    (int)((UserGrid.SelectedItem as DataRowView).Row[0]),
                                    (Time),
                                    Convert.ToInt32(rowView.Item_Count + counter),
                                    Convert.ToInt32(rowView.Item_Cost + Convert.ToDouble(ItemCost.Text)),
                                    Convert.ToInt32(rowView.Id_Cart));
                                UserGrid.ItemsSource = good.GetData();
                                UserGrid.Columns[0].Visibility = Visibility.Collapsed;

                                return;
                            }

                        }
                        if (UserGrid.SelectedItem != null)
                        {
                            goods.UpdateGoods(result, code);
                            UserGrid.ItemsSource = good.GetData();
                            MessageBox.Show("готово");
                            cart.Insert((int)a.Row[0], time.ToString("f"), counter, (int)Convert.ToDouble(ItemCost.Text));
                        }
                        else
                        {
                            MessageBox.Show("беда");
                        }
                    }
               
                }

                UserGrid.ItemsSource = good.GetData();
                UserGrid.Columns[0].Visibility = Visibility.Collapsed;
            }
        }

        private void Cart_Click(object sender, RoutedEventArgs e)
        {
            Cart window = new Cart();
            window.Show();
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UserGrid.Columns[0].Visibility = Visibility.Collapsed;
        }
    }
}
