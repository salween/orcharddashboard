using System.Collections.Generic;
using Orchard;

namespace Hazza.Dashboard.Services
{
    public interface IAdminTemplate : IDependency
    {
        string LayoutName { get; }
        List<string> Zones { get; }
    }

    public interface IAdminPage : IDependency {
        //List<AdminPages> Actions { get; }
        List<KeyValuePair<string,string>> Actions { get; } 
    }

    public class AdminPages {
        string ActionName { get; set; }
        string Layout { get; set; }
    }
}