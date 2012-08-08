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
    public enum FeatureReceiverType
    {
        Activated,
        Deactivating,
        Installed,
        Uninstalling,
        Upgrading,
    }

    public class FeatureProvisioner : CommonProvisioner<FeatureReceiverType, FeatureProvisioner>, IFeatureProvisioner
    {
        public Microsoft.SharePoint.SPFeatureReceiverProperties Properties { get; private set; }

        public override Microsoft.Practices.ServiceLocation.IServiceLocator ServiceLocator
        {
            get
            {
                SPSite site = null;
                SPWeb web = null;

                if (this.Properties == null)
                    return null;
                if (this.Properties.TryCastToSiteCollection(out site))
                    return SharePointServiceLocator.GetCurrent(site);
                else if (this.Properties.TryCastToSite(out web))
                    return SharePointServiceLocator.GetCurrent(web.Site);
                else
                    return null;
            }
        }

        public virtual SPSite SiteCollection 
        {
            get
            {
                SPSite site = null;
                SPWeb web = null;

                if (this.Properties == null)
                    return null;
                if (this.Properties.TryCastToSiteCollection(out site))
                    return site;
                else if (this.Properties.TryCastToSite(out web))
                    return web.Site;
                else
                    return null;
            }
        }

        public virtual SPWeb Web 
        {
            get
            {
                SPSite site = null;
                SPWeb web = null;

                if (this.Properties == null)
                    return null;
                if (this.Properties.TryCastToSiteCollection(out site))
                    return site.RootWeb;
                else if (this.Properties.TryCastToSite(out web))
                    return web;
                else
                    return null;
            }
        }

        public FeatureProvisioner()
        {
            this.Rules.Add(FeatureReceiverType.Activated, new List<BaseRule<FeatureProvisioner>>());
            this.Rules.Add(FeatureReceiverType.Deactivating, new List<BaseRule<FeatureProvisioner>>());
            this.Rules.Add(FeatureReceiverType.Installed, new List<BaseRule<FeatureProvisioner>>());
            this.Rules.Add(FeatureReceiverType.Uninstalling, new List<BaseRule<FeatureProvisioner>>());
            this.Rules.Add(FeatureReceiverType.Upgrading, new List<BaseRule<FeatureProvisioner>>());
        }

        public void ExecuteRulesForActivated(Microsoft.SharePoint.SPFeatureReceiverProperties properties)
        {
            this.Properties = properties;
            this.ExecuteRules(FeatureReceiverType.Activated);
        }

        public void ExecuteRulesForDeactivating(Microsoft.SharePoint.SPFeatureReceiverProperties properties)
        {
            this.Properties = properties;
            this.ExecuteRules(FeatureReceiverType.Deactivating);
        }

        public void ExecuteRulesForInstalled(Microsoft.SharePoint.SPFeatureReceiverProperties properties)
        {
            this.Properties = properties;
            this.ExecuteRules(FeatureReceiverType.Installed);
        }

        public void ExecuteRulesForUninstalling(Microsoft.SharePoint.SPFeatureReceiverProperties properties)
        {
            this.Properties = properties;
            this.ExecuteRules(FeatureReceiverType.Uninstalling);
        }

        public void ExecuteRulesForUpgrading(Microsoft.SharePoint.SPFeatureReceiverProperties properties, string upgradeActionName, IDictionary<string, string> parameters)
        {
            this.Properties = properties;
            this.ExecuteRules(FeatureReceiverType.Upgrading);
        }
    }
}
