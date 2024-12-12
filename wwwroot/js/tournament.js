document.addEventListener('DOMContentLoaded', function () {
    let gameResultSelects = document.querySelectorAll('.gameResultSelect');
    gameResultSelects.forEach(select => {
        select.addEventListener('change', function (e) {
            let form = e.target.closest('form');
            if (form) {
                form.submit();
            }
        });
    });
});


if (document.forms["GenerateNextTour"] != null) {
    document.forms["GenerateNextTour"].addEventListener("submit", e => {
        e.preventDefault();
        let tournamentType = document.querySelector('#tournamentType').value;
        let table = document.getElementById('usersParticipatingInTournamentTable');
        let countPlayers = table.rows.length - 1;

        if (tournamentType == 0) {
            document.querySelector('#countTours').value = countPlayers % 2 == 0 ? countPlayers - 1 : countPlayers;
        }

        if (tournamentType == 1) {
            let countTours = document.getElementById('countTours').value;
            let minimumNumberOfParticipants = 2 + parseInt(countTours, 10);

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

        if (!AreAllTheResultsEntered()) {
            const alertContainer = document.getElementById('alertWhenNotAllResultsEnteredContainer');
            const alertHtml = `
            <div class="alert alert-warning alert-dismissible fade show" role="alert">
                <strong>Заполните результаты текущего тура, прежде чем генерировать следующий тур</strong>
                <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
            </div>`;

            alertContainer.innerHTML = alertHtml;
        } else {
            document.forms["GenerateNextTour"].submit();
        }
    });
}

function AreAllTheResultsEntered() {
    let gameResultSelects = document.querySelectorAll('.gameResultSelect');

    for (let select of gameResultSelects) {
        if (select.value == "Заполнить") {
            return false;
        }
    }

    return true;
}
