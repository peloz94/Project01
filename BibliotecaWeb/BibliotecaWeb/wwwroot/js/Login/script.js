function MostraTextBox() {
    var txt = document.getElementById("txtfind")
    txt.style.display = "inline"

}

function NascondiTextBox() {
    var txt = document.getElementById("txtfind")
    txt.style.display = "none"

}
/*function controlliRegistrazione() {

    if (document.getElementById("username").value.length < 2 || document.getElementById("username").value.length > 150) {
        alert("Titolo non valido");
        return false;
    }
    if (document.getElementById("cognome").value.length < 2 || document.getElementById("cognome").value.length > 150) {
        alert("Cognome non valido");
        return false;
    }
    if (document.getElementById("nome").value.length < 1 || document.getElementById("nome").value.length > 200) {
        alert("URL immagine non valido");
        return false;
    }
    
    return true;

}
*/
function controlli_registrazione() {
    if (document.getElementById('user').value.length < 2) {
        alert("USERNAME " + document.getElementById('user').value + " troppo breve");
        return false;
    }

    let p1 = document.getElementById('passw1').value;
    let p2 = document.getElementById('passw2').value;

    // Controllo che la prima password combaci con la seconda
    if (p1 != p2) {
        alert("Le password inserite non coincidono");
        return false;
    }

    // Controllo che la password rispetti i canoni
    // (minimo 8 caratteri, presenza di almeno un CAPS, un numero)
    let error = [];

    if (p1.length < 8)
        error.push("La password deve avere almeno 8 caratteri");
    if (p1.search(/[a-z]/) < 0)
        error.push("La password deve contenere almeno una lettera minuscola");
    if (p1.search(/[A-Z]/) < 0)
        error.push("La password deve contenere almeno una lettera maiuscola");
    if (p1.search(/[0-9]/) < 0)
        error.push("La password deve contenere almeno un numero");

    if (error.length > 0) {
        alert(error.join("\n"))
        return false;
    }

    return true;
}
function confermaEliminazione() {
    var conferma = confirm("Sei sicuro di voler Eliminare?");

    if (conferma) {
        return true;
    } else {
        return false;
    }
    function reindireizza() {
        window.location.href = "/Libri/Elenco";
    }