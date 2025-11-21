
(function ($) {
   debugger
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
            _service.getVehicleDocsTypeList().done(function (result) {

                var rows = result.result ?? result;

                callback({ data: rows });
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
                    console.log("ROW:", row);   // ← check here to confirm value
                    console.log("value:", value);   // ← check here to confirm value

                    return `
            <input type="checkbox" class="vt-status-toggle"
                   data-id="${row.id}"
                   ${value === true ? "checked" : ""}>
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

        _service.addVehiclDocsType(data)
            .done(function () {
                _$modal.modal("hide");
                _$form[0].reset();
                abp.notify.success(l("SavedSuccessfully"));
                _$vehicleTable.ajax.reload();
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

        abp.services.app.vehicleDocsType.updateVehicleDocsType(data)
            .done(function () {
                abp.notify.success("Updated Successfully");
                $("#EditVehicleDocsTypeModal").modal("hide");
                $("#VehicleDocsTypeTable").DataTable().ajax.reload();
            })
            .fail(function (err) {
                var msg = (err && err.error && err.error.message) || err && err.message || 'Error';
                abp.message.error(msg, l('Error'));
                abp.notify.error(msg);
            })
            .always(function () {
                abp.ui.clearBusy("#EditVehicleDocsTypeModal");
            });
    });

    $(document).on('click', '.delete-vehicle', function () {
        

        var id = $(this).attr("data-id");
        var name = $(this).attr("data-name");

        abp.message.confirm(
            `Are you sure you want to delete '${name}' ?`,
            null,
            function (isConfirmed) {
                if (isConfirmed) {

                    _service.deleteVehicleDocsType({ id: id })

                        .done(() => {
                            abp.notify.info("Deleted successfully");
                            _$vehicleTable.ajax.reload();
                        })
                        .fail(function (err) {
                            var msg = (err && err.error && err.error.message) || err && err.message || 'Error';
                            abp.message.error(msg, l('Error'));
                            abp.notify.error(msg);
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

        abp.ui.setBusy('#VehicleDocsTypeTable');

        _service.updateVehicleDocsTypeStatus({ id: id })
            .done(function () {
                abp.notify.success("Status updated");
                _$vehicleTable.ajax.reload();
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
