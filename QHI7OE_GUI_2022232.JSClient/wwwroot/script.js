﻿const API_URLS = {
    manga: "manga",
    author: "author",
    genre: "genre",
    getAVGPriceByAuthor: "stat/avgpricebyauthor",
    getAVGPriceByGenre: "stat/avgpricebygenre",
    getAllPriceByGenre: "stat/allpricebygenre",
    getAllPriceByYears: "stat/allpricebyyears",
    getAVGRateByGenre: "stat/avgratebygenre"
}

const DISPLAY_VALUES = {
    flex: 'flex',
    none: 'none',
    block: 'block'
}
const defaHeader = { 'Content-Type': 'application/json', }

const XHR_TYPES = {
    get: "GET",
    post: "POST",
    put: "PUT",
    delete: "DELETE"
}

const TAB_UIDS = {
    mangas: "mangas",
    authors: "authors",
    genres: "genres",
    extdata: "extdata"
}

const TAB_QUERY_PARAM = 'currentTab';

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
let authorIdToUpdate = -1;
let genreIdToUpdate = -1;

window.addEventListener('load', () => {
    const currentTab = readCurrentTabFromQueryParams();

    if (currentTab) {
        openTab(document.getElementById(`${currentTab}Tab`), currentTab);
        return;
    }

    openTab(document.getElementById('mangasTab'), TAB_UIDS.mangas);
})

function openTab(evt, tabId) {

    let tabcontent, tablinks;
    tabcontent = document.getElementsByClassName("tabcontent");
    for (var i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = DISPLAY_VALUES.none;
    }

    tablinks = document.getElementsByClassName("tablinks");
    for (var i = 0; i < tablinks.length; i++) {
        removeClass(tablinks[i], ELEMENT_CLASSES.active);
    }

    setElementVisibilityById(tabId, DISPLAY_VALUES.block);
    setQueryParams(TAB_QUERY_PARAM, tabId);

    if (evt.currentTarget) {
        addClass(evt.currentTarget, ELEMENT_CLASSES.active);
    } else {
        addClass(evt, ELEMENT_CLASSES.active);
    }

    if (tabId == TAB_UIDS.extdata) {
        return;
    }

    switch (tabId) {
        case TAB_UIDS.mangas:
            getdata_manga();
            break;
        case TAB_UIDS.authors:
            getdata_author();
            break;
        case TAB_UIDS.genres:
            getdata_genre();
            break;
    }

    closeAllPanelsAndShowTable(tabId);
}


function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:59073/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("MangaCreated", (user, message) => {
        getdata_manga();
    });

    connection.on("MangaDeleted", (user, message) => {
        getdata_manga();
    });

    connection.on("MangaUpdated", (user, message) => {
        getdata_manga();
    });

    connection.on("AuthorCreated", (user, message) => {
        getdata_author();
    });

    connection.on("AuthorDeleted", (user, message) => {
        getdata_author();
    });

    connection.on("AuthorUpdated", (user, message) => {
        getdata_author();
    });

    connection.on("GenreCreated", (user, message) => {
        getdata_genre();
    });

    connection.on("GenreDeleted", (user, message) => {
        getdata_genre();
    });

    connection.on("GenreUpdated", (user, message) => {
        getdata_genre();
    });


    connection.onclose(async () => {
        await start();
    });
    start();
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

// GetData:

async function getdata_manga() {
    xhrGet(API_URLS.manga)
        .then(response => response.json())
        .then(data => {
            mangas = data
            display_manga();
        })
}

async function getdata_author() {
    xhrGet(API_URLS.author)
        .then(response => response.json())
        .then(data => {
            authors = data
            display_author();
        })
}

async function getdata_genre() {
    xhrGet(API_URLS.genre)
        .then(response => response.json())
        .then(data => {
            genres = data
            display_genre();
        })
}

//Display:

function display_manga() {
    closeAllPanelsAndShowTable('mangas')
    resetInnerHtmlById('mangaresultarea')
    mangas.forEach(manga => {
        document.getElementById('mangaresultarea').innerHTML +=
            "<tr><td>" + manga.mangaId + "</td><td>"
        + manga.title + "</td><td>"
        + manga.price + "</td><td>"
        + manga.rating + "</td><td>"
        + manga.release + "</td><td>"
        + manga.authorId + "</td><td>"
        +manga.genreId + "</td></tr>" +
        `<button type= "button" onclick="remove_manga(${manga.mangaId})">Delete</button>` +
        `<button type= "button" onclick="showupdate_manga(${manga.mangaId})">Update</button>`
        + "</td ></tr>";
    });
}

