const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/Tournament")
    .build();

hubConnection.start();

async function AddParticipant(idPlayer, idTournament) {
    const response = await fetch("/api/Tournament/AddTournamentParticipants/", {
        method: "POST",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            IdPlayer: parseInt(idPlayer),
            IdTournament: parseInt(idTournament)
        })
    });
    if (response.ok === true) {
        const player = await response.json();
        hubConnection.invoke("AddTournamentParticipant", player.idPlayer, player.fio, player.currentRating, player.rankOutput);
    }
}

hubConnection.on('AddTournamentParticipant', function (playerId, playerFIO, playerRating, playerTitle) {
    let option = document.getElementById(playerId);
    option.parentNode.removeChild(option);
    let table = $("#usersParticipatingInTournamentTable").DataTable();
    table.row.add([playerFIO, playerRating, playerTitle]).draw();
});

async function GeneratingTournamentDraw(idTournament) {
    const response = await fetch("/api/Tournament/GenerateTournamentDraw/", {
        method: "POST",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            IdTournament: parseInt(idTournament)
        })
    });
    if (response.ok === true) {
        let tournamentDraw = await response.json();
        console.dir(tournamentDraw);
        try
        {
            hubConnection.invoke("GeneratingTournamentDraw", tournamentDraw);
        }
        catch (error) {
            console.error('An error occurred while invoking GeneratingTournamentDraw:', error);
           
        }
    }
}

hubConnection.on('GeneratingTournamentDraw', function (tournamentDraw) {
    let tournamentId = document.querySelector('#tournamentId').value;
    
    if (tournamentDraw != null && tournamentDraw.tours[0].idTournament == tournamentId) {
        let tournamentType = document.querySelector('#tournamentType').value;
        if (tournamentType == 0) {
            for (let index = 0; index < tournamentDraw.tours.length; index++) {
                DrawTournamentDraw(tournamentDraw, index);
            }
        }
        else {
            DrawTournamentDraw(tournamentDraw, tournamentDraw.tours.length - 1);
            let countTours = document.querySelector('#countTours').value;
            if (countTours == tournamentDraw.tours.length) {
                document.forms["GenerateNextTour"].parentNode.removeChild(document.forms["GenerateNextTour"]);
            }
        }
        
        document.querySelector("#GenerateTournamentDraw").remove();
        SetGameResultSelects();
    }
});

function DrawTournamentDraw(tournamentDraw, index) {
    let i = index + 1;

    let tourNumber = "Тур " + i;
    let tourNavDiv = document.createElement('div');
    tourNavDiv.innerHTML = `<button class="nav-link" id="nav-tour${i}-tab" data-bs-toggle="tab" 
                                data-bs-target="#nav-tour${i}" type="button" role="tab" aria-controls="nav-tour${i}"
                                 aria-selected="true">${tourNumber}</button>`;
    let tourNav = tourNavDiv.firstChild;

    let tourContentDiv = document.createElement('div');
    let tourContentText = `<div class="tab-pane fade show" id="nav-tour${i}" role="tabpanel" aria-labelledby="nav-tour${i}-tab">
                        <div class="d-flex justify-content-center">
                            <table id="usersParticipatingInTournamentTable" class="table table-responsive-sm table-bordered table-hover table-dark regular-table">
                <thead>
                    <tr>
                        <th scope="col">#</th>
                        <th scope="col">Белые</th>
                        <th scope="col">Очки</th>
                        <th scope="col">Результат</th>
                        <th scope="col">Очки</th>
                        <th scope="col">Черные</th>
                    </tr>
                </thead>
                <tbody id="PlayersParticipatingInTournament">`;
    for (let jindex = 0; jindex < tournamentDraw.tours[index].games.length; jindex++) {
        j = jindex + 1;
        tournamentDraw.tours[index].games.length
        tourContentText += `<tr>
                            <th scope="row">
                                ${j}
                            </th>
                            <th scope="row" id="Tour${tournamentDraw.tours[index].idTour}Game${tournamentDraw.tours[index].games[jindex].idGame}PlayerWhiteFIO">
                                ${tournamentDraw.tours[index].games[jindex].playerWhite.fio}
                             </th>
                             <th scope="row" id="Tour${tournamentDraw.tours[index].idTour}Game${tournamentDraw.tours[index].games[jindex].idGame}PlayerWhiteScore">
                                1
                             </th>
                            <th scope="row">
                                <select
                                        id="result"
                                        name="GameResult"
                                        idGame="${tournamentDraw.tours[index].games[jindex].idGame}"
                                        class="form-control gameResultSelect">
                                        <option selected disabled>Заполнить</option>
                                        <option value="0">1-0</option>
                                        <option value="1">1/2</option>
                                        <option value="2">0-1</option>
                                </select>
                            </th>
                            <th scope="row" id="Tour${tournamentDraw.tours[index].idTour}Game${tournamentDraw.tours[index].games[jindex].idGame}PlayerBlackScore">
                                2
                            </th>
                            <th scope="row" id="Tour${tournamentDraw.tours[index].idTour}Game${tournamentDraw.tours[index].games[jindex].idGame}PlayerBlackFIO">
                                ${tournamentDraw.tours[index].games[jindex].playerBlack.fio}
                             </th>
                        </tr>`;
    }

    if (tournamentDraw.tours[index].idPlayerSkippingGame > 0) {
        tourContentText += `<tr>
            <th scope="row">
                ${(tournamentDraw.tours[index].games.length + 1)}
            </th>
            <th scope="row">
                ${tournamentDraw.tours[index].playerSkippingGame.fio} 
            </th>
            <th scope="row">
                1
            </th>
            <th scope="row">
                Пропуск
            </th>
            <th scope="row">

            </th>
            <th scope="row">

            </th>
        </tr>`
    }
    tourContentText += `</tbody>
            </table>
                        </div>
                                    </div>`;
    tourContentDiv.innerHTML = tourContentText
    let tourContent = tourContentDiv.firstChild;

    document.querySelector("#nav-tabTours").appendChild(tourNav);
    document.querySelector("#nav-tabContent2").appendChild(tourContent);
}

