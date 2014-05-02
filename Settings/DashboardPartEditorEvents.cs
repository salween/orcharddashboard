using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using Orchard.ContentManagement;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement.MetaData.Builders;
using Orchard.ContentManagement.MetaData.Models;
using Orchard.ContentManagement.ViewModels;

namespace Hazza.Dashboard.Settings
{
    public class DashboardPartEditorEvents : ContentDefinitionEditorEventsBase
    {
        public override IEnumerable<TemplateViewModel> TypePartEditor(ContentTypePartDefinition definition)
        {
            if (definition.PartDefinition.Name == "DashboardPart")
            {
                var model = definition.Settings.GetModel<DashboardPartSettings>();
                yield return DefinitionTemplate(model);
            }
        }

        public override IEnumerable<TemplateViewModel> TypePartEditorUpdate(ContentTypePartDefinitionBuilder builder, IUpdateModel updateModel) {
            if (builder.Name != "DashboardPart")
                yield break;

            var settings = new DashboardPartSettings {};

            if (updateModel.TryUpdateModel(settings, "DashboardPartSettings", null, null)) {
                builder.WithSetting("DashboardPartSettings.Name", settings.Name.ToString(CultureInfo.InvariantCulture));
                builder.WithSetting("DashboardPartSettings.Description", settings.Description.ToString(CultureInfo.InvariantCulture));
            }

            yield return DefinitionTemplate(settings);
        }
    }
}