function display_author() {
    closeAllPanelsAndShowTable('authors');
    resetInnerHtmlById('authorresultarea');
    authors.forEach(author => {
        document.getElementById('authorresultarea').innerHTML +=
            "<tr><td>" + author.authorId + "</td><td>"
            + author.authorName + "</td><td>" +
            `<button type="button" onclick="remove_author (${author.authorId})">Delete</Button>` +
            `<button type="button" onclick="showupdate_author (${author.authorId})">Update</Buttony`
            + "</td ></tr >";
    });
}

function display_genre() {
    closeAllPanelsAndShowTable('genres');
    resetInnerHtmlById('genreresultarea');
    genres.forEach(genre => {
        document.getElementById('genreresultarea').innerHTML +=
            "<tr><td>" + genre.genreId + "</td><td>"
            + genre.genreName + "</td><td>" +
        `<button type="button" onclick="remove_genre (${genre.genreId})">Delete</Button>` +
        `<button type="button" onclick="showupdate_genre (${genre.genreId})">Update</Button>`
            + "</td ></tr >";
    });
}

//Remove

function remove_genre(id) {
    remove(API_URLS.genre, id, getdata_genre);
}

function remove_author(id) {
    remove(API_URLS.author, id, getdata_author);
}

function remove_manga(id) {
    remove(API_URLS.manga, id, getdata_manga);
}

function remove(url, id, successFn) {
    xhrDelete(url, id)
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            successFn();
        })
        .catch((error) => { console.error('Error:', error); });
}

//Update

function showupdate_genre(id) {
    edit(TAB_UIDS.genres);

    const genre = genres.find(t => t.genreId == id)

    setElementValueById('genreNameToUpdate', genre.genreName);

    genreIdToUpdate = id;
}

function update_genre() {
    closeAllPanelsAndShowTable(TAB_UIDS.genres);

    let genreName = document.getElementById('genreNameToUpdate').value;
    fetch('http://localhost:59073/genre', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { genreId: genreIdToUpdate, genreName: genreName })
    })
        .then(respones => response)
        .then(data => {
            console.log('Success:', data);
            getdata_genre();
        })
        .catch((error) => { console.error('Error:', error); });
}

function showupdate_manga(id) {
    edit(TAB_UIDS.mangas);

    const manga = mangas.find(t => t.mangaId == id);

    setElementValueById('titleToUpdate', manga.title);
    setElementValueById('priceToUpdate', manga.price);
    setElementValueById('rateToUpdate', manga.rating);
    setElementValueById('releaseToUpdate', manga.release);
    setElementValueById('mangaAuthorIdToUpdate', manga.authorId);
    setElementValueById('mangaGenreIdToUpdate', manga.genreId);


    mangaIdToUpdate = id;
}

function update_manga() {
    closeAllPanelsAndShowTable(TAB_UIDS.mangas);

    let title = document.getElementById('titleToUpdate').value;
    let price = document.getElementById('priceToUpdate').value;
    let rating = document.getElementById('rateToUpdate').value;
    let release = document.getElementById('releaseToUpdate').value;
    let authorId = document.getElementById('mangaAuthorIdToUpdate').value;
    let genreId = document.getElementById('mangaGenreIdToUpdate').value;
    fetch('http://localhost:59073/manga', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { mangaId: mangaIdToUpdate, title: title, price: price, rating: rating, release: release, authorId: authorId, genreId: genreId })
    })
        .then(respones => response)
        .then(data => {
            console.log('Success:', data);
            getdata_manga();
        })
        .catch((error) => { console.error('Error:', error); });
}

function showupdate_author(id) {

    edit(TAB_UIDS.authors);

    const author = authors.find(t => t.authorId == id)

    setElementValueById('authorNameToUpdate', author.authorName);

    authorIdToUpdate = id;
}

function update_author() {
    closeAllPanelsAndShowTable(TAB_UIDS.authors);

    let authorName = document.getElementById('authorNameToUpdate').value;
    fetch('http://localhost:59073/author', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { authorId: authorIdToUpdate, authorName: authorName  })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata_author();
        })
        .catch((error) => { console.error('Error:', error); });

}

function update(url, request, successFn) {
    xhrPut(url, request)
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            successFn();
        })
        .catch((error) => { console.error('Error:', error); });
}

function create_genre() {
    let genreName = document.getElementById('genreName').value;
    fetch('http://localhost:59073/genre', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {genreName: genreName})
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata_genre();
        })
        .catch((error) => { console.error('Error:', error); });
}

