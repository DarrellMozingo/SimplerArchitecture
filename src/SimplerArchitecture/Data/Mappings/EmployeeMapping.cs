using FluentNHibernate.Mapping;
using SimplerArchitecture.Domain;

namespace SimplerArchitecture.Data.Mappings
{
	public class EmployeeMapping : ClassMap<Employee>
	{
		public EmployeeMapping()
		{
			Id(x => x.Id);
			Map(x => x.FirstName);
			Map(x => x.LastName);
		}
	}
}