const buttonsDeleteUser = document.querySelectorAll('.deleteUser');

for (let i = 0; i < buttonsDeleteUser.length; i++) {
    buttonsDeleteUser[i].addEventListener('click', e => {
        document.querySelector('#deleteUserForm').action = "/Users/Delete?idUser=" + e.target.getAttribute('idUser');
    });
}

const buttonsSetAdmin = document.querySelectorAll('.setAdmin');

for (let i = 0; i < buttonsSetAdmin.length; i++) {
    buttonsSetAdmin[i].addEventListener('click', e => {
        document.querySelector('#setAdminForm').action = "/Users/AppointAdministrator?idUser=" + e.target.getAttribute('idUser');
    });
}
