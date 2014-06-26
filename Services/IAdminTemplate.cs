using System.Collections.Generic;
using Orchard;

namespace Hazza.Dashboard.Services
{
    public interface IAdminTemplate : IDependency
    {
        string LayoutName { get; }
        string[] Zones { get; }
    }
}