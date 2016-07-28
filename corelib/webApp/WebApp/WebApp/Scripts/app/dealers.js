var deletedFile = [],

dealersList = function () {

    $loadPartialView("./Dealers/dealersList", function (data) {

        $('#dealerInfoContainer').html("");
        $('#dealerInfoContainer').hide();

        $('#dealerTableCont').fadeIn();
        $('#dealerTableCont').empty();
        $('#dealerTableCont').html(data);

        TableManageButtons.init("to_datatable");

    }, {})

},

getOneDealerInfo = function (DealerRefCode, isNew, showTab) {

    $loadPartialView("./Dealers/dealers", function (data) {

        $('#dealerTableCont').html("");
        $('#dealerTableCont').hide();

        $('#dealerInfoContainer').empty();
        $('#dealerInfoContainer').html(data);

        if (showTab) $('.nav-tabs a[href="#attachement_tab"]').trigger('click')

    }, { dealerRefCode: DealerRefCode, isNew: isNew });

};

$('#add-new-dealer').on('click', function (e) {

    e.preventDefault();
    e.stopImmediatePropagation();

    getOneDealerInfo(null, true);

    $(this).hide();
    $('#update-dealer').show();

})

$(document).on('click','.get-one-dealer', function (e) {

    e.preventDefault();
    e.stopImmediatePropagation();

    getOneDealerInfo($(this).attr('id'), false);

    $('#add-new-dealer').hide();
    $('#update-dealer').show();
    $('#refresh-dealer').show();

})

$('#refresh-dealer').on('click', function () {
    $refCode = $('input[name="DealerRefCode"]').val();
    getOneDealerInfo($refCode, false, false);
})

/**
 * Click event to collect all files remove from table - OThers Tab
 * Under dealer Form details
 * @params deletedFile = [] - Collections of files
 */
$('.dealer-collect-file').on('click', function () {

    deletedFile.push({ 'filePath': $(this).attr('id') });

    $(this).closest('tr').remove();

    return false;

})


$('#btnUploadFileAttach').on('click', function () {

    var $dealersAttachment = $('#dealer-add-edit-attachment');

    $dealersAttachment.parsley().validate();

    if ($dealersAttachment.parsley().isValid()) {

        swal({
            title: "Save and Upload Selected File",
            text: "Would you like to continue saving changes?",
            type: "info",
            showCancelButton: true,
            confirmButtonColor: "#3bafda",
            confirmButtonText: "Submit Request",
            showLoaderOnConfirm: true,
            closeOnConfirm: false
        }, function (isConfirm) {

            if (isConfirm) {

                $refCode = $('input[name="DealerRefCode"]').val();

                var $form = new FormData($dealersAttachment[0])

                $form.append('dealerRefCode', $refCode);

                $loadJsonResult("./Dealers/uploadFileAttachement", $form, function (json) {

                    if (json.isSuccess) {

                        getOneDealerInfo($refCode, false, true);

                        Custombox.close();

                        swal("Success!", "Dealers Information has successfully been updated!", "success");

                    }
                    else {
                        swal("Error Proccessing Request!", json.message, "error");
                    }

                })

            }

        })

    }

})


$('#update-dealer').on('click', function (e) {

    e.preventDefault();
    e.stopImmediatePropagation();

    var $dealersForm = $('#add-edit-dealers');

    $dealersForm.parsley({ excluded: '#custom-value-form input' });
    $dealersForm.parsley().validate();

    if ($dealersForm.parsley().isValid()) {

        swal({
            title: "Updating Dealers Information List?",
            text: "Would you like to continue saving changes?",
            type: "info",
            showCancelButton: true,
            confirmButtonColor: "#3bafda",
            confirmButtonText: "Continue",
            showLoaderOnConfirm: true,
            closeOnConfirm: false
        }, function (isConfirm) {

            if (isConfirm) {

                if ($('#dealer_deleted_files').val() == "")

                    $('#dealer_deleted_files').val(JSON.stringify(deletedFile));

                deletedFile = [];

                var $form = new FormData($('#add-edit-dealers')[0])

                $loadJsonResult("./Dealers/saveDealerInformation", $form, function (json) {

                    if (json.isSuccess) {

                        swal({
                            title: "Success!", text: "Dealers Information has successfully been updated!",
                            type: "success", showCancelButton: false, closeOnConfirm: false, confirmButtonText: "Ok",
                        }, function () {
                            location.reload();
                        })
                    }
                    else {
                        swal("Error Proccessing Request!", json.message, "error");
                    }

                })

            }

        })

    } else {

        var tabId = '#' + $('.parsley-error').closest('.tab-pane').attr('id');

        $('.nav-tabs a[href="' + tabId + '"]').trigger('click')

        return false;
    }

})
