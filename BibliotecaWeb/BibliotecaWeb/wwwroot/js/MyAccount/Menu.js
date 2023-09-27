function MenuUtente() {
    var htmlCode = `
        <div id="hormenu">
            <ul>
                <li>
                    <ul>
                        <li><a href="#">Libri</a></li>
                        <li><a href="#">Libri in prestito</a></li>
                        <li><a href="#">Storico</a></li>
                        <li><a href="#">Impostazioni</a></li>
                    </ul>
                </li>
            </ul>
        </div>
    `;
    var div = document.createElement("div");
    div.innerHTML = htmlCode;
    document.body.appendChild(div);
}
function MenuAmministratore() {
    var htmlCode = `
        <div id="hormenu">
            <ul>
                <li>
                    <ul>
                        <li><a href="#">Utenti</a></li>
                        <li><a href="#">Impostazioni</a></li>
                    </ul>
                </li>
            </ul>
        </div>
    `;
    var div = document.createElement("div");
    div.innerHTML = htmlCode;
    document.body.appendChild(div);
}
function reindireizza() {
    window.location.href = "/Libri/Elenco";
}
function MostraTextBox() {
    var txt = document.getElementById("txtfind")
    txt.style.display = "inline"

}

function NascondiTextBox() {
    var txt = document.getElementById("txtfind")
    txt.style.display = "none"

}