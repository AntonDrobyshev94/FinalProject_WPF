using FinalProject_WPF.Authenticate;
using FinalProject_WPF.Registration;
using FinalProject_WPF_ClassLibrary;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FinalProject_WPF.Application
{
    /// <summary>
    /// Логика взаимодействия для RequestWindow.xaml
    /// </summary>
    public partial class RequestWindow : Window
    {
        private readonly ApplicationDataApi _applicationDataApi;
        public RequestWindow()
        {
            InitializeComponent();
            AccessCheckMethod();
        }
        public RequestWindow(ApplicationDataApi applicationDataApi) : this()
        {
            _applicationDataApi = applicationDataApi;
            
            AddRequestButton.Click += delegate
            {
                ProjectLogic projectLogic = new ProjectLogic();
                string response = _applicationDataApi.AddApplication(projectLogic.CreateApplicationMethod(TxtName.Text,
                        TxtEmail.Text, TxtMessage.Text));
                if (response == "Успешно")
                {
                    MessageBox.Show("Заявка успешно отправлена!");
                    RequestWindow requestWindow = new RequestWindow(applicationDataApi);
                    requestWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show(response);
                }
            };

            Registration.Click += delegate
            {
                RegistrationWindow registrationWindow = new RegistrationWindow(_applicationDataApi);
                registrationWindow.Show();
                this.Close();
            };
            EnterAccount.Click += delegate
            {
                AuthenticateWindow authenticateWindow = new AuthenticateWindow(_applicationDataApi);
                authenticateWindow.Show();
                this.Close();
            };
            Exit.Click += delegate
            {
                GlobalVariables.userName = "";
                GlobalVariables.userRole = "";
                GlobalVariables.token = "";
                RequestWindow requestWindow = new RequestWindow(applicationDataApi);
                requestWindow.Show();
                MessageBox.Show("Вы успешно вышли из аккаунта!");
                this.Close();
            };
        }

        /// <summary>
        /// Метод проверки прав доступа, в котором проверяется условие
        /// авторизации пользователя. Если пользователь авторизован,
        /// то открывается видимость кнопки выхода из аккаунта, а
        /// также текстовое сообщение с приветствием пользователя.
        /// Если пользователь не авторизован, то открывается видимость
        /// кнопок входа в аккаунт и регистрации аккаунта.
        /// </summary>
        private void AccessCheckMethod()
        {
            ProjectLogic projectLogic = new ProjectLogic();
            string authResponse = projectLogic.IsAuthenticated();
            if (authResponse != string.Empty)
            {
                EnterAccount.Visibility = Visibility.Hidden;
                Registration.Visibility = Visibility.Hidden;
                TxtNameUser.Visibility = Visibility.Visible;
                TxtNameUser.Text = $"Добро пожаловать, {GlobalVariables.userName}!";
                Exit.Visibility = Visibility.Visible;
            }
            else
            {
                EnterAccount.Visibility = Visibility.Visible;
                Registration.Visibility = Visibility.Visible;
                TxtNameUser.Visibility = Visibility.Hidden;
                Exit.Visibility = Visibility.Hidden;
            }
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
