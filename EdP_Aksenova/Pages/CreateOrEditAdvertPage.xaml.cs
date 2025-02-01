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
    /// Логика взаимодействия для CreateOrEditAdvertPage.xaml
    /// </summary>
    public partial class CreateOrEditAdvertPage : Page
    {
        private Advertisement _currentAdvertisement = new Advertisement();

        public CreateOrEditAdvertPage(Advertisement selectedAdvertisement)
        {
            InitializeComponent();
            DataContext = selectedAdvertisement ?? _currentAdvertisement;

            cmbCity.ItemsSource = Entities.Instance.City.ToList();
            cmbCategory.ItemsSource = Entities.Instance.Category.ToList();
            cmbType.ItemsSource = Entities.Instance.AdvertisementType.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (cmbCity.SelectedIndex == -1 || cmbType.SelectedIndex == -1 || cmbCategory.SelectedIndex == -1 ||
                string.IsNullOrWhiteSpace(txtAdvertName.Text) || string.IsNullOrWhiteSpace(txtDescription.Text) || string.IsNullOrWhiteSpace(txtCost.Text))
            {
                MessageBox.Show("Заполнены не все обязательные поля!");
                return;
            }

            if (_currentAdvertisement.ID == 0) 
            {
                _currentAdvertisement.Vendor = barmoley.UserLogin;
                _currentAdvertisement.PublicationDate = DateTime.Now;
                Entities.Instance.Advertisement.Add(_currentAdvertisement);
            }
            //try
            //{
                Entities.Instance.SaveChanges();
                MessageBox.Show("Объявление успешно сохранено!");
                NavigationService.GoBack();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Ошибка при сохранении данных! Попробуйте вводить меньше текста в поля или помолитесь господу...");
            //}
        }
    }
}
