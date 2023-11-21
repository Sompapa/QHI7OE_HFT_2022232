fetch('http://localhost:28642/manga')
    .then(x => x.json())
    .then(y => console.log(y))