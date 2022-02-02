using Interprit_Exam.DTO.Register;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
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
            Register postData = new Register { email= "eve.holt@reqres.in",password= "pistol"};

            string response = await validatePostResponseCodeAndGetResponseBody("https://reqres.in/api/register", postData, "OK");
            RegisterResponse registerResponse = JsonConvert.DeserializeObject<RegisterResponse>(response);

            Assert.IsNotNull(registerResponse.Token);
            Assert.IsNotNull(registerResponse.Id);
        }

        [TestMethod]
        //Verify POST unsuccessful register request
        public async Task TC2()
        {
            Register postData = new Register { email = "sydney@fife", password = null };

            string response = await validatePostResponseCodeAndGetResponseBody("https://reqres.in/api/register", postData, "BadRequest");
            RegisterError registerResponse = JsonConvert.DeserializeObject<RegisterError>(response);
            Assert.AreEqual(registerResponse.error, "Missing password");
        }

    }
}
