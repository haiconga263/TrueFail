using Common;
using Common.Helpers;
using Common.Models;
using Homepage.UI.Interfaces;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homepage.Queries
{
	public class CommonHomepageQueries : BaseQueries, ICommonHomepageQueries
	{
		public List<string> GetAllDirectories()
		{
			var path = GlobalConfiguration.HomepageImagePath;
			var searchPattern = "*";
			SearchOption searchOption = SearchOption.AllDirectories;


			if (searchOption == SearchOption.TopDirectoryOnly)
				return Directory.GetDirectories(path, searchPattern).ToList();

			var directories = new List<string>(GetDirectories(path, searchPattern));

			for (var i = 0; i < directories.Count; i++)
				directories.AddRange(GetDirectories(directories[i], searchPattern));

			return directories;
		}

		private static List<string> GetDirectories(string path, string searchPattern)
		{
			try
			{
				return Directory.GetDirectories(path, searchPattern).ToList();
			}
			catch (UnauthorizedAccessException)
			{
				return new List<string>();
			}
		}
	}
}
