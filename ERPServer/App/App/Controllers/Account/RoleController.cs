using System.Threading.Tasks;
using Accout.DataServiceLayer;
using Data.Constants;
using Entities.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Entities.Shared;


namespace App.Controllers.Account
{
    [Route("Api/Role")]
    [ApiController]
    //[Authorize(Roles = Roles.Admin)]
    public class RoleController : Controller
    {
        IRoleDSL _roleDSL;
        public RoleController(IRoleDSL roleDSL)
        {
            this._roleDSL = roleDSL;
        }

        [HttpPost, Route("GetAll")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAll([FromBody] RoleGroupSearchDTO searchCriteriaDTO) => Ok(await _roleDSL.GetAll(searchCriteriaDTO));

        [HttpGet, Route("GetById/{id}")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetById(long id) => Ok(await _roleDSL.GetById(id));

        [HttpGet, Route("GetAllLite")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAllLite() => Ok(await _roleDSL.GetAllLite());

        [HttpPost, Route("Add")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Add([FromBody] RoleGroupDTO model) => Ok(await _roleDSL.Add(model));

        //[Authorize(Roles = Roles.Admin + "," + Roles.Consumer)]
        [HttpPost, Route("Update")]
        public async Task<IActionResult> Update(RoleGroupDTO model) => Ok(await _roleDSL.Update(model));

        [HttpDelete, Route("Delete/{id}")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Delete(int id) => Ok(await _roleDSL.Delete(id));


    }
}