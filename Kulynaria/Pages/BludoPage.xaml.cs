using Kulynaria.Classes;
using Kulynaria.Model;
using Kulynaria.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для BludoPage.xaml
    /// </summary>
    public partial class BludoPage : Page
    {
        public static Bludo blud;
        List<Bludo> bludos = new List<Bludo>();
        BludaFromDb bludo = new BludaFromDb();
        List<Categoriya> categorys = new List<Categoriya>();
        CategoryFromDb categoryFromDb = new CategoryFromDb();
        public BludoPage()
        {
            InitializeComponent();
        }

        private void allBludos()
        {
            bludos = bludo.loadBludos();
            listViewBludas.ItemsSource = bludos;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            allBludos();
            categorys = categoryFromDb.LoadCategories();
            categorys.Insert(0, new Categoriya(0, "All"));
            cmbCategoriya.ItemsSource = categorys;
            cmbCategoriya.DisplayMemberPath = "vids_blud";

        }

        private List<Bludo> SearchBludos(string txbSearch)
        {
            List<Bludo> bludoSearch = new List<Bludo>();

            foreach (Bludo item in bludos)
            {
                if (item.bludo.StartsWith(txbSearch) || item.Osnova.StartsWith(txbSearch))
                {
                    bludoSearch.Add(item);
                }
            }
            return bludoSearch;
        }

        private void cmbCategoriya_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbCategoriya.SelectedIndex == 0)
            {
                allBludos();
            }
            else
            {
                bludos = bludo.FiltrBludosByCategory(Convert.ToInt32(cmbCategoriya.SelectedIndex));
                listViewBludas.ItemsSource = bludos;
            }
        }

        private void txbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            listViewBludas.ItemsSource = SearchBludos(txbSearch.Text);
        }

        private void Sostav(object sender, RoutedEventArgs e)
        {
            int index = listViewBludas.SelectedIndex;
            blud = bludos[index];
            SostavPage sostavp = new SostavPage(blud);
            NavigationService.Navigate(sostavp);


        }

        private void listViewBludas_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {

          

        }
    }
}
