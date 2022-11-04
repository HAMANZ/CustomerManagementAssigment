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

namespace CustomerManagment.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")] 
    [ApiController]
    public class CountryController : ControllerBase
    {
        private IConfiguration _configuration;
        private readonly ICountry _ICountry;
        private readonly IRepository<Country> _repository;
        public CountryController(IRepository<Country> repository,IConfiguration configuration, ICountry ICountry)
        {
            this._configuration = configuration;
            this._ICountry = ICountry;
            this._repository = repository;
        }

        
        [HttpGet]
        [Route("GetAll")]
        public DynamicResponse<List<CountryDTO>> GetAll()
        {
            var response = _ICountry.GetAll();
            //if (response.HttpStatusCode !=HttpStatusCode.OK)
            //{ return BadRequest(response); }
            return response;
        }



        // [Authorize   (AuthenticationSchemes = "Bearer", Roles = "Admin")]
        
        [HttpPost]
        [Route("AddCountry")]
        public IActionResult Add([FromBody] CountryDTO toAdd)
        {
            var response = _ICountry.Add(toAdd);
            if (response.HttpStatusCode !=HttpStatusCode.OK) 
            { return BadRequest(response); }
            return Ok(response);
        }

        
        [HttpPost]
        [Route("Get")]
        public IActionResult Get([FromBody] int Id)
        {
            var response = _ICountry.Get(Id);
            if (response.HttpStatusCode !=HttpStatusCode.OK) 
            { return BadRequest(response); }
            return Ok(response);
        }

        
        [HttpPost]
        [Route("Edit")]
        public IActionResult Edit([FromBody] CountryDTO toUpdate)
        {
            var response = _ICountry.Add(toUpdate);
            if (response.HttpStatusCode != HttpStatusCode.OK) 
            { return BadRequest(response); }
            return Ok(response);
        }



        
        [HttpPost]
        [Route("Delete")]
        public IActionResult Delete([FromBody] int Id)
        {
            var response = _ICountry.Delete(Id);
            if (response.HttpStatusCode !=HttpStatusCode.OK)
            { return BadRequest(response); }
            return Ok(response);
        }



    }
}