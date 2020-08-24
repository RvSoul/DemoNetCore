using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Model.Model
{
    public partial class JianliEnclosure
    {
        public System.Guid Id { get; set; }
        public System.Guid JianliId { get; set; }
        public string EnclosureName { get; set; }
        public string EnclosureUrl { get; set; }

        public virtual Jianli Jianli { get; set; }
    }
}
