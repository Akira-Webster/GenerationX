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
    public class ListItemEvents<T> : Microsoft.SharePoint.SPItemEventReceiver where T : IEntityProvisioner, new()
    {
        private T Provisioner { get; set; }

        public ListItemEvents()
        {
            this.Provisioner = new T();
        }

        public override void ItemAdded(Microsoft.SharePoint.SPItemEventProperties properties)
        {
            try
            {
                this.Provisioner.ExecuteRules(properties);

                base.ItemAdded(properties);
            }
            catch (Exception ex)
            {
                this.Provisioner.Log(ex);
            }
        }

        public override void ItemAdding(Microsoft.SharePoint.SPItemEventProperties properties)
        {
            try
            {
                base.ItemAdding(properties);

                this.Provisioner.ExecuteRules(properties);
            }
            catch (Exception ex)
            {
                this.Provisioner.Cancel(SPEventReceiverStatus.CancelWithError, ex.Message);
                this.Provisioner.Log(ex);
            }
        }

        public override void ItemAttachmentAdded(Microsoft.SharePoint.SPItemEventProperties properties)
        {
            try
            {
                this.Provisioner.ExecuteRules(properties);

                base.ItemAttachmentAdded(properties);
            }
            catch (Exception ex)
            {
                this.Provisioner.Log(ex);
            }
        }

        public override void ItemAttachmentAdding(Microsoft.SharePoint.SPItemEventProperties properties)
        {
            try
            {
                base.ItemAttachmentAdding(properties);

                this.Provisioner.ExecuteRules(properties);
            }
            catch (Exception ex)
            {
                this.Provisioner.Cancel(SPEventReceiverStatus.CancelWithError, ex.Message);
                this.Provisioner.Log(ex);
            }
        }

        public override void ItemAttachmentDeleted(Microsoft.SharePoint.SPItemEventProperties properties)
        {
            try
            {
                this.Provisioner.ExecuteRules(properties);

                base.ItemAttachmentDeleted(properties);
            }
            catch (Exception ex)
            {
                this.Provisioner.Log(ex);
            }
        }

        public override void ItemAttachmentDeleting(Microsoft.SharePoint.SPItemEventProperties properties)
        {
            try
            {
                base.ItemAttachmentDeleting(properties);

                this.Provisioner.ExecuteRules(properties);
            }
            catch (Exception ex)
            {
                this.Provisioner.Cancel(SPEventReceiverStatus.CancelWithError, ex.Message);
                this.Provisioner.Log(ex);
            }
        }

        public override void ItemCheckedIn(Microsoft.SharePoint.SPItemEventProperties properties)
        {
            //base.ItemCheckedIn(properties);
        }

        public override void ItemCheckedOut(Microsoft.SharePoint.SPItemEventProperties properties)
        {
            //base.ItemCheckedOut(properties);
        }

        public override void ItemCheckingIn(Microsoft.SharePoint.SPItemEventProperties properties)
        {
            //base.ItemCheckingIn(properties);
        }

        public override void ItemCheckingOut(Microsoft.SharePoint.SPItemEventProperties properties)
        {
            //base.ItemCheckingOut(properties);
        }

        public override void ItemDeleted(Microsoft.SharePoint.SPItemEventProperties properties)
        {
            try
            {
                this.Provisioner.ExecuteRules(properties);

                base.ItemDeleted(properties);
            }
            catch (Exception ex)
            {
                this.Provisioner.Log(ex);
            }
        }

        public override void ItemDeleting(Microsoft.SharePoint.SPItemEventProperties properties)
        {
            try
            {
                base.ItemDeleting(properties);

                this.Provisioner.ExecuteRules(properties);
            }
            catch (Exception ex)
            {
                this.Provisioner.Cancel(SPEventReceiverStatus.CancelWithError, ex.Message);
                this.Provisioner.Log(ex);
            }
        }

        public override void ItemFileConverted(Microsoft.SharePoint.SPItemEventProperties properties)
        {
            //base.ItemFileConverted(properties);
        }

        public override void ItemFileMoved(Microsoft.SharePoint.SPItemEventProperties properties)
        {
            //base.ItemFileMoved(properties);
        }

        public override void ItemFileMoving(Microsoft.SharePoint.SPItemEventProperties properties)
        {
            //base.ItemFileMoving(properties);
        }

        public override void ItemUncheckedOut(Microsoft.SharePoint.SPItemEventProperties properties)
        {
            //base.ItemUncheckedOut(properties);
        }

        public override void ItemUncheckingOut(Microsoft.SharePoint.SPItemEventProperties properties)
        {
            //base.ItemUncheckingOut(properties);
        }

        public override void ItemUpdated(Microsoft.SharePoint.SPItemEventProperties properties)
        {
            try
            {
                this.Provisioner.ExecuteRules(properties);

                base.ItemUpdated(properties);
            }
            catch (Exception ex)
            {
                this.Provisioner.Log(ex);
            }
        }

        public override void ItemUpdating(Microsoft.SharePoint.SPItemEventProperties properties)
        {
            try
            {
                base.ItemUpdating(properties);

                this.Provisioner.ExecuteRules(properties);
            }
            catch (Exception ex)
            {
                this.Provisioner.Cancel(SPEventReceiverStatus.CancelWithError, ex.Message);
                this.Provisioner.Log(ex);
            }
        }
    }
}
