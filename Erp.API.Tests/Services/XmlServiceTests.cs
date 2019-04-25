using Erp.API.Models;
using Erp.API.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Erp.API.Tests.Services
{
	[TestClass]
	[DeploymentItem(@"Resources\database.xml", @"Resources\database.xml")]
	public class XmlServiceTests
	{
		private const string filePath = @"Resources\database.xml";
		private const string filePathTmp = @"Resources\databasetmp.xml";

		[TestMethod]
		public void LoadXml_Test()
		{
			var target = new XmlService();

			var result = target.LoadXml(filePath);

			Assert.IsNotNull(result);
			Assert.AreEqual(3, result.Count);
		}

		[TestMethod]
		public void SaveXml_Test()
		{
			var list = new List<EmployeeModel>
			{
				new EmployeeModel
				{
					Birth = DateTime.Now, FirstName = "x", Id = 1, LastName = "x", Salary = 10, TaxNumber = 10, WorkingPosition = "dev"
				}
			};

			var target = new XmlService();

			target.SaveXml(filePathTmp, list);

			Assert.IsTrue(File.Exists(filePathTmp));
		}

		[TestInitialize]
		public void Init()
		{
			Assert.IsTrue(File.Exists(filePath));
		}
	}
}
