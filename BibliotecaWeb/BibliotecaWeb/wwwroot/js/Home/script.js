function MostraTextBox() {
    var txt = document.getElementById("txtfind")
    txt.style.display = "inline"

}

function NascondiTextBox() {
    var txt = document.getElementById("txtfind")
    txt.style.display = "none"

}
//function cerca() {
//    const input = document.getElementById('txtfind').value;
//    console.log("Ciao");
//    // Creare un URL con il parametro di ricerca
//    const url = `/Libri/Elenco?ricerca=${encodeURIComponent(input)}`;
//    // Reindirizzare alla pagina con l'URL creato
//    window.location.href = url;
//}
function reindireizza() {
    window.location.href = "/Libri/Elenco";
}


document.addEventListener("DOMContentLoaded", function () {
    const cookiePopup = document.getElementById("cookie-popup");
    const acceptButton = document.getElementById("accept-cookie-button");

    acceptButton.addEventListener("click", function () {
        // Imposta un cookie per indicare che l'utente ha accettato i cookie
        document.cookie = "cookiesAccepted=true; expires=Fri, 31 Dec 9999 23:59:59 GMT; path=/";
        cookiePopup.style.display = "none";
    });

    // Controlla se il cookie è già stato accettato
    const cookiesAccepted = document.cookie.includes("cookiesAccepted=true");

    if (!cookiesAccepted) {
        // Mostra il popup solo se i cookie non sono stati accettati in precedenza
        cookiePopup.style.display = "block";
    }
});
