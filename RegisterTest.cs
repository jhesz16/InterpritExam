using Interprit_Exam.DTO.Register;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Interprit_Exam
{
    [TestClass]
    public class RegisterTest : Base
    {
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
            RegisterResponse successregisterResponse = JsonConvert.DeserializeObject<RegisterResponse> (res);
            
            Assert.IsNotNull(successregisterResponse.Token);
            Assert.IsNotNull(successregisterResponse.Id);
        }

        [TestMethod]
        //Verify POST unsuccessful register request
        public async Task TC2()
        {
            var client = new HttpClient();
            Register postData = new Register { email = "eve.holt@reqres.in", password = null };
            RegisterError expResponse = new RegisterError { error = "Missing password"};

            using (HttpResponseMessage response = await client.PostAsJsonAsync("https://reqres.in/api/register", postData))
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;

                Assert.AreEqual("BadRequest", response.StatusCode.ToString());
                Assert.AreEqual(JsonConvert.SerializeObject(expResponse), responseContent.ToString());
            }
        }

        [TestMethod]
        [Description("Verify POST Login request")]
        public async Task TC3()
        {
            var client = new HttpClient();
            Register postData = new Register { email = "eve.holt@reqres.in", password = "cityslicka"};
            RegisterError expResponse = new RegisterError { error = "Missing email or username" };

            using (HttpResponseMessage response = await client.PostAsJsonAsync("https://reqres.in/api/login", postData))
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;

                Assert.AreEqual(response.StatusCode.ToString(), "OK");
            }
        }
        [TestMethod]
        [Description("Verify POST unsuccessful Login request")]
        public async Task TC4()
        {
            var client = new HttpClient();
            Register postData = new Register { email = "peter@klaven", password = null };
            RegisterError expResponse = new RegisterError { error = "Missing email or username" };

            using (HttpResponseMessage response = await client.PostAsJsonAsync("https://reqres.in/api/login", postData))
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;

                Assert.AreEqual(response.StatusCode.ToString(), "BadRequest");
            }
        }
    }
}
