using Utility;

namespace BibliotecaWeb.Models
{
    public class Libro : Entity
    {
        public string Titolo { get; set; }
        public string Autore { get; set; }
        public string Genere { get; set; }
        public string Immagine { get; set; }
        public int NumeroPagine { get; set; }
        public bool Disponibile { get; set; }
        public int AnnoPubblicazione { get; set; }
        public string Trama { get; set; }
        public Libro() { }

        public Libro(int id, string titolo, string autore, string genere, string immagine, int numeroPagine, bool disponibile, int annoPubblicazione, string trama)
                    : base(id)
        {
            Titolo = titolo;
            Autore = autore;
            Genere = genere;
            Immagine = immagine;
            NumeroPagine = numeroPagine;
            Disponibile = disponibile;
            AnnoPubblicazione = annoPubblicazione;
            Trama = trama;
        }
    }
}
