using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using T4Toolbox;

namespace SP.GX.Library.T4.Utilities
{
    public class ScopeController : IDisposable
    {
        private Template Template;

        private ScopeController(Template template)
        {
            this.Template = template;
            this.Template.WriteLine("{");
            this.Template.PushIndent("\t");
        }

        public void Dispose()
        {
            this.Template.PopIndent();
            this.Template.WriteLine("}");
            this.Template.WriteLine("");
        }

        public static ScopeController Begin(Template template)
        {
            return new ScopeController(template);
        }
    }
}
