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

function loadDocumentsTable() {
    let table = $("#documentsDatatable").DataTable({
        serverSide: true,
        "ajax": {
            "url": "/Cedacri/GetDocumentsTable",
            "type": "POST",
            "datatype": "json"
        },
        "columns": [
            { "data": "id", title: "Id", name: "id", visible: false },
            { "data": "name", "title": "Name", "name": "name", "autoWidth": true },
            { "data": "type", "title": "Type", "name": "type", "autoWidth": true },
            { "data": "date", "title": "Date", "name": "date", "autoWidth": true },
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

function createDocument() {
    var form = $("#createDocumentForm")[0];
    var formData = new FormData(form);

    $.ajax({
        url: "/Cedacri/CreateDocument",
        data: formData,
        method: "POST",
        processData: false,
        contentType: false,
        success: function (response) {
            $('#modalContainer').modal('toggle');
            $('#documentsDatatable').DataTable().ajax.reload();
        },
        error: function (response) {
            console.log(response);
        }
    });
}

function createProject() {
    var form = $("#createProjectForm");
    $.ajax({
        url: "/Cedacri/CreateProject",
        data: form.serialize(),
        method: "POST",
        success: function (response) {
            $('#modalContainer').modal('toggle');
        },
        error: function (response) {
            console.log(response);
        }
    });
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
                $("<option>").val("").text("--Select MICRO--")
            );
            $.each(data, function (i, item) {
                $("#microValue").append(
                    $("<option>").val(item.id).text(item.name)
                );
            });
        }
    });
}

function macroChange(element) {
    populateMicroSelect(element.value);
    renderProjectSelect(element)
}

function renderProjectSelect(element) {
    var text = element.options[element.selectedIndex].text;

    if (text == "SLA") {
        document.getElementById("projectSelect").hidden = false;
    }
    else {
        console.log(text);
        document.getElementById("projectValue")[0].selected = true;
        document.getElementById("projectSelect").hidden = true;
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
                var selectedNode = data.instance.get_node(data.selected[0]);
                var selectedNodeData = selectedNode.data; // Obțineți datele nodului selectat

                // Colectați datele de la toate nodurile părinți
                var parentNodes = [];
                var currentNode = selectedNode;
                while (currentNode.parent !== '#') {
                    var parentNode = data.instance.get_node(currentNode.parent);
                    parentNodes.push(parentNode.data);
                    currentNode = parentNode;
                }

                // Acum aveți datele de la toate nodurile părinți în parentNodes
                console.log('Date de la nodurile părinți:', parentNodes);

                // Puteți utiliza datele de la nodurile părinți și de la nodul selectat după cum doriți
            });
        }
    });

    
}

