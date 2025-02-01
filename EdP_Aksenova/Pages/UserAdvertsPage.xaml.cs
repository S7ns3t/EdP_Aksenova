using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Логика взаимодействия для UserAdvertsPage.xaml
    /// </summary>
    public partial class UserAdvertsPage : Page
    {
        public UserAdvertsPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CreateOrEditAdvertPage(null));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                Entities.Instance.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
                ListAdvertisements.ItemsSource = Entities.Instance.Advertisement.Where(a => a.Vendor == barmoley.UserLogin).ToList();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CreateOrEditAdvertPage((sender as Button).DataContext as Advertisement));
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var advertsForRemoving = ListAdvertisements.SelectedItems.Cast<Advertisement>().ToList();
            try
            {
                Entities.Instance.Advertisement.RemoveRange(advertsForRemoving);
                Entities.Instance.SaveChanges();
                ListAdvertisements.ItemsSource = Entities.Instance.Advertisement.Where(a => a.Vendor == barmoley.UserLogin).ToList();
                MessageBox.Show("Объявления успешно удалены!");
            }
            catch { MessageBox.Show("Ошибка при удалении данных! Помолитесь господу..."); }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var finishedAdverts = Entities.Instance.Advertisement.Where(a => a.Vendor == barmoley.UserLogin && a.IsFinished);
            ListAdvertisements.ItemsSource = finishedAdverts.ToList();
            txtRevenue.Text = "Выручка: " + finishedAdverts.Sum(x => x.Cost).ToString();
            txtRevenue.Visibility = Visibility.Visible;
        }

        private void chkShowFinished_Unchecked(object sender, RoutedEventArgs e)
        {
            ListAdvertisements.ItemsSource = Entities.Instance.Advertisement.Where(a => a.Vendor == barmoley.UserLogin).ToList();
            txtRevenue.Visibility = Visibility.Hidden;
        }
    }
}
