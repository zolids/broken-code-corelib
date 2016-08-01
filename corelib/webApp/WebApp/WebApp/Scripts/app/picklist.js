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

    $blockContent('#custom-modal .custom-modal-text');

    $loadPartialView("/Picklist/getVehicleTypeView", function (data) {

        openModal(data, 'Vehicle Types', 'editVehicleType', isOpen);
        $('#custom-modal .custom-modal-text').unblock();

    }, {})

},

functionList.fleet_colors = function (isOpen) {

    $loadPartialView("/Picklist/getFleetColors", function (data) {

        openModal(data, 'Fleet Vehicle Colors', 'editColors', isOpen);

    }, {})

},

functionList.fleet_vehicle_make = function (isOpen) {
    $loadPartialView("/Picklist/getVehicleMakeView", function (data) {

        openModal(data, 'Vehicle Make', 'editVehicleMake', isOpen);

    }, {})
},

functionList.fleet_vehicle_models = function (isOpen) {
    $loadPartialView("/Picklist/getVehicleModelView", function (data) {

        openModal(data, 'Vehicle Models', '', isOpen);

    }, {})
},

functionList.fleet_vehicle_series = function (isOpen) {
    $loadPartialView("/Picklist/getVehicleSeriesView", function (data) {

        openModal(data, 'Vehicle Series', '', isOpen);

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

            $loadJsonResult("/Picklist/updateProvinces", fd, function (json) {

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

        isOpen = ($(this).attr('module') == 'refresh' ? true : false);

        functionList.fleet_vehicle_type(isOpen);

    })
    .on('click', '.editVehicleType', function (e) {

        e.preventDefault();
        e.stopImmediatePropagation();

        var me = this;

        $blockContent('#custom-modal .custom-modal-text');

        $loadPartialView("/Picklist/getVehicleTypeViewEditForm", function (data) {

            openModal(data, 'Add / Edit Vehicle Types', '', true);

            $('#custom-modal .custom-modal-text').unblock();

        }, { VehTypeID: $(me).attr('id') });

    })
    .on('click', '#btnUpdateVehicleTypeMapping', function (e) {

        e.preventDefault();
        e.stopImmediatePropagation();
            
        var form = $('#add-edit-vehicle-types');

        form.parsley().validate();

        if (form.parsley().isValid()) {

            swal({
                title: "Updating Vehicle Type Detail?",
                text: "Would you like to continue saving changes?",
                type: "info",
                showCancelButton: true,
                confirmButtonColor: "#3bafda",
                confirmButtonText: "Continue saving",
                showLoaderOnConfirm: true,
                closeOnConfirm: false
            }, function (isConfirm) {

                if (isConfirm) {

                    $loadJsonResult("/Picklist/updateVehicleType", new FormData(form[0]), function (json) {

                        if (json.isSuccess) {

                            swal("Success!", "Vehicle Type has successfully been updated!", "success");
                                
                            functionList.fleet_vehicle_type(true);
                        }
                        else {
                            swal("Error Proccessing Request!", json.message, "error");
                        }

                    })

                }

            })

        }
    })

    .on('click', '.view-colors', function (e) {
        functionList.fleet_colors(false);
    })
    .on('click', '.editColors', function (e) {

        e.preventDefault();
        e.stopImmediatePropagation();

        var id = $(this).attr('id');

        var message = "Edit Fleet Vehicle Color?";
        var text = "Please Fleet Vehicle Color";
        var placeHolder = "Enter Fleet Vehicle Color";
        var defaultValue = "";

        if (id == "add-new-item") {
            message = "Add New Fleet Vehicle Color";
            placeHolder = "Enter Fleet Vehicle Color";
        }
        else {
            defaultValue = $(this).closest('tr').find('td').eq(0).text();
            text = "Update Fleet Vehicle Color";
        }

        swal({
            title: message, text: text, type: "input", confirmButtonColor: "#3bafda", confirmButtonText: "Save Changes",
            showCancelButton: true, closeOnConfirm: false, inputPlaceholder: placeHolder
        }, function (inputValue) {

            if (inputValue === false) return false;

            if (inputValue === "") {
                swal.showInputError("Please Provide Valid Fleet Vehicle Color");
                return false
            }

            var fd = new FormData();

            fd.append('id', id);
            fd.append('color_name', inputValue);

            $loadJsonResult("/Picklist/updateFleetColors", fd, function (json) {

                if (json.isSuccess) {
                    swal("Success!", "Fleet Vehicle Color has successfully been updated!", "success");
                    functionList.fleet_colors(true);
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

    .on('click', '.fleet-equipment-makes', function (e) {
        functionList.fleet_vehicle_make(false);
    })
    .on('click', '.editVehicleMake', function (e) {

        e.preventDefault();
        e.stopImmediatePropagation();

        var id = $(this).attr('id');
        var message = "Edit Vehicle Make?";
        var text = "Please Enter Vehicle make Name";
        var placeHolder = "Enter Vehicle Make";
        var defaultValue = "";

        if (id == "add-new-item") {
            message = "Add New Vehicle Make";
            placeHolder = "Enter Vehicle Make";
        }
        else {
            defaultValue = $(this).closest('tr').find('td').eq(0).text();
            text = "Update Vehicle Make Name";
        }
        swal({
            title: message, text: text, type: "input", confirmButtonColor: "#3bafda", confirmButtonText: "Save Changes",
            showCancelButton: true, closeOnConfirm: false, inputPlaceholder: placeHolder
        }, function (inputValue) {

            if (inputValue === false) return false;

            if (inputValue === "") {
                swal.showInputError("Please Provide Valid Vehicle Make");
                return false
            }

            var fd = new FormData();

            fd.append('id', id);
            fd.append('vehicle_make', inputValue);

            $loadJsonResult("/Picklist/updateVehicleMake", fd, function (json) {

                if (json.isSuccess) {
                    swal("Success!", "Vehicle Make has successfully been updated!", "success");
                    functionList.fleet_vehicle_make(true);
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

    .on('click', '.fleet-equipment-model', function () {

        isOpen = ($(this).attr('module') == 'refresh' ? true : false);

        functionList.fleet_vehicle_models(isOpen);

    })
    .on('click', '.editVehicleModel', function (e) {

        e.preventDefault();
        e.stopImmediatePropagation();

        var me = this;

        $blockContent('#custom-modal .custom-modal-text');

        $loadPartialView("/Picklist/getVehicleTypeModelEditForm", function (data) {

            openModal(data, 'Add / Edit Model', '', true);

            $('#custom-modal .custom-modal-text').unblock();

        }, { ModelID: $(me).attr('id') });
    })
    .on('click', '#btnUpdateVehicleModel', function (e) {

        e.preventDefault();
        e.stopImmediatePropagation();

        var form = $('#add-edit-vehicle-models');

        form.parsley().validate();

        if (form.parsley().isValid()) {
            swal({
                title: "Updating Vehicle Model?",
                text: "Would you like to continue saving changes?",
                type: "info",
                showCancelButton: true,
                confirmButtonColor: "#3bafda",
                confirmButtonText: "Continue saving",
                showLoaderOnConfirm: true,
                closeOnConfirm: false
            }, function (isConfirm) {

                if (isConfirm) {

                    $loadJsonResult("/Picklist/updateVehicleModels", new FormData(form[0]), function (json) {

                        if (json.isSuccess) {
                            swal("Success!", "Vehicle Model has successfully been updated!", "success");
                            functionList.fleet_vehicle_models(true);
                        }
                        else {
                            swal("Error Proccessing Request!", json.message, "error");
                        }

                    })

                }
            });
        }
    })

    .on('click', '.fleet-equipment-series', function () {

        isOpen = ($(this).attr('module') == 'refresh' ? true : false);

        functionList.fleet_vehicle_series(isOpen);

    })
    .on('click', '.editVehicleSeres', function (e) {

        e.preventDefault();
        e.stopImmediatePropagation();

        var me = this;

        $blockContent('#custom-modal .custom-modal-text');

        $loadPartialView("/Picklist/getVehicleSeriesViewEdit", function (data) {

            openModal(data, 'Add / Edit Vehicle Series', '', true);

            $('#custom-modal .custom-modal-text').unblock();

            // populate dropdown
            $('#drp-series-make').trigger('change');

        }, { id : $(me).attr('id') });

    })
    .on('click', '#btnUpdateVehicleSeries', function (e) {

        e.preventDefault();
        e.stopImmediatePropagation();


        var form = $('#add-edit-vehicle-series');

        form.parsley().validate();

        if (form.parsley().isValid()) {

            swal({
                title: "Updating Vehicle Series Detail?",
                text: "Would you like to continue saving changes?",
                type: "info",
                showCancelButton: true,
                confirmButtonColor: "#3bafda",
                confirmButtonText: "Continue saving",
                showLoaderOnConfirm: true,
                closeOnConfirm: false
            }, function (isConfirm) {

                if (isConfirm) {

                    $loadJsonResult("/Picklist/updateVehicleSeries", new FormData(form[0]), function (json) {

                        if (json.isSuccess) {

                            swal("Success!", "Vehicle Series has successfully been updated!", "success");
                            functionList.fleet_vehicle_series(true);
                        }
                        else {
                            swal("Error Proccessing Request!", json.message, "error");
                        }

                    })

                }

            })

        }
    })

    .on('click', '.view-part-category', function () {

    })
},

$pickListRemoveItem = function () {

    $(document).on('click', '.remove-item', function (e) {

        e.preventDefault();
        e.stopImmediatePropagation();

        $integerId = $(this).attr('id');
        $filterBy = $(this).attr('filterBy');

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
            fd.append('filterBy', $filterBy);

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

$searchItemFromTable = function () {

    $(document).on('keyup','.search-from-table', function () {

        var $rows = $(this).closest('table tr');

        var val = $.trim($(this).val()).replace(/ +/g, ' ').toLowerCase();

        $rows.show().filter(function () {
            var text = $(this).text().replace(/\s+/g, ' ').toLowerCase();
            return !~text.indexOf(val);
        }).hide();

    });
}

$pickListDropDown = function () {

    $(document).on('change', '#drp-series-make', function (e) {

        var $form = new FormData()
        var $defaultModel = $('#default-model-name');

        $form.append('module', 'models');
        $form.append('referenceId', $(this).val());

        $loadJsonResult("/Picklist/populateDropDownWithValues", $form, function (json) {

            var $el = $("#drp-series-model");
            $el.empty();
            $el.append($("<option></option>")
                    .attr("value", '').text('Please Select'));

            if (json.fleetModels != null) {
                $.each(json.fleetModels, function (key, value) {
                    $el.append($("<option " + ((value.Model == $defaultModel.val()) ? "selected":"" ) + " ></option>")
                                    .attr("value", value.Model).text(value.Model)
                                    .attr("id", value.ModelID));
                });
            }

        })

    })

}

$picklist = function () {

    'use strict';
    return {
        init: function () {
            $picklistFunctions();
            $pickListRemoveItem();
            $collapsableTable();
            $searchItemFromTable();
            $pickListDropDown();
        }
    }
}();