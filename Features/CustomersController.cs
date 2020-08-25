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
        private readonly IDeleteCustomer delete;

        public CustomersController(IGetCustomers get, IPostCustomer post, IDeleteCustomer delete)
        {
            this.get = get;
            this.post = post;
            this.delete = delete;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var result = await this.get.Handler();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CustomerModel customer)
        {
            // To do's
            // 1. propery error handling , 2 return right code 3. versioing in API 4. looging 5. geo failure .6 security
            var result = await this.post.Handler(customer);
            return Ok(result);
        }


        [HttpDelete("{customerId}")]
        public async Task<IActionResult> DeleteCustomer(int customerId)
        {
            // To do's
            // 1. propery error handling , 2 return right code 3. versioing in API 4. looging 5. geo failure .6 security
            var result = await this.delete.Handler(customerId);
            return Ok(result);
        }
    }
}