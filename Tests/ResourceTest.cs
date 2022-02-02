using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Interprit_Exam
{
    [TestClass]
    public class ResourceTest:Base
    {

        [TestMethod]
        //Verify GET Resouce request by page
        public void TC1()
        {
            Assert.AreEqual(getResponse("get", "https://reqres.in/api/unknown"), "OK");
        }

        [TestMethod]
        //Verify GET User response if single user found
        public void TC2()
        {
           Assert.AreEqual(getResponse("get", "https://reqres.in/api/unknown/2"),"OK");
        }

        [TestMethod]
        //Verify GET User response if single user not found
        public void TC3()
        {
           Assert.AreEqual(getResponse("get", "https://reqres.in/api/unknown/23"), "NotFound");
        }
        
    }
}
