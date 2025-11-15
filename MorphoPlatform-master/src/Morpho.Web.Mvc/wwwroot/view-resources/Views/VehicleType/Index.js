
(function ($) {

    var _service = abp.services.app.vehicleType,
        l = abp.localization.getSource('Morpho'),
        _$modal = $('#CreateVehicleTypeModal'),
        _$form = _$modal.find('form'),
        _$table = $('#VehicleTypeTable');

    var _$vehicleTable = _$table.DataTable({
        paging: true,
        serverSide: false,

        ajax: function (data, callback, settings) {
            _service.getVehicleTypesList().done(function (result) {

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
            { targets: 0, data: 'vehicle_type_name', sortable: false },
            { targets: 1, data: 'remark', sortable: false },
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
                        data-name="${row.vehicle_type_name}">
                        <i class="fas fa-trash"></i> ${l('Delete')}
                    </button>
                `
            }
        ]
    });

    _$form.validate({
        rules: {
            vehicle_type_name: "required",
            remark: "required"
        }
    });

    _$form.find(".save-button").on("click", function (e) {
        e.preventDefault();

        if (!_$form.valid()) return;

        var data = _$form.serializeFormToObject();

        abp.ui.setBusy(_$modal);

        _service.addVehicleType(data)
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
            vehicle_type_name: $("#vehicle_type_name").val(),
            Remark: $("#Remark").val()
        };

        abp.ui.setBusy("#EditVehicleTypeModal");

        abp.services.app.vehicleType.updateVehicleType(data)
            .done(function () {
                abp.notify.success("Updated Successfully");
                $("#EditVehicleTypeModal").modal("hide");
                $("#VehicleTypeTable").DataTable().ajax.reload();
            })
            .fail(function (err) {
                var msg = (err && err.error && err.error.message) || err && err.message || 'Error';
                abp.message.error(msg, l('Error'));
                abp.notify.error(msg);
            })
            .always(function () {
                abp.ui.clearBusy("#EditVehicleTypeModal");
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

                    _service.deleteVehicleType({ vehicleTypeId: id })

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
        var url = '/Vehicle/EditModal?Id=' + id;
        var id = $(this).attr("data-id");

        abp.ajax({
            url:'/Vehicle/EditModal?Id=' + id,
            type: 'GET',
            dataType: 'html',
            success: function (content) {
                $('#EditVehicleTypeModal .modal-content').html(content);
                $('#EditVehicleTypeModal').modal('show');
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

        abp.ui.setBusy('#VehicleTypeTable');

        _service.updateVehicleTypeStatus({ VehicleTypeId: id })
            .done(function () {
                abp.notify.success("Status updated");
                _$vehicleTable.ajax.reload();
            })
            .fail(function (err) {
                var msg = err?.error?.message || "Error updating status";
                abp.message.error(msg);
            })
            .always(function () {
                abp.ui.clearBusy('#VehicleTypeTable');
            });

    });


})(jQuery);
