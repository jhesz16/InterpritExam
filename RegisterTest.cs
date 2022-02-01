using Interprit_Exam.DTO.Register;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using RestSharp;
using System.Net;
using System.IO;

namespace Interprit_Exam
{
    [TestClass]
    public class RegisterTest : Base
    {
        /*

        
        
        Verify POST Login request
        Verify POST unsuccessful Login request
        Verify Get delayed response*/

        [TestMethod]
        //Verify POST register request
        public async Task TC1()
        {
            string res;
            Register postData = new Register { email= "eve.holt@reqres.in",password= "pistol"};
            string postDataSerialized = JsonConvert.SerializeObject(postData);

            WebRequest requestObjPost = getPostReqObj("https://reqres.in/api/register");

            using (var streamWriter = new StreamWriter(requestObjPost.GetRequestStream()))
            {
                streamWriter.Write(postDataSerialized);
                streamWriter.Flush();
                streamWriter.Close();

                var httpResponse = (HttpWebResponse)requestObjPost.GetResponse();
                Assert.AreEqual("OK", httpResponse.StatusCode.ToString());

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    res = streamReader.ReadToEnd();
                }
            }

            Assert.IsNotNull(JsonConvert.DeserializeObject(res));            
        }


        [TestMethod]
        //Verify POST unsuccessful register request
        public async Task TC2()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://reqres.in/api/register");
            Register postData = new Register { email = "eve.holt@reqres.in", password = null };
            using (HttpResponseMessage response = await client.PostAsJsonAsync("https://reqres.in/api/register", postData))
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;

                Assert.AreEqual("BadRequest", response.StatusCode.ToString());
                Assert.AreEqual("{\"error\":\"Missing password\"}", responseContent.ToString());
            }
        }
        [TestMethod]
        //Verify POST unsuccessful register request
        public async Task TC3()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://reqres.in/api/login");
            var postData = new {Email= "eve.holt@reqres.in",Password= "cityslicka"};

            var content = new StringContent(JsonConvert.SerializeObject(postData), Encoding.UTF8, "application/json");


            //string test = getResponse("Post", "https://reqres.in/api/login", content);

            using (HttpResponseMessage response = await client.PostAsync("posts", content))
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;

                Assert.AreEqual("BadRequest", response.StatusCode.ToString());
                Assert.AreEqual("{\"error\":\"Missing email or username\"}", responseContent.ToString());
            }
        }
    }
}
