using System.ComponentModel.DataAnnotations;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Aspects;


namespace Hazza.Dashboard.Models
{
    public class DashboardPart : ContentPart, ITitleAspect
    {
        /// <summary>
        /// The dashboard widgets title.
        /// </summary>
        [Required]
        public string Title
        {
            get { return this.Retrieve(x => x.Title); }
            set { this.Store(x => x.Title, value); }
        }

        /// <summary>
        /// The dashboard zone to place the widget
        /// </summary>
        [Required]
        public string DashboardZone
        {
            get { return this.Retrieve(x => x.DashboardZone); }
            set { this.Store(x => x.DashboardZone, value); }
        }

        /// <summary>
        /// The widget's position within the dashboard
        /// </summary>
        [Required]
        public string Position
        {
            get { return this.Retrieve(x => x.Position); }
            set { this.Store(x => x.Position, value); }
        }
    }
}