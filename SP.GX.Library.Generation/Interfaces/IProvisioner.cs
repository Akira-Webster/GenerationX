using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using Microsoft.SharePoint;
using Microsoft.Practices.ServiceLocation;

namespace SP.GX.Library.Generation.Interfaces
{
    public interface ICommonProvisioner
    {
        IServiceLocator ServiceLocator { get; }
        void Log(Exception ex, int eventID);
        void Log(Exception ex);
        void Log(string message, int eventID);
        void Log(string message);
    }

    public interface IFeatureProvisioner : ICommonProvisioner
    {
        void ExecuteRulesForActivated(Microsoft.SharePoint.SPFeatureReceiverProperties properties);
        void ExecuteRulesForDeactivating(Microsoft.SharePoint.SPFeatureReceiverProperties properties);
        void ExecuteRulesForInstalled(Microsoft.SharePoint.SPFeatureReceiverProperties properties);
        void ExecuteRulesForUninstalling(Microsoft.SharePoint.SPFeatureReceiverProperties properties);
        void ExecuteRulesForUpgrading(Microsoft.SharePoint.SPFeatureReceiverProperties properties, string upgradeActionName, IDictionary<string, string> parameters);

        SPSite SiteCollection { get; }
        SPWeb Web { get; }
    }

    public interface IEmailProvisioner : ICommonProvisioner
    {
        void ExecuteRulesForEmail(Microsoft.SharePoint.SPList list, Microsoft.SharePoint.Utilities.SPEmailMessage emailMessage, string receiverData);
    }

    public interface IEntityProvisioner : ICommonProvisioner
    {
        void ExecuteRules(Microsoft.SharePoint.SPItemEventProperties properties);
        void Cancel(Microsoft.SharePoint.SPEventReceiverStatus status, string message);
    }

}
