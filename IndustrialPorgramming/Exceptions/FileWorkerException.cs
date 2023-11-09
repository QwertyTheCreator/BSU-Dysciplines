using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Industrial_Programming.Exceptions
{
    public class FileWorkerException : Exception
    {
        public FileWorkerException(string message) : base(message) { }
    }
}
