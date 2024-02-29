const buttonsDeletePlayer = document.querySelectorAll('.deleteTournament');

for (let i = 0; i < buttonsDeletePlayer.length; i++) {
    buttonsDeletePlayer[i].addEventListener('click', e => {
        document.querySelector('#beforeDeleteTournamentText').textContent = "Вы уверены, что хотите удалить турнир c названием: " + e.target.getAttribute('tournamentName') + "?";
        document.querySelector('#deleteTournamentForm').action = "/Tournaments/DeleteTournament?idTournament=" + e.target.getAttribute('tournamentId');
    });
}


const buttonsEditPlayer = document.querySelectorAll('.editPlayer');

for (let i = 0; i < buttonsEditPlayer.length; i++) {
    buttonsEditPlayer[i].addEventListener('click', e => {
        document.querySelector('#IdPlayerEdit').value = e.target.getAttribute('idPlayer');
        document.querySelector('#FIOEdit').value = e.target.getAttribute('fioPlayer');
        document.querySelector('#TitlePlayerEdit').value = e.target.getAttribute('titlePlayer');
    });
}

