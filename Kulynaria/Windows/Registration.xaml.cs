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
using System.Windows.Shapes;

namespace Kulynaria.Windows
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void btn_registration_Click(object sender, RoutedEventArgs e)
        {
            if (tb_name.Text == "" || tb_fam.Text == "" || tb_login.Text == "" || tb_password.Text == "" || tb_podPassword.Text == "" || tb_email.Text == "")
            { MessageBox.Show("Необходимо заполнить все поля!"); return; }
            bool rez = UserFromDb.CheckPassword(tb_password.Text, tb_podPassword.Text);
            if (!rez) return;
            else
                if (UserFromDb.CheckUser(tb_login.Text))
            {
                UserFromDb.UserAdd(tb_login.Text, tb_password.Text, tb_name.Text, tb_fam.Text);
                EmailService emailService = new EmailService();
                emailService.SendEmailAsync(tb_email.Text, "Пароль для нового аккаунта", tb_podPassword.Text);
            }
            else return;
            this.Hide();
        }

        private string GeneratePassword()
        {
            string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^";
            StringBuilder password = new StringBuilder();
            Random random = new Random();

            for (int i = 0; i < 8; i++)
            {
                int index = random.Next(validChars.Length);
                password.Append(validChars[index]);
            }

            return password.ToString();
        }

        private void btn_generation_Click(object sender, RoutedEventArgs e)
        {
            tb_password.Text = GeneratePassword();
            tb_podPassword.Text = tb_password.Text;
        }
    }
}
