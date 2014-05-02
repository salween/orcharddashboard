using Orchard;
using Orchard.DisplayManagement;
using Orchard.DisplayManagement.Descriptors;
using System.IO;

namespace Hazza.Dashboard
{
    public class DashboardFactory : IShapeTableProvider
    {
        public DashboardFactory()
        {

        }

        public void Discover(ShapeTableBuilder builder)
        {

        }

        [Shape]
        public void DashboardZone(dynamic Display, dynamic Shape, TextWriter Output)
        {
            foreach (var item in Shape)
                Output.Write(Display(item));
        }
    }
}