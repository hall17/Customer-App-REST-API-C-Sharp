using CustomerAppBLL;
using CustomerAppBLL.BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomerRestAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        BLLFacade facade = new BLLFacade();
        // GET: api/<OrdersController>
        [HttpGet]
        public IEnumerable<OrderBO> Get()
        {
            return facade.OrderService.GetAll();
        }

        // GET api/<OrdersController>/5
        [HttpGet("{id}")]
        public OrderBO Get(int id)
        {
            return facade.OrderService.Get(id);
        }

        // POST api/<OrdersController>
        [HttpPost]
        public IActionResult Post([FromBody] OrderBO order)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(facade.OrderService.Create(order));
        }

        // PUT api/<OrdersController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] OrderBO order)
        {
            if(id != order.Id)
            {
                return BadRequest("Path Id does not match Customer Id");
            }
            try
            {
                var orderUpdated = facade.OrderService.Update(order);
                return Ok(orderUpdated);
            }
            catch (InvalidOperationException e)
            {
                return StatusCode(404, e.Message);
            }
        }

        // DELETE api/<OrdersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            facade.OrderService.Delete(id);
        }
    }
}