function create_author() {
    let authorName = document.getElementById('authorName').value;
    fetch('http://localhost:59073/author', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { authorName: authorName })
    })
        .then(response => response)
        .then(data => {
            console.log('Succes:', data);
            getdata_author();
        })
        .catch((error) => { console.error('Error:', error) });
}

function create_manga() {
    let title = document.getElementById('title').value;
    let price = document.getElementById('price').value;
    let rate = document.getElementById('rate').value;
    let release = document.getElementById('release').value;
    let authorId = document.getElementById('authorId').value;
    let genreId = document.getElementById('genreId').value;
    fetch('http://localhost:59073/manga', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { title: title, price: price, rating: rate, release: release, authorId: authorId, genreId: genreId })
    })
        .then(response => response)
        .then(data => {
            console.log('Succes:', data);
            getdata_manga();
        })
        .catch((error) => { console.error('Error:', error) });
}

function create(url, request, succesFn) {
    xhrPost(url, request)
        .then(response => response)
        .then(data => {
            console.log('Succes:', data);
            succesFn();
        })
        .catch((error) => { console.error('Error:', error); });
    
}

function getAllPriceByGenre() {
    xhrGet(API_URLS.getAllPriceByGenre)
        .then(response => response.json())
        .then(data => {
            console.log(data)
            displayAllPriceByGenre(data)
        })
}

function displayAllPriceByGenre(data) {
    document.getElementById('AllPriceByGenre').innerHTML = JSON.stringify(data)
}

function getAllPriceByYears() {
    xhrGet(API_URLS.getAllPriceByYears)
        .then(response => response.json())
        .then(data => {
            console.log(data)
            displayAllPriceByYears(data)
        })
}

function displayAllPriceByYears(data) {
    document.getElementById('AllPriceByYears').innerHTML = JSON.stringify(data)
}

function getAVGRateByGenre() {
    xhrGet(API_URLS.getAVGRateByGenre)
        .then(response => response.json())
        .then(data => {
            console.log(data)
            displayAVGRateByGenre(data)
        })
}

function displayAVGRateByGenre(data) {
    document.getElementById('AVGRateByGenre').innerHTML = JSON.stringify(data)
}

function getAVGPriceByGenre() {
    xhrGet(API_URLS.getAVGPriceByGenre)
        .then(response => response.json())
        .then(data => {
            console.log(data)
            displayAVGPriceByGenre(data)
        })
}

function displayAVGPriceByGenre(data) {
    document.getElementById('AVGPriceByGenre').innerHTML = JSON.stringify(data)
}

function getAVGPriceByAuthor() {
    xhrGet(API_URLS.getAVGPriceByAuthor)
        .then(response => response.json())
        .then(data => {
            console.log(data)
            displayAVGPriceByAuthor(data)
        })
}

function displayAVGPriceByAuthor(data) {
    document.getElementById('AVGPriceByAuthor').innerHTML = JSON.stringify(data)
}

function add(value) {
    const tableElement = document.getElementById(`${value}${ID_POSFIX.table}`);
    const addFormElement = document.getElementById(`${value}${ID_POSFIX.add}`);

    addClass(tableElement, ELEMENT_CLASSES.hidden);
    removeClass(addFormElement, ELEMENT_CLASSES.hidden);
}

function edit(value) {
    const tableElement = document.getElementById(`${value}${ID_POSFIX.table}`);
    const editFormElement = document.getElementById(`${value}${ID_POSFIX.edit}`);

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
    return urlParams.get(TAB_QUERY_PARAM);
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

function setElementVisibilityById(id, value) {
    document.getElementById(id).style.display = value;
}

function setElementValueById(id, value) {
    document.getElementById(id).value = value;
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
    if (element.classList.contains(className)) {
        element.classList.remove(className);
    }
}

async function xhrGet(url) {
    return await fetch(`http://localhost:59073/${url}`, {
        method: XHR_TYPES.get,
        headers: defaHeader
    })
}

async function xhrPost(url, request) {
    fetch(`http://localhost:59073/${url}`, {
        method: XHR_TYPES.post,
        headers: defaHeader,
        body: JSON.stringify(request)
    })
}

async function xhrPut(url, request) {
    fetch(`http://localhost:59073/${url}`, {
        method: XHR_TYPES.put,
        headers: defaHeader,
        body: JSON.stringify(request)
    })
}

async function xhrDelete(url, id) {
    return await fetch(`http://localhost:59073/${url}/${id}`, {
        method: XHR_TYPES.delete,
        headers: defaHeader
    })
}