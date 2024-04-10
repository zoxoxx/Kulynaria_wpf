using Kulynaria.Classes;
using Kulynaria.Model;
using Kulynaria.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace Kulynaria
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static User user;
        int counter = 0;
        UserFromDb userFromDb = new UserFromDb();
        public static User currentUser { get; set; } = null;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_vhod_Click(object sender, RoutedEventArgs e)
        {
            if (!(tb_login.Text != "" && tb_password.Text != ""))
            {
                MessageBox.Show("Введите данные"); return;
            }
            else
            {
                currentUser = userFromDb.GetUser(tb_login.Text, tb_password.Text);
                if (currentUser != null)
                {
                    user = new User(tb_password.Text, tb_login.Text);
                    MainWind main = new MainWind();
                    main.Show();
                    this.Hide();
                }
                else
                {
                    counter++;
                    MessageBox.Show("Нет такого пользователя");
                    if (counter == 4)
                    {
                       //CaptchaWindow captchaWindow = new CaptchaWindow();
                       // captchaWindow.Owner = this;
                        //captchaWindow.ShowDialog();
                     
                    }
                    if (counter > 5)
                    {
                        counter = 0;
                        counter++;
                    }
                }
            }
        }

        private void btn_reg_Click(object sender, RoutedEventArgs e)
        {
            Registration registration = new Windows.Registration();
            registration.Show();
            this.Hide();
        }
    }
}
