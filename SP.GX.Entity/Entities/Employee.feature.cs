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
	public partial class EmployeeFeatureProvisioner : FeatureProvisioner
	{
		public EmployeeFeatureProvisioner()
		{
			this.AddRule(FeatureReceiverType.Activated, Provision);
		}

		private void Provision()
		{
			if(this.Web == null) throw new ArgumentNullException("SPWeb is not accessible in this scope.");

			FirstNameField.Instance.Provision(this.Web);
			LastNameField.Instance.Provision(this.Web);
			BrithDateField.Instance.Provision(this.Web);

			EmployeeType.Instance.Provision(this.Web);
			EmployeeList.Instance.Provision(this.Web);
		}

	}

	public class EmployeeFeatureEvents : FeatureEvents<EmployeeFeatureProvisioner>
	{
	}

}

