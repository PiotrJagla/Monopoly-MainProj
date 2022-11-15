using Models;
using Org.BouncyCastle.Asn1.Cmp;
using Services.Database_services;

namespace UnitTests
{
    [TestClass]
    public class DatabaseTests
    {
        [TestMethod]
        public void TestLoginValidation()
        {
            DBservice databaseService = new MySqlDBService();

            Assert.IsTrue(databaseService.UserExist(new UserLoginData("Piotr", "1234")) == true);
            Assert.IsTrue(databaseService.UserExist(new UserLoginData("Piotr", "12345")) == false);
            Assert.IsTrue(databaseService.UserExist(new UserLoginData("Piotrr", "12345")) == false);
            Assert.IsTrue(databaseService.UserExist(new UserLoginData("Ziemniak", "ziemniak")) == true);
        }
        
    }
}