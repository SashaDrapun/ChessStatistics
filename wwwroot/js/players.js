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

document.getElementById('rankFilter').addEventListener('change', filterPlayers);
document.getElementById('blitzFilter').addEventListener('input', filterPlayers);
document.getElementById('rapidFilter').addEventListener('input', filterPlayers);
document.getElementById('classicFilter').addEventListener('input', filterPlayers);

function filterPlayers() {
    const rankFilter = document.getElementById('rankFilter').value;
    const blitzFilter = document.getElementById('blitzFilter').value;
    const rapidFilter = document.getElementById('rapidFilter').value;
    const classicFilter = document.getElementById('classicFilter').value;

    const cards = document.querySelectorAll('.player-card');
    cards.forEach(card => {
        const rank = card.getAttribute('data-rank');
        const blitz = card.getAttribute('data-blitz');
        const rapid = card.getAttribute('data-rapid');
        const classic = card.getAttribute('data-classic');

        const shouldShow = (rankFilter === 'all' || rankFilter === rank) &&
                          (blitzFilter === '' || blitz >= blitzFilter) &&
                          (rapidFilter === '' || rapid >= rapidFilter) &&
                          (classicFilter === '' || classic >= classicFilter);

        card.style.display = shouldShow ? 'flex' : 'none';
    });
}
