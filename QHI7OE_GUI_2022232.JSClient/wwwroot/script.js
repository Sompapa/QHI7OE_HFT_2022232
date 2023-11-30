let mangas = [];
let connection = null;
let mangaIdToUpdate = -1;
getdata();
setupSignalR();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:59073/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("MangaCreated", (user, message) => {
        getdata();
    });

    connection.on("MangaDeleted", (user, message) => {
        getdata();
    });

    connection.on("MangaUpdated", (user, message) => {
        getdata();
    });

    connection.onclose(async () => {
        await start();
    });
    start();
}

async function getdata() {
    await fetch('http://localhost:59073/manga')
        .then(x => x.json())
        .then(y => {
            mangas = y;
            /*console.log(mangas);*/
            display();
        });
}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);

    }
};


function display() {
    document.getElementById('resultarea').innerHTML = "";
    mangas.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.mangaId + "</td><td>"
            + t.title + "</td><td>" + t.price + "</td><td>" + t.rating + "</td><td>" + t.release + "</td><td>" +
        `<button type= "button" onclick="remove(${t.mangaId})">Delete</button>` +
        `<button type= "button" onclick="showupdate(${t.mangaId})">Update</button>` +
            "</td ></tr>";
    });
}

function showupdate(id) {
    document.getElementById('titleupdate').value = mangas.find(t => t['mangaId'] == id)['title'];
    document.getElementById('priceupdate').value = mangas.find(t => t['mangaId'] == id)['price'];
    document.getElementById('updateformdiv').style.display = 'flex';
    mangaIdToUpdate = id;
}

function update() {
    document.getElementById('updateformdiv').style.display = 'none';
    let name = document.getElementById('titleupdate').value;
    let cost = document.getElementById('priceupdate').value;
    fetch('http://localhost:59073/manga', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { title: name, cost: price, mangaId: mangaIdToUpdate })
    })
        .then(response => response)
        .then(data => {
            console.log('Succes:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });

}

function remove(id) {
    fetch('http://localhost:59073/manga/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null })
        .then(response => response)
        .then(data => {
            console.log('Succes:', data)
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}

function create() {
    let name = document.getElementById('title').value;
    let cost = document.getElementById('price').value;
    fetch('http://localhost:59073/manga', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { title: name, cost: price })})
        .then(response => response)
        .then(data =>
        {
            console.log('Succes:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
    
}