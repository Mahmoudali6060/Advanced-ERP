using System.Threading.Tasks;
using DataService.Setup.Contracts;
using Microsoft.AspNetCore.Mvc;
using Shared.Entities.Setup;


namespace App.Controllers.Setup
{
    [Route("Api/Dashboard")]
    [ApiController]
    //[Authorize(Roles = Roles.Admin)]
    public class DashboardController : Controller
    {
        IDashboardDSL _dashboardDSL;
        public DashboardController(IDashboardDSL companyDSL)
        {
            this._dashboardDSL = companyDSL;
        }

        [HttpPost, Route("GetDashboard")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetDashboard([FromBody] DashboardSearchDTO searchCriteriaDTO) => Ok(await _dashboardDSL.GetDashboard(searchCriteriaDTO));

    }
}