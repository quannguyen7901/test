using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Common.Execption
{
    public class InternalException : Exception
    {
        public int ErrorCode { get; set; }
        public InternalException() { }
        public InternalException(int errorCode)
        { 
            ErrorCode = errorCode;
        }
        public InternalException(string message) : base(message) { }
        public InternalException(string message, int errorCode) : base(message) {
            ErrorCode = errorCode;
        }
    }
}
