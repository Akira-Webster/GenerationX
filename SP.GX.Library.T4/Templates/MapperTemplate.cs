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
    public class MapperTemplate : Template, ITemplate
    {
        public EntityGenerator Generator { get; set; }
        public FileCodeModel2 Model { get; set; }

        public override string TransformText()
        {
            this.Output.File = System.IO.Path.GetFileNameWithoutExtension(this.Model.Parent.Name) + ".maps.cs";

            this.WriteLine("using System;");
            this.WriteLine("using System.Collections.Generic;");
            this.WriteLine("using System.Text;");
            this.WriteLine("// SharePoint 14");
            this.WriteLine("using Microsoft.SharePoint;");
            this.WriteLine("// SPGenesis Framework http://spgenesis.codeplex.com/");
            this.WriteLine("using SPGenesis.Core;");
            this.WriteLine("using SPGenesis.Entities;");
            this.WriteLine("");

            foreach (var @namespace in this.Model.CodeElements.OfType<CodeNamespace>())
            {
                foreach (var entityClass in @namespace.Children.OfType<CodeClass2>())
                {
                    var className = entityClass.Name + "Mapper";

                    this.WriteLine(string.Format("namespace {0}.{1}", @namespace.Name, Inflector.Pluralize(entityClass.Name)));
                    using (ScopeController.Begin(this))
                    {
                        this.WriteLine(string.Format("public class {0} : SPGENEntityMap<{1}>", className, entityClass.Name));
                        using (ScopeController.Begin(this))
                        {
                            this.WriteLine("protected override void InitializeMapper()");
                            using (ScopeController.Begin(this))
                            {
                                foreach (var entityProperty in entityClass.Children.OfType<CodeProperty>())
                                {
                                    if (entityProperty.Setter == null || entityProperty.Getter == null)
                                        continue;

                                    var isIdentifier = entityProperty.Attributes.OfType<CodeAttribute>().Any(a => a.Name.Contains("EntityIdentity"));

                                    if (isIdentifier)
                                    {
                                        this.WriteLine(String.Format("this.RegisterIdentifierProperty(entity => entity.{0});", entityProperty.Name));
                                    }
                                    else
                                    {
                                        var fieldName = entityProperty.Name + "Field";
                                        this.WriteLine(string.Format("this.MapField(entity => entity.{0}, {1}.InternalName);", entityProperty.Name, fieldName));
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return this.GenerationEnvironment.ToString();
        }
    }
}
