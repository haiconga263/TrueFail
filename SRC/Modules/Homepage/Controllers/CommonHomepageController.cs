using Common;
using Homepage.Commands.FaqCommand;
using Homepage.UI.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Attributes;
using Web.Controllers;
using Web.Controls;

namespace Homepage.Controllers
{
	[Route("api/[controller]")]
	[EnableCors("AllowSpecificOrigin")]
	public class CommonHomepageController : BaseController
	{
		private readonly IMediator mediator = null;
		private readonly ICommonHomepageQueries commonHomepageQueries = null;
		private readonly IHostingEnvironment _hostingEnvironment;
		public CommonHomepageController(IMediator mediator, ICommonHomepageQueries commonHomepageQueries, IHostingEnvironment environment)
		{
			this.mediator = mediator;
			this.commonHomepageQueries = commonHomepageQueries;
			this._hostingEnvironment = environment;
		}

		[HttpGet]
		[Route("get/all-directories")]
		[AuthorizeUser()]
		public APIResult GetAllDirectories()
		{

			var rs = commonHomepageQueries.GetAllDirectories();
			return new APIResult()
			{
				Result = 0,
				Data = rs
			};
		}
		//[HttpPost]
		//public async Task<IActionResult> Upload(IList<IFormFile> files)
		//{
		//	var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
		//	foreach (var file in files)
		//	{
		//		if (file.Length > 0)
		//		{
		//			var filePath = Path.Combine(uploads, file.FileName);
		//			using (var fileStream = new FileStream(filePath, FileMode.Create))
		//			{
		//				await file.CopyToAsync(fileStream);
		//			}
		//		}
		//	}
		//	return View();
		//}

		public class InfoItem
		{
			public Guid Id { get; set; }
			public string Name { get; set; }
			public Guid? ParentId { get; set; }
			public string Path { get; set; }
			public bool IsDir { get; set; }
			public DateTime CreateDate { get; set; }
			public DateTime ModifiedDate { get; set; }
		}

		private List<InfoItem> GetItem(string path, Guid? id)
		{
			List<InfoItem> rs = new List<InfoItem>();
			try
			{
				var dir = new DirectoryInfo(path);
				rs = dir.GetFiles()?.Select(x => new InfoItem()
				{
					Id = Guid.NewGuid(),
					Name = x.Name,
					ParentId = id,
					Path = x.FullName,
					IsDir = false,
					CreateDate = x.CreationTime,
					ModifiedDate = x.LastWriteTime
				}).ToList();

				
				List<InfoItem> dirs = dir.GetDirectories()?.Select(x => new InfoItem()
				{
					Id = Guid.NewGuid(),
					Name = x.Name,
					ParentId = id,
					Path = x.FullName,
					IsDir = true,
					CreateDate = x.CreationTime,
					ModifiedDate = x.LastWriteTime
				}).ToList();

				rs = rs.Concat(dirs).ToList();

				foreach (var item in dirs)
				{
					rs = rs.Concat(GetItem(item.Path, item.Id)).ToList();
				}

			}
			catch (Exception) { }

			return rs ?? new List<InfoItem>(); ;
		}

		[HttpGet]
		[Route("get/directories")]
		[AuthorizeUser()]
		public object Get(string parentIds)
		{
			string[] parents = string.IsNullOrEmpty(parentIds) ? new string[] { "" } : parentIds.Split(',');
			var rootPath = _hostingEnvironment.WebRootPath;

			var rs = GetItem(@"D:\Work\Aritnt2\Aritnt\SRC\Applications\HomePage.Web\wwwroot\images", null);


			//string[] parents = string.IsNullOrEmpty(parentIds) ? new string[] { "" } : parentIds.Split(',');
			//var rootPath = _hostingEnvironment.WebRootPath;

			//var childNodes = parents.SelectMany(parentId => {
			//	var parentPath = String.IsNullOrEmpty(parentId) ? rootPath : Path.Combine(rootPath, parentId);
			//	return Directory.EnumerateFileSystemEntries(parentPath);
			//})
			//	.Where(path => Path.GetFullPath(path).StartsWith(rootPath))
			//	.Select(path => {
			//		var fileInfo = new FileInfo(path);
			//		var isDirectory = System.IO.File.GetAttributes(path).HasFlag(FileAttributes.Directory);
			//		var parentId = Path.GetDirectoryName(path.Substring(rootPath.Length + 1));

			//		return new
			//		{
			//			id = Path.Combine(parentId, Path.GetFileName(path)),
			//			parentId = parentId,
			//			name = Path.GetFileName(path),
			//			modifiedDate = fileInfo.LastWriteTime,
			//			createdDate = fileInfo.CreationTime,
			//			size = isDirectory ? (long?)null : fileInfo.Length,
			//			isDirectory = isDirectory,
			//			hasItems = isDirectory && Directory.EnumerateFileSystemEntries(path).Count() > 0
			//		};
			//	})
			//	.Where(i => i.name != "bin" && i.name != "obj" && i.name != "packages" && i.name.Length > 0 && !i.name.StartsWith("."))
			//	.OrderByDescending(i => i.isDirectory)
			//	.ThenBy(i => i.name);

			return new APIResult()
			{
				Result = 0,
				Data = rs
			};
		}


	}
}
