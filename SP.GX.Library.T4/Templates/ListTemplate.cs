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
    public class ListTemplate : Template, ITemplate
    {
        public EntityGenerator Generator { get; set; }
        public FileCodeModel2 Model { get; set; }

        public override string TransformText()
        {
            this.Output.File = System.IO.Path.GetFileNameWithoutExtension(this.Model.Parent.Name) + ".lists.cs";

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
                    var className = entityClass.Name + "List";
                    var classNamePluralize = Inflector.Pluralize(entityClass.Name);

                    var entityAttribute = entityClass.Attributes.OfType<CodeAttribute>().FirstOrDefault(e => e.Name.Contains("Entity")) as CodeAttribute2;
                    if (entityAttribute == null)
                        continue;

                    var arguments = entityAttribute.Children.Cast<CodeAttributeArgument>().ToDictionary(a => a.Name, a => a.Value);

                    this.WriteLine(string.Format("namespace {0}.{1}", @namespace.Name, Inflector.Pluralize(entityClass.Name)));
                    using (ScopeController.Begin(this))
                    {
                        this.WriteLine(string.Format("[SPGENListInstance(WebRelURL = \"Lists/{0}\", Title = \"{0}\", TemplateType = (int)SPListTemplateType.GenericList)]", classNamePluralize));
                        this.WriteLine(string.Format("public partial class {0} : SPGENListInstance<{0}>", className));
                        using (ScopeController.Begin(this))
                        {
                            this.WriteLine("partial void Initialize(SPGENListInstanceProperties properties);");
                            this.WriteLine("protected override void InitializeDefinition(SPGENListInstanceProperties properties)");
                            using (ScopeController.Begin(this))
                            {
                                this.WriteLine("// Initialize");
                                this.WriteLine("this.Initialize(properties);");
                            }

                            this.WriteLine("partial void ProvisionFinalized(SPList list);");
                            this.WriteLine("protected override void OnProvisionFinalized(SPList list)");
                            using (ScopeController.Begin(this))
                            {
                                this.WriteLine("// Register Entity Provisioner");
                                this.WriteLine("list.RegisterEventReceiver<{0}EntityEvents>(false);", entityClass.Name);
                                this.WriteLine("// Finalized");
                                this.WriteLine("this.ProvisionFinalized(list);");
                            }
                        }
                    }
                }
            }

            return this.GenerationEnvironment.ToString();
        }
    }
}
