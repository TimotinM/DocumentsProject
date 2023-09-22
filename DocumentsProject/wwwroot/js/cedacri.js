function loadCreateDocumentForm() {
    $.ajax({
        url: "/Cedacri/GetCreateDocument",
        method: "GET",
        async: false,
        success: function (response) {
            $('#modalTitle').html("New Document")
            $('#modalBody').html(null);
            $('#modalBody').html(response);
            $("form").removeData("validator");
            $("form").removeData("unobtrusiveValidation");
            $.validator.unobtrusive.parse("form");
        }
    });
}

function loadCreateProjectForm() {
    $.ajax({
        url: "/Cedacri/GetCreateProject",
        method: "GET",
        success: function (response) {
            $('#modalTitle').html("New Project")
            $('#modalBody').html(null);
            $('#modalBody').html(response);
            $("form").removeData("validator");
            $("form").removeData("unobtrusiveValidation");
            $.validator.unobtrusive.parse("form");
        }
    });
}
function loadUpdateProjectForm(id) {
    $.ajax({
        url: "/Cedacri/GetUpdateProject",
        method: "GET",
        data: {
            projectId: id
        },
        success: function (response) {
            $('#modalTitle').html("Update Project")
            $('#modalBody').html(null);
            $('#modalBody').html(response);
            $("form").removeData("validator");
            $("form").removeData("unobtrusiveValidation");
            $.validator.unobtrusive.parse("form");
            configureDatePicker();
            $('#modalContainer').modal('toggle');
        }
    });
}
function loadProjectDetails(id) {
    $.ajax({
        url: "/Cedacri/GetProjectDetails",
        method: "GET",
        data: {
            projectId: id
        },
        success: function (response) {
            $('#modalTitle').html(null)
            $('#modalTitle').html("Project Information")
            $('#modalBody').html(null);
            $('#modalBody').html(response);
            $('#modalContainer').modal('toggle');
        }
    });
}

