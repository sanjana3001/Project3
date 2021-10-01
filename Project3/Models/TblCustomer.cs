using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Project3.Models
{
    public partial class TblCustomer
    {
        public TblCustomer()
        {
            TblOrderedLaptop = new HashSet<TblOrderedLaptop>();
        }

        public int Cid { get; set; }
        public string Cname { get; set; }
        public string Cpass { get; set; }
        public string Ccontact { get; set; }
        public string Cemail { get; set; }

        public virtual ICollection<TblOrderedLaptop> TblOrderedLaptop { get; set; }
    }
}
