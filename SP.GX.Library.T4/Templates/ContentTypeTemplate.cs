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
    public class ContentTypeTemplate : Template, ITemplate
    {
        public EntityGenerator Generator { get; set; }
        public FileCodeModel2 Model { get; set; }

   	    public override string TransformText()
        {
		    this.Output.File = System.IO.Path.GetFileNameWithoutExtension(this.Model.Parent.Name) + ".types.cs";

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
                    var entityAttribute = entityClass.Attributes.OfType<CodeAttribute>().FirstOrDefault(e => e.Name.Contains("Entity")) as CodeAttribute2;
                    if (entityAttribute == null)
                        continue;

                    var arguments = entityAttribute.Children.Cast<CodeAttributeArgument>().ToDictionary(a => a.Name, a => a.Value);

                    var className = entityClass.Name + "Type";
                    var contentTypeID = "0x0100" + Guid.Parse(arguments["ContentTypeId"].Trim('\"')).ToString("N");

                    this.WriteLine(string.Format("namespace {0}.{1}", @namespace.Name, Inflector.Pluralize(entityClass.Name)));
                    using (ScopeController.Begin(this))
                    {
                        this.WriteLine(string.Format("[SPGENContentType(ID = \"{0}\", Name = \"{1}\", Group = \"{2}\")]", contentTypeID, className, @namespace.Name));
                        this.WriteLine(string.Format("public partial class {0} : SPGENContentType<{0}>", className));
                        using (ScopeController.Begin(this))
                        {
                            this.WriteLine("partial void Initializing(SPGENContentTypeProperties properties);");
                            this.WriteLine("protected override void InitializeDefinition(SPGENContentTypeProperties properties)");
                            using (ScopeController.Begin(this))
                            {
                                this.WriteLine("properties.FieldLinks.ProvisioningMode = SPGENProvisioningMode.AddUpdateRemoveExclusive;");
                                this.WriteLine("properties.FieldLinksToRemove.Add(SPBuiltInFieldId.Title);");

                                foreach (var entityProperty in entityClass.Children.OfType<CodeProperty>())
                                {
                                    if (entityProperty.Setter == null || entityProperty.Getter == null)
                                        continue;

                                    var result = entityProperty.Attributes.OfType<CodeAttribute>().Any(a => a.Name.Contains("EntityIdentity"));
                                    if (result)
                                        continue;

                                    var fieldName = entityProperty.Name + "Field";
                                    this.WriteLine(string.Format("properties.FieldLinks.Add<{0}>();", fieldName));
                                }

                                this.WriteLine("this.Initializing(properties);");
                            }

                            this.WriteLine("partial void ProvisionFinalized(SPContentType contentType, bool isParentList);");
                            this.WriteLine("protected override void OnProvisionFinalized(SPContentType contentType, bool isParentList)");
                            using (ScopeController.Begin(this))
                            {
                                this.WriteLine("this.ProvisionFinalized(contentType, isParentList);");
                            }
                        }
                    }
                }
            }

            return this.GenerationEnvironment.ToString();
        }
    }
}
