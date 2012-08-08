using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// VisualStudio Intergration
using EnvDTE;
using EnvDTE80;
// T4 Toolbox
using T4Toolbox;
// SP.GX
using SP.GX.Library.Generation;
using SP.GX.Library.T4.Generators;
using SP.GX.Library.T4.Utilities;

namespace SP.GX.Library.T4.Templates
{
    public class FieldTemplate : Template, ITemplate
    {
        public EntityGenerator Generator { get; set; }
        public FileCodeModel2 Model { get; set; }

        public override string TransformText()
        {
            this.Output.File = System.IO.Path.GetFileNameWithoutExtension(this.Model.Parent.Name) + ".fields.cs";

            var typeMappings = new Dictionary<string, string>();
            typeMappings.Add("Boolean", "SPFieldType.Boolean");
            typeMappings.Add("Decimal", "SPFieldType.Currency");
            typeMappings.Add("DateTime", "SPFieldType.DateTime");
            typeMappings.Add("Guid", "SPFieldType.Guid");
            typeMappings.Add("Int32", "SPFieldType.Integer");
            //typeMappings.Add("String", "SPFieldType.Note");
            typeMappings.Add("Double", "SPFieldType.Number");
            typeMappings.Add("SPFieldLookupValue", "SPFieldType.Lookup");
            typeMappings.Add("String", "SPFieldType.Text");
            typeMappings.Add("SPFieldUrlValue", "SPFieldType.URL");
            typeMappings.Add("SPFieldUserValue", "SPFieldType.User");

            var fieldMappings = new Dictionary<string, string>();
            fieldMappings.Add("Boolean", "SPFieldBoolean");
            fieldMappings.Add("Decimal", "SPFieldCurrency");
            fieldMappings.Add("DateTime", "SPFieldDateTime");
            fieldMappings.Add("Guid", "SPFieldGuid");
            fieldMappings.Add("Int32", "SPFieldNumber");
            //fieldMappings.Add("String", "SPFieldText");
            fieldMappings.Add("Double", "SPFieldNumber");
            fieldMappings.Add("SPFieldLookupValue", "SPFieldType.Lookup");
            fieldMappings.Add("String", "SPFieldText");
            fieldMappings.Add("SPFieldUrlValue", "SPFieldUrl");
            fieldMappings.Add("SPFieldUserValue", "SPFieldUser");

            var fieldAttributes = new List<string>() 
            { 
                "EntityField", 
                "EntityFieldBoolean", 
                "EntityFieldDateTime", 
                "EntityFieldGuid", 
                "EntityFieldLookup", 
                "EntityFieldChoice", 
                "EntityFieldText", 
                "EntityFieldNote", 
                "EntityFieldNumber", 
                "EntityFieldUrl", 
                "EntityFieldPrincipal"
            };
            
            this.WriteLine("using System;");
            this.WriteLine("using System.Collections.Generic;");
            this.WriteLine("using System.Text;");
            this.WriteLine("// SharePoint 14");
            this.WriteLine("using Microsoft.SharePoint;");
            this.WriteLine("// SPGenesis Framework http://spgenesis.codeplex.com/");
            this.WriteLine("using SPGenesis.Core;");
            this.WriteLine("");

            foreach (var @namespace in this.Model.CodeElements.OfType<CodeNamespace>())
		    {
                foreach (var entityClass in @namespace.Children.OfType<CodeClass2>())
                {
                    this.WriteLine(string.Format("namespace {0}.{1}", @namespace.Name, Inflector.Pluralize(entityClass.Name)));
                    using (ScopeController.Begin(this))
                    {
                        foreach (CodeElement field in entityClass.Children)
                        {
                            if ((field is CodeProperty) == false)
                                continue;

                            var entityProperty = field as CodeProperty;
                            if (entityProperty.Setter == null || entityProperty.Getter == null)
                                continue;

                            //CodeAttribute attributeProperty = entityProperty.Attributes.OfType<CodeAttribute>().FirstOrDefault(e => e.Name == "EntityFieldAttribute") as CodeAttribute2;
                            var attributeProperty = entityProperty.Attributes.OfType<CodeAttribute>().Join(fieldAttributes, a => a.Name, attr => attr, (a, attr) => a).FirstOrDefault() as CodeAttribute2;
                            if (attributeProperty == null)
                                continue;

                            var fieldName = entityProperty.Name + "Field";

                            this.WriteLine(string.Format("[SPGENField({0}, Type = {1}, Group = \"{2}\" )]", attributeProperty.Value, typeMappings[entityProperty.Type.CodeType.Name], @namespace.Name));
                            this.WriteLine(string.Format("public partial class {0} : SPGENField<{0}, {1}, {2}>", fieldName, fieldMappings[entityProperty.Type.CodeType.Name], entityProperty.Type.CodeType.Name));
                            using (ScopeController.Begin(this))
                            {
                            }
                        }
                    }
			    }
		    }

            return this.GenerationEnvironment.ToString();
        }
    }
}
