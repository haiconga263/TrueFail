using Common.Helpers;
using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abivin.Integration.Services
{
    public class MDMService : IBaseService
    {
        public void Run()
        {
            Console.WriteLine("MDM Service is running");

            try
            {

                //todo

                Console.WriteLine("MDM Service is completed");
            }
            catch(Exception ex)
            {
                LogHelper.GetLogger().Error(string.Format("{0} - {1}", ex.StackTrace, ex.Message));
                Console.WriteLine("MDM Service is run with error");
            }
        }
    }
}
