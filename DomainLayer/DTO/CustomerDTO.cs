using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerManagment.DomainLayer.DTO
{
    public  class CustomerDTO
    {
    
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string CustomerName { get; set; }
        [Required]
        [StringLength(100)]
        public string FatherName { get; set; }
        [Required]
        [StringLength(100)]
        public string MotherName { get; set; }
        public List<string> CustomerAddresses { get; set; }
        public List<CustomerAddressDTO> CustomerAddress { get; set; }
        [Required]
        public int MaterialStatus { get; set; }
        public string CustomerPhotoBase64 { get; set; }
        public IFormFile CustomerPhotoToUpload { get; set; }
        public byte[] CustomerPhoto { get; set; }

        public int CountryId { get; set; }
    }
}
