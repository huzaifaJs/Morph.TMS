
$(function () {
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
                    abp.notify.error("API not found!");
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
                render: function (value, type, row) {
                    let url = row.device_unique_no ? `
        <a  href="javascript:void(0)" data-id="${row.device_unique_no}"  class="view-Status-details" title="View File">
            <i class="fas fa-eye"></i>
        </a>
    `: "";

                    return url;
                }
            },
            {
                targets: 10,
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



    $(document).on('click', '.view-Status-details', function () {
        
        var id = $(this).attr("data-id");

        let _data = {
            Id: id
        };
        abp.ajax({
            url: '/Device/getDeviceStatus',
            type: 'GET',
            data: _data,
            success: function (response) {
                
                if (response.error) {
                    $('#dvshowDetails').html("<div class='text-danger'>" + response.message + "</div>");
                    $('#DeviceStatusModal').modal('hide');
                    return;
                }

                var d = response.data;
                if (d != null) {


                    var html = `
                <ul>
                  <li><strong>Device ID:</strong> ${d.device_id}</li>
                  <li><strong>Client ID:</strong> ${d.client_id}</li>
                  <li><strong>Firmware Version:</strong> ${d.firmware_version}</li>
                  <li><strong>IP Address:</strong> ${d.ip_address}</li>
                  <li><strong>Timestamp:</strong> ${d.timestamp}</li>

                  <li><strong>GPS:</strong>
                    <ul>
                      <li><strong>Latitude:</strong> ${d.gps.latitude}</li>
                      <li><strong>Longitude:</strong> ${d.gps.longitude}</li>
                      <li><strong>Altitude:</strong> ${d.gps.altitude}</li>
                      <li><strong>Accuracy:</strong> ${d.gps.accuracy}</li>
                    </ul>
                  </li>

                  <li><strong>RSSI:</strong> ${d.rssi}</li>
                  <li><strong>Battery Level:</strong> ${d.batterie_level}</li>
                  <li><strong>Temperature:</strong> ${d.temperature}</li>
                  <li><strong>Humidity:</strong> ${d.humidity}</li>
                  <li><strong>Mean Vibration:</strong> ${d.mean_vibration}</li>
                  <li><strong>Light:</strong> ${d.light}</li>
                  <li><strong>Status:</strong> ${d.status}</li>
                </ul>
            `;
                    $('#dvshowDetails').html(html);
                    $('#DeviceStatusModal').modal('show');
                }
                else {
                    $('#dvshowDetails').html("<div class='text-danger'>" + response.MESSAGE + "</div>");
                    $('#DeviceStatusModal').modal('show');
                    return;
                }
            }
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

                if (response.error) {
                    abp.notify.error(response.error.message || "Something went wrong!");
                }

                else if (response.result.error) {
                    abp.notify.error(response.result.message || "Something went wrong!");
                }
                else {
                    abp.notify.success(response.MESSAGE || "Updated Successfully!");
                    $("#EditDeviceModal").modal("hide");
                    _$vehicleTable.ajax.reload();
                }

            })
            .fail(function (err) {
                console.log(err)
                abp.message.error("Something went wrong");
                abp.notify.error("Something went wrong");
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
                        Id: id
                    };

                    $.ajax({
                        url: "/Device/DeleteDevice",
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
                        });
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
            });

    });

});
