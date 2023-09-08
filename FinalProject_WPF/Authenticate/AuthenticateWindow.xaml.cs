using FinalProject_WPF.Application;
using FinalProject_WPF.Registration;
using FinalProject_WPF_ClassLibrary;
using FinalProject_WPF_ClassLibrary.AuthenticationModel;
using FinalProject_WPF_ClassLibrary.Extensions;
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
using static System.Net.Mime.MediaTypeNames;

namespace FinalProject_WPF.Authenticate
{
    /// <summary>
    /// Логика взаимодействия для AuthenticateWindow.xaml
    /// </summary>
    public partial class AuthenticateWindow : Window
    {
        private readonly ApplicationDataApi _applicationDataApi;
        public AuthenticateWindow()
        {
            InitializeComponent();
        }
        public AuthenticateWindow(ApplicationDataApi applicationDataApi) : this()
        {
            _applicationDataApi = applicationDataApi;
            EnterButton.Click += delegate
            {
                ProjectLogic projectLogic = new ProjectLogic();
                string password = projectLogic.ConvertPasswordToString(PasswordBox.SecurePassword);
                if (Ext.CheckNullOrEmpty(TxtLogin.Text, password))
                {
                    UserLoginProp userLogin = new UserLoginProp()
                    {
                        UserName = TxtLogin.Text,
                        Password = password
                    };
                    string token = string.Empty;
                    token = _applicationDataApi.IsLogin(userLogin);
                    if (projectLogic.TokenDecodingAuthMethod(token))
                    {
                        if (projectLogic.UserRoleAccessInformation())
                        {
                            MainWindow mainWindow = new MainWindow(applicationDataApi);
                            mainWindow.Show();
                            this.Close();
                        }
                        else
                        {
                            RequestWindow requestWindow = new RequestWindow(applicationDataApi);
                            requestWindow.Show();
                            this.Close();
                        }
                    }
                    else if (token == "API не доступен")
                    {
                        MessageBox.Show($"{token}!\nОжидайте, пока API не будет запущен!");
                    }
                    else
                    {
                        MessageBox.Show("Не верное имя пользователя или пароль");
                    }
                }
                else
                {
                    MessageBox.Show("Пустое значение имени пользователя или пароля");
                }
            };

            RegistrationButton.Click += delegate
            {
                RegistrationWindow registrationWindow = new RegistrationWindow(_applicationDataApi);
                registrationWindow.Show();
                this.Close();
            };
        }

        /// <summary>
        /// Метод закрытия окна по нажатию на картинку в виде крестика
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CrossButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
            
        }

        /// <summary>
        /// Метод сворачивания окна по нажатию на картинку сворачивания
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CollapseButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// Метод перемещения окна по нажатию на верхний тулбар окна, в котором
        /// проверяется условие нажатие левой кнопки мыши по тулбару.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }
}
