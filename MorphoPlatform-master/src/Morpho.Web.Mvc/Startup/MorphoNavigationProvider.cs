using Abp.Application.Navigation;
using Abp.Authorization;
using Abp.Localization;
using Morpho.Authorization;
using StackExchange.Redis;

namespace Morpho.Web.Startup
{
    /// <summary>
    /// This class defines menus for the application.
    /// </summary>
    public class MorphoNavigationProvider : NavigationProvider
    {

        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.About,
                        L("About"),
                        url: "About",
                        icon: "fas fa-info-circle"
                    )
                )
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.Home,
                        L("HomePage"),
                        url: "",
                        icon: "fas fa-home",
                        requiresAuthentication: true
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        PageNames.Tenants,
                        L("Tenants"),
                        url: "Tenants",
                        icon: "fas fa-building",
                        permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Tenants)
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        PageNames.Users,
                        L("Users"),
                        url: "Users",
                        icon: "fas fa-users",
                        permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Users)
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        PageNames.Roles,
                        L("Roles"),
                        url: "Roles",
                        icon: "fas fa-theater-masks",
                        permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Roles)
                    )
                );
            // =====================
            // SHIPMENT MANAGEMENT
            // =====================
            context.Manager.MainMenu
                .AddItem(
                    new MenuItemDefinition(
                        "ShipmentManagement",
                        L("ShipmentManagement"),
                        icon: "fas fa-boxes",
                        permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Shipment)
                    )
                    .AddItem(
                        new MenuItemDefinition(
                            "CreateShipmentRequest",
                            L("CreateShipmentRequest"),
                            url: "/Shipment/Create",
                            order: 1,
                            permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Shipment_Create)
                        )
                    )
                    .AddItem(
                        new MenuItemDefinition(
                            "AssignRouteCarrier",
                            L("AssignRouteCarrier"),
                            url: "/Shipment/AssignRoute",
                            order: 2,
                            permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Shipment_AssignRoute)
                        )
                    )
                    .AddItem(
                        new MenuItemDefinition(
                            "ConfirmLoadingDispatch",
                            L("ConfirmLoadingDispatch"),
                            url: "/Shipment/ConfirmDispatch",
                            order: 3,
                            permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Shipment_ConfirmDispatch)
                        )
                    )
                    .AddItem(
                        new MenuItemDefinition(
                            "RegisterPOD",
                            L("RegisterPOD"),
                            url: "/Shipment/RegisterPOD",
                            order: 4,
                            permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Shipment_RegisterPOD)
                        )
                    )
                     
                );


            context.Manager.MainMenu
             .AddItem(
                 new MenuItemDefinition(
                     "Masters",
                     L("Masters"),
                     icon: "fas fa-boxes",
                     permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Master)
                 )
                   .AddItem(
                     new MenuItemDefinition(
                         "VehicleType",
                         L("VehicleTypes"),
                         url: "/Vehicle/VehicleTypeIndex",
                         order: 1,
                         permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Master_VehicleType)
                     )
                 ).AddItem(
                     new MenuItemDefinition(
                         "VehicleDocsTypes",
                         L("VehicleDocsTypes"),
                         url: "/Master/VehicleDocsTypeIndex",
                         order: 2,
                         permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Master_VehicleFuelType)
                     )
                 )
                   .AddItem(
                     new MenuItemDefinition(
                         "VehicleFuelTypes",
                         L("VehicleFuelTypes"),
                         url: "/Master/VehicleFuelTypeIndex",
                         order: 2,
                         permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Master_VehicleFuelType)
                     )
                 )
             );

            // =====================
            // REAL-TIME TRACKING
            // =====================
            context.Manager.MainMenu
                .AddItem(
                    new MenuItemDefinition(
                        "RealTimeTracking",
                        L("RealTimeTracking"),
                        icon: "fas fa-map-marker-alt"
                    )
                    .AddItem(
                        new MenuItemDefinition(
                            "LiveTracking",
                            L("LiveTracking"),
                            url: "/Tracking/Live",
                            order: 1
                        )
                    )
                    .AddItem(
                        new MenuItemDefinition(
                            "DeviceMonitoring",
                            L("DeviceMonitoring"),
                            url: "/Tracking/Monitoring",
                            order: 2
                        )
                    )
                );

            // =====================
            // POLICY & ALERT MANAGEMENT
            // =====================
            context.Manager.MainMenu
                .AddItem(
                    new MenuItemDefinition(
                        "PolicyManagement",
                        L("PolicyManagement"),
                        icon: "fas fa-bell"
                    )
                    .AddItem(
                        new MenuItemDefinition(
                            "ManagePolicies",
                            L("ManagePolicies"),
                            url: "/Policy/Index",
                            order: 1
                        )
                    )
                    .AddItem(
                        new MenuItemDefinition(
                            "CustomAlertConfiguration",
                            L("CustomAlertConfiguration"),
                            url: "/Policy/AlertConfig",
                            order: 2
                        )
                    )
                );

            // =====================
            // IOT DEVICE MANAGEMENT
            // =====================
            context.Manager.MainMenu
                .AddItem(
                    new MenuItemDefinition(
                        "IoTDeviceManagement",
                        L("IoTDeviceManagement"),
                        icon: "fas fa-microchip"
                    )
                    .AddItem(
                        new MenuItemDefinition(
                            "IOTDevice",
                            L("IOTDevice"),
                            url: "/Device/DeviceIndex",
                            order: 1,
                            permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_IOTDevice_DeviceManagement)
                        )
                    )
                    .AddItem(
                        new MenuItemDefinition(
                            "DeviceToShipment",
                            L("DeviceToShipment"),
                            url: "/Devices/AssignShipment",
                            order: 2
                        )
                    )
                    .AddItem(
                        new MenuItemDefinition(
                            "DeviceManagement",
                            L("DeviceManagement"),
                            url: "/Devices/Index",
                            order: 3
                        )
                    )
                );

            context.Manager.MainMenu
      .AddItem(
          new MenuItemDefinition(
              "VehicleManagement",
              L("VehicleManagement"),
              icon: "fas fa-microchip"
          )
          .AddItem(
              new MenuItemDefinition(
                  "Vehicle",
                  L("Vehicle"),
                  url: "/Vehicle/VehicleIndex",
                  order: 1,
                  permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_VehicleManagement)
              )
          )
           .AddItem(
              new MenuItemDefinition(
                  "VehicleDocs",
                  L("VehicleDocs"),
                  url: "/Vehicle/VehicleDocsIndex",
                  order: 1,
                  permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Vehicle_VehicleDocs)
              )
          )
      );


            // =====================
            // EXCEPTION HANDLING
            // =====================
            context.Manager.MainMenu
                .AddItem(
                    new MenuItemDefinition(
                        "ExceptionHandling",
                        L("ExceptionHandling"),
                        icon: "fas fa-exclamation-triangle"
                    )
                    .AddItem(
                        new MenuItemDefinition(
                            "ExceptionManagement",
                            L("ExceptionManagement"),
                            url: "/Exception/Index",
                            order: 1
                        )
                    )
                );


            //.AddItem( // Menu items below is just for demonstration!
            //    new MenuItemDefinition(
            //        "MultiLevelMenu",
            //        L("MultiLevelMenu"),
            //        icon: "fas fa-circle"
            //    ).AddItem(
            //        new MenuItemDefinition(
            //            "AspNetBoilerplate",
            //            new FixedLocalizableString("ASP.NET Boilerplate"),
            //            icon: "far fa-circle"
            //        ).AddItem(
            //            new MenuItemDefinition(
            //                "AspNetBoilerplateHome",
            //                new FixedLocalizableString("Home"),
            //                url: "https://aspnetboilerplate.com?ref=abptmpl",
            //                icon: "far fa-dot-circle"
            //            )
            //        ).AddItem(
            //            new MenuItemDefinition(
            //                "AspNetBoilerplateTemplates",
            //                new FixedLocalizableString("Templates"),
            //                url: "https://aspnetboilerplate.com/Templates?ref=abptmpl",
            //                icon: "far fa-dot-circle"
            //            )
            //        ).AddItem(
            //            new MenuItemDefinition(
            //                "AspNetBoilerplateSamples",
            //                new FixedLocalizableString("Samples"),
            //                url: "https://aspnetboilerplate.com/Samples?ref=abptmpl",
            //                icon: "far fa-dot-circle"
            //            )
            //        ).AddItem(
            //            new MenuItemDefinition(
            //                "AspNetBoilerplateDocuments",
            //                new FixedLocalizableString("Documents"),
            //                url: "https://aspnetboilerplate.com/Pages/Documents?ref=abptmpl",
            //                icon: "far fa-dot-circle"
            //            )
            //        )
            //    ).AddItem(
            //        new MenuItemDefinition(
            //            "AspNetZero",
            //            new FixedLocalizableString("ASP.NET Zero"),
            //            icon: "far fa-circle"
            //        ).AddItem(
            //            new MenuItemDefinition(
            //                "AspNetZeroHome",
            //                new FixedLocalizableString("Home"),
            //                url: "https://aspnetzero.com?ref=abptmpl",
            //                icon: "far fa-dot-circle"
            //            )
            //        ).AddItem(
            //            new MenuItemDefinition(
            //                "AspNetZeroFeatures",
            //                new FixedLocalizableString("Features"),
            //                url: "https://aspnetzero.com/Features?ref=abptmpl",
            //                icon: "far fa-dot-circle"
            //            )
            //        ).AddItem(
            //            new MenuItemDefinition(
            //                "AspNetZeroPricing",
            //                new FixedLocalizableString("Pricing"),
            //                url: "https://aspnetzero.com/Pricing?ref=abptmpl#pricing",
            //                icon: "far fa-dot-circle"
            //            )
            //        ).AddItem(
            //            new MenuItemDefinition(
            //                "AspNetZeroFaq",
            //                new FixedLocalizableString("Faq"),
            //                url: "https://aspnetzero.com/Faq?ref=abptmpl",
            //                icon: "far fa-dot-circle"
            //            )
            //        ).AddItem(
            //            new MenuItemDefinition(
            //                "AspNetZeroDocuments",
            //                new FixedLocalizableString("Documents"),
            //                url: "https://aspnetzero.com/Documents?ref=abptmpl",
            //                icon: "far fa-dot-circle"
            //            )
            //        )
            //    )
            //);
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, MorphoConsts.LocalizationSourceName);
        }
    }
}