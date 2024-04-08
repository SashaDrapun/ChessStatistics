const buttonsDeleteTournament = document.querySelectorAll('.deleteTournament');

for (let i = 0; i < buttonsDeleteTournament.length; i++) {
    buttonsDeleteTournament[i].addEventListener('click', e => {
        document.querySelector('#beforeDeleteTournamentText').textContent = "Вы уверены, что хотите удалить турнир c названием: " + e.target.getAttribute('tournamentName') + "?";
        document.querySelector('#deleteTournamentForm').action = "/Tournaments/DeleteTournament?idTournament=" + e.target.getAttribute('tournamentId');
    });
}


const buttonsEditTournament = document.querySelectorAll('.editTournament');

for (let i = 0; i < buttonsEditTournament.length; i++) {
    buttonsEditTournament[i].addEventListener('click', e => {

        document.querySelector('#IdTournamentEdit').value = e.target.getAttribute('tournamentId');
        document.querySelector('#TournamentNameEdit').value = e.target.getAttribute('tournamentName');

        console.dir(document.querySelector('#TournamentTypeEdit'));
        console.log(e.target.getAttribute('tournamentType'));
        document.querySelector('#DateStartEdit').value = e.target.getAttribute('tournamentDateStart');
        document.querySelector('#TournamentTypeEdit').value = e.target.getAttribute('tournamentType');
        document.querySelector('#RatingTypeEdit').value = e.target.getAttribute('ratingType');
        document.querySelector('#countToursEdit').value = e.target.getAttribute('tournamentCountTours');
        
    });
}

