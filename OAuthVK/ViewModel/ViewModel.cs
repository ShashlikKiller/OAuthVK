using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net;
using static OAuthVK.Model;

namespace OAuthVK
{
    public class ViewModel
    {
        private const string appId = "51781137";
        private const string display = "page";
        private const string redirect_uri = "https://oauth.vk.com/blank.html";
        private const string scope = "offline";
        private static string access_token;
        private static string userID;
        private static string GetAccessToken()
        {
            return access_token;
        }

        public static string GetUriStr()
        {
            return $"https://oauth.vk.com/authorize?client_id={appId}&display={display}" +
           $"&redirect_uri={redirect_uri}&scope={scope}&v=5.6&response_type=token";
        }
        
        public static void GetUserConfInf(string _access_token, string _userID)
        {
            access_token = _access_token;
            userID = _userID;
        }

        public static string GET(string Url, string Method) // Убрал передачу токена каждый раз при вызове метода. Токен берется из переменной
        {
            WebRequest req = WebRequest.Create(String.Format(Url, Method, GetAccessToken()));
            WebResponse resp = req.GetResponse();
            Stream stream = resp.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            string Out = sr.ReadToEnd();
            return Out;
        }

        #region Methods working with the API
        /// <summary>
        /// https://dev.vk.com/ru/method/account.getProfileInfo
        /// </summary>
        /// <returns></returns>
        public static string GetUserData()
        {
            string reqStrTemplate = "https://api.vk.com/method/{0}?access_token={1}&v=5.154";
            string method = "account.getProfileInfo";
            string f = GET(reqStrTemplate, method);
            ResponseUser _user = JsonConvert.DeserializeObject<ResponseUserObject>(f).response;
            if (_user.status == "") _user.status = "Статус отсутствует.";
            string _user_sex = "";
            switch (_user.sex)
            {
                case 0:
                    _user_sex = "Пол не определен.";
                    break;
                case 1:
                    _user_sex = "Женский";
                    break;
                case 2:
                    _user_sex = "Мужской";
                    break;
            }
            string[] list =
            {
                "id: " + _user.id.ToString(),
                "Статус: " + _user.status,
                "Фамилия: " + _user.last_name,
                "Имя: " + _user.first_name,
                "Дата рождения: " + _user.bdate,
                "Родной город:" + _user.home_town,
                "Номер телефона: " + _user.phone,
                "Пол: " + _user_sex,
                "Страна: " + _user.country.title
            };
            return string.Join("\n", list);
        }

        /// <summary>
        /// https://dev.vk.com/ru/method/account.getInfo
        /// </summary>
        /// <returns></returns>
        public static string GetAccountData()
        {
            string reqStrTemplate = "https://api.vk.com/method/{0}?access_token={1}&v=5.154";
            string method = "account.getInfo";
            string f = GET(reqStrTemplate, method);
            ResponseAccount _responseObj = JsonConvert.DeserializeObject<ResponseAccountObject>(f).response;
            string _2fauth = "";
            switch (_responseObj.Twofact_auth)
            {
                case 0:
                    _2fauth = "не включена.";
                    break;
                case 1:
                    _2fauth = "включена.";
                    break;
            }
            string _nowallreplies = "";
            switch (_responseObj.no_wall_replies)
            {
                case 0:
                    _nowallreplies = "включено.";
                    break;
                case 1:
                    _nowallreplies = "отключено.";
                    break;
            }
            string[] list =
            {
                "Страна, из которой сделан запрос: " + _responseObj.country,
                "Интежер языка: " + _responseObj.lang,
                "Двухфакторная аутентификация пользователя " + _2fauth,
                "Комментирование записей на странице " + _nowallreplies
            };
            return string.Join("\n", list);
        }

        /// <summary>
        /// https://dev.vk.com/ru/method/account.getBanned
        /// </summary>
        /// <returns></returns>
        public static string GetBannedData()
        {
            string reqStrTemplate = "https://api.vk.com/method/{0}?access_token={1}&v=5.154";
            string method = "account.getBanned";
            string f = GET(reqStrTemplate, method);
            ResponseBanned _responseObj = JsonConvert.DeserializeObject<ResponseBannedObject>(f).response;
            ResponseUser[] profilesArray = _responseObj.Profiles;
            int _count = _responseObj.Count;
            string _answer = $"Количество людей, связанных с вашим ЧС: {_count}\n \n";
            _answer += string.Join("\n", profilesArray.Select(x => "id: " + x.id + "\n" +
            "   ФИО: " + x.last_name + " " + x.first_name + "\n"
            + "   Вы в ЧС: " + x.is_closed.ToString() + "; У Вас в ЧС: " + x.can_access_closed + "; Статус аккаунта: " + x.deactivated));
            return _answer;
        }
        #endregion
    }
}
