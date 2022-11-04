using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerManagment.DomainLayer.DTO
{
    public  class CustomerAddressDTO
    {

        public int Id { get; set; }

        [StringLength(500)]
        public string CustomerAddress { get; set; }

        public int CustomerId { get; set; }
    }
}
