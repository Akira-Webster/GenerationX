using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using EnvDTE80;
//
using SP.GX.Library.T4.Generators;

namespace SP.GX.Library.T4.Generators
{
    public interface ITemplate
    {
        EntityGenerator Generator { get; set; }
        FileCodeModel2 Model { get; set; }

        void Render();
    }
}
