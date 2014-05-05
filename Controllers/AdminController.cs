using System;
using System.Linq;
using System.Web.UI.WebControls.WebParts;
using Hazza.Dashboard.Models;
using Hazza.Dashboard.Services;
using Orchard;
using Orchard.Localization;
using Orchard.Logging;
using Orchard.Mvc;
using System.Web.Mvc;
using Orchard.ContentManagement;
using Orchard.Mvc.Extensions;
using Orchard.UI.Notify;


namespace Hazza.Dashboard.Controllers
{
    public class AdminController : Controller, IUpdateModel
    {
        private readonly IDashboardService dashboardService;
        private readonly IOrchardServices services;

        public AdminController(IDashboardService dashboardService, IOrchardServices services)
        {
            this.dashboardService = dashboardService;
            this.services = services;

            T = NullLocalizer.Instance;
            Logger = NullLogger.Instance;
        }

        public Localizer T { get; set; }
        public ILogger Logger { get; set; }

        public ActionResult Index()
        {
            var widgets = dashboardService.GetDashboardItems().List();
            var shape = dashboardService.DashboardShape("Dashboard", false);

            foreach (var widget in widgets) {
                var dashboardPart = widget.As<DashboardPart>();
                var item = services.ContentManager.BuildDisplay(widget);
                shape.Zones[dashboardPart.DashboardZone].Add(item);
            }

            return new ShapeResult(this, shape);
        }

        public ActionResult Editor() {
            var widgets = dashboardService.GetDashboardItems().List();
            var shape = dashboardService.DashboardShape("Dashboard", true);

            foreach (var widget in widgets)
            {
                var dashboardPart = widget.As<DashboardPart>();
                var item = services.New.WidgetEditor(Part: dashboardPart);
                shape.Zones[dashboardPart.DashboardZone].Add(item);
            }

            return new ShapeResult(this, shape);
        }

        public ActionResult SelectWidget(string zone)
        {
            var widgets = dashboardService.GetWidgets();
            return View(widgets);
        }

        public ActionResult AddWidget(string type, string zone, string returnUrl)
        {
            var item = services.ContentManager.New(type);
            if (item == null)
                return HttpNotFound();

            var dashboardPart = item.As<DashboardPart>();

            try {
                dashboardPart.DashboardZone = zone;

                var model = services.ContentManager.BuildEditor(dashboardPart);
                return View(model);
            }
            catch(Exception exception) {
                Logger.Error(T("Creating widget failed: {0}", exception.Message).Text);
                services.Notifier.Error(T("Creating widget failed: {0}", exception.Message));
                return this.RedirectLocal(returnUrl, () => RedirectToAction("Editor"));
            }
        }

        [HttpPost, ActionName("AddWidget")]
        public ActionResult AddWidgetPost(string type, string returnUrl) {
            DashboardPart widget = dashboardService.CreateWidget(type);
            if (widget == null)
                return HttpNotFound();

            var model = services.ContentManager.UpdateEditor(widget, this);
            if (!ModelState.IsValid)
            {
                services.TransactionManager.Cancel();
                return View(model);
            }

            services.Notifier.Information(T("Your {0} has been added.", widget.TypeDefinition.DisplayName));

            return this.RedirectLocal(returnUrl, () => RedirectToAction("Editor"));
        }

        #region IUpdateModel 
        public new bool TryUpdateModel<TModel>(TModel model, string prefix, string[] includeProperties, string[] excludeProperties) where TModel : class
        {
            return base.TryUpdateModel(model, prefix, includeProperties, excludeProperties);
        }

        public void AddModelError(string key, LocalizedString errorMessage)
        {
            ModelState.AddModelError(key, errorMessage.ToString());
        }
        #endregion
    }
}