
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
    public class CustomerServices : ICustomer
    {
        private readonly IRepository<Customer> _repository;
        private ICustomerAddress _ICustomerAddress;
        private readonly WenApiDbContext _db_context;
        public CustomerServices(IRepository<Customer> rep, ICustomerAddress ICustomerAddress, WenApiDbContext db_context)
        {
           
            this._repository = rep;
            this._db_context = db_context;
            this._ICustomerAddress = ICustomerAddress;
        }
        public Customer FromDTOtoModel(CustomerDTO Dto)
        {
            Customer Model = new Customer();
            Model.Id = Dto.Id;
            Model.CustomerName = Dto.CustomerName;
            Model.MotherName = Dto.MotherName;
            Model.FatherName = Dto.FatherName;
            if(Dto.CustomerPhoto!=null)
            Model.CustomerPhoto = Dto.CustomerPhoto;
            Model.MaterialStatus = Dto.MaterialStatus;
            Model.CountryId = Dto.CountryId;
            return Model;
        }
       
        
        public CustomerDTO FromModeltoDTO(Customer Model)
        {
            CustomerDTO DTO = new CustomerDTO();
            DTO.Id = Model.Id;
            DTO.CustomerName = Model.CustomerName;
            DTO.MotherName = Model.MotherName;
            DTO.FatherName = Model.FatherName;
            DTO.CustomerPhoto = Model.CustomerPhoto;
            DTO.MaterialStatus = Model.MaterialStatus;
            DTO.CountryId = Model.CountryId;
            DTO.CustomerAddress = _ICustomerAddress.GetAllByCustomer(Model.Id).Data;
            return DTO;
        }


        public DynamicResponse<List<CustomerDTO>> GetAll()
        {
            DynamicResponse<List<CustomerDTO>> response = new DynamicResponse<List<CustomerDTO>>();

            try
            {
                List<Customer> ListModel = _repository.GetAllList();
                List<CustomerDTO> ListDto = new List<CustomerDTO>();
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

        public DynamicResponse<bool> Edit(CustomerDTO ToUpdate)
        {

            DynamicResponse<bool> response = new DynamicResponse<bool>();
            try
            {
                if (ToUpdate != null)
                {
                    //Get Model
                    Customer Model = FromDTOtoModel(ToUpdate);
                    if (Model != null)
                    {
                        //Update
                        Model.Country = _db_context.Country.Where(e => e.Id == Model.CountryId).FirstOrDefault();
                        _repository.Update(Model);
                        if (ToUpdate.CustomerAddresses != null )
                        {
                            if (ToUpdate.CustomerAddresses.Count != 0 && ToUpdate.CustomerAddresses[0]!=null)
                            {

                                DynamicResponse<bool> resp = _ICustomerAddress.AddList(ToUpdate.CustomerAddresses, Model.Id);
                                if (resp.HttpStatusCode != HttpStatusCode.OK)
                                {
                                    response.HttpStatusCode = HttpStatusCode.InternalServerError;
                                    response.Message = "Please try again later";
                                    return response;
                                }
                            }
                        }
                        response.Data = true;
                        response.HttpStatusCode = HttpStatusCode.OK;
                    }
                }

                response.Data = false;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = "Customer not found";
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
                    Customer Model = _repository.GetById(Id);
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
                response.Message = "Customer not found";
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


        public DynamicResponse<CustomerDTO> Get(int Id)
        {
            DynamicResponse<CustomerDTO> response = new DynamicResponse<CustomerDTO>();

            try
            {
                if (Id != 0)
                {
                    Customer Model = _repository.GetById(Id);
                    if (Model != null)
                    {
                        response.Data = FromModeltoDTO(Model);
                        response.HttpStatusCode = HttpStatusCode.OK;
                        return response;
                    }
                }

                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = "Customer not found";
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

       


        public DynamicResponse<int> Add(CustomerDTO ToAdd)
        {
            DynamicResponse<int> response = new DynamicResponse<int>();

            try
            {
                if (ToAdd != null)
                {
                    
                    Customer Model = FromDTOtoModel(ToAdd);
                    //Add Model
                    _repository.Insert(Model);
                    response.Data = Model.Id;
                    if (ToAdd.CustomerAddresses !=null) { 
                    if (ToAdd.CustomerAddresses.Count != 0)
                    {
                       
                        DynamicResponse<bool> resp= _ICustomerAddress.AddList(ToAdd.CustomerAddresses,Model.Id);
                        if (resp.HttpStatusCode != HttpStatusCode.OK)
                        {
                            response.HttpStatusCode = HttpStatusCode.InternalServerError;
                            response.Message = "Please try again later";
                            return response;
                        }
                    }
                    }
                    response.HttpStatusCode = HttpStatusCode.OK;
                }
                else
                {
                    response.Data = 0;
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
