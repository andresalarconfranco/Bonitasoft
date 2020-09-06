using System;
using System.Collections.Generic;

namespace API_SECURITY.Models
{
    public partial class PicUsers
    {
        public PicUsers()
        {
            PicRoleUser = new HashSet<PicRoleUser>();
        }

        public int UseNcode { get; set; }
        public string UseCname { get; set; }
        public string UseCsurname { get; set; }
        public string UseCemail { get; set; }
        public string UseCpassword { get; set; }
        public string UseCaddress { get; set; }
        public string UseCcountry { get; set; }
        public string UseCcity { get; set; }

        public virtual ICollection<PicRoleUser> PicRoleUser { get; set; }
    }
}
