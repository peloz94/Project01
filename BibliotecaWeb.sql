CREATE DATABASE BibliotecaWeb;
USE BibliotecaWeb;
/* DROP DATABASE BibliotecaWeb; */

CREATE TABLE Libri
(
	id INT PRIMARY KEY IDENTITY(1,1),
	titolo VARCHAR(200),
	autore VARCHAR(200),
	genere VARCHAR(100),
	numeroPagine INT,
	immagine VARCHAR(350),
	disponibile BIT,
	annoPubblicazione INT,
	trama VARCHAR(800)
);

CREATE TABLE Utenti_Account
(
	id INT PRIMARY KEY IDENTITY(1,1),
	username VARCHAR(200),
	passw VARCHAR(200),
	email VARCHAR(200),
	amministratore BIT
);

CREATE TABLE Utenti_Info
(
	id INT PRIMARY KEY,
	nome VARCHAR(200),
	cognome VARCHAR(200),
	dob DATE,
	indirizzo VARCHAR(300)

	FOREIGN KEY (id) REFERENCES Utenti_Account(id)
	
	ON DELETE CASCADE
	ON UPDATE CASCADE
);

CREATE TABLE UtentiPrestito
(
	id INT PRIMARY KEY IDENTITY(1,1),
	idLibro INT,
	idUtente INT,
	dataRitito DATE,
	dataConsegna DATE

	FOREIGN KEY (idLibro) REFERENCES Libri(id)
	ON DELETE CASCADE
	ON UPDATE CASCADE,

	FOREIGN KEY (idUtente) REFERENCES Utenti_Account(id)
	ON DELETE CASCADE
	ON UPDATE CASCADE
);


CREATE TABLE UtentiPreferiti
(
	idLibro INT,
	idUtente INT,
	
	PRIMARY KEY (idLibro, idUtente),

	FOREIGN KEY (idLibro) REFERENCES Libri(id)
	ON DELETE CASCADE
	ON UPDATE CASCADE,

	FOREIGN KEY (idUtente) REFERENCES Utenti_Account(id)
	ON DELETE CASCADE
	ON UPDATE CASCADE

);

DROP TABLE UtentiPreferiti;
DROP TABLE UtentiPrestito;

-- Inserisci 10 libri nella tabella Libri
INSERT INTO Libri (titolo, autore, genere, numeroPagine, immagine, disponibile, annoPubblicazione, trama)
VALUES
    ('Il Signore degli Anelli', 'J.R.R. Tolkien', 'Fantasy', 1200, 'img1.jpg', 1, 1954, 'Un gruppo di avventurieri si imbarca in un epico viaggio per distruggere un potente anello.'),
    ('Cronache di Narnia', 'C.S. Lewis', 'Fantasy', 800, 'img2.jpg', 1, 1950, 'Bambini scoprono un mondo magico attraverso un armadio.'),
    ('1984', 'George Orwell', 'Dystopian', 328, 'img3.jpg', 1, 1949, 'In un futuro totalitario, un uomo combatte contro il controllo dello stato.'),
    ('Il Piccolo Principe', 'Antoine de Saint-Exupéry', 'Fiction', 96, 'img4.jpg', 1, 1943, 'Un piccolo principe esplora l''universo e impara importanti lezioni di vita.'),
    ('Harry Potter e la Pietra Filosofale', 'J.K. Rowling', 'Fantasy', 320, 'img5.jpg', 1, 1997, 'Un giovane mago scopre il suo destino nella scuola di magia di Hogwarts.'),
    ('Il Grande Gatsby', 'F. Scott Fitzgerald', 'Fiction', 180, 'img6.jpg', 1, 1925, 'Un misterioso milionario affascina l''alta società durante gli anni ''20.'),
    ('La Divina Commedia', 'Dante Alighieri', 'Epic Poetry', 100, 'img7.jpg', 1, 1320, 'Il poema epico di Dante attraverso Inferno, Purgatorio e Paradiso.'),
    ('Moby Dick', 'Herman Melville', 'Adventure', 544, 'img8.jpg', 1, 1851, 'La caccia all''implacabile balena bianca Moby Dick.'),
    ('Cime tempestose', 'Emily Brontë', 'Gothic Fiction', 342, 'img9.jpg', 1, 1847, 'Una storia tormentata di amore e vendetta nelle brughiere inglesi.'),
    ('Il nome della rosa', 'Umberto Eco', 'Mystery', 512, 'img10.jpg', 1, 1980, 'Un monaco francescano risolve un mistero in una biblioteca medievale.');


