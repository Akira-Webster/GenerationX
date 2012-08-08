using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SP.GX.Library.Generation.Interfaces
{
    public interface IInitializeRepository
    {
        void Initialize(Microsoft.SharePoint.SPWeb web);
        void Initialize(string webUrl);
    }
}
