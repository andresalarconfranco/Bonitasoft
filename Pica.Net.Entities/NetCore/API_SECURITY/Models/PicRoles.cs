using System;
using System.Collections.Generic;

namespace API_SECURITY.Models
{
    public partial class PicRoles
    {
        public PicRoles()
        {
            PicRoleUser = new HashSet<PicRoleUser>();
        }

        public int RolNcode { get; set; }
        public string RolCdescription { get; set; }

        public virtual ICollection<PicRoleUser> PicRoleUser { get; set; }
    }
}
