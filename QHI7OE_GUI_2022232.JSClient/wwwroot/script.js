let mangas = [];
getdata();

async function getdata() {
    await fetch('http://localhost:59073/manga')
        .then(x => x.json())
        .then(y => {
            mangas = y;
            console.log(mangas);
            display();
        });
}


function display() {
    mangas.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.mangaId + "</td><td>"
            + t.title + "</td><td>" + t.price + "</td><td>" + t.rating + "</td><td>" + t.release + "</td></tr>";
    });
}

function create() {
    let name = document.getElementById('title').value;
    fetch('http://localhost:59073/manga', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { title: name })})
        .then(response => response)
        .then(data =>
        {
            console.log('Succes:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
    
}