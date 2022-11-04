using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace  CustomerManagment.DomainLayer.Models
{
    public partial class CustomerAddress : BaseEntity
    {

        [StringLength(500)]
        public string CustomerAddresses { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        public int CustomerId { get; set; }
    }
}
