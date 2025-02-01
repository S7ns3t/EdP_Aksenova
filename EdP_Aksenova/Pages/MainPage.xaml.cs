using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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
    static public class barmoley
    {
        static public bool IsUserLoggedIn = false;
        static public string UserLogin;
    }

    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            cmbCity.ItemsSource = Entities.Instance.City.ToList();
            cmbCategory.ItemsSource = Entities.Instance.Category.ToList();
            cmbType.ItemsSource = Entities.Instance.AdvertisementType.ToList();
            if (barmoley.IsUserLoggedIn) 
            { 
                btnAuth.Visibility = Visibility.Collapsed;
                btnViewUserAdverts.Visibility = Visibility.Visible;
            }
        }

        private void UpdateAdverts()
        {
            var filteredAdverts = Entities.Instance.Advertisement.ToList();

            if (!string.IsNullOrWhiteSpace(txtAdvertName.Text)) filteredAdverts = filteredAdverts.Where(x => x.Name.ToLower().Contains(txtAdvertName.Text.ToLower())).ToList();
            if (cmbCity.SelectedIndex != -1) filteredAdverts = filteredAdverts.Where(x => x.City == cmbCity.SelectedItem).ToList();
            if (cmbCategory.SelectedIndex != -1) filteredAdverts = filteredAdverts.Where(x => x.Category == cmbCategory.SelectedItem).ToList();
            if (cmbType.SelectedIndex != -1) filteredAdverts = filteredAdverts.Where(x => x.AdvertisementType == cmbType.SelectedItem).ToList();
            if (cmbStatus.SelectedIndex == 0) filteredAdverts = filteredAdverts.Where(x => !x.IsFinished).ToList();
            else if (cmbStatus.SelectedIndex == 1) filteredAdverts = filteredAdverts.Where(x => x.IsFinished).ToList();

            ListAdvertisements.ItemsSource = filteredAdverts;
        }

        private void cmbCity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateAdverts();
        }

        private void cmbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateAdverts();
        }

        private void cmbType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateAdverts();
        }

        private void cmbStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateAdverts();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UpdateAdverts();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            cmbCity.SelectedIndex = -1;
            cmbCategory.SelectedIndex = -1;
            cmbType.SelectedIndex = -1;
            cmbStatus.SelectedIndex = -1;
            txtAdvertName.Text = string.Empty;

            UpdateAdverts();
        }


        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthPage());
        }

        private void btnViewUserAdverts_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UserAdvertsPage());
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                Entities.Instance.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
                ListAdvertisements.ItemsSource = Entities.Instance.Advertisement.ToList();
            }
        }
    }
}
