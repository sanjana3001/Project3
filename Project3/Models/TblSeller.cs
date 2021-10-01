using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Project3.Models
{
    public partial class TblSeller
    {
        public TblSeller()
        {
            AvailableLaptop = new HashSet<AvailableLaptop>();
            TblOrderedLaptop = new HashSet<TblOrderedLaptop>();
        }

        public int Sid { get; set; }
        public string Sname { get; set; }
        public string Spass { get; set; }
        public string Scontact { get; set; }
        public string Semail { get; set; }

        public virtual ICollection<AvailableLaptop> AvailableLaptop { get; set; }
        public virtual ICollection<TblOrderedLaptop> TblOrderedLaptop { get; set; }
    }
}
