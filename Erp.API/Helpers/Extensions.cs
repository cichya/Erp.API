using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.API.Helpers
{
	public static class Extensions
	{
		public static int CalculateAge(this DateTime birth)
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
