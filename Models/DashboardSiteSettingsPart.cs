using Orchard.ContentManagement;
using Orchard.ContentManagement.Records;


namespace Hazza.Dashboard.Models
{

    public class DashboardSiteSettingsPart : ContentPart
    {
        public string DashboardTemplate
        {
            //TODO: add driver and let user select which template they wish to use
            get { return this.Retrieve(x => x.DashboardTemplate, "Default"); }
            set { this.Store(x => x.DashboardTemplate, value); }
        }
    }
}