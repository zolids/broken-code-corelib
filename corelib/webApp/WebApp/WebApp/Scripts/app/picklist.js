var openModal = function (data, text, classname, isOpen) {

    $('#custom-modal .custom-modal-text').empty();
    $('#custom-modal .custom-modal-text').html(data);
    $('#custom-modal .custom-modal-title').text(text);

    TableManageButtons.init("responsive_table");
    $addFilterAddBUttom(classname);

    if (isOpen == false) {
        Custombox.open({
            target: '#custom-modal',
            effect: 'door',
            plugin: 'custommodal',
            overlayspeed: 100,
            overlaycolor: '#36404a'
        });
    }

    $('[data-toggle="tooltip"]').tooltip();

},
functionList = {};

functionList.fleet_provinces = function (isOpen) {

    $loadPartialView("/Picklist/getAssignedLocation", function (data) {

        openModal(data, 'Fleet Provinces', 'editProvinces', isOpen);

    }, {})

},

functionList.fleet_vehicle_type = function (isOpen) {

    $loadPartialView("/Picklist/getVehicleTypeView", function (data) {

        openModal(data, 'Vehicle Types', 'editVehicleType', isOpen);

    }, {})

},

$picklistFunctions = function () {

    $(document).on('click', '.view-assign-location', function (e) {

        e.preventDefault();
        e.stopImmediatePropagation();

        functionList.fleet_provinces(false);

    })
    .on('click', '.editProvinces', function (e) {
        
        e.preventDefault();
        e.stopImmediatePropagation();

        var id = $(this).attr('id');
        var message = "Edit Fleet Location?";
        var text = "Please Fleet Location Name";
        var placeHolder = "Enter Fleet Location Name";
        var defaultValue = "";

        if (id == "add-new-item") {
            message = "Add New Fleet Location Name";
            placeHolder = "Enter Fleet Location Name";
        }
        else {
            defaultValue = $(this).closest('tr').find('td').eq(0).text();
            text = "Update Fleet Location Name";
        }

        swal({
            title: message, text: text, type: "input", confirmButtonColor: "#3bafda", confirmButtonText: "Save Changes",
            showCancelButton: true, closeOnConfirm: false, inputPlaceholder: placeHolder
        }, function (inputValue) {

            if (inputValue === false) return false;

            if (inputValue === "") {
                swal.showInputError("Please Provide Valid Fleet Location Name");
                return false
            }

            var fd = new FormData();

            fd.append('id', id);
            fd.append('name', inputValue);

            $loadJsonResult("/Picklist/addEditProvinces", fd, function (json) {

                if (json.isSuccess) {
                    swal("Success!", "Fleet Locations has successfully been updated!", "success");
                    functionList['fleet_provinces'](true);
                }
                else {
                    swal("Error Proccessing Request!", json.message, "error");
                }

            })

        });

        setTimeout(function () {
            $('.sweet-alert.show-input').find('input').val(defaultValue);
            $('.sweet-alert input').css('text-align', 'center')
        }, 1);

    })
    .on('click', '.fleet-equipment-types', function (e) {

        e.preventDefault();
        e.stopImmediatePropagation();

        isOpen = ($(this).attr('module') == 'refresh' ? true : false);

        $('.custombox-modal-container').css('width', '600px');
        functionList.fleet_vehicle_type(isOpen);

    })
    .on('click', '.editVehicleType', function (e) {

        e.preventDefault();
        e.stopImmediatePropagation();

        var me = this;

        $blockContent('#custom-modal .custom-modal-text');

        $loadPartialView("/Picklist/getVehicleTypeViewEditForm", function (data) {

            openModal(data, 'Add / Edit Vehicle Types', '', true);

            $('.custombox-modal-container').css('width', '800px');

            $('#custom-modal .custom-modal-text').unblock();

        }, { VehTypeID: $(me).attr('id') });

    })

},

$pickListRemoveItem = function () {

    $(document).on('click', '.remove-item', function (e) {

        e.preventDefault();
        e.stopImmediatePropagation();

        $integerId = $(this).attr('id');

        var me = this
            ,module = $(this).attr('module')
            ,title = "Are you sure to Delete this record?"
            ,text = "You will not be able to recover this record details!";

        // this is for department location only
        if (module == "location_dept_types")
            text = "Deleting this item will also delete the location/s under this department and may cause blank location and department entries in your work order module.";
        // end

        swal({
            title: title, text: text, type: 'warning', showCancelButton: true,
            confirmButtonColor: '#ef5350', confirmButtonText: 'Yes, Remove it!', closeOnConfirm: false
        }, function () {

            var fd = new FormData($('.form-pick-list')[0]);

            fd.append('integerId', parseInt($integerId));
            fd.append('module', module);

            $loadJsonResult("/Picklist/deleteRecord", fd, function (json) {

                if (json.isSuccess) {
                    
                    swal('Deleted!', 'Record has been deleted.', 'success');

                    if (module in functionList) {

                        functionList[module](true);

                    } else
                        swal("Error Occured!", "Error occured while trying to refresh list. Function Not Found!", "error");
                    
                } else
                    swal("Error Proccessing Request", json.message , "error");
            })

        })

    })

}

$collapsableTable = function () {
    
    /*
    * Toggle Model Views
    **/
    $(document).on('click', '.collapsable-table .toggle', function () {

        //Gets all <tr>'s  of greater depth
        //below element in the table
        var findChildren = function (tr) {
            var depth = tr.data('depth');
            return tr.nextUntil($('tr').filter(function () {
                return $(this).data('depth') <= depth;
            }));
        };

        var el = $(this);
        var tr = el.closest('tr'); //Get <tr> parent of toggle button
        var children = findChildren(tr);

        //Remove already collapsed nodes from children so that we don't
        //make them visible. 
        //(Confused? Remove this code and close Item 2, close Item 1 
        //then open Item 1 again, then you will understand)
        var subnodes = children.filter('.expand_table');
        subnodes.each(function () {
            var subnode = $(this);
            var subnodeChildren = findChildren(subnode);
            children = children.not(subnodeChildren);
        });

        //Change icon and hide/show children
        if (tr.hasClass('collapse_table')) {
            tr.removeClass('collapse_table').addClass('expand_table');
            children.hide();
        } else {
            tr.removeClass('expand_table').addClass('collapse_table');
            children.show();
        }
        return children;
    });

}

$picklist = function () {

    'use strict';
    return {
        init: function () {
            $picklistFunctions();
            $pickListRemoveItem();
            $collapsableTable();
        }
    }
}();