using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;

namespace Web.Extensions
{
    public static class QuartzExtensions
    {
        public static IApplicationBuilder UseQuartz(this IApplicationBuilder app)
        {
            var scheduler = app.ApplicationServices.GetService<IScheduler>();

            scheduler.Start().GetAwaiter().GetResult();

            return app;
        }

        public static IServiceCollection AddQuartz(this IServiceCollection services)
        {
            services.AddSingleton<IJobFactory, JobFactory>();
            services.AddSingleton<IScheduler>(provider =>
            {
                var schedulerFactory = new StdSchedulerFactory();
                var scheduler = schedulerFactory.GetScheduler().GetAwaiter().GetResult();

                scheduler.JobFactory = provider.GetService<IJobFactory>();

                return scheduler;
            });

            return services;
        }
    }

    public class JobFactory : IJobFactory
    {
        private readonly IServiceProvider container;

        public JobFactory(IServiceProvider container)
        {
            this.container = container;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            var jobType = bundle.JobDetail.JobType;

            return container.GetService(jobType) as IJob;
        }

        public void ReturnJob(IJob job)
        {
        }
    }
}
