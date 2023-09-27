using Utility;

namespace BibliotecaWeb.Models
{
    public class DAOUtente : IDAO
    {
        private Database db;

        private static DAOUtente instance = null;

        public static DAOUtente GetInstance()
        {
            if (instance == null)
                instance = new DAOUtente();
            return instance;
        }
        private DAOUtente()
        {
            db = new Database("BibliotecaWeb");
        }

        /*
        * Metodo ReadAll(): Legge tutti gli Utenti presenti in database e restituisce oggetti
        */
        public List<Entity> ReadAll()
        {
            List<Entity> ris = new List<Entity>();
            string query = $"SELECT " +
                           $"Utenti_Account.id, username, passw, email, amministratore, nome, cognome, dob, indirizzo " +
                           $"FROM Utenti_Account inner join Utenti_Info on Utenti_Account.id = Utenti_Info.id "; ;
            List<Dictionary<string, string>> read = db.Read(query);
            foreach (Dictionary<string, string> item in read)
            {
                Utente u = new Utente();
                u.FromDictionary(item);
                // Da Implementare in DAOLibro il Metodo LIbriPreferiti(int id)
                // che restituisce la lista di libri preferiti per l'utente con id = id
                u.Preferiti = DAOLibro.GetInstance().LibriPreferiti(u.Id);

                ris.Add(u);
            }
            return ris;

        }

        public Entity Find(int id)
        {
            foreach (Entity e in ReadAll())
                if (e.Id == id)
                    return e;
            return null;
        }

        /*
        * Metodo Insert e Update vanno ad agire contemporanemanete sulla tabella del database Utente_Account e Utente_Info
        */
        public bool Insert(Entity e)
        {
            string username = Database.CambiaApostrofi(((Utente)e).Username);
            string passw = ((Utente)e).Passw;
            string email = ((Utente)e).Email;
            bool amministratore = ((Utente)e).Amministratore;
            string nome = Database.CambiaApostrofi(((Utente)e).Nome);
            string cognome = Database.CambiaApostrofi(((Utente)e).Cognome);
            DateTime dob = ((Utente)e).Dob;
            string indirizzo = Database.CambiaApostrofi(((Utente)e).Indirizzo);
            return db.Update2(
                             $"INSERT INTO Utenti_Account (username, passw, email, amministratore) " +
                             $"VALUES " +
                             $"('{username}', HASHBYTES('Sha2_512', '{passw}'), '{email}', {(amministratore ? 1 : 0)}); " +
                             $"INSERT INTO Utenti_Info (id, nome, cognome, dob, indirizzo) " +
                             $"VALUES " +
                             $"((select top 1 id from Utenti_Account\r\norder by id desc), '{nome}', '{cognome}', '{dob.ToString("yyyy-MM-dd")}', '{indirizzo}');"
                             );
        }
        public bool Update(Entity e)
        {
            string username = Database.CambiaApostrofi(((Utente)e).Username);
            string passw = ((Utente)e).Passw;
            string email = ((Utente)e).Email;
            bool amministratore = ((Utente)e).Amministratore;
            string nome = Database.CambiaApostrofi(((Utente)e).Nome);
            string cognome = Database.CambiaApostrofi(((Utente)e).Cognome);
            DateTime dob = ((Utente)e).Dob;
            string indirizzo = Database.CambiaApostrofi(((Utente)e).Indirizzo);
            return db.Update(
                             $"UPDATE Utenti_Account " +
                             $"SET " +
                             $"username = '{username}', " +
                             $"email = '{email}', " +
                             $"amministratore = {(amministratore ? 1 : 0)} " +
                             $"WHERE id = {e.Id}; " +
                             $"UPDATE Utenti_Info " +
                             $"SET " +
                             $"nome = '{nome}', " +
                             $"cognome = '{cognome}', " +
                             $"dob = '{dob.ToString("yyyy-MM-dd")}', " +
                             $"indirizzo = '{indirizzo}'" +
                             $"WHERE id = {e.Id};"

                             );
        }
        public bool Delete(Entity e)
        {
            string query = $"DELETE FROM Utenti_Account WHERE id = {e.Id}";

            return db.Update(query);
        }

