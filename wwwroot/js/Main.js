
$(document).ready(function () {
    $('table').DataTable({
        "language": {
            "lengthMenu": document.querySelector('#LengthMenu').textContent,
            "zeroRecords": document.querySelector('#ZeroRecords').textContent,
            "info": document.querySelector('#Info').textContent,
            "infoEmpty": document.querySelector('#InfoEmpty').textContent,
            "infoFiltered": document.querySelector('#InfoFiltered').textContent,
            "search": document.querySelector('#Search').textContent
        }
    });
});

