using Interprit_Exam.DTO.Login;
using Interprit_Exam.DTO.Register;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Interprit_Exam
{
    [TestClass]
    public class LogInTest : Base
    {
        [TestMethod]
        [Description("Verify POST Login request")]
        public async Task TC1()
        {
            Register postData = new Register { email = "eve.holt@reqres.in", password = "cityslicka"};

            string response = await validatePostResponseCodeAndGetResponseBody("https://reqres.in/api/login", postData, "OK");
            Login LoginResponse = JsonConvert.DeserializeObject<Login>(response);

            Assert.IsNotNull(LoginResponse.Token);
        }

        [TestMethod]
        [Description("Verify POST unsuccessful Login request")]
        public async Task TC2()
        {
            Register postData = new Register { email = "peter@klaven", password = null };

            string response = await validatePostResponseCodeAndGetResponseBody("https://reqres.in/api/login",postData, "BadRequest");
            RegisterError errorregisterResponse = JsonConvert.DeserializeObject<RegisterError>(response);

            Assert.AreEqual(errorregisterResponse.error, "Missing password");
        }

    }
}
