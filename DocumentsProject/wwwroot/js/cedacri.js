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