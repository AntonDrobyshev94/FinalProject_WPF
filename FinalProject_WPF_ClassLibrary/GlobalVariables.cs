using FinalProject_WPF_ClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_WPF_ClassLibrary
{
    public class GlobalVariables
    {
        public static string token = "";
        public static string userName = "";
        public static string userRole = "";
        public static Application applicationHighlight { get; set; } = new Application();
        public static List<Application> applicationList { get; set; } = new List<Application>();
        public static List<Application> dateSortApplicationList { get; set; } = new List<Application>();
        public static DateTime startDate = new DateTime();
        public static DateTime endDate = new DateTime();
        public static int requestCount;
        public static int requestPeriodCount;
        public static string requestWord;
    }
}