-- Inserisci 5 utenti nella tabella Utenti_Account e le relative informazioni in Utenti_Info
-- Assicurati di sostituire le password con l'hash generato usando HASHBYTES('SHA2_512', password)
-- Esegui questo comando per ciascun utente
INSERT INTO Utenti_Account (username, passw, email, amministratore)
VALUES
    ('admin', HASHBYTES('SHA2_512', 'password_admin'), 'admin@example.com', 1);

INSERT INTO Utenti_Info (id, nome, cognome, dob, indirizzo)
VALUES
    (SCOPE_IDENTITY(), 'Admin', 'Admin', '1990-01-01', '123 Main St');

INSERT INTO Utenti_Account (username, passw, email, amministratore)
VALUES
    ('user1', HASHBYTES('SHA2_512', 'password_user1'), 'user1@example.com', 0);

INSERT INTO Utenti_Info (id, nome, cognome, dob, indirizzo)
VALUES
    (SCOPE_IDENTITY(), 'User', 'One', '1995-05-15', '456 Elm St');

INSERT INTO Utenti_Account (username, passw, email, amministratore)
VALUES
    ('user2', HASHBYTES('SHA2_512', 'password_user2'), 'user2@example.com', 0);

INSERT INTO Utenti_Info (id, nome, cognome, dob, indirizzo)
VALUES
    (SCOPE_IDENTITY(), 'User', 'Two', '1988-11-30', '789 Oak St');

INSERT INTO Utenti_Account (username, passw, email, amministratore)
VALUES
    ('user3', HASHBYTES('SHA2_512', 'password_user3'), 'user3@example.com', 0);

INSERT INTO Utenti_Info (id, nome, cognome, dob, indirizzo)
VALUES
    (SCOPE_IDENTITY(), 'User', 'Three', '1998-07-20', '101 Pine St');

INSERT INTO Utenti_Account (username, passw, email, amministratore)
VALUES
    ('user4', HASHBYTES('SHA2_512', 'password_user4'), 'user4@example.com', 0);

INSERT INTO Utenti_Info (id, nome, cognome, dob, indirizzo)
VALUES
    (SCOPE_IDENTITY(), 'User', 'Four', '1992-03-10', '202 Cedar St');

INSERT INTO Utenti_Account (username, passw, email, amministratore)
VALUES
    ('user5', HASHBYTES('SHA2_512', 'password_user5'), 'user5@example.com', 0);

INSERT INTO Utenti_Info (id, nome, cognome, dob, indirizzo)
VALUES
    (SCOPE_IDENTITY(), 'User', 'Five', '1997-09-25', '303 Maple St');

-- Inserisci dati nella tabella UtentiPrestito (esempio con 3 prestiti)
INSERT INTO UtentiPrestito (idLibro, idUtente, dataRitito, dataConsegna)
VALUES
    (1, 2, '2023-09-22', '2023-10-05'),
    (3, 4, '2023-09-23', '2023-10-10'),
    (5, 1, '2023-09-24', '2023-10-08');

-- Inserisci dati nella tabella UtentiPreferiti (esempio con 3 preferenze)
INSERT INTO UtentiPreferiti (idLibro, idUtente)
VALUES
    (2, 3),
    (4, 5),
    (6, 2);

-- Inserisci dati aggiuntivi nella tabella UtentiPrestito (esempio con 2 prestiti)
INSERT INTO UtentiPrestito (idLibro, idUtente, dataRitito, dataConsegna)
VALUES
    (7, 3, '2023-09-25', '2023-10-05'),
    (9, 5, '2023-09-26', '2023-10-15');

-- Inserisci dati aggiuntivi nella tabella UtentiPreferiti (esempio con 2 preferenze)
INSERT INTO UtentiPreferiti (idLibro, idUtente)
VALUES
    (8, 4),
    (10, 1);

