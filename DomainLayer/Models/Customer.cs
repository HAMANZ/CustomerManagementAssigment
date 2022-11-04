using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace  CustomerManagment.DomainLayer.Models
{
    public partial class Customer : BaseEntity
    {

        [StringLength(100)]
        public string CustomerName { get; set; }
        
        [StringLength(100)]
        public string FatherName { get; set; }
        [StringLength(100)]
        public string MotherName { get; set; }
        public int MaterialStatus { get; set; }
      
        public byte[] CustomerPhoto { get; set; }

        public int CountryId { get; set; }
        [ForeignKey("CountryId")]
        public Country Country { get; set; }

    }
}
