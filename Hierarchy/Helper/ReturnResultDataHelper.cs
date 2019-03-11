using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hierarchy.Helper
{
    public class ReturnResultDataHelper
    {
        public ResultEntity GetSuccessResultEntity(string Message)
        {
            return new ResultEntity
            {
                Success = true,
                Message = Message,
                ErrorDetails = new ErrorDetailsEntity
                {
                    ErrorCode = 0,
                    ErrorMessage = ""
                }
            };
        }

        public ResultEntity GetFailureResultEntity(string Message, int ErrorCode, string ErrorMessage)
        {
            return new ResultEntity
            {
                Success = false,
                Message = Message,
                ErrorDetails = new ErrorDetailsEntity
                {
                    ErrorCode = ErrorCode,
                    ErrorMessage = ErrorMessage
                }
            };
        }
    }
}
