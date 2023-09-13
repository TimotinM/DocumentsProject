
$(document).ready(function () {
    loadInstitutionsTable();
    loadUsersTable();

});

function createInstitution() {
    var form = $("#createInstitutionForm");
    if (form.valid()) {
        $.ajax({
            url: "/Admin/CreateInstitution",
            data: form.serialize(),
            method: "POST",
            success: function (response) {
               $('#modalContainer').modal('toggle');
               $('#institutionsDatatable').DataTable().ajax.reload();
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
                $('#modalContainer').modal('toggle');
                $("#createUserForm")[0].reset();
                $('#usersDatatable').DataTable().ajax.reload();
            }
        });
    }
}

function changeUserEnable(userId) {
    $.ajax({
        url: "/Admin/UpdateUserEnabled",
        data: {
            id: userId
        },
        method: "POST",
        success: function (response) {
            $('#usersDatatable').DataTable().ajax.reload();
        }
    });
}

function changeUserPassword() {
    var form = $("#changeUserPasswordForm");
    if (form.valid()) {
        $.ajax({
            url: form.attr('action'),
            data: form.serialize(),
            method: "POST",
            success: function (response) {
                $('#modalContainer').modal('toggle');
                $("#changeUserPasswordForm")[0].reset();
            },
            error: function (response) {
                console.log(response.responseText);
                alert(response.responseText);
            }
        });
    }
}


function loadCreateInstitutionForm() {
    $.ajax({
        url: "/Admin/GetCreateInstitution",
        method: "GET",
        async: false,
        success: function (response) {
            $('#modalTitle').html("New Institution")
            $('#modalBody').html(null);
            $('#modalBody').html(response);
            $("form").removeData("validator");
            $("form").removeData("unobtrusiveValidation");
            $.validator.unobtrusive.parse("form");
        }
    });
}

function loadCreateUserForm() {
    $.ajax({
        url: "/Admin/GetCreateUser",
        method: "GET",
        async: false,
        success: function (response) {
            $('#modalTitle').html(null)
            $('#modalTitle').html("New User")
            $('#modalBody').html(null);
            $('#modalBody').html(response);
            $("form").removeData("validator");
            $("form").removeData("unobtrusiveValidation");
            $.validator.unobtrusive.parse("form");
        }
    });
}

function loadChangeUserPasswordForm(userId) {
    $.ajax({
        url: "/Admin/GetChangeUserPassword",
        method: "GET",
        data: {
            id: userId
        },
        async: false,
        success: function (response) {
            $('#modalTitle').html(null)
            $('#modalTitle').html("Change Password")
            $('#modalBody').html(null);
            $('#modalBody').html(response);
            $("form").removeData("validator");
            $("form").removeData("unobtrusiveValidation");
            $.validator.unobtrusive.parse("form");
            $('#modalContainer').modal('toggle');
        }
    });
}

function loadInstitutionsTable() {
    let table = $("#institutionsDatatable").DataTable({
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
    let contextmenu = $('#institutionsDatatable').contextMenu({
        selector: 'tr',
        trigger: 'right',
        callback: function (key, options) {
            let row = table.row(options.$trigger);
            switch (key) {
                case 'details':
                    break;
                case 'edit':
                    break;
                default:
                    break
            }
        },
        items: {
            "edit": { name: "Edit" },
            "details": { name: "Details" }
        }
    });
}

function loadUsersTable() {
    let table = $("#usersDatatable").DataTable({
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
                        return '<input class="form-check-input" type="checkbox" disabled ' + (data ? 'checked' : '') + '>';
                    }
                    return data;
                }
            }
        ],
        columnDefs: [
            { className: 'text-center', targets: [4] },
        ],
    });
    let contextmenu = $('#usersDatatable').contextMenu({
        selector: 'tr',
        trigger: 'right',
        callback: function (key, options) {
            let row = table.row(options.$trigger);
            switch (key) {
                case 'enabled':
                    changeUserEnable(row.data()["id"]);
                    break;
                case 'changePassword':
                    loadChangeUserPasswordForm(row.data()["id"]);
                    break;
                default:
                    break
            }
        },
        items: {           
            "changePassword": { name: "Change Password" },
            "enabled": { name: "Enable/Disable" },
        }
    });
}

function renderInstitutionSelect(value) {
    if (value == 'BankOperator') {
        document.getElementById("institutionSelect").hidden = false;
    }
    else {
        document.getElementById("institutionSelect").hidden = true;
        document.getElementById("institutionValue").value = null;
    }
}