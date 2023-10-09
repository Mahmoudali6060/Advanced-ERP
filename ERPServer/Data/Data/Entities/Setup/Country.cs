using Data.Entities.UserManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.Entities.Setup
{
    public class Country 
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public virtual IList<UserProfile> UserProfile { get; set; }
    }
}
