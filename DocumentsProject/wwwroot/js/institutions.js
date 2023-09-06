
$(document).ready(function () {
    $("#institutionsDatatable").DataTable({
        "ajax": {
            "url": "/Admin/GetInstitutionList",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id", title: "Id", name: "id", visible: false },
            { "data": "instCode", "title": "Institution Code", "name": "instCode", "autoWidth": true },
            { "data": "name", "title": "Name", "name": "name", "autoWidth": true },
            { "data": "additionalInfo", "title": "Information", "name": "additionalInfo", "autoWidth": true },
        ]

    });
});

function createInstitution() {
    var form = $("#createInstitutionForm");
    var url = form.attr('action');
    $.ajax({
        url: url,
        data: form.serialize(),
        method: "POST",
        success: function (response) {
            $('#institutionsDatatable').DataTable().ajax.reload();
            $('#createInstitutionModal').modal('toggle');
        },
        error: function (response) {
            $('#createInstitutionFormContent').html(null);
            $('#createInstitutionFormContent').html(response);
            $("form").removeData("validator");
            $("form").removeData("unobtrusiveValidation");
            $.validator.unobtrusive.parse("form");
        }
    });
}


function loadCreateInstitutionForm() {
    $.ajax({
        url: "/Admin/CreateInstitution",
        method: "GET",
        success: function (response) {
            $('#createInstitutionFormContent').html(null);
            $('#createInstitutionFormContent').html(response);
        }
    });
}