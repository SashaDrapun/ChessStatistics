document.addEventListener('DOMContentLoaded', () => {
    const countToursElement = document.getElementById('countToursAddTournament');
    const countToursElementEdit = document.getElementById('countToursEditTournament');
    const platformGroup = document.getElementById('platformGroup');
    const platformGroupEdit = document.getElementById('platformGroupEdit');
    
    countToursElement.style.display = 'none';
    countToursElementEdit.style.display = 'none';

    const buttonsDeleteTournament = document.querySelectorAll('.deleteTournament');
    buttonsDeleteTournament.forEach(button => {
        button.addEventListener('click', handleDeleteButtonClick);
    });

    const buttonsEditTournament = document.querySelectorAll('.editTournament');
    buttonsEditTournament.forEach(button => {
        button.addEventListener('click', handleEditButtonClick);
    });

    document.getElementById('OnlineOffline').addEventListener('change', handleOnlineOfflineChange);
    document.getElementById('TournamentType').addEventListener('change', handleTournamentTypeChange);
    document.getElementById('statusEdit').addEventListener('change', handleStatusEditChange);
    document.getElementById('TournamentTypeEdit').addEventListener('change', handleTournamentTypeEditChange);

    document.getElementById('statusFilter').addEventListener('change', filterTournaments);
    document.getElementById('typeFilter').addEventListener('change', filterTournaments);
    document.getElementById('cityFilter').addEventListener('change', filterTournaments);
    document.getElementById('ratingFilter').addEventListener('change', filterTournaments);
    document.getElementById('ageFilter').addEventListener('change', filterTournaments);

    // Initial check for the tournament type
});

function handleDeleteButtonClick(e) {
    const tournamentName = e.target.getAttribute('tournamentName');
    const tournamentId = e.target.getAttribute('tournamentId');
    document.querySelector('#beforeDeleteTournamentText').textContent = `Вы уверены, что хотите удалить турнир с названием: ${tournamentName}?`;
    document.querySelector('#deleteTournamentForm').action = `/Tournaments/DeleteTournament?idTournament=${tournamentId}`;
}

function handleEditButtonClick(e) {
    e.preventDefault();
    handleTournamentTypeEditChange({ target: document.getElementById('TournamentTypeEdit') });
    const button = e.target;
    const tournamentData = getTournamentDataFromButton(button);
    populateEditForm(tournamentData);
    togglePlatformGroupEdit(tournamentData.tournamentStatus);
    toggleCountToursEdit(tournamentData.tournamentType);
}

function getTournamentDataFromButton(button) {
    return {
        tournamentId: button.getAttribute('tournamentId'),
        tournamentName: button.getAttribute('tournamentName'),
        tournamentDateStart: button.getAttribute('tournamentDateStart'),
        ratingType: button.getAttribute('ratingType'),
        tournamentCountTours: button.getAttribute('tournamentCountTours'),
        tournamentType: button.getAttribute('tournamentType'),
        tournamentCity: button.getAttribute('tournamentCity'),
        tournamentAdress: button.getAttribute('tournamentAdress'),
        tournamentStatus: button.getAttribute('tournamentStatus'),
        tournamentPlatform: button.getAttribute('tournamentPlatform'),
        tournamentLink: button.getAttribute('tournamentLink'),
        tournamentMinYear: button.getAttribute('tournamentMinYear'),
        tournamentMaxRating: button.getAttribute('tournamentMaxRating'),
        tournamentMaxCountPlayers: button.getAttribute('tournamentMaxCountPlayers'),
        tournamentIsPlatformCalculated: button.getAttribute('tournamentIsPlatformCalculated'),
        tournamentCost: button.getAttribute('tournamentCost')
    };
}

