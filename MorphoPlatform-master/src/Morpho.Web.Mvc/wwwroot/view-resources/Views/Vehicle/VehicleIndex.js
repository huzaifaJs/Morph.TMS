

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
                    data-name="${row.vehicle_name} , ${row.vehicle_unqiue_id} (${row.vehicle_number}) ">
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



    $(document).on('click', '.edit-vehicle', function () {

        var id = $(this).attr("data-id");

        abp.ajax({
            url: '/Vehicle/EditVehicleModal?Id=' + id,
            type: 'GET',
            dataType: 'html',
            success: function (content) {
                $('#EditVehicleModal .modal-content').html(content);
                $('#EditVehicleModal').modal('show');
                let vehicle_types_id = $("#hdnvehicle_types_id").val();
                let fuel_types_id = $("#hdnfuel_types_id").val();
                BindVehicleTypeDDL("vehicle_types_id", vehicle_types_id);
                BindFuelTypeDDL("fuel_types_id", fuel_types_id);
            }
        });
    });





    $(document).on("click", ".save-edit", function () {
        
        let data = {
            Id: $("#Id").val(),
            vehicle_types_id: $("#vehicle_types_id").val(),
            fuel_types_id: $("#fuel_types_id").val(),
            vehicle_number: $("#vehicle_number").val(),
            vehicle_unqiue_id: $("#vehicle_unqiue_id").val(),
            vehicle_name: $("#vehicle_name").val(),
            model_name: $("#model_name").val(),
            manufacturer: $("#manufacturer").val(),
            remark: $("#remark").val(),
            manufacturing_year: $("#manufacturing_year").val(),
            chassis_number: $("#chassis_number").val(),
            engine_number: $("#engine_number").val(),
        };

        abp.ui.setBusy("#EditVehicleModal");

        $.ajax({
            url: "/Vehicle/UpdateVehicle",
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
                    $("#EditVehicleModal").modal("hide");
                    _$vehicleTable.ajax.reload();
                }

            })
            .fail(function (err) {
                console.log(err)
                abp.message.error("Something went wrong");
                abp.notify.error("Something went wrong");
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
                        Id: id
                    };

                    $.ajax({
                        url: "/Vehicle/DeleteVehicle",
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
            url: "/Vehicle/UpdateStatusVehicle",
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

    function BindVehicleTypeDDL(IdName,value) {

        $.ajax({
            type: "POST",
            url: '/MorphoUtility/getVehicleTypeDropDown',
            dataType: 'json',
            success: function (response) {
                
                let options = "";
                console.log("VehicleType:", response.result.data);

                if (response.ERROR) {
                    abp.notify.error(response.MESSAGE);
                    return;
                }

                let ddl = $("#" + IdName);

                ddl.html("");

                for (let i = 0; i < response.result.data.length; i++) {
                    let item = response.result.data[i];
                    if (item.value === value) {
                        options += `<option value="${item.value}" selected>${item.text}</option>`;
                    }
                    else {
                        options += `<option value="${item.value}" >${item.text}</option>`;
                    }
                  
                }

                ddl.html(options);
            }
        });
    }

    function BindFuelTypeDDL(IdName, value) {

        $.ajax({
            type: "POST",
            url: '/MorphoUtility/getFuelTypeDropDown',
            dataType: 'json',
            success: function (response) {
                let options = "";
                if (response.ERROR) {
                    abp.notify.error(response.MESSAGE);
                    return;
                }

                let ddl = $("#" + IdName);

                ddl.html("");

                for (let i = 0; i < response.result.data.length; i++) {
                    let item = response.result.data[i];
                    if (item.value === value) {
                        options += `<option value="${item.value}" selected>${item.text}</option>`;
                    }
                    else {
                        options += `<option value="${item.value}" >${item.text}</option>`;
                    }

                }
                ddl.html(options);
            }
        });
    }
    //$('#CreateVehicleTypeModal').on('shown.bs.modal', function () {
    //    BindVehicleTypeDDL("Createvehicle_types_id", "0");
    //    BindFuelTypeDDL("Createfuel_types_id", "0");
    //});
    $(function () {
        $("#btnCreateVehicle").on("click", function () {
            BindVehicleTypeDDL("Createvehicle_types_id", "0");
            BindFuelTypeDDL("Createfuel_types_id", "0");
            GenerateUniqueVehicleId();
        });
    });
    function GenerateUniqueVehicleId() {
        $.ajax({
            type: "Post",
            url: "/MorphoUtility/getGenerateVehicleId",
            success: function (res) {

                if (res.ERROR) {
                    abp.notify.error(res.MESSAGE);
                    return;
                }
                if (res.success) {
                    $("#Createvehicle_unqiue_id").val(res.result.data);
                } else {
                    abp.notify.error(res.message);
                }
            }
        });
    }

    //$(document).on("click", ".edit-vehicle", function () {
    //    
    //    let vehicle_types_id = $("#hdnvehicle_types_id").val();
    //    let fuel_types_id = $("#hdnfuel_types_id").val();

    //    BindVehicleTypeDDL("vehicle_types_id", vehicle_types_id);
    //    BindFuelTypeDDL("fuel_types_id", fuel_types_id);
    //});


});
