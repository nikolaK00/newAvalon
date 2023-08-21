using Microsoft.Extensions.Options;
using Quartz;

namespace NewAvalon.Catalog.App.ServiceInstallers.BackgroundTasks
{
    public sealed class QuartzHostedServiceOptionsSetup : IConfigureOptions<QuartzHostedServiceOptions>
    {
        public void Configure(QuartzHostedServiceOptions options) => options.WaitForJobsToComplete = true;
    }
}
