
(function ($) {

    var _service = abp.services.app.vehicleDocsType,
        l = abp.localization.getSource('Morpho'),
        _$modal = $('#CreateVehicleDocsTypeModal'),
        _$form = _$modal.find('form'),
        _$table = $('#VehicleDocsTypeTable');
    console.log(_service)

    var _$vehicleTable = _$table.DataTable({

        paging: true,
        serverSide: false,

        ajax: function (data, callback, settings) {

            $.post("/Master/getAllVehicleDocsTypeList")
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
            { targets: 0, data: 'document_type_name', sortable: false },
            { targets: 1, data: 'description', sortable: false },

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
                <button class="btn btn-sm bg-secondary edit-vehicle-docs"
                    data-id="${row.id}">
                    <i class="fas fa-pencil-alt"></i> Edit
                </button>

                <button class="btn btn-sm bg-danger delete-vehicle-docs"
                    data-id="${row.id}"
                    data-name="${row.document_type_name}">
                    <i class="fas fa-trash"></i> Delete
                </button>
            `
            }
        ]
    });



    _$form.validate({
        rules: {
            document_type_name: "required",
            descriptionk: "required"
        }
    });

    _$form.find(".save-button").on("click", function (e) {

        e.preventDefault();
        if (!_$form.valid()) return;

        let data = _$form.serializeFormToObject();

        abp.ui.setBusy(_$modal);

        $.ajax({
            url: "/Master/CreateVehicleDocsType",
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



    $(document).on('click', '.edit-vehicle-docs', function () {

        var id = $(this).attr("data-id");

        abp.ajax({
            url: '/Master/EditVehicleDocsTypeModal?id=' + id,
            type: 'GET',
            dataType: 'html',
            success: function (content) {

                $('#EditVehicleDocsTypeModal .modal-content').html(content);
                $('#EditVehicleDocsTypeModal').modal('show');
            }
        });
    });

    $(document).on("click", ".save-edit", function () {

        let data = {
            Id: $("#Id").val(),
            document_type_name: $("#document_type_name").val(),
            description: $("#description").val()
        };

        abp.ui.setBusy("#EditVehicleDocsTypeModal");

        $.ajax({
            url: "/Master/UpdateVehicleDocsType",
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
                    $("#EditVehicleDocsTypeModal").modal("hide");
                    _$vehicleTable.ajax.reload();
                }

            })
            .fail(function (err) {
                console.log(err)
                abp.message.error("Something went wrong");
                abp.notify.error("Something went wrong");
            })
            .always(() => abp.ui.clearBusy("#EditVehicleDocsTypeModal"));
    });
    $(document).on('click', '.delete-vehicle-docs', function () {

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
                        url: "/Master/DeleteVehicleDocsType",
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
            Id: id
        };

        $.ajax({
            url: "/Master/UpdateStatusVehicleDocsType",
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

})(jQuery);
