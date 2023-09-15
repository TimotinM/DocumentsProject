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
