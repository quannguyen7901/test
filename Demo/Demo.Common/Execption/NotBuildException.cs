using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Common.Execption
{
    public class NotBuildException : Exception
    {
        public int ErrorCode { get; set; }
        public NotBuildException() { }
        public NotBuildException(int errorCode)
        {
            ErrorCode = errorCode;
        }
        public NotBuildException(string message) : base(message) { }
        public NotBuildException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
