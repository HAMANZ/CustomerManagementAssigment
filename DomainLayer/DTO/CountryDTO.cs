using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerManagment.DomainLayer.DTO
{
    public  class CountryDTO
    {
    
        public int Id { get; set; }
        public string CountryName { get; set; }
    }
}
