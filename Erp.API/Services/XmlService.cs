using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Erp.API.Models;

namespace Erp.API.Services
{
	public class XmlService : IXmlService
	{
		public IList<EmployeeModel> LoadXml(string filePath)
		{
			var xmlStr = File.ReadAllText(filePath);

			using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(xmlStr)))
			{
				XmlRootAttribute xRoot = new XmlRootAttribute();
				xRoot.ElementName = "ArrayOfEmployee";

				XmlSerializer serializer = new XmlSerializer(typeof(List<EmployeeModel>), xRoot);
				return ((List<EmployeeModel>)serializer.Deserialize(ms));
			}
		}

		public void SaveXml(string filePath, IList<EmployeeModel> employees)
		{
			using (var stringWriter = new StringWriter())
			{
				using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings() { OmitXmlDeclaration = true }))
				{
					XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<EmployeeModel>));

					xmlSerializer.Serialize(xmlWriter, employees);

					File.WriteAllText(filePath, stringWriter.ToString(), Encoding.UTF8);
				}
			}
		}
	}
}
