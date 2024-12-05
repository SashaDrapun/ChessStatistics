document.addEventListener('DOMContentLoaded', () => {
    const buttonsDeleteClub = document.querySelectorAll('.deleteClub');
    buttonsDeleteClub.forEach(button => {
        button.addEventListener('click', handleDeleteButtonClick);
    });

    const buttonsEditClub = document.querySelectorAll('.editClub');
    buttonsEditClub.forEach(button => {
        button.addEventListener('click', handleEditButtonClick);
    });
});

function handleDeleteButtonClick(e) {
    const clubName = e.target.getAttribute('clubName');
    const clubId = e.target.getAttribute('clubId');
    document.querySelector('#beforeDeleteClubText').textContent = `Вы уверены, что хотите удалить клуб с названием: ${clubName}?`;
    document.querySelector('#deleteClubForm').action = `/Clubs/Delete?id=${clubId}`;
}

function handleEditButtonClick(e) {
    e.preventDefault();
    const button = e.target;
    const clubData = getClubDataFromButton(button);
    populateEditForm(clubData);
}

function getClubDataFromButton(button) {
    return {
        clubId: button.getAttribute('clubId'),
        clubName: button.getAttribute('clubName'),
        clubDescription: button.getAttribute('clubDescription')
    };
}

function populateEditForm(clubData) {
    document.querySelector('#IdClubEdit').value = clubData.clubId;
    document.querySelector('#ClubNameEdit').value = clubData.clubName;
    document.querySelector('#ClubDescriptionEdit').value = clubData.clubDescription;
}
