using BibliotecaWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaWeb.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View(DAOLibro.GetInstance().Top10());
        }
        public IActionResult AboutUs()
        {
            return View();
        }
        public IActionResult Contatti()
        {
            return View();
        }
    }
}
