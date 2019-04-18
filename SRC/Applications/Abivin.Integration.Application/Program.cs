using System;
using System.Transactions;
using Abivin.Integration.Extentions;
using Abivin.Integration.Services;
using Hangfire;
using Hangfire.MySql;

namespace Abivin.Integration.Application
{
    class Program
    {
        private const bool IsDebug = true;
        static void Main(string[] args)
        {
            var connStr = IsDebug ? "Server=192.168.1.201;Database=aritnt;Uid=root;Pwd=@Abcd1234;Charset=utf8mb4;" : args[1];

            AbivinBuilder.BuildHangFire(connStr);

            using (var server = new BackgroundJobServer())
            {

                AbivinBuilder.Build();

                AbivinBuilder.HangService();
            }
        }
    }

}
