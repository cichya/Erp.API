using AutoMapper;
using Erp.API.DTOs;
using Erp.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.API.Helpers
{
	public class AutoMapperHelper : Profile
	{
		public AutoMapperHelper()
		{
			this.CreateMap<EmployeeModel, EmployeeForListDto>()
				.ForMember(member => member.Age, memberOpt =>
				{
					memberOpt.ResolveUsing(value => value.Birth.CalculateAge());
				});
		}
	}
}
