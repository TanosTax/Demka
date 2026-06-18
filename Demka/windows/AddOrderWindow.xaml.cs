using Demka.db;
using Demka.pages;
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
using System.Windows.Shapes;

namespace Demka.windows
{
    /// <summary>
    /// Логика взаимодействия для AddOrderWindow.xaml
    /// </summary>
    public partial class AddOrderWindow : Window
    {
        public static List<Pvz> pvzs {  get; set; }
        public static List<User> users {  get; set; }
        public static List<Status> statuses {  get; set; }

        public AdminOrderListPage _parentPage;
        public AddOrderWindow()
        {
            InitializeComponent();
            pvzs = new List<Pvz>(ConnectionString.dbContext.Pvz.ToList());
            users = new List<User>(ConnectionString.dbContext.User.ToList());
            statuses = new List<Status>(ConnectionString.dbContext.Status.ToList());
            this.DataContext = this;
        }

        public AddOrderWindow(AdminOrderListPage parentPage)
        {
            InitializeComponent();
            pvzs = new List<Pvz>(ConnectionString.dbContext.Pvz.ToList());
            users = new List<User>(ConnectionString.dbContext.User.ToList());
            statuses = new List<Status>(ConnectionString.dbContext.Status.ToList());
            this.DataContext = this;
            _parentPage = parentPage;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            Order order = new Order();
            if (dateStartCal.SelectedDate != null && dateEndCal.SelectedDate != null
                && pzvComboBox.Text != null && statusComboBox.Text != null
                && codeTxtBox.Text != null && userComboBox.Text != null)
            {
                order.dateStart = dateStartCal.SelectedDate;
                order.dateEnd = dateEndCal.SelectedDate;
                order.pzvId = ((Pvz)pzvComboBox.SelectedItem).pvzId;
                order.userId = ((User)userComboBox.SelectedItem).userId;
                order.code = Convert.ToInt32(codeTxtBox.Text);
                order.statusId = ((Status)statusComboBox.SelectedItem).statusId;


                ConnectionString.dbContext.Order.Add(order);
                ConnectionString.dbContext.SaveChanges();
                
                _parentPage?.RefreshProductList();

                MessageBox.Show("успех");
                Close(); // Закрываем окно после сохранения
            }
            else
                MessageBox.Show("Заполните все поля");
        }

    }
}
