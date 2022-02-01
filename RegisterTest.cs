using Interprit_Exam.DTO.Register;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://reqres.in/api/register");
            Register postData = new Register { Email = "eve.holt@reqres.in", Password = "pistol" };
            var content = new StringContent(JsonConvert.SerializeObject(postData), Encoding.UTF8, "application/json");

            using (HttpResponseMessage response = await client.PostAsync("posts", content))
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                Assert.AreEqual("Created", response.StatusCode.ToString());
            }
        }
                
        [TestMethod]
        //Verify POST unsuccessful register request
        public async Task TC2()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://reqres.in/api/register");
            Register postData = new Register  { Email = "sydney@fife"};
            var content = new StringContent(JsonConvert.SerializeObject(postData), Encoding.UTF8, "application/json");

            using (HttpResponseMessage response = await client.PostAsync(client.BaseAddress, content))
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;

                Assert.AreEqual("BadRequest", response.StatusCode.ToString());
                Assert.AreEqual("{\"error\":\"Missing email or username\"}", responseContent.ToString());
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
