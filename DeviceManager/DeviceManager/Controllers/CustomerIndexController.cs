using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DeviceManager.Controllers
{
    public class CustomerIndexController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}