        /*
         * Metodo OrdinaPer, riceve come parametro una stringa che corrisponde al nome di una colonna del database 
         * e ritorna la lista di udenti ordinata in base alla colonna scelta
         */
        public List<Entity> OrdinaPer(string tipo)
        {
            List<Entity> ris = new List<Entity>();
            string query = $"SELECT * FROM Utenti_Account inner join Utenti_Info on Utenti_Account.id = Utenti_Info.id Order by {tipo} ";
            List<Dictionary<string, string>> read = db.Read(query);
            foreach (Dictionary<string, string> item in read)
            {
                Utente u = new Utente();
                u.FromDictionary(item);
                // Da Implementare in DAOLibro il Metodo LIbriPreferiti(int id)
                // che restituisce la lista di libri preferiti per l'utente con id = id
                u.Preferiti = DAOLibro.GetInstance().LibriPreferiti(u.Id);

                ris.Add(u);
            }
            return ris;

        }
        public bool Valida(string username, string password)
        {
            Dictionary<string, string> utente = db.ReadOne($"SELECT * FROM Utenti_Account WHERE username = '{Database.CambiaApostrofi(username)}' " +
                                                            $"AND passw = HASHBYTES('Sha2_512','{password}');");
            if (utente != null)
                return true;
            else
                return false;
        }
        public Entity Find(string username) 
        {
            Console.WriteLine("\nsei nel find");
            Dictionary<string, string> utenti = db.ReadOne($"SELECT " +
                                                           $"Utenti_Account.id, username, passw, email, amministratore, nome, cognome, dob, indirizzo " +
                                                           $"FROM Utenti_Account inner join Utenti_Info on Utenti_Account.id = Utenti_Info.id " +
                                                           $"WHERE Utenti_Account.username = '{Database.CambiaApostrofi(username)}'");
            Utente u = new Utente();
            u.FromDictionary(utenti);
            u.Preferiti = DAOLibro.GetInstance().LibriPreferiti(u.Id);
            return u;
        } // Search per gli utenti
        public bool InsertPreferiti(int idLibro, int idUtente)
        {
            return db.Update(
                                $"Insert into UtentiPreferiti (idLibro, idUtente) " +
                                $"VALUES " +
                                $"({idLibro},{idUtente})"
                            );
        }
        public bool RimuoviPreferiti(int idLibro, int idUtente)
        {
            return db.Update(
                                $"Delete from UtentiPreferiti Where idlibro = {idLibro}  and idUtente = {idUtente}"
                            );
        }
        public bool PrendiPrestito(int idLibro, int idUtente)
        {
            return db.Update(
                                $"Insert into UtentiPrestito (idLibro, idUtente, dataRitito) " +
                                $"VALUES " +
                                $"({idLibro},{idUtente}, '{DateTime.Now.ToString("yyyy-MM-dd")}'); " +
                                $"update Libri\r\nset\r\ndisponibile = 0\r\nwhere id = {idLibro}"
                            );
        }
        public bool VerificaPrestito(int idLibro, int idUtente)
        {
            foreach(Entity p in DAOPrestito.GetInstance().ListaPrestiti(idUtente))
            {
                if (((Prestito)p).Libro.Id == idLibro && ((Prestito)p).DataConsegna.Year == 0001)
                    return true;

            }
            return false;
            
        }
        public bool ConsegnaPrestito(int idLibro, int idUtente)
        {
            return db.Update(
                                $"Update UtentiPrestito " +
                                $"set " +
                                $"dataConsegna =  '{DateTime.Now.ToString("yyyy-MM-dd")}'" +
                                $"where idLibro = {idLibro} and idUtente = {idUtente} and dataConsegna is null " +
                                $"update Libri\r\nset\r\ndisponibile = 1\r\nwhere id = {idLibro}"
                            );
        }

        public bool UpdatePassword(int id,string password)
        {
            return db.Update(
                    $"Update Utenti_Account " +
                    $"set " +
                    $"passw = HASHBYTES('SHA2_512', '{password}') " +
                    $"where id = {id}"
                );
        }
    }
}
