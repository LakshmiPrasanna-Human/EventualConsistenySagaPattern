/*
    Description:    Resuable class to support sending error details.
    Author:         Lakshmi Prasanna

*/
using System;
using System.Collections.Generic;
using System.Text;

namespace HttpClientFactory
{
    public class ErrorDetailsEntity
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
