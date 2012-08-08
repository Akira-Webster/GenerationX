using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using SPGenesis.Core;

namespace SP.GX.Library.Generation
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class EntityAttribute : Attribute
    {
        public string ContentTypeId { get; set; }
        public string Description { get; set; }

        //public string ListDescription { get; set; }
        //public string TypeDescription { get; set; }

        //public string TemplateFeatureId { get; set; }
        //public int TemplateType { get; set; } //TemplateType = (int)SPListTemplateType.GenericList

        //public SPGENListInstanceGetMethod GetListDefaultMethod { get; set; }
        //public SPGENProvisionEventCallBehavior ProvisionEventCallBehavior { get; set; }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class EntityIdentityAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class EntityFieldAttribute : Attribute
    {
        public string ID { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        //public string InternalName { get; set; }
        //public string StaticName { get; set; }

        public bool Required { get; set; }
        public bool Indexed { get; set; }
        public bool NoCrawl { get; set; }
        public bool Filterable { get; set; }
        public bool Sortable { get; set; }
        public bool EnforceUniqueValues { get; set; }

        public SPFieldType Type { get; set; }

        /*
        public bool AllowMultipleValues { get; set; }
        public string AggregationFunction { get; set; }
        public string BdcField { get; set; }
        public string BdcEntityName { get; set; }
        public string BdcEntityNamespace { get; set; }
        public string BdcSystemInstance { get; set; }
        public SPCalendarType CalendarType { get; set; }
        public SPDateTimeFieldFormatType DateFormat { get; set; }
        public string DefaultFormula { get; set; }
        public string DefaultValue { get; set; }
        public string Description { get; set; }
        public int DifferencingLimit { get; set; }
        public string Direction { get; set; }
        public SPNumberFormatTypes DisplayFormat { get; set; }
        public SPUrlFieldFormatType DisplayUrlFormat { get; set; }
        public string DisplaySize { get; set; }
        public SPChoiceFormatType EditFormat { get; set; }
        public bool EnforceUniqueValues { get; set; }
        public bool FillInChoice { get; set; 
        public string Formula { get; set; }
        public bool Hidden { get; set; }
        public string JumpToField { get; set; }
        public double MaximumValue { get; set; }
        public int MaxLength { get; set; }
        public double MinimumValue { get; set; }
        public int NumberOfLines { get; set; }
        public SPFieldType OutputType { get; set; }
        public bool Presence { get; set; }
        public SPPreviewValueSize PreviewValueSize { get; set; }
        public bool ReadOnlyField { get; set; }
        public string RelatedField { get; set; }
        public string RelatedFieldWssStaticName { get; set; }
        public SPRelationshipDeleteBehavior RelationshipDeleteBehavior { get; set; }
        public bool RestrictedMode { get; set; }
        public bool RichText { get; set; }
        public SPRichTextMode RichTextMode { get; set; }
        public bool Sealed { get; set; }
        public int SelectionGroup { get; set; }
        public SPFieldUserSelectionMode SelectionMode { get; set; }
        public bool ShowAsPercentage { get; set; }
        public bool ShowInDisplayForm { get; set; }
        public bool ShowInEditForm { get; set; }
        public bool ShowInListSettings { get; set; }
        public bool ShowInNewForm { get; set; }
        public bool ShowInVersionHistory { get; set; }
        public bool ShowInViewForms { get; set; }
        public string ValidationFormula { get; set; }
        public string ValidationMessage { get; set; }
        public bool UnlimitedLengthInDocumentLibrary { get; set; }
         * 
        //public SPFieldType Type { get; set; }
        //public string CustomType { get; set; }
        public object LookupList { get; set; }
        public object LookupField { get; set; }
        public object LookupFieldRef { get; set; }

        public SPGENProvisionEventCallBehavior ProvisionEventCallBehavior { get; set; }
        */
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class EntityFieldBooleanAttribute : EntityFieldAttribute
    {
        public EntityFieldBooleanAttribute()
        {
            this.Type = SPFieldType.Boolean;
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class EntityFieldDateTimeAttribute : EntityFieldAttribute
    {
        public SPDateTimeFieldFormatType DateFormat { get; set; }

        public EntityFieldDateTimeAttribute()
        {
            this.Type = SPFieldType.DateTime;
            this.DateFormat = SPDateTimeFieldFormatType.DateTime;
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class EntityFieldGuidAttribute : EntityFieldAttribute
    {
        public EntityFieldGuidAttribute()
        {
            this.Type = SPFieldType.Guid;
        }
    }

    /// <see cref="http://msdn.microsoft.com/en-us/library/microsoft.sharepoint.spfieldlookup.aspx"/>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class EntityFieldLookupAttribute : EntityFieldAttribute
    {
        public object LookupList { get; set; }
        public object LookupField { get; set; }

        /// <summary>
        /// Cascade - Deleting an item from the lookup list causes all related items to be deleted from the list that contains the lookup field.
        /// Restrict - Prevents an item on the lookup list from being deleted if related items exist on the list that contains the lookup field.
        /// You cannot set a deletion constraint on a lookup field that allows multiple values, nor is it possible if the lookup list is in another Web site.
        /// </summary>
        public SPRelationshipDeleteBehavior RelationshipDeleteBehavior { get; set; }

        public EntityFieldLookupAttribute()
        {
            this.Type = SPFieldType.Lookup;
            this.RelationshipDeleteBehavior = SPRelationshipDeleteBehavior.None;
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class EntityFieldChoiceAttribute : EntityFieldAttribute
    {
        public bool FillInChoice { get; set; }
        public SPChoiceFormatType EditFormat { get; set; }

        public EntityFieldChoiceAttribute(bool multiple)
        {
            this.Type = (multiple) ? SPFieldType.MultiChoice : SPFieldType.Choice;
            this.EditFormat = SPChoiceFormatType.RadioButtons;
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class EntityFieldTextAttribute : EntityFieldAttribute
    {
        public int MaxLength { get; set; }

        public EntityFieldTextAttribute()
        {
            this.Type = SPFieldType.Text;
            this.MaxLength = 255;
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class EntityFieldNoteAttribute : EntityFieldAttribute
    {
        public int MaxLength { get; set; }
        public bool RichText { get; set; }
        public SPRichTextMode RichTextMode { get; set; }

        public EntityFieldNoteAttribute()
        {
            this.Type = SPFieldType.Note;
            this.RichText = true;
            this.RichTextMode = SPRichTextMode.FullHtml;
            this.MaxLength = int.MaxValue;
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class EntityFieldNumberAttribute : EntityFieldAttribute
    {
        public bool ShowAsPercentage { get; set; }
        public SPNumberFormatTypes DisplayFormat { get; set; }
        public double MaximumValue { get; set; }
        public double MinimumValue { get; set; }

        public EntityFieldNumberAttribute()
        {
            this.Type = SPFieldType.Number;
            this.ShowAsPercentage = false;
            this.DisplayFormat = SPNumberFormatTypes.TwoDecimals;
            this.MinimumValue = double.MinValue;
            this.MinimumValue = double.MaxValue;
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class EntityFieldUrlAttribute : EntityFieldAttribute
    {
        public SPUrlFieldFormatType DisplayUrlFormat { get; set; }

        public EntityFieldUrlAttribute()
        {
            this.Type = SPFieldType.URL;
            this.DisplayUrlFormat = SPUrlFieldFormatType.Hyperlink;
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class EntityFieldPrincipalAttribute : EntityFieldAttribute
    {
        public SPFieldUserSelectionMode SelectionMode { get; set; }
        public bool Presence { get; set; }
        public bool AllowMultipleValues { get; set; }
        public int SelectionGroup { get; set; }

        public EntityFieldPrincipalAttribute()
        {
            this.Type = SPFieldType.User;
            this.SelectionMode = SPFieldUserSelectionMode.PeopleAndGroups;
            this.Presence = false;
            this.AllowMultipleValues = true;
            this.SelectionGroup = 0;
        }
    }
}
