using System.Collections.Generic;

namespace Hazza.Dashboard.Services.Templates
{
    public class DashboardTemplate : IAdminTemplate
    {
        public string LayoutName
        {
            get { return "Default"; }
        }

        public string[] Zones
        {
            get
            {
                return new[] {
                    "Head", "Column1", "Column2", "Column3"
                };
            }
        }
    }
}