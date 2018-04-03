using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccsess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccsess.Repositories.Tests
{
    [TestClass()]
    public class ShowRepositoryTests
    {
        ShowRepository repo = new ShowRepository();

        [TestMethod()]
        public void TestShowRepo()
        {
            repo.EmptyTable();
            repo.InsertShow(new DTO.Show { Title = "asd", Distribution = "khda asdhk dahs", Genre = 2, Day = DateTime.Now.AddDays(5) });
            repo.InsertShow(new DTO.Show { Title = "asd", Distribution = "dasd", Genre = 2, Day = DateTime.Now });
            var shows = repo.GetShowByDay(DateTime.Now);

            Assert.AreNotEqual(shows,null);
        }

    }
}