using BibliotecaWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaWeb.Controllers
{
    public class MyAccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult MyAccountUtente()
        {
            return View(DAOUtente.GetInstance().Find(LoginController.utenteLoggato.Id));
        }
        public IActionResult MyAccountAdmin()
        {
            return View(DAOUtente.GetInstance().Find(LoginController.amministratoreLoggato.Id));
        }
        
        public IActionResult ModificaProfiloUtente() 
        { 
            return View(); 
        }
        public IActionResult ModificaProfiloAdmin()
        {
            return View();
        }
        public IActionResult PreferitiUtente()
        {
            return View(DAOLibro.GetInstance().LibriPreferiti(LoginController.utenteLoggato.Id));
        }
        public IActionResult StoricoPrestiti()
        {
            return View(DAOPrestito.GetInstance().ListaPrestiti());
        }
        public IActionResult ListaUtenti()
        {
            return View(DAOUtente.GetInstance().ReadAll());
        }
        public IActionResult EliminaProfilo(int id) 
        {
            DAOUtente.GetInstance().Delete(DAOUtente.GetInstance().Find(id));
            return Redirect("/Login/Logout");
        }
    }
}
