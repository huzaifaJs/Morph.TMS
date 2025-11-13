(function ($) {
    var _roleService = abp.services.app.role,
        l = abp.localization.getSource('Morpho'),
        _$modal = $('#RoleEditModal'),
        _$form = _$modal.find('form');

    function save() {
        if (!_$form.valid()) {
            return;
        }

        var role = _$form.serializeFormToObject();
        role.grantedPermissions = _$form.collectCheckedValues('permission');

        abp.ui.setBusy(_$form);
        _roleService.update(role).done(function () {
            _$modal.modal('hide');
            abp.notify.info(l('SavedSuccessfully'));
            abp.event.trigger('role.edited', role);
        }).always(function () {
            abp.ui.clearBusy(_$form);
        });
    }

    _$form.closest('div.modal-content').find(".save-button").click(function (e) {
        e.preventDefault();
        save();
    });

    _$form.find('input').on('keypress', function (e) {
        if (e.which === 13) {
            e.preventDefault();
            save();
        }
    });

    _$modal.on('shown.bs.modal', function () {
        _$form.find('input[type=text]:first').focus();
    });
})(jQuery);
