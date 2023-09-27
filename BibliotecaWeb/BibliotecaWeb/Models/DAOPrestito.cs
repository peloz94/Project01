using Utility;
using System.Data.Sql;
namespace BibliotecaWeb.Models
{
    public class DAOPrestito : IDAO {
        private Database db;
        private static DAOPrestito instance = null;
        public static DAOPrestito GetInstance() {
            if (instance == null)
                instance = new DAOPrestito();
            return instance;
        }
        private DAOPrestito() {
            db = new Database("BibliotecaWeb");
        }
        public bool Delete(Entity e) {
            return db.Update($"DELETE * FROM UtentiPrestito {e.Id}");
        } // Fatto
        public Entity Find(int id) {
            foreach(Entity e in ReadAll())
                if(e.Id == id) 
                    return e;
            return null;
        } // Fatto
        public bool Insert(Entity e) {
            return db.Update("INSERT INTO UtentiPrestito (idLibro, idUtente, dataRitito, dataConsegna) VALUES " +
                             $"({(((Prestito)e).Libro.Id)}, {(((Prestito)e).Utente.Id)}, '{(((Prestito)e).DataRitito).ToString("yyyy-MM-gg")}', '{(((Prestito)e).DataConsegna).ToString("yyyy-MM-gg")}');");
        } // Fatto
        public List<Entity> ReadAll() {
            List<Entity> ris = new List<Entity>();
            List<Dictionary<string, string>> righe = db.Read("SELECT * FROM UtentiPrestito");
            foreach (Dictionary<string, string> riga in righe) {
                Prestito p = new Prestito();
                p.FromDictionary(riga);
                p.Libro = (Libro)DAOLibro.GetInstance().Find(int.Parse(riga["idlibro"]));
                p.Utente = (Utente)DAOUtente.GetInstance().Find(int.Parse(riga["idutente"]));
                ris.Add(p);
            }
            return ris;
        } // Fatto
        public bool Update(Entity e) {
            return db.Update("UPDATE UtentiPrestito SET" +
                             $"idLibro = {(((Prestito)e).Libro.Id)} " +
                             $"idUtente = {(((Prestito)e).Utente.Id)} " +
                             $"dataRitito = '{(((Prestito)e).DataRitito).ToString("yyyy-MM-gg")}' " +
                             $"dataConsegna = '{(((Prestito)e).DataConsegna.ToString("yyyy-MM-gg"))}' " +
                             $"WHERE id = {e.Id}");
        } // Fatto
        public List<Entity> ListaPrestiti(int id)
        {
            List<Entity> ris = new List<Entity> ();
            List<Dictionary<string, string>> righe = db.Read($"SELECT * FROM UtentiPrestito where idUtente = {id}");
            foreach (Dictionary<string, string> riga in righe)
            {
                Prestito p = new Prestito();
                p.FromDictionary(riga);
                p.Libro = (Libro)DAOLibro.GetInstance().Find(int.Parse(riga["idlibro"]));
                p.Utente = (Utente)DAOUtente.GetInstance().Find(int.Parse(riga["idutente"]));
                ris.Add(p);
            }
            return ris;
        }
        public List<Entity> ListaPrestiti()
        {
            List<Entity> ris = new List<Entity>();
            List<Dictionary<string, string>> righe = db.Read($"SELECT * FROM UtentiPrestito");
            foreach (Dictionary<string, string> riga in righe)
            {
                Prestito p = new Prestito();
                p.FromDictionary(riga);
                p.Libro = (Libro)DAOLibro.GetInstance().Find(int.Parse(riga["idlibro"]));
                p.Utente = (Utente)DAOUtente.GetInstance().Find(int.Parse(riga["idutente"]));
                ris.Add(p);
            }
            return ris;
        }
    }
}
