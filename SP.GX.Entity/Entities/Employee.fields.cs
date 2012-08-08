using System;
using System.Collections.Generic;
using System.Text;
// SharePoint 14
using Microsoft.SharePoint;
// SPGenesis Framework http://spgenesis.codeplex.com/
using SPGenesis.Core;

namespace SP.GX.Entities.Employees
{
	[SPGENField(ID = "{9cc801eb-25e9-4253-9af5-c9119bc56576}", Type = SPFieldType.Text, Group = "SP.GX.Entities" )]
	public partial class FirstNameField : SPGENField<FirstNameField, SPFieldText, String>
	{
	}

	[SPGENField(ID = "{a315d21a-6305-4db5-9e14-8893698dd3d2}", Type = SPFieldType.Text, Group = "SP.GX.Entities" )]
	public partial class LastNameField : SPGENField<LastNameField, SPFieldText, String>
	{
	}

	[SPGENField(ID = "{23091343-57ae-4c67-abe3-73a26bbcdb04}", Type = SPFieldType.DateTime, Group = "SP.GX.Entities" )]
	public partial class BrithDateField : SPGENField<BrithDateField, SPFieldDateTime, DateTime>
	{
	}

}

