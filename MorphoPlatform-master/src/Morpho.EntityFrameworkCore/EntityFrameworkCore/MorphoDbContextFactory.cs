using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Morpho.Configuration;
using Morpho.Web;

namespace Morpho.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class MorphoDbContextFactory : IDesignTimeDbContextFactory<MorphoDbContext>
    {
        public MorphoDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<MorphoDbContext>();
            
            /*
             You can provide an environmentName parameter to the AppConfigurations.Get method. 
             In this case, AppConfigurations will try to read appsettings.{environmentName}.json.
             Use Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") method or from string[] args to get environment if necessary.
             https://docs.microsoft.com/en-us/ef/core/cli/dbcontext-creation?tabs=dotnet-core-cli#args
             */
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            MorphoDbContextConfigurer.Configure(builder, configuration.GetConnectionString(MorphoConsts.ConnectionStringName));

            return new MorphoDbContext(builder.Options);
        }
    }
}
