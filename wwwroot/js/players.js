const buttonsDeletePlayer = document.querySelectorAll('.deletePlayer');

for (let i = 0; i < buttonsDeletePlayer.length; i++) {
    buttonsDeletePlayer[i].addEventListener('click', e => {
        document.querySelector('#deletePlayerForm').action = "/Players/DeletePlayer?idPlayer=" + e.target.getAttribute('idPlayer');
    });
}

const buttonsEditPlayer = document.querySelectorAll('.editPlayer');

for (let i = 0; i < buttonsEditPlayer.length; i++) {
    buttonsEditPlayer[i].addEventListener('click', e => {
        document.querySelector('#IdPlayerEdit').value = e.target.getAttribute('idPlayer');
        document.querySelector('#FIOEdit').value = e.target.getAttribute('fioPlayer');
        console.log(e.target.getAttribute('titlePlayer'));
        document.querySelector('#TitlePlayerEdit').value = e.target.getAttribute('titlePlayer');
        console.log(document.querySelector('#TitlePlayerEdit').value);
    });
}

