﻿
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

    $("#usersDatatable").DataTable({
        "ajax": {
            "url": "/Admin/GetUsers",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id", title: "Id", name: "id", visible: false },
            { "data": "userName", "title": "User Name", "name": "userName", "autoWidth": true },
            { "data": "nameSurname", "title": "Name Surname", "name": "nameSurname", "autoWidth": true },
            { "data": "email", "title": "Email", "name": "email", "autoWidth": true },
            {
                "data": "isEnabled",
                "title": "Enabled",
                "name": "isEnabled",
                "autoWidth": true,
                "render": function (data, type, full, meta) {
                    if (type === 'display') {
                        return '<input type="checkbox" ' + (data ? 'checked' : '') + '>';
                    }
                    return data;
                }
            }
        ]
    });

});

function createInstitution() {
    var form = $("#createInstitutionForm");
    if (form.valid()) {
        $.ajax({
            url: "/Admin/CreateInstitution",
            data: form.serialize(),
            method: "POST",
            success: function(response) {
                $('#institutionsDatatable').DataTable().columns.adjust();
            }
        });
    }
    
}


function loadCreateInstitutionForm() {
    $.ajax({
        url: "/Admin/GetCreateInstitution",
        method: "GET",
        success: function (response) {
            $('#createInstitutionFormContent').html(null);
            $('#createInstitutionFormContent').html(response);
        }
    });
}