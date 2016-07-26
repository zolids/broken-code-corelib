formTweaks = function () {

    $('#additional_notes').summernote({
        height: 200,                 // set editor height
        minHeight: null,             // set minimum height of editor
        maxHeight: null,             // set maximum height of editor
        focus: false,                 // set focus to editable area after initializing summernote
    });

    $('#justifications').summernote({
        height: 500,                 // set editor height
        minHeight: null,             // set minimum height of editor
        maxHeight: null,             // set maximum height of editor
        focus: false,                 // set focus to editable area after initializing summernote
    })

    $('#request_date').datepicker({
        autoclose: true,
        todayHighlight: true,
        minDate: new Date(),
    });

    $('#discard-changes').on('click', function () {
        swal({
            title: 'You currently have unsaved changes!',
            text: "Proceed in refeshing form?",
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: "#3bafda",
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes',
            closeOnConfirm: true
        },
        function (isConfirm) {
            if (isConfirm) {
                setTimeout(function () { location.reload(); }, 100);
            }
        })
    })

    $('#approve-request').on('click', function () {
        $('#form-data-request').block({ message: null });
        $('#notes').fadeIn();
        $('#additional_notes').code("");
    })

    $('#decline-request').on('click', function () {
        $('#form-data-request').block({ message: null });
        $('#notes').fadeIn();
        $('#additional_notes').code("");
    })

},

submitAcquisition = function () {

    $('#submit-acquisition').on('click', function (e) {
        
        e.preventDefault();
        e.stopImmediatePropagation();

        var form = $('#form-acquisition');

        form.parsley().validate();

        if (form.parsley().isValid()) {

            swal({
                title: "Vehicle Requisition Form",
                text: "Would you like to continue request?",
                type: "info",
                showCancelButton: true,
                confirmButtonColor: "#3bafda",
                confirmButtonText: "Submit Request",
                showLoaderOnConfirm: true,
                closeOnConfirm: false
            }, function (isConfirm) {

                if (isConfirm) {

                    var $radio = $('.request-status input[name="request_status"]');
                    $.each($radio, function (k,v) { $($radio[k]).prop('disabled', false) })

                    var $form   = new FormData(form[0]);
                    var $status = $getUrlVars()["isNew"];
                    var $requestId = $getUrlVars()["request_id"];

                    $form.append('justifications', $('#justifications').code());
                    $form.append('additional_notes', $('#additional_notes').code());

                    $form.append('isNew', $status);
                    $form.append('id', $requestId);

                    if ($('#approve-request').is(':checked') || $('#decline-request').is(':checked'))
                        ;//$form.append('additional_notes', $('#additional_notes').code());
                    else $form.append('request_status', 'Pending');
                    
                    $loadJsonResult("/Acquisition/updateAcquisition", $form, function (json) {

                        if (json.isSuccess) {

                            swal({
                                title: "Success!", text: json.message, type: "success", showCancelButton: false, closeOnConfirm: false, confirmButtonText: "Ok",
                            }, function () {
                                if (json.emailSent) {
                                    window.location.replace('/Acquisition/Index?emailSent=true');
                                } else {
                                    window.location.replace('/Acquisition/Index');
                                }
                            })

                        }

                        else {
                            swal("Error Proccessing Request!", json.message, "error");
                        }

                    })

                }

            })

        }

    });

},

acquisition = function () {
    
    "use strict";

    return  {
        init: function () {

            formTweaks();
            submitAcquisition();

        }
    }

}();
