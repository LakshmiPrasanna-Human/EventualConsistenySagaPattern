using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hierarchy.Class;
using Hierarchy.Helper;
using Hierarchy.Interfaces;
using Hierarchy.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hierarchy
{
   // [Route("api/[controller]")]
    public class GrandParentController : ControllerBase
    {
        IGrandParent _grandParent;
        private ResultEntity _res;
        private readonly ReturnResultDataHelper _resHelper;

        public GrandParentController(IGrandParent grandParent)
        {
            _grandParent = grandParent;
            _resHelper = new ReturnResultDataHelper();
        }

        // GET: api/<controller>
        [HttpGet]
        [Route("api/Hierarchy/GrandParent")]
        // public async Task<IEnumerable<GrandParentModel>> GetGrandParents()
        public async Task<IActionResult> GetGrandParents()
        {
            try
            {
                IGrandParent _grandParent = new GrandParent();
                List<GrandParentModel> _lst = _grandParent.GetAllGrandParents();
                if (_lst.Count != 0)
                {
                    return Ok(_lst);
                }

                 _res = _resHelper.GetFailureResultEntity("Failure", 601, "No Parents Configured");
                 return new BadRequestObjectResult(_res);
               

            }
            catch (Exception ex)
            {
             
                _res = _resHelper.GetFailureResultEntity("Failure", 701, ex.Message);
                return new BadRequestObjectResult(_res);
            }
        }


        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
