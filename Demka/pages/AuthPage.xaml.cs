using Demka.db;
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
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public static List<User> users {  get; set; }
        public AuthPage()
        {
            InitializeComponent();
        }

        private void btnAuth_Click(object sender, RoutedEventArgs e)
        {
            string login = loginBox.Text.Trim();
            string pass = passBox.Text.Trim();

            users = new List<User>(ConnectionString.dbContext.User.ToList());
            User currentUser = users.FirstOrDefault(i => i.login.Trim() == login && i.password.Trim() == pass);

            if (currentUser != null) 
            {
                MessageBox.Show("Бур мал, да?");
                
                if (currentUser.roleId == 1)
                {
                    NavigationService.Navigate(new ProductListAdminPage(currentUser));
                }
                if (currentUser.roleId == 2)
                {
                    NavigationService.Navigate(new ProductListManagerPage(currentUser));
                }
                if (currentUser.roleId == 3)
                {
                    NavigationService.Navigate(new ProductListPage(currentUser));
                }
            }
            else
            {
                MessageBox.Show("zapolni polya");
            }

        }
        private void btnAuthAsGuest_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProductListPage());
        }
    }
}
