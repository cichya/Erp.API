using Erp.API.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.API.Tests
{
	[TestClass]
	public class ExtensionsTests
	{
		[TestMethod]
		public void CalculateAge_Equal_3_Years_Return_2_Test()
		{
			DateTime birth = DateTime.Now.AddYears(-3);

			int result = birth.CalculateAge();

			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public void CalculateAge_More_Than_3_Years_Return_3_Test()
		{
			DateTime birth = DateTime.Now.AddYears(-3).AddDays(-1);

			int result = birth.CalculateAge();

			Assert.AreEqual(3, result);
		}

		private int CalculateAge(DateTime birth)
		{
			var age = DateTime.Today.Year - birth.Year;

			if (birth.AddYears(age) > DateTime.Today)
			{
				age--;
			}

			return age;
		}
	}
}
