using FinalProject_WPF_ClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using FinalProject_WPF_ClassLibrary.Extensions;

namespace FinalProject_WPF_ClassLibrary
{
    public class ProjectLogic
    { 
        ApplicationDataApi context = new ApplicationDataApi();

        /// <summary>
        /// Метод создания новой заявки
        /// </summary>
        /// <returns></returns>
        public Application CreateApplicationMethod(string name, string eMail, string message)
        {
            Application newApplication = new Application()
            {
                Name = name,
                Date = DateTime.Now,
                EMail = eMail,
                Message = message,
                Status = "Получена"
            };
            return newApplication;
        }

        /// <summary>
        /// Метод создания новой коллекции заявок
        /// </summary>
        public void CreateNewApplicationCollection()
        {
            GlobalVariables.dateSortApplicationList = new List<Application>();
        }

        /// <summary>
        /// Метод создания коллекции заявок, принимающий строковое значение
        /// даты заявки и возвращающий коллекцию заявок.
        /// В методе происходит проверка условия совпадения значения
        /// переменной dateString указанному значению. Если значение совпадает,
        /// то происходит присвоение значений стартовой даты и конечной даты
        /// и запуск цикла с перебором коллекции заявок на предмет совпадения
        /// периоду дат. При совпадении происходит добавление заявки в
        /// новую коллекцию. По окончанию происходит возврат коллекции заявок 
        /// ключевым словом return.
        /// </summary>
        /// <param name="dateString"></param>
        /// <returns></returns>
        public List<Application> DateTimeApplicationCollCreate(string dateString)
        {
            CreateNewApplicationCollection();
            GlobalVariables.applicationList = GetApplicationsMethod().ToList();
            if (dateString == "today")
            {
                GlobalVariables.startDate = DateTime.Now.Date;
                GlobalVariables.endDate = DateTime.Now.Date.AddDays(1).AddSeconds(-1);
                GlobalVariables.requestPeriodCount = 0;
                foreach (var item in GlobalVariables.applicationList)
                {
                    if (item.Date > GlobalVariables.startDate && item.Date < GlobalVariables.endDate)
                    {
                        GlobalVariables.dateSortApplicationList.Add(item);
                        GlobalVariables.requestPeriodCount++;
                    }
                }
            }
            else if (dateString == "yesterday")
            {
                DateTime yesterday = DateTime.Now.AddDays(-1).Date;
                GlobalVariables.startDate = yesterday;
                DateTime endOfYesterday = yesterday.AddDays(1).AddSeconds(-1);
                GlobalVariables.endDate = endOfYesterday;
                GlobalVariables.requestPeriodCount = 0;
                foreach (var item in GlobalVariables.applicationList)
                {
                    if (item.Date > GlobalVariables.startDate && item.Date < GlobalVariables.endDate)
                    {
                        GlobalVariables.dateSortApplicationList.Add(item);
                        GlobalVariables.requestPeriodCount++;
                    }
                }
            }
            else if (dateString == "week")
            {
                DateTime today = DateTime.Now.Date;
                int daysToSubtract = (int)today.DayOfWeek - (int)DayOfWeek.Monday;
                DateTime startOfWeek = today.AddDays(-daysToSubtract);

                GlobalVariables.startDate = startOfWeek;

                int daysUntilEndOfWeek = (int)DayOfWeek.Saturday - (int)today.DayOfWeek;
                DateTime endOfWeek = today.AddDays(daysUntilEndOfWeek).AddDays(1).AddSeconds(-1);

                GlobalVariables.endDate = endOfWeek;
                GlobalVariables.requestPeriodCount = 0;
                foreach (var item in GlobalVariables.applicationList)
                {
                    if (item.Date > GlobalVariables.startDate && item.Date < GlobalVariables.endDate)
                    {
                        GlobalVariables.dateSortApplicationList.Add(item);
                        GlobalVariables.requestPeriodCount++;
                    }
                }
            }
            else if(dateString == "month")
            {
                DateTime today = DateTime.Now.Date;
                DateTime startOfMonth = new DateTime(today.Year, today.Month, 1);

                GlobalVariables.startDate = startOfMonth;

                DateTime startOfNextMonth = new DateTime(today.Year, today.Month, 1).AddMonths(1);
                DateTime endOfMonth = startOfNextMonth.AddSeconds(-1);
                GlobalVariables.endDate = endOfMonth;
                GlobalVariables.requestPeriodCount = 0;
                foreach (var item in GlobalVariables.applicationList)
                {
                    if (item.Date > GlobalVariables.startDate && item.Date < GlobalVariables.endDate)
                    {
                        GlobalVariables.dateSortApplicationList.Add(item);
                        GlobalVariables.requestPeriodCount++;
                    }
                }
            }
            else
            {
                GlobalVariables.startDate = DateTime.Now.AddDays(-100000);
                GlobalVariables.endDate = DateTime.Now.AddDays(100000);
                GlobalVariables.requestCount = 0;
                foreach (var item in GlobalVariables.applicationList)
                {
                    if (item.Date > GlobalVariables.startDate && item.Date < GlobalVariables.endDate)
                    {
                        GlobalVariables.dateSortApplicationList.Add(item);
                        GlobalVariables.requestCount++;
                    }
                }
            }
            RequestWordMethod();
            return GlobalVariables.dateSortApplicationList;
        }

