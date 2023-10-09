using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Shared.Entities.Shared
{

    public class BaseEntity 
    {
        public long Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
    }
}
