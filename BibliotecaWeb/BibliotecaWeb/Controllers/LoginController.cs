using BibliotecaWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaWeb.Controllers
{
    public class LoginController : Controller
    {
        private ILogger<LoginController> il;

        public static Utente utenteLoggato = null;
        public static Utente amministratoreLoggato = null;
        private static int chiamata = -1;
        public LoginController(ILogger<LoginController> l) {
            il = l;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            //Ogni volta che passo per la pagina index di login conto un tentativo
            chiamata++;

            // Salviamo nel fogio di note che tentativo di accesso stiamo provando
            il.LogInformation($"Tentativo numero: {chiamata}");

            // Chiamo il file index.cshtml in cui si può tentare il login e informo la pagina del tentativo in cui si trova l'utente
            return View(chiamata);
        }


        public IActionResult Valida(Dictionary<string, string> parametri) {
            if (DAOUtente.GetInstance().Valida(parametri["username"], parametri["passw"])) {
                il.LogInformation($"Utente loggato: {parametri["username"]}");


                Console.WriteLine(parametri["username"]);
                if (((Utente)DAOUtente.GetInstance().Find(parametri["username"])).Amministratore)
                {
                    il.LogInformation($"Utente loggato: {parametri["username"]}");
                    amministratoreLoggato = (Utente)DAOUtente.GetInstance().Find(parametri["username"]);
                    utenteLoggato = null;
                    return Redirect("/MyAccount/MyAccountAdmin");
                }
                else if (((Utente)DAOUtente.GetInstance().Find(parametri["username"])).Amministratore == false)
                {
                    il.LogInformation($"Utente loggato: {parametri["username"]}");
                    utenteLoggato = (Utente)DAOUtente.GetInstance().Find(parametri["username"]);
                    amministratoreLoggato = null;
                    Console.WriteLine((utenteLoggato == null ? "\n\n\nNessun utente loggato" : "\n\n\n" + utenteLoggato.Username));
                    return Redirect("/MyAccount/MyAccountUtente");

                }
                else
                {
                    return Redirect("Login");
                }


            }
            return Redirect("Login");
        } // Fatto
        public IActionResult Registrazione()
        {
            return View();
        }
        public IActionResult RegistrazioneAdmin()
        {
            return View();
        }
        public IActionResult Salva(Dictionary<string, string> parametri)
        {
            Utente utente = new Utente();
            utente.FromDictionary(parametri);

            chiamata--;

            if (DAOUtente.GetInstance().Insert(utente))
            {
                return View("/Views/Login/ConfermaRegistrazione.cshtml");
            }
            else
            {
                return Content("Registrazione Fallita");
            }
        }
        public IActionResult ModificaProfiloUtente(Dictionary<string, string> parametri)
        {
            Utente utente = new Utente();
            utente.FromDictionary(parametri);
            if (DAOUtente.GetInstance().Update(utente))
            {
                return Redirect("/MyAccount/MyAccountUtente");
            }
            else
            {
                return Content("Registrazione Fallita");
            }
        }
        public IActionResult ModificaProfiloAdmin(Dictionary<string, string> parametri)
        {
            Utente utente = new Utente();
            utente.FromDictionary(parametri);
            if (DAOUtente.GetInstance().Update(utente))
            {
                return Redirect("/MyAccount/MyAccountAdmin");
            }
            else
            {
                return Content("Registrazione Fallita");
            }
        }
        public IActionResult Logout()
        {
            chiamata = -1;
            if(utenteLoggato != null)
                il.LogInformation($"Logout: {utenteLoggato.Username}");
            else if (amministratoreLoggato != null)
                il.LogInformation($"Logout: {amministratoreLoggato.Username}");

            utenteLoggato = null;
            amministratoreLoggato = null;

            return Redirect("/Home/Index");
        }
        public IActionResult Log() { // IAction per capire se mettere il menù a tendina e capire quale mettere.
            if (amministratoreLoggato != null)
                return View(); // Se trova l'amministratore apre il AmministratoreMenu
            else if(utenteLoggato != null)
                return View(); // Se trova l'utente apre il UtenteMenu
            return null; // Se entrambi sono null non ritorna niente, quindi non fa niente
        }
        
        public IActionResult RecuperoCredenziali()
        {
            return View();
        }
        public IActionResult FormRecuperoCredenziali(Dictionary<string, string> parametri)
        {
            Utente utenteVeccio = (Utente)DAOUtente.GetInstance().Find(parametri["user"]);

            Utente utenteNuovo = utenteVeccio;
            utenteNuovo.Passw = parametri["passw"];

            if (parametri["passw"] == parametri["passw2"])
            {
                if (DAOUtente.GetInstance().UpdatePassword(utenteNuovo.Id, parametri["passw"]))
                    return Redirect("/Home/Index");
                else return Content("Modifica fallita");
            }
            else return Content("Modifica non partita");
        }
        public IActionResult VerificaCredenziali(Dictionary<string, string> parametri) {
            bool verificato = false;
            foreach (Utente u in DAOUtente.GetInstance().ReadAll())
            {
                if (u.Username == parametri["username"]) 
                {
                    verificato = false;
                    break;
                }
                else if (u.Email == parametri["email"])
                {
                    verificato = false;
                    break;
                }
                else
                {
                    verificato = true;
                }
            }
            if(verificato)
            {
                string url = "?";
                foreach (KeyValuePair<string, string> kvp in parametri)
                    url += kvp.Key + "=" + kvp.Value + "&";
                string url2 = url.Substring(0, url.Length - 1);
                return Redirect($"/Login/Salva{url2}");
            }
            else
            { return Redirect("/Login/Registrazione"); }

        }
        public IActionResult ModificaPassword() { return View(); }
    }
}
