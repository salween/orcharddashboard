using Orchard.ContentManagement;
using Orchard.ContentManagement.Records;


namespace Hazza.Dashboard.Models
{
    public class DashboardShapePart : ContentPart
    {
        public string Shape
        {
            get { return this.Retrieve(x => x.Shape); }
            set { this.Store(x => x.Shape, value); }
        }
    }
}