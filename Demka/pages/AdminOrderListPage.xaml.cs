using Demka.db;
using Demka.windows;
using System;
using System.Collections.Generic;
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

namespace Demka.pages
{
    /// <summary>
    /// Логика взаимодействия для AdminOrderListPage.xaml
    /// </summary>
    public partial class AdminOrderListPage : Page
    {
        public static List<Order> orders {  get; set; }
        public AdminOrderListPage(User user)
        {
            InitializeComponent();
            orders = new List<Order>(ConnectionString.dbContext.Order.ToList());
            userBlock.Text = $"Администратор: {user.name} {user.surname} {user.otchestvo}";
            this.DataContext = this;
        }

        public void RefreshProductList()
        {
            orders = new List<Order>(ConnectionString.dbContext.Order.ToList());
            orderLV.ItemsSource = orders;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddOrderWindow addOrderWindow = new AddOrderWindow(this);
            addOrderWindow.Show();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var order = orderLV.SelectedItem as Order;
            if (order != null)
            {
                EditOrderWindow editOrderWindow = new EditOrderWindow(order, this);
                editOrderWindow.Show();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var sel = orderLV.SelectedItem as Tovar;
            if (sel != null)
            {
                ConnectionString.dbContext.Tovar.Remove(sel);
                ConnectionString.dbContext.SaveChanges();

                orders = new List<Order>(ConnectionString.dbContext.Order.ToList());
                orderLV.ItemsSource = orders;
            }
            else
            {
                MessageBox.Show("нечего удалять");
            }
        }
    }
}