        /// <summary>
        /// Метод для определения окончания в слове "заявка". В зависимости
        /// от последней цифры числа (находится по остатку числа)
        /// определяется одна из трёх вариаций окончания слова "заявка".
        /// Результат сохраняется в строковую переменную requestWord.
        /// </summary>
        public void RequestWordMethod()
        {
            int lastDigit = GlobalVariables.requestPeriodCount % 10;
            if (lastDigit == 0 || lastDigit == 5 || lastDigit == 6
            || lastDigit == 7 || lastDigit == 8 || lastDigit == 9)
            {
                GlobalVariables.requestWord = "заявок";
            }
            else if (lastDigit == 2 || lastDigit == 3 || lastDigit == 4)
            {
                GlobalVariables.requestWord = "заявки";
            }
            else if (lastDigit == 1)
            {
                GlobalVariables.requestWord = "заявка";
            }
        }

        /// <summary>
        /// Метод создания коллекции заявок, принимающий значения
        /// промежутка дат заявки и возвращающий коллекцию заявок.
        /// В методе происходит запуск цикла с перебором коллекции 
        /// заявок на предмет совпадения периоду дат. При совпадении 
        /// происходит добавление заявки в новую коллекцию.
        /// По окончанию происходит возврат коллекции заявок ключевым
        /// словом return.
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public List<Application> DatePickApplicationCollCreate(DateTime? startTime, DateTime? endTime)
        {
            CreateNewApplicationCollection();
            GlobalVariables.applicationList = GetApplicationsMethod().ToList();
            GlobalVariables.requestPeriodCount = 0;
            foreach (var item in GlobalVariables.applicationList)
            {
                if (item.Date > startTime && item.Date < endTime)
                {
                    GlobalVariables.dateSortApplicationList.Add(item);
                    GlobalVariables.requestPeriodCount++;
                }
            }
            RequestWordMethod();
            return GlobalVariables.dateSortApplicationList;
        }

