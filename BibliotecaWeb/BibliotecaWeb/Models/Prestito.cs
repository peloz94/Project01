using Utility;

namespace BibliotecaWeb.Models
{
    public class Prestito : Entity
    {
        public Libro Libro { get; set; }
        public Utente Utente { get; set; }
        public DateTime DataRitito { get; set; }
        public DateTime DataConsegna { get; set; }
        public Prestito() { }
        public Prestito(int id, Libro libro, Utente utente, DateTime dataRitito, DateTime dataConsegna) : base(id){
            Libro = libro;
            Utente = utente;
            DataRitito = dataRitito;
            DataConsegna = dataConsegna;
        }
    }
}
