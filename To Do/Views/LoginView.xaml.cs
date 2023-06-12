using MaterialDesignThemes.Wpf;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using To_Do.Services;
using To_Do.Shared;

namespace To_Do.Views
{
    /// <summary>
    /// LoginView.xaml 的交互逻辑
    /// </summary>
    public partial class LoginView : UserControl
    {
        private readonly IUserService userService;
        private readonly SnackbarMessageQueue snackbarMessage;

        private readonly int LOGIN = 0;
        private readonly int REGISTER = 1;
        
        public LoginView(IUserService userService)
        {
            InitializeComponent();
            this.userService = userService;

            snackbarMessage = new SnackbarMessageQueue(TimeSpan.FromSeconds(5));
            MessageBar.MessageQueue = snackbarMessage;
            Transitioner.SelectedIndex = LOGIN;
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var window = Window.GetWindow(this);
                window.DragMove();
            }
        }

        private async void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            var email = LoginEmail.Text;
            var pwd = LoginPassword.Password;

            // TODO validator
            if (string.IsNullOrEmpty(email)
                || string.IsNullOrEmpty(pwd))
            {
                snackbarMessage.Enqueue("邮箱或密码为空");
                return;
            }

            var response = await userService.LoginAsync(
                new LoginDTO(email, SHA2Hash(pwd)));

            // response
            if (response.IsSuccessStatusCode)
            {
                snackbarMessage.Enqueue("登录成功, 窗口即将关闭");
                await Task.Delay(TimeSpan.FromSeconds(3));
                await SaveTokenToUserFile(response.Content);
                CloseWindow();
            }
            else
            {
                snackbarMessage.Enqueue("登录失败, 请重试");
            }
        }

        /// <summary>
        /// TODO TokenHelper.cs
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private async Task SaveTokenToUserFile(string? token)
        {
            string userFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string filePath = Path.Combine(userFolderPath, "ToDoToken.txt");
            await File.WriteAllTextAsync(filePath, token);
        }

        private void CloseWindow()
        {
            Window.GetWindow(this).Close();
        }

        private async void RegisterBtn_Click(object sender, RoutedEventArgs e)
        {
            var email = RegisterEmail.Text;
            var pwd = RegisterPassword.Password;
            var cfmPwd = RegisterConfirmPassword.Password;

            // TODO validator
            if (string.IsNullOrEmpty(email)
                || string.IsNullOrEmpty(pwd)
                || string.IsNullOrEmpty(cfmPwd))
            {
                snackbarMessage.Enqueue("请输入用户名和密码");
                return;
            }

            if (pwd != cfmPwd)
            {
                snackbarMessage.Enqueue("两次输入密码不一致");
                return;
            }

            pwd = SHA2Hash(pwd);
            cfmPwd = SHA2Hash(cfmPwd);

            // TODO Loading
            var response = await userService.RegisterAsync(
                new RegisterDTO(email, pwd, cfmPwd));

            // response
            if (response.IsSuccessStatusCode)
            {
                snackbarMessage.Enqueue("注册成功, 请验证邮箱后登录");
            }
            else
            {
                snackbarMessage.Enqueue("注册失败, 请重试");
            }
        }

        private string SHA2Hash(string input)
        {
            input += SecretConstants.SALT;

            var sha = SHA256.Create();

            var inputBytes = Encoding.UTF8.GetBytes(input);
            var hashBytes = sha.ComputeHash(inputBytes);
            var hashStr = BitConverter.ToString(hashBytes);
            hashStr += SecretConstants.SALT;

            return hashStr.Replace("-", "");
        }

        private void ToRegisterViewBtn_Click(object sender, RoutedEventArgs e)
        {
            Transitioner.SelectedIndex = REGISTER;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            CloseWindow();
        }

        private void ToLoginViewBtn_Click(object sender, RoutedEventArgs e)
        {
            Transitioner.SelectedIndex = LOGIN;
        }
    }
}
