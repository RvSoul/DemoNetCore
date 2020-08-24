using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.DTO.BasicsDataDTO
{
    public class BiaoduanModel
    {
        public System.Guid Id { get; set; }
        public Nullable<System.Guid> XianluId { get; set; }
        public string BiaoduanName { get; set; }
    }
    public class BiaoduanDTO : BiaoduanModel
    {
    }
}
