using Hazza.Dashboard.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.Handlers;


namespace Hazza.Dashboard.Drivers
{
    public class DashboardShapePartDriver : ContentPartDriver<DashboardShapePart>
    {
        protected override string Prefix
        {
            get { return "DashboardShapePart"; }
        }

        protected override DriverResult Display(DashboardShapePart part, string displayType, dynamic shapeHelper)
        {
            return ContentShape("Parts_DashboardShapePart",
                () => shapeHelper.Parts_DashboardShapePart(
                    Model: part));
        }

        protected override DriverResult Editor(DashboardShapePart part, dynamic shapeHelper)
        {
            return ContentShape("Parts_DashboardShapePart_Edit",
                () => shapeHelper.EditorTemplate(
                    TemplateName: "Parts/DashboardShapePart",
                    Model: part,
                    Prefix: Prefix));
        }

        protected override DriverResult Editor(DashboardShapePart part, IUpdateModel updater, dynamic shapeHelper)
        {
            updater.TryUpdateModel(part, Prefix, null, null);
            return Editor(part, shapeHelper);
        }
    }
}