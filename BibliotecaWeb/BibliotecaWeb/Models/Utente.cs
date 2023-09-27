using Utility;

namespace BibliotecaWeb.Models
{
    public class Utente : Entity
    {
        public string Username { get; set; }
        public string Passw { get; set; }   
        public string Email { get; set; }
        public bool Amministratore { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public DateTime Dob { get; set; }
        public string Indirizzo { get; set; }
        public List<Libro> Preferiti { get; set; }
        public Utente() { }

        public Utente(int id, string username, string passw, string email, bool amministratore, string nome, string cognome, DateTime dob, string indirizzo, List<Libro> preferiti)
                        : base(id)
        {
            Username = username;
            Passw = passw;
            Email = email;
            Amministratore = amministratore;
            Nome = nome;
            Cognome = cognome;
            Dob = dob;
            Indirizzo = indirizzo;
            Preferiti = preferiti;
        }
    }
}
