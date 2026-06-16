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
    public partial class EditProductWindow : Window
    {
        Tovar tovar = new Tovar();
        public static List<Category> categories { get; set; }
        public static List<Manufacturer> manufacturers { get; set; }
        public static List<Supplier> suppliers { get; set; }
        public static List<Unit> units { get; set; }
        private ProductListAdminPage _parentPage;
        public EditProductWindow(Tovar tovar1, ProductListAdminPage plap)
        {
            InitializeComponent();
            categories = new List<Category>
                (ConnectionString.dbContext.Category.ToList());

            manufacturers = new List<Manufacturer>
                (ConnectionString.dbContext.Manufacturer.ToList());

            suppliers = new List<Supplier>
                (ConnectionString.dbContext.Supplier.ToList());
            units = new List<Unit>(ConnectionString.dbContext.Unit.ToList());

            tovar = tovar1; //ОБЯЗАТЕЛЬНО!!!!!!!!!!!!!!!!!
            _parentPage = plap;

            tovarImg.Source = new BitmapImage(new Uri(tovar.image, UriKind.Relative));
            nameProdTb.Text = tovar.name;
            descriptionTb.Text = tovar.description;
            discountTb.Text = tovar.discount.ToString();
            priceTb.Text = tovar.price.ToString();
            wshCountTb.Text = tovar.quantity.ToString();

            categoryCmb.SelectedItem = categories.
                FirstOrDefault(i => i.name == tovar.Category.name);

            manufacturerCmb.SelectedItem = manufacturers.
                FirstOrDefault(i => i.name == tovar.Manufacturer.name);

            supplierCmb.SelectedItem = suppliers.
                FirstOrDefault(i => i.name == tovar.Supplier.name);

            unitTb.SelectedItem = units.
                FirstOrDefault(i => i.name == tovar.Unit.name);

            this.DataContext = this;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (tovarImg.Source != null && nameProdTb.Text != string.Empty && categoryCmb.SelectedItem != null
                && descriptionTb.Text != string.Empty && manufacturerCmb.SelectedItem != null
                && supplierCmb.SelectedItem != null && priceTb.Text != string.Empty
                && discountTb.Text != string.Empty && wshCountTb.Text != string.Empty
                && unitTb.SelectedItem != null)
            {

                tovar.image = tovarImg.Source.ToString();
                tovar.name = nameProdTb.Text.Trim();
                tovar.categoryId = (categoryCmb.SelectedItem as Category).categoryId;
                tovar.description = descriptionTb.Text.Trim();
                tovar.manufacturerId = ((Manufacturer)manufacturerCmb.SelectedItem).manufacturerId;
                tovar.supplierId = ((Supplier)supplierCmb.SelectedItem).supplierId;
                tovar.price = Convert.ToInt32(priceTb.Text.Trim());
                tovar.discount = Convert.ToInt32(discountTb.Text.Trim());
                tovar.quantity = Convert.ToInt32(wshCountTb.Text.Trim());
                tovar.unitId = ((Unit)unitTb.SelectedItem).unitId;

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
