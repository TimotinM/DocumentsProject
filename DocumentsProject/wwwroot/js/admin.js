
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
            },
            error: function (response) {
                var errorDiv = $("#createUserErrors");
                errorDiv.empty();
                $.each(response.responseJSON, function (key, value) {
                    errorDiv.append("<p>" + value + "</p>");
                });
                document.getElementById("createUserErrors").hidden = false;
            }
        });
    }
}

function updateUser() {
    var form = $("#updateUserForm");
    var url = form.attr('action');
    if (form.valid()) {
        $.ajax({
            url: url,
            xhrFields: {
                withCredentials: true
            },
            data: form.serialize(),
            method: "POST",
            success: function (response) {
                $('#modalContainer').modal('toggle');
                $('#usersDatatable').DataTable().ajax.reload();
            },
            error: function (response) {
                var errorDiv = $("#editUserErrors");
                errorDiv.empty();
                $.each(response.responseJSON, function (key, value) {
                    errorDiv.append("<p>" + value + "</p>");
                });
                document.getElementById("editUserErrors").hidden = false;
            }
        });
    }
}

function updateInstitution() {
    var form = $("#updateInstitutionForm");
    var url = form.attr('action');
    if (form.valid()) {
        $.ajax({
            url: url,
            xhrFields: {
                withCredentials: true
            },
            data: form.serialize(),
            method: "POST",
            success: function (response) {
                $('#modalContainer').modal('toggle');
                $('#institutionsDatatable').DataTable().ajax.reload();
            },
            error: function (response) {
                var errorDiv = $("#updateInstitutionErrors");
                errorDiv.empty();
                $.each(response.responseJSON, function (key, value) {
                    errorDiv.append("<p>" + value + "</p>");
                });
                document.getElementById("updateInstitutionErrors").hidden = false;
            }
        });
    }
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

function loadUpdateInstitutionForm(id) {
    $.ajax({
        url: "/Admin/GetUpdateInstitution",
        method: "GET",
        data: {
            institutionId: id
        },
        success: function (response) {
            $('#modalTitle').html("Update Institution")
            $('#modalBody').html(null);
            $('#modalBody').html(response);
            $("form").removeData("validator");
            $("form").removeData("unobtrusiveValidation");
            $.validator.unobtrusive.parse("form");
            $('#modalContainer').modal('toggle');
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
            $('#roleSelect').multiselect({
                templates: {
                    button: '<button type="button" class="multiselect dropdown-toggle btn btn-light" data-bs-toggle="dropdown" aria-expanded="false"><span class="multiselect-selected-text"></span></button>',
                },
            });
            $("form").removeData("validator");
            $("form").removeData("unobtrusiveValidation");
            $.validator.unobtrusive.parse("form");
        }
    });
}

function loadEditUserForm(userId) {
    $.ajax({
        url: "/Admin/GetUserEdit",
        method: "GET",
        data: {
            id: userId
        },
        success: function (response) {
            $('#modalTitle').html(null)
            $('#modalTitle').html("Edit User")
            $('#modalBody').html(null);
            $('#modalBody').html(response);
            $('#roleSelect').multiselect({
                templates: {
                    button: '<button type="button" class="multiselect dropdown-toggle btn btn-light" data-bs-toggle="dropdown" aria-expanded="false"><span class="multiselect-selected-text"></span></button>',
                },
            });
            renderInstitutionSelect(document.getElementById("roleSelect").value);
            $("form").removeData("validator");
            $("form").removeData("unobtrusiveValidation");
            $.validator.unobtrusive.parse("form");
            $('#modalContainer').modal('toggle');
        }
            
    });
}

function loadUserDetails(userId) {
    $.ajax({
        url: "/Admin/GetUserDetails",
        method: "GET",
        data: {
            id: userId
        },
        success: function (response) {
            $('#modalTitle').html(null)
            $('#modalTitle').html("User Details")
            $('#modalBody').html(null);
            $('#modalBody').html(response);          
            $('#modalContainer').modal('toggle');
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
                case 'edit':
                    loadUpdateInstitutionForm(row.data()["id"])
                    break;
                default:
                    break
            }
        },
        items: {
            "edit": { name: "Edit" },
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
                case 'edit':
                    loadEditUserForm(row.data()["id"]);
                    break;
                case 'changePassword':
                    loadChangeUserPasswordForm(row.data()["id"]);
                    break;
                case 'details':
                    loadUserDetails(row.data()["id"]);
                    break;
                default:
                    break
            }
        },
        items: {           
            "changePassword": { name: "Change Password" },
            "edit": { name: "Edit" },
            "details": { name: "Details" }
        }
    });
}

function renderInstitutionSelect(value) {

    var selected = [];
    for (var option of document.getElementById('roleSelect').options) {
        if (option.selected) {
            selected.push(option.value);
        }
    }
    if (selected.includes('BankOperator')) {
        document.getElementById("institutionSelect").hidden = false;
    }
    else {
        document.getElementById("institutionValue")[0].selected = true;
        document.getElementById("institutionSelect").hidden = true;       
    }
}