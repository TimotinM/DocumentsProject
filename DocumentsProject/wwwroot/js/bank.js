function generateServiceReport() {
    $.ajax({
        url: "/Bank/GetServiceReportDocuments",
        method: "GET",
        success: function (data) {
            data.forEach(function (item) {

                if (item.children && item.children.length > 0) {
                    var treeId = 'serviceReport_' + item.text;
                    var container = $('<div>').addClass('col-md-4 bg-secondary rounded').appendTo('#serviceReportContainer');
                    var hElement = $('<h3>').text(item.text).addClass('d-flex justify-content-center text-white').appendTo(container);
                    $('<div>').attr('id', treeId).appendTo('#serviceReportContainer');

                    $('#' + treeId).jstree({
                        'core': {
                            'data': item.children
                        },
                        'plugins': ['contextmenu'],
                        'contextmenu': {
                            'items': function (node) {
                                if (node.children.length === 0) {
                                    var contextMenuItems = {
                                        'download': {
                                            'label': 'Download',
                                            'action': function () {
                                                downloadFile(node.original.value, node.original.text);
                                            }
                                        },
                                        'info': {
                                            'label': 'Info',
                                            'action': function () {
                                                loadDocumentDetails(node.original.value);
                                            }
                                        }
                                    };
                                    return contextMenuItems;
                                }
                                return {};
                            }
                        }
                    });
                }
            });
        }
    });
}

function generateSLAReport() {
    $.ajax({
        url: "/Bank/GetSLAReportDocuments",
        method: "GET",
        success: function (data) {
            data.forEach(function (item) {

                if (item.children && item.children.length > 0) {
                    var treeId = 'slaReport_' + item.text;
                    var container = $('<div>').addClass('col-md-4 bg-secondary rounded').appendTo('#slaReportContainer');
                    var hElement = $('<h3>').text(item.text).addClass('d-flex justify-content-center text-white').appendTo(container);
                    $('<div>').attr('id', treeId).appendTo('#slaReportContainer');

                    $('#' + treeId).jstree({
                        'core': {
                            'data': item.children
                        },
                        'plugins': ['contextmenu'],
                        'contextmenu': {
                            'items': function (node) {
                                if (node.children.length === 0) {
                                    var contextMenuItems = {
                                        'download': {
                                            'label': 'Download',
                                            'action': function () {
                                                downloadFile(node.original.value, node.original.text);
                                            }
                                        },
                                        'info': {
                                            'label': 'Info',
                                            'action': function () {
                                                loadDocumentDetails(node.original.value);
                                            }
                                        }
                                    };
                                    return contextMenuItems;
                                }
                                return {};
                            }
                        }
                    });
                }
            });
        }
    });
}

function generateProjectReport() {
    $.ajax({
        url: "/Bank/GetProjectReportDocuments",
        method: "GET",
        success: function (data) {
            data.forEach(function (item) {

                if (item.children && item.children.length > 0) {
                    var treeId = 'projectReport_' + item.text.replace(/\s/g, '');
                    var container = $('<div>').addClass('col-md-4 bg-secondary rounded').appendTo('#projectReportContainer');
                    var hElement = $('<h3>').text(item.text).addClass('d-flex justify-content-center text-white').appendTo(container);
                    $('<div>').attr('id', treeId).appendTo('#projectReportContainer');

                    $('#' + treeId).jstree({
                        'core': {
                            'data': item.children
                        },
                        'plugins': ['contextmenu'],
                        'contextmenu': {
                            'items': function (node) {
                                if (node.children.length === 0) {
                                    var contextMenuItems = {
                                        'download': {
                                            'label': 'Download',
                                            'action': function () {
                                                downloadFile(node.original.value, node.original.text);
                                            }
                                        },
                                        'info': {
                                            'label': 'Info',
                                            'action': function () {
                                                loadDocumentDetails(node.original.value);
                                            }
                                        }
                                    };
                                    return contextMenuItems;
                                }
                                return {};
                            }
                        }
                    });
                }
            });
        }
    });
}

function downloadFile(id, name) {
    $.ajax({
        url: "/Bank/GetFileById",
        method: "GET",
        data: {
            id: id
        },
        xhrFields: {
            responseType: 'blob'
        },
        success: function (data) {
            var a = document.createElement('a');
            var url = window.URL.createObjectURL(data);
            a.href = url;
            document.body.append(a);
            a.download = name;
            a.click();
            a.remove();
            window.URL.revokeObjectURL(url);
        },
        error: function (result) {
            console.error("err", result);
        }
    });
}

function loadDocumentDetails(id) {
    $.ajax({
        url: "/Bank/GetDocumentDetails",
        method: "GET",
        data: {
            id: id
        },
        success: function (response) {
            $('#modalTitle').html(null)
            $('#modalTitle').html("Document Information")
            $('#modalBody').html(null);
            $('#modalBody').html(response);
            $('#modalContainer').modal('toggle');
        }
    });
}

