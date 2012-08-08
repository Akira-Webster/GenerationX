using System;
using System.Collections.Generic;
using System.Text;
// SharePoint 14
using Microsoft.SharePoint;
// SPGenesis Framework http://spgenesis.codeplex.com/
using SPGenesis.Core;

namespace SP.GX.Entities.Employees
{
	[SPGENContentType(ID = "0x0100571ae8b9abd54984954ddbbf3b239fb3", Name = "EmployeeType", Group = "SP.GX.Entities")]
	public partial class EmployeeType : SPGENContentType<EmployeeType>
	{
		partial void Initializing(SPGENContentTypeProperties properties);
		protected override void InitializeDefinition(SPGENContentTypeProperties properties)
		{
			properties.FieldLinks.ProvisioningMode = SPGENProvisioningMode.AddUpdateRemoveExclusive;
			properties.FieldLinksToRemove.Add(SPBuiltInFieldId.Title);
			properties.FieldLinks.Add<FirstNameField>();
			properties.FieldLinks.Add<LastNameField>();
			properties.FieldLinks.Add<BrithDateField>();
			this.Initializing(properties);
		}

		partial void ProvisionFinalized(SPContentType contentType, bool isParentList);
		protected override void OnProvisionFinalized(SPContentType contentType, bool isParentList)
		{
			this.ProvisionFinalized(contentType, isParentList);
		}

	}

}

