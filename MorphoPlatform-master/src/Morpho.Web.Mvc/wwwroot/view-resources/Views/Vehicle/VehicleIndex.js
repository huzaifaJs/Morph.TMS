
$(function () {

    const API_BASE = "/api/MasterApi/";

    let _service = abp.services.app.vehicleType,
        l = abp.localization.getSource('Morpho'),
        _$modal = $('#CreateVehicleModal'),
        _$form = _$modal.find('form'),
        _$table = $('#VehicleTable');


    var _$vehicleTable = _$table.DataTable({

        paging: true,
        serverSide: false,

        ajax: function (data, callback, settings) {
            
            $.post("/Vehicle/getAllDataVehicleModal")

                .done(function (response) {
                    if (response.ERROR) {
                        abp.notify.error(response.MESSAGE || "Something went wrong!");
                        return callback({ data: [] });
                    }
                    let rows = response.result.data || [];
                    callback({ data: rows });
                })
                .fail(function (err) {
                    abp.notify.error("Vehicle not found!");
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
            { targets: 0, data: 'vehicle_type', sortable: false },
            { targets: 1, data: 'fuel_type', sortable: false },
            { targets: 2, data: 'vehicle_unqiue_id', sortable: false },
            { targets: 3, data: 'vehicle_name', sortable: false },
            { targets:4, data: 'vehicle_number', sortable: false },
            { targets:5, data: 'model_name', sortable: false },
            { targets: 6, data: 'manufacturer', sortable: false },
            { targets:7, data: 'manufacturing_year', sortable: false },
            { targets: 8, data: 'chassis_number', sortable: false },
            { targets: 9, data: 'engine_number', sortable: false },
            { targets: 10, data: 'remark', sortable: false },

            {
                targets: 11,
                data: 'isblock',
                sortable: false,
                render: function (value, type, row) {
                    return `
                    <input type="checkbox" class="vt-status-toggle"
                           data-id="${row.id}"
                           ${value ? "" : "checked"}>
                `;
                }
            },

            {
                targets:12,
                data: null,
                sortable: false,
                render: (data, type, row) => `
                <button class="btn btn-sm bg-secondary edit-vehicle"
                    data-id="${row.id}">
                    <i class="fas fa-pencil-alt"></i> ${l('Edit')}
                </button>

                <button class="btn btn-sm bg-danger delete-vehicle"
                    data-id="${row.id}"
                    data-name="${row.vehicle_number} (${row.vehicle_number}) ">
                    <i class="fas fa-trash"></i> ${l('Delete')}
                </button>
            `
            }
        ]
    });




    _$form.validate({
        rules: {
            vehicle_types_id: "required",
            fuel_types_id: "required",
            vehicle_number: "required",
            vehicle_unqiue_id: "required",
            vehicle_name: "required"
        }
    });

    _$form.find(".save-button").on("click", function (e) {
        debugger

        e.preventDefault();
        if (!_$form.valid()) return;

        let data = _$form.serializeFormToObject();  

        abp.ui.setBusy(_$modal);

        $.ajax({
            url: "/Vehicle/CreateVehicle",
            type: "POST",
            data: data, 
        })
            .done(function (response) {
                
      
                if (response.ERROR) {
                    abp.message.error(response.MESSAGE || "Error");
                    _$vehicleTable.ajax.reload();
                    return; 
                }
                else if (response.result && response.result.error) {
                    abp.notify.error(response.result.message || "Something went wrong!");
                    _$vehicleTable.ajax.reload();
                }
                else {
                    _$modal.modal("hide");
                    _$form[0].reset();
                    abp.notify.success("Saved Successfully");
                    _$vehicleTable.ajax.reload();
                }
               
            })
            .fail(function (err) {
                console.log("Create Fail:", err);

                let msg =
                    err?.responseJSON?.MESSAGE ||  
                    err?.responseJSON?.message ||
                    "Error";

                abp.message.error(msg);
            })
            .always(function () {
                abp.ui.clearBusy(_$modal);
            });
    });



    $(document).on('click', '.edit-vehicle', function () {

        var id = $(this).attr("data-id");

        abp.ajax({
            url: '/Vehicle/EditVehicleModal?Id=' + id,
            type: 'GET',
            dataType: 'html',
            success: function (content) {
                $('#EditVehicleModal .modal-content').html(content);
                $('#EditVehicleModal').modal('show');
            }
        });
    });





    $(document).on("click", ".save-edit", function () {
        
        let data = {
            Id: $("#Id").val(),
            vehicle_type_name: $("#vehicle_type_name").val(),
            Remark: $("#Remark").val()
        };

        abp.ui.setBusy("#EditVehicleModal");

        $.ajax({
            url: "/Vehicle/UpdateVehicleType",
            type: "POST",
            data: data,
        })
            .done(function (response) {
                
      
                if (response.ERROR) {
                    abp.message.error(response.MESSAGE || "Error");
                    _$vehicleTable.ajax.reload();
                    return;
                }
                else if (response.result && response.result.error) {
                    abp.notify.error(response.result.message || "Something went wrong!");
                    _$vehicleTable.ajax.reload();
                }
                else {
                    abp.notify.success("Updated Successfully");
                    $("#EditVehicleModal").modal("hide");
                    _$vehicleTable.ajax.reload();
                }

            })
            .fail(function (err) {
                console.log("Create Fail:", err);

                let msg =
                    err?.responseJSON?.MESSAGE ||
                    err?.responseJSON?.message ||
                    "Error";

                abp.message.error(msg);
            })

            .always(() => abp.ui.clearBusy("#EditVehicleModal"));
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
                        VehicleTypeId: id,
                    };

                    $.ajax({
                        url: "/Vehicle/DeleteVehicleType",
                        type: "POST",
                        data: data,
                    })
                        .done(function (response) {
                            
                  
                            if (response.ERROR) {
                                abp.message.error(response.MESSAGE || "Error");
                                _$vehicleTable.ajax.reload();
                                return;
                            }
                            else if (response.result && response.result.error) {
                                abp.notify.error(response.result.message || "Something went wrong!");
                                _$vehicleTable.ajax.reload();
                            }
                            else {
                                abp.notify.success("Deleted Successfully");
                                $("#EditVehicleModal").modal("hide");
                                _$vehicleTable.ajax.reload();
                            }

                        })
                        .fail(function (err) {
                            console.log("Create Fail:", err);

                            let msg =
                                err?.responseJSON?.MESSAGE ||
                                err?.responseJSON?.message ||
                                "Error";

                            abp.message.error(msg);
                        })

                }
            }
        );

    });

    $(document).on('change', '.vt-status-toggle', function () {

        var id = $(this).data('id');
        let data = {
            VehicleTypeId: id,
        };

        $.ajax({
            url: "/Vehicle/UpdateStatusVehicleType",
            type: "POST",
            data: data,
        })
            .done(function (response) {
                
      
                if (response.ERROR) {
                    abp.message.error(response.MESSAGE || "Error");
                    _$vehicleTable.ajax.reload();
                    return;
                }
                else if (response.result && response.result.error) {
                    abp.notify.error(response.result.message || "Something went wrong!");
                    _$vehicleTable.ajax.reload();
                }
                else {
                    abp.notify.success(response.result.message);
                    $("#EditVehicleModal").modal("hide");
                    _$vehicleTable.ajax.reload();
                }

            })
            .fail(function (err) {
                console.log("Create Fail:", err);

                let msg =
                    err?.responseJSON?.MESSAGE ||
                    err?.responseJSON?.message ||
                    "Error";

                abp.message.error(msg);
            })


    });

});
