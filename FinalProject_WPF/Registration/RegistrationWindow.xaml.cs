using FinalProject_WPF_ClassLibrary.AuthenticationModel;
using FinalProject_WPF_ClassLibrary.Extensions;
using FinalProject_WPF_ClassLibrary;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FinalProject_WPF.Registration;
using FinalProject_WPF.Application;

namespace FinalProject_WPF.Registration
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        private readonly ApplicationDataApi _applicationDataApi;
        public RegistrationWindow()
        {
            InitializeComponent();
        }

        public RegistrationWindow(ApplicationDataApi applicationDataApi) : this()
        {
            _applicationDataApi = applicationDataApi;
            RegistrationButton.Click += delegate
            {
                ProjectLogic projectLogic = new ProjectLogic();
                string password = projectLogic.ConvertPasswordToString(PasswordBox.SecurePassword);
                string passwordConfirmed = projectLogic.ConvertPasswordToString(PasswordBoxConfirmed.SecurePassword);
                if (Ext.CheckNullOrEmpty(TxtLogin.Text, password, passwordConfirmed) && password == passwordConfirmed)
                {
                    UserRegistration userRegistration = new UserRegistration()
                    {
                        LoginProp = TxtLogin.Text,
                        Password = password,
                        ConfirmPassword = passwordConfirmed
                    };
                    string token = string.Empty;
                    token = applicationDataApi.IsRegistration(userRegistration);

                    if (projectLogic.TokenDecodingRegistrMethod(token))
                    {
                        RequestWindow requestWindow = new RequestWindow(applicationDataApi);
                        requestWindow.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Не верное имя пользователя или пароль");
                    }
                }
                else
                {
                    MessageBox.Show("Пустое значение имени пользователя или пароля" +
                        "или пароли не совпадают");
                }
            };
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
        /// Метод закрытия окна по нажатию на картинку в виде крестика
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CrossButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
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
