/**
* Component: Datatable
* 
*/

var $handleDataTableButtons = function() {
        "use strict";
        0 !== $(".datatable-custom").length && $(".datatable-custom").DataTable({
            dom: "Bfrtip",
            buttons: [{
                extend: "copy",
                className: "btn-sm"
            }, {
                extend: "csv",
                className: "btn-sm"
            }, {
                extend: "excel",
                className: "btn-sm"
            }, {
                extend: "pdf",
                className: "btn-sm"
            }, {
                extend: "print",
                className: "btn-sm"
            }],
            responsive: !0
        })
},

$responsiveTable = function () {
    $('.datatable-responsive').dataTable()
},

$keyTable = function () {
    $('.datatable-keytable').DataTable({ keys: true });
}

TableManageButtons = function() {
    "use strict";
    return {
        init: function (tableName) {
            switch (tableName) {
                case "with_export_btn":
                    $handleDataTableButtons()
                    break;
                case "responsive_table":
                    $responsiveTable();
                    break;
                case "key_table":
                    $keyTable();
                    break;
                default:
            }
        }
    }
}();