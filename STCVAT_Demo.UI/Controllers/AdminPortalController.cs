using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STCVAT_Demo.UI.Controllers
{
    public class AdminPortalController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
