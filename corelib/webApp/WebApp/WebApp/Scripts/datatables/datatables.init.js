/**
* Component: Datatable
* 
*/

var handleDataTableButtons = function() {
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

TableManageButtons = function() {
    "use strict";
    return {
        init: function (tableName) {
            switch (tableName) {
                case "system_users":
                case "acquisition_request":
                    handleDataTableButtons()
                    break;
                default:
            }
        }
    }
}();