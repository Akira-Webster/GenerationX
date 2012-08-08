using System;
using System.Collections.Generic;
using System.Text;
// SharePoint 14
using Microsoft.SharePoint;
// SPGenesis Framework http://spgenesis.codeplex.com/
using SPGenesis.Core;

namespace SP.GX.Entities.Employees
{
	[SPGENListInstance(WebRelURL = "Lists/Employees", Title = "Employees", TemplateType = (int)SPListTemplateType.GenericList)]
	public partial class EmployeeList : SPGENListInstance<EmployeeList>
	{
		partial void Initialize(SPGENListInstanceProperties properties);
		protected override void InitializeDefinition(SPGENListInstanceProperties properties)
		{
			// Initialize
			this.Initialize(properties);
		}

		partial void ProvisionFinalized(SPList list);
		protected override void OnProvisionFinalized(SPList list)
		{
			// Register Entity Provisioner
			list.RegisterEventReceiver<EmployeeEntityEvents>(false);
			// Finalized
			this.ProvisionFinalized(list);
		}

	}

}

