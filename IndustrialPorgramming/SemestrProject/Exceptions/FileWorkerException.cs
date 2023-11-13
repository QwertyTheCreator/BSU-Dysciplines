using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndustrialProgramming.Exceptions
{
    public class FileWorkerException : Exception
    {
        public FileWorkerException(string message) : base(message) { }
    }
}
