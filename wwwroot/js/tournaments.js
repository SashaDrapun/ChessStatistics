document.addEventListener('DOMContentLoaded', function () {
    const countToursElement = document.getElementById('countToursAddTournament');
    countToursElement.style.display = 'none';

    const countToursElementEdit = document.getElementById('countToursEditTournament');
    countToursElementEdit.style.display = 'none';
});

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
        
        document.querySelector('#RatingTypeEdit').value = e.target.getAttribute('ratingType');
        document.querySelector('#countToursEdit').value = e.target.getAttribute('tournamentCountTours');

        let tournamentType = e.target.getAttribute('tournamentType');
        document.querySelector('#TournamentTypeEdit').value = tournamentType;
        const countToursElement = document.getElementById('countToursEditTournament');
        if (tournamentType == "1") {
            countToursElement.style.display = 'block';
        }
        else {
            countToursElement.style.display = 'none';
        }
    });
}

document.getElementById('TournamentTypeEdit').addEventListener('change', e => {
    const countToursElement = document.getElementById('countToursEditTournament');
    if (e.target.value == "1") {
        countToursElement.style.display = 'block';
    }
    else {
        countToursElement.style.display = 'none';
    }


});


document.getElementById('TournamentType').addEventListener('change', e => {
    const countToursElement = document.getElementById('countToursAddTournament');
    if (e.target.value == "1") {
        countToursElement.style.display = 'block';
    }
    else {
        countToursElement.style.display = 'none';
    }
    
    
});

