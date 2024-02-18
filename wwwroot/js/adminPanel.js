const buttonsConfirmLink = document.querySelectorAll('.confirmLink');

for (let i = 0; i < buttonsConfirmLink.length; i++) {
    buttonsConfirmLink[i].addEventListener('click', e => {
        document.querySelector('#IdLink').value = e.target.getAttribute('idLink');
        document.querySelector('#FIOUser').value = e.target.getAttribute('UserFIO');
        document.querySelector('#emailUser').value = e.target.getAttribute('UserEmail');
        document.querySelector('#FIOPlayer').value = e.target.getAttribute('PlayerFIO');
    });
}


