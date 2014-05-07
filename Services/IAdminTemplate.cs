using System.Collections.Generic;
using Orchard;

namespace Hazza.Dashboard.Services
{
    public interface IAdminTemplate : IDependency
    {
        string LayoutName { get; }
        List<string> Zones { get; }
    }
}