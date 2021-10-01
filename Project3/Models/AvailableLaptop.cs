using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Project3.Models
{
    public partial class AvailableLaptop
    {
        public AvailableLaptop()
        {
            TblOrderedLaptop = new HashSet<TblOrderedLaptop>();
        }

        public int Lid { get; set; }
        public int? SellerId { get; set; }
        public string Lname { get; set; }
        public string Lmodel { get; set; }
        public double? Lprice { get; set; }

        public virtual TblSeller Seller { get; set; }
        public virtual ICollection<TblOrderedLaptop> TblOrderedLaptop { get; set; }
    }
}
