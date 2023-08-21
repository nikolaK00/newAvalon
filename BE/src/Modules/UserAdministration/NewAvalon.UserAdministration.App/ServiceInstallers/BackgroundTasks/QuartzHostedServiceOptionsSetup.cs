﻿using Microsoft.Extensions.Options;
using Quartz;

namespace NewAvalon.UserAdministration.App.ServiceInstallers.BackgroundTasks
{
    public sealed class QuartzHostedServiceOptionsSetup : IConfigureOptions<QuartzHostedServiceOptions>
    {
        public void Configure(QuartzHostedServiceOptions options) => options.WaitForJobsToComplete = true;
    }
}
