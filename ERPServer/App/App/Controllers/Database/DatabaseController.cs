using Microsoft.AspNetCore.Mvc;

namespace App.Controllers.Authentication
{

    [Route("Api/Database")]
    [ApiController]
    public class DatabaseController : ControllerBase
    {

       // IDatabaseBackupDSL _databaseDSL;

        //public DatabaseController(IDatabaseBackupDSL databaseDSL)
        //{
        //    _databaseDSL = databaseDSL;
        //}

        //[HttpPost, Route("BackupDatabase")]
        //public IActionResult BackupDatabase(DatabaseEntity model) => Ok(_databaseDSL.BackupDatabase(model));
    }
}
