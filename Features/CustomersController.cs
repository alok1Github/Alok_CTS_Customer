using Customer.API.ExceptionHandlers;
using Customer.API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        private readonly IEnumerable<IUpdateCustomer> update;
        private readonly IDeleteCustomer delete;

        public CustomersController(IEnumerable<IGetCustomers> get,
                                   IEnumerable<IUpdateCustomer> update,
                                   IDeleteCustomer delete)
        {
            this.get = get;
            this.update = update;
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

            return !result.Any() ? NotFound(Constant.NotCustomer) : (IActionResult)Ok(result);
        }

        /// <summary>
        /// Search customer by date of birth 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [HttpGet("{search}")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> SearchByDate(DateTimeOffset date)
        {
            var result = await this.get.First().Handler(new SerachModel { DOB = date });

            return !result.Any() ? NotFound(Constant.NotFound) : (IActionResult)Ok(result);
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
        public async Task<IActionResult> SearchByPostCode(string code)
        {
            var result = await this.get.First().Handler(new SerachModel { ZipCode = code.ToString(), DOB = null });

            return !result.Any() ? NotFound(Constant.NotFound) : (IActionResult)Ok(result);
        }

        /// <summary>
        /// Create new Customer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<CustomerModel>> CreateCustomer(CustomerModel customer)
        {
            var result = await this.update.First().Handler(customer);
            return Ok(result);
        }

        /// <summary>
        /// Delete a Customer based on customerId
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpDelete("{customerId}")]
        public async Task<IActionResult> DeleteCustomer(string customerId)
        {
            if (string.IsNullOrEmpty(customerId)) BadRequest(Constant.BadId);

            await this.delete.Handler(customerId);

            return Ok();
        }

        /// <summary>
        /// Update Customer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateCustomer(CustomerModel customer)
        {
            var result = await this.update.Last().Handler(customer);
            return Ok(result);
        }
    }
}