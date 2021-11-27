using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CFarma_TemplateN.Controllers
{
    public class FooterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Legal()
        {
            return View();
        }
    }
}
