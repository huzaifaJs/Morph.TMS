using System;
using Abp;
using Abp.Authorization;
using Abp.Dependency;
using Abp.UI;

namespace Morpho.Authorization
{
    /// <summary>
    /// Helper class for creating exceptions and localized messages for failed login attempts.
    /// </summary>
    public class AbpLoginResultTypeHelper : AbpServiceBase, ITransientDependency
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AbpLoginResultTypeHelper"/> class.
        /// </summary>
        public AbpLoginResultTypeHelper()
        {
            LocalizationSourceName = MorphoConsts.LocalizationSourceName;
        }

        /// <summary>
        /// Creates an appropriate exception for a failed login attempt based on the result type.
        /// </summary>
        /// <param name="result">The login result type indicating the reason for failure.</param>
        /// <param name="usernameOrEmailAddress">The username or email address that was used in the login attempt.</param>
        /// <param name="tenancyName">The tenancy name that was used in the login attempt.</param>
        /// <returns>An exception appropriate for the failed login attempt.</returns>
        public Exception CreateExceptionForFailedLoginAttempt(AbpLoginResultType result, string usernameOrEmailAddress, string tenancyName)
        {
            switch (result)
            {
                case AbpLoginResultType.Success:
                    return new Exception("Don't call this method with a success result!");
                case AbpLoginResultType.InvalidUserNameOrEmailAddress:
                case AbpLoginResultType.InvalidPassword:
                    return new UserFriendlyException(L("LoginFailed"), L("InvalidUserNameOrPassword"));
                case AbpLoginResultType.InvalidTenancyName:
                    return new UserFriendlyException(L("LoginFailed"), L("ThereIsNoTenantDefinedWithName{0}", tenancyName));
                case AbpLoginResultType.TenantIsNotActive:
                    return new UserFriendlyException(L("LoginFailed"), L("TenantIsNotActive", tenancyName));
                case AbpLoginResultType.UserIsNotActive:
                    return new UserFriendlyException(L("LoginFailed"), L("UserIsNotActiveAndCanNotLogin", usernameOrEmailAddress));
                case AbpLoginResultType.UserEmailIsNotConfirmed:
                    return new UserFriendlyException(L("LoginFailed"), L("UserEmailIsNotConfirmedAndCanNotLogin"));
                case AbpLoginResultType.LockedOut:
                    return new UserFriendlyException(L("LoginFailed"), L("UserLockedOutMessage"));
                default: // Can not fall to default actually. But other result types can be added in the future and we may forget to handle it
                    Logger.Warn("Unhandled login fail reason: " + result);
                    return new UserFriendlyException(L("LoginFailed"));
            }
        }

        /// <summary>
        /// Creates a localized message for a failed login attempt based on the result type.
        /// </summary>
        /// <param name="result">The login result type indicating the reason for failure.</param>
        /// <param name="usernameOrEmailAddress">The username or email address that was used in the login attempt.</param>
        /// <param name="tenancyName">The tenancy name that was used in the login attempt.</param>
        /// <returns>A localized message describing the failed login attempt.</returns>
        public string CreateLocalizedMessageForFailedLoginAttempt(AbpLoginResultType result, string usernameOrEmailAddress, string tenancyName)
        {
            switch (result)
            {
                case AbpLoginResultType.Success:
                    throw new Exception("Don't call this method with a success result!");
                case AbpLoginResultType.InvalidUserNameOrEmailAddress:
                case AbpLoginResultType.InvalidPassword:
                    return L("InvalidUserNameOrPassword");
                case AbpLoginResultType.InvalidTenancyName:
                    return L("ThereIsNoTenantDefinedWithName{0}", tenancyName);
                case AbpLoginResultType.TenantIsNotActive:
                    return L("TenantIsNotActive", tenancyName);
                case AbpLoginResultType.UserIsNotActive:
                    return L("UserIsNotActiveAndCanNotLogin", usernameOrEmailAddress);
                case AbpLoginResultType.UserEmailIsNotConfirmed:
                    return L("UserEmailIsNotConfirmedAndCanNotLogin");
                default: // Can not fall to default actually. But other result types can be added in the future and we may forget to handle it
                    Logger.Warn("Unhandled login fail reason: " + result);
                    return L("LoginFailed");
            }
        }
    }
}
