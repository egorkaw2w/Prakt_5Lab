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
using System.Windows.Shapes;
using Prakt_5Lab.DataSet1TableAdapters;

namespace Prakt_5Lab
{
    /// <summary>
    /// Логика взаимодействия для UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        GoodInfoTableAdapter good = new GoodInfoTableAdapter();
        GoodsTableAdapter goods = new GoodsTableAdapter();
        CartTableAdapter cart = new CartTableAdapter();
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
            if (UserGrid.SelectedItem != null)
            {
                ItemCount.IsEnabled = true;
                List<int> ints = new List<int>();
                var selected = (UserGrid.SelectedItem as DataRowView);
                int counter = Convert.ToInt32(selected[3].ToString());
                ItemName.Text = selected[0].ToString();
                ItemCost.Text = selected[4].ToString();
                ItemStatus.Text = selected[6].ToString();

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
                ItemCost.Text = (Convert.ToInt32(ItemCount.SelectedItem) * Convert.ToInt32((UserGrid.SelectedItem as DataRowView).Row[4])).ToString();
            }

        }

        private void AddOrder_Click(object sender, RoutedEventArgs e)
        {
            if (UserGrid.SelectedItem != null && ItemCount.SelectedItem != null && isTradable == true)
            {
                int counter = Convert.ToInt32(ItemCount.SelectedItem.ToString());
                int First = Convert.ToInt32((UserGrid.SelectedItem as DataRowView).Row[3]);
                int result = First - counter;
                string code = (UserGrid.SelectedItem as DataRowView).Row[8].ToString();
                good.UpdateQuery(result, code);
                MessageBox.Show(result.ToString());

                foreach (var rowView in cart.GetData())
                {
                    if (rowView.Order_Code == ((UserGrid.SelectedItem as DataRowView).Row[8]).ToString())
                    {
                        MessageBox.Show()
                        cart.UpdateQuery(
                            (rowView.Item_count + counter * Convert.ToInt32((UserGrid.SelectedItem as DataRowView).Row[4].ToString())),
                            (rowView.Item_count) + counter,
                            time.ToString("f"),
                            ((UserGrid.SelectedItem as DataRowView).Row[8]).ToString());
                        return;
/*                        cart.Update(ItemName.Text, Convert.ToDecimal(ItemCost.Text), Convert.ToInt32(ItemCount.SelectedItem), time.ToString("f"), ((UserGrid.SelectedItem as DataRowView).Row[8]).ToString());
*/
                    }
                }
                cart.Insert(ItemName.Text, Convert.ToDecimal(ItemCost.Text), Convert.ToInt32(ItemCount.SelectedItem), time.ToString("f"), ((UserGrid.SelectedItem as DataRowView).Row[8]).ToString());
                UserGrid.ItemsSource = good.GetData();
            }
        }

        private void Cart_Click(object sender, RoutedEventArgs e)
        {
            Cart window = new Cart();
            window.Show();
            Close();
        }
    }
}
