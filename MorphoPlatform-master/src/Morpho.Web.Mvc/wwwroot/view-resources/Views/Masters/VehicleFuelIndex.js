
(function ($) {
   
    var _service = abp.services.app.vehicleDocsType,
        l = abp.localization.getSource('Morpho'),
        _$modal = $('#CreateVehicleFuelTypeModal'),
        _$form = _$modal.find('form'),
        _$table = $('#VehicleFuelTypeTable');
    console.log(_service)
    var _$vehicleTable = _$table.DataTable({
        paging: true,
        serverSide: false,

   
        ajax: function (data, callback, settings) {

            $.post("/Master/getAllVehicleFuelTypeList")
                .done(function (response) {
                    
                    console.log(response)
                    if (response.error) {
                        abp.notify.error(response.error.message || "Something went wrong!");
                        return callback({ data: [] });
                    }

                    if (response.result.error) {
                        abp.notify.error(response.result.message || "Something went wrong!");
                        return callback({ data: [] });
                    }

                    let rows = response.result.data || [];
                    callback({ data: rows });
                })
                .fail(function () {
                    abp.notify.error("Vehicle Fuel Type API not found!");
                    callback({ data: [] });
                });
        },


        buttons: [
            {
                name: 'refresh',
                text: '<i class="fas fa-redo-alt"></i>',
                action: () => _$vehicleTable.ajax.reload()
            }
        ],

        columnDefs: [
            { targets: 0, data: 'fuel_type_name', sortable: false },
            { targets: 1, data: 'remark', sortable: false },

            {
                targets: 2,
                data: 'is_active',
                sortable: false,
                render: function (value, type, row) {
                    return `
                    <input type="checkbox" class="vt-status-toggle"
                        data-id="${row.id}"
                        ${value ? "checked" : ""}>
                `;
                }
            },

            {
                targets: 3,
                data: null,
                sortable: false,
                render: (data, type, row) => `
                <button class="btn btn-sm bg-secondary edit-vehicle"
                    data-id="${row.id}">
                    <i class="fas fa-pencil-alt"></i> ${l('Edit')}
                </button>

                <button class="btn btn-sm bg-danger delete-vehicle"
                    data-id="${row.id}"
                    data-name="${row.fuel_type_name}">
                    <i class="fas fa-trash"></i> ${l('Delete')}
                </button>
            `
            }
        ]
    });


    _$form.validate({
        rules: {
            fuel_type_name: "required",
            remark: "required"
        }
    });

    _$form.find(".save-button").on("click", function (e) {
        
        
        e.preventDefault();

        if (!_$form.valid()) return;

        var data = _$form.serializeFormToObject();

        abp.ui.setBusy(_$modal);
        $.ajax({
            url: "/Master/CreateVehicleFuelType",
            type: "POST",
            data: data,
        })
            .done(function (response) {

                if (response.error) {
                    abp.notify.error(response.error.message || "Something went wrong!");
                }

                else if (response.result.error) {
                    abp.notify.error(response.result.message || "Something went wrong!");
                }
                else {
                    _$modal.modal("hide");
                    _$form[0].reset();
                    abp.notify.success(response.result.message || "Created Successfully!");
                    _$vehicleTable.ajax.reload();
                }

            })
            .fail(function (err) {
                console.log(err)
                abp.message.error("Something went wrong");
                abp.notify.error("Something went wrong");
            })
            .always(() => abp.ui.clearBusy(_$modal));

    });
 
    $(document).on("click", ".save-edit", function () {
        

        let data = {
            Id: $("#Id").val(),
            fuel_type_name: $("#fuel_type_name").val(),
            remark: $("#remark").val()
        };

        abp.ui.setBusy("#EditVehicleFuelTypeModal");
        $.ajax({
            url: "/Master/UpdateVehicleFuelType",
            type: "POST",
            data: data,
        })
            .done(function (response) {

                if (response.error) {
                    abp.notify.error(response.error.message || "Something went wrong!");
                }

                else if (response.result.error) {
                    abp.notify.error(response.result.message || "Something went wrong!");
                }
                else {
                    abp.notify.success(response.MESSAGE || "Updated Successfully!");
                    $("#EditVehicleFuelTypeModal").modal("hide");
                    _$vehicleTable.ajax.reload();
                }

            })
            .fail(function (err) {
                console.log(err)
                abp.message.error("Something went wrong");
                abp.notify.error("Something went wrong");
            })
            .always(() => abp.ui.clearBusy("#EditVehicleFuelTypeModal"));
    });

    $(document).on('click', '.delete-vehicle', function () {
        

        var id = $(this).attr("data-id");
        var name = $(this).attr("data-name");
        abp.message.confirm(
            `Are you sure you want to delete '${name}' ?`,
            null,
            function (isConfirmed) {
                if (isConfirmed) {

                    let data = {
                        FuelTypeId: id
                    };

                    $.ajax({
                        url: "/Master/DeleteVehicleFuelType",
                        type: "POST",
                        data: data,
                    })
                        .done(function (response) {

                            if (response.ERROR) {
                                abp.message.error(response.MESSAGE || "Error");
                                return;
                            }
                            else if (response.result.error) {
                                abp.notify.error(response.result.message || "Something went wrong!");
                            }
                            else {
                                abp.notify.success(response.MESSAGE || "Deleted Successfully!");
                                _$vehicleTable.ajax.reload();
                            }
                        })
                        .fail(function (err) {
                            console.log(err)
                            abp.message.error("Something went wrong!");
                        }).always(function () {
                            abp.ui.clearBusy('#VehicleFuelTypeTable');
                        });
;
                }
            }
        );

    });

    $(document).on('click', '.edit-vehicle', function (e) {
        
        e.preventDefault();
        var url = '/Master/EditVehicleFuelTypeModal?Id=' + id;
        var id = $(this).attr("data-id");

        abp.ajax({
            url:'/Master/EditVehicleFuelTypeModal?Id=' + id,
            type: 'GET',
            dataType: 'html',
            success: function (content) {
                $('#EditVehicleFuelTypeModal .modal-content').html(content);
                $('#EditVehicleFuelTypeModal').modal('show');
            }
        });
    });
    _$modal.on('shown.bs.modal', () => {
        _$modal.find('input:first').focus();
    }).on('hidden.bs.modal', () => {
        _$form.clearForm();
    });


    $(document).on('change', '.vt-status-toggle', function () {
        

        var id = $(this).data('id');
        var isActive = $(this).is(':checked');
        let data = {
            FuelTypeId: id,
        };
        abp.ui.setBusy('#VehicleFuelTypeTable');
        $.ajax({
            url: "/Master/UpdateStatusVehicleFuelType",
            type: "POST",
            data: data,
        })
            .done(function (response) {


                if (response.ERROR) {
                    abp.message.error(response.MESSAGE || "Error");
                    return;
                }
                else if (response.result.error) {
                    abp.notify.error(response.result.message || "Something went wrong!");
                }
                else {
                    abp.notify.success(response.MESSAGE || "Status Updated!");
                    _$vehicleTable.ajax.reload();
                }
                _$vehicleTable.ajax.reload();
            })
            .fail(function (err) {
                console.log(err)
                abp.message.error("Something went wrong!");
            })
             .always(function () {
                 abp.ui.clearBusy('#VehicleFuelTypeTable');
             });


    });


})(jQuery);
