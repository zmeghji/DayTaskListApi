using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DayTaskList.Exceptions
{
    public class ApiException : Exception
    {
        public int StatusCode;
        public List<string> ErrorMessages { get; set; }
        public ApiException(string errorMessage, int statusCode)
        {
            StatusCode = statusCode;
            ErrorMessages = new List<string> { errorMessage };
        }
        public ApiException(List<string> errorMessages, int statusCode)
        {
            StatusCode = statusCode;
            ErrorMessages = errorMessages;
        }
    }
}
