using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Customer.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Customer.API.Features
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IGetCustomers getCustomer;

        public CustomersController(IGetCustomers getCustomer)
        {
            this.getCustomer = getCustomer;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await this.getCustomer.Handler();
            // Try check in next
            return Ok(result);
        }
    }
}