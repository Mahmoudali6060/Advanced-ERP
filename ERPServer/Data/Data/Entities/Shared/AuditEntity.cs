using Data.Entities.UserManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Entities.Shared
{

    public class AuditEntity :BaseEntity
    {

        public long? CreatedByProfileId { get; set; }
        public string CreatedByUsername { get; set; }
        public virtual UserProfile CreatedByProfile { get; set; }
        public long? ModifiedByProfileId { get; set; }
        public string ModifiedByUsername { get; set; }
        public virtual UserProfile ModifiedByProfile { get; set; }
    }
}
