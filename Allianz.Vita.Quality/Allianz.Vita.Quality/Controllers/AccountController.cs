using Allianz.Vita.Quality.Models;
using System.Web.Mvc;
using System.Web.Security;

public class AccountController : Controller
{
    [HttpPost]
    public ActionResult SignIn(SignInViewModel model)
    {
        if (ModelState.IsValid)
        {
            //if (Membership.ValidateUser(model.UserName, model.Password))
            if (!string.IsNullOrEmpty(model.UserName))
            {
                FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                return RedirectToAction("Index", "Home", new { });
            }
            else
            {
                ModelState.AddModelError("", "The user name or password provided is incorrect.");
            }
        }

        return View(model);
    }


    [HttpPost]
    public ActionResult LogOn(SignInViewModel model)
    {
        if (ModelState.IsValid)
        {
            //if (Membership.ValidateUser(model.UserName, model.Password))
            if(!string.IsNullOrEmpty(model.UserName))
            {
                FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                return RedirectToAction("Index", "Home", new { });
            }
            else
            {
                ModelState.AddModelError("", "The user name or password provided is incorrect.");
            }
        }

        return View(model);
    }

    [HttpPost]
    public ActionResult UpdateCredentials(CredentialsViewModel model)
    {
        if (ModelState.IsValid)
        {
            //if (Membership.ValidateUser(model.UserName, model.Password))
            if (!string.IsNullOrEmpty(model.UserName))
            {
                FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                return RedirectToAction("Index", "Home", new { });
            }
            else
            {
                ModelState.AddModelError("", "The user name or password provided is incorrect.");
            }
        }

        return View(model);
    }


    public ActionResult LogOff()
    {
        return RedirectToAction("Index", "Home");
    }
}