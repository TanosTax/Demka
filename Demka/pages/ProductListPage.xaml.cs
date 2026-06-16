using Demka.db;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Demka.pages
{
    /// <summary>
    /// Логика взаимодействия для ProductListPage.xaml
    /// </summary>
    public partial class ProductListPage : Page
    {
        public static List<Tovar> tovar {  get; set; }
        public ProductListPage(User user)
        {
            InitializeComponent();
            tovar = new List<Tovar>(ConnectionString.dbContext.Tovar.ToList());
            userBlock.Text = $"Пользователь: {user.name} {user.surname} {user.otchestvo}";
            this.DataContext = this;
        }

        public ProductListPage()
        {
            InitializeComponent();
            tovar = new List<Tovar>(ConnectionString.dbContext.Tovar.ToList());
            userBlock.Text = $"Пользователь: Босс";
            this.DataContext = this;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
