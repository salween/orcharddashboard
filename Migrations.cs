using Orchard.ContentManagement.MetaData;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;


namespace Hazza.Dashboard
{
    public class Migrations : DataMigrationImpl
    {
        public int Create()
        {
            ContentDefinitionManager.AlterPartDefinition("DashboardPart", part => part
                .Attachable()
                .WithDescription("Turns a content type into a  Dashboard widget. Note: you need to set the stereotype to \"Dashboard\" as well."));


            return 1;
        }

        public int UpdateFrom1()
        {
            ContentDefinitionManager.AlterPartDefinition("DashboardShapePart", part => part
                .Attachable()
                .WithDescription("Lets the user select a shape to render"));

            return 2;
        }

        //public int UpdateFrom2() {
        //    ContentDefinitionManager.AlterTypeDefinition("Dashboard");

        //    return 3;
        //}
    }
}