using Bussiness.Models;
using Bussiness.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class CashierController : Controller
    {
        TicketService ticketService = new TicketService();
        ShowService showService = new ShowService();
        // GET: Cashier
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SellTicket(int? row, int? seat, int? showId)
        {
            if (row == 0 || seat == 0 || showId == 0 || row == null || seat == null || showId == null)
            {
                TempData["error"] = "invalid row, seat or showid";

                return RedirectToAction("Details", new { id = showId.GetValueOrDefault() });
            }
            var show = showService.GetShowById(showId.GetValueOrDefault());
            var ticket = new TicketModel { Row = row.GetValueOrDefault(), Seat = seat.GetValueOrDefault(), Show = show };
            ticketService.CreateTicket(ticket);            

            return RedirectToAction("Details", new { id = show.Id });
        }

        public ActionResult EditTicket(int? id, int? row, int? seat, int? showId)
        {
            if (row == 0 || seat == 0 || showId == 0 || row == null || seat == null || showId == null || id == null || id == 0)
            {
                TempData["errorEdit"] = "invalid row, seat or showid";

                return RedirectToAction("Details", new { id = showId.GetValueOrDefault() });
            }
            var show = showService.GetShowById(showId.GetValueOrDefault());
            var ticket = new TicketModel { Id = id.GetValueOrDefault(), Row = row.GetValueOrDefault(), Seat = seat.GetValueOrDefault(), Show = show };
            var changed=ticketService.EditSeat(ticket);
            if (!changed)
            {
                TempData["error"] = "something went bad when creating ticket";
            }

            return RedirectToAction("Details", new { id = show.Id });
        }

        

        public ActionResult ViewShows()
        {
            var shows = showService.GetAllShows();
            return View(shows);
        }

        public ActionResult Details(int id)
        {
            var show = showService.GetShowById(id);
            if (show == null)
            {

                return RedirectToAction("ViewShows");
            }
            TempData["remainingSeats"] = showService.TicketsLeftForShow(show);

            return View(show);
        }

        public ActionResult DeleteTicket(int id)
        {
            var ticket = ticketService.GetTicketById(id);
            var success = ticketService.DeleteTicket(ticket);
            return RedirectToAction("Details", new { id = ticket.Show.Id });
        }
    }
}