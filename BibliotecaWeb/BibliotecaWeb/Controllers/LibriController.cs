using BibliotecaWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaWeb.Controllers
{
    public class LibriController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Elenco(string tipo = "id DESC")
        {
            return View(DAOLibro.GetInstance().OrdinaPer(tipo));
        }
        public IActionResult AggiungiAiPreferiti(int idLibro, int idUtente)
        {
            Console.WriteLine("Metodo Aggiungi ai preferiti controller: " + idLibro + " " + idUtente);
            Console.WriteLine("Metodo Aggiungi ai preferiti utente loggato: " + LoginController.utenteLoggato.ToString());
            DAOUtente.GetInstance().InsertPreferiti(idLibro, idUtente);
            LoginController.utenteLoggato.Preferiti = DAOLibro.GetInstance().LibriPreferiti(idUtente);
            foreach (Libro l in DAOLibro.GetInstance().LibriPreferiti(idUtente))
                Console.WriteLine("Libro preferito: " + l.ToString());
            return View("Views/Libri/Elenco.cshtml", DAOLibro.GetInstance().OrdinaPer("id desc"));
        }
        public IActionResult RimuoviDaiPreferiti(int idLibro, int idUtente)
        {
            Console.WriteLine("Metodo Aggiungi ai preferiti controller: " + idLibro + " " + idUtente);
            DAOUtente.GetInstance().RimuoviPreferiti(idLibro, idUtente);
            LoginController.utenteLoggato.Preferiti = DAOLibro.GetInstance().LibriPreferiti(idUtente);
            foreach(Libro l in DAOLibro.GetInstance().LibriPreferiti(idUtente))
                Console.WriteLine("Libro preferito: " + l.ToString());
            return Redirect("/Libri/Elenco");
        }
        public IActionResult Dettagli(int id)
        {
            return View(DAOLibro.GetInstance().Find(id));
        }

        public IActionResult FormModifica(int id)
        {
            Libro l = (Libro)DAOLibro.GetInstance().Find(id);
            return View(l);
        }
        public IActionResult ModificaLibro(Dictionary<string, string> parametri)
        {
            foreach (KeyValuePair<string, string> kvp in parametri)
                Console.WriteLine(kvp.Key + "=" + kvp.Value);
            Libro l = new Libro();
            l.FromDictionary(parametri);
            Console.WriteLine(l.ToString());
            if (DAOLibro.GetInstance().Update(l))
            {
                return Redirect("/Libri/Elenco");
            }
            else return Content("Modifica Fallita");
        }
        public IActionResult NuovoLibro()
        {
            return View();
        }

        public IActionResult AggiungiLibro(Dictionary<string,string> parametri)
        {
            Libro l = new Libro();
            l.FromDictionary(parametri);
            if (DAOLibro.GetInstance().Insert(l))
            {
                return Redirect("/Libri/Elenco");
            }
            else return Content("Inserimento Fallito");
        }
        public IActionResult PrendiPrestito(int idLibro, int idUtente)
        {
            int c = 0;
            foreach (Prestito p in DAOPrestito.GetInstance().ListaPrestiti(idUtente))
            {
                if (p.DataConsegna.Year == 0001)
                    c++;
            }
            if (c < 2)
            {
                DAOUtente.GetInstance().PrendiPrestito(idLibro, idUtente);

                return Redirect("/Libri/Elenco");
            }
            else
                return Content("Hai già due libri, \nconsegnane uno prima di poterne prendere ancora");
        }
        public IActionResult ConsegnaPrestito(int idLibro, int idUtente)
        {
            DAOUtente.GetInstance().ConsegnaPrestito(idLibro, idUtente);

            return Redirect("/Libri/Elenco");
        }
        public IActionResult Elimina(int id)
        {
            DAOLibro.GetInstance().Delete(DAOLibro.GetInstance().Find(id));
            return Redirect("/Libri/Elenco");
        }
    }
}
