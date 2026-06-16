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
    /// Логика взаимодействия для ProductListAdminPage.xaml
    /// </summary>
    public partial class ProductListAdminPage : Page
    {
        public static List<Tovar> tovar {  get; set; }
        public static List<Supplier> supplier { get; set; }
        public User currentuser;

        public ProductListAdminPage(User user)
        {
            InitializeComponent();
            tovar = new List<Tovar>(ConnectionString.dbContext.Tovar.ToList());
            supplier = new List<Supplier>(ConnectionString.dbContext.Supplier.ToList());
            currentuser = user;
            supplier.Insert(0, new Supplier { supplierId = 1, name = "Все поставщики" });
            userBlock.Text = $"Администратор: {user.name} {user.surname} {user.otchestvo}";
            this.DataContext = this;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void SortProductsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var SelectedItem = PriceSortProductsComboBox.SelectedItem as ComboBoxItem;
            if (SelectedItem == null)
            {
                return;
            }
            string selectedtext = SelectedItem.Content.ToString();
            if (selectedtext == "По возрастанию по на складе")
            {
                tovarLV.ItemsSource = tovar.OrderBy(i => i.quantity).ToList();
            }
            else if (selectedtext == "По убыванию по на складе")
            {
                tovarLV.ItemsSource = tovar.OrderByDescending(i => i.quantity).ToList();
            }
            else if (selectedtext == "По возрастанию по цене")
            {
                tovarLV.ItemsSource = tovar.OrderBy(i => i.price).ToList();
            }
            else if (selectedtext == "По убыванию по цене")
            {
                tovarLV.ItemsSource = tovar.OrderByDescending(i => i.price).ToList();
            }
            else
            {
                tovarLV.ItemsSource = tovar;
            }
        }

        private void SupplierCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var sup = SupplierCmb.SelectedItem as Supplier;
            if (sup.supplierId != -1)
                tovarLV.ItemsSource = tovar.Where(i => i.supplierId == sup.supplierId).ToList();
            else
                tovarLV.ItemsSource = tovar;
        }

        private void searchTovar_TextChanged(object sender, TextChangedEventArgs e)
        {
            tovarLV.ItemsSource = tovar.Where(i => i.name.ToLower().Contains(searchTovar.Text.ToLower())).ToList();
            //||
            //                                       i.articul.ToLower().Contains(searchTovar.Text.ToLower()) ||
            //                                       i.Manufacturer.name.ToLower().Contains(searchTovar.Text.ToLower())).ToList();
        }

        private void btnToOrdersPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminOrderListPage(currentuser));
        }


        public void RefreshProductList()
        {
            tovar = new List<Tovar>(ConnectionString.dbContext.Tovar.ToList());
            tovarLV.ItemsSource = tovar;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddProductWindow addProductWindow = new AddProductWindow(this);
            addProductWindow.Show();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var tovar = tovarLV.SelectedItem as Tovar;
            if (tovar != null)
            {
                EditProductWindow editProductWindow = new EditProductWindow(tovar, this);
                editProductWindow.Show();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var sel = tovarLV.SelectedItem as Tovar;
            if (sel != null)
            {
                ConnectionString.dbContext.Tovar.Remove(sel);
                ConnectionString.dbContext.SaveChanges();

                tovar = new List<Tovar>(ConnectionString.dbContext.Tovar.ToList());
                tovarLV.ItemsSource = tovar;
            }
            else
            {
                MessageBox.Show("нечего удалять");
            }
        }
    }
}
