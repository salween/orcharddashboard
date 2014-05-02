using System.Collections.Generic;
using System.Linq;
using Hazza.Dashboard.Settings;
using Hazza.Dashboard.ViewModels;
using Orchard;
using Orchard.ContentManagement;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement.MetaData.Models;
using Orchard.Data.Migration.Schema;
using Orchard.DisplayManagement;
using Orchard.UI.Zones;

namespace Hazza.Dashboard.Services
{
    public interface IDashboardService : IDependency
    {
        dynamic DashboardShape(string shapeType);
        dynamic DashboardShape(string shapeType, dynamic viewModel);
        IEnumerable<SelectWidgetViewModel> GetWidgets();
    }

    public class DashboardService : IDashboardService
    {
        private readonly IShapeFactory shapeFactory;
        private readonly IOrchardServices services;
        private readonly IContentDefinitionManager defManager;
        private readonly IContentManager contentManager;

        public DashboardService(IShapeFactory shapeFactory, IOrchardServices services, IContentDefinitionManager defManager, IContentManager contentManager)
        {
            this.shapeFactory = shapeFactory;
            this.services = services;
            this.defManager = defManager;
            this.contentManager = contentManager;
        }

        public dynamic DashboardShape(string shapeType)
        {
            dynamic vm = new { Prop1 = "harry", Prop2 = "daniel" };
            return DashboardShape(shapeType, vm);
        }

        public dynamic DashboardShape(string shapeType, dynamic viewModel)
        {
            var shape = CreateItemShape(shapeType);
            shape.ViewModel = viewModel;
            var newShape = services.New.Penis();
            newShape.ViewModel = viewModel;
            var newShape2 = services.New.Penis2();
            newShape2.ViewModel = viewModel;

            shape.Zones["Head"].Add(newShape);
            shape.Zones["Content"].Add(newShape2);

            return shape;
        }

        private dynamic CreateItemShape(string shapeType)
        {
            return shapeFactory.Create(shapeType, Arguments.Empty(), () => new ZoneHolding(() => shapeFactory.Create("DashboardZone", Arguments.Empty())));
        }

        public IEnumerable<SelectWidgetViewModel> GetWidgets()
        {
            return from contentTypeDefinition in GetDashboardTypes()
                   let contentTypePartDefinition = contentTypeDefinition.Parts.FirstOrDefault(e => e.PartDefinition.Name == "DashboardPart")
                   where contentTypePartDefinition != null
                   let settings = contentTypePartDefinition.Settings.GetModel<DashboardPartSettings>()
                   select new SelectWidgetViewModel()
                   {
                       Name = contentTypeDefinition.Name,
                       DisplayName = settings.Name,
                       Decription = settings.Description
                   };
        }

        public IContentQuery<ContentItem> GetDashboardItems()
        {
            return contentManager.Query(VersionOptions.Published, GetDashboardTypes().Select(e => e.Name).ToArray());
        }

        private IEnumerable<ContentTypeDefinition> GetDashboardTypes()
        {
            return defManager.ListTypeDefinitions()
                .Where(e => e.Parts.Any(y => y.PartDefinition.Name == "DashboardPart"))
                .ToList();
        }
    }
}