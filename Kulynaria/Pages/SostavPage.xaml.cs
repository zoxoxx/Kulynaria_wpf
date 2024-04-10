using Kulynaria.Classes;
using Kulynaria.Model;
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

namespace Kulynaria.Pages
{
    /// <summary>
    /// Логика взаимодействия для SostavPage.xaml
    /// </summary>
    public partial class SostavPage : Page
    {
      public  Bludo currentBludo;
        List<SostavBluda> currentsostav = new List<SostavBluda>();
        public SostavPage(Bludo bludo)
        {
            currentBludo = bludo;
            InitializeComponent();
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            currentsostav = BludaFromDb.SostavBludFromDB(currentBludo.Identityb);
            dgSostav.ItemsSource = currentsostav;
            dgSostav.Columns[0].Visibility = Visibility.Collapsed;
        }
    }
}
