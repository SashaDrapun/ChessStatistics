$(document).ready(function () {

    new DataTable('#tournamentTable', {
        lengthMenu: [10, 12]
    });

    var usersParticipatingInTournamentTable = new DataTable('#usersParticipatingInTournamentTable', {
        lengthMenu: [10, 12]
    });

    new DataTable('#playersTable', {
        lengthMenu: [10, 12]
    });
    
});

