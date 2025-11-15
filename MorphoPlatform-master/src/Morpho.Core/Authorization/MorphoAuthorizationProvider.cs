using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Morpho.Authorization
{
    public class MorphoAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            context.CreatePermission(PermissionNames.Pages_Users_Activation, L("UsersActivation"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
            var pages = context.GetPermissionOrNull(PermissionNames.Pages)
             ?? context.CreatePermission(PermissionNames.Pages, L("Pages"));
            var shipment = pages.CreateChildPermission(
     PermissionNames.Pages_Shipment,
     L("ShipmentManagement")
 );

           
            shipment.CreateChildPermission(
                PermissionNames.Pages_Shipment_Create,
                L("CreateShipmentRequest")
            );

            shipment.CreateChildPermission(
                PermissionNames.Pages_Shipment_AssignRoute,
                L("AssignRouteCarrier")
            );

            shipment.CreateChildPermission(
                PermissionNames.Pages_Shipment_ConfirmDispatch,
                L("ConfirmLoadingDispatch")
            );

            shipment.CreateChildPermission(
                PermissionNames.Pages_Shipment_RegisterPOD,
                L("RegisterPOD")
            );
            shipment.CreateChildPermission(
                PermissionNames.Pages_Shipment_VehicleType,
                L("VehicleTypes")
            );
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, MorphoConsts.LocalizationSourceName);
        }
    }
}
