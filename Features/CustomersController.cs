using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Customer.API.ExceptionHandlers;
using Customer.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Customer.API.Features
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(ValidationHandler))]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public class CustomersController : ControllerBase
    {
        private readonly IEnumerable<IGetCustomers> get;
        private readonly IPostCustomer post;
        private readonly IPutCustomer put;
        private readonly IDeleteCustomer delete;

        public CustomersController(IEnumerable<IGetCustomers> get, IPostCustomer post,
                                    IPutCustomer put, IDeleteCustomer delete)
        {
            this.get = get;
            this.post = post;
            this.put = put;
            this.delete = delete;
        }

        /// <summary>
        /// GetCustomers All customers 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var result = await this.get.Last().Handler();
            return Ok(result);
        }

        /// <summary>
        /// Search customer by date of birth 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [HttpGet("{search}")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> SearchByDate(DateTime date)
        {
            var result = await this.get.First().Handler(new SerachModel { DOB = date });

            return !result.Any() ? NotFound(Constant.SerachNotFound) : (IActionResult)Ok(result);
        }

        /// <summary>
        /// Search customer by date of birth And will show the count of customers
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [HttpGet("{search}")]
        [MapToApiVersion("2.0")]
        public async Task<IActionResult> SearchByDateWithCount(DateTime date)
        {
            var customers = await this.get.First().Handler(new SerachModel { DOB = date });

            var result = new { count = customers.Count(), customers };

            return Ok(result);
        }

        /// <summary>
        /// Search customer by Postcode
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>  
        [HttpGet("{search}/postCode")]
        public async Task<IActionResult> SearchByPostCode(int code)
        {
            var result = await this.get.First().Handler(new SerachModel { ZipCode = code.ToString(), DOB = null });

            return !result.Any() ? NotFound(Constant.SerachNotFound) : (IActionResult)Ok(result);
        }

        /// <summary>
        /// Create new Customer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<CustomerModel>> CreateCustomer(CustomerModel customer)
        {
            var result = await this.post.Handler(customer);
            return Ok(result);
        }

        /// <summary>
        /// Delete a Customer based on customerId
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpDelete("{customerId}")]
        public async Task<IActionResult> DeleteCustomer(int customerId)
        {
            if (customerId <= 0) BadRequest(Constant.BadId);

            await this.delete.Handler(customerId);

            return Ok();
        }

        /// <summary>
        /// Update Customers
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateCustomer(CustomerModel customer)
        {
            await this.put.Handler(customer);
            return Ok();
        }
    }
}