function populateEditForm(tournamentData) {
    document.querySelector('#IdTournamentEdit').value = tournamentData.tournamentId;
    document.querySelector('#TournamentNameEdit').value = tournamentData.tournamentName;
    document.querySelector('#DateStartEdit').value = tournamentData.tournamentDateStart;
    document.querySelector('#RatingTypeEdit').value = tournamentData.ratingType;
    document.querySelector('#countToursEdit').value = tournamentData.tournamentCountTours;
    document.querySelector('#TournamentTypeEdit').value = tournamentData.tournamentType;
    document.getElementById('cityEdit').value = tournamentData.tournamentCity;
    document.querySelector('#addressEdit').value = tournamentData.tournamentAdress;
    document.getElementById('statusEdit').value = tournamentData.tournamentStatus;
    document.querySelector('#platformEdit').value = tournamentData.tournamentPlatform;
    document.querySelector('#tournamentLinkEdit').value = tournamentData.tournamentLink;
    document.querySelector('#minYearEdit').value = tournamentData.tournamentMinYear;
    document.querySelector('#maxRatingEdit').value = tournamentData.tournamentMaxRating;
    document.querySelector('#maxParticipantsEdit').value = tournamentData.tournamentMaxCountPlayers;
    document.getElementById('isPlatformCalculatedEdit').value = tournamentData.tournamentIsPlatformCalculated;
    document.getElementById('CostEdit').value = tournamentData.tournamentCost.toString();
    console.log(tournamentData.tournamentCost.toString());
    
}

function togglePlatformGroupEdit(tournamentStatus) {
    platformGroupEdit.style.display = tournamentStatus == 0 ? 'block' : 'none';
}

function toggleCountToursEdit(tournamentType) {
    const countToursElementEdit = document.getElementById('countToursEditTournament');
    if (countToursElementEdit)
    {
        countToursElementEdit.style.display = tournamentType === "1" ? 'block' : 'none';
        
        const countToursEditInput = document.getElementById('countToursEdit');
        if (countToursEditInput){
            countToursEditInput.required = tournamentType === "1";
        }
    }
}

function handleOnlineOfflineChange() {
    platformGroup.style.display = this.value === 'online' ? 'block' : 'none';
}

function handleTournamentTypeChange() {
    const countToursElement = document.getElementById('countToursAddTournament');
    if (countToursElement)
    {
        countToursElement.style.display = this.value === "1" ? 'block' : 'none';
        const countToursInput = document.getElementById('countTours');
   
        if (countToursInput)
        {
            countToursInput.required = this.value === "1";
        }
    }
}

function handleStatusEditChange(e) {
    platformGroupEdit.style.display = e.target.value === "0" ? 'block' : 'none';
}

function handleTournamentTypeEditChange(e) {
    const countToursElementEdit = document.getElementById('countToursEditTournament');
    countToursElementEdit.style.display = e.target.value === "1" ? 'block' : 'none';
    const countToursEditInput = document.getElementById('countToursEdit');
    countToursEditInput.required = e.target.value === "1";
}

function filterTournaments() {
    const statusFilter = document.getElementById('statusFilter').value;
    const typeFilter = document.getElementById('typeFilter').value;
    const cityFilter = document.getElementById('cityFilter').value;
    const ratingFilter = document.getElementById('ratingFilter').value;
    const ageFilter = document.getElementById('ageFilter').value;

    const cards = document.querySelectorAll('.tournament-card');
    cards.forEach(card => {
        const status = card.getAttribute('data-status');
        const type = card.getAttribute('data-type');
        const city = card.getAttribute('data-city');
        const rating = card.getAttribute('data-rating');
        const age = card.getAttribute('data-age');

        const shouldShow = (statusFilter === 'all' || statusFilter === status) &&
            (typeFilter === 'all' || typeFilter === type) &&
            (cityFilter === 'all' || cityFilter === city) &&
            (ratingFilter === 'all' || ratingFilter === rating) &&
            (ageFilter === 'all' || ageFilter === age);

        card.style.display = shouldShow ? 'flex' : 'none';
    });
}

function validateInput(input, min, max) {
    if (input.value < min) {
        input.value = min;
    } else if (input.value > max) {
        input.value = max;
    }
}
document.getElementById('TournamentTypeEdit').addEventListener('change', function(e) {
    var countToursEditTournament = document.getElementById('countToursEditTournament');
    if (e.target.value == 0) {
        countToursEditTournament.style.display = 'none';
    } else {
        countToursEditTournament.style.display = 'block';
    }
});

document.getElementById('TournamentType').addEventListener('change', function(e) {
    var countToursEditTournament = document.getElementById('countToursAddTournament');
    if (e.target.value == 0) {
        countToursEditTournament.style.display = 'none';
    } else {
        countToursEditTournament.style.display = 'block';
    }
});