using Data.Entities.UserManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.Entities.Setup
{
    public class State 
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }
        public string Name { get; set; }
        public long CountryId { get; set; }
        public virtual IList<UserProfile> UserProfile { get; set; }

    }
}
