using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// SharePoint
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
// ServiceLocation
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.SharePoint.Common.ServiceLocation;
using Microsoft.Practices.SharePoint.Common.Logging;
// GenerationX
using SP.GX.Library.Generation.Interfaces;
// SharePoint Extensions
using SharePoint.Contrib.Extensions;

namespace SP.GX.Library.Generation.Events
{
    public class EmailEvents<T> : Microsoft.SharePoint.SPEmailEventReceiver where T : IEmailProvisioner, new()
    {
        private T Provisioner { get; set; }

        public EmailEvents()
        {
            this.Provisioner = new T();

        }
        public override void EmailReceived(Microsoft.SharePoint.SPList list, Microsoft.SharePoint.Utilities.SPEmailMessage emailMessage, string receiverData)
        {
            try
            {
                this.Provisioner.ExecuteRulesForEmail(list, emailMessage, receiverData);
            }
            catch (Exception ex)
            {
                this.Provisioner.Log(ex);
            }
        }
    }
}
