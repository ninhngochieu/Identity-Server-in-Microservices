using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CatalogServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        // GET: api/<CatalogController>
        [Authorize(Policies.Read)]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<CatalogController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CatalogController>
        [HttpPost]
        [Authorize(Policies.Write)]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CatalogController>/5
        [HttpPut("{id}")]
        [Authorize(Policies.Write)]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CatalogController>/5
        [HttpDelete("{id}")]
        [Authorize(Policies.Write)]
        public void Delete(int id)
        {
        }
    }
}
