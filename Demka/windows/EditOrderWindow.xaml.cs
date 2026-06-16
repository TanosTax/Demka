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
    /// Логика взаимодействия для EditOrderWindow.xaml
    /// </summary>
    public partial class EditOrderWindow : Window
    {
        Order order = new Order();
        public static List<Pvz> pvzs { get; set; }
        public static List<User> users { get; set; }
        public static List<Status> statuses { get; set; }

        public AdminOrderListPage _parentPage;
        public EditOrderWindow(Order order1, AdminOrderListPage parentPage)
        {
            InitializeComponent();
            pvzs = new List<Pvz>
                (ConnectionString.dbContext.Pvz.ToList());

            users = new List<User>
                (ConnectionString.dbContext.User.ToList());

            statuses = new List<Status>
                (ConnectionString.dbContext.Status.ToList());

            order = order1; //ОБЯЗАТЕЛЬНО!!!!!!!!!!!!!!!!!
            _parentPage = parentPage;

            dateStartCal.SelectedDate = order.dateStart;
            dateEndCal.SelectedDate = order.dateEnd;
            pzvComboBox.SelectedItem = order.pzvId;
            userComboBox.SelectedItem = order.userId;
            codeTxtBox.Text = Convert.ToString(order.code);
            statusComboBox.SelectedItem = order.statusId;
            

            pzvComboBox.SelectedItem = pvzs.
                FirstOrDefault(i => i.pvzId == order.pzvId);

            userComboBox.SelectedItem = users.
                FirstOrDefault(i => i.name == order.User.name);

            statusComboBox.SelectedItem = statuses.FirstOrDefault(i => i.name == order.Status.name);



            //codeTxtBox.SelectedItem = suppliers.
            //    FirstOrDefault(i => i.name == tovar.Supplier.name);

            //unitTb.SelectedItem = units.
            //    FirstOrDefault(i => i.name == tovar.Unit.name);

            this.DataContext = this;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
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


                ConnectionString.dbContext.SaveChanges();

                _parentPage.RefreshProductList();
                MessageBox.Show("успех");
                this.Close();
            }
            else
                MessageBox.Show("Заполните все поля");
        }
    }
}
