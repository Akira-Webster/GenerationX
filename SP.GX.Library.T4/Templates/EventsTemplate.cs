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
    public class EventsTemplate : Template, ITemplate
    {
        public EntityGenerator Generator { get; set; }
        public FileCodeModel2 Model { get; set; }

        public override string TransformText()
        {
            this.Output.File = System.IO.Path.GetFileNameWithoutExtension(this.Model.Parent.Name) + ".events.cs";
            
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
                        this.WriteLine(string.Format("public partial class {0}EntityProvisioner : EntityProvisioner", entityClass.Name));
                        using (ScopeController.Begin(this))
                        {
                        }

                        this.WriteLine(string.Format("public class {0}EntityEvents : ListItemEvents<{0}EntityProvisioner>", entityClass.Name));
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
