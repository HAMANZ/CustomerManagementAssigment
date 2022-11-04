using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace  CustomerManagment.DomainLayer.Models
{
    public partial class Country: BaseEntity
    {
        [StringLength(50)]
        public string CountryName { get; set; }
    }
}
