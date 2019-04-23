using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
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

		public static void AddApplicationError(this HttpResponse response, string message)
		{
			response.Headers.Add("Application-Error", message);
			response.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
			response.Headers.Add("Access-Control-Allow-Origin", "*");
		}

		public static void AddPagination(this HttpResponse response, int currentPage, int itemsPerPage, int totalItems, int totalPages)
		{
			var paginationHeader = new PaginationHeader(currentPage, itemsPerPage, totalItems, totalPages);

			var cammelCaseFormatter = new JsonSerializerSettings();
			cammelCaseFormatter.ContractResolver = new CamelCasePropertyNamesContractResolver();

			response.Headers.Add("Pagination", JsonConvert.SerializeObject(paginationHeader, cammelCaseFormatter));
			response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
		}
	}
}
