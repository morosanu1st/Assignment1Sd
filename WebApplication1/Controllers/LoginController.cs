using Bussiness.Models;
using Bussiness.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class LoginController : Controller
    {
        private UserService _userService = new UserService();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(UserModel model)
        {
            if (model.Username == "" || model.PasswordHash == "" || model.Username == null || model.PasswordHash == null)
            {
                ViewData["error"] = "Username and password can't be empty";

                return View();
            }
            var user = _userService.GetUserByUserName(model.Username);
            if (user == null)
            {
                ViewData["error"] = "Username not found";

                return View();
            }

            model.PasswordHash = _userService.HashPassword(model.PasswordHash);
            if (user.PasswordHash == model.PasswordHash)
            {
                if (user.IsAdmin)
                {

                    return Redirect("Admin");
                }
                else
                {


                    return Redirect("Cashier");
                }
            }
            ViewData["error"] = "Wrong password";

            return View();

        }
    }
}