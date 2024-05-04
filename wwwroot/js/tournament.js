const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/Tournament")
    .build();

hubConnection.start();




function SetButtonsDeleteParicipant() {
    const buttonsDeleteParticipant = document.querySelectorAll('.deleteParticipant');
    for (let i = 0; i < buttonsDeleteParticipant.length; i++) {
        buttonsDeleteParticipant[i].addEventListener('click', e => {
            DeleteParticipant(e.target.getAttribute('playerId'), e.target.getAttribute('tournamentId'));
        });
    }
}

SetButtonsDeleteParicipant();


async function DeleteParticipant(idPlayer, idTournament) {
    const response = await fetch("/api/Tournament/DeleteTournamentParticipant/", {
        method: "POST",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            IdPlayer: parseInt(idPlayer),
            IdTournament: parseInt(idTournament)
        })
    });
    if (response.ok === true) {
        const player = await response.json();
        hubConnection.invoke("DeleteTournamentParticipant", player);
    }
    else {
        console.log("При удалении пользователя произошла ошибка");
        console.dir(response);
    }
}

function removeRowByFIO(fio) {
    // Найти все строки таблицы
    let table = $("#usersParticipatingInTournamentTable").DataTable();
    console.log("Удаляем из таблицы " + fio);
    // Перебрать все строки
    table.rows().every(function (rowIdx) {
        let rowData = this.data();
        let fioCell = rowData[0]; // предполагаем, что ФИО находится в первой колонке

        // Сравнить текст ячейки с искомым ФИО
        if ($.trim(fioCell) === fio) {
            // Удалить строку, если текст совпадает
            table.row(rowIdx).remove().draw(false);
            return false; // Прекратить поиск после первого совпадения
        }
    });
}

hubConnection.on('DeleteTournamentParticipant', function (player) {
    console.log("Зашли в DeleteTournamentParticipant");
    console.dir(player);

    let newOption = document.createElement("option");

    newOption.value = player.idPlayer;
    newOption.textContent = player.fio;
    newOption.id = player.idPlayer;
    var selectElement = document.getElementById("playerId");
    selectElement.appendChild(newOption);
    console.log("перед удалением из таблицы");
    removeRowByFIO(player.fio);
});

function addPlayerToTable(player) {
    console.log("Добавляем " + player.fio);
    let table = $("#usersParticipatingInTournamentTable").DataTable();

    // Проверяем, существует ли столбец с кнопкой "Удаление участника"
    let deleteButtonColumnExists = false;
    let headers = $('#usersParticipatingInTournamentTable thead th');
    headers.each(function () {
        if ($(this).text().trim() === 'Удалить') {
            deleteButtonColumnExists = true;
            return false;
        }
    });

    let rowData = [player.fio, player.currentRating, player.rankOutput];

    if (deleteButtonColumnExists) {
        // Создаем элемент кнопки
        let deleteButton = document.createElement('button');
        deleteButton.className = 'btn btn-danger m-2 deleteParticipant';
        deleteButton.textContent = 'Удаление участника';
        deleteButton.setAttribute('data-toggle', 'modal');
        deleteButton.setAttribute('tournamentId', player.idTournament);
        deleteButton.setAttribute('playerId', player.idPlayer);

        // Добавляем обработчик события click
        deleteButton.addEventListener('click', e => {
            DeleteParticipant(e.target.getAttribute('playerId'), e.target.getAttribute('tournamentId'));
        });

        // Преобразуем элемент кнопки в строку
        let deleteButtonHtml = deleteButton.outerHTML;
        rowData.push(deleteButtonHtml);
    }

    table.row.add(rowData).draw();
    SetButtonsDeleteParicipant();
}


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
        hubConnection.invoke("AddTournamentParticipant", player);
    }
}

