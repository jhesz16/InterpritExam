using HttpClientExtensions.Patch;
using Interprit_Exam.DTO.Register;
using Interprit_Exam.DTO.Resources;
using Interprit_Exam.DTO.UserList;
using Interprit_Exam.DTO.Users;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Interprit_Exam
{
    public class Base
    {
        private static HttpClient restClient = new HttpClient();
        private static string URI = "https://reqres.in/api/";
        private static string response;

        public static string getResponse(string action, string url, object payLoad)
        {
            switch (action.ToUpper())
            {
                case "POST":
                    var pl = new StringContent(JsonConvert.SerializeObject(payLoad), Encoding.UTF8, "application/json");
                    response = restClient.PostAsync(url, pl).Result.StatusCode.ToString();
                    break;
                case "PUT":
                    response = restClient.PutAsJsonAsync(url, payLoad).Result.StatusCode.ToString();
                    break;
                case "PATCH":
                    response = restClient.PatchAsJsonAsync(url, payLoad).Result.StatusCode.ToString();
                    break;
                default:
                    break;
            }
            return response;
        }

        public static string getResponse(string action, string url)
        {
            switch (action.ToUpper())
            {
                case "GET":
                    response = restClient.GetAsync(url).Result.StatusCode.ToString();
                    break;
                case "DELETE":
                    response = restClient.DeleteAsync(url).Result.StatusCode.ToString();
                    break;
                default:
                    break;
            }
            return response;
        }

        public static async Task<Users> getResponseRootUser(string user)
        {
            var response = await restClient.GetStringAsync(URI + "users/" + user);
            return JsonConvert.DeserializeObject<Users>(response);
        }

        public static async Task<Resources> getResponseRootResource(string resource)
        {
            var response = await restClient.GetStringAsync(URI + "unknown/" + resource);
            return JsonConvert.DeserializeObject<Resources>(response);
        }

        public static async Task<UserList.root> getResponseData(string page)
        {
            var response = await restClient.GetStringAsync(URI + "users?page=" + page);
            return JsonConvert.DeserializeObject<UserList.root>(response);
        }

        public WebRequest getPostReqObj(string url)
        {
            WebRequest requestObjPost = WebRequest.Create(String.Format(url));
            requestObjPost.Method = "POST";
            requestObjPost.ContentType = "application/json";
            return requestObjPost;
        }

    }
}
