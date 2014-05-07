using System.Collections.Generic;
using Hazza.Dashboard.Services;
using Orchard;

namespace Hazza.Dashboard.Templates
{
    public class DashboardTemplate : IAdminTemplate
    {
        public string LayoutName
        {
            get { return "Dashboard"; }
        }

        public List<string> Zones
        {
            get
            {
                return new List<string>() {
                    "Head", "Column1", "Column2", "Column3"
                };
            }
        }
    }
}