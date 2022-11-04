using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.RespositoryPattern;
using CustomerManagment.DomainLayer.CommonObjects;
using CustomerManagment.DomainLayer.DTO;
using CustomerManagment.DomainLayer.Models;
using CustomerManagment.ServiceLayer.Interface;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace CustomerManagment.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")] 
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private IConfiguration _configuration;
        private readonly ICustomer _ICustomer;
        private readonly ICustomerAddress _ICustomerAddress;
        private readonly IRepository<Customer> _repository;
        
        public CustomerController(IRepository<Customer> repository,IConfiguration configuration, ICustomer ICustomer, ICustomerAddress ICustomerAddress)
        {
            this._configuration = configuration;
            this._ICustomer = ICustomer;
            this._ICustomerAddress = ICustomerAddress;
            this._repository = repository;
        }

        
        [HttpGet]
        [Route("GetAll")]
        public DynamicResponse<List<CustomerDTO>> GetAll()
        {
            var response = _ICustomer.GetAll();
            //if (response.HttpStatusCode !=HttpStatusCode.OK)
            //{ return BadRequest(response); }
            return response;
        }



        // [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        
        [HttpPost]
        [Route("AddCustomer")]
        public DynamicResponse<int> Add([FromBody] CustomerDTO toAdd)
        {
            if (toAdd.CustomerPhotoBase64 != null)
            {
                byte[] decodedByteArray = Convert.FromBase64String(toAdd.CustomerPhotoBase64);
                toAdd.CustomerPhoto = decodedByteArray;
            }
            var response = _ICustomer.Add(toAdd);
            
            //if (response.HttpStatusCode !=HttpStatusCode.OK) 
            //{ return BadRequest(response); }
            return response;
        }

        
        [HttpGet]
        [Route("Get")]
        public IActionResult Get(int Id)
        {
            var response = _ICustomer.Get(Id);
            if (response.HttpStatusCode !=HttpStatusCode.OK) 
            { return BadRequest(response); }
            if (response.Data.CustomerPhoto != null)
            {
                response.Data.CustomerPhotoBase64 = Convert.ToBase64String(response.Data.CustomerPhoto);
            }
            return Ok(response);
        }

        
        [HttpPost]
        [Route("Edit")]
        public DynamicResponse<bool> Edit([FromBody] CustomerDTO toUpdate)
        {
            if (toUpdate.CustomerPhotoBase64 != null)
            {
                byte[] decodedByteArray = Convert.FromBase64String(toUpdate.CustomerPhotoBase64);
                toUpdate.CustomerPhoto = decodedByteArray;
            }
            var response = _ICustomer.Edit(toUpdate);
            if (response.HttpStatusCode != HttpStatusCode.OK) 
            { return response; }
            return response;
        }



        
        [HttpGet]
        [Route("Delete")]
        public DynamicResponse<bool> Delete(int Id)
        {
            var response = _ICustomer.Delete(Id);
            if (response.HttpStatusCode !=HttpStatusCode.OK)
            { return response; }
            return response;
        }



    }
}