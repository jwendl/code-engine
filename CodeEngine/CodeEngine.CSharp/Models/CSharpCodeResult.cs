using System;

namespace CodeEngine.CSharp.Models
{
    public class CSharpCodeResult<T>
    {
        public Exception Exception { get; set; }

        public T ReturnValue { get; set; }
    }
}
