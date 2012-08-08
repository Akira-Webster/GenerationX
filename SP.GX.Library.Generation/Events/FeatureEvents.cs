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
    public class FeatureEvents<T> : Microsoft.SharePoint.SPFeatureReceiver where T : IFeatureProvisioner, new()
    {
        private T Provisioner { get; set; }

        public FeatureEvents()
        {
            this.Provisioner = new T();
        }

        public override void FeatureActivated(Microsoft.SharePoint.SPFeatureReceiverProperties properties)
        {
            try
            {
                this.Provisioner.ExecuteRulesForActivated(properties);
            }
            catch (Exception ex)
            {
                this.Provisioner.Log(ex);
            }
        }

        public override void FeatureDeactivating(Microsoft.SharePoint.SPFeatureReceiverProperties properties)
        {
            try
            {
                this.Provisioner.ExecuteRulesForDeactivating(properties);
            }
            catch (Exception ex)
            {
                this.Provisioner.Log(ex);
            }
        }

        public override void FeatureInstalled(Microsoft.SharePoint.SPFeatureReceiverProperties properties)
        {
            try
            {
                this.Provisioner.ExecuteRulesForInstalled(properties);
            }
            catch (Exception ex)
            {
                this.Provisioner.Log(ex);
            }
        }

        public override void FeatureUninstalling(Microsoft.SharePoint.SPFeatureReceiverProperties properties)
        {
            try
            {
                this.Provisioner.ExecuteRulesForUninstalling(properties);
            }
            catch (Exception ex)
            {
                this.Provisioner.Log(ex);
            }
        }

        public override void FeatureUpgrading(Microsoft.SharePoint.SPFeatureReceiverProperties properties, string upgradeActionName, IDictionary<string, string> parameters)
        {
            try
            {
                this.Provisioner.ExecuteRulesForUpgrading(properties, upgradeActionName, parameters);
            }
            catch (Exception ex)
            {
                this.Provisioner.Log(ex);
            }
        }
    }
}
