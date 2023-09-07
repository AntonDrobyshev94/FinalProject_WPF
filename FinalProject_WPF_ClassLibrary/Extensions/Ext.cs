using FinalProject_WPF_ClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_WPF_ClassLibrary.Extensions
{
    public static class Ext
    {
        /// <summary>
        /// Метод конвертации перечня контактов в коллецию ObservableCollection 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static ObservableCollection<Application> ToObservableCollection(this IEnumerable<Application> e)
        {
            ObservableCollection<Application> t = new ObservableCollection<Application>();
            foreach (var item in e)
            {
                t.Add(item);
            }
            return t;
        }

        /// <summary>
        /// Метод конвертации конкретного контакта в коллекцию ObservableCollection
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static ObservableCollection<Application> ToObservableCollection(this Application e)
        {
            ObservableCollection<Application> t = new ObservableCollection<Application>();
            t.Add(e);
            return t;
        }

        /// <summary>
        /// Метод проверки принимаемой строки (строк) на значение равное нулю или пустой строки. 
        /// Если значение не равно нулю или пустой строке, возвращается bool переменная
        /// true. В обратном случае - false. Является первой перегрузкой.
        /// </summary>
        /// <param name="firstString"></param>
        /// <returns></returns>
        public static bool CheckNullOrEmpty(string firstString)
        {
            if (!string.IsNullOrEmpty(firstString))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Метод проверки принимаемой строки (строк) на значение равное нулю или пустой строки. 
        /// Если значение не равно нулю или пустой строке, возвращается bool переменная
        /// true. В обратном случае - false. Является второй перегрузкой.
        /// </summary>
        /// <param name="firstString"></param>
        /// <param name="secondString"></param>
        /// <returns></returns>
        public static bool CheckNullOrEmpty(string firstString, string secondString)
        {
            if (!string.IsNullOrEmpty(firstString) && !string.IsNullOrEmpty(secondString))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Метод проверки принимаемой строки (строк) на значение равное нулю или пустой строки. 
        /// Если значение не равно нулю или пустой строке, возвращается bool переменная
        /// true. В обратном случае - false. Является третьей перегрузкой.
        /// </summary>
        /// <param name="firstString"></param>
        /// <param name="secondString"></param>
        /// <param name="thirdString"></param>
        /// <returns></returns>
        public static bool CheckNullOrEmpty(string firstString, string secondString, string thirdString)
        {
            if (!string.IsNullOrEmpty(firstString) && !string.IsNullOrEmpty(secondString) &&
                !string.IsNullOrEmpty(thirdString))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Метод проверки принимаемых строк на значение равное нулю или пустой строки. 
        /// Если значение не равно нулю или пустой строке, возвращается bool переменная
        /// true. В обратном случае - false. Является четвёртой перегрузкой.
        /// </summary>
        /// <param name="firstString"></param>
        /// <param name="secondString"></param>
        /// <param name="thirdString"></param>
        /// <param name="fourthString"></param>
        /// <returns></returns>
        public static bool CheckNullOrEmpty(string firstString, string secondString, string thirdString,
            string fourthString)
        {
            if (!string.IsNullOrEmpty(firstString) && !string.IsNullOrEmpty(secondString) &&
                !string.IsNullOrEmpty(thirdString) && !string.IsNullOrEmpty(fourthString))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
