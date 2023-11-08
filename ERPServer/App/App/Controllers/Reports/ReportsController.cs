using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Telerik.Reporting.Cache.File;
using Telerik.Reporting.Services;
using Telerik.Reporting.Services.AspNetCore;

namespace Nazeel.Api.BackEnd.Controllers.Reports
{
    //using DocumentFormat.OpenXml;
    //using Telerik.Reporting.Services.AspNetCore;

    [Route("api/reports")]
    [ApiController]
    //[Authorize]
    public class ReportsController : ReportsControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ReportsController(IWebHostEnvironment webHostEnvironment, IReportServiceConfiguration reportServiceConfiguration
           ) : base(reportServiceConfiguration)
        {
            _webHostEnvironment = webHostEnvironment;
            string contentRootPath = _webHostEnvironment.ContentRootPath;
            var reportsPath = Path.Combine(contentRootPath, "Reports");
            var resolver = new ReportFileResolver(reportsPath)
                .AddFallbackResolver(new ReportTypeResolver());

            //Setup the ReportServiceConfiguration
            reportServiceConfiguration = new ReportServiceConfiguration
            {
                HostAppId = "Html5App",
                Storage = new FileStorage("C:\\NazeelReport"),
                ReportResolver = resolver,
            };
        }



        public override IActionResult GetParameters(string clientID, [FromBody] ClientReportSource reportSource)
        {
            var reportParameters = base.GetParameters(clientID, reportSource);
            return reportParameters;
            //return base.CreateJsonResponse(HttpStatusCode.Unauthorized, null);
        }

        ////protected override UserIdentity GetUserIdentity()
        ////{
        ////    var id = base.GetUserIdentity();

        ////    return id;
        ////}

        //protected override string GetBasePath()
        //{
        //    var path = base.GetBasePath();
        //    return path;
        //}

        ////protected override IReportResolver CreateReportResolver()
        ////{
        ////    var resolver = base.CreateReportResolver();
        ////    return resolver;
        ////}

        //protected override IConfiguration CreateReportingEngineConfiguration()
        //{
        //    var conf = base.CreateReportingEngineConfiguration();
        //    return conf;
        //}

        //public override void SendDocument(string clientID, string instanceID, string documentID, [FromBody] SendDocumentArgs args)
        //{
        //    base.SendDocument(clientID, instanceID, documentID, args);
        //}

        public override void GetResource(string clientID, string instanceID, string documentID, string resourceID)
        {
            var syncIOFeature = HttpContext?.Features?.Get<IHttpBodyControlFeature>();
            if (syncIOFeature != null)
            {
                syncIOFeature.AllowSynchronousIO = true;
            }
            base.GetResource(clientID, instanceID, documentID, resourceID);
        }
        //public override IActionResult GetSearchResults(string clientID, string instanceID, string documentID, [FromBody] Telerik.Reporting.Services.AspNetCore.SearchArgs args)
        //{
        //    var result = base.GetSearchResults(clientID, instanceID, documentID, args);
        //    return result;
        //}

        //public override IActionResult GetPageSettings(string clientID, [FromBody] ClientReportSource reportSource)
        //{
        //    var settings = base.GetPageSettings(clientID, reportSource);
        //    return settings;
        //}

        //public override JsonResult GetDocumentFormats()
        //{
        //    var formats = base.GetDocumentFormats();
        //    return formats;
        //}

        //public override IActionResult GetDocumentInfo(string clientID, string instanceID, string documentID)
        //{
        //    var info = base.GetDocumentInfo(clientID, instanceID, documentID);
        //    return info;
        //}

        //public override IActionResult GetDocument(string clientID, string instanceID, string documentID)
        //{
        //    var doc = base.GetDocument(clientID, instanceID, documentID);
        //    return doc;
        //}

        //public override IActionResult CreateDocument(string clientID, string instanceID, [FromBody] CreateDocumentArgs args)
        //{
        //    var doc = base.CreateDocument(clientID, instanceID, args);
        //    return doc;
        //}
    }
}