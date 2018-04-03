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
    public class TicketRepositoryTests
    {
        private TicketRepository repo = new TicketRepository();

        [TestMethod()]
        public void TestTicket()
        {
            repo.EmptyTable();
            repo.InsertTicket(new DTO.Ticket { Row = 1, Seat = 1, ShowId = 1 });
            repo.InsertTicket(new DTO.Ticket { Row = 1, Seat = 2, ShowId = 1 });
            repo.InsertTicket(new DTO.Ticket { Row = 1, Seat = 2, ShowId = 2 });

            Assert.AreEqual(repo.GetTicektsByShowId(1).Count(),2);
        }

    }
}