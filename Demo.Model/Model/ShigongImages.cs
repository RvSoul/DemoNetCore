using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Model.Model
{
    public partial class ShigongImages
    {
        public System.Guid Id { get; set; }
        public System.Guid ShigongId { get; set; }
        public string ImgUrl { get; set; }

        public virtual Shigong Shigong { get; set; }
    }
}
