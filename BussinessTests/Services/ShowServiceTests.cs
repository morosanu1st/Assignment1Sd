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
    public class ShowServiceTests
    {
        private TicketService ticketService = new TicketService();
        private ShowService showService = new ShowService();

        [TestMethod()]
        public void ServiceTest()
        {
            var shows = showService.GetAllShows();
            foreach (var show in shows)
            {
                var tl = showService.TicketsLeftForShow(show);
                Assert.IsTrue(tl >= 0 && tl <= Constants.Rows * Constants.Seats);
            }

        }       
    }
}