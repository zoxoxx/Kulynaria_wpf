using Kulynaria.Classes;
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

namespace Kulynaria.Windows
{
    /// <summary>
    /// Логика взаимодействия для MainWind.xaml
    /// </summary>
    public partial class MainWind : Window
    {
        public MainWind()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            FrameClass.bludoFrame = bludoFrame;
            FrameClass.bludoFrame.Navigate(new Pages.BludoPage());
            lbl_FIO.Content = MainWindow.currentUser.LastName + " " + MainWindow.currentUser.FirstName;
        }
    }
}
