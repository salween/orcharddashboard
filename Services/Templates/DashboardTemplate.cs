using System.Collections.Generic;

namespace Hazza.Dashboard.Services.Templates
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

    public class DashboardPage : IAdminPage {
        public List<KeyValuePair<string, string>> Actions {
            get {
                return new List<KeyValuePair<string, string>>() {
                    new KeyValuePair<string, string>("Dashboard", "Dashboard")
                };
            }
        }
    }
}