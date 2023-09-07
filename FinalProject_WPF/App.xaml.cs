using FinalProject_WPF.Application;
using FinalProject_WPF.Authenticate;
using FinalProject_WPF_ClassLibrary;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FinalProject_WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        private void App_Startup(object sender, StartupEventArgs e)
        {
            ApplicationDataApi applicationDataApi = new ApplicationDataApi();   
            RequestWindow requestWindow = new RequestWindow(applicationDataApi);
            requestWindow.Show();
        }
    }
}
