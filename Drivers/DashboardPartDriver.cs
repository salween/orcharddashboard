using Hazza.Dashboard.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.Handlers;


namespace Hazza.Dashboard.Drivers
{

    public class DashboardPartDriver : ContentPartDriver<DashboardPart>
    {
        protected override string Prefix
        {
            get { return "DashboardPart"; }
        }

        protected override DriverResult Display(DashboardPart part, string displayType, dynamic shapeHelper)
        {
            return ContentShape("Parts_DashboardPart",
                () => shapeHelper.Parts_DashboardPart(Model: part));
        }

        protected override DriverResult Editor(DashboardPart part, dynamic shapeHelper)
        {
            return ContentShape("Parts_DashboardPart_Edit",
                () => shapeHelper.EditorTemplate(
                    TemplateName: "Parts/DashboardPart",
                    Model: part,
                    Prefix: Prefix));
        }

        protected override DriverResult Editor(DashboardPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            updater.TryUpdateModel(part, Prefix, null, null);
            return Editor(part, shapeHelper);
        }
    }
}