async function SetGameResult(idGame, gameResult) {
    console.log("Зашли в SetGameResult");
    const response = await fetch("/api/Tournament/SetGameResult/", {
        method: "POST",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            IdGame: parseInt(idGame),
            GameResult: parseInt(gameResult)
        })
    });
    if (response.ok === true) {
        const game = await response.json();
        hubConnection.invoke("SetGameResult", game);
    }
    else {
        console.log("Запрос провален");
    }
}

hubConnection.on('SetGameResult', function (gameModel) {
    let playerWhiteFIO = document.querySelector(`#Tour${gameModel.idTour}Game${gameModel.idGame}PlayerWhiteFIO`);
    let playerWhiteScore = document.querySelector(`#Tour${gameModel.idTour}Game${gameModel.idGame}PlayerWhiteScore`);
    let playerBlackFIO = document.querySelector(`#Tour${gameModel.idTour}Game${gameModel.idGame}PlayerBlackFIO`);
    let playerBlackScore = document.querySelector(`#Tour${gameModel.idTour}Game${gameModel.idGame}PlayerBlackScore`);

    console.log("Данные:")
    console.dir(gameModel);
    playerWhiteFIO.innerText = `${gameModel.playerWhite.fio} \n ${gameModel.ratingWhite} ${gameModel.ratingWhiteChange}`
    playerBlackFIO.innerText = `${gameModel.playerBlack.fio} \n ${gameModel.ratingBlack} ${gameModel.ratingBlackChange}`
});

function SetGameResultSelects() {
    let gameResultSelects = document.querySelectorAll('.gameResultSelect');

    if (gameResultSelects != null) {
        for (let i = 0; i < gameResultSelects.length; i++) {
            gameResultSelects[i].addEventListener('change', e => {
                let idGame = e.target.getAttribute('idGame');
                let gameResult = e.target.value;
                SetGameResult(idGame, gameResult);
            });
        }
    }
}

document.forms["AddParticipant"].addEventListener("submit", e => {
    e.preventDefault();
    const tournamentId = document.querySelector('#tournamentId').value;
    const playerId = document.querySelector('#playerId').value;
    AddParticipant(playerId, tournamentId);
});

if (document.forms["GeneratingTournamentDraw"] != null) {
    document.forms["GeneratingTournamentDraw"].addEventListener("submit", e => {
        e.preventDefault();
        const tournamentId = document.querySelector('#idTournament').value;
        GeneratingTournamentDraw(tournamentId);
    });
}

if (document.forms["GenerateNextTour"] != null) {
    document.forms["GenerateNextTour"].addEventListener("submit", e => {
        e.preventDefault();
        let table = document.getElementById('usersParticipatingInTournamentTable');
        let countPlayers = table.rows.length - 1;
        let countTours = document.getElementById('countTours').value; 
        let minimumNumberOfParticipants = 2 + parseInt(countTours);

        if (countPlayers < minimumNumberOfParticipants) {
            const alertContainer = document.getElementById('alertWhenInsufficientNumberOfParticipants');
            const alertHtml = `
            <div class="alert alert-warning alert-dismissible fade show" role="alert">
                <strong>Минимальное количество участников при ${countTours} турах = ${minimumNumberOfParticipants}</strong>
                <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
            </div>`;
            alertContainer.innerHTML = alertHtml;
            return;
        }
        
        if (AreAllTheResultsEntered()) {
            const tournamentId = document.querySelector('#idTournament').value;
            GeneratingTournamentDraw(tournamentId);
        }
        else {
            const alertContainer = document.getElementById('alertWhenNotAllResultsEnteredContainer');
            const alertHtml = `
            <div class="alert alert-warning alert-dismissible fade show" role="alert">
                <strong>Заполните результаты текущего тура, прежде чем генерировать следующий тур</strong>
                <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
            </div>`;

            alertContainer.innerHTML = alertHtml;
        }
    });
}

function AreAllTheResultsEntered() {
    let gameResultSelects = document.querySelectorAll('.gameResultSelect');

    if (gameResultSelects != null) {
        for (let i = 0; i < gameResultSelects.length; i++) {
            if (gameResultSelects[i].value == "Заполнить") {
                return false;
            }
        }
    }

    return true;
}

SetGameResultSelects();
