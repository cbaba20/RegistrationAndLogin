using LoginPageInMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoginPageInMVC.Controllers
{
    public class RegistrationController : Controller
    {
        // GET: Registration
        public ActionResult Index(int id = 0)
        {
            User userModel = new User();
            return View(userModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(User userModel)
        {
            //User userModel = new User();
            //return View(userModel);

            using (LoginModelDbEntities dbmodel = new LoginModelDbEntities())
            {
                if (ModelState.IsValid)
                {


                    if (dbmodel.Users.Any(x => x.UserName == userModel.UserName))
                    {
                        ViewBag.DuplicateMessage = "User Name already Exists,Please Login.";
                        return View("Index", userModel);

                    }
                    dbmodel.Users.Add(userModel);
                    dbmodel.SaveChanges();
                }
                ModelState.Clear();
                ViewBag.Success = "Added Successfully";
                return View("Index", new User());
            }
        }
    }

}