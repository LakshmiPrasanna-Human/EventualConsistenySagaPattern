/*
    Description:    Resuable class for sending ResultEntity in case of failures. Its will have error code and error message
    Author:         Lakshmi Prasanna

*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientFactory
{
    public class ResultEntity
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public ErrorDetailsEntity ErrorDetails { get; set; }
    }
}
