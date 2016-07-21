/**
  * Generic ajax method for json request
  * @return Json
  * @param mixed
  */
var loadJsonResult = function params(url, params, callback) {

    $.ajax({
        url: url,
        data: params,
        type: 'POST',
        dataType: "json",
        cache: false,
        contentType: false,
        processData: false,
        traditional: true,
        success: callback,
        error: function (XHR, errStatus, errorThrown) {
            var err = JSON.parse(XHR.responseText);
            errorMessage = err.Message;
            alert(errorMessage);
        }
    });

},

formTweaks = function () {

    $('.summernote').summernote({
        height: 350,                 // set editor height
        minHeight: null,             // set minimum height of editor
        maxHeight: null,             // set maximum height of editor
        focus: false                 // set focus to editable area after initializing summernote
    });

},

submitAcquisition = function () {

    $('#submit-acquisition').on('click', function (e) {
        
        e.preventDefault();
        e.stopImmediatePropagation();

        var form = $('#form-acquisition');

        form.parsley().validate();

        if (form.parsley().isValid()) {
            
            swal({
                title: "Updating Dealers Information List?",
                text: "Would you like to continue saving changes?",
                type: "info",
                showCancelButton: true,
                confirmButtonColor: "#34495e",
                confirmButtonText: "Continue saving",
                showLoaderOnConfirm: true,
                closeOnConfirm: false
            }, function (isConfirm) {

                if (isConfirm) {

                    setTimeout(function () {
                        swal("Success!", "Dealers Information has successfully been updated!", "success");
                    }, 2000)

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
