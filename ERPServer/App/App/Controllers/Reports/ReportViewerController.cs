using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Nazeel.Api.BackEnd.Controllers.Reports
{

    [Route("api/ReportViewer")]
    [ApiController]
    //[Authorize]
    public class ReportViewerController : Controller
    {
        //[Author:Mahmoud Ali Salman]
        //[Date:23/3/2021]
        //[Reason:This Controller was splitted from ReportsController because it inherits from BaseController
        //To add new token in Reposnse according to Bug:10903]
        public ReportViewerController()
        {

        }

        [HttpGet("IsReportViewerAuthorized")]
        public IActionResult IsReportViewerAuthorized()
        {
            ///This action is used to send a very simple response to front end >>because it is very easy,we don't need a service for it !
            //if user is UnAuthorized,User can not hit this action and will receive 401 (UnAuthorized)
            var response = true;
            return Ok(response);
        }
    }
}