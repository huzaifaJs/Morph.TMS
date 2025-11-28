
$(function () {

    const API_BASE = "/api/MasterApi/";

    let _service = abp.services.app.vehicleType,
        l = abp.localization.getSource('Morpho'),
        _$modal = $('#CreateVehicleDocsModal'),
        _$form = _$modal.find('form'),
        _$table = $('#VehicleDocsTable');


    var _$vehicleTable = _$table.DataTable({

        paging: true,
        serverSide: false,

        ajax: function (data, callback, settings) {
            $.post("/Vehicle/getAllDataVehicleDocs")
                .done(function (response) {


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
            { targets: 0, data: 'vehicle_name', sortable: false },
            { targets: 1, data: 'document_type', sortable: false },
            { targets: 2, data: 'document_number', sortable: false },
            {
                targets: 3,
                data: null,
                sortable: false,

                render: function (value, type, row) {
                    let url = row.document_docs_url ? `
        <a href="${row.document_docs_url}" target="_blank" title="View File">
            <i class="fas fa-eye"></i>
        </a>
    `: "";

                    return url;
                }
            },


            { targets: 4, data: 'issue_date_string', sortable: false },
            { targets: 5, data: 'expiry_date_string', sortable: false },
            { targets: 6, data: 'description', sortable: false },

            {
                targets: 7,
                data: 'is_active',
                sortable: false,
                render: function (value, type, row) {
                    return `
                <input type="checkbox" class="vt-status-toggle"
                       data-id="${row.id}"
                       ${value ? "checked" : ""}>
            `
                }
            },

            {
                targets: 8,
                data: null,
                sortable: false,
                render: (data, type, row) => `
            <button class="btn btn-sm bg-secondary edit-vehicle"
                data-id="${row.id}">
                <i class="fas fa-pencil-alt"></i> ${l('Edit')}
            </button>

            <button class="btn btn-sm bg-danger delete-vehicle"
                data-id="${row.id}"
                data-name="${row.document_number} (${row.vehicle_name})">
                <i class="fas fa-trash"></i> ${l('Delete')}
            </button>
        `
            }
        ]

    });



    _$form.validate({
        rules: {
            vehicle_id: "required",
            document_type_id: "required",
            document_number: "required",
            issue_date: "required",
            expiry_date: "required",
            description: "required",
            fileUpload: "required"
        }
    });

    _$form.find(".save-button").on("click", function (e) {
        e.preventDefault();
        if (!_$form.valid()) return;  
        let formData = new FormData();
        formData.append("vehicle_id", $("#Createvehicle_id").val());
        formData.append("document_type_id", $("#Createdocument_type_id").val());
        formData.append("document_number", $("#Createdocument_number").val());
        formData.append("issue_date", $("#Createissue_date").val());
        formData.append("expiry_date", $("#Createexpiry_date").val());
        formData.append("description", $("#createdescription").val());

        let fileInput = $("#fileUpload")[0];
        if (fileInput.files.length > 0) {
            formData.append("fileUpload", fileInput.files[0]);
        }

        abp.ui.setBusy(_$modal);
        $.ajax({
            url: "/Vehicle/CreateVehicleDocs",
            type: "POST",
            data: formData,
            processData: false,
            contentType: false,   
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
            url: '/Vehicle/EditVehicleDocsModal?Id=' + id,
            type: 'GET',
            dataType: 'html',
            success: function (content) {
                $('#EditVehicleDocsModal .modal-content').html(content);
                $('#EditVehicleDocsModal').modal('show');
                let vehicle_id = $("#Hdnvehicle_id").val();
                let document_types_id = $("#Hdndocument_type_id").val();
                BindVehicleDDL("vehicle_id", vehicle_id);
                BindVehicleDocsTypeDDL("document_type_id", document_types_id);
            }
        });
    });





    $(document).on("click", ".save-edit", function () {
        
        let formData = new FormData();
        formData.append("Id", $("#Id").val());
        formData.append("vehicle_id", $("#vehicle_id").val());
        formData.append("document_type_id", $("#document_type_id").val());
        formData.append("document_number", $("#document_number").val());
        formData.append("issue_date", $("#issue_date").val());
        formData.append("expiry_date", $("#expiry_date").val());
        formData.append("description", $("#description").val());

        let fileInput = $("#EditfileUpload")[0];
        if (fileInput.files.length > 0) {
            formData.append("fileUpload", fileInput.files[0]);
        }


        abp.ui.setBusy("#EditVehicleDocsModal");

        $.ajax({
            url: "/Vehicle/UpdateVehicleDocs",
            type: "POST",
            data: formData,
            processData: false,
            contentType: false,   
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
                    $("#EditVehicleDocsModal").modal("hide");
                    _$vehicleTable.ajax.reload();
                }

            })
            .fail(function (err) {
                console.log(err)
                abp.message.error("Something went wrong");
                abp.notify.error("Something went wrong");
            })
            .always(() => abp.ui.clearBusy("#EditVehicleDocsModal"));

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
                        url: "/Vehicle/DeleteVehicleDocs",
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
            url: "/Vehicle/UpdateStatusVehicleDocs",
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

            })
            .fail(function (err) {
                console.log(err)
                abp.message.error("Something went wrong!");
            });
       
    });

    function BindVehicleDDL(IdName,value) {

        $.ajax({
            type: "POST",
            url: '/MorphoUtility/getVehicleDropDown',
            dataType: 'json',
            success: function (response) {
                
                let options = "";
                console.log("Vehicle:", response.result.data);

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

    function BindVehicleDocsTypeDDL(IdName, value) {

        $.ajax({
            type: "POST",
            url: '/MorphoUtility/getVehicleDocsTypDropDown',
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
        $("#btnCreateVehicleDocs").on("click", function () {
            BindVehicleDDL("Createvehicle_id", "0");
            BindVehicleDocsTypeDDL("Createdocument_type_id", "0");
          
        });
    });


});
