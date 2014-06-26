using Hazza.Dashboard.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Handlers;
using Orchard.Localization;


namespace Hazza.Dashboard.Handlers
{
    public class DashboardSiteSettingsPartHander : ContentHandler
    {
        public DashboardSiteSettingsPartHander()
        {
            T = NullLocalizer.Instance;
            Filters.Add(new ActivatingFilter<DashboardSiteSettingsPart>("Site"));
            Filters.Add(new TemplateFilterForPart<DashboardSiteSettingsPart>("DashboardSiteSettings", "Parts/DashboardSiteSettingsPart", "dashboard"));
        }

        public Localizer T { get; set; }

        protected override void GetItemMetadata(GetContentItemMetadataContext context)
        {
            if (context.ContentItem.ContentType != "Site")
                return;
            base.GetItemMetadata(context);
            context.Metadata.EditorGroupInfo.Add(new GroupInfo(T("Dashboard")));
        }
    }
}