using CustomerManagment.DomainLayer.CommonObjects;
using CustomerManagment.DomainLayer.DTO;
using System.Collections.Generic;

namespace CustomerManagment.ServiceLayer.Interface
{
    public interface ICustomerAddress
    {
        DynamicResponse<List<CustomerAddressDTO>>  GetAll();
        DynamicResponse<List<CustomerAddressDTO>> GetAllByCustomer(int CustmerId);
        DynamicResponse<CustomerAddressDTO> Get(int Id);
        DynamicResponse<bool> Add(CustomerAddressDTO ToAdd);
        DynamicResponse<bool> AddList(List<string> ToAdd,int Customer);
        DynamicResponse<bool> Edit(CustomerAddressDTO ToUpdate);
        DynamicResponse<bool> Delete(int Id);
    }
}
