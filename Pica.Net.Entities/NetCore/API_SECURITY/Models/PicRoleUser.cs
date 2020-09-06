using System;
using System.Collections.Generic;

namespace API_SECURITY.Models
{
    public partial class PicRoleUser
    {
        public int RusNcode { get; set; }
        public int? UseNcode { get; set; }
        public int? RolNcode { get; set; }

        public virtual PicRoles RolNcodeNavigation { get; set; }
        public virtual PicUsers UseNcodeNavigation { get; set; }
    }
}
