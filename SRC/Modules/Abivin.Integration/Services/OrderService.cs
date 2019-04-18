using Common.Helpers;
using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abivin.Integration.Services
{
    public class OrderService : IBaseService
    {
        public void Run()
        {
            Console.WriteLine("Order Service is running");
            try
            {

                //todo

                Console.WriteLine("Order Service is completed");
            }
            catch (Exception ex)
            {
                LogHelper.GetLogger().Error(string.Format("{0} - {1}", ex.StackTrace, ex.Message));
                Console.WriteLine("Order Service is run with error");
            }
        }
    }
}
