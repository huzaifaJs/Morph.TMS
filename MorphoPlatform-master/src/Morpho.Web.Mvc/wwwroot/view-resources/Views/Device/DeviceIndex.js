
$(function () {

    const API_BASE = "/api/MasterApi/";

    let _service = abp.services.app.vehicleType,
        l = abp.localization.getSource('Morpho'),
        _$modal = $('#CreateDeviceModal'),
        _$form = _$modal.find('form'),
        _$table = $('#IOTDeviceTable');


    var _$vehicleTable = _$table.DataTable({

        paging: true,
        serverSide: false,

        ajax: function (data, callback, settings) {
            
            $.post("/Device/getAllDeviceList")

                .done(function (response) {
                    if (response.ERROR) {
                        abp.notify.error(response.MESSAGE || "Something went wrong!");
                        return callback({ data: [] });
                    }
                    let rows = response.result.data || [];
                    callback({ data: rows });
                })

                .fail(function (err) {
                    abp.notify.error("VehicleType API not found!");
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
           
            { targets: 0, data: 'device_unique_no', sortable: false },
            { targets: 1, data: 'device_type_name', sortable: false },
            { targets: 2, data: 'device_name', sortable: false },
            { targets: 3, data: 'manufacturer', sortable: false },
            { targets: 4, data: 'serial_number', sortable: false },
            { targets: 5, data: 'imei_number', sortable: false },
            { targets: 6, data: 'min_value', sortable: false },
            { targets: 7, data: 'max_value', sortable: false },

            {
                targets: 8,
                data: 'isblock',
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
                targets: 9,
                data: null,
                sortable: false,
                render: (data, type, row) => `
                <button class="btn btn-sm bg-secondary edit-vehicle"
                    data-id="${row.id}">
                    <i class="fas fa-pencil-alt"></i> ${l('Edit')}
                </button>

                <button class="btn btn-sm bg-danger delete-vehicle"
                    data-id="${row.id}"
                    data-name="${row.device_name} (${row.device_unique_no} )">
                    <i class="fas fa-trash"></i> ${l('Delete')}
                </button>
            `
            }
        ]
    });




    _$form.validate({
        rules: {
            device_unique_no: "required",
            device_type_name: "required",
            device_name: "required",
            imei_number: "required",
        }
    });

    _$form.find(".save-button").on("click", function (e) {

        e.preventDefault();
        if (!_$form.valid()) return;

        let data = _$form.serializeFormToObject();  

        abp.ui.setBusy(_$modal);

        $.ajax({
            url: "/Device/CreateDevice",
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
            url: '/Device/EditDeviceModal?Id=' + id,
            type: 'GET',
            dataType: 'html',
            success: function (content) {
                $('#EditDeviceModal .modal-content').html(content);
                $('#EditDeviceModal').modal('show');
            }
        });
    });





    $(document).on("click", ".save-edit", function () {
        debugger
        let data = {
            Id: $("#Id").val(),
            device_unique_no: $("#device_unique_no").val(),
            device_name: $("#device_name").val(),
            device_type_name: $("#device_type_name").val(),
            manufacturer: $("#manufacturer").val(),
            serial_number: $("#serial_number").val(),
            imei_number: $("#imei_number").val(),
            min_value: $("#min_value").val(),
            max_value: $("#max_value").val(),
            remark: $("#remark").val(),
        };

        abp.ui.setBusy("#EditDeviceModal");

        $.ajax({
            url: "/Device/UpdateDevice",
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
                    $("#EditDeviceModal").modal("hide");
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

            .always(() => abp.ui.clearBusy("#EditDeviceModal"));
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
                        Id: id,
                    };

                    $.ajax({
                        url: "/Device/DeleteDevice",
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
                                $("#EditDeviceModal").modal("hide");
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
            Id: id,
        };

        $.ajax({
            url: "/Device/UpdateStatusDevice",
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
                    $("#EditDeviceModal").modal("hide");
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