hubConnection.on('AddTournamentParticipant', function (player) {
    let option = document.getElementById(player.idPlayer);
    option.parentNode.removeChild(option);

    addPlayerToTable(player);
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

        DrawTournamentDraw(tournamentDraw, tournamentDraw.tours.length - 1);
        let countTours = document.querySelector('#countTours').value;
        if (countTours == tournamentDraw.tours.length) {
            document.forms["GenerateNextTour"].parentNode.removeChild(document.forms["GenerateNextTour"]);
        }
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
                        <th scope="col"><i class="fas fa-hashtag" style="color: #ffffff; margin-right: 5px;"></i>Номер</th>
                        <th scope="col"><i class="fas fa-chess-pawn" style="color: #ffffff; margin-right: 5px;"></i>Белые</th>
                        <th scope="col"><i class="fas fa-trophy" style="color: #ffffff; margin-right: 5px;"></i>Очки</th>
                        <th scope="col"><i class="fas fa-exchange-alt" style="color: #ffffff; margin-right: 5px;"></i>Результат</th>
                        <th scope="col"><i class="fas fa-trophy" style="color: #ffffff; margin-right: 5px;"></i>Очки</th>
                        <th scope="col"><i class="fas fa-chess-pawn" style="color: #000000; margin-right: 5px;"></i>Черные</th>
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
                                ${tournamentDraw.tours[index].games[jindex].playerWhite.fio + '\n'}
                                ${String(tournamentDraw.tours[index].games[jindex].ratingWhite)}
                             </th>
                             <th scope="row" id="Tour${tournamentDraw.tours[index].idTour}Game${tournamentDraw.tours[index].games[jindex].idGame}PlayerWhiteScore">
                                ${tournamentDraw.tours[index].games[jindex].playerWhiteScore}
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
                                ${tournamentDraw.tours[index].games[jindex].playerBlackScore}
                            </th>
                            <th scope="row" id="Tour${tournamentDraw.tours[index].idTour}Game${tournamentDraw.tours[index].games[jindex].idGame}PlayerBlackFIO">
                                ${tournamentDraw.tours[index].games[jindex].playerBlack.fio + '\n'}
                                ${String(tournamentDraw.tours[index].games[jindex].ratingBlack)}
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
                ${String(tournamentDraw.tours[index].playerSkippingGameScore)}
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
    SetGameResultSelects();
}

async function SetGameResult(idGame, gameResult) {
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
    let tournamentId = document.querySelector('#tournamentId').value;

    if (gameModel != null && gameModel.idTournament == tournamentId) {
        let playerWhiteFIO = document.querySelector(`#Tour${gameModel.idTour}Game${gameModel.idGame}PlayerWhiteFIO`);
        let playerWhiteScore = document.querySelector(`#Tour${gameModel.idTour}Game${gameModel.idGame}PlayerWhiteScore`);
        let playerBlackFIO = document.querySelector(`#Tour${gameModel.idTour}Game${gameModel.idGame}PlayerBlackFIO`);
        let playerBlackScore = document.querySelector(`#Tour${gameModel.idTour}Game${gameModel.idGame}PlayerBlackScore`);
        playerWhiteFIO.innerText = `${gameModel.playerWhite.fio} \n ${gameModel.ratingWhite} ${gameModel.ratingWhiteChange}`;
        playerBlackFIO.innerText = `${gameModel.playerBlack.fio} \n ${gameModel.ratingBlack} ${gameModel.ratingBlackChange}`;
        playerWhiteScore.innerText = `${gameModel.playerWhiteScore}`;
        playerBlackScore.innerText = `${gameModel.playerBlackScore}`;
        UpdateResultTable(gameModel.idTournament);
    }
});

async function UpdateResultTable(idTournament) {
    const response = await fetch("/api/Tournament/GetTournamentResult/", {
        method: "POST",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            IdTournament: parseInt(idTournament),
        })
    });
    if (response.ok === true) {
        const roundRobinTournamentResult = await response.json();
        
        hubConnection.invoke("UpdateResultTable", roundRobinTournamentResult);
    }
    else {
        console.dir(response);
        console.log("Запрос на обновление результата турнира провалился")
    }
}

hubConnection.on('UpdateResultTable', function (roundRobinTournamentResult) {
    if (roundRobinTournamentResult != null) {
        let tournamentResultTable = document.querySelector('#resultOfTournament');

        if (tournamentResultTable == null) {
            let resultDiv = document.querySelector('#results');
            resultDiv.innerHTML = `<table id="resultOfTournament" class="table table-responsive-sm table-bordered table-hover table-dark regular-table">
                    <thead>
                        <tr>
                            <th scope="col">Место</th>
                            <th scope="col">ФИО</th>
                            <th scope="col">Рейтинг</th>
                            <th scope="col">Очки</th>
                            <th scope="col">Личная встреча</th>
                            <th scope="col">Коэффициент Койя</th>
                            <th scope="col">Коэффициент Соннеборна-Бергера</th>
                            <th scope="col">Число выигранных партий</th>
                        </tr>
                    </thead>
                    <tbody id="PlayersParticipatingInTournament">
                    </tbody>
                </table>
            }`
        }

        tournamentResultTable = document.querySelector('#resultOfTournament');
        let tournamentResultTableBodyInnerHTML = "";
        for (let i = 0; i < roundRobinTournamentResult.players.length; i++) {
            tournamentResultTableBodyInnerHTML += `
                <tr>
                    <th scope="row">
                        ${roundRobinTournamentResult.players[i].place.toString()}
                    </th>
                    <th scope="row">
                        ${roundRobinTournamentResult.players[i].fio}
                    </th>
                    <th scope="row">
                          ${roundRobinTournamentResult.players[i].rating.toString()}
                    </th>
                    <th scope="row">
                        ${roundRobinTournamentResult.players[i].points.toString()}
                    </th>
                    <th scope="row">
                        ${roundRobinTournamentResult.players[i].coefficientPersonalMeeting.toString()}
                    </th>
                    <th scope="row">
                        ${roundRobinTournamentResult.players[i].coefficientKoya.toString()}
                    </th>
                    <th scope="row">
                        ${String(roundRobinTournamentResult.players[i].сoefficientSonnebornBerger)}
                    </th>
                    <th scope="row">
                        ${roundRobinTournamentResult.players[i].numberOfWonGames.toString()}
                    </th>
                </tr>`;
        }
        const tbody = tournamentResultTable.getElementsByTagName("tbody")[0];
        if (tbody) {
            tbody.innerHTML = tournamentResultTableBodyInnerHTML;
        }
        
    }
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
        let tournamentType = document.querySelector('#tournamentType').value;
        let table = document.getElementById('usersParticipatingInTournamentTable');
        let countPlayers = table.rows.length - 1;

        if (tournamentType == 0) {
            document.querySelector('#countTours').value = parseInt(countPlayers) % 2 == 0 ? countPlayers - 1 : countPlayers;
        }

        if (tournamentType == 1) {
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