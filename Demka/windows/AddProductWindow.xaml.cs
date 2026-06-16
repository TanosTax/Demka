using Demka.db;
using Demka.pages;
using Microsoft.Win32;
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
    /// Логика взаимодействия для AddProductWindow.xaml
    /// </summary>
    public partial class AddProductWindow : Window
    {
        public static List<Category> categories { get; set; }
        public static List<Manufacturer> manufacturers { get; set; }
        public static List<Supplier> suppliers { get; set; }
        public static List<Unit> units { get; set; }

        private string _selectedPhototPath = null;

        private ProductListAdminPage _parentPage;

        public AddProductWindow()
        {
            InitializeComponent();
            categories = new List<Category>(ConnectionString.dbContext.Category.ToList());
            manufacturers = new List<Manufacturer>(ConnectionString.dbContext.Manufacturer.ToList());
            suppliers = new List<Supplier>(ConnectionString.dbContext.Supplier.ToList());
            units = new List<Unit>(ConnectionString.dbContext.Unit.ToList());
            this.DataContext = this;
        }

        public AddProductWindow(ProductListAdminPage parentPage)
        {
            InitializeComponent();
            categories = new List<Category>(ConnectionString.dbContext.Category.ToList());
            manufacturers = new List<Manufacturer>(ConnectionString.dbContext.Manufacturer.ToList());
            suppliers = new List<Supplier>(ConnectionString.dbContext.Supplier.ToList());
            units = new List<Unit>(ConnectionString.dbContext.Unit.ToList());
            this.DataContext = this;
            _parentPage = parentPage; // Сохраняем ссылку
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            Tovar product = new Tovar();
            if (nameProdTb.Text != string.Empty && categoryCmb.SelectedItem != null
                && descriptionTb.Text != string.Empty && manufacturerCmb.SelectedItem != null
                && supplierCmb.SelectedItem != null && priceTb.Text != string.Empty
                && discountTb.Text != string.Empty && wshCountTb.Text != string.Empty
                && unitTb.SelectedItem != null)
            {
                product.name = nameProdTb.Text.Trim();
                product.categoryId = (categoryCmb.SelectedItem as Category).categoryId;
                product.description = descriptionTb.Text.Trim();
                product.manufacturerId = ((Manufacturer)manufacturerCmb.SelectedItem).manufacturerId;
                product.supplierId = ((Supplier)supplierCmb.SelectedItem).supplierId;
                product.price = Convert.ToInt32(priceTb.Text.Trim());
                product.discount = Convert.ToInt32(discountTb.Text.Trim());
                product.quantity = Convert.ToInt32(wshCountTb.Text.Trim());
                product.unitId = ((Unit)unitTb.SelectedItem).unitId; 
                
                if (!string.IsNullOrEmpty(_selectedPhototPath))
                {
                    product.image = SavePhoto(_selectedPhototPath);
                }
                else
                { product.image = null; }
                
                ConnectionString.dbContext.Tovar.Add(product);
                ConnectionString.dbContext.SaveChanges();

                _parentPage?.RefreshProductList();

                MessageBox.Show("успех");
                this.Close(); // Закрываем окно после сохранения
            }
            else
                MessageBox.Show("Заполните все поля");
        }


        private void ChoosePhotoBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Изображения|*.jpg;*jpeg;*png";
            openFileDialog.Title = "Выберите фото товара";

            if (openFileDialog.ShowDialog() == true)
            {
                _selectedPhototPath = openFileDialog.FileName;
                productImage.Source = new BitmapImage(new Uri(_selectedPhototPath));
            }
        }
        private string SavePhoto(string sourcePath)
        {

            string fullPath = System.IO.Path.GetFullPath(sourcePath);
            string fileName = System.IO.Path.GetFileName(fullPath);

            return $"/Resources/{fileName}";
        }
    }
}
