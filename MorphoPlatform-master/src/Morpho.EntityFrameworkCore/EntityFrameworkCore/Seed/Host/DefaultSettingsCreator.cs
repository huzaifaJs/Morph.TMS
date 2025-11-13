using System.Linq;
using Microsoft.EntityFrameworkCore;
using Abp.Configuration;
using Abp.Localization;
using Abp.MultiTenancy;
using Abp.Net.Mail;

namespace Morpho.EntityFrameworkCore.Seed.Host
{
    public class DefaultSettingsCreator
    {
        private readonly MorphoDbContext _context;

        public DefaultSettingsCreator(MorphoDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            int? tenantId = MorphoConsts.MultiTenancyEnabled ? null : MultiTenancyConsts.DefaultTenantId;

            // Emailing - Configure these values in production
            AddSettingIfNotExists(EmailSettingNames.DefaultFromAddress, "noreply@morpho.app", tenantId);
            AddSettingIfNotExists(EmailSettingNames.DefaultFromDisplayName, "Morpho Application", tenantId);

            // Languages
            AddSettingIfNotExists(LocalizationSettingNames.DefaultLanguage, "en", tenantId);
        }

        private void AddSettingIfNotExists(string name, string value, int? tenantId = null)
        {
            if (_context.Settings.IgnoreQueryFilters().Any(s => s.Name == name && s.TenantId == tenantId && s.UserId == null))
            {
                return;
            }

            _context.Settings.Add(new Setting(tenantId, null, name, value));
            _context.SaveChanges();
        }
    }
}
