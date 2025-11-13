using System;
using Morpho.Debugging;

namespace Morpho
{
    public class MorphoConsts
    {
        public const string LocalizationSourceName = "Morpho";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = true;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// SECURITY: This should be loaded from configuration/environment variables in production
        /// </summary>
        public static readonly string DefaultPassPhrase = 
            Environment.GetEnvironmentVariable("MORPHO_ENCRYPTION_KEY") ?? 
            (DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : throw new InvalidOperationException("MORPHO_ENCRYPTION_KEY environment variable must be set in production"));
    }
}
