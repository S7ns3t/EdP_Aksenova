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

namespace EdP_Aksenova.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (txtUsername.Text == string.Empty || pwdPassword.Password == string.Empty)
            {
                MessageBox.Show("Заполнены не все поля для ввода");
                return;
            }


            var user = Entities.Instance.User.FirstOrDefault(u => u.Username == txtUsername.Text && u.Password == pwdPassword.Password);
            if (user != null)
            {
               MessageBox.Show("Авторизация успешна, переход на главную страницу");
               barmoley.IsUserLoggedIn = true;
               barmoley.UserLogin = user.Username;
            }

            NavigationService.GoBack();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
