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
using Microsoft.Practices.ServiceLocation;

namespace SP.GX.Library.Generation.Provisioners
{
    public class EntityProvisioner: CommonProvisioner<SPEventReceiverType, FeatureProvisioner>, IEntityProvisioner
    {
        public Microsoft.SharePoint.SPItemEventProperties Properties { get; private set; }

        public override Microsoft.Practices.ServiceLocation.IServiceLocator ServiceLocator
        {
            get
            {
                if (this.Properties == null)
                    return null;
                else if (this.Properties.Web == null)
                    return null;
                else
                    return SharePointServiceLocator.GetCurrent(this.Properties.Web.Site);
            }
        }

        public EntityProvisioner()
        {
            this.Rules.Add(SPEventReceiverType.ItemAdded, new List<BaseRule<FeatureProvisioner>>());
            this.Rules.Add(SPEventReceiverType.ItemAdding, new List<BaseRule<FeatureProvisioner>>());
            this.Rules.Add(SPEventReceiverType.ItemAttachmentAdded, new List<BaseRule<FeatureProvisioner>>());
            this.Rules.Add(SPEventReceiverType.ItemAttachmentAdding, new List<BaseRule<FeatureProvisioner>>());
            this.Rules.Add(SPEventReceiverType.ItemAttachmentDeleted, new List<BaseRule<FeatureProvisioner>>());
            this.Rules.Add(SPEventReceiverType.ItemAttachmentDeleting, new List<BaseRule<FeatureProvisioner>>());
            this.Rules.Add(SPEventReceiverType.ItemCheckedIn, new List<BaseRule<FeatureProvisioner>>());
            this.Rules.Add(SPEventReceiverType.ItemCheckedOut, new List<BaseRule<FeatureProvisioner>>());
            this.Rules.Add(SPEventReceiverType.ItemCheckingIn, new List<BaseRule<FeatureProvisioner>>());
            this.Rules.Add(SPEventReceiverType.ItemCheckingOut, new List<BaseRule<FeatureProvisioner>>());
            this.Rules.Add(SPEventReceiverType.ItemDeleted, new List<BaseRule<FeatureProvisioner>>());
            this.Rules.Add(SPEventReceiverType.ItemDeleting, new List<BaseRule<FeatureProvisioner>>());
            this.Rules.Add(SPEventReceiverType.ItemFileConverted, new List<BaseRule<FeatureProvisioner>>());
            this.Rules.Add(SPEventReceiverType.ItemFileMoved, new List<BaseRule<FeatureProvisioner>>());
            this.Rules.Add(SPEventReceiverType.ItemFileMoving, new List<BaseRule<FeatureProvisioner>>());
            this.Rules.Add(SPEventReceiverType.ItemUncheckedOut, new List<BaseRule<FeatureProvisioner>>());
            this.Rules.Add(SPEventReceiverType.ItemUncheckingOut, new List<BaseRule<FeatureProvisioner>>());
            this.Rules.Add(SPEventReceiverType.ItemUpdated, new List<BaseRule<FeatureProvisioner>>());
            this.Rules.Add(SPEventReceiverType.ItemUpdating, new List<BaseRule<FeatureProvisioner>>());
            //this.Rules.Add(SPEventReceiverType., new List<BaseRule<FeatureProvisioner>>());   
        }

        public void ExecuteRules(SPItemEventProperties properties)
        {
            this.Properties = properties;
            this.ExecuteRules(properties.EventType);
        }

        public void Cancel(SPEventReceiverStatus status, string message)
        {
            this.Properties.Cancel = true;
            this.Properties.Status = SPEventReceiverStatus.CancelWithError;
            this.Properties.ErrorMessage = message;
        }

        public R GetRepository<R>() where R : class, IInitializeRepository
        {
            IServiceLocator serviceLocator = this.ServiceLocator;
            if (serviceLocator == null)
                return null;
            else 
                return serviceLocator.TryResolve<R, IInitializeRepository>(r => r.Initialize(this.Properties.Web));
        }
    }
}
