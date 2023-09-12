
$(document).ready(function () {
    $("#institutionsDatatable").DataTable({
        serverSide: true,
        "ajax": {
            "url": "/Admin/GetInstitutionList",
            "type": "POST",
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
        serverSide: true,
        "ajax": {
            "url": "/Admin/GetUsers",
            "type": "POST",
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

function createUser() {
    var form = $("#createUserForm");
    if (form.valid()) {
        $.ajax({
            url: "/Admin/CreateUser",           
            data: form.serialize(),
            method: "POST",
            success: function (response) {
                $('#usersDatatable').DataTable().columns.adjust();
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

function loadCreateUserForm() {
    $.ajax({
        url: "/Admin/GetCreateUser",
        method: "GET",
        success: function (response) {
            $('#createUserFormContent').html(null);
            $('#createUserFormContent').html(response);
        }
    });
}

function renderInstitutionSelect(value) {
    if (value == 'BankOperator') {
        document.getElementById("institutionSelect").hidden = false;
    }
    else {
        document.getElementById("institutionSelect").hidden = true;
    }
}