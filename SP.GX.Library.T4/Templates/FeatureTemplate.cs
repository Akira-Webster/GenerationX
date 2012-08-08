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

///Microsoft.SharePoint.SPEventReceiverType

namespace SP.GX.Library.T4.Templates
{
    public class FeatureTemplate : Template, ITemplate
    {
        public EntityGenerator Generator { get; set; }
        public FileCodeModel2 Model { get; set; }

        public override string TransformText()
        {
            this.Output.File = System.IO.Path.GetFileNameWithoutExtension(this.Model.Parent.Name) + ".feature.cs";
            
            this.WriteLine("using System;");
            this.WriteLine("using System.Collections.Generic;");
            this.WriteLine("using System.Text;");
            this.WriteLine("// SharePoint 14");
            this.WriteLine("using Microsoft.SharePoint;");
            this.WriteLine("// SPGenesis Framework http://spgenesis.codeplex.com/");
            this.WriteLine("using SPGenesis.Core;");
            this.WriteLine("// GenerationX");
            this.WriteLine("using SP.GX.Library.Generation.Provisioners;");
            this.WriteLine("using SP.GX.Library.Generation.Events;");
            this.WriteLine("");

            foreach (var @namespace in this.Model.CodeElements.OfType<CodeNamespace>())
		    {
                foreach (var entityClass in @namespace.Children.OfType<CodeClass2>())
                {
                    this.WriteLine(string.Format("namespace {0}.{1}", @namespace.Name, Inflector.Pluralize(entityClass.Name)));
                    using (ScopeController.Begin(this))
                    {
                        this.WriteLine(string.Format("public partial class {0}FeatureProvisioner : FeatureProvisioner", entityClass.Name));
                        using (ScopeController.Begin(this))
                        {
                            // Constructor
                            this.WriteLine(string.Format("public {0}FeatureProvisioner()", entityClass.Name));
                            using (ScopeController.Begin(this))
                            {
                                this.WriteLine("this.AddRule(FeatureReceiverType.Activated, Provision);");
                            }

                            //Initialize
                            this.WriteLine(string.Format("private void Provision()", entityClass.Name));
                            using (ScopeController.Begin(this))
                            {
                                this.WriteLine("if(this.Web == null) throw new ArgumentNullException(\"SPWeb is not accessible in this scope.\");");
                                this.WriteLine("");

                                //Fields
                                foreach (var entityProperty in entityClass.Children.OfType<CodeProperty>())
                                {
                                    if (entityProperty.Setter == null || entityProperty.Getter == null)
                                        continue;

                                    var result = entityProperty.Attributes.OfType<CodeAttribute>().Any(a => a.Name.Contains("EntityIdentity"));
                                    if (result)
                                        continue;

                                    var fieldName = entityProperty.Name + "Field";
                                    this.WriteLine(string.Format("{0}.Instance.Provision(this.Web);", fieldName));
                                }
                                this.WriteLine("");

                                //Type
                                var typeName = entityClass.Name + "Type";
                                this.WriteLine(string.Format("{0}.Instance.Provision(this.Web);", typeName));

                                //List
                                var listName = entityClass.Name + "List";
                                this.WriteLine(string.Format("{0}.Instance.Provision(this.Web);", listName));

                            }
                        }

                        this.WriteLine(string.Format("public class {0}FeatureEvents : FeatureEvents<{0}FeatureProvisioner>", entityClass.Name));
                        using (ScopeController.Begin(this))
                        {
                        }
                    }
			    }
		    }

            return this.GenerationEnvironment.ToString();
        }
    }
}
