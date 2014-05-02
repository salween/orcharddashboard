using System;
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
    public class AdminController : Controller
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

            var shape = dashboardService.DashboardShape("Vag");
            return new ShapeResult(this, shape);
        }

        public ActionResult SelectWidget()
        {
            var widgets = dashboardService.GetWidgets();

            return View(widgets);
        }

        public ActionResult AddWidget(string type)
        {
            var item = services.ContentManager.New(type);
            if (item == null)
                return HttpNotFound();

            var dashboardPart = item.As<DashboardPart>();

            try {
                dashboardPart.DashboardZone = "Content";

                var model = services.ContentManager.BuildEditor(dashboardPart);
                return View(model);
            }
            catch(Exception exception) {
                Logger.Error(T("Creating widget failed: {0}", exception.Message).Text);
                services.Notifier.Error(T("Creating widget failed: {0}", exception.Message));
                return RedirectToAction("Index");
            }
        }
    }
}