using Abivin.Integration.Services;
using Hangfire;
using Hangfire.MySql;
using Hangfire.Storage;
using System;
using System.Transactions;

namespace Abivin.Integration.Extentions
{
    public class AbivinBuilder
    {
        public static void BuildHangFire(string conStr)
        {
            GlobalConfiguration.Configuration
                .UseColouredConsoleLogProvider()
                .UseStorage(new MySqlStorage(conStr,
                                      new MySqlStorageOptions
                                      {
                                          TransactionIsolationLevel = IsolationLevel.ReadCommitted,
                                          QueuePollInterval = TimeSpan.FromSeconds(15),
                                          JobExpirationCheckInterval = TimeSpan.FromHours(1),
                                          CountersAggregateInterval = TimeSpan.FromMinutes(5),
                                          PrepareSchemaIfNecessary = true,
                                          DashboardJobListLimit = 50000,
                                          TransactionTimeout = TimeSpan.FromMinutes(1),
                                          TablesPrefix = "aritnt_hangfire_"
                                      }
                                     ));
        }

        public static void Build()
        {
            using (var connection = JobStorage.Current.GetConnection())
            {
                foreach (var recurringJob in connection.GetRecurringJobs())
                {
                    RecurringJob.RemoveIfExists(recurringJob.Id);
                }
            }

            RecurringJob.AddOrUpdate<OrderService>("OrderJob", s => s.Run(), Cron.Daily(17, 9), TimeZoneInfo.Local);
            RecurringJob.AddOrUpdate<MDMService>("MDMJob", s => s.Run(), Cron.Daily(17, 9), TimeZoneInfo.Local);
        }

        public static void HangService()
        {
            Console.WriteLine("Hangfire Server started.");
            
            while(true)
            {
                var cmt = Console.ReadLine();

                if("exit".Equals(cmt.ToLower()))
                {
                    Console.WriteLine("Application is stopping");
                    return;
                }

                Console.WriteLine(ServiceExtention.GetContent(cmt.ToLower()));
            }
        }
    }
}
