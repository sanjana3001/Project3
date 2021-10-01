using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Project3.Models
{
    public partial class TblAdmin
    {   [Key]
        public int? Aid { get; set; }
        public string Aname { get; set; }
        public string Apass { get; set; }
        public string Acontact { get; set; }
        public string Aemail { get; set; }
    }
}
