using Common.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using System.IO;

namespace Common
{
	public class GlobalConfiguration
	{
		public static string SQLConnectionString { set; get; } = "Server=localhost;Database=aritnt;Uid=root;Pwd=Abcd1234;";
		public static string NoSQLConnectionString { set; get; } = "Data Source=itrecruitment.cselolkuvyrt.us-east-2.rds.amazonaws.com;Initial Catalog=ITRecruitment;Persist Security Info=True;User ID=giang;Password=123456789;Max Pool Size=100000";
		public static string NoSQLDatabase { set; get; } = "Aritnt";
		public static string AuthenURI { set; get; } = "localhost";
		public static string APIGateWayURI { set; get; } = "localhost";
		public static IContainer Container { set; get; }
		public static IList<ModuleInfo> Modules { get; set; }

		public static string Log4netFilePath { set; get; } = "log4net.config";

		public static string ImageDataStoredPath { set; get; } = Directory.GetCurrentDirectory();

		public static string EmployeeImagePath { set; get; } = "/Images/Employee";
		public static string ProductImagePath { set; get; } = "/Images/Product";
		public static string FarmerImagePath { set; get; } = "/Images/Farmer";
		public static string RetailerImagePath { set; get; } = "/Images/Retailer";
		public static string CollectionImagePath { set; get; } = "/Images/Collection";
		public static string FulfillmentImagePath { set; get; } = "/Images/Fulfillment";
		public static string DistributionImagePath { set; get; } = "/Images/Fulfillment";
		public static string VehicleImagePath { set; get; } = "/Images/Vehicle";
		public static string HomepageImagePath { set; get; } = "/Images/Homepage";


		public static int TimeoutOfRetailerOrder { set; get; } = 86400; //1day: from seconds

		public static IntegrationTemplate IntegrationTemplate { set; get; }
		public static string IntegrationConfigFileName { set; get; } = "integration.config.xml";

		private static IConfiguration _configuration;
		public static void Load(IConfiguration _configuration)
		{
			GlobalConfiguration._configuration = _configuration;
			SQLConnectionString = (string)_configuration.GetValue(typeof(string), "SQLConnectionString");
			NoSQLConnectionString = (string)_configuration.GetValue(typeof(string), "NoSQLConnectionString");
			NoSQLDatabase = (string)_configuration.GetValue(typeof(string), "NoSQLDatabase");
			AuthenURI = (string)_configuration.GetValue(typeof(string), "AuthenURI");
			APIGateWayURI = (string)_configuration.GetValue(typeof(string), "APIGateWayURI");
			Log4netFilePath = (string)_configuration.GetValue(typeof(string), "Log4netFilePath");

			ImageDataStoredPath = (string)_configuration.GetValue(typeof(string), "ImageDataStoredPath");
			EmployeeImagePath = (string)_configuration.GetValue(typeof(string), "EmployeeImagePath");
			ProductImagePath = (string)_configuration.GetValue(typeof(string), "ProductImagePath");
			FarmerImagePath = (string)_configuration.GetValue(typeof(string), "FarmerImagePath");
			RetailerImagePath = (string)_configuration.GetValue(typeof(string), "RetailerImagePath");
			CollectionImagePath = (string)_configuration.GetValue(typeof(string), "CollectionImagePath");
			FulfillmentImagePath = (string)_configuration.GetValue(typeof(string), "FulfillmentImagePath");
			DistributionImagePath = (string)_configuration.GetValue(typeof(string), "DistributionImagePath");
			VehicleImagePath = (string)_configuration.GetValue(typeof(string), "VehicleImagePath");
			HomepageImagePath = (string)_configuration.GetValue(typeof(string), "HomepageImagePath");

			if (int.TryParse((string)_configuration.GetValue(typeof(string), "TimeoutOfRetailerOrder"), out int timeoutOfRetailerOrder))
			{
				TimeoutOfRetailerOrder = timeoutOfRetailerOrder;
			}
		}

		public static string GetByKey(string key)
		{
			return (string)_configuration.GetValue(typeof(string), key);
		}
	}
}
