using System;
using System.Collections.Generic;
using System.Text;
// SharePoint 14
using Microsoft.SharePoint;
// SPGenesis Framework http://spgenesis.codeplex.com/
using SPGenesis.Core;
// GenerationX
using SP.GX.Library.Generation.Provisioners;
using SP.GX.Library.Generation.Events;

namespace SP.GX.Entities.Employees
{
	public partial class EmployeeEntityProvisioner : EntityProvisioner
	{
	}

	public class EmployeeEntityEvents : ListItemEvents<EmployeeEntityProvisioner>
	{
	}

}

