var dealersList = function () {

    $loadPartialView("./Dealers/dealersList", function (data) {

        $('#dealerTableCont').empty();
        $('#dealerTableCont').html(data);

    }, {})

},

getOneDealerInfo = function (DealerRefCode, isNew, showTab) {

    $loadPartialView("./Dealers/dealers", function (data) {

        $('#dealerTableCont').html("");
        $('#dealerTableCont').hide();

        $('#dealerInfoContainer').empty();
        $('#dealerInfoContainer').html(data);

        if (showTab) $('.nav-tabs a[href="#attachement_tab"]').tab('show');

    }, { dealerRefCode: DealerRefCode, isNew: isNew });

};

$('#add-new-dealer').on('click', function (e) {

    e.preventDefault();
    e.stopImmediatePropagation();

    getOneDealerInfo(null, true);

    $(this).hide();
    $('#update-dealer').show();

})

$('.get-one-dealer').on('click', function (e) {

    e.preventDefault();
    e.stopImmediatePropagation();

    getOneDealerInfo($(this).attr('id'), false);

    $('#add-new-dealer').hide();
    $('#update-dealer').show();

})