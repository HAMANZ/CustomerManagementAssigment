using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerManagment.DomainLayer.DTO
{
    public  class CustomManageCustomerDTOerDTO
    {
    
        public List<CustomerDTO> List { get; set; }
        public List<CountryDTO> Country { get; set; }
        public CustomerDTO Customer { get; set; }
    }
}
