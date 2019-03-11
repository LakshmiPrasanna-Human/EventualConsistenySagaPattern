using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Hierarchy.Helper
{
    public class BadCustomActionResult : IActionResult
    {
        public ResultEntity _resultEntity { get; set; }

        public BadCustomActionResult(ResultEntity resultEntity)
        {
            _resultEntity = resultEntity;
        }        

        public HttpResponseMessage MyCustomResponse(ResultEntity entity)
        {
           
            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new StringContent(JsonConvert.SerializeObject(entity))
            };

        }

     
        public Task ExecuteResultAsync(ActionContext context)
        {
            return Task.FromResult(MyCustomResponse(_resultEntity));
        }
    }
}