function loadDocumentsTable() {
    let table = $("#documentsDatatable").DataTable({
        serverSide: true,
        "ajax": {
            "url": "/Cedacri/GetDocumentsTable",
            "type": "POST",
            "datatype": "json"
        },
        "dateFormat": "yy-mm-dd",
        "columns": [
            { "data": "id", title: "Id", name: "id", visible: false },
            { "data": "name", "title": "Name", "name": "name", "autoWidth": true },
            { "data": "documentType", "title": "Type", "name": "documentType", "autoWidth": true },
            { "data": "formattedGroupingDate", "title": "Date", "name": "groupingDate", "autoWidth": true },
            { "data": "institution", "title": "Institution", "name": "institution", "autoWidth": true },
            { "data": "project", "title": "Project", "name": "project", "autoWidth": true },
        ]

    });
    let contextmenu = $('#documentsDatatable').contextMenu({
        selector: 'tr',
        trigger: 'right',
        callback: function (key, options) {
            let row = table.row(options.$trigger);
            switch (key) {
                case 'details':
                    loadDocumentDetails(row.data()["id"]);
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

function loadProjectsTable() {
    let table = $("#projectsDatatable").DataTable({
        serverSide: true,
        "ajax": {
            "url": "/Cedacri/GetProjectsTable",
            "type": "POST",
            "datatype": "json"
        },
        "dateFormat": "yy-mm-dd",
        "columns": [
            { "data": "id", title: "Id", name: "id", visible: false },
            { "data": "name", "title": "Name", "name": "name", "autoWidth": true },
            { "data": "formattedDateFrom", "title": "Date From", "name": "dateFrom", "autoWidth": true },
            { "data": "formattedDateTill", "title": "Date Till", "name": "DateTill", "autoWidth": true },
            { "data": "institution", "title": "Institution", "name": "institution", "autoWidth": true },
            { "data": "userName", "title": "User", "name": "applicationUser", "autoWidth": true },
            {
                "data": "isActive",
                "title": "Is Active",
                "name": "isActive",
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
            { className: 'text-center', targets: [6] },
        ],

    });
    let contextmenu = $('#projectsDatatable').contextMenu({
        selector: 'tr',
        trigger: 'right',
        callback: function (key, options) {
            let row = table.row(options.$trigger);
            switch (key) {
                case 'details':
                    loadProjectDetails(row.data()["id"]);
                    break;
                case 'edit':
                    loadUpdateProjectForm(row.data()["id"])
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

function createDocument() {
    var form = $("#createDocumentForm")[0];
    var formData = new FormData(form);
    if ($("#createDocumentForm").valid()) {
        $.ajax({
            url: "/Cedacri/CreateDocument",
            data: formData,
            method: "POST",
            processData: false,
            contentType: false,
            success: function (response) {
                $('#modalContainer').modal('toggle');
                $('#documentsDatatable').DataTable().ajax.reload();
                refreshDocumentsTree();
            },
            error: function (response) {
                var errorDiv = $("#createDocumentErrors");
                errorDiv.empty();
                $.each(response.responseJSON, function (key, value) {
                    errorDiv.append("<p>" + value + "</p>");
                });
                document.getElementById("createDocumentErrors").hidden = false;
            }
        });
    }
}

function createProject() {
    var form = $("#createProjectForm");
    if (form.valid()) {
        $.ajax({
            url: "/Cedacri/CreateProject",
            data: form.serialize(),
            method: "POST",
            success: function (response) {
                $('#modalContainer').modal('toggle');
                $('#projectsDatatable').DataTable().ajax.reload();
                refreshProjetsTree();
            },
            error: function (response) {
                var errorDiv = $("#createProjectErrors");
                errorDiv.empty();
                $.each(response.responseJSON, function (key, value) {
                    errorDiv.append("<p>" + value + "</p>");
                });
                document.getElementById("createProjectErrors").hidden = false;
            }
        });
    }
}
function updateProject() {
    var form = $("#updateProjectForm");
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
                $('#projectsDatatable').DataTable().ajax.reload();
                refreshProjetsTree();
            },
            error: function (response) {
                var errorDiv = $("#updateProjectErrors");
                errorDiv.empty();
                $.each(response.responseJSON, function (key, value) {
                    errorDiv.append("<p>" + value + "</p>");
                });
                document.getElementById("updateProjectErrors").hidden = false;
            }
        });
    }
}

function populateMicroSelect(id) {
    $.ajax({
        url: "/Cedacri/GetDocumentsTypeMicro",
        type: "GET",
        data: {
            macroId: id
        },
        success: function (data) {
            $("#microValue").empty();
            $("#microValue").append(
                $("<option disabled selected hidden>").val("").text("--Select MICRO--")
            );
            $.each(data, function (i, item) {
                $("#microValue").append(
                    $("<option>").val(item.id).text(item.name)
                );
            });
        }
    });
}
function populateProjectSelect(id) {
    $.ajax({
        url: "/Cedacri/GetInstitutionProjects",
        type: "GET",
        data: {
            institutionId: id
        },
        success: function (data) {
            $("#projectValue").empty();
            $("#projectValue").append(
                $("<option disabled selected hidden>").val("").text("--Select Project--")
            );
            $.each(data, function (i, item) {
                $("#projectValue").append(
                    $("<option>").val(item.id).text(item.name)
                );
            });
            document.getElementById("projectValue").disabled = false;
        }
    });
}

function macroChange(element) {
    populateMicroSelect(element.value);
    renderProjectSelect(element);
    renderMicroSelect(element);
}

function renderProjectSelect(element) {
    var text = element.options[element.selectedIndex].text;

    if (text == "Project") {
        document.getElementById("projectSelect").hidden = false;
    }
    else {
        document.getElementById("projectValue")[0].selected = true;
        document.getElementById("projectSelect").hidden = true;
    }
}

function configureDatePicker() {
    var dateFromInput = document.getElementById("dateFrom");
    var dateTillInput = document.getElementById("dateTill");
    var dateFromValue = new Date(dateFromInput.value);
    var nextDay = new Date(dateFromValue);
    nextDay.setDate(nextDay.getDate() + 1);
    dateTillInput.setAttribute("min", nextDay.toISOString().split("T")[0]);
    dateTillInput.disabled = false;

}

function renderMicroSelect(element) {
    var text = element.options[element.selectedIndex].text;

    if (text == "SLA") {
        document.getElementById("microValue")[0].selected = true;
        document.getElementById("microSelect").hidden = true;
    }
    else {
        document.getElementById("microSelect").hidden = false;
    }
}

function generateDocumentsTree() {

    $.ajax({
        url: "/Cedacri/GetDocumentsTree",
        method: "GET",
        success: function (data) {
            $('#doctree').jstree({
                'core': {
                    'data': data
                }
            });

            $('#doctree').on('changed.jstree', function (e, data) {
                var table = $('#documentsDatatable').DataTable();
                var selectedNode = data.instance.get_node(data.selected[0]);
                table.columns().search('');
                table.column(selectedNode.original.column).search(selectedNode.original.text)
                var currentNode = selectedNode;
                while (currentNode.parent !== '#') {
                    var parentNode = data.instance.get_node(currentNode.parent);
                    table.column(parentNode.original.column).search(parentNode.original.text)
                    currentNode = parentNode;
                }
                table.draw();
            });
        }
    });  
}
function generateProjectsTree() {
    $.ajax({
        url: "/Cedacri/GetProjectsTree",
        method: "GET",
        success: function (data) {
            $('#projtree').jstree({
                'core': {
                    'data': data
                }
            });

            $('#projtree').on('changed.jstree', function (e, data) {
                var table = $('#projectsDatatable').DataTable();
                var selectedNode = data.instance.get_node(data.selected[0]);
                table.columns().search('');
                table.column(selectedNode.original.column).search(selectedNode.original.text)
                var currentNode = selectedNode;
                while (currentNode.parent !== '#') {
                    var parentNode = data.instance.get_node(currentNode.parent);
                    table.column(parentNode.original.column).search(parentNode.original.text)
                    currentNode = parentNode;
                }
                table.draw();
            });
        }
    });
}
function refreshDocumentsTree() {
    $.ajax({
        url: "/Cedacri/GetDocumentsTree",
        method: "GET",
        success: function (data) {
            $('#doctree').jstree(true).settings.core.data = data;
            $('#doctree').jstree(true).refresh();
        }
    });
}
function refreshProjetsTree() {
    $.ajax({
        url: "/Cedacri/GetProjectsTree",
        method: "GET",
        success: function (data) {
            $('#projtree').jstree(true).settings.core.data = data;
            $('#projtree').jstree(true).refresh();
        }
    });
}

