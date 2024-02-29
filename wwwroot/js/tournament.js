const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/Tournament")
    .build();

hubConnection.start();
console.dir(usersParticipatingInTournamentTable);

async function AddParticipant(idPlayer, idTournament) {
    const response = await fetch("/api/tournamentParticipants/", {
        method: "POST",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            IdPlayer: parseInt(idPlayer),
            IdTournament: parseInt(idTournament)
        })
    });
    if (response.ok === true) {
        const player = await response.json();
        hubConnection.invoke("Send", player.id, player.fio, player.rating, player.title);
    }

    hubConnection.on('Send', function (playerId, playerFIO, playerRating, playerTitle) {
        let option = document.getElementById(playerId);
        option.parentNode.removeChild(option);

        table.row.add([playerFIO, playerRating, playerTitle]).draw();
    });
}

document.forms["AddParticipant"].addEventListener("submit", e => {
    e.preventDefault();
    const tournamentId = document.querySelector('#tournamentId').value;
    const playerId = document.querySelector('#playerId').value;
    AddParticipant(playerId, tournamentId);
});
