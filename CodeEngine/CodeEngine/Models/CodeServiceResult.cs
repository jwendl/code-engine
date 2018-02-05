using System;
using System.Collections.Generic;

namespace CodeEngine.Models
{
    public class CodeServiceResult<T>
    {
        public T ReturnValue { get; set; }

        public IEnumerable<Exception> Exceptions { get; set; }
    }
}
