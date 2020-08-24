using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.DTO.BasicsDataDTO
{
    public class GongdianModel
    {
        public System.Guid Id { get; set; }
        public Nullable<System.Guid> BiaoduanID { get; set; }
        public string GongdianName { get; set; }
    }
    public class GongdianDTO : GongdianModel
    {
    }
}
