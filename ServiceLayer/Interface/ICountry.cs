using CustomerManagment.DomainLayer.CommonObjects;
using CustomerManagment.DomainLayer.DTO;
using System.Collections.Generic;

namespace CustomerManagment.ServiceLayer.Interface
{
    public interface ICountry
    {
        DynamicResponse<List<CountryDTO>>  GetAll();
        DynamicResponse<CountryDTO> Get(int Id);
        DynamicResponse<bool> Add(CountryDTO ToAdd);
        DynamicResponse<bool> Edit(CountryDTO ToUpdate);
        DynamicResponse<bool> Delete(int Id);
    }
}
