using Bussiness;
using Bussiness.Models;
using Bussiness.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class AdminController : Controller
    {
        ShowService showService = new ShowService();
        UserService userService = new UserService();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Cashiers()
        {
            var users = userService.GetAllUsers().Where(cashier => cashier.IsAdmin == false).ToList();

            return View(users);
        }

        public ActionResult CreateCashier()
        {

            return View();
        }

        [HttpPost]
        public ActionResult CreateCashier(UserModel cashier)
        {
            if (userService.GetUserByUserName(cashier.Username) != null )
            {
                TempData["error"] = "the username already exists";

                return RedirectToAction("CreateCashier");
            }

            if( cashier.Username == null || cashier.PasswordHash == null || cashier.FirstName == null || cashier.LastName == null)
            {
                TempData["error"] = "All the fields are mandatory";

                return RedirectToAction("CreateCashier");
            }

            cashier.PasswordHash = userService.HashPassword(cashier.PasswordHash);
            userService.CreatetUser(cashier);

            return RedirectToAction("Cashiers");
        }

        public ActionResult DeleteCashier(int id)
        {
            userService.DeleteUser(new UserModel { Id = id });

            return RedirectToAction("Cashiers");
        }

        public ActionResult EditCashier(int id)
        {

            return View(new UserModel { Id = id });
        }

        [HttpPost]
        public ActionResult EditCashier(UserModel cashier)
        {
            cashier.PasswordHash = userService.HashPassword(cashier.PasswordHash);
            var v = userService.UpdateUser(cashier);
            if (!v)
            {
                TempData["error"] = "The user already exists";

                return View(new UserModel { Id = cashier.Id });
            }

            return RedirectToAction("Cashiers");
        }

        public ActionResult Shows()
        {
            var shows = showService.GetAllShows();

            return View(shows);
        }

        public ActionResult CreateShow()
        {

            return View();
        }

        [HttpPost]
        public ActionResult CreateShow(ShowModel show)
        {
            if (show.Title == null  || show.Seats <= 0 || show.Day == null)
            {
                TempData["error"] = "all fields are mandatory";

                return View();
            }

            if (show.Seats > Constants.Rows * Constants.Seats)
            {
                TempData["error"] = "maximum number of seats is "+ Constants.Rows * Constants.Seats;

                return View();
            }

            if (showService.GetShowByDay(show.Day) != null)
            {
                TempData["error"] = "There is a show that day";

                return View();
            }

            showService.CreateShow(show);

            return RedirectToAction("Shows");
        }

        public ActionResult DeleteShow(int id)
        {
            showService.DeleteShow(new ShowModel { Id = id });

            return RedirectToAction("Shows");
        }

        public ActionResult EditShow(int id)
        {
            return View(showService.GetShowById(id));
        }

        [HttpPost]
        public ActionResult EditShow(ShowModel show)
        {
            var update = showService.UpdateShow(show);
            if (!update)
            {
                TempData["error"] = "There is a show that day";

                return View();
            }

            return RedirectToAction("Shows");

        }

        public ActionResult Export(int id,string extension)
        {
            showService.ExportShow(showService.GetShowById(id), extension);

            return RedirectToAction("Shows");
        }
    }
}