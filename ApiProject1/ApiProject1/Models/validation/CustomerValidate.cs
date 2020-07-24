using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ApiProject1.Models
{
    [MetadataType(typeof(Customer.MetaData))]
    public partial class Customer
    {
        
            sealed class MetaData
        {
            [Required]
            public string UserName;
            [Required]
            public string Password;

        }
    }
            
        
         
}