let mangas = [];

fetch('http://localhost:28642/manga')
    .then(x => x.json())
    .then(y => {
        mangas = y;
        console.log(mangas);
        display();
    });

function display() {
    actors.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.mangaId + "</td><td>"
            + t.title + "</td></tr>";
    });
}