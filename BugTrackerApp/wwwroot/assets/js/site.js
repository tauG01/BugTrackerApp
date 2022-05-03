// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
// Write your JavaScript code.

$(document).ready(function () {
    $('#userTable').DataTable({
        "fnDrawCallback": function (oSettings) {
            
        },
        "scrollY": "450px",
        "scrollCollapse": true,
        "paging": true,
        "pageLength": 5,
        "lengthMenu": [[5, 10, 20, -1], [5, 10, 20, 'Todos']]
    });
});

//ShowInPopup = (url, title) => {
//    $.ajax({
//        type: "GET",
//        url: url,
//        success: function (res) {
//            $("#form-modal .modal-body").html(res);
//            $("#form-modal .modal-title").html(title);
//            $("#form-modal").modal('show');
//        }
//    })
//}

//$(document).ready(function () {
//    $('#example').dataTable({
//        "fnDrawCallback": function (oSettings) {
//            alert('DataTables has redrawn the table');
//        }
//    });
//});