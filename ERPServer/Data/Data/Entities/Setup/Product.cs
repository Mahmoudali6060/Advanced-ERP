using Data.Entities.Shared;
using Data.Entities.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities.Setup
{
    public class Product 
    {
        public long Id { get; set; }
        public string HSCode { get; set; }
        public string Item { get; set; }
        public long CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
