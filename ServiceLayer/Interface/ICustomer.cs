using CustomerManagment.DomainLayer.CommonObjects;
using CustomerManagment.DomainLayer.DTO;
using System.Collections.Generic;

namespace CustomerManagment.ServiceLayer.Interface
{
    public interface ICustomer
    {
        DynamicResponse<List<CustomerDTO>>  GetAll();
        DynamicResponse<CustomerDTO> Get(int Id);
        DynamicResponse<int> Add(CustomerDTO ToAdd);
        DynamicResponse<bool> Edit(CustomerDTO ToUpdate);
        DynamicResponse<bool> Delete(int Id);
    }
}
