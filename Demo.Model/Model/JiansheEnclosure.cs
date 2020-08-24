using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Model.Model
{
    public partial class JiansheEnclosure
    {
        public System.Guid Id { get; set; }
        public System.Guid JiansheId { get; set; }
        public string EnclosureName { get; set; }
        public string EnclosureUrl { get; set; }

        public virtual Jianshe Jianshe { get; set; }
    }
}
