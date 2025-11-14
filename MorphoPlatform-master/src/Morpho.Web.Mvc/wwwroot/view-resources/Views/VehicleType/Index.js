(function ($) {

    var _service = abp.services.app.vehicleType,  
        l = abp.localization.getSource('Morpho'),
        _$modal = $('#CreateVehicleTypeModal'),
        _$form = _$modal.find('form'),
        _$table = $('#VehicleTypeTable');

    var _$vehicleTable = _$table.DataTable({
        paging: true,
        serverSide: true,
        listAction: {
            ajaxFunction: _service.getVehicleTypesListAsync,
            inputFilter: function () {
                return $('#VehicleTypeSearchForm').serializeFormToObject(true);
            }
        },

        buttons: [
            {
                name: 'refresh',
                text: '<i class="fas fa-redo-alt"></i>',
                action: () => _$vehicleTable.draw(false)
            }
        ],

        responsive: {
            details: { type: 'column' }
        },

        columnDefs: [
            {
                targets: 0,
                className: 'control',
                defaultContent: '',
            },
            {
                targets: 1,
                data: 'vehicleTypeName',
                sortable: false
            },
            {
                targets: 2,
                data: 'remark',
                sortable: false
            },
            {
                targets: 3,
                data: 'isActive',
                sortable: false,
                render: data => `<input type="checkbox" disabled ${data ? 'checked' : ''}>`
            },
            {
                targets: 4,
                data: null,
                sortable: false,
                autoWidth: false,
                render: (data, type, row, meta) => {

                    return `
                        <button 
                            type="button" 
                            class="btn btn-sm bg-secondary edit-vehicle" 
                            data-id="${row.vehicleTypeId}" 
                            data-toggle="modal" 
                            data-target="#EditVehicleTypeModal">
                            <i class="fas fa-pencil-alt"></i> ${l('Edit')}
                        </button>

                        <button 
                            type="button" 
                            class="btn btn-sm bg-danger delete-vehicle" 
                            data-id="${row.vehicleTypeId}" 
                            data-name="${row.vehicleTypeName}">
                            <i class="fas fa-trash"></i> ${l('Delete')}
                        </button>
                    `;
                }
            }
        ]
    });


    _$form.validate({
        rules: {
            VehicleTypeName: "required",
            Remark: "required"
        }
    });

    _$form.find('.save-button').on('click', function (e) {
        e.preventDefault();

        if (!_$form.valid()) {
            return;
        }

        var data = _$form.serializeFormToObject();

        abp.ui.setBusy(_$modal);

        _service.addVehicleTypeAsync(data)
            .done(function () {
                _$modal.modal('hide');
                _$form[0].reset();
                abp.notify.success(l('SavedSuccessfully'));
                _$vehicleTable.ajax.reload();
            })
            .always(function () {
                abp.ui.clearBusy(_$modal);
            });
    });

    $(document).on('click', '.delete-vehicle', function () {
        var id = $(this).attr("data-id");
        var name = $(this).attr("data-name");

        abp.message.confirm(
            abp.utils.formatString(l('AreYouSureDeleteVehicleType'), name),
            null,
            function (isConfirmed) {
                if (isConfirmed) {
                    _service.deleteVehicleTypeAsync({ vehicleTypeId: id })
                        .done(() => {
                            abp.notify.info(l('SuccessfullyDeleted'));
                            _$vehicleTable.ajax.reload();
                        });
                }
            }
        );
    });

    $(document).on('click', '.edit-vehicle', function (e) {
        var id = $(this).attr("data-id");

        e.preventDefault();
        abp.ajax({
            url: abp.appPath + 'VehicleType/EditModal?Id=' + id,
            type: 'POST',
            dataType: 'html',
            success: function (content) {
                $('#EditVehicleTypeModal .modal-content').html(content);
            }
        });
    });

    _$modal.on('shown.bs.modal', () => {
        _$modal.find('input:first').focus();
    }).on('hidden.bs.modal', () => {
        _$form.clearForm();
    });

    $('.btn-search').on('click', () => {
        _$vehicleTable.ajax.reload();
    });

    $('.txt-search').on('keypress', (e) => {
        if (e.which === 13) {
            _$vehicleTable.ajax.reload();
            return false;
        }
    });

})(jQuery);
