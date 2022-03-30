using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using Library.Classes;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Library.Windows
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        public AuthorizationWindow()
        {
            InitializeComponent();
        }

        static string sha256(string inputString)
        {
            var crypt = new SHA256Managed();
            string hash = String.Empty;
            byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(inputString));
            foreach (byte theByte in crypto)
            {
                hash += theByte.ToString("x2");
            }
            return hash;
        }

        private async void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient { BaseAddress = new Uri(Properties.Settings.Default.BaseAddress) })
                {
                    var content = new StringContent("", Encoding.UTF8, "applocation/json");
                    HttpResponseMessage httpResponseMessage = await httpClient.PostAsync($"/Login?login={tbLogin.Text}&password={sha256(pbPassword.Password)}", content);
                    string token = httpResponseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        MainWindow main = new MainWindow(token);
                        main.Show();
                        this.Close();
                    }
                    else MessageBox.Show("Логин или пароль не верный");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Пользователь не найден");
            }
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TbPassword.Text = pbPassword.Password;
            TbPassword.Visibility = Visibility.Visible;
            pbPassword.Visibility = Visibility.Collapsed;
        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TbPassword.Visibility = Visibility.Collapsed;
            pbPassword.Visibility = Visibility.Visible;
        }
    }
}
