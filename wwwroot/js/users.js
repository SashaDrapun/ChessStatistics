const buttonsDeleteUser = document.querySelectorAll('.deleteUser');
console.log(123);

for (let i = 0; i < buttonsDeleteUser.length; i++) {
    buttonsDeleteUser[i].addEventListener('click', e => {
        document.querySelector('#deleteUserForm').action = "/Users/Delete?idUser=" + e.target.getAttribute('idUser');
    });
}

const buttonsSetAdmin = document.querySelectorAll('.setAdmin');

for (let i = 0; i < buttonsSetAdmin.length; i++) {
    buttonsSetAdmin[i].addEventListener('click', e => {
        console.log(e.target.getAttribute('idUser'));
        document.querySelector('#setAdminForm').action = "/Users/AppointAdministrator?idUser=" + e.target.getAttribute('idUser');
    });
}

