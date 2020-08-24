using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Model.Model
{
    public partial class ShigongEnclosure
    {
        public System.Guid Id { get; set; }
        public System.Guid ShigongId { get; set; }
        public string EnclosureName { get; set; }
        public string EnclosureUrl { get; set; }

        public virtual Shigong Shigong { get; set; }
    }
}
