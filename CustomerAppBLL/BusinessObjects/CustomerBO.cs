using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CustomerAppBLL.BusinessObjects
{
    public class CustomerBO
    {
        public int Id { get; set; }

        public string FullName 
        { 
            get { return $"{FirstName} {LastName}"; } 
        }

        [Required]
        [MinLength(2)]
        [MaxLength(20)]
        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public List<int> AddressIds { get; set; }
        public List<AddressBO> Addresses { get; set; }

        //public List<OrderBO> Orders { get; set; }
    }
}
