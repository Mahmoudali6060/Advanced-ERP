using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shared.Entities.Shared
{
    public class ResponseEntityList<T>
    {
        public int Total { get; set; }
        public IEnumerable<T> List { get; set; }
    }
}
