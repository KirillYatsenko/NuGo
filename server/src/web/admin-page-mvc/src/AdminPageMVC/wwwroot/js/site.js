// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    $('#eventsTable').DataTable({
        columnDefs: [{
            orderable: false,
            targets: [4, 6]
        }],
        "order": [
            [2, "asc"]
        ],
    });

    $('.dataTables_length').addClass('bs-select');
});