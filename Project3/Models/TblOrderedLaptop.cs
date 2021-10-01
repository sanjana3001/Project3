using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Project3.Models
{
    public partial class TblOrderedLaptop
    {
        
        
       
        public int Oid { get; set; }
        public int? Lid { get; set; }
        public int? SellerId { get; set; }
        public int? CustomerId { get; set; }

        public virtual TblCustomer Customer { get; set; }
        public virtual AvailableLaptop L { get; set; }
        public virtual TblSeller Seller { get; set; }
    }
}
