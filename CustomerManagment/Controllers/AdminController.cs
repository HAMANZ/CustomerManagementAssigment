using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using CustomerManagment.DomainLayer.CommonObjects;
using CustomerManagment.DomainLayer.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace CustomerManagment.Controllers
{
    public class AdminController: Controller
    {
     
       // [CheckSession]
        public IActionResult Index(int?Id)
        {
            CustomManageCustomerDTOerDTO Model=new CustomManageCustomerDTOerDTO();
            Model.List = GetAllCustomers();
            Model.Country = GetAllCountries();
            if(Id!=null )
            Model.Customer = GetCustomer((int)Id);
            return View(Model);
        }   
       
        public IActionResult Error()
        {
            return View();
        }   
       
        [HttpPost]
        public IActionResult Save(CustomerDTO ToAdd)
        {
            try
            {
                DynamicResponse<List<CustomerDTO>> customers = new DynamicResponse<List<CustomerDTO>>();
                string apiUrl = "https://localhost:44354/api/Customer/AddCustomer";

                HttpClient client = new HttpClient();
                if (ToAdd.CustomerPhotoToUpload != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        ToAdd.CustomerPhotoToUpload.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        ToAdd.CustomerPhotoBase64 = Convert.ToBase64String(fileBytes);
                        ToAdd.CustomerPhotoToUpload = null;
                        // act on the Base64 data
                    }
                }

                HttpContent body = new StringContent(JsonConvert.SerializeObject(ToAdd), Encoding.UTF8, "application/json");
                var response = client.PostAsync(apiUrl, body).Result;
                if (response.IsSuccessStatusCode)
                {
                    return Redirect("Index");
                }
                return Redirect("Error");
            }
            catch (Exception ex)
            {
                return Redirect("Error");
            }
            
        }
       
         [HttpPost]
        public IActionResult Edit(CustomerDTO ToEdit)
        {
            try
            {
                DynamicResponse<List<CustomerDTO>> customers = new DynamicResponse<List<CustomerDTO>>();
                string apiUrl = "https://localhost:44354/api/Customer/Edit";

                HttpClient client = new HttpClient();
                if (ToEdit.CustomerPhotoToUpload != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        ToEdit.CustomerPhotoToUpload.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        ToEdit.CustomerPhotoBase64 = Convert.ToBase64String(fileBytes);
                        ToEdit.CustomerPhotoToUpload = null;
                        // act on the Base64 data
                    }
                }

                HttpContent body = new StringContent(JsonConvert.SerializeObject(ToEdit), Encoding.UTF8, "application/json");
                var response = client.PostAsync(apiUrl, body).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Admin", new { Id = ToEdit.Id});
                }
                return Redirect("Error");
            }
            catch (Exception ex)
            {
                return Redirect("Error");
            }
            
        }
       
        [HttpGet]
        public IActionResult EditAddress(string Address,int CustomerId,int Id)
        {
            try
            {
                DynamicResponse<List<CustomerDTO>> customers = new DynamicResponse<List<CustomerDTO>>();
                string apiUrl = "https://localhost:44354/api/CustomerAddress/Edit";
                CustomerAddressDTO DTO = new CustomerAddressDTO();
                DTO.CustomerAddress = Address;
                DTO.CustomerId = CustomerId;
                DTO.Id = Id;
                HttpClient client = new HttpClient();
               
                HttpContent body = new StringContent(JsonConvert.SerializeObject(DTO), Encoding.UTF8, "application/json");
                var response = client.PostAsync(apiUrl, body).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Admin", new { Id = CustomerId });
                }
                return Redirect("Error");
            }
            catch (Exception ex)
            {
                return Redirect("Error");
            }
            
        }
       
        
        public IActionResult Login()
        {
            HttpContext.Session.SetString("Id", "acc1");
            HttpContext.Session.SetString("AdminId", "acc1");
            return View("Index");
        }
        private static List<CustomerDTO>  GetAllCustomers()
        {
            DynamicResponse<List<CustomerDTO>> customers = new DynamicResponse<List<CustomerDTO>>();
            string apiUrl = "https://localhost:44354/api/Customer";

            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(apiUrl + string.Format("/GetAll")).Result;
            if (response.IsSuccessStatusCode)
            {
                customers = JsonConvert.DeserializeObject<DynamicResponse<List<CustomerDTO>>>(response.Content.ReadAsStringAsync().Result);
            }

            return customers.Data;
        }
        
        private static List<CountryDTO>  GetAllCountries()
        {
            try{
                DynamicResponse<List<CountryDTO>> countries = new DynamicResponse<List<CountryDTO>>();
            string apiUrl = "https://localhost:44354/api/Country";

            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(apiUrl + string.Format("/GetAll")).Result;
            if (response.IsSuccessStatusCode)
            {
                countries = JsonConvert.DeserializeObject<DynamicResponse<List<CountryDTO>>>(response.Content.ReadAsStringAsync().Result);
            }

            return countries.Data;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        
        private static CustomerDTO  GetCustomer(int Id)
        {
            try{
            
             DynamicResponse<CustomerDTO> list = new DynamicResponse<CustomerDTO>();
            string apiUrl = "https://localhost:44354/api/Customer";

            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(apiUrl + string.Format("/Get")+ "?Id=" + Id.ToString()).Result;
            if (response.IsSuccessStatusCode)
            {
                    list = JsonConvert.DeserializeObject<DynamicResponse<CustomerDTO>>(response.Content.ReadAsStringAsync().Result);
            }

            return list.Data;
             }
            catch (Exception ex)
            {
                throw;
            }
        }
        
        
        public IActionResult   Get(int Id)
        {
            try{
            
             DynamicResponse<CustomerDTO> list = new DynamicResponse<CustomerDTO>();
            string apiUrl = "https://localhost:44354/api/Customer";

            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(apiUrl + string.Format("/Get")+ "?Id=" + Id.ToString()).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Admin", new { Id = Id });
                }
                return Redirect("Error");
            }
            catch (Exception ex)
            {
                return Redirect("Error");
            }
        }


        [HttpPost]
        public IActionResult   Delete(int Id)
        {
            try
            {
                DynamicResponse<CustomerDTO> countries = new DynamicResponse<CustomerDTO>();
                string apiUrl = "https://localhost:44354/api/Customer";

                HttpClient client = new HttpClient();
                HttpResponseMessage response = client.GetAsync(apiUrl + string.Format("/Delete") + "?Id=" + Id.ToString()).Result;
                if (response.IsSuccessStatusCode)
                {
                    return Redirect("Index");
                }
                return Redirect("Error");
            }
            catch (Exception ex)
            {
                return Redirect("Error");
            }
        }
        
        [HttpGet]
        public IActionResult   DeleteAddress(int Id,int CustomerId)
        {
            try
            {
                DynamicResponse<CustomerDTO> countries = new DynamicResponse<CustomerDTO>();
                string apiUrl = "https://localhost:44354/api/CustomerAddress";

                HttpClient client = new HttpClient();
                HttpResponseMessage response = client.GetAsync(apiUrl + string.Format("/Delete") + "?Id=" + Id.ToString()).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Admin", new { Id = CustomerId });
                }
                return Redirect("Error");
            }
            catch (Exception ex)
            {
                return Redirect("Error");
            }
        }


        

    }
}