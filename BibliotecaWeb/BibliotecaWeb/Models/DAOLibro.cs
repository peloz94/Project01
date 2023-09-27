using Utility;

namespace BibliotecaWeb.Models
{
    public class DAOLibro : IDAO
    {
        private Database db;
        private static DAOLibro instance = null;
        private DAOLibro()
        {
            db = new Database("BibliotecaWeb");
        }
        public static DAOLibro GetInstance()
        {
            if (instance == null)
                instance = new DAOLibro();
            return instance;
        }

        public bool Delete(Entity e)
        {
            return db.Update($"DELETE FROM Libri WHERE id = {((Libro)e).Id}");
        }

        public Entity Find(int id)
        {
            foreach(Entity e in ReadAll())
                if(((Libro)e).Id == id) return e;
            return null;
        }
        public Libro Find(string titolo)
        {
            Dictionary<string, string> libri = db.ReadOne($"SELECT * FROM Libri WHERE titolo = '{titolo}';");
            Libro l = new Libro();
            l.FromDictionary(libri);
            return l;
        }

        public bool Insert(Entity e)
        {
            return db.Update($"INSERT INTO Libri " +
                $"(titolo, autore, genere, numeroPagine, immagine, disponibile, annoPubblicazione, trama) " +
                $"VALUES " +
                $"('{Database.CambiaApostrofi(((Libro)e).Titolo)}', "     +
                $"'{Database.CambiaApostrofi(((Libro)e).Autore)}', "      +
                $"'{Database.CambiaApostrofi(((Libro)e).Genere)}', "      +
                $"{((Libro)e).NumeroPagine}, "  +
                $"'{Database.CambiaApostrofi(((Libro)e).Immagine)}', "    +
                $"{((((Libro)e).Disponibile) == true ? "1" : "0")}), " +
                $"{((Libro)e).NumeroPagine}, " +
                $"'{Database.CambiaApostrofi(((Libro)e).Trama)}';")  ;
        }

        public List<Entity> ReadAll()
        {
            List<Entity>ris=new();
            List<Dictionary<string, string>> righe = db.Read($"SELECT * FROM Libri");

            foreach (var riga in righe)
            {
                Libro l =new Libro();
                l.FromDictionary(riga);
                ris.Add(l);
            }
            return ris;
        }

        public bool Update(Entity e)
        {
            return db.Update($"UPDATE Libri SET " +
                             $"titolo = '{Database.CambiaApostrofi(((Libro)e).Titolo)}', " +
                             $"autore = '{Database.CambiaApostrofi(((Libro)e).Autore)}', " +
                             $"genere = '{Database.CambiaApostrofi(((Libro)e).Genere)}', " +
                             $"numeropagine = {((Libro)e).NumeroPagine}, " +
                             $"immagine = '{((Libro)e).Immagine}', " +
                             $"disponibile = {(((Libro)e).Disponibile ? "1" : "0")}, " +
                             $"annoPubblicazione = {((Libro)e).AnnoPubblicazione}, " +
                             $"trama = '{Database.CambiaApostrofi(((Libro)e).Trama)}' " +
                             $"WHERE id = {((Libro)e).Id};");
        }

        public List<Libro> LibriPreferiti(int id)
        {
            List<Libro> ris = new();
            List<Dictionary<string, string>> righe = db.Read($"SELECT * FROM UtentiPreferiti WHERE idUtente = {id}");
            foreach (var riga in righe)
            {
                Libro l = new Libro();
                l = (Libro)DAOLibro.GetInstance().Find(int.Parse(riga["idlibro"]));
                ris.Add(l);
            }
            return ris;
        }
        public List<Entity> OrdinaPer(string tipo)
        {
            List<Entity> ris = new();
            List<Dictionary<string, string>> righe = db.Read($"SELECT * FROM Libri order by {tipo}");

            foreach (var riga in righe)
            {
                Libro l = new Libro();
                l.FromDictionary(riga);
                ris.Add(l);
            }
            return ris;

        }
        public List<Entity> Top10()
        {
            List<Entity> ris = new();
            List<Dictionary<string, string>> righe = db.Read($"SELECT top 4 Libri.id, COUNT(Libri.id) as num FROM UtentiPrestito left join Libri on Libri.id = idLibro group by Libri.id order by num desc");

            foreach (var riga in righe)
            {
                Libro l = new Libro();
                l = (Libro)DAOLibro.GetInstance().Find(int.Parse(riga["id"]));
                ris.Add(l);
            }
            return ris;
        }
    }
}
