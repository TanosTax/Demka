using Demka.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    /// Логика взаимодействия для ManagerOrderListPage.xaml
    /// </summary>
    public partial class ManagerOrderListPage : Page
    {
        public static List<Order> orders {  get; set; }
        public ManagerOrderListPage(User user)
        {
            InitializeComponent();
            orders = new List<Order>(ConnectionString.dbContext.Order.ToList());
            userBlock.Text = $"Менеджер: {user.name} {user.surname} {user.otchestvo}";
            this.DataContext = this;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
