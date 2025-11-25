
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
                <button class="btn btn-sm bg-secondary edit-vehicle"
                    data-id="${row.id}">
                    <i class="fas fa-pencil-alt"></i> ${l('Edit')}
                </button>

                <button class="btn btn-sm bg-danger delete-vehicle"
                    data-id="${row.id}"
                    data-name="${row.document_type_name}">
                    <i class="fas fa-trash"></i> ${l('Delete')}
                </button>
            `
            }
        ]
    });


    _$form.validate({
        rules: {
            document_type_name: "required",
            description: "required"
        }
    });

    _$form.find(".save-button").on("click", function (e) {
        
        e.preventDefault();

        if (!_$form.valid()) return;

        var data = _$form.serializeFormToObject();

        abp.ui.setBusy(_$modal);
        $.ajax({
            url: "/Master/CreateVehicleDocsType",
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
                
                var msg = (err && err.error && err.error.message) || err && err.message || 'Error';
                abp.message.error(msg, l('Error'));
                abp.notify.error(msg);
            })
            .always(() => abp.ui.clearBusy(_$modal));
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
                    $("#EditVehicleDocsTypeModal").modal("hide");
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
                abp.ui.clearBusy("#EditVehicleDocsTypeModal");
            });
    });

    $(document).on('click', '.delete-vehicle', function () {
        

        var id = $(this).attr("data-id");
        var name = $(this).attr("data-name");

        let data = {
            Id: id,
        };
        abp.message.confirm(
            `Are you sure you want to delete '${name}' ?`,
            null,
            function (isConfirmed) {
                if (isConfirmed) {

                    $.ajax({
                        url: "/Master/DeleteVehicleDocsType",
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
                                $("#EditVehicleDocsTypeModal").modal("hide");
                                _$vehicleTable.ajax.reload();
                            }

                        })

                }
            }
        );

    });

    $(document).on('click', '.edit-vehicle', function (e) {
        
        e.preventDefault();
        var url = '/Master/EditVehicleDocsTypeModal?Id=' + id;
        var id = $(this).attr("data-id");

        abp.ajax({
            url:'/Master/EditVehicleDocsTypeModal?Id=' + id,
            type: 'GET',
            dataType: 'html',
            success: function (content) {
                $('#EditVehicleDocsTypeModal .modal-content').html(content);
                $('#EditVehicleDocsTypeModal').modal('show');
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
            Id: id,
        };
        abp.ui.setBusy('#VehicleDocsTypeTable');

        $.ajax({
            url: "/Master/UpdateStatusVehicleDocsType",
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
                    $("#EditVehicleDocsTypeModal").modal("hide");
                    _$vehicleTable.ajax.reload();
                }

            })
            .fail(function (err) {
                var msg = err?.error?.message || "Error updating status";
                abp.message.error(msg);
            })
            .always(function () {
                abp.ui.clearBusy('#VehicleDocsTypeTable');
            });

    });


})(jQuery);
