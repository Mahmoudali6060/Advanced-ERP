using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Entities.Shared
{

    public class BaseEntity : IEntity
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Key]
        public long Id { get; set; }
        public DateTime Created { get; set; }
        public long? CreatedById { get; set; }
        public DateTime? Modified { get; set; }
        public long? ModifiedById { get; set; }
        public bool? IsActive { get; set; }
    }
}
