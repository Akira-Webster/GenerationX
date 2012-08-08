using System;
using System.Collections.Generic;
using System.Text;
// SharePoint 14
using Microsoft.SharePoint;
// SPGenesis Framework http://spgenesis.codeplex.com/
using SPGenesis.Core;
using SPGenesis.Entities;

namespace SP.GX.Entities.Employees
{
	public class EmployeeMapper : SPGENEntityMap<Employee>
	{
		protected override void InitializeMapper()
		{
			this.RegisterIdentifierProperty(entity => entity.EmployeeID);
			this.MapField(entity => entity.FirstName, FirstNameField.InternalName);
			this.MapField(entity => entity.LastName, LastNameField.InternalName);
			this.MapField(entity => entity.BrithDate, BrithDateField.InternalName);
		}

	}

}

