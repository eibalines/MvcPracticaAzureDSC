using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcPracticaAzureDSC.Controllers
{
    public class SeriesClienteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CrearSerie()
        {
            return View();
        }
    }
}
