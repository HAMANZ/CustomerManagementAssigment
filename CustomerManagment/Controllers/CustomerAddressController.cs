using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using CustomerManagment.DomainLayer.CommonObjects;
using CustomerManagment.DomainLayer.DTO;
using CustomerManagment.ServiceLayer.Interface;
using System;
using System.Net;

namespace CustomerManagment.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")] 
    [ApiController]
    public class CustomerAddressController : ControllerBase
    {
        private IConfiguration _configuration;
        private readonly ICustomerAddress _ICustomerAddress;
        public CustomerAddressController(IConfiguration configuration, ICustomerAddress ICustomerAddress)
        {
            this._configuration = configuration;
            this._ICustomerAddress = ICustomerAddress;
        }

        
        [HttpPost]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            var response = _ICustomerAddress.GetAll();
            if (response.HttpStatusCode !=HttpStatusCode.OK)
            { return BadRequest(response); }
            return Ok(response);
        }



        // [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        
        [HttpPost]
        [Route("AddCustomerAddress")]
        public IActionResult Add([FromBody] CustomerAddressDTO toAdd)
        {
            var response = _ICustomerAddress.Add(toAdd);
            if (response.HttpStatusCode !=HttpStatusCode.OK) 
            { return BadRequest(response); }
            return Ok(response);
        }

        
        [HttpPost]
        [Route("Get")]
        public IActionResult Get([FromBody] int Id)
        {
            var response = _ICustomerAddress.Get(Id);
            if (response.HttpStatusCode !=HttpStatusCode.OK) 
            { return BadRequest(response); }
            return Ok(response);
        }

        
        [HttpPost]
        [Route("Edit")]
        public DynamicResponse<bool> Edit([FromBody] CustomerAddressDTO toUpdate)
        {
            var response = _ICustomerAddress.Add(toUpdate);
            if (response.HttpStatusCode != HttpStatusCode.OK) 
            { return response; }
            return response;
        }



        
        [HttpGet]
        [Route("Delete")]
        public DynamicResponse<bool> Delete( int Id)
        {
            var response = _ICustomerAddress.Delete(Id);
            if (response.HttpStatusCode !=HttpStatusCode.OK)
            { return response; }
            return response;
        }



    }
}