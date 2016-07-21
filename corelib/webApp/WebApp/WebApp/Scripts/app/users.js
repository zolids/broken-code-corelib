/**
* Scope: User registration, Add/Edit Users
* all user related staff
* @author JCabrito
*/

var userUIfunctions = function () {

    $(".select2").select2();

    $('#allow_view_page').on('click', function (e) {
        $(this).closest('table').find('.access-view-page').prop('checked', this.checked);
    });

    $('#allow_download_page').on('click', function (e) {
        $(this).closest('table').find('.access-download-page').prop('checked', this.checked);
    });

    $('#allow_upload_page').on('click', function (e) {
        $(this).closest('table').find('.access-upload-page').prop('checked', this.checked);
    });

    $('#allow_view_report').on('click', function (e) {
        $(this).closest('table').find('.access-view-report').prop('checked', this.checked);
    });

    $('#allow_download_report').on('click', function (e) {
        $(this).closest('table').find('.access-download-report').prop('checked', this.checked);
    });

    $('#allow_upload_report').on('click', function (e) {
        $(this).closest('table').find('.access-upload-report').prop('checked', this.checked);
    });

    $('.access-view-page').on('click', function () {
        if (!$(this).prop('checked')) {
            $('#allow_view_page').prop('checked', false);
        }
    })
    $('.access-download-page').on('click', function () {
        if (!$(this).prop('checked')) {
            $('#allow_download_page').prop('checked', false);
        }
    })
    $('.access-upload-page').on('click', function () {
        if (!$(this).prop('checked')) {
            $('#allow_upload_page').prop('checked', false);
        }
    })

    $('.access-view-report').on('click', function () {
        if (!$(this).prop('checked')) {
            $('#allow_view_report').prop('checked', false);
        } else
            this.checkallCheckboxes($(this));
    })
    $('.access-download-report').on('click', function () {
        if (!$(this).prop('checked')) {
            $('#allow_download_report').prop('checked', false);
        }
    })
    $('.access-upload-report').on('click', function () {
        if (!$(this).prop('checked')) {
            $('#allow_upload_report').prop('checked', false);
        }
    })

    var checkallCheckboxes = function (element, parentEl) {
        var allchecked = true;
        $.each(element, function (e, l) {
            console.log($(l).prop('checked'));
        })
    }

},

updateUserInfo = function () {

    "use strict";

    var selMulti = $.map($("#project_involved option:selected"), function (el, i) {
        return $(el).text();
    });

    selMulti.join(", ");

},

initiateUserFunctions = function () {

    "use strict";

    return {
        init: function () {

            userUIfunctions();
            updateUserInfo();

        }
    }

}();