using Hazza.Dashboard.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.Handlers;
using Orchard.DisplayManagement;


namespace Hazza.Dashboard.Drivers
{
    public class DashboardShapePartDriver : ContentPartDriver<DashboardShapePart>
    {
        private readonly IShapeDisplay shapeDisplay;
        private readonly IShapeFactory shapeFactory;
        public DashboardShapePartDriver(IShapeDisplay shapeDisplay, IShapeFactory shapeFactory) {
            this.shapeDisplay = shapeDisplay;
            this.shapeFactory = shapeFactory;
        }

        protected override string Prefix
        {
            get { return "DashboardShapePart"; }
        }

        protected override DriverResult Display(DashboardShapePart part, string displayType, dynamic shapeHelper)
        {
            return ContentShape("Parts_DashboardShapePart",
                () => {
                    var shape = shapeFactory.Create(part.Shape);
                    var html = shapeDisplay.Display(shape);

                    return shapeHelper.Parts_DashboardShapePart(Html: html);
                });
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