            /// <summary>
            /// Метод проверки токена, возвращающий bool переменную
            /// </summary>
            /// <returns></returns>
            public bool CheckTokenMethod()
        {
            if (context.CheckToken())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Метод, предоставляющий перечень всех контактов (возвращает
        /// ObservableCollection контактов).
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Application> GetApplicationsMethod()
        {
            return context.GetApplications().ToObservableCollection();
        }

        /// <summary>
        /// Метод изменения статуса заявки, принимающий строковое значение
        /// статуса и id заявки.
        /// </summary>
        /// <param name="status"></param>
        /// <param name="id"></param>
        public void ChangeApplicationStatusMethod(string status, int id)
        {

            context.ChangeApplicationStatus(status, id);
        }

        /// <summary>
        /// Метод выхода из пользовательского аккаунта, при котором происходит
        /// присвоение пустых значений для глобальных переменных токена,
        /// имени и роли пользователя, а также создаётся новый экземпляр
        /// ApplicationDataApi, который возвращается по окончанию ключевым словом
        /// return
        /// </summary>
        /// <returns></returns>
        public ApplicationDataApi ExitMethod()
        {
            GlobalVariables.token = string.Empty;
            GlobalVariables.userRole = string.Empty;
            GlobalVariables.userName = string.Empty;
            ApplicationDataApi applicationDataApi = new ApplicationDataApi();
            return applicationDataApi;
        }

        /// <summary>
        /// Метод предоставления информации о правах доступа пользователя,
        /// который, в случае если роль пользователя Admin возвращает
        /// значение логической переменной true, в обратном случае - false.
        /// </summary>
        /// <returns></returns>
        public bool UserRoleAccessInformation()
        {
            if (GlobalVariables.userRole == "Admin")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    
        public string IsAuthenticated()
        {
            if (GlobalVariables.userName != string.Empty)
            {
                return GlobalVariables.userName;
            }
            else
            {
                return string.Empty;
            }
        }



        /// <summary>
        /// Метод предоставления информации об имени пользователя,
        /// который возвращает строку с приветствием и именем пользователя.
        /// </summary>
        /// <returns></returns>
        public string UserNameInformation()
        {
            return $"Добро пожаловать, {GlobalVariables.userName}";
        }

        /// <summary>
        /// Метод расшифровки засекреченного звёздочками пароля из
        /// PasswordBox
        /// </summary>
        /// <param name="securePassword"></param>
        /// <returns></returns>
        public string ConvertPasswordToString(SecureString securePassword)
        {
            if (securePassword == null)
                return string.Empty;

            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(securePassword);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }

        /// <summary>
        /// Метод расшифровки токена, принимающий строковое значение токена и возвращающий
        /// логический ответ true или false. В методе происходит проверка на содержимое
        /// переменной и если она не равна пустой строке, то происходит расшифровка токена
        /// и запись значений имени, роли и токена в глобальные переменные.
        /// </summary>
        /// <param name="concreteToken"></param>
        /// <returns></returns>
        public bool TokenDecodingAuthMethod(string concreteToken)
        {
            if (concreteToken == string.Empty)
            {
                return false;
            }
            else if(concreteToken == "API не доступен")
            {
                return false;
            }    
            else
            {
                GlobalVariables.token = concreteToken;
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadJwtToken(concreteToken);
                foreach (var item in jwtToken.Claims)
                {
                    if (item.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")
                    {
                        if (item.Value == "Admin")
                        {
                            GlobalVariables.userRole = item.Value;
                            break;
                        }
                        else
                        {
                            GlobalVariables.userRole = item.Value;
                        }
                    }
                }
                foreach (var item in jwtToken.Claims)
                {
                    if (item.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")
                    {
                        GlobalVariables.userName = item.Value;
                    }
                }
                return true;
            }
        }

        /// <summary>
        /// Метод расшифровки токена, принимающий строковое значение токена и возвращающий
        /// логический ответ true или false. В методе происходит проверка на содержимое
        /// переменной и если она не равна пустой строке, то происходит расшифровка токена
        /// и запись значений имени, роли и токена в глобальные переменные.
        /// </summary>
        /// <param name="concreteToken"></param>
        /// <returns></returns>
        public bool TokenDecodingRegistrMethod(string concreteToken)
        {
            if (concreteToken == string.Empty)
            {
                return false;
            }
            else
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadJwtToken(concreteToken);

                GlobalVariables.token = concreteToken;
                GlobalVariables.userRole = "User";
                foreach (var item in jwtToken.Claims)
                {
                    if (item.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")
                    {
                        GlobalVariables.userName = item.Value;
                    }
                }
                return true;
            }
        }
    }
}
