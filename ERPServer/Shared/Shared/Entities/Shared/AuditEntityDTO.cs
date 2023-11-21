using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Shared.Entities.Shared
{

    public class AuditEntityDTO : BaseDTO
    {
        public long? CreatedByProfileId { get; set; }
        public long? ModifiedByProfileId { get; set; }
        public string CreatedByUsername { get; set; }
        public string ModifiedByUsername { get; set; }
    }
}
