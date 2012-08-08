using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// SharePoint
using Microsoft.SharePoint;
// GenerationX
using SP.GX.Library.Generation.Interfaces;
// ServiceLocation
using Microsoft.Practices.SharePoint.Common.ServiceLocation;
// SharePoint Extensions
using SharePoint.Contrib.Extensions;

namespace SP.GX.Library.Generation.Provisioners
{
    public class EmailProvisioner : CommonProvisioner<SPEventReceiverType, EmailProvisioner>, IEmailProvisioner
    {
        public SPList List { get; set; }
        public Microsoft.SharePoint.Utilities.SPEmailMessage Message { get; set; }
        public string Data { get; set; }

        public EmailProvisioner()
        {
            this.Rules.Add(SPEventReceiverType.EmailReceived, new List<BaseRule<EmailProvisioner>>());
        }

        public override Microsoft.Practices.ServiceLocation.IServiceLocator ServiceLocator
        {
            get
            {
                if (this.List == null)
                    return null;
                else
                    return SharePointServiceLocator.GetCurrent(this.List.ParentWeb.Site);  
            }
        }

        public void ExecuteRulesForEmail(SPList list, Microsoft.SharePoint.Utilities.SPEmailMessage emailMessage, string receiverData)
        {
            this.List = list;
            this.Message = emailMessage;
            this.Data = receiverData;
            this.ExecuteRules(SPEventReceiverType.EmailReceived);
        }
    }
}
