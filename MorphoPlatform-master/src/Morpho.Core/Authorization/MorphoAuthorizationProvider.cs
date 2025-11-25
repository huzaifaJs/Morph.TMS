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

            var masters = pages.CreateChildPermission(
PermissionNames.Pages_Master,
L("Masters")
);
            masters.CreateChildPermission(
                PermissionNames.Pages_Master_VehicleType,
                L("VehicleTypes")
            );
            masters.CreateChildPermission(
               PermissionNames.Pages_Master_VehicleDocsType,
               L("VehicleDocsTypes")
           );
            masters.CreateChildPermission(
              PermissionNames.Pages_Master_VehicleFuelType,
              L("VehicleFuelTypes")
          );

            var iotDevice = pages.CreateChildPermission(
            PermissionNames.Pages_IOTDevice_DeviceManagement,
            L("IOTDeviceManagement")
            );
            iotDevice.CreateChildPermission(
             PermissionNames.Pages_IOTDevice,
             L("IOTDeviceRegister")
         );
            var vehicleIndex = pages.CreateChildPermission(
          PermissionNames.Pages_VehicleManagement,
          L("VehicleManagement")
          );
            vehicleIndex.CreateChildPermission(
             PermissionNames.Pages_Vehicle,
             L("Vehicle")
         );

        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, MorphoConsts.LocalizationSourceName);
        }
    }
}
