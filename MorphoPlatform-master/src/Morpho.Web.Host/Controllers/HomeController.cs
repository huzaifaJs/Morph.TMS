using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp;
using Abp.Extensions;
using Abp.Notifications;
using Abp.Timing;
using Abp.Web.Security.AntiForgery;
using Morpho.Controllers;
using ElmahCore;

namespace Morpho.Web.Host.Controllers
{
    public class HomeController : MorphoControllerBase
    {
        private readonly INotificationPublisher _notificationPublisher;

        public HomeController(INotificationPublisher notificationPublisher)
        {
            _notificationPublisher = notificationPublisher;
        }

        public IActionResult Index()
        {
            return Redirect("/swagger");
        }

        /// <summary>
        /// This is a demo code to demonstrate sending notification to default tenant admin and host admin uers.
        /// Don't use this code in production !!!
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<ActionResult> TestNotification(string message = "")
        {
            if (message.IsNullOrEmpty())
            {
                message = "This is a test notification, created at " + Clock.Now;
            }

            var defaultTenantAdmin = new UserIdentifier(1, 2);
            var hostAdmin = new UserIdentifier(null, 1);

            await _notificationPublisher.PublishAsync(
                "App.SimpleMessage",
                new MessageNotificationData(message),
                severity: NotificationSeverity.Info,
                userIds: new[] { defaultTenantAdmin, hostAdmin }
            );

            return Content("Sent notification: " + message);
        }

        /// <summary>
        /// Test method to demonstrate ElmahCore error logging functionality.
        /// This will log an exception to ElmahCore and can be viewed at /elmah
        /// </summary>
        /// <returns></returns>
        public ActionResult TestElmahError()
        {
            try
            {
                // Intentionally throw an exception to test ElmahCore
                throw new InvalidOperationException("This is a test exception for ElmahCore demonstration. Generated at: " + Clock.Now);
            }
            catch (Exception ex)
            {
                // Log the error to ElmahCore
                ElmahExtensions.RiseError(ex);
                
                return Content($"Test exception logged to ElmahCore at {Clock.Now}. Check /elmah to view the error log.");
            }
        }

        /// <summary>
        /// Test method to log a custom error message to ElmahCore without throwing an exception
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public ActionResult TestElmahLog(string message = "")
        {
            if (message.IsNullOrEmpty())
            {
                message = "This is a test log entry created at " + Clock.Now;
            }

            // Log custom error to ElmahCore
            var customException = new ApplicationException($"Custom Log Entry: {message}");
            ElmahExtensions.RiseError(customException);

            return Content($"Custom log entry created in ElmahCore: {message}. Check /elmah to view the log.");
        }
    }
}
