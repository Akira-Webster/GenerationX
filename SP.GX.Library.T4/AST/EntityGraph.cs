using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// Visual Studio Automation
using System.ComponentModel.Design;
using EnvDTE;
using EnvDTE80;
// GenerationX
using SP.GX.Library.Generation;

namespace SP.GX.Library.T4.AST
{
    using EntityDefination = Tuple<string, string, Dictionary<string, string>>;
    using IdentityDefination = Tuple<string, string>;
    using FieldDefination = Tuple<string, string, Dictionary<string, string>>;
    
    public class EntityGraph
    {
        public string Namespace { get; set; }
        public EntityDefination Self {get; set;}
        public IdentityDefination Identity { get; set; }
        public List<FieldDefination> Fields { get; set; }

        public EntityGraph(CodeClass2 @class)
        {
            this.Namespace = @class.Namespace.Name;
            var attr = @class.Attributes.OfType<CodeAttribute>().FirstOrDefault();
            //attr.Children.Cast<CodeAttributeArgument>().FirstOrDefault().
            this.Self = new EntityDefination(@class.Name, @class.FullName, null);
        }
    }
}