-- Inserisci prestiti non riconsegnati
INSERT INTO UtentiPrestito (idLibro, idUtente, dataRitito, dataConsegna)
VALUES
    (2, 1, '2023-09-27', NULL),  -- Non riconsegnato
    (6, 3, '2023-09-28', NULL);  -- Non riconsegnato


select * from UtentiPrestito;
select * from UtentiPreferiti;
select * from Libri;

SELECT * 
FROM Utenti_Account left join Utenti_Info on Utenti_Account.id = Utenti_Info.id;

SELECT * 
FROM Utenti_Account left join Utenti_Info on Utenti_Account.id = Utenti_Info.id
WHERE amministratore = 1;

SELECT * 
FROM UtentiPrestito inner join Utenti_Info on Utenti_Info.id = idUtente
					left join Libri on idLibro = Libri.id;

SELECT * FROM Utenti_Account inner join Utenti_Info on Utenti_Account.id = Utenti_Info.id Order by nome 


UPDATE Libri 
SET
immagine = 'https://img.illibraio.it/images/9788845294044_92_310_0_75.jpg'
WHERE id = 1;
UPDATE Libri 
SET
immagine = 'https://m.media-amazon.com/images/I/A1yPOdvMgJL._AC_UF1000,1000_QL80_.jpg'
WHERE id = 2;
UPDATE Libri 
SET
immagine = 'https://www.einaudi.it/content/uploads/2021/01/978880624818HIG.JPG'
WHERE id = 3;
UPDATE Libri 
SET
immagine = 'https://copertine.hoepli.it/hoepli/xxl/978/8858/9788858012833.jpg'
WHERE id = 4;
UPDATE Libri 
SET
immagine = 'https://m.media-amazon.com/images/I/718kKmxQBWL._AC_UF1000,1000_QL80_.jpg'
WHERE id = 5;
UPDATE Libri 
SET
immagine = 'https://www.illibraio.it/wp-content/uploads/2021/04/il-grande-gatsby-libro-copertina.jpg'
WHERE id = 6;
UPDATE Libri 
SET
immagine = 'https://www.itacalibri.it/System/134279/ns_commedia-inferno-dante(1).jpg'
WHERE id = 7;
UPDATE Libri 
SET
immagine = 'https://www.ibs.it/images/9788807900761_0_536_0_75.jpg'
WHERE id = 8;
UPDATE Libri 
SET
immagine = 'https://www.lafeltrinelli.it/images/9788807900129_0_536_0_75.jpg'
WHERE id = 9;
UPDATE Libri 
SET
immagine = 'https://m.media-amazon.com/images/I/91sFibkjXaL._AC_UF1000,1000_QL80_.jpg'
WHERE id = 10;

-- Inserimento dell'undicesimo libro
INSERT INTO Libri (titolo, autore, genere, numeroPagine, immagine, disponibile, annoPubblicazione, trama)
VALUES
    ('Il Conte di Montecristo', 'Alexandre Dumas', 'Adventure', 1248, 'https://m.media-amazon.com/images/I/91sdGyU2YaL._AC_UF1000,1000_QL80_.jpg', 1, 1844, 'Un uomo vendicativo si fa strada nella società parigina con l''obiettivo di vendicarsi di coloro che l''hanno tradito, portando con sé un immenso tesoro nascosto.');

-- Inserimento del dodicesimo libro
INSERT INTO Libri (titolo, autore, genere, numeroPagine, immagine, disponibile, annoPubblicazione, trama)
VALUES
    ('Il vecchio e il mare', 'Ernest Hemingway', 'Adventure', 127, 'https://m.media-amazon.com/images/I/81Q5sYM4mQL._AC_UF1000,1000_QL80_.jpg', 1, 1952, 'Un pescatore anziano e determinato, Santiago, intraprende una lotta epica con un enorme pesce spada nel mare. La sua perseveranza diventa una metafora della vita stessa.');

