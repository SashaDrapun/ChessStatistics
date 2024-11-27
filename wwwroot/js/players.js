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
        document.querySelector('#TitlePlayerEdit').value = e.target.getAttribute('titlePlayer');
        document.querySelector('#CurrentRatingClassicEdit').value = e.target.getAttribute('ratingPlayerClassic');
        document.querySelector('#CurrentRatingRapidEdit').value = e.target.getAttribute('ratingPlayerRapid');
        document.querySelector('#CurrentRatingBlitzEdit').value = e.target.getAttribute('ratingPlayerBlitz');
    });
}

function validateInput(input, min, max) {
    if (input.value < min) {
        input.value = min;
    } else if (input.value > max) {
        input.value = max;
    }
}