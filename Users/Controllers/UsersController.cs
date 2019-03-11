using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HttpClientFactory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Saga;
using Users.Helper;
using Users.Models;

namespace Users.Controllers
{
   // [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;
        private Users.Helper.ResultEntity _res;
        private readonly ReturnResultDataHelper _resHelper;

        public UsersController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _resHelper = new ReturnResultDataHelper();
        }



               
        // GET: api/<controller>
        [HttpGet]
        [Route("api/Users")]
        //  public async Task<IEnumerable<GrandParentModel>> GetUsers()
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var grandParentDetails = new List<GrandParentModel>();
                var client = _clientFactory.CreateClient("Hierarchy");
                RestClient<GrandParentModel, GrandParentModel> _hierarchyClient = new RestClient<GrandParentModel, GrandParentModel>();
                grandParentDetails = _hierarchyClient.GetMultipleItemsRequest(client, "api/Hierarchy/GrandParent").Result.OrderBy(grandparent => grandparent.GrandParentID).ToList();
                if(grandParentDetails.Count!=0)
                    return Ok(grandParentDetails);

                _res = _resHelper.GetFailureResultEntity("Failure", 601, "No Parents Configured");
                return new BadRequestObjectResult(_res);

            }
            catch (Exception ex)
            {

                _res = _resHelper.GetFailureResultEntity("Failure", 701, ex.Message);
                return new BadRequestObjectResult(_res);
            }
        }

        



    }
}
