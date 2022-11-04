
using RepositoryLayer.RespositoryPattern;
using CustomerManagment.DomainLayer.CommonObjects;
using CustomerManagment.DomainLayer.DTO;
using  CustomerManagment.DomainLayer.Models;
using CustomerManagment.ServiceLayer.Interface;
using System;
using System.Collections.Generic;
using System.Net;

namespace CustomerManagment.ServiceLayer.Implementation
{
    public class CountryServices : ICountry
    {
        private readonly IRepository<Country> _repository;

        public CountryServices(IRepository<Country> rep)
        {
           
            this._repository = rep;
        }
        public Country FromDTOtoModel(CountryDTO Dto)
        {
            Country Model = new Country();
            Model.Id = Dto.Id;
            Model.CountryName = Dto.CountryName;
            return Model;
        }
       
        
        public CountryDTO FromModeltoDTO(Country Model)
        {
            CountryDTO DTO = new CountryDTO();
            DTO.Id = Model.Id;
            DTO.CountryName = Model.CountryName;
            return DTO;
        }


        public DynamicResponse<List<CountryDTO>> GetAll()
        {
            DynamicResponse<List<CountryDTO>> response = new DynamicResponse<List<CountryDTO>>();

            try
            {
                List<Country> ListModel = _repository.GetAllList();
                List<CountryDTO> ListDto = new List<CountryDTO>();
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

        public DynamicResponse<bool> Edit(CountryDTO ToUpdate)
        {

            DynamicResponse<bool> response = new DynamicResponse<bool>();
            try
            {
                if (ToUpdate == null)
                {
                    //Get Model
                    Country Model = _repository.GetById(ToUpdate.Id);
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
                response.Message = "Country not found";
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
                    Country Model = _repository.GetById(Id);
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
                response.Message = "Country not found";
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


        public DynamicResponse<CountryDTO> Get(int Id)
        {
            DynamicResponse<CountryDTO> response = new DynamicResponse<CountryDTO>();

            try
            {
                if (Id != 0)
                {
                    Country Model = _repository.GetById(Id);
                    if (Model != null)
                    {
                        response.Data = FromModeltoDTO(Model);
                        response.HttpStatusCode = HttpStatusCode.OK;
                        return response;
                    }
                }

                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = "Country not found";
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

       


        public DynamicResponse<bool> Add(CountryDTO ToAdd)
        {
            DynamicResponse<bool> response = new DynamicResponse<bool>();

            try
            {
                if (ToAdd != null)
                {
                    
                    Country Model = FromDTOtoModel(ToAdd);
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

    }
}
