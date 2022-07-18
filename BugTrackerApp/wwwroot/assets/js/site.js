// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
// Write your JavaScript code.

//datatables
$(document).ready(function () {
    $('#userTable').DataTable({
        "scrollY": "450px",
        "scrollCollapse": true,
        "paging": true,
        "pageLength": 5,
        "lengthMenu": [[5, 10, 20, -1], [5, 10, 20, 'Todos']]
    });
});

//Handling modal popup
showInPopup = (url, title) => {
    $.ajax({
        type: "GET",
        url: url,
        success: function (res) {
            $("#form-modal .modal-body").html(res);
            $("#form-modal .modal-title").html(title);
            $("#form-modal").modal('show');
        }
    })
}

//Handling form post on submit
jQueryAjaxPost = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isValid) {
                    $('#view-all').html(res.html)
                    $('#form-modal .modal-body').html('');
                    $('#form-modal .modal-title').html('');
                    $('#form-modal').modal('hide');

                    //reinitialise the datatable
                    if ($.fn.DataTable.isDataTable("#userTable")) {
                        $('#userTable').DataTable().clear().destroy();
                    }

                    $("#userTable").dataTable({
                        "scrollY": "450px",
                        "scrollCollapse": true,
                        "paging": true,
                        "pageLength": 5,
                        "lengthMenu": [[5, 10, 20, -1], [5, 10, 20, 'Todos']]
                    });

                    toastr.options = {
                        "positionClass": "toast-top-center"
                    }
                    toastr.success("Successfully added");
                }
                else
                    $('#form-modal .modal-body').html(res.html);
            },
            error: function (err) {
                console.log(err)
            }
        })
        //to prevent default form submit event
        return false;
    } catch (ex) {
        console.log(ex)
    }
}

jQueryAjaxDelete = form => {
    if (confirm('Are you sure to delete this record ?')) {
        try {
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {
                    $('#view-all').html(res.html);
                    //reinitialise the datatables
                    if ($.fn.DataTable.isDataTable("#userTable")) {
                        $('#userTable').DataTable().clear().destroy();
                    }
                    $("#userTable").dataTable({
                        "scrollY": "450px",
                        "scrollCollapse": true,
                        "paging": true,
                        "pageLength": 5,
                        "lengthMenu": [[5, 10, 20, -1], [5, 10, 20, 'Todos']]
                    });
                    toastr.success("Successfully deleted");
                },
                error: function (err) {
                    console.log(err)
                }
            })
        } catch (ex) {
            console.log(ex)
        }
    }
    //prevent default form submit event
    return false;
}

//$(document).ready(function () {
//    $('#example').dataTable({
//        "fnDrawCallback": function (oSettings) {
//            alert('DataTables has redrawn the table');
//        }
//    });
//});