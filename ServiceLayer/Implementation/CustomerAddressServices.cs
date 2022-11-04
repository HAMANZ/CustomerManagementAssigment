
using RepositoryLayer.RespositoryPattern;
using CustomerManagment.DomainLayer.CommonObjects;
using CustomerManagment.DomainLayer.DTO;
using  CustomerManagment.DomainLayer.Models;
using CustomerManagment.ServiceLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace CustomerManagment.ServiceLayer.Implementation
{
    public class CustomerAddressServices : ICustomerAddress
    {
        private readonly IRepository<CustomerAddress> _repository;
        private  WenApiDbContext _db_context;

        public CustomerAddressServices(IRepository<CustomerAddress> rep, WenApiDbContext db_context)
        {
           
            this._repository = rep;
            this._db_context = db_context;
        }
        public CustomerAddress FromDTOtoModel(CustomerAddressDTO Dto)
        {
            CustomerAddress Model = new CustomerAddress();
            Model.Id = Dto.Id;
            Model.CustomerAddresses = Dto.CustomerAddress;
            Model.CustomerId = Dto.CustomerId;
            return Model;
        }
       
        
        public CustomerAddressDTO FromModeltoDTO(CustomerAddress Model)
        {
            CustomerAddressDTO DTO = new CustomerAddressDTO();
            DTO.Id = Model.Id;
            DTO.CustomerAddress = Model.CustomerAddresses;
            DTO.CustomerId = Model.CustomerId;
            return DTO;
        }


        public DynamicResponse<List<CustomerAddressDTO>> GetAll()
        {
            DynamicResponse<List<CustomerAddressDTO>> response = new DynamicResponse<List<CustomerAddressDTO>>();

            try
            {
                List<CustomerAddress> ListModel = _repository.GetAllList();
                List<CustomerAddressDTO> ListDto = new List<CustomerAddressDTO>();
                if (ListModel.Count != 0)
                {
                    foreach (var item in ListModel)
                    {
                        ListDto.Add(FromModeltoDTO(item));
                    }
                }
                response.Data = ListDto;
                response.HttpStatusCode = HttpStatusCode.OK;

                return response;
            }
            catch (Exception ex)
            {
                response.HttpStatusCode = HttpStatusCode.InternalServerError;
                response.Message = "Please try again later";
                response.ServerMessage = ex.Message;

                return response;
            }
        }
        public DynamicResponse<List<CustomerAddressDTO>> GetAllByCustomer(int CustmerId)
        {
            DynamicResponse<List<CustomerAddressDTO>> response = new DynamicResponse<List<CustomerAddressDTO>>();

            try
            {
                List<CustomerAddress> ListModel = _db_context.CustomerAddress.Where(e=>e.CustomerId== CustmerId).ToList();
                List<CustomerAddressDTO> ListDto = new List<CustomerAddressDTO>();
                if (ListModel.Count != 0)
                {
                    foreach (var item in ListModel)
                    {
                        ListDto.Add(FromModeltoDTO(item));
                    }
                }
                response.Data = ListDto;
                response.HttpStatusCode = HttpStatusCode.OK;

                return response;
            }
            catch (Exception ex)
            {
                response.HttpStatusCode = HttpStatusCode.InternalServerError;
                response.Message = "Please try again later";
                response.ServerMessage = ex.Message;

                return response;
            }
        }

        public DynamicResponse<bool> Edit(CustomerAddressDTO ToUpdate)
        {

            DynamicResponse<bool> response = new DynamicResponse<bool>();
            try
            {
                if (ToUpdate == null)
                {
                    //Get Model
                    CustomerAddress Model = _repository.GetById(ToUpdate.Id);
                    if (Model != null)
                    {
                        Model = FromDTOtoModel(ToUpdate);
                        //Update
                        _repository.Update(Model);
                        response.Data = true;
                        response.HttpStatusCode = HttpStatusCode.OK;
                        return response;
                    }
                }

                response.Data = false;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = "Customer Address not found";
                return response;

              
            }
            catch (Exception ex)
            {
                response.HttpStatusCode = HttpStatusCode.InternalServerError;
                response.Message = "Please try again later";
                response.ServerMessage = ex.Message;

                return response;
            }
        }
       
        public DynamicResponse<bool> Delete(int Id)
        {

            DynamicResponse<bool> response = new DynamicResponse<bool>();

            try
            {
                if (Id != 0)
                {
                    CustomerAddress Model = _repository.GetById(Id);
                    if (Model != null)
                    {

                        _repository.Remove(Model);
                        response.Data = true;
                        response.HttpStatusCode = HttpStatusCode.OK;
                        return response;
                        
                    }

                }
                response.Data = false;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = "Customer Address not found";
                return response;

            }
            catch (Exception ex)
            {
                response.HttpStatusCode = HttpStatusCode.InternalServerError;
                response.Message = "Please try again later";
                response.ServerMessage = ex.Message;

                return response;
            }

        }


        public DynamicResponse<CustomerAddressDTO> Get(int Id)
        {
            DynamicResponse<CustomerAddressDTO> response = new DynamicResponse<CustomerAddressDTO>();

            try
            {
                if (Id != 0)
                {
                    CustomerAddress Model = _repository.GetById(Id);
                    if (Model != null)
                    {
                        response.Data = FromModeltoDTO(Model);
                        response.HttpStatusCode = HttpStatusCode.OK;
                        return response;
                    }
                }

                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = "Customer Address not found";
                return response;
            }
            catch (Exception ex)
            {
                response.HttpStatusCode = HttpStatusCode.InternalServerError;
                response.Message = "Please try again later";
                response.ServerMessage = ex.Message;

                return response;
            }
        }

       


        public DynamicResponse<bool> Add(CustomerAddressDTO ToAdd)
        {
            DynamicResponse<bool> response = new DynamicResponse<bool>();

            try
            {
                if (ToAdd != null)
                {
                    
                    CustomerAddress Model = FromDTOtoModel(ToAdd);
                    //Add Model
                    _repository.Insert(Model);
                    response.Data = true;
                    response.HttpStatusCode = HttpStatusCode.OK;
                }
                else
                {
                    response.Data = false;
                    response.HttpStatusCode = HttpStatusCode.NoContent;
                }

                return response;
            }
            catch (Exception ex)
            {
                response.HttpStatusCode = HttpStatusCode.InternalServerError;
                response.Message = "Please try again later";
                response.ServerMessage = ex.Message;

                return response;
            }
        }
        
        public DynamicResponse<bool> AddList(List<string> ToAdd,int CustomerId)
        {
            DynamicResponse<bool> response = new DynamicResponse<bool>();

            try
            {
                if (ToAdd != null && ToAdd.Count!=0)
                {
                    CustomerAddressDTO Model = new CustomerAddressDTO();
                    foreach (var item in ToAdd)
                    {

                        Model.CustomerAddress = item;
                        Model.CustomerId =CustomerId;
                        //Add Model
                        _repository.Insert(FromDTOtoModel(Model));
                        Model = new CustomerAddressDTO();

                    }
                   
                  
                    response.Data = true;
                    response.HttpStatusCode = HttpStatusCode.OK;
                }
                else
                {
                    response.Data = false;
                    response.HttpStatusCode = HttpStatusCode.NoContent;
                }

                return response;
            }
            catch (Exception ex)
            {
                response.HttpStatusCode = HttpStatusCode.InternalServerError;
                response.Message = "Please try again later";
                response.ServerMessage = ex.Message;

                return response;
            }
        }

    }
}
