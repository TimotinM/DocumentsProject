function goToAdminIndex() {
    $.ajax({
        url: "/Admin/Index",
        method: "GET",
        success: function (response) {
            $('#contentDiv').html(null);
            $('#contentDiv').html(response);
            loadInstitutionsTable();
            loadUsersTable();
        }
    });
}

function goToCedacriIndex() {
    $.ajax({
        url: "/Cedacri/Index",
        method: "GET",
        success: function (response) {
            $('#contentDiv').html(null);
            $('#contentDiv').html(response);
            generateDocumentsTree(); 
            loadDocumentsTable();
        }
    });
}

function goToBankIndex() {
    $.ajax({
        url: "/Bank/Index",
        method: "GET",
        success: function (response) {
            $('#contentDiv').html(null);
            $('#contentDiv').html(response);
        }
    });
}