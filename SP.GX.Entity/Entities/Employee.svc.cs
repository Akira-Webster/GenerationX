using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Activation;
// SharePoint 14
using Microsoft.SharePoint;
// SPGenesis Framework http://spgenesis.codeplex.com/
using SPGenesis.Core;
// GenerationX
using SP.GX.Library.Generation.Provisioners;
using SP.GX.Library.Generation.Events;

namespace SP.GX.Entities.Employees
{
	[ServiceContract(Namespace = "")]
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
	public class EmployeeService
	{
		[OperationContract]
		[WebInvoke(Method = "GET",BodyStyle = WebMessageBodyStyle.Bare,ResponseFormat = WebMessageFormat.Json)]
		public string Hello()
		{
			return string.Format("Hello from {0}", this);
		}

	}

}

