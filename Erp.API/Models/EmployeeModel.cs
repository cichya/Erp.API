using System;
using System.Xml.Serialization;

namespace Erp.API.Models
{
	[XmlType("employee")]
	public class EmployeeModel
	{
		[XmlElement(ElementName = "id")]
		public int Id { get; set; }

		[XmlElement(ElementName = "firstName")]
		public string FirstName { get; set; }

		[XmlElement(ElementName = "lastName")]
		public string LastName { get; set; }

		[XmlElement(ElementName = "birth")]
		public DateTime Birth { get; set; }

		[XmlElement(ElementName = "workingPosition")]
		public string WorkingPosition { get; set; }

		[XmlElement(ElementName = "salary")]
		public decimal Salary { get; set; }

		[XmlElement(ElementName = "taxNumber")]
		public int TaxNumber { get; set; }
	}
}