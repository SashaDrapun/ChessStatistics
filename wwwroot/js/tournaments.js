
    const countToursElement = document.getElementById('countToursAddTournament');
    countToursElement.style.display = 'none';

    const countToursElementEdit = document.getElementById('countToursEditTournament');
    countToursElementEdit.style.display = 'none';

    const buttonsDeleteTournament = document.querySelectorAll('.deleteTournament');
    buttonsDeleteTournament.forEach(button => {
        button.addEventListener('click', e => {
            const tournamentName = e.target.getAttribute('tournamentName');
            const tournamentId = e.target.getAttribute('tournamentId');
            document.querySelector('#beforeDeleteTournamentText').textContent = `Вы уверены, что хотите удалить турнир с названием: ${tournamentName}?`;
            document.querySelector('#deleteTournamentForm').action = `/Tournaments/DeleteTournament?idTournament=${tournamentId}`;
        });

    const buttonsEditTournament = document.querySelectorAll('.editTournament');
    buttonsEditTournament.forEach(button => {
        button.addEventListener('click', e => {
            e.preventDefault();
            console.log(button);
            const tournamentId = button.getAttribute('tournamentId');
            const tournamentName = button.getAttribute('tournamentName');
            const tournamentDateStart = button.getAttribute('tournamentDateStart');
            const ratingType = button.getAttribute('ratingType');
            const tournamentCountTours = button.getAttribute('tournamentCountTours');
            const tournamentType = button.getAttribute('tournamentType');
            const tournamentCity = button.getAttribute('tournamentCity');
            const tournamentAdress = button.getAttribute('tournamentAdress');
            const tournamentStatus = button.getAttribute('tournamentStatus');
            const tournamentPlatform = button.getAttribute('tournamentPlatform');
            const tournamentLink = button.getAttribute('tournamentLink');
            const tournamentMinYear = button.getAttribute('tournamentMinYear');
            const tournamentMaxRating = button.getAttribute('tournamentMaxRating');
            const tournamentMaxCountPlayers = button.getAttribute('tournamentMaxCountPlayers');
            const tournamentIsPlatformCalculated = button.getAttribute('tournamentIsPlatformCalculated');

            document.querySelector('#IdTournamentEdit').value = tournamentId;
            document.querySelector('#TournamentNameEdit').value = tournamentName;
            document.querySelector('#DateStartEdit').value = tournamentDateStart;
            document.querySelector('#RatingTypeEdit').value = ratingType;
            document.querySelector('#countToursEdit').value = tournamentCountTours;
            document.querySelector('#TournamentTypeEdit').value = tournamentType;
            document.querySelector('#cityEdit').value = tournamentCity;
            document.querySelector('#addressEdit').value = tournamentAdress;
            document.querySelector('#statusEdit').value = tournamentStatus;
            document.querySelector('#platformEdit').value = tournamentPlatform;
            document.querySelector('#tournamentLinkEdit').value = tournamentLink;
            document.querySelector('#minYearEdit').value = tournamentMinYear;
            document.querySelector('#maxRatingEdit').value = tournamentMaxRating;
            document.querySelector('#maxParticipantsEdit').value = tournamentMaxCountPlayers;
            document.querySelector('#isPlatformCalculatedEdit').value = tournamentIsPlatformCalculated;

            const countToursElementEdit = document.getElementById('countToursEditTournament');
            countToursElementEdit.style.display = tournamentType === "1" ? 'block' : 'none';
        });
    });

    document.getElementById('TournamentTypeEdit').addEventListener('change', e => {
        const countToursElementEdit = document.getElementById('countToursEditTournament');
        countToursElementEdit.style.display = e.target.value === "1" ? 'block' : 'none';
    });

    document.getElementById('TournamentType').addEventListener('change', e => {
        const countToursElement = document.getElementById('countToursAddTournament');
        countToursElement.style.display = e.target.value === "1" ? 'block' : 'none';
    });
});