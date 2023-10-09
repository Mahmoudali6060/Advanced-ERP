using System.Threading.Tasks;
using Accout.DataServiceLayer;
using Data.Constants;
using Entities.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Entities.Shared;


namespace App.Controllers.Account
{
    [Route("Api/UserProfile")]
    [ApiController]
    //[Authorize(Roles = Roles.Admin)]
    public class UserProfileController : Controller
    {
        IUserProfileDSL _userProfileDSL;
        public UserProfileController(IUserProfileDSL userProfileDSL)
        {
            this._userProfileDSL = userProfileDSL;
        }

        [HttpPost, Route("GetAll")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAll([FromBody] UserProfileSearchCriteriaDTO searchCriteriaDTO) => Ok(await _userProfileDSL.GetAll(searchCriteriaDTO));

        [HttpGet, Route("GetById/{id}")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetById(long id) => Ok(await _userProfileDSL.GetById(id));

        [HttpGet, Route("GetAllLite")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAllLite() => Ok(await _userProfileDSL.GetAllLite());

        [HttpPost, Route("Add")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Add([FromBody] UserProfileDTO model) => Ok(await _userProfileDSL.Add(model));

        //[Authorize(Roles = Roles.Admin + "," + Roles.Consumer)]
        [HttpPost, Route("Update")]
        public async Task<IActionResult> Update(UserProfileDTO model) => Ok(await _userProfileDSL.Update(model));

        [HttpDelete, Route("Delete/{id}")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Delete(int id) => Ok(await _userProfileDSL.Delete(id));


    }
}