-- Inserimento del tredicesimo libro
INSERT INTO Libri (titolo, autore, genere, numeroPagine, immagine, disponibile, annoPubblicazione, trama)
VALUES
    ('Anna Karenina', 'Lev Tolstoj', 'Classic', 864, 'https://www.ibs.it/images/9788807900006_0_464_0_75.jpg', 1, 1877, 'La tragica storia di Anna Karenina, una donna aristocratica russa che sacrifica tutto per il suo amore proibito e affronta le conseguenze devastanti delle sue scelte.');

-- Inserimento del quattordicesimo libro
INSERT INTO Libri (titolo, autore, genere, numeroPagine, immagine, disponibile, annoPubblicazione, trama)
VALUES
    ('L''Odissea', 'Omero', 'Epic Poetry', 300, 'https://www.ibs.it/images/2560732003956_0_0_536_0_75.jpg', 1, -800, 'Questo epico antico segue le avventure di Odisseo nel suo lungo viaggio di ritorno a casa dopo la Guerra di Troia, affrontando mostri, dei e sfide epiche.');

-- Inserimento del quindicesimo libro
INSERT INTO Libri (titolo, autore, genere, numeroPagine, immagine, disponibile, annoPubblicazione, trama)
VALUES
    ('Don Chisciotte', 'Miguel de Cervantes', 'Satire', 992, 'https://m.media-amazon.com/images/I/81QWz7WgrfL._AC_UF1000,1000_QL80_.jpg', 1, 1605, 'Le avventure di un gentiluomo spagnolo, Don Chisciotte, che diventa un cavaliere errante e affronta molte bizzarre e comiche situazioni.');

-- Inserimento del sedicesimo libro
INSERT INTO Libri (titolo, autore, genere, numeroPagine, immagine, disponibile, annoPubblicazione, trama)
VALUES
    ('Cent''anni di solitudine', 'Gabriel García Márquez', 'Magic Realism', 448, 'https://m.media-amazon.com/images/I/81nxsT-8NWS._AC_UF1000,1000_QL80_.jpg', 1, 1967, 'La storia multigenerazionale della famiglia Buendía in un paese immaginario, con eventi straordinari che sfidano la realtà.');

-- Inserimento del diciassettesimo libro
INSERT INTO Libri (titolo, autore, genere, numeroPagine, immagine, disponibile, annoPubblicazione, trama)
VALUES
    ('Il giovane Holden', 'J.D. Salinger', 'Fiction', 224, 'https://m.media-amazon.com/images/I/81vrtjhnQeL._AC_UF1000,1000_QL80_.jpg', 1, 1951, 'Le avventure di un adolescente ribelle di nome Holden Caulfield a New York mentre cerca di trovare un significato nella sua vita.');

-- Inserimento del diciottesimo libro
INSERT INTO Libri (titolo, autore, genere, numeroPagine, immagine, disponibile, annoPubblicazione, trama)
VALUES
    ('La Metamorfosi', 'Franz Kafka', 'Surrealism', 55, 'https://m.media-amazon.com/images/I/41U6Zbrwb-L._AC_UF1000,1000_QL80_.jpg', 1, 1915, 'La storia di Gregor Samsa, che si sveglia una mattina trasformato in un insetto, e affronta l''alienazione e la disumanizzazione.');

-- Inserimento del diciannovesimo libro
INSERT INTO Libri (titolo, autore, genere, numeroPagine, immagine, disponibile, annoPubblicazione, trama)
VALUES
    ('Guerra e pace', 'Lev Tolstoj', 'Classic', 1225, 'https://www.lafeltrinelli.it/images/9788811686200_0_536_0_75.jpg', 1, 1869, 'La narrazione epica delle vite di numerosi personaggi durante le guerre napoleoniche, con riflessioni sulla storia e la condizione umana.');

-- Inserimento del ventesimo libro
INSERT INTO Libri (titolo, autore, genere, numeroPagine, immagine, disponibile, annoPubblicazione, trama)
VALUES
    ('Orgoglio e pregiudizio', 'Jane Austen', 'Classic', 432, 'https://m.media-amazon.com/images/I/71CgbSAOOVL._AC_UF1000,1000_QL80_.jpg', 1, 1813, 'La storia di Elizabeth Bennet e Mr. Darcy, due persone orgogliose e prevenute che alla fine si innamorano l''una dell''altra, nonostante i pregiudizi sociali.');
