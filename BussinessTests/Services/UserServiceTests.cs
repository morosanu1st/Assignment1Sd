using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bussiness.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Services.Tests
{
    [TestClass()]
    public class UserServiceTests
    {
        private UserService userService = new UserService();

        [TestMethod()]
        public void HashPasswordTest()
        {
            userService.CreatetUser(new Models.UserModel { Username="admin",PasswordHash=userService.HashPassword("admin"),FirstName="admin",LastName="admin"});
            var random = new Random().Next().ToString();
            Assert.AreEqual(userService.HashPassword(random).Count(), 64);
        }
    }
}