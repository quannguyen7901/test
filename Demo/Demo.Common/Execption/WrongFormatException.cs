using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Common.Execption
{
    public class WrongFormatException : Exception
    {
        public int ErrorCode { get; set; }
        public WrongFormatException() { }
        public WrongFormatException(int errorCode)
        {
            ErrorCode = errorCode;
        }
        public WrongFormatException(string message) : base(message) { }
        public WrongFormatException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
