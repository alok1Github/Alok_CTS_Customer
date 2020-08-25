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
        private readonly IGetCustomers get;
        private readonly IPostCustomer post;
        private readonly IPutCustomer put;
        private readonly IDeleteCustomer delete;

        public CustomersController(IGetCustomers get, IPostCustomer post,
                                    IPutCustomer put, IDeleteCustomer delete)
        {
            this.get = get;
            this.post = post;
            this.put = put;
            this.delete = delete;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var result = await this.get.Handler();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerModel>> CreateCustomer(CustomerModel customer)
        {
            // To do's
            // 1. propery error handling , 2 return right code 3. versioing in API 4. looging 5. geo failure .6 security
            var result = await this.post.Handler(customer);
            return Ok(result);
        }


        [HttpDelete("{customerId}")]
        public async Task<IActionResult> DeleteCustomer(int customerId)
        {
            await this.delete.Handler(customerId);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCustomer(CustomerModel customer)
        {
            await this.put.Handler(customer);
            return Ok();
        }
    }
}