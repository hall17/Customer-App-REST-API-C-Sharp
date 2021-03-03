using CustomerAppBLL;
using CustomerAppBLL.BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CustomerRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        BLLFacade facade = new BLLFacade();

        // GET: api/<CustomersController>
        [HttpGet]
        public IEnumerable<CustomerBO> Get()
        {
            return facade.CustomerService.GetAll();
        }

        // GET api/<CustomersController>/5
        [HttpGet("{id}")]
        public CustomerBO Get(int id)
        {
            return facade.CustomerService.Get(id);
        }

        // POST api/<CustomersController>
        [HttpPost]
        public IActionResult Post([FromBody] CustomerBO cust)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(facade.CustomerService.Create(cust));
        }

        // PUT api/<CustomersController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CustomerBO cust)
        {
            if (id != cust.Id)
            {
                return BadRequest("Path Id does not match Customer ID");
            }
            try
            {
                return Ok(facade.CustomerService.Update(cust));
            }
            catch (InvalidOperationException e)
            {
                return StatusCode(404,e.Message);
            }
            
        }

        // DELETE api/<CustomersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            facade.CustomerService.Delete(id);
        }
    }
}
