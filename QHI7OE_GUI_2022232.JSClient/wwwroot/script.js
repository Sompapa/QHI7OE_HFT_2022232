const urlapi = {
    manga: "manga",
    author: "author",
    genre: "genre",
    getAvgPriceByAuthor: "stat/avgpricebyauthor",
    getAvgPriceByGenre: "stat/avgpricebygenre",
    getAllPriceByGenre: "stat/allpricebygenre",
    getAllPriceByYears: "stat/allpricebyyears",
    getAVGRateByGenre: "stat/avgratebygenre"
}

const valueDisplayer = {
    flex: 'flex',
    none: 'none',
    block: 'block'
}
const defaHeader = { 'Content-Type': 'application/json', }

const crudTypes = {
    get: "GET",
    post: "POST",
    put: "PUT",
    delete: "DELETE"
}

const IdTab = {
    mangas: "mangas",
    authors: "authors",
    genres: "genres",
    extdata: "extdata"
}

const queryTab = 'currentTab';

const ELEMENT_CLASSES = {
    active: "active",
    hidden: 'hidden'
}

const ID_POSFIX = {
    table: "Table",
    add: 'AddForm',
    edit: 'EditForm',
}

let mangas = [];
let authors = [];
let genres = [];
let connection = null;


setupSignalR();

let mangaIdToUpdate = -1;

window.addEventListener('load', () => {
    const currentTab = readCurrentTabFromQueryParams();

    if (currentTab) {


    }
})

function openTab(evt, tabId) {

    let tabcontent, tablinks;
    tabcontent = document.getElementByClassName("tabcontent");
    for (var i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = valueDisplayer.none;
    }
    tablinks = document.getElementsByClassName("tablinks");
    for (var i = 0; i < tablinks.length; i++) {
        remveCl
    }
}

getdata();


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
            { title: name, price: cost, mangaId: mangaIdToUpdate })
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
            { title: name, price: cost })})
        .then(response => response)
        .then(data =>
        {
            console.log('Succes:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
    
}

function edit(value) {
    const tableElement = document.getElementById(`${value}${ID_POSFIX.table}`);
    const editElement = document.getElementById(`${value}${ID_POSFIX.edit}`);

    addClass(tableElement, ELEMENT_CLASSES.hidden);
    removeClass(editFormElement, ELEMENT_CLASSES.hidden);
}

function closeAllPanelsAndShowTable(value) {
    const tableElement = document.getElementById(`${value}${ID_POSFIX.table}`);
    const addFormElement = document.getElementById(`${value}${ID_POSFIX.add}`);
    const editFormElement = document.getElementById(`${value}${ID_POSFIX.edit}`);

    addClass(addFormElement, ELEMENT_CLASSES.hidden);
    addClass(editFormElement, ELEMENT_CLASSES.hidden);
    removeClass(tableElement, ELEMENT_CLASSES.hidden);
}

function readCurrentTabFromQueryParams() {
    const urlParams = new URLSearchParams(window.location.search);
    return urlParams.get(queryTab);
}

function setQueryParams(key, value) {
    const searchParams = new URLSearchParams(window.location.search);
    searchParams.set(key, value);
    const newRelativePathQuery = window.location.pathname + "?" + searchParams.toString();
    history.pushState(null, "", newRelativePathQuery);
}

function resetInnerHtmlById(id) {
    document.getElementById(id).innerHTML = "";
}

function steElementVisibilityById(id, value) {
    document.getElementById(id).style.display = value;
}

function getElementValueById(id, value) {
    document.getElementById(id).value = value;
}

function addClass(element, className) {
    if (!element.classList.contains(className)) {
        element.classList.add(className);
    }
}

function removeClass(element, className) {
    if (!element.classList.contains(className)) {
        element.classList.remove(className);
    }
}

async function CrudGet(url) {
    return await fetch(`http://localhost:59073/${url}`, {
        method: crudTypes.get,
        headers: defaHeader
    })
}

async function CrudPost(url, request) {
    fetch(`http://localhost:59073/${url}`, {
        method: crudTypes.post,
        headers: defaHeader,
        body: JSON.stringify(request)
    })
}

async function CrudPut(url, request) {
    fetch(`http://localhost:59073/${url}`, {
        method: crudTypes.put,
        headers: defaHeader
        body: JSON.stringify(request)
    })
}

async function CrudDelete(url, id) {
    return await fetch(`http://localhost:59073/${url}/${id}`, {
        method: crudTypes.delete,
        headers: defaHeader
    })
}