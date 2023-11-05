using Newtonsoft.Json;

namespace OAuthVK
{
    internal class Model
    {
        public class ResponseUser
        {
            [JsonProperty("id")]
            public int id { get; set; }
            [JsonProperty("status")]
            public string status { get; set; }
            [JsonProperty("last_name")]
            public string last_name { get; set; }
            [JsonProperty("first_name")]
            public string first_name { get; set; }
            [JsonProperty("bdate")]
            public string bdate { get; set; }
            [JsonProperty("home_town")]
            public string home_town { get; set; }
            [JsonProperty("phone")]
            public string phone { get; set; }
            [JsonProperty("sex")]
            public int sex { get; set; }
            [JsonProperty("country")]
            public Country country { get; set; }
            [JsonProperty("deactivated")]
            public string deactivated { get; set; }
            [JsonProperty("can_access_closed")]
            public bool can_access_closed { get; set; }
            [JsonProperty("is_closed")]
            public bool is_closed { get; set; }
        }

        public class ResponseUserObject
        {
            public ResponseUser response { get; set; }
        }

        public class ResponseAccount
        {
            [JsonProperty("country")]
            public string country { get; set; }
            [JsonProperty("lang")]
            public int lang { get; set; }
            [JsonProperty("2fa_required")]
            public int Twofact_auth { get; set; }
            [JsonProperty("no_wall_replies")]
            public int no_wall_replies { get; set; }
        }

        public class ResponseAccountObject
        {
            public ResponseAccount response { get; set; }
        }

        public class ResponseBanned
        {
            [JsonProperty("count")]
            public int Count { get; set; }

            [JsonProperty("items")]
            public object[] Items { get; set; }

            [JsonProperty("profiles")]
            public ResponseUser[] Profiles { get; set; }
        }

        public class ResponseBannedObject
        {
            public ResponseBanned response { get; set; }
        }

        public class Country
        {
            public int id { get; set; }
            public string title { get; set; }
        }
    }
}
