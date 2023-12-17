const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/item")
    .build();

const containerCountLikes = document.querySelector('#countLikes');
let countLikes = document.querySelector('#countLikes').textContent;
let countComments = document.querySelector('#countComments').textContent;

async function CreateLike(nickname, idItem) {
    const response = await fetch("/api/like/", {
        method: "POST",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            NicknameUser: nickname,
            IdItem: parseInt(idItem)
        })
    });
    if (response.ok === true) {
        containerCountLikes.textContent = ++countLikes;
        document.querySelector('.like').src = '/img/LikePressed.png';
        document.querySelector('#isUserLike').textContent = 'True';
        const itemId = parseInt(document.querySelector('#idItem').textContent);
        hubConnection.invoke("SendLike", itemId, countLikes);
    }
}

async function AddComment(nickname, idItem, text) {
    const response = await fetch("/api/comment/", {
        method: "POST",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            NicknameUser: nickname,
            IdItem: parseInt(idItem),
            Text: text
        })
    });
    if (response.ok === true) {
        const comment = await response.json();
        hubConnection.invoke("Send", comment.text, comment.user.nickname, comment.itemId, comment.dateTime);
    }
}

async function DeleteLike() {
    const itemId = parseInt(document.querySelector('#idItem').textContent);
    const response = await fetch("/api/like/" + itemId, {
        method: "DELETE",
        headers: { "Accept": "application/json" }
    });
    if (response.ok) {
        containerCountLikes.textContent = --countLikes;
        document.querySelector('.like').src = '/img/Like.png';
        document.querySelector('#isUserLike').textContent = 'False';
        hubConnection.invoke("SendLike", itemId, countLikes);
    }
}

document.querySelector('.like').addEventListener('click', () => {
    const userNicknameContainer = document.querySelector('#userNickname');
    const nicknameAutorizeUser = userNicknameContainer == null ? undefined : userNicknameContainer.textContent;
    if (nicknameAutorizeUser != undefined) {
        let isUserLike = document.querySelector('#isUserLike').textContent === 'False' ? false : true;
        if (isUserLike) {
            DeleteLike();
        }
        else {
            const idItem = document.querySelector('#idItem').textContent;
            CreateLike(nicknameAutorizeUser, idItem);
        }
    }
    else {
        $('#notFullAccess').modal('show');
    }
});

document.forms["commentForm"].addEventListener("submit", e => {
    e.preventDefault();
    const userNicknameContainer = document.querySelector('#userNickname');
    const nicknameAutorizeUser = userNicknameContainer == null ? undefined : userNicknameContainer.textContent;
    if (nicknameAutorizeUser != undefined) {
        const form = document.forms["commentForm"];
        const text = form.elements["text"].value;
        const idItem = document.querySelector('#idItem').textContent;
        AddComment(nicknameAutorizeUser, idItem, text);
        document.querySelector('#comment').textContent = '';
    }
    else {
        $('#notFullAccess').modal('show');
    }
});



hubConnection.on('Send', function (message, userNickname, idItem, dateTime) {
    if (idItem == document.querySelector('#idItem').textContent) {
        document.querySelector('#countComments').textContent = ++countComments;
        const comment = document.createElement('div');
        comment.innerHTML = `<div class="modal-dialog" role="document">
        <div class="modal-content text-white bg-dark">
            <div class="modal-header">
                <h5 class="modal-title">${userNickname}</h5>
                <p>${dateTime}</p>
            </div>
            <div class="modal-body">
                <div class="container-fluid">
                    <span>${message}</span>
                </div>
            </div>
        </div>
    </div>`;
        document.querySelector('#formCommentModal').appendChild(comment);
    }
});


hubConnection.on('SendLike', function (idItem, countLikesNew) {
    if (idItem == document.querySelector('#idItem').textContent) {
        countLikes = countLikesNew;
        document.querySelector('#countLikes').textContent = countLikes;
    }
});

hubConnection.start();