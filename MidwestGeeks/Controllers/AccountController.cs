using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceStack.Mvc;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;

namespace MidwestGeeks.Controllers
{
    [Authenticate]
    public class AccountController : ServiceStackController<AuthUserSession>
    {
        public ActionResult Index()
        {
            return View();
        }

        public override string LoginRedirectUrl
        {
            get
            {
                return "/Home";
            }
        }

    }
}
