using Interprit_Exam.DTO.UserList;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Interprit_Exam
{
    [TestClass]
    public class UserTest:Base
    {
        public string URI = "https://reqres.in/";

        [TestMethod]
        //Verify GET User response if single user found
        public void TC1()
        {
            var test = getResponseData("2").Result.data.GetValue(0);
            Assert.AreEqual(getResponse("Get",URI + "api/users/2"), "OK");
        }

        [TestMethod]
        //Verify GET User response if single user not found
        public void TC2()
        {
            Assert.AreEqual(getResponse("Get", URI + "api/users/23"), "NotFound");
        }

        [TestMethod]
        //Verify POST User response
        public void TC3()
        {
            CreateUser payLoad = new CreateUser(){ Name= "morpheus", Job = "leader"};

            Assert.AreEqual("Created",getResponse("post",URI + "api/users", payLoad));
        }

        [TestMethod]
        //Verify PUT User response
        public void TC4()
        {
            UpdateUser payLoad = new UpdateUser() { Name = "morpheus", Job = "zion resident" };

            Assert.AreEqual(getResponse("put",URI + "api/users", payLoad), "OK");
        }

        [TestMethod]
        //Verify PATCH User response
        public void TC5()
        {
            UpdateUser payLoad = new UpdateUser() { Name = "morpheus", Job = "zion resident" };

            Assert.AreEqual(getResponse("patch",URI + "api/users", payLoad), "OK");
        }

        [TestMethod]
        //Verify DELETE User response if single user not found
        public void TC6()
        {
            Assert.AreEqual(getResponse("Delete", URI + "api/users/2"), "NoContent");
        }

        [TestMethod]
        //Verify delayed response
        public void TC7()
        {
            Assert.AreEqual(getResponse("Get", URI + "api/users?delay=3"), "OK");
        }
        
    }
}
