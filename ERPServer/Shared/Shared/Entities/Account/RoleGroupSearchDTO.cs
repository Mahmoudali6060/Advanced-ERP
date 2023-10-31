using Shared.Entities.Shared;
using Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace Entities.Account
{
    public class RoleGroupSearchDTO :Paging
    {
        public string Name { get; set; }
    }
}
