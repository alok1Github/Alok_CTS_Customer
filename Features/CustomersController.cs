﻿using System;
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
        public async Task<IActionResult> GetCustomers()
        {
            var result = await this.getCustomer.Handler();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CustomerModel customer)
        {
            // To do's
            // 1. propery error handling , 2 return right code 3. versioing in API 4. looging
            var result = await this.getCustomer.Handler();
            return Ok(result);
        }
    }
}