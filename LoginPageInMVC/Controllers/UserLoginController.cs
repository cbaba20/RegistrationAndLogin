using LoginPageInMVC.Models;
using System.Linq;
using System.Web.Mvc;
namespace LoginPageInMVC.Controllers
{
    public class UserLoginController : Controller
    {
        [HttpGet]
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(User userModel)
        {
            using (LoginModelDbEntities db = new LoginModelDbEntities())
            {
                var userDetails = db.Users.Where(z => z.UserName == userModel.UserName && z.Password == userModel.Password).FirstOrDefault();
                if (userDetails == null)
                {
                    userModel.LoginErrorMessage = "Wrong User Name or Password.";
                    return View("Index", userModel);
                }
                else
                {
                    Session["UserID"] = userDetails.UserId;
                    Session["UserName"] = userDetails.UserName;
                    return RedirectToAction("Index","Home");
                }
            }
        }
        public ActionResult LogOut()
        {
            int userId = (int)Session["userID"];
          
            Session.Abandon();
            return RedirectToAction("Index", "UserLogin");
        }
    }
}