using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using FinalProject_WPF_ClassLibrary.Models;
using FinalProject_WPF_ClassLibrary.AuthenticationModel;
using Newtonsoft.Json;

namespace FinalProject_WPF_ClassLibrary
{
    public class ApplicationDataApi
    {
        private HttpClient httpClient { get; set; }

        public ApplicationDataApi()
        {
            httpClient = new HttpClient();
        }

        #region Application
        /// <summary>
        /// Запрос на получение всех заявок, передающийся на API 
        /// сервер. Запрос возвращает результат в виде коллекции
        /// объектов типа Application.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Application> GetApplications()
        {
            string url = @"https://localhost:7037/api/values";
            AddTokenHeaderMethod();
            string json = httpClient.GetStringAsync(url).Result;
            return JsonConvert.DeserializeObject<IEnumerable<Application>>(json);
        }

        /// <summary>
        /// Запрос на создание новой заявки, передающийся на API 
        /// сервер. Запрос является невозвратным.
        /// </summary>
        /// <param name="application"></param>
        public string AddApplication(Application application)
        {
            string url = @"https://localhost:7037/api/values/";
            try
            {
                AddTokenHeaderMethod();
                var r = httpClient.PostAsync(
                    requestUri: url,
                    content: new StringContent(JsonConvert.SerializeObject(application), Encoding.UTF8,
                    mediaType: "application/json")
                    ).Result;
                return "Успешно";
            }
            catch (Exception ex)
            {
                return $"Произошла ошибка!\n{ex}";
            }
        }

        /// <summary>
        /// Запрос на удаление заявки по указанному id, передающийся 
        /// на API сервер. Запрос является невозвратным.
        /// </summary>
        /// <param name="id"></param>
        public void DeleteApplication(int id)
        {
            string url = $"https://localhost:7037/api/values/{id}";
            AddTokenHeaderMethod();
            var r = httpClient.DeleteAsync(
                requestUri: url);
        }

        /// <summary>
        /// Запрос на изменение статуса заявки, передающийся 
        /// на API сервер. Данный запрос принимает строковую переменную,
        /// которая используется для создания нового экземпляра Application
        /// Далее происходит передача 
        /// экземпляра Application. Данный метод является невозвратным.
        /// </summary>
        /// <param name="status"></param>
        /// <param name="id"></param>
        public void ChangeApplicationStatus(string status, int id)
        {
            Application application = new Application()
            {
                ID = id,
                Name = "",
                EMail = "",
                Message = "",
                Date = DateTime.Now,
                Status = status
            };

            string url = $"https://localhost:7037/api/values/ChangeStatus";
            AddTokenHeaderMethod();
            var r = httpClient.PostAsync(
                requestUri: url,
                content: new StringContent(JsonConvert.SerializeObject(application), Encoding.UTF8,
                mediaType: "application/json")
                ).Result;
        }
        #endregion
        #region Authentication
        /// <summary>
        /// Запрос на регистрацию, передающийся в API сервер.
        /// Данный запрос принимает модель регистрации и возвращает
        /// результат запроса в виде строки, содержащей токен.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string IsRegistration(UserRegistration model)
        {
            string url = $"https://localhost:7037/api/values/Registration/";

            var r = httpClient.PostAsync(
                requestUri: url,
                content: new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8,
                mediaType: "application/json")
                ).Result;
            string token = "";
            if (r.IsSuccessStatusCode)
            {
                string json = r.Content.ReadAsStringAsync().Result;
                var tokenResponse = JsonConvert.DeserializeObject<TokenResponseModel>(json);
                string tokenAuth = tokenResponse.access_token;
                token = tokenAuth;
            }
            else
            {
                token = string.Empty;
            }
            return token;
        }

        /// <summary>
        /// Запрос на вход пользователя, передающийся в API
        /// Данный запрос принимает модель UserLoginProp
        /// и возвращает строковую переменную с ответом,
        /// который будет включать в себя токен при удачном
        /// входе или пустую строку при неудачном входе.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string IsLogin(UserLoginProp model)
        {
            string url = $"https://localhost:7037/api/values/Authenticate/";
            string token = "";
            try
            {
                var r = httpClient.PostAsync(
                requestUri: url,
                content: new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8,
                mediaType: "application/json")
                ).Result;
                
                if (r.IsSuccessStatusCode)
                {
                    string json = r.Content.ReadAsStringAsync().Result;
                    var tokenResponse = JsonConvert.DeserializeObject<TokenResponseModel>(json);
                    string tokenAuth = tokenResponse.access_token;

                    token = tokenAuth;
                }
                else
                {
                    token = string.Empty;
                }
            }
            catch
            {
                token = "API не доступен";
            }
            return token;
        }

        /// <summary>
        /// Запрос для проверки валидности токена. Запрос пытается
        /// обратиться к API и если нет ошибки в блоке try (запрос 
        /// удался), то токен валидный. Если возникает ошибка, то 
        /// происходит переход в catch (токен не валидный).
        /// </summary>
        /// <returns></returns>
        public bool CheckToken()
        {
            string response = string.Empty;
            string url = @"https://localhost:7037/api/values/CheckToken";
            try
            {
                AddTokenHeaderMethod();
                response = httpClient.GetStringAsync(url).Result;
                if (response == "true")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Метод добавления токена в заголовок запроса.
        /// В методе происходит добавление токена в заголовок 
        /// запроса с помощью создания нового экземпляра заголовка 
        /// аутентификации AuthenticationHeaderValue.
        /// </summary>
        private void AddTokenHeaderMethod()
        {
            string tokenValue = GlobalVariables.token;
            if (!string.IsNullOrEmpty(tokenValue))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenValue);
                Console.WriteLine($"{tokenValue}");
            }
        }
        #endregion
    }
}
