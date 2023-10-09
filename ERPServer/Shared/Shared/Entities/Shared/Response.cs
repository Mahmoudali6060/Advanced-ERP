using Infrastructure.ExceptionHandling.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Entities.Shared
{
    public class Response
    {
        public int Total { get; set; }
        public dynamic Entity { get; set; }
        public dynamic Data { get; set; }
    }

    public class Response<T>
    {
        public int Total { get; set; }
        public T Entity { get; set; }
        public List<ErrorDetails> ErrorDetailsList { get; set; }
    }
}
