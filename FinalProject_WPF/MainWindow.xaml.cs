using FinalProject_WPF.Application;
using FinalProject_WPF_ClassLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace FinalProject_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ApplicationDataApi _applicationDataApi;

        public MainWindow()
        {
            InitializeComponent();
            TxtNameUser.Text = $"Добро пожаловать, {GlobalVariables.userName}!";
            ProjectLogic projectLogic = new ProjectLogic();

            DataGridView.ItemsSource = projectLogic.DateTimeApplicationCollCreate("");
            RequestCount.Text = $"Количество заявок: {GlobalVariables.requestCount.ToString()}";
            RequestPeriodCount.Text = $"Не выбран рассматриваемый период";
        }

        public MainWindow(ApplicationDataApi applicationDataApi) :this()
        {
            _applicationDataApi = applicationDataApi;

            ProjectLogic projectLogic = new ProjectLogic();

            Today.Click += delegate
            {
                DataGridView.ItemsSource = projectLogic.DateTimeApplicationCollCreate("today");
                RequestPeriodCount.Text = $"Количество заявок в рассматриваемый период: {GlobalVariables.requestPeriodCount.ToString()} {GlobalVariables.requestWord}";
            };
            Yesterday.Click += delegate
            {
                DataGridView.ItemsSource = projectLogic.DateTimeApplicationCollCreate("yesterday");
                RequestPeriodCount.Text = $"Количество заявок в рассматриваемый период: {GlobalVariables.requestPeriodCount.ToString()} {GlobalVariables.requestWord}";
            };
            Week.Click += delegate
            {
                DataGridView.ItemsSource = projectLogic.DateTimeApplicationCollCreate("week");
                RequestPeriodCount.Text = $"Количество заявок в рассматриваемый период: {GlobalVariables.requestPeriodCount.ToString()} {GlobalVariables.requestWord}";
            };
            Month.Click += delegate
            {
                DataGridView.ItemsSource = projectLogic.DateTimeApplicationCollCreate("month");
                RequestPeriodCount.Text = $"Количество заявок в рассматриваемый период: {GlobalVariables.requestPeriodCount.ToString()} {GlobalVariables.requestWord}";
            };

            FirstDatePick.SelectedDateChanged += delegate
            {
                DateTime? startDate = FirstDatePick.SelectedDate;
                DateTime? endDate = SecondDatePick.SelectedDate;
                DateTime actualEndDate = DateTime.MinValue;
                if (endDate != null)
                {
                    actualEndDate = endDate.Value.AddDays(1).AddSeconds(-1);
                }
                else
                {
                    actualEndDate = DateTime.MinValue;
                }

                DataGridView.ItemsSource = projectLogic.DatePickApplicationCollCreate(startDate, actualEndDate);

                RequestPeriodCount.Text = $"Количество заявок в период с {startDate.Value.ToShortDateString()} по {actualEndDate.ToShortDateString()}: {GlobalVariables.requestPeriodCount.ToString()} {GlobalVariables.requestWord}";
            };
            SecondDatePick.SelectedDateChanged += delegate
            {
                DateTime? startDate = FirstDatePick.SelectedDate;
                DateTime? endDate = SecondDatePick.SelectedDate;
                DateTime actualStartDate = DateTime.MaxValue;
                if (startDate != null)
                {
                    actualStartDate = startDate.Value;
                }
                else
                {
                    actualStartDate = DateTime.MaxValue;
                }
                DateTime actualEndDate = endDate.Value.AddDays(1).AddSeconds(-1);
                DataGridView.ItemsSource = projectLogic.DatePickApplicationCollCreate(actualStartDate, actualEndDate);
                RequestPeriodCount.Text = $"Количество заявок в период с {startDate.Value.ToShortDateString()} по {actualEndDate.ToShortDateString()}: {GlobalVariables.requestPeriodCount.ToString()} {GlobalVariables.requestWord}";
            };

            AddRequestButton.Click += delegate
            {
                ProjectLogic projectLogic = new ProjectLogic();
                string response = _applicationDataApi.AddApplication(projectLogic.CreateApplicationMethod(TxtName.Text,
                        TxtEmail.Text, TxtMessage.Text));
                if (response == "Успешно")
                {
                    MessageBox.Show("Заявка успешно отправлена!");
                    MainWindow mainWindow = new MainWindow(applicationDataApi);
                    mainWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show(response);
                }
            };
            ExitButton.Click += delegate
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        /// <summary>
        /// Метод обработки события изменения в DataGrid. При внесении изменений в 
        /// DataGrid происходит получение экземпляра выделенного элемента DataGrid,
        /// параметаризированного классом Application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GVCellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            try
            {
                GlobalVariables.applicationHighlight = (FinalProject_WPF_ClassLibrary.Models.Application)DataGridView.SelectedItem;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Метод обработки изменений в DataGrid, в котором при выделении
        /// поля DataGrid происходит проверка текущего экземпляра
        /// applicationHighlight. Если он не задан, то происходит завершение
        /// метода с помощью ключевого слова return. 
        /// При заданном applicationHighlight происходит отработка метода
        /// ChangeApplicationStatusMethod, который принимает значения
        /// глобальных переменных статуса и id.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GVCurrentCellChanged(object sender, EventArgs e)
        {
            if (GlobalVariables.applicationHighlight == null) return;
            string status = GlobalVariables.applicationHighlight.Status;
            int id = GlobalVariables.applicationHighlight.ID;
            ProjectLogic projectLogic = new ProjectLogic();
            projectLogic.ChangeApplicationStatusMethod(status, id);
        }
    }
}
