using System.Collections.Generic;
using System.Linq;
using Orchard;
using Orchard.DisplayManagement;
using Orchard.DisplayManagement.Descriptors;
using System.IO;
using Orchard.UI;

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
        public void DashboardZone(dynamic Display, dynamic Shape, TextWriter Output) {
            foreach (var item in Shape)
                Output.Write(Display(item));
        }

        public static IEnumerable<dynamic> Order(dynamic shape)
        {
            IEnumerable<dynamic> unordered = shape;
            if (unordered == null || unordered.Count() < 2)
                return shape;

            var i = 1;
            var progress = 1;
            var flatPositionComparer = new FlatPositionComparer();
            var ordering = unordered.Select(item =>
            {
                var position = (item == null || item.GetType().GetProperty("Metadata") == null || item.Metadata.GetType().GetProperty("Position") == null)
                                   ? null
                                   : item.Metadata.Position;
                return new { item, position };
            }).ToList();

            // since this isn't sticking around (hence, the "hack" in the name), throwing (in) a gnome 
            while (i < ordering.Count())
            {
                if (flatPositionComparer.Compare(ordering[i].position, ordering[i - 1].position) > -1)
                {
                    if (i == progress)
                        progress = ++i;
                    else
                        i = progress;
                }
                else
                {
                    var higherThanItShouldBe = ordering[i];
                    ordering[i] = ordering[i - 1];
                    ordering[i - 1] = higherThanItShouldBe;
                    if (i > 1)
                        --i;
                }
            }

            return ordering.Select(ordered => ordered.item).ToList();
        }
    }
}